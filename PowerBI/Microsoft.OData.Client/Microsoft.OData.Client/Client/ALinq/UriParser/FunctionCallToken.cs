using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000121 RID: 289
	public sealed class FunctionCallToken : QueryToken
	{
		// Token: 0x06000C06 RID: 3078 RVA: 0x0002CCD8 File Offset: 0x0002AED8
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

		// Token: 0x06000C07 RID: 3079 RVA: 0x0002CD43 File Offset: 0x0002AF43
		public FunctionCallToken(string name, IEnumerable<FunctionParameterToken> arguments, QueryToken source)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			this.name = name;
			this.arguments = new ReadOnlyEnumerableForUriParser<FunctionParameterToken>(arguments ?? FunctionParameterToken.EmptyParameterList);
			this.source = source;
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0002CD79 File Offset: 0x0002AF79
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.FunctionCall;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0002CD7C File Offset: 0x0002AF7C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0002CD84 File Offset: 0x0002AF84
		public IEnumerable<FunctionParameterToken> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x0002CD8C File Offset: 0x0002AF8C
		public QueryToken Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002CD94 File Offset: 0x0002AF94
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000665 RID: 1637
		private readonly string name;

		// Token: 0x04000666 RID: 1638
		private readonly IEnumerable<FunctionParameterToken> arguments;

		// Token: 0x04000667 RID: 1639
		private readonly QueryToken source;
	}
}
