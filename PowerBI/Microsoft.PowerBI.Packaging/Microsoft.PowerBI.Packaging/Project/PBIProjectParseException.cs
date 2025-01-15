using System;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000070 RID: 112
	[Serializable]
	public class PBIProjectParseException : PBIProjectException
	{
		// Token: 0x0600030C RID: 780 RVA: 0x000089CA File Offset: 0x00006BCA
		public PBIProjectParseException(string message, PBIProjectException.PBIProjectErrorCode errorCode)
			: base(message, errorCode, null, null)
		{
		}
	}
}
