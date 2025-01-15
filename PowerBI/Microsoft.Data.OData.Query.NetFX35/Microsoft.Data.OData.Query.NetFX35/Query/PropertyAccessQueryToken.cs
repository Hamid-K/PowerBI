using System;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200004B RID: 75
	public sealed class PropertyAccessQueryToken : QueryToken
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x0000A899 File Offset: 0x00008A99
		public PropertyAccessQueryToken(string name, QueryToken parent)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			this.name = name;
			this.parent = parent;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000A8BA File Offset: 0x00008ABA
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.PropertyAccess;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000A8BD File Offset: 0x00008ABD
		public QueryToken Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000A8C5 File Offset: 0x00008AC5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040001C3 RID: 451
		private readonly string name;

		// Token: 0x040001C4 RID: 452
		private readonly QueryToken parent;
	}
}
