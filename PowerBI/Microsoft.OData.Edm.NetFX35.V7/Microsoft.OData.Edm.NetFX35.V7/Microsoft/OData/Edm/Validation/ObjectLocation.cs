using System;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x020000D1 RID: 209
	public class ObjectLocation : EdmLocation
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x0000E932 File Offset: 0x0000CB32
		internal ObjectLocation(object obj)
		{
			this.Object = obj;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0000E941 File Offset: 0x0000CB41
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x0000E949 File Offset: 0x0000CB49
		public object Object { get; private set; }

		// Token: 0x06000611 RID: 1553 RVA: 0x0000E952 File Offset: 0x0000CB52
		public override string ToString()
		{
			return "(" + this.Object.ToString() + ")";
		}
	}
}
