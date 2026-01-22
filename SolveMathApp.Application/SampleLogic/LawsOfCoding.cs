using Azure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using SolveMathApp.Domain.Entities;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SolveMathApp.Application.SampleLogic
{
//    Absolutely! Writing** high-performance, effective code** in C# (or any language) isn’t just about speed—it’s about **clarity, maintainability, and efficiency**. Here’s a **list of 10 “laws” or rules” you can follow**, with explanations and practical tips.

//---

//# **10 Laws of Performance-Effective Code**

//---

//## **1️⃣ Law of Simplicity**

//> Simple code is faster to understand, maintain, and optimize.

//* Avoid unnecessary abstractions or over-engineered patterns.
//* **Example:** Use `string.IsNullOrEmpty(name)` instead of complicated null checks.

//---

//## **2️⃣ Law of Value vs Reference Awareness**

//> Know how value types and reference types behave in memory.

//*** Value types** are copied on assignment → cheap for small data
//* **Reference types** live on heap → watch for allocations
//* Avoid unnecessary boxing/unboxing.

//```csharp
//int x = 5;
//	object obj = x; // Boxing, avoid in tight loops
//```

//---

//## **3️⃣ Law of Allocation Awareness**

//> Minimize unnecessary memory allocations.

//* Reuse objects when possible
//* Use `StringBuilder` instead of repeated string concatenation in loops

//```csharp
//var sb = new StringBuilder();
//for(int i=0;i<1000;i++)
//    sb.Append(i);
//string result = sb.ToString();
//```

//---

//## **4️⃣ Law of Algorithm Efficiency**

//> Choose the right algorithm; complexity matters more than micro-optimizations.

//* O(n log n) sort is better than O(n²)
//* Use dictionaries for ** fast lookup**, lists for ** sequential access**

//```csharp
//var dict = new Dictionary<int, string>();
//dict[1] = "One"; // O(1) lookup
//```

//---

//## **5️⃣ Law of Lazy Evaluation**

//> Delay work until it’s actually needed.

//* Use `yield return` in C# for streaming sequences
//* Use LINQ deferred execution

//```csharp
//IEnumerable<int> GetEvenNumbers(IEnumerable<int> numbers)
//{
//		foreach (var n in numbers)
//			if (n % 2 == 0)
//				yield return n;
//	}
//```

//---

//## **6️⃣ Law of Caching and Reuse**

//> Avoid recomputation; store frequently used results.

//* Cache DB results, API calls, or expensive computations
//* Use `MemoryCache` or `IMemoryCache` in ASP.NET Core

//```csharp
//services.AddMemoryCache();
//```

//---

//## **7️⃣ Law of Avoiding Premature Optimization**

//> First, make it work, then profile for bottlenecks.

//* Optimize ** after identifying slow parts**
//* Use ** BenchmarkDotNet** or profiling tools in Visual Studio

//---

//## **8️⃣ Law of Asynchronous and Parallel Efficiency**

//> Use async/await and parallelism for I/O-bound or CPU-bound tasks.

//* Avoid blocking threads for I/O
//* Use `Task.Run()` or `Parallel.For` for CPU-heavy workloads

//```csharp
//await HttpClient.GetStringAsync(url); // Async I/O
//```

//---

//## **9️⃣ Law of Minimal Dependencies**

//> Every dependency can impact performance and maintainability.

//* Avoid heavy frameworks or unnecessary layers
//* Prefer lightweight libraries and** direct API usage** when performance matters

//---

//## **🔟 Law of Continuous Monitoring**

//> Performance is not just coding—it’s testing and measuring.

//* Use logging, telemetry, and metrics
//* Monitor response times, memory, and CPU usage
//* Optimize based on real usage, not assumptions

//---

//# **Bonus Tips**

//* Avoid** large object allocations** in tight loops
//* Use** structs wisely**; too big structs hurt performance
//* Prefer ** Span<T> / Memory<T>** for high-performance memory operations
//* Minimize **LINQ in tight loops** if performance-critical
//* Use **async streams** (`IAsyncEnumerable`) for large datasets

//---

//✅ ** Summary:**

//> Write simple, memory-conscious, algorithmically efficient, and asynchronous-aware code.Profile before optimizing. Cache smartly and monitor continuously.

 

}
