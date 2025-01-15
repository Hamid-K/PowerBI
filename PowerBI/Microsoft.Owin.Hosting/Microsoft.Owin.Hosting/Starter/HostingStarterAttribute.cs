using System;

namespace Microsoft.Owin.Hosting.Starter
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Assembly)]
	public sealed class HostingStarterAttribute : Attribute
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00003620 File Offset: 0x00001820
		public HostingStarterAttribute(Type hostingStarterType)
		{
			this.HostingStarterType = hostingStarterType;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000362F File Offset: 0x0000182F
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003637 File Offset: 0x00001837
		public Type HostingStarterType { get; private set; }
	}
}
