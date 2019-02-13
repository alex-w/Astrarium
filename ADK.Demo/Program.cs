﻿using ADK.Demo.Calculators;
using ADK.Demo.Config;
using ADK.Demo.Objects;
using ADK.Demo.Renderers;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADK.Demo
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            IKernel kernel = new StandardKernel();

            kernel.Bind<ISettings, Settings>().To<Settings>().InSingletonScope();

            kernel.Get<Settings>().Load();

            // TODO: get location info from settings
            SkyContext context = new SkyContext(
                new Date(DateTime.Now).ToJulianEphemerisDay(),
                new CrdsGeographical(56.3333, -44, +3));

            // collect all calculators implementations
            // TODO: to support plugin system, we need to load assemblies 
            // from the specific directory and search for calculators there
            Type[] calcTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(ISkyCalc).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .ToArray();
                        
            foreach (Type calcType in calcTypes)
            {
                var interfaces = calcType.GetInterfaces().ToList();
                if (interfaces.Any())
                {
                    // each interface that calculator implements
                    // should be bound to the calc instance
                    interfaces.Add(calcType);
                    kernel.Bind(interfaces.ToArray()).To(calcType).InSingletonScope();
                }
            }

            // collect all calculators implementations
            // TODO: to support plugin system, we need to load assemblies 
            // from the specific directory and search for renderers there
            Type[] rendererTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(IRenderer).IsAssignableFrom(t) && !t.IsAbstract)
                .ToArray();

            foreach (Type rendererType in rendererTypes)
            {                
                kernel.Bind(rendererType).ToSelf().InSingletonScope();                
            }
            
            var calculators = calcTypes
                .Select(c => kernel.Get(c))
                .Cast<ISkyCalc>()
                .ToArray();

            var renderers = rendererTypes
                .Select(r => kernel.Get(r))
                .Cast<IRenderer>()
                .OrderBy(r => r.ZOrder)
                .ToArray();

            kernel.Bind<Sky>().ToConstant(new Sky(context, calculators));
            kernel.Bind<ISkyMap>().ToConstant(new SkyMap(context, renderers));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(kernel.Get<FormMain>());
        }
    }
}
