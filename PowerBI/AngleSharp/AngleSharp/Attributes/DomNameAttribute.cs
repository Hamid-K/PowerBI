using System;

namespace AngleSharp.Attributes
{
	// Token: 0x02000412 RID: 1042
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	public sealed class DomNameAttribute : Attribute
	{
		// Token: 0x06002111 RID: 8465 RVA: 0x00058892 File Offset: 0x00056A92
		public DomNameAttribute(string officialName)
		{
			this.OfficialName = officialName;
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x000588A1 File Offset: 0x00056AA1
		// (set) Token: 0x06002113 RID: 8467 RVA: 0x000588A9 File Offset: 0x00056AA9
		public string OfficialName { get; private set; }
	}
}
