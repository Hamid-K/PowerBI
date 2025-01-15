using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Data.OData
{
	// Token: 0x020001E8 RID: 488
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public class ODataContentTypeException : ODataException
	{
		// Token: 0x06000E3B RID: 3643 RVA: 0x0003320D File Offset: 0x0003140D
		public ODataContentTypeException()
			: this(Strings.ODataException_GeneralError)
		{
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0003321A File Offset: 0x0003141A
		public ODataContentTypeException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00033224 File Offset: 0x00031424
		public ODataContentTypeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0003322E File Offset: 0x0003142E
		protected ODataContentTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
