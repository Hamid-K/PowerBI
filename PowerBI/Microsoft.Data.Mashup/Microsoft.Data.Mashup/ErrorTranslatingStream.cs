using System;
using System.IO;
using Microsoft.Mashup.Common;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000020 RID: 32
	internal class ErrorTranslatingStream : DelegatingStream
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00006960 File Offset: 0x00004B60
		public ErrorTranslatingStream(Stream stream, Func<Exception, Exception> translateException)
			: base(stream)
		{
			this.translateException = translateException;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006970 File Offset: 0x00004B70
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num;
			try
			{
				num = base.Read(buffer, offset, count);
			}
			catch (Exception ex)
			{
				this.HandleException(ex);
				throw;
			}
			return num;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000069A4 File Offset: 0x00004BA4
		public override int ReadByte()
		{
			int num;
			try
			{
				num = base.ReadByte();
			}
			catch (Exception ex)
			{
				this.HandleException(ex);
				throw;
			}
			return num;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000069D8 File Offset: 0x00004BD8
		public override void Close()
		{
			try
			{
				base.Close();
			}
			catch (Exception ex)
			{
				this.HandleException(ex);
				throw;
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006A08 File Offset: 0x00004C08
		protected override void Dispose(bool disposing)
		{
			try
			{
				base.Dispose(disposing);
			}
			catch (Exception ex)
			{
				this.HandleException(ex);
				throw;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006A38 File Offset: 0x00004C38
		private void HandleException(Exception e)
		{
			Exception ex = this.translateException(e);
			if (ex != e)
			{
				throw ex;
			}
		}

		// Token: 0x040000A9 RID: 169
		private readonly Func<Exception, Exception> translateException;
	}
}
