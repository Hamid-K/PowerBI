using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Microsoft.OData
{
	// Token: 0x02000060 RID: 96
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public class ODataException : InvalidOperationException
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000A077 File Offset: 0x00008277
		public ODataException()
			: this(Strings.ODataException_GeneralError)
		{
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000A084 File Offset: 0x00008284
		public ODataException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000A08E File Offset: 0x0000828E
		public ODataException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000A098 File Offset: 0x00008298
		[SuppressMessage("Microsoft.Design", "CA1047", Justification = "Follows serialization info pattern.")]
		[SuppressMessage("Microsoft.Design", "CA1032", Justification = "Follows serialization info pattern.")]
		protected ODataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
