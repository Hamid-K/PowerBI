using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Microsoft.OData
{
	// Token: 0x02000048 RID: 72
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public class ODataContentTypeException : ODataException
	{
		// Token: 0x06000252 RID: 594 RVA: 0x00008FF1 File Offset: 0x000071F1
		public ODataContentTypeException()
			: this(Strings.ODataException_GeneralError)
		{
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00008FFE File Offset: 0x000071FE
		public ODataContentTypeException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009008 File Offset: 0x00007208
		public ODataContentTypeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009012 File Offset: 0x00007212
		[SuppressMessage("Microsoft.Design", "CA1047", Justification = "Follows serialization info pattern.")]
		[SuppressMessage("Microsoft.Design", "CA1032", Justification = "Follows serialization info pattern.")]
		protected ODataContentTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
