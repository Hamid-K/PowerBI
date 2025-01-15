using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Batch
{
	// Token: 0x020001D5 RID: 469
	public class ChangeSetResponseItem : ODataBatchResponseItem
	{
		// Token: 0x06000F71 RID: 3953 RVA: 0x0003F2EA File Offset: 0x0003D4EA
		public ChangeSetResponseItem(IEnumerable<HttpResponseMessage> responses)
		{
			if (responses == null)
			{
				throw Error.ArgumentNull("responses");
			}
			this.Responses = responses;
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0003F307 File Offset: 0x0003D507
		// (set) Token: 0x06000F73 RID: 3955 RVA: 0x0003F30F File Offset: 0x0003D50F
		public IEnumerable<HttpResponseMessage> Responses { get; private set; }

		// Token: 0x06000F74 RID: 3956 RVA: 0x0003F318 File Offset: 0x0003D518
		public override async Task WriteResponseAsync(ODataBatchWriter writer, CancellationToken cancellationToken)
		{
			if (writer == null)
			{
				throw Error.ArgumentNull("writer");
			}
			writer.WriteStartChangeset();
			foreach (HttpResponseMessage httpResponseMessage in this.Responses)
			{
				await ODataBatchResponseItem.WriteMessageAsync(writer, httpResponseMessage, cancellationToken);
			}
			IEnumerator<HttpResponseMessage> enumerator = null;
			writer.WriteEndChangeset();
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0003F36D File Offset: 0x0003D56D
		internal override bool IsResponseSuccessful()
		{
			return this.Responses.All((HttpResponseMessage r) => r.IsSuccessStatusCode);
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0003F39C File Offset: 0x0003D59C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (HttpResponseMessage httpResponseMessage in this.Responses)
				{
					if (httpResponseMessage != null)
					{
						httpResponseMessage.Dispose();
					}
				}
			}
		}
	}
}
