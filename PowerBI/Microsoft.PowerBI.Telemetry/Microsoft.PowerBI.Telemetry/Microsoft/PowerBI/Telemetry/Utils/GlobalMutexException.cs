using System;

namespace Microsoft.PowerBI.Telemetry.Utils
{
	// Token: 0x0200003E RID: 62
	public class GlobalMutexException : Exception
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00005D49 File Offset: 0x00003F49
		public GlobalMutexException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
