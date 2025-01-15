using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001BD RID: 445
	public sealed class FunctionCallToken : QueryToken
	{
		// Token: 0x060014AF RID: 5295 RVA: 0x0003BEBC File Offset: 0x0003A0BC
		public FunctionCallToken(string name, IEnumerable<QueryToken> argumentValues)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			this.name = name;
			IEnumerable<FunctionParameterToken> enumerable;
			if (argumentValues != null)
			{
				enumerable = new ReadOnlyEnumerableForUriParser<FunctionParameterToken>(argumentValues.Select((QueryToken v) => new FunctionParameterToken(null, v)));
			}
			else
			{
				enumerable = new ReadOnlyEnumerableForUriParser<FunctionParameterToken>(FunctionParameterToken.EmptyParameterList);
			}
			this.arguments = enumerable;
			this.source = null;
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0003BF27 File Offset: 0x0003A127
		public FunctionCallToken(string name, IEnumerable<FunctionParameterToken> arguments, QueryToken source)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			this.name = name;
			this.arguments = new ReadOnlyEnumerableForUriParser<FunctionParameterToken>(arguments ?? FunctionParameterToken.EmptyParameterList);
			this.source = source;
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x0003B90E File Offset: 0x00039B0E
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.FunctionCall;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0003BF5D File Offset: 0x0003A15D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0003BF65 File Offset: 0x0003A165
		public IEnumerable<FunctionParameterToken> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x0003BF6D File Offset: 0x0003A16D
		public QueryToken Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0003BF75 File Offset: 0x0003A175
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400090A RID: 2314
		private readonly string name;

		// Token: 0x0400090B RID: 2315
		private readonly IEnumerable<FunctionParameterToken> arguments;

		// Token: 0x0400090C RID: 2316
		private readonly QueryToken source;
	}
}
