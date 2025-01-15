using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200002B RID: 43
	internal sealed class SafeCryptoBlobOut : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000134 RID: 308 RVA: 0x000059B2 File Offset: 0x00003BB2
		internal SafeCryptoBlobOut()
			: base(true)
		{
			this.m_pBlob = SafeLocalFree.LocalAlloc(Marshal.SizeOf(typeof(NativeMethods.DATA_BLOB)));
			this.handle = this.m_pBlob.DangerousGetHandle();
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000059E6 File Offset: 0x00003BE6
		internal NativeMethods.DATA_BLOB Blob
		{
			get
			{
				if (!this.m_managedBlobInitialized)
				{
					this.m_blob = (NativeMethods.DATA_BLOB)Marshal.PtrToStructure(this.handle, typeof(NativeMethods.DATA_BLOB));
					this.m_managedBlobInitialized = true;
				}
				return this.m_blob;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005A20 File Offset: 0x00003C20
		internal void ZeroBuffer()
		{
			NativeMethods.DATA_BLOB blob = this.Blob;
			byte[] array = new byte[blob.cbData];
			Marshal.Copy(blob.pbData, array, 0, blob.cbData);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005A53 File Offset: 0x00003C53
		protected override bool ReleaseHandle()
		{
			if (this.m_pBlob != null)
			{
				NativeMemoryMethods.LocalFree(this.Blob.pbData);
				this.m_pBlob.Close();
			}
			return true;
		}

		// Token: 0x040000A4 RID: 164
		private SafeLocalFree m_pBlob;

		// Token: 0x040000A5 RID: 165
		private bool m_managedBlobInitialized;

		// Token: 0x040000A6 RID: 166
		private NativeMethods.DATA_BLOB m_blob;
	}
}
