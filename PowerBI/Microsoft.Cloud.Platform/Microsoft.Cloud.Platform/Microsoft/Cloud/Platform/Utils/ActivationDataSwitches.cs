using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000165 RID: 357
	public class ActivationDataSwitches : IApplicationSwitchesProvider
	{
		// Token: 0x17000177 RID: 375
		[CanBeNull]
		public string this[string name]
		{
			get
			{
				if (name.Equals("activationData", StringComparison.Ordinal) && AppDomain.CurrentDomain.SetupInformation != null && AppDomain.CurrentDomain.SetupInformation.ActivationArguments != null && AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData != null)
				{
					return AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData[0];
				}
				return null;
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x000202D2 File Offset: 0x0001E4D2
		public bool GetBoolSwitch(string name, out bool specified)
		{
			return ApplicationSwitchesProviderUtil.GetBoolSwitch(this, name, out specified);
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x000202DC File Offset: 0x0001E4DC
		public string Name
		{
			get
			{
				return "ActivationData";
			}
		}

		// Token: 0x04000385 RID: 901
		public const string c_key = "activationData";
	}
}
