using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200016C RID: 364
	public sealed class CustomQueryOptionToken : QueryToken
	{
		// Token: 0x06000F5C RID: 3932 RVA: 0x0002BCB4 File Offset: 0x00029EB4
		public CustomQueryOptionToken(string name, string value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x00028903 File Offset: 0x00026B03
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.CustomQueryOption;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0002BCCA File Offset: 0x00029ECA
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0002BCD2 File Offset: 0x00029ED2
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0002BCDA File Offset: 0x00029EDA
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040007B5 RID: 1973
		private readonly string name;

		// Token: 0x040007B6 RID: 1974
		private readonly string value;
	}
}
