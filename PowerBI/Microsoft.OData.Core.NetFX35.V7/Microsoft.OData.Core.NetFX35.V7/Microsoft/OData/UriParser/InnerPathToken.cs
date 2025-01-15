using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000174 RID: 372
	public sealed class InnerPathToken : PathToken
	{
		// Token: 0x06000F96 RID: 3990 RVA: 0x0002C026 File Offset: 0x0002A226
		public InnerPathToken(string identifier, QueryToken nextToken, IEnumerable<NamedValue> namedValues)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(identifier, "Identifier");
			this.identifier = identifier;
			this.nextToken = nextToken;
			this.namedValues = ((namedValues == null) ? null : new ReadOnlyEnumerableForUriParser<NamedValue>(namedValues));
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0002C05A File Offset: 0x0002A25A
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.InnerPath;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0002C05E File Offset: 0x0002A25E
		public override string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0002C066 File Offset: 0x0002A266
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x0002C06E File Offset: 0x0002A26E
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

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x0002C077 File Offset: 0x0002A277
		public IEnumerable<NamedValue> NamedValues
		{
			get
			{
				return this.namedValues;
			}
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0002C07F File Offset: 0x0002A27F
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040007CF RID: 1999
		private readonly string identifier;

		// Token: 0x040007D0 RID: 2000
		private readonly IEnumerable<NamedValue> namedValues;

		// Token: 0x040007D1 RID: 2001
		private QueryToken nextToken;
	}
}
