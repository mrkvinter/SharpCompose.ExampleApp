using SharpCompose.Base.ComposesApi.Providers;
using SharpCompose.Drawer.Core.Images;

namespace SharpCompose.ExampleApp.CustomProviders;

public readonly record struct AppIcons(IImage Logo, IImage Menu);

public class LocalAppIconProvider : LocalProvider<AppIcons>
{
    private static readonly IImage StabIcon = new VectorImage(
        @"<svg width=""50"" height=""50"" viewBox=""0 0 100 100"" fill=""none"" xmlns=""http://www.w3.org/2000/svg"">
<rect x=""3"" y=""3"" width=""44"" height=""44"" stroke=""#FF00F5"" stroke-width=""6""/></svg>");

    public static readonly LocalAppIconProvider Instance = new(
        new AppIcons(StabIcon, StabIcon));

    private LocalAppIconProvider(AppIcons defaultValue) : base(defaultValue)
    {
    }
}