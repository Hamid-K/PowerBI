using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200004E RID: 78
	[Serializable]
	internal sealed class InvalidDataSourceCredentialSettingForITokenDataExtensionException : ReportCatalogException
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x000044CE File Offset: 0x000026CE
		public InvalidDataSourceCredentialSettingForITokenDataExtensionException()
			: base(ErrorCode.rsInvalidDataSourceCredentialSettingForITokenDataExtension, ErrorStringsWrapper.rsInvalidDataSourceCredentialSettingForITokenDataExtension, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000044E7 File Offset: 0x000026E7
		private InvalidDataSourceCredentialSettingForITokenDataExtensionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
