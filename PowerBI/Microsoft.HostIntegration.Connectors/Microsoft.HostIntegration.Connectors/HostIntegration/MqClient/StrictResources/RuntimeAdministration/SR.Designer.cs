using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.MqClient.StrictResources.RuntimeAdministration
{
	// Token: 0x02000BDA RID: 3034
	internal class SR
	{
		// Token: 0x06005E84 RID: 24196 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x06005E85 RID: 24197 RVA: 0x0018137C File Offset: 0x0017F57C
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.MqClient.StrictResources.RuntimeAdministration.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x06005E86 RID: 24198 RVA: 0x001813A8 File Offset: 0x0017F5A8
		// (set) Token: 0x06005E87 RID: 24199 RVA: 0x001813AF File Offset: 0x0017F5AF
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

		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x06005E88 RID: 24200 RVA: 0x001813B7 File Offset: 0x0017F5B7
		internal static string DuplicateOrdering
		{
			get
			{
				return SR.ResourceManager.GetString("DuplicateOrdering", SR.Culture);
			}
		}

		// Token: 0x06005E89 RID: 24201 RVA: 0x001813CD File Offset: 0x0017F5CD
		internal static string QueueWrongManager(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("QueueWrongManager", SR.Culture), param0, param1);
		}

		// Token: 0x06005E8A RID: 24202 RVA: 0x001813EF File Offset: 0x0017F5EF
		internal static string DuplicateManager(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DuplicateManager", SR.Culture), param0);
		}

		// Token: 0x06005E8B RID: 24203 RVA: 0x00181410 File Offset: 0x0017F610
		internal static string DuplicateQueue(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DuplicateQueue", SR.Culture), param0);
		}

		// Token: 0x06005E8C RID: 24204 RVA: 0x00181431 File Offset: 0x0017F631
		internal static string DifferentSslSettings(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DifferentSslSettings", SR.Culture), param0, param1);
		}

		// Token: 0x04004FDE RID: 20446
		private static ResourceManager resourceManager;

		// Token: 0x04004FDF RID: 20447
		private static CultureInfo resourceCulture;
	}
}
