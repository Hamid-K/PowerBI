using System;

namespace Microsoft.OData.Client
{
	// Token: 0x020000C2 RID: 194
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public sealed class NamedStreamAttribute : Attribute
	{
		// Token: 0x06000656 RID: 1622 RVA: 0x0001BE5E File Offset: 0x0001A05E
		public NamedStreamAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x0001BE6D File Offset: 0x0001A06D
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x0001BE75 File Offset: 0x0001A075
		public string Name { get; private set; }
	}
}
