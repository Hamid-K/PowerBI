using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.Interop
{
	// Token: 0x02000130 RID: 304
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct SecBufferDesc
	{
		// Token: 0x0600107E RID: 4222 RVA: 0x00039078 File Offset: 0x00037278
		public SecBufferDesc(int count, IEnumerable<SecBuffer> buffers)
		{
			this.ulVersion = 0U;
			this.cbBuffers = (uint)count;
			if (count > 0)
			{
				this.pBuffers = Marshal.AllocHGlobal(SecBuffer.Size * count);
				int num = 0;
				using (IEnumerator<SecBuffer> enumerator = buffers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SecBuffer secBuffer = enumerator.Current;
						secBuffer.CopyTo(InteropHelper.Add(this.pBuffers, num * SecBuffer.Size));
						num++;
					}
					return;
				}
			}
			this.pBuffers = IntPtr.Zero;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00039108 File Offset: 0x00037308
		public IEnumerable<SecBuffer> GetBuffers()
		{
			int i = 0;
			while ((long)i < (long)((ulong)this.cbBuffers))
			{
				yield return new SecBuffer(InteropHelper.Add(this.pBuffers, i * SecBuffer.Size));
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00039120 File Offset: 0x00037320
		public void Release(bool releaseBuffers)
		{
			if (this.cbBuffers > 0U && this.pBuffers != IntPtr.Zero)
			{
				if (releaseBuffers)
				{
					int num = 0;
					while ((long)num < (long)((ulong)this.cbBuffers))
					{
						IntPtr intPtr = Marshal.ReadIntPtr(this.pBuffers, num * SecBuffer.Size + 8);
						if (intPtr != IntPtr.Zero)
						{
							int num2 = NativeMethods.FreeContextBuffer(intPtr);
							if (num2 != 0)
							{
								throw new Win32Exception(num2);
							}
						}
						num++;
					}
				}
				Marshal.FreeHGlobal(this.pBuffers);
				this.pBuffers = IntPtr.Zero;
				this.cbBuffers = 0U;
			}
		}

		// Token: 0x04000AA1 RID: 2721
		private const uint SECBUFFER_VERSION = 0U;

		// Token: 0x04000AA2 RID: 2722
		public uint ulVersion;

		// Token: 0x04000AA3 RID: 2723
		public uint cbBuffers;

		// Token: 0x04000AA4 RID: 2724
		public IntPtr pBuffers;
	}
}
