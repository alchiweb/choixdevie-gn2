using Grand.Domain.Catalog;
using Grand.Infrastructure;
using Grand.Web.Common.Components;
using Grand.Web.Features.Models.Catalog;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Grand.Web.Components;

public class BrandNavigationViewComponent : BaseViewComponent
{
    private readonly CatalogSettings _catalogSettings;
    private readonly IMediator _mediator;
    private readonly IWorkContext _workContext;

    public BrandNavigationViewComponent(
        IMediator mediator,
        IWorkContext workContext,
        CatalogSettings catalogSettings)
    {
        _mediator = mediator;
        _workContext = workContext;
        _catalogSettings = catalogSettings;
    }

    public async Task<IViewComponentResult> InvokeAsync(string currentBrandId)
    {
        if (_catalogSettings.MaxCatalogPageSize == 0)
            return Content("");

        var model = await _mediator.Send(new GetBrandNavigation {
            CurrentBrandId = currentBrandId,
            Customer = _workContext.CurrentCustomer,
            Language = _workContext.WorkingLanguage,
            Store = _workContext.CurrentStore
        });

        return !model.Brands.Any() ? Content("") : View(model);
    }
}