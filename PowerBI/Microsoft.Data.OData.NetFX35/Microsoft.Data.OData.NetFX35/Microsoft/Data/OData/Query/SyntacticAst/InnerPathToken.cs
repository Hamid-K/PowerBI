using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x020000A4 RID: 164
	internal sealed class InnerPathToken : PathToken
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x0000BE26 File Offset: 0x0000A026
		public InnerPathToken(string identifier, QueryToken nextToken, IEnumerable<NamedValue> namedValues)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "Identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
			this.namedValues = ((namedValues == null) ? null : new ReadOnlyEnumerableForUriParser<NamedValue>(namedValues));
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000BE59 File Offset: 0x0000A059
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.InnerPath;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000BE5D File Offset: 0x0000A05D
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000BE65 File Offset: 0x0000A065
		// (set) Token: 0x060003CD RID: 973 RVA: 0x0000BE6D File Offset: 0x0000A06D
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

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000BE76 File Offset: 0x0000A076
		public IEnumerable<NamedValue> NamedValues
		{
			get
			{
				return this.namedValues;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000BE7E File Offset: 0x0000A07E
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000129 RID: 297
		private readonly string identifier;

		// Token: 0x0400012A RID: 298
		private readonly IEnumerable<NamedValue> namedValues;

		// Token: 0x0400012B RID: 299
		private QueryToken nextToken;
	}
}
