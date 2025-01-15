using System;
using System.Diagnostics;

namespace Microsoft.OData
{
	// Token: 0x02000083 RID: 131
	[DebuggerDisplay("{Message}")]
	public class ODataException : InvalidOperationException
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x0000C018 File Offset: 0x0000A218
		public ODataException()
			: this(Strings.ODataException_GeneralError)
		{
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000C025 File Offset: 0x0000A225
		public ODataException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000C02F File Offset: 0x0000A22F
		public ODataException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
