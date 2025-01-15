using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000C4 RID: 196
	public abstract class EdmType : EdmElement, IEdmType, IEdmElement
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060004AC RID: 1196
		public abstract EdmTypeKind TypeKind { get; }

		// Token: 0x060004AD RID: 1197 RVA: 0x0000BE23 File Offset: 0x0000A023
		public override string ToString()
		{
			return this.ToTraceString();
		}
	}
}
