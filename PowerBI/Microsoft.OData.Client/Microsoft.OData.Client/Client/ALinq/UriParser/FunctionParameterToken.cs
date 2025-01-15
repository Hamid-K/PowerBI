using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000123 RID: 291
	public sealed class FunctionParameterToken : QueryToken
	{
		// Token: 0x06000C14 RID: 3092 RVA: 0x0002CDDD File Offset: 0x0002AFDD
		public FunctionParameterToken(string parameterName, QueryToken valueToken)
		{
			this.parameterName = parameterName;
			this.valueToken = valueToken;
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0002CDF3 File Offset: 0x0002AFF3
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0002CDFB File Offset: 0x0002AFFB
		public QueryToken ValueToken
		{
			get
			{
				return this.valueToken;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0002CE03 File Offset: 0x0002B003
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.FunctionParameter;
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002CE07 File Offset: 0x0002B007
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400066A RID: 1642
		public static FunctionParameterToken[] EmptyParameterList = new FunctionParameterToken[0];

		// Token: 0x0400066B RID: 1643
		private readonly string parameterName;

		// Token: 0x0400066C RID: 1644
		private readonly QueryToken valueToken;
	}
}
