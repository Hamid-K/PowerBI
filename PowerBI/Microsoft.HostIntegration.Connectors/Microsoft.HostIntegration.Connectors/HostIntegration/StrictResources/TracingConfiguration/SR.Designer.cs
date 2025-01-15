using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.StrictResources.TracingConfiguration
{
	// Token: 0x02000676 RID: 1654
	internal class SR
	{
		// Token: 0x0600379C RID: 14236 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x0600379D RID: 14237 RVA: 0x000BB964 File Offset: 0x000B9B64
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.StrictResources.TracingConfiguration.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x0600379E RID: 14238 RVA: 0x000BB990 File Offset: 0x000B9B90
		// (set) Token: 0x0600379F RID: 14239 RVA: 0x000BB997 File Offset: 0x000B9B97
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

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x000BB99F File Offset: 0x000B9B9F
		internal static string ContainerNodeNameAttribute
		{
			get
			{
				return SR.ResourceManager.GetString("ContainerNodeNameAttribute", SR.Culture);
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x060037A1 RID: 14241 RVA: 0x000BB9B5 File Offset: 0x000B9BB5
		internal static string ContainerNodeInstanceNameAttribute
		{
			get
			{
				return SR.ResourceManager.GetString("ContainerNodeInstanceNameAttribute", SR.Culture);
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x060037A2 RID: 14242 RVA: 0x000BB9CB File Offset: 0x000B9BCB
		internal static string ContainerNodeInstanceNameAttributeIllegal
		{
			get
			{
				return SR.ResourceManager.GetString("ContainerNodeInstanceNameAttributeIllegal", SR.Culture);
			}
		}

		// Token: 0x04001FD4 RID: 8148
		private static ResourceManager resourceManager;

		// Token: 0x04001FD5 RID: 8149
		private static CultureInfo resourceCulture;
	}
}
