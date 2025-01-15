using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	internal sealed class WrongItemTypeException : ReportCatalogException
	{
		// Token: 0x06000172 RID: 370 RVA: 0x00003F58 File Offset: 0x00002158
		public WrongItemTypeException(string itemPathOrType)
			: base(ErrorCode.rsWrongItemType, ErrorStringsWrapper.rsWrongItemType(itemPathOrType), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00003F6F File Offset: 0x0000216F
		private WrongItemTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
