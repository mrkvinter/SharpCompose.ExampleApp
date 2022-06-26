using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SharpCompose.Base;
using SharpCompose.Base.ComposesApi.Providers;
using SharpCompose.Base.Modifiers.Extensions;
using SharpCompose.Drawer.Core;
using SharpCompose.Drawer.Core.Utilities;
using SharpCompose.ExampleApp.CustomProviders;

namespace SharpCompose.ExampleApp;

public static partial class Markup
{
    public static void App()
    {
        var isLight = Remember.Get(true);
        ApplicationTheme(isLight.Value, content: () =>
        {
            var absoluteScope = new List<Action>();

            CompositionLocalProvider(new[] {LocalAbsoluteScopeProvider.Instance.Provide(absoluteScope)},
                () =>
                {
                    Column(
                        Modifier.FillMaxWidth().FillMaxHeight().BackgroundColor(LocalProviders.Colors.Value.Background),
                        content: () =>
                        {
                            var page = Remember.Get(0);

                            Header(page, isLight);

                            Box(Modifier.FillMaxWidth().FillMaxHeight().Padding(30, 15),
                                alignment: Alignment.TopStart, content: () =>
                                {
                                    switch (page.Value)
                                    {
                                        case 0:
                                            Home();
                                            break;
                                        case 1:
                                            Counter();
                                            break;
                                    }
                                });
                        });
                });

            Box(Modifier.FillMaxHeight().FillMaxWidth(), content: () =>
                For(Enumerable.Range(0, absoluteScope.Count), i => absoluteScope[i].Invoke()));
        });
    }

    private static void Header(ValueRemembered<int> page, ValueRemembered<bool> isLight) =>
        Box(Modifier
                .FillMaxWidth().Height(50)
                .Shadow(Color.Black.WithAlpha(0.1f), new IntOffset(0, 1), 8)
                .BackgroundColor(LocalAdditionalColorsProvider.Instance.Value.SolidBackground),
            alignment: Alignment.CenterStart, content: () =>
            {
                Row(Modifier.FillMaxHeight().Padding(start: 12), Alignment.Center, content: () =>
                {
                    Icon(LocalAppIconProvider.Instance.Value.Logo);
                    Spacer(Modifier.Width(6));
                    Text("Example App", size: 18);
                });
                Box(Modifier.FillMaxHeight().FillMaxWidth(), alignment: Alignment.BottomCenter, content: () =>
                    Box(Modifier.FillMaxWidth().Height(2).BackgroundColor(Color.Black.WithAlpha(0.15f))));

                NavigationButtons(page, isLight);
            });

    private static void NavigationButtons(ValueRemembered<int> page, ValueRemembered<bool> isLight)
    {
        Box(Modifier.FillMaxHeight().FillMaxWidth(), alignment: Alignment.CenterEnd, content: () =>
        {
            var selectedFont = new Font(LocalProviders.TextStyle.Value.Font.FontFamily, FontWeight.SemiBold);
            Navigation.NavPanel(new List<Action>
            {
                () => Box(Modifier.FillMaxHeight().FillMaxWidth().MinSize(100, 0)
                        .Clickable(() => isLight.Value = !isLight.Value)
                        .Padding(10, 0),
                    alignment: Alignment.Center, content: () => Row(verticalAlignment: Alignment.Center,
                        content: () =>
                        {
                            Text("Theme:");
                            Box(Modifier.FillMaxHeight().Width(30), Alignment.Center,
                                () => Text(isLight.Value ? "☀️" : "🌙", font: selectedFont));
                        })),

                () => Box(Modifier.FillMaxHeight().FillMaxWidth().MinSize(100, 0)
                        .BackgroundColor(Color.Black.WithAlpha(0.05f))
                        .Clickable(() => page.Value = 0)
                        .Padding(10, 0),
                    alignment: Alignment.Center,
                    content: () => Text("Home", font: page.Value == 0 ? selectedFont : null)),

                () => Box(Modifier.FillMaxHeight().FillMaxWidth().MinSize(100, 0)
                        .BackgroundColor(Color.Black.WithAlpha(0.05f))
                        .Clickable(() => page.Value = 1).Padding(10, 0),
                    alignment: Alignment.Center,
                    content: () => Text("Counter", font: page.Value == 1 ? selectedFont : null)),
            });
        });
    }

    private static void Home() => Column(content: () =>
    {
        Text("Hello Sharp.Compose!", size: 32);
        Spacer(Modifier.Height(8));
        Text("Welcome to Sharp.Compose example app!");
    });

    private static void Counter() => Column(content: () =>
    {
        var counter = Remember.Get(0);
        Text("Counter", size: 32);
        Spacer(Modifier.Height(8));
        Text($"Current count: {counter.Value}");
        Spacer(Modifier.Height(16));
        Button(() => counter.Value++, "Click me");
    });
}