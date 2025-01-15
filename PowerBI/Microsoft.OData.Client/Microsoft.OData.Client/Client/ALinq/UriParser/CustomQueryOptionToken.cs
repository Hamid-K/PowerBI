using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x0200011B RID: 283
	public sealed class CustomQueryOptionToken : QueryToken
	{
		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002CA18 File Offset: 0x0002AC18
		public CustomQueryOptionToken(string name, string value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0002CA2E File Offset: 0x0002AC2E
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.CustomQueryOption;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0002CA32 File Offset: 0x0002AC32
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0002CA3A File Offset: 0x0002AC3A
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002CA42 File Offset: 0x0002AC42
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000658 RID: 1624
		private readonly string name;

		// Token: 0x04000659 RID: 1625
		private readonly string value;
	}
}
