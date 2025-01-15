using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Batch
{
	// Token: 0x020001D1 RID: 465
	public class OperationResponseItem : ODataBatchResponseItem
	{
		// Token: 0x06000F57 RID: 3927 RVA: 0x0003EE90 File Offset: 0x0003D090
		public OperationResponseItem(HttpResponseMessage response)
		{
			if (response == null)
			{
				throw Error.ArgumentNull("response");
			}
			this.Response = response;
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0003EEAD File Offset: 0x0003D0AD
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x0003EEB5 File Offset: 0x0003D0B5
		public HttpResponseMessage Response { get; private set; }

		// Token: 0x06000F5A RID: 3930 RVA: 0x0003EEBE File Offset: 0x0003D0BE
		public override Task WriteResponseAsync(ODataBatchWriter writer, CancellationToken cancellationToken)
		{
			if (writer == null)
			{
				throw Error.ArgumentNull("writer");
			}
			return ODataBatchResponseItem.WriteMessageAsync(writer, this.Response, cancellationToken);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0003EEDB File Offset: 0x0003D0DB
		internal override bool IsResponseSuccessful()
		{
			return this.Response.IsSuccessStatusCode;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0003EEE8 File Offset: 0x0003D0E8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Response.Dispose();
			}
		}
	}
}
