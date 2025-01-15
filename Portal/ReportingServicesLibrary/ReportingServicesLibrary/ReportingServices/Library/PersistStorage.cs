using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200016C RID: 364
	internal sealed class PersistStorage
	{
		// Token: 0x06000D77 RID: 3447
		[DllImport("ole32.dll")]
		private static extern int StgCreateDocfileOnILockBytes([In] ILockBytes plkbyt, [In] STGM grfMode, [In] uint reserved, out ComIStorage ppstgOpen);

		// Token: 0x06000D78 RID: 3448
		[DllImport("ReportingServicesRenderingNative.dll", CharSet = CharSet.Unicode)]
		internal static extern int CreateILockBytesOnHeap(out ILockBytes lockBytes);

		// Token: 0x06000D79 RID: 3449 RVA: 0x00030C88 File Offset: 0x0002EE88
		public PersistStorage()
		{
			if (PersistStorage.CreateILockBytesOnHeap(out this.m_lockBytes) != 0)
			{
				throw new PowerPointExportException(RepLibRes.UnableToAllocateILockBytes);
			}
			if (PersistStorage.StgCreateDocfileOnILockBytes(this.m_lockBytes, STGM.READWRITE | STGM.SHARE_EXCLUSIVE | STGM.CREATE, 0U, out this.m_storage) != 0)
			{
				throw new PowerPointExportException(RepLibRes.UnableToCreateStorage);
			}
			this.m_storage.CreateStream("Contents", STGM.WRITE | STGM.SHARE_EXCLUSIVE | STGM.CREATE, 0U, 0U, out this.m_stream);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00030CF4 File Offset: 0x0002EEF4
		public void SetContent(Stream data)
		{
			data.Position = 0L;
			this.m_stream.SetSize(data.Length);
			this.m_stream.Seek(0L, 0, IntPtr.Zero);
			byte[] array = new byte[8192];
			int num = 0;
			while ((long)num < data.Length)
			{
				int num2 = Math.Min((int)data.Length - num, array.Length);
				int num3 = data.Read(array, 0, num2);
				num += num3;
				this.m_stream.Write(array, num3, IntPtr.Zero);
			}
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00030D78 File Offset: 0x0002EF78
		public Stream GetData()
		{
			this.m_storage.Commit(0U);
			this.m_lockBytes.Flush();
			global::System.Runtime.InteropServices.ComTypes.STATSTG statstg;
			this.m_lockBytes.Stat(out statstg, STATFLAG.STATFLAG_DEFAULT);
			Stream stream = SegmentedMemoryStream.CreateMemoryStream((int)statstg.cbSize);
			byte[] array = new byte[8192];
			int num = 0;
			while ((long)num < statstg.cbSize)
			{
				int num2;
				this.m_lockBytes.ReadAt((ulong)((long)num), array, array.Length, out num2);
				num += num2;
				stream.Write(array, 0, num2);
			}
			stream.Position = 0L;
			return stream;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00030DFB File Offset: 0x0002EFFB
		public void Close()
		{
			Marshal.FinalReleaseComObject(this.m_stream);
			Marshal.FinalReleaseComObject(this.m_storage);
			Marshal.FinalReleaseComObject(this.m_lockBytes);
		}

		// Token: 0x0400056E RID: 1390
		private IStream m_stream;

		// Token: 0x0400056F RID: 1391
		private ComIStorage m_storage;

		// Token: 0x04000570 RID: 1392
		private ILockBytes m_lockBytes;
	}
}
