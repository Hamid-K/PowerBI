using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.StaticFiles;

namespace Microsoft.Owin
{
	// Token: 0x02000006 RID: 6
	public static class SendFileResponseExtensions
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000021B6 File Offset: 0x000003B6
		public static bool SupportsSendFile(this IOwinResponse response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			return response.Get<Func<string, long, long?, CancellationToken, Task>>("sendfile.SendAsync") != null;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021D4 File Offset: 0x000003D4
		public static Task SendFileAsync(this IOwinResponse response, string fileName)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			return response.SendFileAsync(fileName, 0L, null, CancellationToken.None);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002208 File Offset: 0x00000408
		public static Task SendFileAsync(this IOwinResponse response, string fileName, long offset, long? count, CancellationToken cancellationToken)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			Func<string, long, long?, CancellationToken, Task> sendFileFunc = response.Get<Func<string, long, long?, CancellationToken, Task>>("sendfile.SendAsync");
			if (sendFileFunc == null)
			{
				throw new NotSupportedException(Resources.Exception_SendFileNotSupported);
			}
			return sendFileFunc(fileName, offset, count, cancellationToken);
		}
	}
}
