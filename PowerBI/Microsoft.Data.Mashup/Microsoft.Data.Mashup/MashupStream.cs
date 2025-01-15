using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000041 RID: 65
	internal sealed class MashupStream : Stream
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0000CEC0 File Offset: 0x0000B0C0
		public MashupStream(MashupCommand command, IStreamSource streamSource)
		{
			this.command = command;
			this.streamSource = streamSource;
			try
			{
				this.internalStream = streamSource.Stream;
			}
			catch (Exception ex)
			{
				this.streamSource.Dispose();
				this.streamSource = null;
				this.command.TryCheckException(ex);
				throw;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000CF20 File Offset: 0x0000B120
		public override bool CanRead
		{
			get
			{
				return this.internalStream.CanRead;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000CF2D File Offset: 0x0000B12D
		public override bool CanSeek
		{
			get
			{
				return this.internalStream.CanSeek;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000CF3A File Offset: 0x0000B13A
		public override bool CanWrite
		{
			get
			{
				return this.internalStream.CanWrite;
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000CF47 File Offset: 0x0000B147
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && !this.isDisposed)
			{
				this.streamSource.Dispose();
				this.internalStream.Close();
				this.command.NotifyDataReaderClosing();
				this.isDisposed = true;
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000CF83 File Offset: 0x0000B183
		public override void Flush()
		{
			this.internalStream.Flush();
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000CF90 File Offset: 0x0000B190
		public override long Length
		{
			get
			{
				return this.internalStream.Length;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000CF9D File Offset: 0x0000B19D
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000CFAA File Offset: 0x0000B1AA
		public override long Position
		{
			get
			{
				return this.internalStream.Position;
			}
			set
			{
				this.internalStream.Position = value;
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000CFB8 File Offset: 0x0000B1B8
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num;
			try
			{
				num = this.internalStream.Read(buffer, offset, count);
			}
			catch (Exception ex)
			{
				this.command.TryCheckException(ex);
				throw;
			}
			return num;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000CFF8 File Offset: 0x0000B1F8
		public override int ReadByte()
		{
			int num;
			try
			{
				num = this.internalStream.ReadByte();
			}
			catch (Exception ex)
			{
				this.command.TryCheckException(ex);
				throw;
			}
			return num;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000D034 File Offset: 0x0000B234
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.internalStream.Seek(offset, origin);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000D043 File Offset: 0x0000B243
		public override void SetLength(long value)
		{
			this.internalStream.SetLength(value);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000D051 File Offset: 0x0000B251
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.internalStream.Write(buffer, offset, count);
		}

		// Token: 0x040001A6 RID: 422
		private readonly MashupCommand command;

		// Token: 0x040001A7 RID: 423
		private bool isDisposed;

		// Token: 0x040001A8 RID: 424
		private readonly IStreamSource streamSource;

		// Token: 0x040001A9 RID: 425
		private readonly Stream internalStream;
	}
}
