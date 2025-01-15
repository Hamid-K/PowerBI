using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200002A RID: 42
	internal sealed class SafeCryptoBlobIn : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000131 RID: 305 RVA: 0x000058B0 File Offset: 0x00003AB0
		internal SafeCryptoBlobIn(byte[] data)
			: base(true)
		{
			this.m_bufferSize = data.Length;
			this.m_blob = SafeLocalFree.LocalAlloc(Marshal.SizeOf(typeof(NativeMethods.DATA_BLOB)));
			this.handle = this.m_blob.DangerousGetHandle();
			this.m_pbData = SafeLocalFree.LocalAlloc(data.Length);
			Marshal.Copy(data, 0, this.m_pbData.DangerousGetHandle(), data.Length);
			Marshal.StructureToPtr<NativeMethods.DATA_BLOB>(new NativeMethods.DATA_BLOB
			{
				cbData = data.Length,
				pbData = this.m_pbData.DangerousGetHandle()
			}, this.handle, true);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000594C File Offset: 0x00003B4C
		internal void ZeroBuffer()
		{
			if (!this.IsInvalid && this.m_bufferSize > 0 && this.m_pbData != null)
			{
				Marshal.Copy(new byte[this.m_bufferSize], 0, this.m_pbData.DangerousGetHandle(), this.m_bufferSize);
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005989 File Offset: 0x00003B89
		protected override bool ReleaseHandle()
		{
			if (this.m_pbData != null)
			{
				this.m_pbData.Close();
			}
			if (this.m_blob != null)
			{
				this.m_blob.Close();
			}
			return true;
		}

		// Token: 0x040000A1 RID: 161
		private int m_bufferSize;

		// Token: 0x040000A2 RID: 162
		private SafeLocalFree m_blob;

		// Token: 0x040000A3 RID: 163
		private SafeLocalFree m_pbData;
	}
}
