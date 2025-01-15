using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000E9 RID: 233
	internal sealed class GetItemDataSourcePromptsActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00026494 File Offset: 0x00024694
		// (set) Token: 0x060009E8 RID: 2536 RVA: 0x0002649C File Offset: 0x0002469C
		public string ItemPath
		{
			get
			{
				return this.m_ItemPath;
			}
			set
			{
				this.m_ItemPath = value;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x000264A5 File Offset: 0x000246A5
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x000264AD File Offset: 0x000246AD
		public DataSourcePrompt[] DataSourcePrompts
		{
			get
			{
				return this.m_dataSourcePrompts;
			}
			set
			{
				this.m_dataSourcePrompts = value;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x000264B6 File Offset: 0x000246B6
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000264BE File Offset: 0x000246BE
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Item");
			}
		}

		// Token: 0x04000474 RID: 1140
		private string m_ItemPath;

		// Token: 0x04000475 RID: 1141
		private DataSourcePrompt[] m_dataSourcePrompts;
	}
}
