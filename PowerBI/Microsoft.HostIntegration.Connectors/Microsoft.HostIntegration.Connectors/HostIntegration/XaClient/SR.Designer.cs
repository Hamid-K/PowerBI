using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.XaClient
{
	// Token: 0x0200070A RID: 1802
	internal class SR
	{
		// Token: 0x06003916 RID: 14614 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06003917 RID: 14615 RVA: 0x000BF2F3 File Offset: 0x000BD4F3
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.XaClient.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06003918 RID: 14616 RVA: 0x000BF31F File Offset: 0x000BD51F
		// (set) Token: 0x06003919 RID: 14617 RVA: 0x000BF326 File Offset: 0x000BD526
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

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x0600391A RID: 14618 RVA: 0x000BF32E File Offset: 0x000BD52E
		internal static string NoCurrentTransaction
		{
			get
			{
				return SR.ResourceManager.GetString("NoCurrentTransaction", SR.Culture);
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x0600391B RID: 14619 RVA: 0x000BF344 File Offset: 0x000BD544
		internal static string NoUnmanagedCode
		{
			get
			{
				return SR.ResourceManager.GetString("NoUnmanagedCode", SR.Culture);
			}
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000BF35A File Offset: 0x000BD55A
		internal static string RegisterRecoveryInformationFailed(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("RegisterRecoveryInformationFailed", SR.Culture), param0);
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000BF37B File Offset: 0x000BD57B
		internal static string EnlistInXaTransactionFailed(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("EnlistInXaTransactionFailed", SR.Culture), param0);
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000BF39C File Offset: 0x000BD59C
		internal static string InitializeAppDomainFailed(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("InitializeAppDomainFailed", SR.Culture), param0);
		}

		// Token: 0x0400213F RID: 8511
		private static ResourceManager resourceManager;

		// Token: 0x04002140 RID: 8512
		private static CultureInfo resourceCulture;
	}
}
