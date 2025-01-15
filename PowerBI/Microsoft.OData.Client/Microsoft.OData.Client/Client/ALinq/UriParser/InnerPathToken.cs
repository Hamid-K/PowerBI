using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000124 RID: 292
	public sealed class InnerPathToken : PathToken
	{
		// Token: 0x06000C1A RID: 3098 RVA: 0x0002CE1D File Offset: 0x0002B01D
		public InnerPathToken(string identifier, QueryToken nextToken, IEnumerable<NamedValue> namedValues)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
			this.namedValues = ((namedValues == null) ? null : new ReadOnlyEnumerableForUriParser<NamedValue>(namedValues));
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0002CE51 File Offset: 0x0002B051
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.InnerPath;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0002CE55 File Offset: 0x0002B055
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0002CE5D File Offset: 0x0002B05D
		// (set) Token: 0x06000C1E RID: 3102 RVA: 0x0002CE65 File Offset: 0x0002B065
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

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0002CE6E File Offset: 0x0002B06E
		public IEnumerable<NamedValue> NamedValues
		{
			get
			{
				return this.namedValues;
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002CE76 File Offset: 0x0002B076
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400066D RID: 1645
		private readonly string identifier;

		// Token: 0x0400066E RID: 1646
		private readonly IEnumerable<NamedValue> namedValues;

		// Token: 0x0400066F RID: 1647
		private QueryToken nextToken;
	}
}
