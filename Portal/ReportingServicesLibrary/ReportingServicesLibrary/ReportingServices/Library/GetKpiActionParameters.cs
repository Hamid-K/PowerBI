using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200006C RID: 108
	internal sealed class GetKpiActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x000129C9 File Offset: 0x00010BC9
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x000129D1 File Offset: 0x00010BD1
		public string ItemPath
		{
			get
			{
				return this._itemPath;
			}
			set
			{
				this._itemPath = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x000129DA File Offset: 0x00010BDA
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x000129E2 File Offset: 0x00010BE2
		public KpiCatalogItem Item
		{
			get
			{
				return this._item;
			}
			set
			{
				this._item = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x000129EB File Offset: 0x00010BEB
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x000129EB File Offset: 0x00010BEB
		internal override string OutputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000129F3 File Offset: 0x00010BF3
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
		}

		// Token: 0x04000210 RID: 528
		private string _itemPath;

		// Token: 0x04000211 RID: 529
		private KpiCatalogItem _item;
	}
}
