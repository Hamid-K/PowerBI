using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000090 RID: 144
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBLENGTH
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000899C File Offset: 0x00006B9C
		public static DBLENGTH MaxValue
		{
			get
			{
				return new DBLENGTH
				{
					Value = ulong.MaxValue
				};
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x000089BC File Offset: 0x00006BBC
		public static DBLENGTH Size
		{
			get
			{
				return new DBLENGTH
				{
					Value = 8UL
				};
			}
		}

		// Token: 0x040002A9 RID: 681
		public ulong Value;
	}
}
