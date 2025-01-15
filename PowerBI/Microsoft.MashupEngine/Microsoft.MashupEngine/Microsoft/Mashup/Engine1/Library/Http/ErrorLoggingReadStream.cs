using System;
using System.IO;
using System.Net;
using Microsoft.Internal;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A4E RID: 2638
	internal class ErrorLoggingReadStream : ForwardReadOnlyStream
	{
		// Token: 0x060049B2 RID: 18866 RVA: 0x000F5B59 File Offset: 0x000F3D59
		public ErrorLoggingReadStream(Stream stream, Action<Exception, long> errorLogger)
		{
			this.stream = stream;
			this.errorLogger = errorLogger;
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x000F5B70 File Offset: 0x000F3D70
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num2;
			try
			{
				int num = this.stream.Read(buffer, offset, count);
				this.position += (long)num;
				num2 = num;
			}
			catch (IOException ex)
			{
				throw this.LogError(ex);
			}
			catch (WebException ex2)
			{
				throw this.LogError(ex2);
			}
			return num2;
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x000F5BD0 File Offset: 0x000F3DD0
		public override int ReadByte()
		{
			int num2;
			try
			{
				int num = this.stream.ReadByte();
				if (num >= 0)
				{
					this.position += 1L;
				}
				num2 = num;
			}
			catch (IOException ex)
			{
				throw this.LogError(ex);
			}
			catch (WebException ex2)
			{
				throw this.LogError(ex2);
			}
			return num2;
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x000F5C30 File Offset: 0x000F3E30
		public override void Close()
		{
			try
			{
				this.stream.Close();
			}
			catch (IOException ex)
			{
				throw this.LogError(ex);
			}
			catch (WebException ex2)
			{
				throw this.LogError(ex2);
			}
		}

		// Token: 0x060049B6 RID: 18870 RVA: 0x000F5C78 File Offset: 0x000F3E78
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.stream != null)
			{
				this.stream.Dispose();
				this.stream = null;
			}
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x000F5C97 File Offset: 0x000F3E97
		private ValueException LogError(Exception exception)
		{
			if (this.errorLogger != null)
			{
				this.errorLogger(exception, this.position);
			}
			return ValueException.NewDataSourceError(exception.Message, Value.Null, exception);
		}

		// Token: 0x04002747 RID: 10055
		private readonly Action<Exception, long> errorLogger;

		// Token: 0x04002748 RID: 10056
		private Stream stream;

		// Token: 0x04002749 RID: 10057
		private long position;
	}
}
