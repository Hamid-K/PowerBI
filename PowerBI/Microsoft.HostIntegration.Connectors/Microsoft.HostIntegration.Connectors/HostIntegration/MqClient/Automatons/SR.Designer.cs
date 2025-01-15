using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000B00 RID: 2816
	internal class SR
	{
		// Token: 0x0600598B RID: 22923 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x0600598C RID: 22924 RVA: 0x00171866 File Offset: 0x0016FA66
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.MqClient.Automatons.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x0600598D RID: 22925 RVA: 0x00171892 File Offset: 0x0016FA92
		// (set) Token: 0x0600598E RID: 22926 RVA: 0x00171899 File Offset: 0x0016FA99
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

		// Token: 0x0600598F RID: 22927 RVA: 0x001718A1 File Offset: 0x0016FAA1
		internal static string QueueManagerConnectFailed(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("QueueManagerConnectFailed", SR.Culture), param0, param1);
		}

		// Token: 0x06005990 RID: 22928 RVA: 0x001718C3 File Offset: 0x0016FAC3
		internal static string QueueManagerDisconnectFailed(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("QueueManagerDisconnectFailed", SR.Culture), param0, param1);
		}

		// Token: 0x0400461A RID: 17946
		private static ResourceManager resourceManager;

		// Token: 0x0400461B RID: 17947
		private static CultureInfo resourceCulture;
	}
}
