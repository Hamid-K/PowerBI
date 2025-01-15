using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000482 RID: 1154
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public sealed class ECFSoapHeaderAttribute : ECFRemovableParamAttribute
	{
		// Token: 0x060023AC RID: 9132 RVA: 0x00080A66 File Offset: 0x0007EC66
		public ECFSoapHeaderAttribute(string headerName, string headerNamespace)
		{
			this.HeaderName = headerName;
			this.HeaderNamespace = headerNamespace;
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x00080A7C File Offset: 0x0007EC7C
		// (set) Token: 0x060023AE RID: 9134 RVA: 0x00080A84 File Offset: 0x0007EC84
		public string HeaderName { get; private set; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x00080A8D File Offset: 0x0007EC8D
		// (set) Token: 0x060023B0 RID: 9136 RVA: 0x00080A95 File Offset: 0x0007EC95
		public string HeaderNamespace { get; private set; }
	}
}
