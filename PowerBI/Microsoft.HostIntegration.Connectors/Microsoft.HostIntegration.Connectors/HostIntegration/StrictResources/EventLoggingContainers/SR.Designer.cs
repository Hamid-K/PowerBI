using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.StrictResources.EventLoggingContainers
{
	// Token: 0x02000774 RID: 1908
	internal class SR
	{
		// Token: 0x06003DB9 RID: 15801 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06003DBA RID: 15802 RVA: 0x000CFBC6 File Offset: 0x000CDDC6
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.StrictResources.EventLoggingContainers.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06003DBB RID: 15803 RVA: 0x000CFBF2 File Offset: 0x000CDDF2
		// (set) Token: 0x06003DBC RID: 15804 RVA: 0x000CFBF9 File Offset: 0x000CDDF9
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

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06003DBD RID: 15805 RVA: 0x000CFC01 File Offset: 0x000CDE01
		internal static string ConversionPipelineEventLogSource
		{
			get
			{
				return SR.ResourceManager.GetString("ConversionPipelineEventLogSource", SR.Culture);
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06003DBE RID: 15806 RVA: 0x000CFC17 File Offset: 0x000CDE17
		internal static string DrdaArEventLogSource
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaArEventLogSource", SR.Culture);
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06003DBF RID: 15807 RVA: 0x000CFC2D File Offset: 0x000CDE2D
		internal static string DrdaAsEventLogSource
		{
			get
			{
				return SR.ResourceManager.GetString("DrdaAsEventLogSource", SR.Culture);
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06003DC0 RID: 15808 RVA: 0x000CFC43 File Offset: 0x000CDE43
		internal static string HipEventLogSource
		{
			get
			{
				return SR.ResourceManager.GetString("HipEventLogSource", SR.Culture);
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06003DC1 RID: 15809 RVA: 0x000CFC59 File Offset: 0x000CDE59
		internal static string HostFilesEventLogSource
		{
			get
			{
				return SR.ResourceManager.GetString("HostFilesEventLogSource", SR.Culture);
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06003DC2 RID: 15810 RVA: 0x000CFC6F File Offset: 0x000CDE6F
		internal static string MqChannelEventLogSource
		{
			get
			{
				return SR.ResourceManager.GetString("MqChannelEventLogSource", SR.Culture);
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06003DC3 RID: 15811 RVA: 0x000CFC85 File Offset: 0x000CDE85
		internal static string MqClientEventLogSource
		{
			get
			{
				return SR.ResourceManager.GetString("MqClientEventLogSource", SR.Culture);
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06003DC4 RID: 15812 RVA: 0x000CFC9B File Offset: 0x000CDE9B
		internal static string SessionIntegratorEventLogSource
		{
			get
			{
				return SR.ResourceManager.GetString("SessionIntegratorEventLogSource", SR.Culture);
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06003DC5 RID: 15813 RVA: 0x000CFCB1 File Offset: 0x000CDEB1
		internal static string WipEventLogSource
		{
			get
			{
				return SR.ResourceManager.GetString("WipEventLogSource", SR.Culture);
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06003DC6 RID: 15814 RVA: 0x000CFCC7 File Offset: 0x000CDEC7
		internal static string FfpEventLogSource
		{
			get
			{
				return SR.ResourceManager.GetString("FfpEventLogSource", SR.Culture);
			}
		}

		// Token: 0x04002493 RID: 9363
		private static ResourceManager resourceManager;

		// Token: 0x04002494 RID: 9364
		private static CultureInfo resourceCulture;
	}
}
