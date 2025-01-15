using System;
using System.IO;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200002F RID: 47
	internal sealed class MemoryXmlaStream : XmlaStream
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x0000CA3C File Offset: 0x0000AC3C
		internal MemoryXmlaStream(XmlaDataType streamDataType)
			: base(false)
		{
			this.baseStream = new MemoryStream();
			this.streamDataType = streamDataType;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000CA58 File Offset: 0x0000AC58
		~MemoryXmlaStream()
		{
			this.InternalDispose(false);
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000CA88 File Offset: 0x0000AC88
		public override long Length
		{
			get
			{
				return this.baseStream.Length;
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000CA95 File Offset: 0x0000AC95
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.baseStream.Seek(offset, origin);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000CAA4 File Offset: 0x0000ACA4
		public override XmlaDataType GetRequestDataType()
		{
			return this.streamDataType;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000CAAC File Offset: 0x0000ACAC
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.baseStream.Write(buffer, offset, count);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000CABC File Offset: 0x0000ACBC
		public override void WriteEndOfMessage()
		{
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000CABE File Offset: 0x0000ACBE
		public override XmlaDataType GetResponseDataType()
		{
			return this.streamDataType;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000CAC6 File Offset: 0x0000ACC6
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.baseStream.Read(buffer, offset, count);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000CAD6 File Offset: 0x0000ACD6
		public override void Flush()
		{
			this.baseStream.Flush();
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000CAE3 File Offset: 0x0000ACE3
		public override void Skip()
		{
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000CAE5 File Offset: 0x0000ACE5
		public override void Dispose()
		{
			this.InternalDispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000CAF4 File Offset: 0x0000ACF4
		private void InternalDispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				try
				{
					this.baseStream.Dispose();
				}
				catch
				{
				}
			}
			this.disposed = true;
		}

		// Token: 0x040001FC RID: 508
		private MemoryStream baseStream;

		// Token: 0x040001FD RID: 509
		private XmlaDataType streamDataType;
	}
}
