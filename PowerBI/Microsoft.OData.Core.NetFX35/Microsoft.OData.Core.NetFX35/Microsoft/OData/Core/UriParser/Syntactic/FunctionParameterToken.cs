using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x02000279 RID: 633
	internal sealed class FunctionParameterToken : QueryToken
	{
		// Token: 0x06001609 RID: 5641 RVA: 0x0004C3BF File Offset: 0x0004A5BF
		public FunctionParameterToken(string parameterName, QueryToken valueToken)
		{
			this.parameterName = parameterName;
			this.valueToken = valueToken;
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x0004C3D5 File Offset: 0x0004A5D5
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x0004C3DD File Offset: 0x0004A5DD
		public QueryToken ValueToken
		{
			get
			{
				return this.valueToken;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x0004C3E5 File Offset: 0x0004A5E5
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.FunctionParameter;
			}
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x0004C3E9 File Offset: 0x0004A5E9
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000926 RID: 2342
		public static FunctionParameterToken[] EmptyParameterList = new FunctionParameterToken[0];

		// Token: 0x04000927 RID: 2343
		private readonly string parameterName;

		// Token: 0x04000928 RID: 2344
		private readonly QueryToken valueToken;
	}
}
