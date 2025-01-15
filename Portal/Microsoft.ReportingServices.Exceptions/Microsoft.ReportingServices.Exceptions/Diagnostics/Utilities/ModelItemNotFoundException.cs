using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200007A RID: 122
	[Serializable]
	internal sealed class ModelItemNotFoundException : ReportCatalogException
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00004B5D File Offset: 0x00002D5D
		public ModelItemNotFoundException(string modelPath, string modelItemID)
			: base(ErrorCode.rsModelItemNotFound, ErrorStringsWrapper.rsModelItemNotFound(modelPath, modelItemID), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00004B75 File Offset: 0x00002D75
		private ModelItemNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
