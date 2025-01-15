using System;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200002C RID: 44
	public sealed class QueryOptionQueryToken : QueryToken
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00005D47 File Offset: 0x00003F47
		public QueryOptionQueryToken(string name, string value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005D5D File Offset: 0x00003F5D
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.QueryOption;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005D61 File Offset: 0x00003F61
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005D69 File Offset: 0x00003F69
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000154 RID: 340
		private readonly string name;

		// Token: 0x04000155 RID: 341
		private readonly string value;
	}
}
