using System;
using System.Collections;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200023F RID: 575
	internal sealed class DeliveryReportServerInfo : IDeliveryReportServerInformation
	{
		// Token: 0x0600150E RID: 5390 RVA: 0x00053CFD File Offset: 0x00051EFD
		public DeliveryReportServerInfo(Setting[] serverSettings)
		{
			this.m_serverSettings = serverSettings;
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x00053D0C File Offset: 0x00051F0C
		public Microsoft.ReportingServices.Interfaces.Extension[] RenderingExtension
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				RSService rsservice = new RSService(false);
				CachedSystemProperties.InvalidateCache();
				Microsoft.ReportingServices.Library.Soap2005.Extension[] array = rsservice.ListExtensions(ExtensionTypeEnum.Render);
				if (array != null)
				{
					foreach (Microsoft.ReportingServices.Library.Soap2005.Extension extension in array)
					{
						string name = extension.Name;
						string localizedName = extension.LocalizedName;
						bool visible = extension.Visible;
						Microsoft.ReportingServices.Interfaces.Extension extension2 = new Microsoft.ReportingServices.Interfaces.Extension(name, localizedName, visible);
						arrayList.Add(extension2);
					}
				}
				return (Microsoft.ReportingServices.Interfaces.Extension[])arrayList.ToArray(typeof(Microsoft.ReportingServices.Interfaces.Extension));
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x00053D89 File Offset: 0x00051F89
		public Setting[] ServerSettings
		{
			get
			{
				return this.m_serverSettings;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x00053D91 File Offset: 0x00051F91
		public static DeliveryReportServerInfo InfoObject
		{
			get
			{
				return DeliveryReportServerInfo.m_infoObject;
			}
		}

		// Token: 0x04000790 RID: 1936
		private Setting[] m_serverSettings;

		// Token: 0x04000791 RID: 1937
		private static DeliveryReportServerInfo m_infoObject = new DeliveryReportServerInfo(null);

		// Token: 0x020004B2 RID: 1202
		public static class ServerSettingName
		{
			// Token: 0x040010C2 RID: 4290
			public const string SubscriptionOwner = "SubscriptionOwner";

			// Token: 0x040010C3 RID: 4291
			public const string SubscriptionOwnerAddress = "SubscriptionOwnerAddress";

			// Token: 0x040010C4 RID: 4292
			public const string ContextUserName = "ContextUserName";

			// Token: 0x040010C5 RID: 4293
			public const string AccessToken = "AccessToken";

			// Token: 0x040010C6 RID: 4294
			public const string RefreshToken = "RefreshToken";

			// Token: 0x040010C7 RID: 4295
			public const string DashboardName = "DashboardName";

			// Token: 0x040010C8 RID: 4296
			public const string DashboardID = "DashboardID";

			// Token: 0x040010C9 RID: 4297
			public const string GroupName = "GroupName";

			// Token: 0x040010CA RID: 4298
			public const string GroupID = "GroupID";
		}
	}
}
