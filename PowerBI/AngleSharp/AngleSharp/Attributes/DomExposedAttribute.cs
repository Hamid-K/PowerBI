using System;

namespace AngleSharp.Attributes
{
	// Token: 0x0200040D RID: 1037
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	public sealed class DomExposedAttribute : Attribute
	{
		// Token: 0x06002106 RID: 8454 RVA: 0x0005883A File Offset: 0x00056A3A
		public DomExposedAttribute(string target)
		{
			this.Target = target;
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06002107 RID: 8455 RVA: 0x00058849 File Offset: 0x00056A49
		// (set) Token: 0x06002108 RID: 8456 RVA: 0x00058851 File Offset: 0x00056A51
		public string Target { get; private set; }
	}
}
