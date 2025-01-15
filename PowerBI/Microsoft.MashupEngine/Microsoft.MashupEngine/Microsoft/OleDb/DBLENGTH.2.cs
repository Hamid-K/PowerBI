using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001ED8 RID: 7896
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBLENGTH
	{
		// Token: 0x17002F9F RID: 12191
		// (get) Token: 0x0600C27B RID: 49787 RVA: 0x00270BCC File Offset: 0x0026EDCC
		public static DBLENGTH MaxValue
		{
			get
			{
				return new DBLENGTH
				{
					value = ulong.MaxValue
				};
			}
		}

		// Token: 0x17002FA0 RID: 12192
		// (get) Token: 0x0600C27C RID: 49788 RVA: 0x00270BEC File Offset: 0x0026EDEC
		public static DBLENGTH Size
		{
			get
			{
				return new DBLENGTH
				{
					value = 8UL
				};
			}
		}

		// Token: 0x040063DE RID: 25566
		public ulong value;
	}
}
