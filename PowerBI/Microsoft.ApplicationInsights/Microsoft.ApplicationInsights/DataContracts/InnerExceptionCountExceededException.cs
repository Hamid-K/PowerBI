using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000D1 RID: 209
	[SuppressMessage("Microsoft.Design", "CA1064:ExceptionsShouldBePublic", Justification = "We expect that this exception will be caught within the internal scope and should never be exposed to an end user.")]
	[Serializable]
	internal class InnerExceptionCountExceededException : Exception
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x0001908D File Offset: 0x0001728D
		public InnerExceptionCountExceededException()
		{
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00019095 File Offset: 0x00017295
		public InnerExceptionCountExceededException(string message)
			: base(message)
		{
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001909E File Offset: 0x0001729E
		public InnerExceptionCountExceededException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x000190A8 File Offset: 0x000172A8
		protected InnerExceptionCountExceededException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
