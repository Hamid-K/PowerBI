using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200047D RID: 1149
	[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class ECFGeneratedInterfaceAttribute : Attribute
	{
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060023A3 RID: 9123 RVA: 0x00080A2D File Offset: 0x0007EC2D
		// (set) Token: 0x060023A4 RID: 9124 RVA: 0x00080A35 File Offset: 0x0007EC35
		public string OriginalInterface { get; set; }
	}
}
