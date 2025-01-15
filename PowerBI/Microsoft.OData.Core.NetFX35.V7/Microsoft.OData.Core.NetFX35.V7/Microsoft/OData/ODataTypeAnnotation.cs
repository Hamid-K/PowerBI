using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x0200009C RID: 156
	public sealed class ODataTypeAnnotation
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x00002CFE File Offset: 0x00000EFE
		public ODataTypeAnnotation()
		{
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000100A4 File Offset: 0x0000E2A4
		public ODataTypeAnnotation(string typeName)
		{
			this.TypeName = typeName;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000100B3 File Offset: 0x0000E2B3
		internal ODataTypeAnnotation(string typeName, IEdmType type)
			: this(typeName)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmType>(type, "type");
			this.Type = type;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x000100CF File Offset: 0x0000E2CF
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x000100D7 File Offset: 0x0000E2D7
		public string TypeName { get; private set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x000100E0 File Offset: 0x0000E2E0
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x000100E8 File Offset: 0x0000E2E8
		internal IEdmType Type { get; private set; }
	}
}
