using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004C8 RID: 1224
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class DispatchByBodyElementAttribute : Attribute
	{
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x00084C36 File Offset: 0x00082E36
		// (set) Token: 0x06002557 RID: 9559 RVA: 0x00084C3E File Offset: 0x00082E3E
		public string BodyElement { get; private set; }

		// Token: 0x06002558 RID: 9560 RVA: 0x00084C47 File Offset: 0x00082E47
		public DispatchByBodyElementAttribute(string bodyElement)
		{
			this.BodyElement = bodyElement;
		}
	}
}
