using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200009A RID: 154
	[Serializable]
	internal sealed class OperationNotSupportedNativeModeException : ReportCatalogException
	{
		// Token: 0x06000274 RID: 628 RVA: 0x000050D8 File Offset: 0x000032D8
		public OperationNotSupportedNativeModeException()
			: base(ErrorCode.rsOperationNotSupportedNativeMode, ErrorStringsWrapper.rsOperationNotSupportedNativeMode, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000050F1 File Offset: 0x000032F1
		private OperationNotSupportedNativeModeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
