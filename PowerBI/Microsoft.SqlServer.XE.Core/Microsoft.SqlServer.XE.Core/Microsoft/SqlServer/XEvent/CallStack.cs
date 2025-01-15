using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000028 RID: 40
	public class CallStack
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x0000406C File Offset: 0x0000406C
		internal CallStack(IntPtr stack, int len, int ptrSize)
		{
			this.m_ptrSize = ptrSize;
			if (this.m_ptrSize == 4)
			{
				this.m_frameFormat = "0x{0:X8}\r\n";
				this.m_stackFrames = new ulong[len / 4];
				for (int i = 0; i < len / 4; i++)
				{
					this.m_stackFrames[i] = (ulong)Marshal.ReadInt32(stack, i * 4);
				}
				return;
			}
			if (this.m_ptrSize == 8)
			{
				this.m_frameFormat = "0x{0:X16}\r\n";
				this.m_stackFrames = new ulong[len / 8];
				for (int j = 0; j < len / 8; j++)
				{
					this.m_stackFrames[j] = (ulong)Marshal.ReadInt64(stack, j * 8);
				}
				return;
			}
			if (this.m_ptrSize == 0)
			{
				this.m_stackBytes = new byte[len];
				for (int k = 0; k < len; k++)
				{
					this.m_stackBytes[k] = Marshal.ReadByte(stack, k);
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000413C File Offset: 0x0000413C
		public override string ToString()
		{
			if (this.m_stackString == null && this.m_stackFrames != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < this.m_stackFrames.Length; i++)
				{
					stringBuilder.AppendFormat(this.m_frameFormat, this.m_stackFrames[i]);
				}
				this.m_stackString = stringBuilder.ToString();
			}
			return this.m_stackString;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000041A0 File Offset: 0x000041A0
		public static explicit operator byte[](CallStack stk)
		{
			if (stk.m_stackBytes == null)
			{
				stk.m_stackBytes = new byte[stk.m_stackFrames.Length * 8];
				Buffer.BlockCopy(stk.m_stackFrames, 0, stk.m_stackBytes, 0, stk.m_stackBytes.Length);
			}
			return stk.m_stackBytes;
		}

		// Token: 0x04000056 RID: 86
		private byte[] m_stackBytes;

		// Token: 0x04000057 RID: 87
		private ulong[] m_stackFrames;

		// Token: 0x04000058 RID: 88
		private int m_ptrSize;

		// Token: 0x04000059 RID: 89
		private string m_stackString;

		// Token: 0x0400005A RID: 90
		private const string m_frameFormat32 = "0x{0:X8}\r\n";

		// Token: 0x0400005B RID: 91
		private const string m_frameFormat64 = "0x{0:X16}\r\n";

		// Token: 0x0400005C RID: 92
		private string m_frameFormat;
	}
}
