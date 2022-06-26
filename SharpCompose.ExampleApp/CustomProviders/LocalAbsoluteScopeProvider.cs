using System;
using System.Collections.Generic;
using SharpCompose.Base.ComposesApi.Providers;

namespace SharpCompose.ExampleApp.CustomProviders;

public class LocalAbsoluteScopeProvider : LocalProvider<List<Action>>
{
    public static readonly LocalAbsoluteScopeProvider Instance = new(new List<Action>());

    private LocalAbsoluteScopeProvider(List<Action> defaultValue) : base(defaultValue)
    {
    }
}