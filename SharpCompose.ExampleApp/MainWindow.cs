using System.Threading.Tasks;
using SharpCompose.Base;
using SharpCompose.WinUI;


namespace SharpCompose.ExampleApp
{
    public class MainWindow : ComposeWindow
    {
        protected override void SetContent() => Markup.App();

        protected override async Task LoadResources()
        {
            var iconDark = await LoadSvgImage("Assets/Icons/Logo-Dark.svg");
            var iconLight = await LoadSvgImage("Assets/Icons/Logo-Light.svg");
            var menuIconDark = await LoadSvgImage("Assets/Icons/Menu-Icon-Dark.svg");
            var menuIconLight = await LoadSvgImage("Assets/Icons/Menu-Icon-Light.svg");

            Resource.Instance.AddResource("logo-dark", () => iconDark);
            Resource.Instance.AddResource("logo-light", () => iconLight);
            Resource.Instance.AddResource("menu-icon-dark", () => menuIconDark);
            Resource.Instance.AddResource("menu-icon-light", () => menuIconLight);
        }
    }
}
