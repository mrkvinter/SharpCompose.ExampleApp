using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SharpCompose.Base;
using SharpCompose.Base.ComposesApi.Providers;
using SharpCompose.Base.Layouting;
using SharpCompose.Base.Modifiers;
using SharpCompose.Base.Modifiers.Extensions;
using SharpCompose.Drawer.Core;
using SharpCompose.Drawer.Core.Utilities;
using SharpCompose.ExampleApp.CustomProviders;

namespace SharpCompose.ExampleApp;

public static class Navigation
{
    public static void NavPanel(List<Action> items) =>
        Layout(IModifier.Empty, () =>
            {
                Box(Modifier.FillMaxHeight().Width(40), alignment: Alignment.Center, content: () =>
                {
                    var isOpen = Remember.Get(() => false);

                    Icon(LocalAppIconProvider.Instance.Value.Menu,
                        Modifier.Clickable(() => isOpen.Value = !isOpen.Value));

                    if (isOpen.Value)
                        AbsoluteBox(null, new IntOffset(-20, 30), () =>
                        {
                            Column(Modifier
                                    .Width(150)
                                    .Shadow(Color.Black.WithAlpha(0.15f), new IntOffset(0, 4), 8,
                                        Shapes.RoundCorner(10))
                                    .Clip(Shapes.RoundCorner(10))
                                    .BackgroundColor(LocalProviders.Colors.Value.Background), Alignment.Center,
                                content: () =>
                                {
                                    Spacer(Modifier.Size(12));
                                    For(Enumerable.Range(0, items.Count), i =>
                                    {
                                        var itemContent = items[i];
                                        Box(Modifier.FillMaxWidth().Height(40)
                                                .Clickable(() => isOpen.Value = false),
                                            alignment: Alignment.Center, content: () => itemContent());
                                        Spacer(Modifier.Size(12));
                                    });
                                });
                        });
                }); // mobile

                Row(Modifier.FillMaxHeight(),
                    content: () =>
                    {
                        For(Enumerable.Range(0, items.Count), i =>
                        {
                            var itemContent = items[i];
                            Box(
                                Modifier.FillMaxHeight().Width(100),
                                alignment: Alignment.Center, content: () => itemContent());
                            Spacer(Modifier.Size(12));
                        });
                        Spacer(Modifier.Size(24));
                    }); // desktop
            },
            (measures, constraints) =>
            {
                var placeable = constraints switch
                {
                    {MaxWidth: < 540} => measures[0].Measure(constraints),
                    _ => measures[1].Measure(constraints)
                };

                return new MeasureResult
                {
                    Width = placeable.Width,
                    Height = placeable.Height,
                    Placeable = (x, y) => placeable.Placeable(x, y)
                };
            });

    private static void AbsoluteBox(ScopeModifier modifier, IntOffset offset, Action content)
    {
        LocalAbsoluteScopeProvider.Instance.Value.Add(() => Box(Modifier.FillMaxHeight().FillMaxWidth(),
            alignment: Alignment.TopEnd, content: () =>
                Layout(
                    modifier?.SelfModifier ?? IModifier.Empty,
                    content,
                    (measures, _) =>
                    {
                        var placeables = measures
                            .Select(e => e.Measure(new Constraints(0, Constraints.Infinity, 0, Constraints.Infinity)))
                            .ToArray();
                        return new MeasureResult
                        {
                            Width = placeables.Length > 0 ? placeables.Max(e => e.Width) : 0,
                            Height = placeables.Length > 0 ? placeables.Max(e => e.Height) : 0,
                            Placeable = (x, y) => placeables.ForEach(e => e.Placeable(x + offset.X, y + offset.Y))
                        };
                    })));
    }
}