using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000148 RID: 328
	public class ODataUnresolvedFunctionParameterAlias
	{
		// Token: 0x06000E8D RID: 3725 RVA: 0x0002A294 File Offset: 0x00028494
		public ODataUnresolvedFunctionParameterAlias(string alias, IEdmTypeReference type)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(alias, "alias");
			this.Alias = alias;
			this.Type = type;
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x0002A2B5 File Offset: 0x000284B5
		// (set) Token: 0x06000E8F RID: 3727 RVA: 0x0002A2BD File Offset: 0x000284BD
		public IEdmTypeReference Type { get; private set; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x0002A2C6 File Offset: 0x000284C6
		// (set) Token: 0x06000E91 RID: 3729 RVA: 0x0002A2CE File Offset: 0x000284CE
		public string Alias { get; private set; }
	}
}
