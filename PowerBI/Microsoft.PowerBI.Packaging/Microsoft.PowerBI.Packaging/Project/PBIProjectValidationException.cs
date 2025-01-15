using System;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x0200006F RID: 111
	[Serializable]
	public class PBIProjectValidationException : PBIProjectException
	{
		// Token: 0x0600030A RID: 778 RVA: 0x000089AB File Offset: 0x00006BAB
		public PBIProjectValidationException(string message, string artifactName, PBIProjectException.PBIProjectErrorCode errorCode)
			: base(message, PBIProjectValidationException.GetAnonymizedErrorDescription(artifactName), errorCode, null, null)
		{
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000089BD File Offset: 0x00006BBD
		private static string GetAnonymizedErrorDescription(string artifactName)
		{
			return "ArtifactName: " + artifactName;
		}
	}
}
