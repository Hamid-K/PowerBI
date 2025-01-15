using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020000FC RID: 252
	public abstract class EdmType : EdmElement, IEdmType, IEdmElement
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060004F8 RID: 1272
		public abstract EdmTypeKind TypeKind { get; }

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000D1D1 File Offset: 0x0000B3D1
		public override string ToString()
		{
			return this.ToTraceString();
		}
	}
}
