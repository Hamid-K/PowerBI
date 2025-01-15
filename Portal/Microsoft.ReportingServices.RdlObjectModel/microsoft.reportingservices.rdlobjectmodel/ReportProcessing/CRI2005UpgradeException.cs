using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200007A RID: 122
	[Serializable]
	internal sealed class CRI2005UpgradeException : Exception
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x000170E4 File Offset: 0x000152E4
		public CRI2005UpgradeException(string msg)
			: base(msg)
		{
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000170ED File Offset: 0x000152ED
		public CRI2005UpgradeException()
			: base("2005 CRI needs to be processed by Yukon Engine")
		{
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000170FA File Offset: 0x000152FA
		public CRI2005UpgradeException(string msg, Exception inner)
			: base(msg, inner)
		{
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00017104 File Offset: 0x00015304
		private CRI2005UpgradeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
