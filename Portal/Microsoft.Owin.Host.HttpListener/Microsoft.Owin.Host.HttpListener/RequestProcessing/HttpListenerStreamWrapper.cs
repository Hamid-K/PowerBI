using System;
using System.IO;
using System.Net;

namespace Microsoft.Owin.Host.HttpListener.RequestProcessing
{
	// Token: 0x02000010 RID: 16
	internal class HttpListenerStreamWrapper : ExceptionFilterStream
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00005A49 File Offset: 0x00003C49
		internal HttpListenerStreamWrapper(Stream innerStream)
			: base(innerStream)
		{
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005A52 File Offset: 0x00003C52
		protected override bool TryWrapException(Exception ex, out Exception wrapped)
		{
			if (ex is HttpListenerException)
			{
				wrapped = new IOException(string.Empty, ex);
				return true;
			}
			wrapped = null;
			return false;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005A6F File Offset: 0x00003C6F
		public override void Close()
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005A71 File Offset: 0x00003C71
		protected override void Dispose(bool disposing)
		{
		}
	}
}
