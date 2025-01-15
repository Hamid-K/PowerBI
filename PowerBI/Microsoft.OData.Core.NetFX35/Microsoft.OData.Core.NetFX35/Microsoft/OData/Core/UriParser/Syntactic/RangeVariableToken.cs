using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x0200027F RID: 639
	internal sealed class RangeVariableToken : QueryToken
	{
		// Token: 0x06001632 RID: 5682 RVA: 0x0004C58C File Offset: 0x0004A78C
		public RangeVariableToken(string name)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(name, "visitor");
			this.name = name;
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x0004C5A6 File Offset: 0x0004A7A6
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.RangeVariable;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x0004C5AA File Offset: 0x0004A7AA
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0004C5B2 File Offset: 0x0004A7B2
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000935 RID: 2357
		private readonly string name;
	}
}
