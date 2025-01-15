using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000122 RID: 290
	internal sealed class FunctionParameterAliasToken : QueryToken
	{
		// Token: 0x06000C0D RID: 3085 RVA: 0x0002CD9D File Offset: 0x0002AF9D
		public FunctionParameterAliasToken(string alias)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(alias, "alias");
			this.Alias = alias;
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0002CDB7 File Offset: 0x0002AFB7
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x0002CDBF File Offset: 0x0002AFBF
		public string Alias { get; private set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0002CDC8 File Offset: 0x0002AFC8
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.FunctionParameterAlias;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0002CDCC File Offset: 0x0002AFCC
		// (set) Token: 0x06000C12 RID: 3090 RVA: 0x0002CDD4 File Offset: 0x0002AFD4
		internal IEdmTypeReference ExpectedParameterType { get; set; }

		// Token: 0x06000C13 RID: 3091 RVA: 0x00006FEF File Offset: 0x000051EF
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}
	}
}
