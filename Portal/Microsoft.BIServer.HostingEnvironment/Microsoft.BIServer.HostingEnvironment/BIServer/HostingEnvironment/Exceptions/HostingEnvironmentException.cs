using System;

namespace Microsoft.BIServer.HostingEnvironment.Exceptions
{
	// Token: 0x02000030 RID: 48
	public class HostingEnvironmentException : Exception
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00004B91 File Offset: 0x00002D91
		public HostingEnvironmentException()
		{
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000050FA File Offset: 0x000032FA
		public HostingEnvironmentException(string message, params object[] parametersObjects)
			: base(string.Format(message, parametersObjects))
		{
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005109 File Offset: 0x00003309
		public HostingEnvironmentException(Exception innerException, string message, params object[] parametersObjects)
			: base(string.Format(message, parametersObjects), innerException)
		{
		}
	}
}
