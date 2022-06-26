using System.Drawing;
using SharpCompose.Base;
using SharpCompose.Base.ComposesApi.Providers;

namespace SharpCompose.ExampleApp.CustomProviders;

public record struct AdditionalColors(Color SolidBackground);

public class LocalAdditionalColorsProvider : LocalProvider<AdditionalColors>
{
    public static readonly LocalAdditionalColorsProvider Instance = new(
        new AdditionalColors("#F3F3F3".AsColor()));

    private LocalAdditionalColorsProvider(AdditionalColors defaultValue) : base(defaultValue)
    {
    }
}