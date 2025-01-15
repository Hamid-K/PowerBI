using System;
using System.Diagnostics;

namespace Microsoft.OData.Core.UriParser.Parsers.Common
{
	// Token: 0x020002BC RID: 700
	[DebuggerDisplay("{Message}")]
	public sealed class UriLiteralParsingException : ODataException
	{
		// Token: 0x06001829 RID: 6185 RVA: 0x000523E3 File Offset: 0x000505E3
		public UriLiteralParsingException()
		{
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x000523EB File Offset: 0x000505EB
		public UriLiteralParsingException(string message)
			: base(message)
		{
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x000523F4 File Offset: 0x000505F4
		public UriLiteralParsingException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
