using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200004C RID: 76
	[Serializable]
	public sealed class InvalidDataSourceCredentialSettingException : ReportCatalogException
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x0000448E File Offset: 0x0000268E
		public InvalidDataSourceCredentialSettingException()
			: base(ErrorCode.rsInvalidDataSourceCredentialSetting, ErrorStringsWrapper.rsInvalidDataSourceCredentialSetting, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000044A4 File Offset: 0x000026A4
		private InvalidDataSourceCredentialSettingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
