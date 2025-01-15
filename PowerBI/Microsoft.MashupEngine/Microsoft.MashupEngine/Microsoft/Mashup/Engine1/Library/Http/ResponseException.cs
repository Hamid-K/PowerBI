using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A95 RID: 2709
	[Serializable]
	internal class ResponseException : Exception
	{
		// Token: 0x06004BE8 RID: 19432 RVA: 0x000FAE10 File Offset: 0x000F9010
		public ResponseException(Exception innerException)
			: this(innerException, innerException.Message)
		{
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x000FAE1F File Offset: 0x000F901F
		public ResponseException(Exception innerException, string message)
			: base(message, innerException)
		{
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x00005F45 File Offset: 0x00004145
		protected ResponseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
