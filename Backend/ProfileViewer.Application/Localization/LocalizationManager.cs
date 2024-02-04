using ProfileViewer.Domain.Localization;
using System.Globalization;
using System.Resources;

namespace ProfileViewer.Application.Localization
{
    public class LocalizationManager : ILocalizationManager
    {
        private readonly ResourceManager _resourceManager;

        public LocalizationManager()
        {
            var assembly = typeof(LocalizationManager).Assembly;
            _resourceManager = new ResourceManager($"{assembly.GetName().Name}.Localization.Resources.Resources", assembly);
        }

        public string Localize(string key)
        {
            CultureInfo currentCulture = CultureInfo.CurrentUICulture;
            string? localizedValue = _resourceManager.GetString(key, currentCulture);

            if (localizedValue is null && currentCulture.Name != "en")
                localizedValue = _resourceManager.GetString(key, new CultureInfo("en")) ?? key;

            return localizedValue ?? key;
        }
    }
}
