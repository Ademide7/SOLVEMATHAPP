using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;
using static System.Net.Mime.MediaTypeNames;

namespace SolveMathApp.Application.SampleLogic
{
//	As a full-stack dev, Azure DevOps helps you:

//Version control for backend(.NET Core API) and frontend(Blazor/React/Angular)

//Continuous Integration(CI) – automatically build and test your app on every push

//Continuous Deployment(CD) – deploy to Azure App Service, Kubernetes, or other environments

//Environment management – Dev, QA, Staging, Production pipelines

//Collaboration – share code, track work, and manage backlog with Boards.



	//CI/CD pipeline steps for .NET + frontend apps 

    // Deploying Blazor / React / Angular + .NET Core API.



//	Perfect! Let’s go ** step by step** on how to ** deploy a.NET Core API project** and a** Blazor app(Server or WASM)**. I’ll cover ** Azure deployment** (the most common for .NET developers), plus some alternatives.

//---

//# **1️⃣ Deploying a .NET Core API Project**

//## **Option 1: Deploy to Azure App Service (Recommended)**

//### **Step 1: Publish the API**

//1. Open your API project in ** Visual Studio**.
//2. Right-click the project → ** Publish** → Select** Azure** → ** Azure App Service(Windows/Linux)**.
//3. Sign in to Azure and select/create an App Service.
//4. Select** Release configuration** (usually `Release`) → Publish.

//** Behind the scenes:** Visual Studio builds the project and uploads the `wwwroot` + DLLs to Azure.

//---

//### **Step 2: Configure App Settings**

//* In Azure Portal → Your App Service → ** Configuration**

//  * Add ** connection strings**
//  * Add ** app settings / environment variables**
//  * Ensure ** ASPNETCORE_ENVIRONMENT = Production * *

//---

//### **Step 3: Verify the API**

//*Browse the App Service URL → Should return JSON from your API.
//* Test endpoints with** Postman** or** Swagger**.

//---

//## **Option 2: Deploy via Azure DevOps Pipeline**

//* Use** CI/CD pipeline** to build and deploy automatically.

//```yaml
//# Example pipeline snippet
//steps:
//- task: UseDotNet@2
//  inputs:

//	packageType: 'sdk'

//	version: '7.x'
//- script: dotnet restore
//- script: dotnet build --configuration Release
//- script: dotnet publish -c Release -o $(Build.ArtifactStagingDirectory)
//- task: AzureWebApp@1
//  inputs:

//	azureSubscription: 'MyAzureConnection'

//	appName: 'MyApiApp'

//	package: '$(Build.ArtifactStagingDirectory)/**.zip'
//```

//* Automatically deploys on commit to `main`.

//---

//# **2️⃣ Deploying a Blazor App**

//Blazor comes in ** two flavors**, so deployment differs slightly.

//---

//## **A) Blazor Server App Deployment**

//**Blazor Server** runs on the server → needs** App Service or IIS**.

//### Steps:

//1. Open Blazor Server project → **Publish** → Azure App Service.
//2. Configure** App Service settings**:

//   * `DOTNET_ENVIRONMENT = Production`
//   * Enable WebSockets (SignalR uses it)
//3. Browse the App Service → App should work instantly.
//4. Optional: add** SSL / custom domain**.

//> Note: Server-side Blazor relies on **SignalR** → low latency network recommended.

//---

//## **B) Blazor WebAssembly (WASM) Deployment**

//**Blazor WASM** runs in the browser → can be **static hosted**.

//### Option 1: Azure Static Web Apps

//1. Create** Static Web App** in Azure portal.
//2. Connect to GitHub/Azure Repos.
//3. Configure build:

//   * `App location: ClientApp` (Blazor WASM folder)
//   * `Api location: optional` (if using Azure Functions)
//4. Git push triggers** build and deployment** automatically.

//### Option 2: Azure App Service

//1. Publish the project → outputs** wwwroot folder with index.html and .dlls**.
//2. Deploy `wwwroot` to App Service → set** web.config** for static hosting.
//3. API calls → must point to backend API hosted elsewhere(CORS enabled).

//---

//### **C) Blazor + API Combined Deployment**

//* If API and Blazor app are in the** same solution**, you can:

//  1. Host Blazor Server** with API in one App Service** → easier
//  2. Host Blazor WASM** separately**:


//	 * Frontend → Static Web App
//	 * API → App Service

//	 * Enable ** CORS** on API for frontend URL

//---

//# **3️⃣ Post-deployment Tasks**

//1. ** CORS for APIs**:

//```csharp
//builder.Services.AddCors(options =>
//{
//		options.AddPolicy("AllowBlazor", policy =>
//		{
//			policy.WithOrigins("https://yourblazorapp.azurestaticapps.net")
//				  .AllowAnyHeader()
//				  .AllowAnyMethod();
//		});
//	});
//```

//2. ** HTTPS**

//* Use ** Azure managed SSL** for App Service or Static Web App.

//3. **Monitoring**

//* Enable **Application Insights** for logs and telemetry.

//4. **Environment variables**

//* `ASPNETCORE_ENVIRONMENT` → Production/Development
//* Connection strings, API keys, secrets

//---

//# **4️⃣ Quick Summary Table**

//| App Type                  | Hosting Option                     | Notes                                    |
//| ------------------------- | ---------------------------------- | ---------------------------------------- |
//| .NET Core API             | Azure App Service                  | Publish via VS or DevOps                 |
//| Blazor Server             | Azure App Service                  | SignalR, WebSockets needed               |
//| Blazor WASM               | Azure Static Web App / App Service | Static files, API separate, CORS enabled |
//| Full-stack (API + Blazor) | App Service(Server)               | Single hosting, easiest                  |
//| Full-stack(API + WASM)   | Static Web App + API App Service   | Separate hosting, CORS required          |

 
	//             ┌─────────────────────┐
 //                │   Developer Push    │
 //                │   GitHub / Azure    │
 //                │   Repos / GitLab    │
 //                └─────────┬───────────┘
 //                          │
 //                          ▼
 //                ┌─────────────────────┐
 //                │   Continuous         │
 //                │   Integration(CI)   │
 //                └─────────┬───────────┘
 //                          │
 //          ┌───────────────┴───────────────┐
 //          │                               │
 //          ▼                               ▼
 //  ┌──────────────┐                 ┌───────────────┐
 //  │ Build .NET   │                 │ Build Frontend │
 //  │ Core API     │                 │ React/Angular/ │
 //  │ - Restore    │                 │ Blazor WASM    │
 //  │ - Build      │                 │ - Install deps │
 //  │ - Test       │                 │ - Lint / Tests │
 //  │ - Publish    │                 │ - Build / Dist │
 //  └─────┬────────┘                 └─────┬─────────┘
 //        │                                  │
 //        └───────────┬──────────────┬──────┘
 //                    ▼              ▼
 //          ┌─────────────────────────────┐
 //          │  Package Artifacts           │
 //          │ - API DLLs / config          │
 //          │ - Frontend static files      │
 //          └────────────┬────────────────┘
 //                       │
 //                       ▼
 //            ┌─────────────────────┐
 //            │ Continuous Deployment│
 //            │ (CD Pipeline)        │
 //            └─────────┬───────────┘
 //                      │
 //       ┌──────────────┴───────────────┐
 //       ▼                              ▼
 //┌──────────────┐               ┌───────────────┐
 //│ Deploy API   │               │ Deploy Frontend│
 //│ - Azure App  │               │ - Same server  │
 //│   Service    │               │ - CDN / S3     │
 //│ - IIS        │               │ - Static Web   │
 //│ - Docker     │               │   Hosting      │
 //└──────────────┘               └───────────────┘
 //       │                              │
 //       └───────────────┬──────────────┘
 //                       ▼
 //             ┌─────────────────────┐
 //             │  Verification /     │
 //             │  Smoke Tests        │
 //             └─────────┬───────────┘
 //                       │
 //                       ▼
 //             ┌─────────────────────┐
 //             │ Monitoring / Logging │
 //             │ - Application Insights│
 //             │ - Serilog / ELK       │
 //             └─────────────────────┘
   


}
