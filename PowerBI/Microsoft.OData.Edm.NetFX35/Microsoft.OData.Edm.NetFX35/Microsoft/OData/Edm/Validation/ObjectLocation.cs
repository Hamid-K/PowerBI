using System;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x0200016A RID: 362
	public class ObjectLocation : EdmLocation
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x00010147 File Offset: 0x0000E347
		internal ObjectLocation(object obj)
		{
			this.Object = obj;
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00010156 File Offset: 0x0000E356
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x0001015E File Offset: 0x0000E35E
		public object Object { get; private set; }

		// Token: 0x060006D7 RID: 1751 RVA: 0x00010167 File Offset: 0x0000E367
		public override string ToString()
		{
			return "(" + this.Object.ToString() + ")";
		}
	}
}
