using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x0200005F RID: 95
	[NullableContext(1)]
	[Nullable(0)]
	public class ResponseClassifier
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00009BF9 File Offset: 0x00007DF9
		internal static ResponseClassifier Shared { get; } = new ResponseClassifier();

		// Token: 0x06000346 RID: 838 RVA: 0x00009C00 File Offset: 0x00007E00
		public virtual bool IsRetriableResponse(HttpMessage message)
		{
			int status = message.Response.Status;
			if (status <= 429)
			{
				if (status != 408 && status != 429)
				{
					return false;
				}
			}
			else if (status != 500 && status - 502 > 2)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00009C48 File Offset: 0x00007E48
		public virtual bool IsRetriableException(Exception exception)
		{
			if (!(exception is IOException))
			{
				RequestFailedException ex = exception as RequestFailedException;
				return ex != null && ex.Status == 0;
			}
			return true;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00009C74 File Offset: 0x00007E74
		public virtual bool IsRetriable(HttpMessage message, Exception exception)
		{
			return this.IsRetriableException(exception) || (exception is OperationCanceledException && !message.CancellationToken.IsCancellationRequested);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00009CA8 File Offset: 0x00007EA8
		public virtual bool IsErrorResponse(HttpMessage message)
		{
			int num = message.Response.Status / 100;
			return num == 4 || num == 5;
		}
	}
}
