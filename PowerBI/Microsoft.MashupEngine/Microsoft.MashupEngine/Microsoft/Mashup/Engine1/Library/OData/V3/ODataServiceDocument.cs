using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Data.Experimental.OData.Query;
using Microsoft.Data.OData;
using Microsoft.Data.OData.Atom;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008D7 RID: 2263
	internal class ODataServiceDocument
	{
		// Token: 0x0600409D RID: 16541 RVA: 0x000D7D64 File Offset: 0x000D5F64
		public ODataServiceDocument(ODataWorkspace serviceDocument, Uri serviceLocation, IEnumerable<QueryOptionQueryToken> queryOptions = null)
		{
			this.serviceDocument = serviceDocument;
			this.serviceLocation = ODataUriNormalizer.NormalizeServiceDocumentUri(serviceLocation);
			this.queryOptions = ((queryOptions == null) ? EmptyArray<QueryOptionQueryToken>.Instance : queryOptions.ToArray<QueryOptionQueryToken>());
		}

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x0600409E RID: 16542 RVA: 0x000D7D95 File Offset: 0x000D5F95
		public ODataWorkspace Document
		{
			get
			{
				return this.serviceDocument;
			}
		}

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x0600409F RID: 16543 RVA: 0x000D7D9D File Offset: 0x000D5F9D
		public Uri ServiceLocation
		{
			get
			{
				return this.serviceLocation;
			}
		}

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x060040A0 RID: 16544 RVA: 0x000D7DA5 File Offset: 0x000D5FA5
		public IEnumerable<QueryOptionQueryToken> QueryOptions
		{
			get
			{
				return this.queryOptions;
			}
		}

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x060040A1 RID: 16545 RVA: 0x000D7DAD File Offset: 0x000D5FAD
		public string FoundFeedName
		{
			get
			{
				return this.serviceDocument.Collections.First<ODataResourceCollectionInfo>().Name;
			}
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x000D7DC4 File Offset: 0x000D5FC4
		public static ODataServiceDocument Create(ODataMessageReader reader, Uri serviceUri, IEnumerable<QueryOptionQueryToken> queryOptions = null)
		{
			ODataServiceDocument odataServiceDocument;
			try
			{
				odataServiceDocument = new ODataServiceDocument(reader.ReadServiceDocument(), serviceUri, queryOptions);
			}
			catch (XmlException ex)
			{
				throw ODataCommonErrors.ServiceDocumentCouldNotBeParsed(serviceUri, ex);
			}
			catch (ODataException ex2)
			{
				throw ODataCommonErrors.ServiceDocumentCouldNotBeParsed(serviceUri, ex2);
			}
			return odataServiceDocument;
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x000D7E10 File Offset: 0x000D6010
		public static ODataServiceDocument Create(Uri serviceUri, string resourceName, Uri alternateUri = null, string alternateName = null, IEnumerable<QueryOptionQueryToken> queryOptions = null)
		{
			ODataWorkspace odataWorkspace = new ODataWorkspace();
			ODataWorkspace odataWorkspace2 = odataWorkspace;
			ODataResourceCollectionInfo[] array;
			if (!(serviceUri != null) || resourceName == null)
			{
				array = new ODataResourceCollectionInfo[0];
			}
			else
			{
				(array = new ODataResourceCollectionInfo[1])[0] = ODataServiceDocument.CreateResourceCollection(resourceName, serviceUri, alternateName, alternateUri);
			}
			odataWorkspace2.Collections = array;
			return new ODataServiceDocument(odataWorkspace, serviceUri, queryOptions);
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x000D7E58 File Offset: 0x000D6058
		public static ODataResourceCollectionInfo CreateResourceCollection(string collectionName, Uri baseUri, string alternateName = null, Uri alternateUri = null)
		{
			ODataResourceCollectionInfo odataResourceCollectionInfo = new ODataResourceCollectionInfo
			{
				Url = ((baseUri == null) ? new Uri(collectionName, UriKind.Relative) : new Uri(baseUri, collectionName))
			};
			odataResourceCollectionInfo.SetAnnotation<AtomResourceCollectionMetadata>(new AtomResourceCollectionMetadata
			{
				Title = new AtomTextConstruct
				{
					Kind = AtomTextConstructKind.Text,
					Text = collectionName
				}
			});
			if (alternateName != null)
			{
				odataResourceCollectionInfo.SetAnnotation<AlternateFeedName>(new AlternateFeedName
				{
					Name = alternateName,
					Uri = alternateUri
				});
			}
			return odataResourceCollectionInfo;
		}

		// Token: 0x040021F8 RID: 8696
		public static readonly ODataMessageReaderSettings ServiceDocumentReaderSettings = new ODataMessageReaderSettings
		{
			EnableAtomMetadataReading = true
		};

		// Token: 0x040021F9 RID: 8697
		private readonly ODataWorkspace serviceDocument;

		// Token: 0x040021FA RID: 8698
		private readonly Uri serviceLocation;

		// Token: 0x040021FB RID: 8699
		private readonly QueryOptionQueryToken[] queryOptions;
	}
}
