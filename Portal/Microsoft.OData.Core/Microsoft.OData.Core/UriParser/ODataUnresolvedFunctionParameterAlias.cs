using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000194 RID: 404
	public class ODataUnresolvedFunctionParameterAlias
	{
		// Token: 0x0600139E RID: 5022 RVA: 0x0003A0B0 File Offset: 0x000382B0
		public ODataUnresolvedFunctionParameterAlias(string alias, IEdmTypeReference type)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(alias, "alias");
			this.Alias = alias;
			this.Type = type;
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x0003A0D1 File Offset: 0x000382D1
		// (set) Token: 0x060013A0 RID: 5024 RVA: 0x0003A0D9 File Offset: 0x000382D9
		public IEdmTypeReference Type { get; private set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x0003A0E2 File Offset: 0x000382E2
		// (set) Token: 0x060013A2 RID: 5026 RVA: 0x0003A0EA File Offset: 0x000382EA
		public string Alias { get; private set; }
	}
}
