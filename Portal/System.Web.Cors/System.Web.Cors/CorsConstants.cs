using System;

namespace System.Web.Cors
{
	// Token: 0x02000004 RID: 4
	public static class CorsConstants
	{
		// Token: 0x0400000A RID: 10
		public static readonly string PreflightHttpMethod = "OPTIONS";

		// Token: 0x0400000B RID: 11
		public static readonly string Origin = "Origin";

		// Token: 0x0400000C RID: 12
		public static readonly string AnyOrigin = "*";

		// Token: 0x0400000D RID: 13
		public static readonly string AccessControlRequestMethod = "Access-Control-Request-Method";

		// Token: 0x0400000E RID: 14
		public static readonly string AccessControlRequestHeaders = "Access-Control-Request-Headers";

		// Token: 0x0400000F RID: 15
		public static readonly string AccessControlAllowOrigin = "Access-Control-Allow-Origin";

		// Token: 0x04000010 RID: 16
		public static readonly string AccessControlAllowHeaders = "Access-Control-Allow-Headers";

		// Token: 0x04000011 RID: 17
		public static readonly string AccessControlExposeHeaders = "Access-Control-Expose-Headers";

		// Token: 0x04000012 RID: 18
		public static readonly string AccessControlAllowMethods = "Access-Control-Allow-Methods";

		// Token: 0x04000013 RID: 19
		public static readonly string AccessControlAllowCredentials = "Access-Control-Allow-Credentials";

		// Token: 0x04000014 RID: 20
		public static readonly string AccessControlMaxAge = "Access-Control-Max-Age";

		// Token: 0x04000015 RID: 21
		internal static readonly string[] SimpleRequestHeaders = new string[] { "Origin", "Accept", "Accept-Language", "Content-Language" };

		// Token: 0x04000016 RID: 22
		internal static readonly string[] SimpleResponseHeaders = new string[] { "Cache-Control", "Content-Language", "Content-Type", "Expires", "Last-Modified", "Pragma" };

		// Token: 0x04000017 RID: 23
		internal static readonly string[] SimpleMethods = new string[] { "GET", "HEAD", "POST" };
	}
}
