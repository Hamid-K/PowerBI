using System;
using System.Diagnostics;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A5 RID: 421
	[DebuggerDisplay("{Message}")]
	public sealed class UriLiteralParsingException : ODataException
	{
		// Token: 0x060010F5 RID: 4341 RVA: 0x0002F3DA File Offset: 0x0002D5DA
		public UriLiteralParsingException()
		{
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0002F3E2 File Offset: 0x0002D5E2
		public UriLiteralParsingException(string message)
			: base(message)
		{
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00009008 File Offset: 0x00007208
		public UriLiteralParsingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
