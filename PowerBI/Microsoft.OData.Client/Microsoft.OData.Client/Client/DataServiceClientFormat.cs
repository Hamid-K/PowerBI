using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client
{
	// Token: 0x02000048 RID: 72
	public sealed class DataServiceClientFormat
	{
		// Token: 0x0600022C RID: 556 RVA: 0x00009274 File Offset: 0x00007474
		internal DataServiceClientFormat(DataServiceContext context)
		{
			this.ODataFormat = ODataFormat.Json;
			this.context = context;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000928E File Offset: 0x0000748E
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00009296 File Offset: 0x00007496
		public ODataFormat ODataFormat { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000929F File Offset: 0x0000749F
		// (set) Token: 0x06000230 RID: 560 RVA: 0x000092A7 File Offset: 0x000074A7
		public Func<IEdmModel> LoadServiceModel { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000092B0 File Offset: 0x000074B0
		internal IEdmModel ServiceModel
		{
			get
			{
				if (this.serviceModel == null && this.LoadServiceModel != null)
				{
					this.serviceModel = this.LoadServiceModel();
				}
				return this.serviceModel;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000092D9 File Offset: 0x000074D9
		public void UseJson(IEdmModel serviceModel)
		{
			Util.CheckArgumentNull<IEdmModel>(serviceModel, "serviceModel");
			this.ODataFormat = ODataFormat.Json;
			this.serviceModel = serviceModel;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000092F9 File Offset: 0x000074F9
		public void UseJson()
		{
			if (this.ServiceModel == null)
			{
				throw new InvalidOperationException(Strings.DataServiceClientFormat_LoadServiceModelRequired);
			}
			this.ODataFormat = ODataFormat.Json;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009319 File Offset: 0x00007519
		internal void SetRequestAcceptHeader(HeaderCollection headers)
		{
			this.SetAcceptHeaderAndCharset(headers, DataServiceClientFormat.ChooseMediaType(false));
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009328 File Offset: 0x00007528
		internal void SetRequestAcceptHeaderForQuery(HeaderCollection headers, QueryComponents components)
		{
			this.SetAcceptHeaderAndCharset(headers, DataServiceClientFormat.ChooseMediaType(components.HasSelectQueryOption));
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000933C File Offset: 0x0000753C
		internal void SetRequestAcceptHeaderForStream(HeaderCollection headers)
		{
			this.SetAcceptHeaderAndCharset(headers, "*/*");
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000934A File Offset: 0x0000754A
		internal void SetRequestAcceptHeaderForCount(HeaderCollection headers)
		{
			this.SetAcceptHeaderAndCharset(headers, "text/plain");
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009358 File Offset: 0x00007558
		internal void SetRequestAcceptHeaderForBatch(HeaderCollection headers)
		{
			this.SetAcceptHeaderAndCharset(headers, "multipart/mixed");
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00009366 File Offset: 0x00007566
		internal void SetRequestContentTypeForEntry(HeaderCollection headers)
		{
			this.SetRequestContentTypeHeader(headers, DataServiceClientFormat.ChooseMediaType(false));
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009375 File Offset: 0x00007575
		internal void SetRequestContentTypeForOperationParameters(HeaderCollection headers)
		{
			this.SetRequestContentTypeHeader(headers, "application/json;odata.metadata=minimal");
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00009366 File Offset: 0x00007566
		internal void SetRequestContentTypeForLinks(HeaderCollection headers)
		{
			this.SetRequestContentTypeHeader(headers, DataServiceClientFormat.ChooseMediaType(false));
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009384 File Offset: 0x00007584
		internal static void ValidateCanWriteRequestFormat(IODataRequestMessage requestMessage)
		{
			string header = requestMessage.GetHeader("Content-Type");
			DataServiceClientFormat.ValidateContentType(header);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000093A4 File Offset: 0x000075A4
		internal static void ValidateCanReadResponseFormat(IODataResponseMessage responseMessage)
		{
			string header = responseMessage.GetHeader("Content-Type");
			DataServiceClientFormat.ValidateContentType(header);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000093C4 File Offset: 0x000075C4
		private static void ValidateContentType(string contentType)
		{
			if (string.IsNullOrEmpty(contentType))
			{
				return;
			}
			string text;
			ContentTypeUtil.ReadContentType(contentType, out text);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000093E3 File Offset: 0x000075E3
		private void SetRequestContentTypeHeader(HeaderCollection headers, string mediaType)
		{
			if (mediaType == "application/json;odata.metadata=minimal")
			{
				headers.SetRequestVersion(Util.ODataVersion4, this.context.MaxProtocolVersionAsVersion);
			}
			headers.SetHeaderIfUnset("Content-Type", mediaType);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009414 File Offset: 0x00007614
		[SuppressMessage("Microsoft.Performance", "CA1822", Justification = "If this becomes static, then so do its more visible callers, and we do not want to provide a mix of instance and static methods on this class.")]
		private void SetAcceptHeaderAndCharset(HeaderCollection headers, string mediaType)
		{
			headers.SetHeaderIfUnset("Accept", mediaType);
			headers.SetHeaderIfUnset("Accept-Charset", "UTF-8");
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009432 File Offset: 0x00007632
		private static string ChooseMediaType(bool hasSelectQueryOption)
		{
			if (hasSelectQueryOption)
			{
				return "application/json;odata.metadata=full";
			}
			return "application/json;odata.metadata=minimal";
		}

		// Token: 0x040000B9 RID: 185
		private const string MimeApplicationAtom = "application/atom+xml";

		// Token: 0x040000BA RID: 186
		private const string MimeApplicationJson = "application/json";

		// Token: 0x040000BB RID: 187
		private const string MimeApplicationJsonODataLight = "application/json;odata.metadata=minimal";

		// Token: 0x040000BC RID: 188
		private const string MimeApplicationJsonODataLightWithAllMetadata = "application/json;odata.metadata=full";

		// Token: 0x040000BD RID: 189
		private const string MimeMultiPartMixed = "multipart/mixed";

		// Token: 0x040000BE RID: 190
		private const string MimeApplicationXml = "application/xml";

		// Token: 0x040000BF RID: 191
		private const string MimeApplicationAtomOrXml = "application/atom+xml,application/xml";

		// Token: 0x040000C0 RID: 192
		private const string Utf8Encoding = "UTF-8";

		// Token: 0x040000C1 RID: 193
		private const string HttpAcceptCharset = "Accept-Charset";

		// Token: 0x040000C2 RID: 194
		private readonly DataServiceContext context;

		// Token: 0x040000C3 RID: 195
		private IEdmModel serviceModel;
	}
}
