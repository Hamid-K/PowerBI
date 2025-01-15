using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x0200000D RID: 13
	internal sealed class ODataNotificationReader : TextReader
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00002ED9 File Offset: 0x000010D9
		internal ODataNotificationReader(TextReader textReader, IODataStreamListener listener)
		{
			this.textReader = textReader;
			this.listener = listener;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002EEF File Offset: 0x000010EF
		public override int GetHashCode()
		{
			return this.textReader.GetHashCode();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002EFC File Offset: 0x000010FC
		public override string ToString()
		{
			return this.textReader.ToString();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002F09 File Offset: 0x00001109
		public override int Peek()
		{
			return this.textReader.Peek();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002F16 File Offset: 0x00001116
		public override int Read()
		{
			return this.textReader.Read();
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002F23 File Offset: 0x00001123
		public override int Read(char[] buffer, int index, int count)
		{
			return this.textReader.Read(buffer, index, count);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002F33 File Offset: 0x00001133
		public override int ReadBlock(char[] buffer, int index, int count)
		{
			return this.textReader.ReadBlock(buffer, index, count);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002F43 File Offset: 0x00001143
		public override string ReadLine()
		{
			return this.textReader.ReadLine();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002F50 File Offset: 0x00001150
		public override string ReadToEnd()
		{
			return this.textReader.ReadToEnd();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002F5D File Offset: 0x0000115D
		public override Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			return this.textReader.ReadAsync(buffer, index, count);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002F6D File Offset: 0x0000116D
		public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
		{
			return this.ReadBlockAsync(buffer, index, count);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002F78 File Offset: 0x00001178
		public override Task<string> ReadLineAsync()
		{
			return this.textReader.ReadLineAsync();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002F85 File Offset: 0x00001185
		public override Task<string> ReadToEndAsync()
		{
			return this.textReader.ReadToEndAsync();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002F92 File Offset: 0x00001192
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.listener != null)
			{
				this.listener.StreamDisposed();
				this.listener = null;
			}
			this.textReader.Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x0400001F RID: 31
		private readonly TextReader textReader;

		// Token: 0x04000020 RID: 32
		private IODataStreamListener listener;
	}
}
