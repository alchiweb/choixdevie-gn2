using Grand.Web.Common.Themes;

namespace Theme.Showcase;

public class ShowcaseThemeView : IThemeView
{
    public string AreaName => "";
    public string ThemeName => "Showcase";

    public ThemeInfo ThemeInfo => new("Showcase theme (beta)", "~/Plugins/Theme.Showcase/Content/theme.jpg",
        "Minimal theme (beta)", false);

    public IEnumerable<string> GetViewLocations()
    {
        return new List<string> {
            "/Views/Showcase/{1}/{0}.cshtml",
            "/Views/Showcase/Shared/{0}.cshtml",
            "/Views/{1}/{0}.cshtml",
            "/Views/Shared/{0}.cshtml"
        };
    }
}