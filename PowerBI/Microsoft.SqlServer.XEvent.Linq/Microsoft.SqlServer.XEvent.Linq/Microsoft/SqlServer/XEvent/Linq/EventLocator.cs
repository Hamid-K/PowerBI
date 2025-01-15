using System;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000D0 RID: 208
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public class EventLocator
	{
		// Token: 0x0400028E RID: 654
		internal ulong m_Handle1;

		// Token: 0x0400028F RID: 655
		internal uint m_Handle2;
	}
}
