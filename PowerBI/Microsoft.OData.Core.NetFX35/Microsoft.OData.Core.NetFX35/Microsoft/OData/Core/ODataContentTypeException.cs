using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Microsoft.OData.Core
{
	// Token: 0x0200015A RID: 346
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public class ODataContentTypeException : ODataException
	{
		// Token: 0x06000CF1 RID: 3313 RVA: 0x00030608 File Offset: 0x0002E808
		public ODataContentTypeException()
			: this(Strings.ODataException_GeneralError)
		{
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00030615 File Offset: 0x0002E815
		public ODataContentTypeException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0003061F File Offset: 0x0002E81F
		public ODataContentTypeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00030629 File Offset: 0x0002E829
		[SuppressMessage("Microsoft.Design", "CA1032", Justification = "Follows serialization info pattern.")]
		[SuppressMessage("Microsoft.Design", "CA1047", Justification = "Follows serialization info pattern.")]
		protected ODataContentTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
