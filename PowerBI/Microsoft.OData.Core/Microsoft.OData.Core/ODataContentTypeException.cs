using System;
using System.Diagnostics;

namespace Microsoft.OData
{
	// Token: 0x0200006A RID: 106
	[DebuggerDisplay("{Message}")]
	public class ODataContentTypeException : ODataException
	{
		// Token: 0x060003CC RID: 972 RVA: 0x0000AD35 File Offset: 0x00008F35
		public ODataContentTypeException()
			: this(Strings.ODataException_GeneralError)
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000AD42 File Offset: 0x00008F42
		public ODataContentTypeException(string message)
			: this(message, null)
		{
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public ODataContentTypeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
