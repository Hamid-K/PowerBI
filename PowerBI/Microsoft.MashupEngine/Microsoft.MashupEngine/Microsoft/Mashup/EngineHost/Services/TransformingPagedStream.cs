using System;
using System.IO;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B4A RID: 6986
	internal sealed class TransformingPagedStream : PagedStream
	{
		// Token: 0x0600AEC6 RID: 44742 RVA: 0x0023C86F File Offset: 0x0023AA6F
		public TransformingPagedStream(Stream inputStream, int inputPageSize, int outputPageSize, Func<byte[], int, byte[], int> inputToOutput, Func<byte[], int, byte[], int> outputToInput)
			: base(outputPageSize)
		{
			this.inputStream = inputStream;
			this.inputPageSize = inputPageSize;
			this.inputToOutput = inputToOutput;
			this.outputToInput = outputToInput;
			this.workingBuffer = new byte[Math.Max(inputPageSize, outputPageSize)];
			base.SeekToPage(0);
		}

		// Token: 0x17002BD5 RID: 11221
		// (get) Token: 0x0600AEC7 RID: 44743 RVA: 0x0023C8AF File Offset: 0x0023AAAF
		public Stream InputStream
		{
			get
			{
				return this.inputStream;
			}
		}

		// Token: 0x0600AEC8 RID: 44744 RVA: 0x0023C8B7 File Offset: 0x0023AAB7
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Flush();
				if (this.inputStream != null)
				{
					this.inputStream.Dispose();
					this.inputStream = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x17002BD6 RID: 11222
		// (get) Token: 0x0600AEC9 RID: 44745 RVA: 0x0023C8E3 File Offset: 0x0023AAE3
		public override bool CanRead
		{
			get
			{
				return this.inputStream.CanRead;
			}
		}

		// Token: 0x17002BD7 RID: 11223
		// (get) Token: 0x0600AECA RID: 44746 RVA: 0x0023C8F0 File Offset: 0x0023AAF0
		public override bool CanSeek
		{
			get
			{
				return this.inputStream.CanSeek;
			}
		}

		// Token: 0x17002BD8 RID: 11224
		// (get) Token: 0x0600AECB RID: 44747 RVA: 0x0023C8FD File Offset: 0x0023AAFD
		public override bool CanWrite
		{
			get
			{
				return this.inputStream.CanWrite;
			}
		}

		// Token: 0x17002BD9 RID: 11225
		// (get) Token: 0x0600AECC RID: 44748 RVA: 0x0023C90C File Offset: 0x0023AB0C
		// (set) Token: 0x0600AECD RID: 44749 RVA: 0x0023C93E File Offset: 0x0023AB3E
		protected override int PageCount
		{
			get
			{
				long length = this.inputStream.Length;
				int num = (int)(length / (long)this.inputPageSize);
				if (length % (long)this.inputPageSize != 0L)
				{
					num++;
				}
				return num;
			}
			set
			{
				this.inputStream.SetLength((long)value * (long)this.inputPageSize);
			}
		}

		// Token: 0x0600AECE RID: 44750 RVA: 0x0023C958 File Offset: 0x0023AB58
		protected override int ReadPage(int page, byte[] buffer)
		{
			this.inputStream.Seek((long)page * (long)this.inputPageSize, SeekOrigin.Begin);
			int num = this.inputStream.Read(this.workingBuffer, 0, this.inputPageSize);
			if (num == 0)
			{
				return 0;
			}
			return this.inputToOutput(this.workingBuffer, num, buffer);
		}

		// Token: 0x0600AECF RID: 44751 RVA: 0x0023C9B0 File Offset: 0x0023ABB0
		protected override void WritePage(int page, byte[] buffer, int length)
		{
			this.inputStream.Seek((long)page * (long)this.inputPageSize, SeekOrigin.Begin);
			int num = this.outputToInput(buffer, length, this.workingBuffer);
			this.inputStream.Write(this.workingBuffer, 0, num);
			if (page == this.PageCount - 1 && this.inputStream.Position < this.inputStream.Length)
			{
				this.inputStream.SetLength(this.inputStream.Position);
			}
		}

		// Token: 0x04005A1C RID: 23068
		private Stream inputStream;

		// Token: 0x04005A1D RID: 23069
		private int inputPageSize;

		// Token: 0x04005A1E RID: 23070
		private Func<byte[], int, byte[], int> inputToOutput;

		// Token: 0x04005A1F RID: 23071
		private Func<byte[], int, byte[], int> outputToInput;

		// Token: 0x04005A20 RID: 23072
		private byte[] workingBuffer;
	}
}
