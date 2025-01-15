using System;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000B5 RID: 181
	public class DefaultQuerySettings
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00015A80 File Offset: 0x00013C80
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x00015A88 File Offset: 0x00013C88
		public bool EnableExpand { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00015A91 File Offset: 0x00013C91
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x00015A99 File Offset: 0x00013C99
		public bool EnableSelect { get; set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x00015AA2 File Offset: 0x00013CA2
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x00015AAA File Offset: 0x00013CAA
		public bool EnableCount { get; set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00015AB3 File Offset: 0x00013CB3
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x00015ABB File Offset: 0x00013CBB
		public bool EnableOrderBy { get; set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00015AC4 File Offset: 0x00013CC4
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00015ACC File Offset: 0x00013CCC
		public bool EnableFilter { get; set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00015AD5 File Offset: 0x00013CD5
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x00015AE0 File Offset: 0x00013CE0
		public int? MaxTop
		{
			get
			{
				return this._maxTop;
			}
			set
			{
				if (value != null)
				{
					int? num = value;
					int num2 = 0;
					if ((num.GetValueOrDefault() < num2) & (num != null))
					{
						throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 0);
					}
				}
				this._maxTop = value;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00015B2C File Offset: 0x00013D2C
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x00015B34 File Offset: 0x00013D34
		public bool EnableSkipToken { get; set; }

		// Token: 0x04000177 RID: 375
		private int? _maxTop = new int?(0);
	}
}
