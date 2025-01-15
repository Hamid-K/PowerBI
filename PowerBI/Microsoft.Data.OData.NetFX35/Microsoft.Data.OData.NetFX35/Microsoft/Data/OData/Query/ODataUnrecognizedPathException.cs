using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000050 RID: 80
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class ODataUnrecognizedPathException : ODataException
	{
		// Token: 0x0600021B RID: 539 RVA: 0x000081C0 File Offset: 0x000063C0
		public ODataUnrecognizedPathException()
			: this(Strings.ODataUriParserException_GeneralError, null)
		{
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000081CE File Offset: 0x000063CE
		public ODataUnrecognizedPathException(string message)
			: this(message, null)
		{
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000081D8 File Offset: 0x000063D8
		public ODataUnrecognizedPathException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000081E2 File Offset: 0x000063E2
		private ODataUnrecognizedPathException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
