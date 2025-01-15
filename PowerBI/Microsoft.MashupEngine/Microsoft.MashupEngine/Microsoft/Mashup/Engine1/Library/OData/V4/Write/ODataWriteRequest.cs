using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4.Write
{
	// Token: 0x020008A3 RID: 2211
	internal abstract class ODataWriteRequest
	{
		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x06003F46 RID: 16198 RVA: 0x000D053D File Offset: 0x000CE73D
		public IValueReference Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x06003F47 RID: 16199 RVA: 0x000D0545 File Offset: 0x000CE745
		public ODataEnvironment OdataEnvironment
		{
			get
			{
				return this.odataEnvironment;
			}
		}

		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x06003F48 RID: 16200 RVA: 0x000D054D File Offset: 0x000CE74D
		public string Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x06003F49 RID: 16201 RVA: 0x000D0555 File Offset: 0x000CE755
		public Uri OdataUri
		{
			get
			{
				return this.odataUri;
			}
		}

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x06003F4A RID: 16202 RVA: 0x000D055D File Offset: 0x000CE75D
		public Microsoft.OData.Edm.IEdmNavigationSource Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x06003F4B RID: 16203 RVA: 0x000D0565 File Offset: 0x000CE765
		public Dictionary<string, string> Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x06003F4C RID: 16204
		public abstract IValueReference ProcessWebResponse(HttpResponseData data);

		// Token: 0x06003F4D RID: 16205 RVA: 0x000D056D File Offset: 0x000CE76D
		public ODataWriteRequest(Uri odataUri, IValueReference content, Dictionary<string, string> headers, string method, Microsoft.OData.Edm.IEdmNavigationSource source, ODataEnvironment environment)
		{
			this.content = content;
			this.odataEnvironment = environment;
			this.headers = headers;
			this.method = method;
			this.odataUri = odataUri;
			this.source = source;
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x000D05A4 File Offset: 0x000CE7A4
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

		// Token: 0x06003F4F RID: 16207 RVA: 0x000D0664 File Offset: 0x000CE864
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

		// Token: 0x06003F50 RID: 16208 RVA: 0x000D06D8 File Offset: 0x000CE8D8
		public virtual void SetODataRequestContents(IODataRequestMessage requestMessage)
		{
			this.SetODataRequestHeaders(requestMessage);
			using (ODataMessageWriter odataMessageWriter = new ODataMessageWriter(requestMessage))
			{
				ODataWriter odataWriter = odataMessageWriter.CreateODataEntryWriter();
				RecordValue asRecord = this.Content.Value.AsRecord;
				List<ODataProperty> list = new List<ODataProperty>(asRecord.Count);
				for (int i = 0; i < asRecord.Count; i++)
				{
					Microsoft.OData.Edm.IEdmProperty edmProperty = this.Source.EntityType().FindProperty(asRecord.Keys[i]);
					object obj = new ValueODataValueVisitor().ToSource(asRecord[i], edmProperty.Type.Definition);
					list.Add(new ODataProperty
					{
						Name = asRecord.Keys[i],
						Value = obj
					});
				}
				ODataEntry odataEntry = new ODataEntry
				{
					Properties = list
				};
				Value value;
				if (this.Content.Value.MetaValue.TryGetValue("@odata.type", out value) && !value.IsNull)
				{
					odataEntry.TypeName = value.AsString;
				}
				odataWriter.WriteStart(odataEntry);
				odataWriter.WriteEnd();
			}
		}

		// Token: 0x0400213B RID: 8507
		public const string StatusCode = "StatusCode";

		// Token: 0x0400213C RID: 8508
		public const string ODataUri = "ODataUri";

		// Token: 0x0400213D RID: 8509
		public const string ErrorMessage = "Error";

		// Token: 0x0400213E RID: 8510
		private readonly string method;

		// Token: 0x0400213F RID: 8511
		private readonly Microsoft.OData.Edm.IEdmNavigationSource source;

		// Token: 0x04002140 RID: 8512
		private readonly Dictionary<string, string> headers;

		// Token: 0x04002141 RID: 8513
		private readonly IValueReference content;

		// Token: 0x04002142 RID: 8514
		private readonly ODataEnvironment odataEnvironment;

		// Token: 0x04002143 RID: 8515
		private readonly Uri odataUri;
	}
}
