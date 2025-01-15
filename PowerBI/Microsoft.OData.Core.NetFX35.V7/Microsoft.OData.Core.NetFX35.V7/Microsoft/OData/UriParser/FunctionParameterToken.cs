using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000173 RID: 371
	public sealed class FunctionParameterToken : QueryToken
	{
		// Token: 0x06000F90 RID: 3984 RVA: 0x0002BFEA File Offset: 0x0002A1EA
		public FunctionParameterToken(string parameterName, QueryToken valueToken)
		{
			this.parameterName = parameterName;
			this.valueToken = valueToken;
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0002C000 File Offset: 0x0002A200
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0002C008 File Offset: 0x0002A208
		public QueryToken ValueToken
		{
			get
			{
				return this.valueToken;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0002ABBC File Offset: 0x00028DBC
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.FunctionParameter;
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0002C010 File Offset: 0x0002A210
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040007CC RID: 1996
		public static FunctionParameterToken[] EmptyParameterList = new FunctionParameterToken[0];

		// Token: 0x040007CD RID: 1997
		private readonly string parameterName;

		// Token: 0x040007CE RID: 1998
		private readonly QueryToken valueToken;
	}
}
