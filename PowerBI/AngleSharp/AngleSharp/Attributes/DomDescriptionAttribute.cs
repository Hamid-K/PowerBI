using System;

namespace AngleSharp.Attributes
{
	// Token: 0x0200040C RID: 1036
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	public sealed class DomDescriptionAttribute : Attribute
	{
		// Token: 0x06002103 RID: 8451 RVA: 0x0005881A File Offset: 0x00056A1A
		public DomDescriptionAttribute(string description)
		{
			this.Description = description;
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x00058829 File Offset: 0x00056A29
		// (set) Token: 0x06002105 RID: 8453 RVA: 0x00058831 File Offset: 0x00056A31
		public string Description { get; private set; }
	}
}
