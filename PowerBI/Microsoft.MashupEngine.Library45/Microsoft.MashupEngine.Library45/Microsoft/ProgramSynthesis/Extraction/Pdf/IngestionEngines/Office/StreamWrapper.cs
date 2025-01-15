using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.IngestionEngines.Office
{
	// Token: 0x02000DAC RID: 3500
	[NullableContext(1)]
	[Nullable(0)]
	internal class StreamWrapper : IStream
	{
		// Token: 0x06005922 RID: 22818 RVA: 0x0011B7FD File Offset: 0x001199FD
		public StreamWrapper(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream", "Can't wrap null stream.");
			}
			this._stream = stream;
		}

		// Token: 0x06005923 RID: 22819 RVA: 0x0011B820 File Offset: 0x00119A20
		public void Read(byte[] pv, int cb, IntPtr pcbRead)
		{
			int num = this._stream.RepeatRead(pv, 0, cb);
			Marshal.WriteInt32(pcbRead, num);
		}

		// Token: 0x06005924 RID: 22820 RVA: 0x000170F6 File Offset: 0x000152F6
		public void Write(byte[] pv, int cb, IntPtr pcbWritten)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005925 RID: 22821 RVA: 0x0011B844 File Offset: 0x00119A44
		public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
		{
			long num = ((dlibMove == 0L && dwOrigin == 1) ? this._stream.Position : this._stream.Seek(dlibMove, (SeekOrigin)dwOrigin));
			if (plibNewPosition != IntPtr.Zero)
			{
				Marshal.WriteInt64(plibNewPosition, num);
			}
		}

		// Token: 0x06005926 RID: 22822 RVA: 0x000170F6 File Offset: 0x000152F6
		public void SetSize(long libNewSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005927 RID: 22823 RVA: 0x000170F6 File Offset: 0x000152F6
		public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x000170F6 File Offset: 0x000152F6
		public void Commit(int grfCommitFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x000170F6 File Offset: 0x000152F6
		public void Revert()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600592A RID: 22826 RVA: 0x000170F6 File Offset: 0x000152F6
		public void LockRegion(long libOffset, long cb, int dwLockType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x000170F6 File Offset: 0x000152F6
		public void UnlockRegion(long libOffset, long cb, int dwLockType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600592C RID: 22828 RVA: 0x0011B88C File Offset: 0x00119A8C
		public void Stat(out global::System.Runtime.InteropServices.ComTypes.STATSTG streamStats, int grfStatFlag)
		{
			streamStats = new global::System.Runtime.InteropServices.ComTypes.STATSTG
			{
				type = 2,
				cbSize = this._stream.Length,
				grfMode = 0
			};
			if (this._stream.CanRead && this._stream.CanWrite)
			{
				streamStats.grfMode |= 2;
				return;
			}
			if (this._stream.CanRead)
			{
				streamStats.grfMode |= 0;
				return;
			}
			if (this._stream.CanWrite)
			{
				streamStats.grfMode |= 1;
				return;
			}
			throw new IOException();
		}

		// Token: 0x0600592D RID: 22829 RVA: 0x000170F6 File Offset: 0x000152F6
		public void Clone(out IStream ppstm)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002972 RID: 10610
		private readonly Stream _stream;
	}
}
