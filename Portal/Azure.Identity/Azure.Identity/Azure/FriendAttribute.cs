using System;

namespace Azure
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class FriendAttribute : Attribute
	{
		// Token: 0x06000029 RID: 41 RVA: 0x0000222C File Offset: 0x0000042C
		public FriendAttribute(string friendAssembly)
		{
			this.FriendAssembly = friendAssembly;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000223B File Offset: 0x0000043B
		public string FriendAssembly { get; }
	}
}
