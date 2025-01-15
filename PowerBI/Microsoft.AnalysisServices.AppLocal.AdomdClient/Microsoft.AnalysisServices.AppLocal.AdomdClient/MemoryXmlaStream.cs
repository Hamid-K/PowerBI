using System;
using System.IO;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200002F RID: 47
	internal sealed class MemoryXmlaStream : XmlaStream
	{
		// Token: 0x060002AD RID: 685 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		internal MemoryXmlaStream(XmlaDataType streamDataType)
			: base(false)
		{
			this.baseStream = new MemoryStream();
			this.streamDataType = streamDataType;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000CD88 File Offset: 0x0000AF88
		~MemoryXmlaStream()
		{
			this.InternalDispose(false);
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000CDB8 File Offset: 0x0000AFB8
		public override long Length
		{
			get
			{
				return this.baseStream.Length;
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000CDC5 File Offset: 0x0000AFC5
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.baseStream.Seek(offset, origin);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000CDD4 File Offset: 0x0000AFD4
		public override XmlaDataType GetRequestDataType()
		{
			return this.streamDataType;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000CDDC File Offset: 0x0000AFDC
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.baseStream.Write(buffer, offset, count);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000CDEC File Offset: 0x0000AFEC
		public override void WriteEndOfMessage()
		{
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000CDEE File Offset: 0x0000AFEE
		public override XmlaDataType GetResponseDataType()
		{
			return this.streamDataType;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000CDF6 File Offset: 0x0000AFF6
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.baseStream.Read(buffer, offset, count);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000CE06 File Offset: 0x0000B006
		public override void Flush()
		{
			this.baseStream.Flush();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000CE13 File Offset: 0x0000B013
		public override void Skip()
		{
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000CE15 File Offset: 0x0000B015
		public override void Dispose()
		{
			this.InternalDispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000CE24 File Offset: 0x0000B024
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

		// Token: 0x04000209 RID: 521
		private MemoryStream baseStream;

		// Token: 0x0400020A RID: 522
		private XmlaDataType streamDataType;
	}
}
