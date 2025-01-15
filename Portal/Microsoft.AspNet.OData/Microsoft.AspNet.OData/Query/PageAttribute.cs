using System;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000B2 RID: 178
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class PageAttribute : Attribute
	{
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00015621 File Offset: 0x00013821
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x00015629 File Offset: 0x00013829
		public int MaxTop
		{
			get
			{
				return this._maxTop;
			}
			set
			{
				this._maxTop = value;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00015632 File Offset: 0x00013832
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x0001563A File Offset: 0x0001383A
		public int PageSize { get; set; }

		// Token: 0x0400016B RID: 363
		private int _maxTop = -1;
	}
}
