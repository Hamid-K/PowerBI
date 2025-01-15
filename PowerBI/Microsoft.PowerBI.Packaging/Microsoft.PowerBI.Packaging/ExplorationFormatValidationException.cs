using System;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	public class ExplorationFormatValidationException : ExplorationFormatException
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00002BDD File Offset: 0x00000DDD
		public ExplorationFormatValidationException(string message, ExplorationFormatErrorCode errorCode, ExplorationErrorSource errorSource)
			: base(message, errorCode, errorSource)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public ExplorationFormatValidationException(string message, Exception innerException, ExplorationFormatErrorCode errorCode, ExplorationErrorSource errorSource)
			: base(message, innerException, errorCode, errorSource)
		{
		}
	}
}
