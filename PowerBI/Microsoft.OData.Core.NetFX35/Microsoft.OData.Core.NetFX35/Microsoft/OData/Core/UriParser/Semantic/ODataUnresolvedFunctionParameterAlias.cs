using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200024F RID: 591
	public class ODataUnresolvedFunctionParameterAlias : ODataValue
	{
		// Token: 0x060014FB RID: 5371 RVA: 0x0004A674 File Offset: 0x00048874
		public ODataUnresolvedFunctionParameterAlias(string alias, IEdmTypeReference type)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(alias, "alias");
			this.Alias = alias;
			this.Type = type;
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x0004A695 File Offset: 0x00048895
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x0004A69D File Offset: 0x0004889D
		public IEdmTypeReference Type { get; private set; }

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x0004A6A6 File Offset: 0x000488A6
		// (set) Token: 0x060014FF RID: 5375 RVA: 0x0004A6AE File Offset: 0x000488AE
		public string Alias { get; private set; }
	}
}
