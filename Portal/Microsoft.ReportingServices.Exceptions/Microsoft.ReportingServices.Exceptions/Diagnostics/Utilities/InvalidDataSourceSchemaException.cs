using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200004F RID: 79
	[Serializable]
	internal sealed class InvalidDataSourceSchemaException : ReportCatalogException
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x000044F1 File Offset: 0x000026F1
		public InvalidDataSourceSchemaException()
			: base(ErrorCode.rsInvalidRSDSSchema, ErrorStringsWrapper.rsInvalidRSDSSchema, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000450A File Offset: 0x0000270A
		private InvalidDataSourceSchemaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
