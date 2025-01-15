using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Write
{
	// Token: 0x02000788 RID: 1928
	internal abstract class ODataWriteRequest
	{
		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x0600389A RID: 14490 RVA: 0x000B69F0 File Offset: 0x000B4BF0
		public IValueReference Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x0600389B RID: 14491 RVA: 0x000B69F8 File Offset: 0x000B4BF8
		public ODataEnvironment OdataEnvironment
		{
			get
			{
				return this.odataEnvironment;
			}
		}

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x0600389C RID: 14492 RVA: 0x000B6A00 File Offset: 0x000B4C00
		public string Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x0600389D RID: 14493 RVA: 0x000B6A08 File Offset: 0x000B4C08
		public Uri OdataUri
		{
			get
			{
				return this.odataUri;
			}
		}

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x0600389E RID: 14494 RVA: 0x000B6A10 File Offset: 0x000B4C10
		public Microsoft.OData.Edm.IEdmNavigationSource Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x0600389F RID: 14495 RVA: 0x000B6A18 File Offset: 0x000B4C18
		public Dictionary<string, string> Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x060038A0 RID: 14496
		public abstract IValueReference ProcessWebResponse(HttpResponseData data);

		// Token: 0x060038A1 RID: 14497 RVA: 0x000B6A20 File Offset: 0x000B4C20
		public ODataWriteRequest(Uri odataUri, IValueReference content, Dictionary<string, string> headers, string method, Microsoft.OData.Edm.IEdmNavigationSource source, ODataEnvironment environment)
		{
			this.content = content;
			this.odataEnvironment = environment;
			this.headers = headers;
			this.method = method;
			this.odataUri = odataUri;
			this.source = source;
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x000B6A58 File Offset: 0x000B4C58
		public MashupHttpWebRequest CreateWebRequest()
		{
			IResource resource = this.OdataEnvironment.HttpResource.NewUrl(this.OdataUri.AbsoluteUri).Resource;
			IResource resource2;
			MashupHttpWebRequest mashupHttpWebRequest = this.OdataEnvironment.GetRequest(this.OdataUri, false).BuildWebRequest(resource, this.OdataUri, out resource2);
			mashupHttpWebRequest.Method = this.Method;
			if (this.Headers != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.Headers)
				{
					mashupHttpWebRequest.Headers.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return mashupHttpWebRequest;
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000B6B18 File Offset: 0x000B4D18
		public virtual void SetODataRequestHeaders(IODataRequestMessage requestMessage)
		{
			requestMessage.Method = this.Method;
			if (this.Headers != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.Headers)
				{
					requestMessage.SetHeader(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x000B6B8C File Offset: 0x000B4D8C
		public virtual void SetODataRequestContents(IODataRequestMessage requestMessage)
		{
			this.SetODataRequestHeaders(requestMessage);
			using (ODataMessageWriter odataMessageWriter = new ODataMessageWriter(requestMessage))
			{
				new ODataResourceValueWriter(odataMessageWriter.CreateODataResourceWriter()).WriteRecord(this.Content.Value.AsRecord, this.Source.EntityType());
			}
		}

		// Token: 0x04001D46 RID: 7494
		public const string StatusCode = "StatusCode";

		// Token: 0x04001D47 RID: 7495
		public const string ODataUri = "ODataUri";

		// Token: 0x04001D48 RID: 7496
		public const string ErrorMessage = "Error";

		// Token: 0x04001D49 RID: 7497
		private readonly string method;

		// Token: 0x04001D4A RID: 7498
		private readonly Microsoft.OData.Edm.IEdmNavigationSource source;

		// Token: 0x04001D4B RID: 7499
		private readonly Dictionary<string, string> headers;

		// Token: 0x04001D4C RID: 7500
		private readonly IValueReference content;

		// Token: 0x04001D4D RID: 7501
		private readonly ODataEnvironment odataEnvironment;

		// Token: 0x04001D4E RID: 7502
		private readonly Uri odataUri;
	}
}
