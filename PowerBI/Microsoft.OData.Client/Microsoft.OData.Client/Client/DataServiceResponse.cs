using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Client
{
	// Token: 0x020000D0 RID: 208
	[SuppressMessage("Microsoft.Design", "CA1010", Justification = "required for this feature")]
	[SuppressMessage("Microsoft.Naming", "CA1710", Justification = "required for this feature")]
	public sealed class DataServiceResponse : IEnumerable<OperationResponse>, IEnumerable
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x0001C96E File Offset: 0x0001AB6E
		internal DataServiceResponse(HeaderCollection headers, int statusCode, IEnumerable<OperationResponse> response, bool batchResponse)
		{
			this.headers = headers ?? new HeaderCollection();
			this.statusCode = statusCode;
			this.batchResponse = batchResponse;
			this.response = response;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001C99C File Offset: 0x0001AB9C
		public IDictionary<string, string> BatchHeaders
		{
			get
			{
				return this.headers.UnderlyingDictionary;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001C9A9 File Offset: 0x0001ABA9
		public int BatchStatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001C9B1 File Offset: 0x0001ABB1
		public bool IsBatchResponse
		{
			get
			{
				return this.batchResponse;
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001C9B9 File Offset: 0x0001ABB9
		public IEnumerator<OperationResponse> GetEnumerator()
		{
			return this.response.GetEnumerator();
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001C9C6 File Offset: 0x0001ABC6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002F0 RID: 752
		private readonly HeaderCollection headers;

		// Token: 0x040002F1 RID: 753
		private readonly int statusCode;

		// Token: 0x040002F2 RID: 754
		private readonly IEnumerable<OperationResponse> response;

		// Token: 0x040002F3 RID: 755
		private readonly bool batchResponse;
	}
}
