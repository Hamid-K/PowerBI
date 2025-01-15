using System;
using Microsoft.AspNet.OData.Builder;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000045 RID: 69
	public class QueryableRestrictions
	{
		// Token: 0x0600019B RID: 411 RVA: 0x00002557 File Offset: 0x00000757
		public QueryableRestrictions()
		{
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007AD8 File Offset: 0x00005CD8
		public QueryableRestrictions(PropertyConfiguration propertyConfiguration)
		{
			this.NotFilterable = propertyConfiguration.NotFilterable;
			this.NotSortable = propertyConfiguration.NotSortable;
			this.NotNavigable = propertyConfiguration.NotNavigable;
			this.NotExpandable = propertyConfiguration.NotExpandable;
			this.NotCountable = propertyConfiguration.NotCountable;
			this.DisableAutoExpandWhenSelectIsPresent = propertyConfiguration.DisableAutoExpandWhenSelectIsPresent;
			this._autoExpand = propertyConfiguration.AutoExpand;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00007B3F File Offset: 0x00005D3F
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00007B47 File Offset: 0x00005D47
		public bool NotFilterable { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00007B50 File Offset: 0x00005D50
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00007B58 File Offset: 0x00005D58
		public bool NonFilterable
		{
			get
			{
				return this.NotFilterable;
			}
			set
			{
				this.NotFilterable = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00007B61 File Offset: 0x00005D61
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00007B69 File Offset: 0x00005D69
		public bool NotSortable { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00007B72 File Offset: 0x00005D72
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00007B7A File Offset: 0x00005D7A
		public bool Unsortable
		{
			get
			{
				return this.NotSortable;
			}
			set
			{
				this.NotSortable = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00007B83 File Offset: 0x00005D83
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00007B8B File Offset: 0x00005D8B
		public bool NotNavigable { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00007B94 File Offset: 0x00005D94
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00007B9C File Offset: 0x00005D9C
		public bool NotExpandable { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00007BA5 File Offset: 0x00005DA5
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00007BAD File Offset: 0x00005DAD
		public bool NotCountable { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00007BB6 File Offset: 0x00005DB6
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00007BC8 File Offset: 0x00005DC8
		public bool AutoExpand
		{
			get
			{
				return !this.NotExpandable && this._autoExpand;
			}
			set
			{
				this._autoExpand = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00007BD1 File Offset: 0x00005DD1
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00007BD9 File Offset: 0x00005DD9
		public bool DisableAutoExpandWhenSelectIsPresent { get; set; }

		// Token: 0x0400006D RID: 109
		private bool _autoExpand;
	}
}
