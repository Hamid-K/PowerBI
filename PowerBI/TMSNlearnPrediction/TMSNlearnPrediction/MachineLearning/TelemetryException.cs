using System;

namespace Microsoft.MachineLearning
{
	// Token: 0x02000146 RID: 326
	public sealed class TelemetryException : TelemetryMessage
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x0002317D File Offset: 0x0002137D
		public TelemetryException(Exception exception)
		{
			this.Exception = exception;
		}

		// Token: 0x0400035F RID: 863
		public readonly Exception Exception;
	}
}
