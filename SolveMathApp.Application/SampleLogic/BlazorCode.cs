using Azure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SolveMathApp.Application.SampleLogic
{

	//MyBlazorApp/
	//├── Pages/                 # Razor pages (like Views)
	//│   ├── Index.razor
	//│   ├── Counter.razor
	//│   └── FetchData.razor
	//├── Shared/                # Shared components
	//│   ├── MainLayout.razor
	//│   └── NavMenu.razor
	//├── wwwroot/               # Static files (css, js, images)
	//├── Data/                  # Services or models
	//│   └── WeatherForecastService.cs
	//├── _Imports.razor         # Global using directives
	//├── Program.cs             # App entry point & DI setup
	//├── App.razor              # App root component
	//└── MyBlazorApp.csproj

	//blazor app project structure example. 

	//	Program.cs → Configures services and middleware

	//App.razor → Root component, defines the router

	//Pages/ → Components representing pages with routing

	//Shared/ → Components shared across pages



//	Absolutely! Let’s go ** deep into Blazor**, especially for **.NET Core / .NET 6+ developers**, so you understand it from** basics to advanced**, including** project structure, components, and common interview topics**.

//---

//# **1️⃣ What is Blazor?**

//** Blazor** is a** framework for building interactive web UIs using C# instead of JavaScript**.

//*** Full-stack C#**: You can write **frontend + backend in C#**
//*** Component-based**: Everything is a reusable component
//*** Runs in two modes**:

//  1. ** Blazor Server** → Runs on the server, updates the browser via** SignalR**
//  2. ** Blazor WebAssembly(WASM)** → Runs in the browser on** WebAssembly**

//---

//# **2️⃣ Blazor Project Structure**

//Example** Blazor Server project**:

//```
//MyBlazorApp/
//├── Pages/                 # Razor pages (like Views)
//│   ├── Index.razor
//│   ├── Counter.razor
//│   └── FetchData.razor
//├── Shared/                # Shared components
//│   ├── MainLayout.razor
//│   └── NavMenu.razor
//├── wwwroot/               # Static files (css, js, images)
//├── Data/                  # Services or models
//│   └── WeatherForecastService.cs
//├── _Imports.razor         # Global using directives
//├── Program.cs             # App entry point & DI setup
//├── App.razor              # App root component
//└── MyBlazorApp.csproj
//```

//** Key files:**

//* `Program.cs` → Configures services and middleware
//* `App.razor` → Root component, defines the router
//* `Pages/` → Components representing pages with routing
//* `Shared/` → Components shared across pages

//---

//# **3️⃣ Components**

//A** component** is a `.razor` file with** HTML + C#**:

//```razor
//<h3> Counter</h3>
//<p>Current count: @currentCount</p>
//<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>

//@code {

//	private int currentCount = 0;
//	private void IncrementCount()
//	{
//		currentCount++;
//	}
//}
//```

//* `@code { }` → C# code for the component
//* `@onclick` → Event binding

//**Key concepts**:

//***Parameters * *: Pass data to components

//```razor
//@code { [Parameter] public string Title { get; set; } }
//```

//***EventCallback * *: Raise events to parent components
//* **Lifecycle methods**: `OnInitialized`, `OnAfterRender`, `OnParametersSet`

//---

//# **4️⃣ Routing**

//* Defined using `@page` directive

//```razor
//@page "/counter"
//```

//* `App.razor` sets up the router:

//```razor
//<Router AppAssembly="@typeof(Program).Assembly">
//    <Found Context="routeData">
//        <RouteView RouteData="@routeData" />
//    </Found>
//    <NotFound>
//        <h1>Page not found</h1>
//    </NotFound>
//</Router>
//```

//---

//# **5️⃣ Dependency Injection in Blazor**

//* Services are registered in `Program.cs`:

//```csharp
//builder.Services.AddSingleton<WeatherForecastService>();
//```

//* Injected into components:

//```razor
//@inject WeatherForecastService ForecastService
//```

//* Scope options:

//  * **Singleton** → one instance for the app lifetime
//  * **Scoped** → one instance per user connection (Blazor Server)
//  * **Transient** → new instance every injection

//---

//# **6️⃣ Forms and Validation**

//```razor
//<EditForm Model="@userModel" OnValidSubmit="HandleSubmit">
//    <DataAnnotationsValidator />
//    <ValidationSummary />
//    <InputText @bind-Value="userModel.Name" />
//    <button type="submit">Submit</button>
//</EditForm>

//@code {
//    private UserModel userModel = new();
//    private void HandleSubmit() { /* save logic */ }
//}

//public class UserModel
//{
//    [Required]
//    public string Name { get; set; }
//}
//```

//* `EditForm` → Forms in Blazor
//* Supports **data annotations** for validation

//---

//# **7️⃣ State Management**

//* **Component state** → private variables
//* **Scoped services** → share state between pages/components
//* **LocalStorage / SessionStorage** → persist state in browser

//---

//# **8️⃣ JavaScript Interop**

//Blazor can call JS functions:

//```razor
//@inject IJSRuntime JS

//<button @onclick="CallJs">Click me</button>

//@code {
//    private async Task CallJs()
//    {
//        await JS.InvokeVoidAsync("alert", "Hello from JS");
//    }
//}
//```

//* Can also call Blazor from JS

//---

//# **9️⃣ Blazor Server vs WebAssembly**

//| Feature         | Blazor Server        | Blazor WebAssembly      |
//| --------------- | -------------------- | ----------------------- |
//| Where code runs | Server               | Browser                 |
//| Latency         | Requires network     | Runs offline in browser |
//| App size        | Small                | Large (~1-2MB download) |
//| State           | Server-side per user | Client-side             |

//---

//# **10️⃣ Common Interview Topics for Blazor**

//1. Component lifecycle (`OnInitialized`, `OnAfterRender`)
//2. Event handling and binding (`@onclick`, `@bind`)
//3. Dependency Injection in Blazor
//4. State management between pages/components
//5. Routing (`@page` directive)
//6. Forms and validation (`EditForm`, `DataAnnotations`)
//7. JS interop (`IJSRuntime`)
//8. Differences between **Blazor Server vs WebAssembly**
//9. How to handle authentication/authorization in Blazor
//10. Performance optimization (lazy loading, `RenderFragment`, partial rendering)

//---

//# **11️⃣ Advanced Blazor Topics**

//* **Dynamic components** → `DynamicComponent`
//* **RenderFragment** → pass UI templates as parameters
//* **Cascading parameters** → share data deeply across component tree
//* **Authentication / Authorization** → `AuthorizeView`, `AuthenticationStateProvider`
//* **Custom middleware for Blazor Server** → e.g., logging SignalR events
//* **Partial classes for components** → separate `.razor.cs` logic from markup

//---

//# **12️⃣ Example: Blazor + Clean Architecture**

//* `BlazorApp/Pages/Users.razor` → calls service from **Application layer**
//* `BlazorApp/Services/UserService.cs` → calls **IUnitOfWork** → Repository → Database
//* UI is fully **decoupled from data access**

//---

//# ✅ **Key Takeaways**

//* **Blazor = Component-based SPA framework in C#**
//* Can replace most JS frameworks for .NET devs
//* Supports **server-side and client-side execution**
//* Integrates tightly with **Dependency Injection, EF Core, and .NET services**
//* Must know **lifecycle, state management, routing, forms, JS interop** for interviews

 

}
