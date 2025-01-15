using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200017A RID: 378
	public static class ApplicationSwitchesProviderFactory
	{
		// Token: 0x060009E0 RID: 2528 RVA: 0x0002236C File Offset: 0x0002056C
		public static ICollection<IApplicationSwitchesProvider> GetApplicationSwitchProviders(ApplicationSwitchesTypes switchesType)
		{
			if ((switchesType & ApplicationSwitchesTypes.CommandLine) != (ApplicationSwitchesTypes)0)
			{
				throw new InvalidOperationException("Command Line must be inputted if command line type of switches are requsted");
			}
			List<IApplicationSwitchesProvider> list = new List<IApplicationSwitchesProvider>();
			ApplicationSwitchesProviderFactory.AddProvidersNoCommandLine(list, switchesType);
			return list;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0002238C File Offset: 0x0002058C
		public static ICollection<IApplicationSwitchesProvider> GetApplicationSwitchProviders(ApplicationSwitchesTypes switchesType, string cmdLine)
		{
			IList<IApplicationSwitchesProvider> list = new List<IApplicationSwitchesProvider>();
			if ((switchesType & ApplicationSwitchesTypes.CommandLine) != (ApplicationSwitchesTypes)0)
			{
				list.Add(new CommandLineParser(cmdLine));
			}
			ApplicationSwitchesProviderFactory.AddProvidersNoCommandLine(list, switchesType);
			return list;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x000223B8 File Offset: 0x000205B8
		public static ICollection<IApplicationSwitchesProvider> GetApplicationSwitchProviders(ApplicationSwitchesTypes switchesType, string[] cmdLine)
		{
			IList<IApplicationSwitchesProvider> list = new List<IApplicationSwitchesProvider>();
			if ((switchesType & ApplicationSwitchesTypes.CommandLine) != (ApplicationSwitchesTypes)0)
			{
				list.Add(new CommandLineParser(cmdLine));
			}
			ApplicationSwitchesProviderFactory.AddProvidersNoCommandLine(list, switchesType);
			return list;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x000223E4 File Offset: 0x000205E4
		private static void AddProvidersNoCommandLine(IList<IApplicationSwitchesProvider> providers, ApplicationSwitchesTypes switchesType)
		{
			if ((switchesType & ApplicationSwitchesTypes.AppConfig) != (ApplicationSwitchesTypes)0 || (switchesType & ApplicationSwitchesTypes.WebConfig) != (ApplicationSwitchesTypes)0)
			{
				providers.Add(new ConfigurationSwitches(switchesType));
			}
			if ((switchesType & ApplicationSwitchesTypes.EnvironmentVariables) != (ApplicationSwitchesTypes)0)
			{
				providers.Add(new EnvironmentVariablesSwitches());
			}
			if ((switchesType & ApplicationSwitchesTypes.ActivationData) != (ApplicationSwitchesTypes)0)
			{
				providers.Add(new ActivationDataSwitches());
			}
		}
	}
}
