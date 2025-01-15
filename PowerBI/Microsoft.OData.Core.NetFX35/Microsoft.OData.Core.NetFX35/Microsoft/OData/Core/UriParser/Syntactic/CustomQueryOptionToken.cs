using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x02000271 RID: 625
	internal sealed class CustomQueryOptionToken : QueryToken
	{
		// Token: 0x060015D2 RID: 5586 RVA: 0x0004C08F File Offset: 0x0004A28F
		public CustomQueryOptionToken(string name, string value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x0004C0A5 File Offset: 0x0004A2A5
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.CustomQueryOption;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0004C0A9 File Offset: 0x0004A2A9
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x0004C0B1 File Offset: 0x0004A2B1
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x0004C0B9 File Offset: 0x0004A2B9
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400090F RID: 2319
		private readonly string name;

		// Token: 0x04000910 RID: 2320
		private readonly string value;
	}
}
