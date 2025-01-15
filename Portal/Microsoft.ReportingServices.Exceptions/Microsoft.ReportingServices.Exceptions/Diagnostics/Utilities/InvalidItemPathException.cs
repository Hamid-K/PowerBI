using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	internal sealed class InvalidItemPathException : ReportCatalogException
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00003DCD File Offset: 0x00001FCD
		public InvalidItemPathException(string invalidPath, string parameterName)
			: this(invalidPath, parameterName, null)
		{
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public InvalidItemPathException(string invalidPath)
			: this(invalidPath, null, null)
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00003DE3 File Offset: 0x00001FE3
		public InvalidItemPathException(string invalidPath, string parameterName, Exception innerException)
			: base(ErrorCode.rsInvalidItemPath, ErrorStringsWrapper.rsInvalidItemPath(StringUtils.RemoveControlAndNonSpacesWhitespaceCharacters(invalidPath), CatalogItemNames.MaxItemPathLength), innerException, null, null)
		{
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00003E00 File Offset: 0x00002000
		private InvalidItemPathException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
