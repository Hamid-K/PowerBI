using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B8 RID: 440
	public sealed class CustomQueryOptionToken : QueryToken
	{
		// Token: 0x0600148B RID: 5259 RVA: 0x0003BCB4 File Offset: 0x00039EB4
		public CustomQueryOptionToken(string name, string value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00038237 File Offset: 0x00036437
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.CustomQueryOption;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x0003BCCA File Offset: 0x00039ECA
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0003BCD2 File Offset: 0x00039ED2
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0003BCDA File Offset: 0x00039EDA
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000900 RID: 2304
		private readonly string name;

		// Token: 0x04000901 RID: 2305
		private readonly string value;
	}
}
