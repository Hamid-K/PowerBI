using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000BD RID: 189
	public sealed class ODataTypeAnnotation
	{
		// Token: 0x06000858 RID: 2136 RVA: 0x000036A9 File Offset: 0x000018A9
		public ODataTypeAnnotation()
		{
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001397C File Offset: 0x00011B7C
		public ODataTypeAnnotation(string typeName)
		{
			this.TypeName = typeName;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001398B File Offset: 0x00011B8B
		internal ODataTypeAnnotation(string typeName, IEdmType type)
			: this(typeName)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			this.Type = type;
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x000139A7 File Offset: 0x00011BA7
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x000139AF File Offset: 0x00011BAF
		public string TypeName { get; private set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x000139B8 File Offset: 0x00011BB8
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x000139C0 File Offset: 0x00011BC0
		internal IEdmType Type { get; private set; }
	}
}
