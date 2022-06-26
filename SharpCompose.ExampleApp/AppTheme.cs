using System;
using SharpCompose.Base;
using SharpCompose.Base.ComposesApi.Providers;
using SharpCompose.Drawer.Core;
using SharpCompose.Drawer.Core.Images;
using SharpCompose.ExampleApp.CustomProviders;

namespace SharpCompose.ExampleApp;

public static partial class Markup
{
    private static void ApplicationTheme(bool isLight, Action content)
    {
        var colors = isLight
            ? new Colors
            {
                Accent = "#005FB8".AsColor(),
                OnAccent = "#FFFFFF".AsColor(),
                Standard = "#FFFFFF".AsColor(),
                OnStandard = "#000000".AsColor(),
                Background = "#FFFFFF".AsColor()
            }
            : new Colors
            {
                Accent = "#60CDFF".AsColor(),
                OnAccent = "#000000".AsColor(),
                Standard = "#000000".AsColor(),
                OnStandard = "#FFFFFF".AsColor(),
                Background = "#272727".AsColor()
            };

        var additionalColors = isLight
            ? new AdditionalColors("#F3F3F3".AsColor())
            : new AdditionalColors("#202020".AsColor());

        var icons = isLight
            ? new AppIcons(Resource.Instance.GetResource<IImage>("logo-dark"),
                Resource.Instance.GetResource<IImage>("menu-icon-dark"))
            : new AppIcons(Resource.Instance.GetResource<IImage>("logo-light"),
                Resource.Instance.GetResource<IImage>("menu-icon-light"));

        var font = Remember.Get(() => new Font("Segoe UI", FontWeight.Regular));

        CompositionLocalProvider(new[]
        {
            LocalProviders.Colors.Provide(colors),
            LocalAdditionalColorsProvider.Instance.Provide(additionalColors),
            LocalAppIconProvider.Instance.Provide(icons),
            LocalProviders.TextStyle.Provide(new TextStyle {Font = font.Value})
        }, content);
    }
}