using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200000D RID: 13
	[Serializable]
	internal sealed class MissingRequiredPropertyForItemTypeException : ReportCatalogException
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00003BBC File Offset: 0x00001DBC
		public MissingRequiredPropertyForItemTypeException(string propertyName)
			: base(ErrorCode.rsMissingRequiredPropertyForItemType, ErrorStringsWrapper.rsMissingRequiredPropertyForItemType(propertyName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00003BD6 File Offset: 0x00001DD6
		private MissingRequiredPropertyForItemTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
