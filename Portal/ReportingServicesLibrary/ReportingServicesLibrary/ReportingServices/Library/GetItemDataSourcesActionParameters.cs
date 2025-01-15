using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000E7 RID: 231
	internal sealed class GetItemDataSourcesActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00026252 File Offset: 0x00024452
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x0002625A File Offset: 0x0002445A
		public string ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00026263 File Offset: 0x00024463
		// (set) Token: 0x060009DC RID: 2524 RVA: 0x0002626B File Offset: 0x0002446B
		public bool InternalUsePermissionForExecution
		{
			get
			{
				return this.m_internalUsePermissionForExecution;
			}
			set
			{
				this.m_internalUsePermissionForExecution = value;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00026274 File Offset: 0x00024474
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x0002627C File Offset: 0x0002447C
		public DataSource[] DataSources
		{
			get
			{
				return this.m_dataSources;
			}
			set
			{
				this.m_dataSources = value;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x00026285 File Offset: 0x00024485
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x0002628D File Offset: 0x0002448D
		public bool FetchEncryptedCredentials
		{
			get
			{
				return this.m_fetchEncryptedCredentials;
			}
			set
			{
				this.m_fetchEncryptedCredentials = value;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00026296 File Offset: 0x00024496
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0002629E File Offset: 0x0002449E
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Item");
			}
		}

		// Token: 0x04000470 RID: 1136
		private string m_itemPath;

		// Token: 0x04000471 RID: 1137
		private bool m_internalUsePermissionForExecution;

		// Token: 0x04000472 RID: 1138
		private DataSource[] m_dataSources;

		// Token: 0x04000473 RID: 1139
		private bool m_fetchEncryptedCredentials;
	}
}
