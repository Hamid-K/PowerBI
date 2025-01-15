using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.StrictResources.AutomatonDriver
{
	// Token: 0x020004BD RID: 1213
	internal class SR
	{
		// Token: 0x06002966 RID: 10598 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x0007CF5B File Offset: 0x0007B15B
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.StrictResources.AutomatonDriver.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x0007CF87 File Offset: 0x0007B187
		// (set) Token: 0x06002969 RID: 10601 RVA: 0x0007CF8E File Offset: 0x0007B18E
		internal static CultureInfo Culture
		{
			get
			{
				return SR.resourceCulture;
			}
			set
			{
				SR.resourceCulture = value;
			}
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x0007CF96 File Offset: 0x0007B196
		internal static string StateProcessingFailed(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("StateProcessingFailed", SR.Culture), param0, param1);
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x0007CFB8 File Offset: 0x0007B1B8
		internal static string AutomatonProcessingFailed(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("AutomatonProcessingFailed", SR.Culture), param0, param1);
		}

		// Token: 0x04001874 RID: 6260
		private static ResourceManager resourceManager;

		// Token: 0x04001875 RID: 6261
		private static CultureInfo resourceCulture;
	}
}
