using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000171 RID: 369
	public sealed class FunctionCallToken : QueryToken
	{
		// Token: 0x06000F82 RID: 3970 RVA: 0x0002BEE8 File Offset: 0x0002A0E8
		public FunctionCallToken(string name, IEnumerable<QueryToken> argumentValues)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			this.name = name;
			IEnumerable<FunctionParameterToken> enumerable;
			if (argumentValues != null)
			{
				enumerable = new ReadOnlyEnumerableForUriParser<FunctionParameterToken>(Enumerable.Select<QueryToken, FunctionParameterToken>(argumentValues, (QueryToken v) => new FunctionParameterToken(null, v)));
			}
			else
			{
				enumerable = new ReadOnlyEnumerableForUriParser<FunctionParameterToken>(FunctionParameterToken.EmptyParameterList);
			}
			this.arguments = enumerable;
			this.source = null;
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0002BF53 File Offset: 0x0002A153
		public FunctionCallToken(string name, IEnumerable<FunctionParameterToken> arguments, QueryToken source)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			this.name = name;
			this.arguments = new ReadOnlyEnumerableForUriParser<FunctionParameterToken>(arguments ?? FunctionParameterToken.EmptyParameterList);
			this.source = source;
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0002B862 File Offset: 0x00029A62
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.FunctionCall;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0002BF89 File Offset: 0x0002A189
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0002BF91 File Offset: 0x0002A191
		public IEnumerable<FunctionParameterToken> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0002BF99 File Offset: 0x0002A199
		public QueryToken Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0002BFA1 File Offset: 0x0002A1A1
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040007C7 RID: 1991
		private readonly string name;

		// Token: 0x040007C8 RID: 1992
		private readonly IEnumerable<FunctionParameterToken> arguments;

		// Token: 0x040007C9 RID: 1993
		private readonly QueryToken source;
	}
}
