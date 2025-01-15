using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000172 RID: 370
	internal sealed class FunctionParameterAliasToken : QueryToken
	{
		// Token: 0x06000F89 RID: 3977 RVA: 0x0002BFAA File Offset: 0x0002A1AA
		public FunctionParameterAliasToken(string alias)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(alias, "alias");
			this.Alias = alias;
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x0002BFC4 File Offset: 0x0002A1C4
		// (set) Token: 0x06000F8B RID: 3979 RVA: 0x0002BFCC File Offset: 0x0002A1CC
		public string Alias { get; private set; }

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x0002BFD5 File Offset: 0x0002A1D5
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.FunctionParameterAlias;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0002BFD9 File Offset: 0x0002A1D9
		// (set) Token: 0x06000F8E RID: 3982 RVA: 0x0002BFE1 File Offset: 0x0002A1E1
		internal IEdmTypeReference ExpectedParameterType { get; set; }

		// Token: 0x06000F8F RID: 3983 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}
	}
}
