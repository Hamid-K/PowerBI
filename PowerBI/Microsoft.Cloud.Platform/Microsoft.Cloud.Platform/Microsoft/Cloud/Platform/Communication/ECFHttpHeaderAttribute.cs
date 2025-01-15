using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200047E RID: 1150
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public sealed class ECFHttpHeaderAttribute : ECFRemovableParamAttribute
	{
		// Token: 0x060023A6 RID: 9126 RVA: 0x00080A3E File Offset: 0x0007EC3E
		public ECFHttpHeaderAttribute(string headerName)
		{
			this.HeaderName = headerName;
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x00080A4D File Offset: 0x0007EC4D
		// (set) Token: 0x060023A8 RID: 9128 RVA: 0x00080A55 File Offset: 0x0007EC55
		public string HeaderName { get; private set; }
	}
}
