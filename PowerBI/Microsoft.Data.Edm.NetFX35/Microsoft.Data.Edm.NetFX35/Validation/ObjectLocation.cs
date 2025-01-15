using System;

namespace Microsoft.Data.Edm.Validation
{
	// Token: 0x02000123 RID: 291
	public class ObjectLocation : EdmLocation
	{
		// Token: 0x06000595 RID: 1429 RVA: 0x0000DBBE File Offset: 0x0000BDBE
		internal ObjectLocation(object obj)
		{
			this.Object = obj;
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0000DBCD File Offset: 0x0000BDCD
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x0000DBD5 File Offset: 0x0000BDD5
		public object Object { get; private set; }

		// Token: 0x06000598 RID: 1432 RVA: 0x0000DBDE File Offset: 0x0000BDDE
		public override string ToString()
		{
			return "(" + this.Object.ToString() + ")";
		}
	}
}
