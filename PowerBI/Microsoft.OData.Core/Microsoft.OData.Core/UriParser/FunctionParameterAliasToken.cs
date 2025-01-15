using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001BE RID: 446
	internal sealed class FunctionParameterAliasToken : QueryToken
	{
		// Token: 0x060014B6 RID: 5302 RVA: 0x0003BF7E File Offset: 0x0003A17E
		public FunctionParameterAliasToken(string alias)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(alias, "alias");
			this.Alias = alias;
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x0003BF98 File Offset: 0x0003A198
		// (set) Token: 0x060014B8 RID: 5304 RVA: 0x0003BFA0 File Offset: 0x0003A1A0
		public string Alias { get; private set; }

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x0003BFA9 File Offset: 0x0003A1A9
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.FunctionParameterAlias;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x0003BFAD File Offset: 0x0003A1AD
		// (set) Token: 0x060014BB RID: 5307 RVA: 0x0003BFB5 File Offset: 0x0003A1B5
		internal IEdmTypeReference ExpectedParameterType { get; set; }

		// Token: 0x060014BC RID: 5308 RVA: 0x000032BD File Offset: 0x000014BD
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}
	}
}
