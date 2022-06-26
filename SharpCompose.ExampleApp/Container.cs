using SharpCompose.Base.Layouting;
using SharpCompose.Base.Modifiers;
using SharpCompose.Base.Modifiers.LayoutModifiers;

namespace SharpCompose.ExampleApp;

public enum Container
{
    Default,
    Small,
    Medium,
    Large,
    ExtraLarge,
    ExtraExtraLarge,
    Fluid
}

public static class ContainerModifier
{
    public static T Container<T>(this T self, Container container = ExampleApp.Container.Default) where T : IScopeModifier<T> =>
        self.Then(new Modifier(container));

    private sealed class Modifier : ILayoutModifier
    {
        private readonly Container container;

        public Modifier(Container container)
        {
            this.container = container;
        }

        public MeasureResult Measure(Measurable measurable, Constraints constraints)
        {
            var (minWidth, maxWidth) = constraints switch
            {
                {MaxWidth: >= 1400} when container < ExampleApp.Container.Fluid => (1320, 1320),
                {MaxWidth: >= 1200} when container < ExampleApp.Container.ExtraExtraLarge  => (1140, 1140),
                {MaxWidth: >= 992} when container < ExampleApp.Container.ExtraLarge => (960, 960),
                {MaxWidth: >= 768} when container < ExampleApp.Container.Large => (720, 720),
                {MaxWidth: >= 576} when container < ExampleApp.Container.Medium => (540, 540),
                _ => (constraints.MinWidth, constraints.MaxWidth)
            };

            return measurable.Measure(new Constraints(minWidth, maxWidth, constraints.MinHeight, constraints.MaxHeight));
        }
    }
}