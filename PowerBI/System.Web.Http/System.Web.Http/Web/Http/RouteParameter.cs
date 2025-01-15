using System;

namespace System.Web.Http
{
	// Token: 0x02000030 RID: 48
	public sealed class RouteParameter
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00003AA7 File Offset: 0x00001CA7
		private RouteParameter()
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004CCE File Offset: 0x00002ECE
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x04000044 RID: 68
		public static readonly RouteParameter Optional = new RouteParameter();
	}
}
