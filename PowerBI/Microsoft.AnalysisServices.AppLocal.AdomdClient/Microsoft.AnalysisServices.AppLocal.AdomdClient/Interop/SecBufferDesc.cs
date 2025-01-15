using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.AdomdClient.Interop
{
	// Token: 0x0200013B RID: 315
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct SecBufferDesc
	{
		// Token: 0x06000FF0 RID: 4080 RVA: 0x00036774 File Offset: 0x00034974
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

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00036804 File Offset: 0x00034A04
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

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0003681C File Offset: 0x00034A1C
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

		// Token: 0x04000AE8 RID: 2792
		private const uint SECBUFFER_VERSION = 0U;

		// Token: 0x04000AE9 RID: 2793
		public uint ulVersion;

		// Token: 0x04000AEA RID: 2794
		public uint cbBuffers;

		// Token: 0x04000AEB RID: 2795
		public IntPtr pBuffers;
	}
}
