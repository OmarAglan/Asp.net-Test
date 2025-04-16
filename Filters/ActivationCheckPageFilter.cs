using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Roshta.Services.Interfaces;
using System.Threading.Tasks;

namespace Roshta.Filters;

public class ActivationCheckPageFilter : IAsyncPageFilter
{
    private readonly ILicenseService _licenseService;
    private readonly ILogger<ActivationCheckPageFilter> _logger;

    public ActivationCheckPageFilter(ILicenseService licenseService, ILogger<ActivationCheckPageFilter> logger)
    {
        _licenseService = licenseService;
        _logger = logger;
    }

    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        // This method runs before model binding. We don't need logic here for this filter.
        return Task.CompletedTask;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        // Get the path of the page being accessed
        string pagePath = context.ActionDescriptor.RelativePath;

        // Allow access to the Activation page itself without checking
        if (pagePath.Equals("/Pages/Activate.cshtml", StringComparison.OrdinalIgnoreCase))
        {
            await next(); // Proceed to the Activation page handler
            return;
        }

        // Check if the application is activated
        if (!_licenseService.IsActivated())
        {
            _logger.LogInformation("Application not activated. Redirecting to /Activate from {PagePath}.", pagePath);
            // Not activated, redirect to the activation page
            context.Result = new RedirectToPageResult("/Activate");
            return; // Short-circuit the pipeline
        }

        // Activated, proceed to the intended page handler
        await next();
    }
} 