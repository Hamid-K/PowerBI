using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Microsoft.OData.Client
{
	// Token: 0x020000C3 RID: 195
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public sealed class DataServiceQueryException : InvalidOperationException
	{
		// Token: 0x06000659 RID: 1625 RVA: 0x0001BE7E File Offset: 0x0001A07E
		public DataServiceQueryException()
			: base(Strings.DataServiceException_GeneralError)
		{
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001BE8B File Offset: 0x0001A08B
		public DataServiceQueryException(string message)
			: base(message)
		{
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001BE94 File Offset: 0x0001A094
		public DataServiceQueryException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001BE9E File Offset: 0x0001A09E
		public DataServiceQueryException(string message, Exception innerException, QueryOperationResponse response)
			: base(message, innerException)
		{
			this.response = response;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001BEAF File Offset: 0x0001A0AF
		[SuppressMessage("Microsoft.Design", "CA1047", Justification = "Follows serialization info pattern.")]
		[SuppressMessage("Microsoft.Design", "CA1032", Justification = "Follows serialization info pattern.")]
		protected DataServiceQueryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001BEB9 File Offset: 0x0001A0B9
		public QueryOperationResponse Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x040002D5 RID: 725
		[NonSerialized]
		private readonly QueryOperationResponse response;
	}
}
