using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Microsoft.OData.Client
{
	// Token: 0x020000C5 RID: 197
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class DataServiceRequestException : InvalidOperationException
	{
		// Token: 0x0600066A RID: 1642 RVA: 0x0001BE7E File Offset: 0x0001A07E
		public DataServiceRequestException()
			: base(Strings.DataServiceException_GeneralError)
		{
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001BE8B File Offset: 0x0001A08B
		public DataServiceRequestException(string message)
			: base(message)
		{
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001BE94 File Offset: 0x0001A094
		public DataServiceRequestException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001BF74 File Offset: 0x0001A174
		public DataServiceRequestException(string message, Exception innerException, DataServiceResponse response)
			: base(message, innerException)
		{
			this.response = response;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001BEAF File Offset: 0x0001A0AF
		[SuppressMessage("Microsoft.Design", "CA1047", Justification = "Follows serialization info pattern.")]
		[SuppressMessage("Microsoft.Design", "CA1032", Justification = "Follows serialization info pattern.")]
		protected DataServiceRequestException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0001BF85 File Offset: 0x0001A185
		public DataServiceResponse Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x040002D7 RID: 727
		[NonSerialized]
		private readonly DataServiceResponse response;
	}
}
