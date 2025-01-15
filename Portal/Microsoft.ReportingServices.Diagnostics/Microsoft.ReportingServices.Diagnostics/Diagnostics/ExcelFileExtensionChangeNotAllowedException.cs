using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	internal sealed class ExcelFileExtensionChangeNotAllowedException : ReportCatalogException
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002295 File Offset: 0x00000495
		public ExcelFileExtensionChangeNotAllowedException(string message)
			: base(ErrorCode.rsInvalidItemName, message, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000022A7 File Offset: 0x000004A7
		private ExcelFileExtensionChangeNotAllowedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
