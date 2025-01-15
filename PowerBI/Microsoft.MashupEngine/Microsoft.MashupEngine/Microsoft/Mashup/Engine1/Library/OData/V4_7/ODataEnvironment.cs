using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.Edm;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7
{
	// Token: 0x0200076B RID: 1899
	internal class ODataEnvironment : ODataEnvironmentBase
	{
		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x060037EB RID: 14315 RVA: 0x000B3479 File Offset: 0x000B1679
		public Microsoft.OData.Edm.IEdmModel EdmModel
		{
			get
			{
				return this.v4Settings.EdmModel;
			}
		}

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x060037EC RID: 14316 RVA: 0x000B3486 File Offset: 0x000B1686
		public override Uri ServiceUri
		{
			get
			{
				return this.serviceRoot;
			}
		}

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x060037ED RID: 14317 RVA: 0x00002139 File Offset: 0x00000339
		public override bool SupportJson
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x060037EE RID: 14318 RVA: 0x000B348E File Offset: 0x000B168E
		public ODataSettings Settings
		{
			get
			{
				return this.v4Settings;
			}
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x060037EF RID: 14319 RVA: 0x000B3496 File Offset: 0x000B1696
		public Annotations Annotations
		{
			get
			{
				return this.ProcessedModel.Annotations;
			}
		}

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x060037F0 RID: 14320 RVA: 0x000B34A3 File Offset: 0x000B16A3
		public Dictionary<string, List<KeyValuePair<Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue>>> UnboundFunctionOverloads
		{
			get
			{
				return this.ProcessedModel.UnboundFunctionOverloads;
			}
		}

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x060037F1 RID: 14321 RVA: 0x000B34B0 File Offset: 0x000B16B0
		public RecordValue TypeMetaValue
		{
			get
			{
				return this.ProcessedModel.TypeMetaValue;
			}
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x060037F2 RID: 14322 RVA: 0x000B34BD File Offset: 0x000B16BD
		public Uri MetadataUri
		{
			get
			{
				return this.metadataUri;
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x060037F3 RID: 14323 RVA: 0x000B34C5 File Offset: 0x000B16C5
		// (set) Token: 0x060037F4 RID: 14324 RVA: 0x000B34CD File Offset: 0x000B16CD
		private EdmModelProcessorOutput ProcessedModel { get; set; }

		// Token: 0x060037F5 RID: 14325 RVA: 0x000B34D6 File Offset: 0x000B16D6
		protected ODataEnvironment(Uri serviceRoot, Uri metadataUri, EdmTypeToTypeValueVisitor edmTypeToTypeValueVisitor, EdmModelProcessorOutput output, Value headers, HttpResource resource, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings, ResourceCredentialCollection cachedCredentials)
			: base(output, headers, resource, host, settings, userSettings, cachedCredentials)
		{
			this.ProcessedModel = output;
			this.v4Settings = settings;
			this.metadataUri = metadataUri;
			this.serviceRoot = ODataUriNormalizer.NormalizeServiceRoot(serviceRoot);
			this.edmTypeToTypeValueVisitor = edmTypeToTypeValueVisitor;
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x000091AE File Offset: 0x000073AE
		protected override void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x000091AE File Offset: 0x000073AE
		protected override ValueBuilderBase Compile(Query originalQuery, IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x000B3518 File Offset: 0x000B1718
		public static ODataEnvironment Create(Uri serviceRoot, Uri metadataUri, Value headers, HttpResource resource, Uri requestUri, ResourceCredentialCollection credentials, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings, bool useCachedCredentials)
		{
			ODataEnvironmentBase.UpdateMaxConcurrentRequests(serviceRoot, settings);
			IObjectCache objectCache = host.QueryService<ICacheSets>().Data.ObjectCache;
			PersistentCacheKeyBuilder persistentCacheKeyBuilder = new PersistentCacheKeyBuilder();
			persistentCacheKeyBuilder.Add(userSettings.Fingerprint);
			persistentCacheKeyBuilder.Add("EdmModelProcessorOutput");
			string cacheKey = settings.Cache.GetCacheKey(credentials, headers, metadataUri, persistentCacheKeyBuilder.ToString());
			object obj;
			EdmModelProcessorOutput edmModelProcessorOutput;
			EdmTypeToTypeValueVisitor edmTypeToTypeValueVisitor;
			if (objectCache.TryGetValue(cacheKey, out obj))
			{
				edmModelProcessorOutput = (EdmModelProcessorOutput)obj;
				edmTypeToTypeValueVisitor = new EdmTypeToTypeValueVisitor(resource.Resource, settings.EdmModel, userSettings, edmModelProcessorOutput);
			}
			else
			{
				EdmModelProcessor edmModelProcessor = new EdmModelProcessor(host, serviceRoot, settings.EdmModel, userSettings, resource.Resource);
				edmModelProcessorOutput = edmModelProcessor.Build(resource.NewUrl(serviceRoot.AbsoluteUri));
				edmTypeToTypeValueVisitor = edmModelProcessor.EdmTypeToTypeValueVisitor;
				objectCache.CommitValue(cacheKey, settings.EdmModelSize, edmModelProcessorOutput);
			}
			return new ODataEnvironment(serviceRoot, metadataUri, edmTypeToTypeValueVisitor, edmModelProcessorOutput, headers, resource, host, settings, userSettings, useCachedCredentials ? credentials : null);
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x000B3608 File Offset: 0x000B1808
		public override bool OtherCanFoldToThis(EnvironmentBase other)
		{
			ODataEnvironment odataEnvironment = other as ODataEnvironment;
			return odataEnvironment != null && this.EdmModel == odataEnvironment.EdmModel && this.headers == odataEnvironment.headers;
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x000B363F File Offset: 0x000B183F
		public TypeValue ConvertType(Microsoft.OData.Edm.IEdmType type)
		{
			return this.edmTypeToTypeValueVisitor.VisitType(type);
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x000B3650 File Offset: 0x000B1850
		public Value BuildFunctionValue(string functionName, TypeValue typeValue, ODataResource resource, Microsoft.OData.Edm.IEdmEntitySetBase resourceEntitySet)
		{
			Dictionary<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue> dictionary;
			if (this.ProcessedModel.BoundFunctionOverloads.TryGetValue(functionName, out dictionary))
			{
				return new BoundFunctionValue(this, typeValue.AsFunctionType, dictionary, resource, resourceEntitySet);
			}
			return base.BuildFunctionValue(functionName, typeValue, null, null);
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x000B3690 File Offset: 0x000B1890
		public ODataRequest GetRequest(Uri uri, bool catch404)
		{
			return new ODataRequest(base.HttpResource, this.ServiceUri, uri, base.Headers, base.Credentials, base.SettingsBase.ProposedResultContentTypes, false, catch404, base.Host, this.Settings, base.UserSettings, this.Settings.ServerVersion, true);
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x000B36E8 File Offset: 0x000B18E8
		public bool TryConvertToOpaquePath(Uri uri, out ODataPath path)
		{
			path = null;
			if (!string.IsNullOrEmpty(uri.Query))
			{
				return false;
			}
			bool flag;
			try
			{
				ICollection<string> collection = new UriPathParser(new ODataUriParserSettings()).ParsePathIntoSegments(uri, this.ServiceUri);
				path = new ODataPath(collection.Select((string s) => new DynamicPathSegment(s)));
				flag = true;
			}
			catch (ODataException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x000B3768 File Offset: 0x000B1968
		public Uri GetCanonicalUrl(Microsoft.OData.Edm.IEdmNamedElement element)
		{
			Capabilities elementCapability = this.Annotations.GetElementCapability(element);
			if (elementCapability.ResourcePath != null)
			{
				Uri uri = elementCapability.ResourcePath;
				if (!uri.IsAbsoluteUri)
				{
					uri = new Uri(this.ServiceUri, uri);
				}
				return uri;
			}
			return new Uri(this.ServiceUri, "/" + element.Name);
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x000B37C9 File Offset: 0x000B19C9
		public Lazy<IODataPayloadReader> GetRequestReader(GetReaderArgs args)
		{
			return new Lazy<IODataPayloadReader>(() => ODataResponse.CreateResponseReader(this.Host, this.resource, args.Uri, HttpResponseDataWithContextUri.FixupRelativeMetadataUrl(this.GetRequest(args.Uri, args.Catch404).GetResponseStream()), this.Settings.EdmModel));
		}

		// Token: 0x04001D07 RID: 7431
		private readonly ODataSettings v4Settings;

		// Token: 0x04001D08 RID: 7432
		private readonly Uri metadataUri;

		// Token: 0x04001D09 RID: 7433
		private readonly Uri serviceRoot;

		// Token: 0x04001D0A RID: 7434
		private readonly EdmTypeToTypeValueVisitor edmTypeToTypeValueVisitor;
	}
}
