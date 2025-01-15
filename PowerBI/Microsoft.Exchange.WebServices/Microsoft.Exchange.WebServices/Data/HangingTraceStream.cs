using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000298 RID: 664
	internal class HangingTraceStream : Stream
	{
		// Token: 0x06001761 RID: 5985 RVA: 0x0003FA96 File Offset: 0x0003EA96
		internal HangingTraceStream(Stream stream, ExchangeService service)
		{
			this.underlyingStream = stream;
			this.service = service;
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x0003FAAC File Offset: 0x0003EAAC
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x0003FAAF File Offset: 0x0003EAAF
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x0003FAB2 File Offset: 0x0003EAB2
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0003FAB5 File Offset: 0x0003EAB5
		public override void Flush()
		{
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001766 RID: 5990 RVA: 0x0003FAB7 File Offset: 0x0003EAB7
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x0003FABE File Offset: 0x0003EABE
		// (set) Token: 0x06001768 RID: 5992 RVA: 0x0003FAC5 File Offset: 0x0003EAC5
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0003FACC File Offset: 0x0003EACC
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.underlyingStream.Read(buffer, offset, count);
			if (HangingServiceRequestBase.LogAllWireBytes)
			{
				string @string = Encoding.UTF8.GetString(buffer, offset, num);
				string text = string.Format("HangingTraceStream ID [{0}] returned {1} bytes. Bytes returned: [{2}]", this.GetHashCode(), num, @string);
				this.service.TraceMessage(TraceFlags.DebugMessage, text);
			}
			if (this.responseCopy != null)
			{
				this.responseCopy.Write(buffer, offset, num);
			}
			return num;
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0003FB43 File Offset: 0x0003EB43
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0003FB4A File Offset: 0x0003EB4A
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0003FB51 File Offset: 0x0003EB51
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0003FB58 File Offset: 0x0003EB58
		internal void SetResponseCopy(MemoryStream responseCopy)
		{
			this.responseCopy = responseCopy;
		}

		// Token: 0x04001353 RID: 4947
		private Stream underlyingStream;

		// Token: 0x04001354 RID: 4948
		private ExchangeService service;

		// Token: 0x04001355 RID: 4949
		private MemoryStream responseCopy;
	}
}
