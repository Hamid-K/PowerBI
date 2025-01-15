using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Microsoft.OData.Core
{
	// Token: 0x02000159 RID: 345
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public class ODataException : InvalidOperationException
	{
		// Token: 0x06000CED RID: 3309 RVA: 0x000305DD File Offset: 0x0002E7DD
		public ODataException()
			: this(Strings.ODataException_GeneralError)
		{
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x000305EA File Offset: 0x0002E7EA
		public ODataException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x000305F4 File Offset: 0x0002E7F4
		public ODataException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x000305FE File Offset: 0x0002E7FE
		[SuppressMessage("Microsoft.Design", "CA1032", Justification = "Follows serialization info pattern.")]
		[SuppressMessage("Microsoft.Design", "CA1047", Justification = "Follows serialization info pattern.")]
		protected ODataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
