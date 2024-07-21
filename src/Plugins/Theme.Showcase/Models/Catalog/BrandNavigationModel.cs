using Grand.Infrastructure.Models;

namespace Grand.Web.Models.Catalog;

public class BrandNavigationModel : BaseModel
{
    public IList<BrandBriefInfoModel> Brands { get; set; } = new List<BrandBriefInfoModel>();

    public int TotalBrands { get; set; }
}

public class BrandBriefInfoModel : BaseEntityModel
{
    public string Name { get; set; }
    public string SeName { get; set; }
    public string Icon { get; set; }
    public bool IsActive { get; set; }
}