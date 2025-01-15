using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x0200008B RID: 139
	public class ODataUnresolvedFunctionParameterAlias : ODataValue
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0000B631 File Offset: 0x00009831
		public ODataUnresolvedFunctionParameterAlias(string alias, IEdmTypeReference type)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(alias, "alias");
			this.Alias = alias;
			this.Type = type;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000B652 File Offset: 0x00009852
		// (set) Token: 0x06000345 RID: 837 RVA: 0x0000B65A File Offset: 0x0000985A
		public IEdmTypeReference Type { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000B663 File Offset: 0x00009863
		// (set) Token: 0x06000347 RID: 839 RVA: 0x0000B66B File Offset: 0x0000986B
		public string Alias { get; private set; }
	}
}
