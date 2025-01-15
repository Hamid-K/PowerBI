using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.StaticFiles
{
	// Token: 0x02000007 RID: 7
	internal static class Constants
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002248 File Offset: 0x00000448
		private static Task CreateCompletedTask()
		{
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			tcs.SetResult(null);
			return tcs.Task;
		}

		// Token: 0x04000001 RID: 1
		internal const string ServerCapabilitiesKey = "server.Capabilities";

		// Token: 0x04000002 RID: 2
		internal const string SendFileVersionKey = "sendfile.Version";

		// Token: 0x04000003 RID: 3
		internal const string SendFileVersion = "1.0";

		// Token: 0x04000004 RID: 4
		internal const string SendFileAsyncKey = "sendfile.SendAsync";

		// Token: 0x04000005 RID: 5
		internal const string Location = "Location";

		// Token: 0x04000006 RID: 6
		internal const string IfMatch = "If-Match";

		// Token: 0x04000007 RID: 7
		internal const string IfNoneMatch = "If-None-Match";

		// Token: 0x04000008 RID: 8
		internal const string IfModifiedSince = "If-Modified-Since";

		// Token: 0x04000009 RID: 9
		internal const string IfUnmodifiedSince = "If-Unmodified-Since";

		// Token: 0x0400000A RID: 10
		internal const string IfRange = "If-Range";

		// Token: 0x0400000B RID: 11
		internal const string Range = "Range";

		// Token: 0x0400000C RID: 12
		internal const string ContentRange = "Content-Range";

		// Token: 0x0400000D RID: 13
		internal const string LastModified = "Last-Modified";

		// Token: 0x0400000E RID: 14
		internal const string HttpDateFormat = "r";

		// Token: 0x0400000F RID: 15
		internal const string TextHtmlUtf8 = "text/html; charset=utf-8";

		// Token: 0x04000010 RID: 16
		internal const int Status200Ok = 200;

		// Token: 0x04000011 RID: 17
		internal const int Status206PartialContent = 206;

		// Token: 0x04000012 RID: 18
		internal const int Status304NotModified = 304;

		// Token: 0x04000013 RID: 19
		internal const int Status412PreconditionFailed = 412;

		// Token: 0x04000014 RID: 20
		internal const int Status416RangeNotSatisfiable = 416;

		// Token: 0x04000015 RID: 21
		internal static readonly Task CompletedTask = Constants.CreateCompletedTask();
	}
}
