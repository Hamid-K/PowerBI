using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.StrictResources.CommonGlobals
{
	// Token: 0x020004EF RID: 1263
	internal class SR
	{
		// Token: 0x06002AC8 RID: 10952 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002AC9 RID: 10953 RVA: 0x00093C30 File Offset: 0x00091E30
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.StrictResources.CommonGlobals.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x00093C5C File Offset: 0x00091E5C
		// (set) Token: 0x06002ACB RID: 10955 RVA: 0x00093C63 File Offset: 0x00091E63
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

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002ACC RID: 10956 RVA: 0x00093C6B File Offset: 0x00091E6B
		internal static string ClassNotRegistered
		{
			get
			{
				return SR.ResourceManager.GetString("ClassNotRegistered", SR.Culture);
			}
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x00093C81 File Offset: 0x00091E81
		internal static string MissingOverrideValue(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("MissingOverrideValue", SR.Culture), param0, param1);
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x00093CA3 File Offset: 0x00091EA3
		internal static string InvalidRegistryEntry(object param0, object param1, object param2)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("InvalidRegistryEntry", SR.Culture), param0, param1, param2);
		}

		// Token: 0x040019FA RID: 6650
		private static ResourceManager resourceManager;

		// Token: 0x040019FB RID: 6651
		private static CultureInfo resourceCulture;
	}
}
