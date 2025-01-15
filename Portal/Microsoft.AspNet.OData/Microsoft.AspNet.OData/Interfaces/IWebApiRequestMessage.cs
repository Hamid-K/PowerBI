using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Formatter.Deserialization;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Interfaces
{
	// Token: 0x02000060 RID: 96
	internal interface IWebApiRequestMessage
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000298 RID: 664
		IWebApiContext Context { get; }

		// Token: 0x06000299 RID: 665
		bool IsCountRequest();

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600029A RID: 666
		ODataRequestMethod Method { get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600029B RID: 667
		IWebApiOptions Options { get; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600029C RID: 668
		IWebApiHeaders Headers { get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600029D RID: 669
		IServiceProvider RequestContainer { get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600029E RID: 670
		Uri RequestUri { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600029F RID: 671
		ODataDeserializerProvider DeserializerProvider { get; }

		// Token: 0x060002A0 RID: 672
		string CreateETag(IDictionary<string, object> properties);

		// Token: 0x060002A1 RID: 673
		ETag GetETag(EntityTagHeaderValue etagHeaderValue);

		// Token: 0x060002A2 RID: 674
		ETag GetETag<TEntity>(EntityTagHeaderValue etagHeaderValue);

		// Token: 0x060002A3 RID: 675
		Uri GetNextPageLink(int pageSize, object instance, Func<object, string> objToSkipTokenValue);

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002A4 RID: 676
		IDictionary<string, string> ODataContentIdMapping { get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002A5 RID: 677
		IODataPathHandler PathHandler { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002A6 RID: 678
		IDictionary<string, string> QueryParameters { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002A7 RID: 679
		ODataMessageReaderSettings ReaderSettings { get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002A8 RID: 680
		ODataMessageWriterSettings WriterSettings { get; }
	}
}
