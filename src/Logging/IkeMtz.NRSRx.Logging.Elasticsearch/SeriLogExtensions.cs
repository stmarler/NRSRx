using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace IkeMtz.NRSRx.Core.Web
{
  public static class SeriLogExtensions
  {
    public static IHostBuilder UseLogging(this IHostBuilder hostBuilder)
    {
      return hostBuilder.UseSerilog();
    }
    public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder)
    {
#pragma warning disable CS0618 // Type or member is obsolete
      return webHostBuilder.UseSerilog();
#pragma warning restore CS0618 // Type or member is obsolete
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    internal static ILogger Logger { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// Sets up Console Logging only, leverages SeriLog sinks
    /// </summary>
    /// <param name="startup"></param>
    /// <param name="app"></param>
    public static ILogger SetupConsoleLogging(this CoreWebStartup startup, IApplicationBuilder? app)
    {
      _ = (app?.UseSerilog());
      return GetLogger(() => new LoggerConfiguration()
           .MinimumLevel.Debug()
           .Enrich.FromLogContext()
           .WriteTo.Console(theme: AnsiConsoleTheme.Code)
           .CreateLogger());
    }

    internal static ILogger GetLogger(Func<ILogger> loggerFactory)
    {
      if (Logger != null)
      {
        return Logger;
      }
      return Logger = Log.Logger = loggerFactory();
    }

    public static IApplicationBuilder? UseSerilog(this IApplicationBuilder app)
    {
      return app?.UseSerilogRequestLogging(options =>
      {
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
          diagnosticContext.Set("UserName", httpContext.User?.Identity?.Name);
          diagnosticContext.Set("RemoteIpAddress", httpContext.Connection?.RemoteIpAddress?.ToString());
        };
      });
    }
  }
}
