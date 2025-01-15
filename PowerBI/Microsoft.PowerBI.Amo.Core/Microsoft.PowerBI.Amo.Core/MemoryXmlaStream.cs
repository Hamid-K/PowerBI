using System;
using System.IO;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000048 RID: 72
	internal sealed class MemoryXmlaStream : XmlaStream
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0000FC90 File Offset: 0x0000DE90
		internal MemoryXmlaStream(XmlaDataType streamDataType)
			: base(false)
		{
			this.baseStream = new MemoryStream();
			this.streamDataType = streamDataType;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000FCAC File Offset: 0x0000DEAC
		~MemoryXmlaStream()
		{
			this.InternalDispose(false);
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000FCDC File Offset: 0x0000DEDC
		public override long Length
		{
			get
			{
				return this.baseStream.Length;
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000FCE9 File Offset: 0x0000DEE9
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.baseStream.Seek(offset, origin);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000FCF8 File Offset: 0x0000DEF8
		public override XmlaDataType GetRequestDataType()
		{
			return this.streamDataType;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000FD00 File Offset: 0x0000DF00
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.baseStream.Write(buffer, offset, count);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000FD10 File Offset: 0x0000DF10
		public override void WriteEndOfMessage()
		{
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000FD12 File Offset: 0x0000DF12
		public override XmlaDataType GetResponseDataType()
		{
			return this.streamDataType;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000FD1A File Offset: 0x0000DF1A
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.baseStream.Read(buffer, offset, count);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000FD2A File Offset: 0x0000DF2A
		public override void Flush()
		{
			this.baseStream.Flush();
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000FD37 File Offset: 0x0000DF37
		public override void Skip()
		{
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000FD39 File Offset: 0x0000DF39
		public override void Dispose()
		{
			this.InternalDispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000FD48 File Offset: 0x0000DF48
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

		// Token: 0x0400024E RID: 590
		private MemoryStream baseStream;

		// Token: 0x0400024F RID: 591
		private XmlaDataType streamDataType;
	}
}
