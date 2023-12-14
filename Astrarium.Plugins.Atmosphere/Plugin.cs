﻿using Astrarium.Plugins.Atmosphere.Controls;
using Astrarium.Types;

namespace Astrarium.Plugins.Atmosphere
{
    public class Plugin : AbstractPlugin
    {
        public Plugin(ISettings settings)
        {
            DefineSetting("Atmosphere", true);

            DefineSetting("LightPollution", false);
            DefineSetting("LightPollutionAltitude", 60m);
            DefineSetting("LightPollutionTone", 30m);
            DefineSetting("LightPollutionIntensity", 30m);

            DefineSetting("Fog", false);
            DefineSetting("FogAltitude", 10m);
            DefineSetting("FogSpreading", 10m);
            DefineSetting("FogIntensity", 30m);

            DefineSetting("Refraction", false);

            DefineSettingsSection<AtmosphereSettingsSection, SettingsViewModel>();

            ExportResourceDictionaries("Images.xaml");

            ToolbarItems.Add("Ground", new ToolbarToggleButton("IconAtmosphere", "$Settings.Atmosphere", new SimpleBinding(settings, "Atmosphere", "IsChecked")));

            ToolbarItems.Add("Ground", new ToolbarToggleButton("IconStar", "Refraction", new SimpleBinding(settings, "Refraction", "IsChecked")));
        }
    }
}
