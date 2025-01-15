using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x02000277 RID: 631
	internal sealed class FunctionCallToken : QueryToken
	{
		// Token: 0x060015FA RID: 5626 RVA: 0x0004C2B0 File Offset: 0x0004A4B0
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

		// Token: 0x060015FB RID: 5627 RVA: 0x0004C319 File Offset: 0x0004A519
		public FunctionCallToken(string name, IEnumerable<FunctionParameterToken> arguments, QueryToken source)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			this.name = name;
			this.arguments = new ReadOnlyEnumerableForUriParser<FunctionParameterToken>(arguments ?? ((IEnumerable<FunctionParameterToken>)FunctionParameterToken.EmptyParameterList));
			this.source = source;
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x0004C354 File Offset: 0x0004A554
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.FunctionCall;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x0004C357 File Offset: 0x0004A557
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x0004C35F File Offset: 0x0004A55F
		public IEnumerable<FunctionParameterToken> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0004C367 File Offset: 0x0004A567
		public QueryToken Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x0004C36F File Offset: 0x0004A56F
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000920 RID: 2336
		private readonly string name;

		// Token: 0x04000921 RID: 2337
		private readonly IEnumerable<FunctionParameterToken> arguments;

		// Token: 0x04000922 RID: 2338
		private readonly QueryToken source;
	}
}
