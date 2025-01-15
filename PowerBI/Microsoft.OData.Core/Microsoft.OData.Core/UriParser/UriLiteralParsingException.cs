using System;
using System.Diagnostics;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200011B RID: 283
	[DebuggerDisplay("{Message}")]
	public sealed class UriLiteralParsingException : ODataException
	{
		// Token: 0x06000F94 RID: 3988 RVA: 0x00026BD6 File Offset: 0x00024DD6
		public UriLiteralParsingException()
		{
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00026BDE File Offset: 0x00024DDE
		public UriLiteralParsingException(string message)
			: base(message)
		{
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public UriLiteralParsingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
