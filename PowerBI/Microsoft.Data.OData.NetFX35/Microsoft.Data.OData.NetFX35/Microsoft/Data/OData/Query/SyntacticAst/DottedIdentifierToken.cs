using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000C2 RID: 194
	internal sealed class DottedIdentifierToken : PathToken
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x0000FEC3 File Offset: 0x0000E0C3
		public DottedIdentifierToken(string identifier, QueryToken nextToken)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(identifier, "Identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000FEE4 File Offset: 0x0000E0E4
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.DottedIdentifier;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000FEE8 File Offset: 0x0000E0E8
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x0000FEF8 File Offset: 0x0000E0F8
		public override QueryToken NextToken
		{
			get
			{
				return this.nextToken;
			}
			set
			{
				this.nextToken = value;
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000FF01 File Offset: 0x0000E101
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000199 RID: 409
		private readonly string identifier;

		// Token: 0x0400019A RID: 410
		private QueryToken nextToken;
	}
}
