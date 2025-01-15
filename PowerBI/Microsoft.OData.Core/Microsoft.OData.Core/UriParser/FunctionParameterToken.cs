using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001BF RID: 447
	public sealed class FunctionParameterToken : QueryToken
	{
		// Token: 0x060014BD RID: 5309 RVA: 0x0003BFBE File Offset: 0x0003A1BE
		public FunctionParameterToken(string parameterName, QueryToken valueToken)
		{
			this.parameterName = parameterName;
			this.valueToken = valueToken;
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x0003BFD4 File Offset: 0x0003A1D4
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x0003BFDC File Offset: 0x0003A1DC
		public QueryToken ValueToken
		{
			get
			{
				return this.valueToken;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x0003A9D8 File Offset: 0x00038BD8
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.FunctionParameter;
			}
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0003BFE4 File Offset: 0x0003A1E4
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400090F RID: 2319
		public static FunctionParameterToken[] EmptyParameterList = new FunctionParameterToken[0];

		// Token: 0x04000910 RID: 2320
		private readonly string parameterName;

		// Token: 0x04000911 RID: 2321
		private readonly QueryToken valueToken;
	}
}
