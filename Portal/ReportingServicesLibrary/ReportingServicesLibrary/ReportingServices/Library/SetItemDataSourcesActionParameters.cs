using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000ED RID: 237
	internal sealed class SetItemDataSourcesActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0002676C File Offset: 0x0002496C
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x00026774 File Offset: 0x00024974
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

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0002677D File Offset: 0x0002497D
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x00026785 File Offset: 0x00024985
		public DataSource[] ItemDataSources
		{
			get
			{
				return this.m_itemDataSources;
			}
			set
			{
				this.m_itemDataSources = value;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0002678E File Offset: 0x0002498E
		// (set) Token: 0x060009FD RID: 2557 RVA: 0x00026796 File Offset: 0x00024996
		public bool IgnoreSecCheck
		{
			get
			{
				return this.m_ignoreSecCheck;
			}
			set
			{
				this.m_ignoreSecCheck = value;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0002679F File Offset: 0x0002499F
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x000267A7 File Offset: 0x000249A7
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Item");
			}
			if (this.ItemDataSources == null)
			{
				throw new MissingParameterException("DataSources");
			}
		}

		// Token: 0x04000477 RID: 1143
		private string m_itemPath;

		// Token: 0x04000478 RID: 1144
		private DataSource[] m_itemDataSources;

		// Token: 0x04000479 RID: 1145
		private bool m_ignoreSecCheck;
	}
}
