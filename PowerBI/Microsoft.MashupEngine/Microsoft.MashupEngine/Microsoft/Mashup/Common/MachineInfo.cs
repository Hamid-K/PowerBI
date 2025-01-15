using System;
using System.Runtime.InteropServices;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C01 RID: 7169
	public static class MachineInfo
	{
		// Token: 0x0600B2F8 RID: 45816 RVA: 0x00246DB5 File Offset: 0x00244FB5
		public static int GetActiveProcessorGroupCount()
		{
			return (int)MachineInfo.NativeMethods.GetActiveProcessorGroupCount();
		}

		// Token: 0x0600B2F9 RID: 45817 RVA: 0x00246DBC File Offset: 0x00244FBC
		public static int GetActiveProcessorCount(int group)
		{
			return MachineInfo.NativeMethods.GetActiveProcessorCount((short)group);
		}

		// Token: 0x0600B2FA RID: 45818 RVA: 0x00246DC8 File Offset: 0x00244FC8
		public static int GetTotalProcessorCount()
		{
			int activeProcessorGroupCount = MachineInfo.GetActiveProcessorGroupCount();
			int num = 0;
			for (int i = 0; i < activeProcessorGroupCount; i++)
			{
				num += MachineInfo.GetActiveProcessorCount(i);
			}
			return num;
		}

		// Token: 0x02001C02 RID: 7170
		private static class NativeMethods
		{
			// Token: 0x0600B2FB RID: 45819
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern short GetActiveProcessorGroupCount();

			// Token: 0x0600B2FC RID: 45820
			[DllImport("kernel32.dll", SetLastError = true)]
			public static extern int GetActiveProcessorCount(short group);
		}
	}
}
