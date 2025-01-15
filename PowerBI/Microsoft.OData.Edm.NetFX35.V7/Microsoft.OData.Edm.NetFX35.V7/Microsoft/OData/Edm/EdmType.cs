using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000079 RID: 121
	public abstract class EdmType : EdmElement, IEdmType, IEdmElement
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600042A RID: 1066
		public abstract EdmTypeKind TypeKind { get; }

		// Token: 0x0600042B RID: 1067 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
		public override string ToString()
		{
			return this.ToTraceString();
		}
	}
}
