using System;
using System.Globalization;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x0200006D RID: 109
	[Serializable]
	public class PBIProjectWriteException : PBIProjectException
	{
		// Token: 0x06000303 RID: 771 RVA: 0x000088FC File Offset: 0x00006AFC
		public PBIProjectWriteException(string path, string issueMessage, PBIProjectException.PBIProjectErrorCode errorCode)
			: base(("Cannot write '" + path + "'. " + issueMessage).ToString(CultureInfo.CurrentCulture), PBIProjectException.GetErrorWithPath(path), errorCode, null, null)
		{
		}
	}
}
