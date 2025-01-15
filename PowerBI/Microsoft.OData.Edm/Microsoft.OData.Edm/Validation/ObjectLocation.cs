using System;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000140 RID: 320
	public class ObjectLocation : EdmLocation
	{
		// Token: 0x0600080B RID: 2059 RVA: 0x00013C1E File Offset: 0x00011E1E
		internal ObjectLocation(object obj)
		{
			this.Object = obj;
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x00013C2D File Offset: 0x00011E2D
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x00013C35 File Offset: 0x00011E35
		public object Object { get; private set; }

		// Token: 0x0600080E RID: 2062 RVA: 0x00013C3E File Offset: 0x00011E3E
		public override string ToString()
		{
			return "(" + this.Object.ToString() + ")";
		}
	}
}
