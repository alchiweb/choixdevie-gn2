using Grand.Business.Core.Extensions;
using Grand.Business.Core.Interfaces.Catalog.Brands;
using Grand.Domain.Catalog;
using Grand.Domain.Customers;
using Grand.Infrastructure.Caching;
using Grand.Web.Events.Cache;
using Grand.Web.Features.Models.Catalog;
using Grand.Web.Models.Catalog;
using MediatR;

namespace Grand.Web.Features.Handlers.Catalog;

public class GetBrandNavigationHandler : IRequestHandler<GetBrandNavigation, BrandNavigationModel>
{
    private readonly ICacheBase _cacheBase;
    private readonly CatalogSettings _catalogSettings;
    private readonly IBrandService _brandService;

    public GetBrandNavigationHandler(ICacheBase cacheBase,
        IBrandService brandService,
        CatalogSettings catalogSettings)
    {
        _cacheBase = cacheBase;
        _brandService = brandService;
        _catalogSettings = catalogSettings;
    }

    public async Task<BrandNavigationModel> Handle(GetBrandNavigation request,
        CancellationToken cancellationToken)
    {
        var cacheKey = string.Format(CacheKeyConst.COLLECTION_NAVIGATION_MODEL_KEY,
            request.CurrentBrandId, request.Language.Id, string.Join(",", request.Customer.GetCustomerGroupIds()),
            request.Store.Id);
        var cacheModel = await _cacheBase.GetAsync(cacheKey, async () =>
        {
            var currentBrand = await _brandService.GetBrandById(request.CurrentBrandId);
            var brands =
                await _brandService.GetAllBrands(pageSize: _catalogSettings.MaxCatalogPageSize,
                    storeId: request.Store.Id);
            var model = new BrandNavigationModel {
                TotalBrands = brands.TotalCount
            };

            foreach (var brand in brands)
            {
                var modelMan = new BrandBriefInfoModel {
                    Id = brand.Id,
                    Name = brand.GetTranslation(x => x.Name, request.Language.Id),
                    Icon = brand.Icon,
                    SeName = brand.GetSeName(request.Language.Id),
                    IsActive = currentBrand != null && currentBrand.Id == brand.Id
                };
                model.Brands.Add(modelMan);
            }

            return model;
        });
        return cacheModel;
    }
}