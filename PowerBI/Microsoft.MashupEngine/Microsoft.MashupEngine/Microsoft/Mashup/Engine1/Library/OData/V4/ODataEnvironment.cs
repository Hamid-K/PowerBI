using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200085C RID: 2140
	internal class ODataEnvironment : ODataEnvironmentBase
	{
		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x06003DA0 RID: 15776 RVA: 0x000C8CB3 File Offset: 0x000C6EB3
		public Microsoft.OData.Edm.IEdmModel EdmModel
		{
			get
			{
				return this.v4Settings.EdmModel;
			}
		}

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x06003DA1 RID: 15777 RVA: 0x000C8CC0 File Offset: 0x000C6EC0
		public override Uri ServiceUri
		{
			get
			{
				return this.serviceRoot;
			}
		}

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x06003DA2 RID: 15778 RVA: 0x00002139 File Offset: 0x00000339
		public override bool SupportJson
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x06003DA3 RID: 15779 RVA: 0x000C8CC8 File Offset: 0x000C6EC8
		public ODataSettings Settings
		{
			get
			{
				return this.v4Settings;
			}
		}

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x06003DA4 RID: 15780 RVA: 0x000C8CD0 File Offset: 0x000C6ED0
		public Annotations Annotations
		{
			get
			{
				return this.ProcessedModel.Annotations;
			}
		}

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x06003DA5 RID: 15781 RVA: 0x000C8CDD File Offset: 0x000C6EDD
		public Dictionary<Microsoft.OData.Edm.IEdmType, TypeValue> EdmTypeValueLookup
		{
			get
			{
				return this.ProcessedModel.EdmTypeValueLookup;
			}
		}

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x06003DA6 RID: 15782 RVA: 0x000C8CEA File Offset: 0x000C6EEA
		public Uri MetadataUri
		{
			get
			{
				return this.metadataUri;
			}
		}

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x06003DA7 RID: 15783 RVA: 0x000C8CF2 File Offset: 0x000C6EF2
		// (set) Token: 0x06003DA8 RID: 15784 RVA: 0x000C8CFA File Offset: 0x000C6EFA
		private EdmModelProcessorOutput ProcessedModel { get; set; }

		// Token: 0x06003DA9 RID: 15785 RVA: 0x000C8D04 File Offset: 0x000C6F04
		protected ODataEnvironment(ServiceDocumentWrapper serviceDocument, Uri metadataUri, EdmModelProcessorOutput output, Value headers, HttpResource resource, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings, ResourceCredentialCollection cachedCredentials)
			: base(output, headers, resource, host, settings, userSettings, cachedCredentials)
		{
			this.ProcessedModel = output;
			this.serviceDocument = serviceDocument;
			this.v4Settings = settings;
			this.metadataUri = metadataUri;
			this.serviceRoot = ODataUriNormalizer.NormalizeServiceRoot(serviceDocument.ServiceUri);
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x000091AE File Offset: 0x000073AE
		protected override void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x000091AE File Offset: 0x000073AE
		protected override ValueBuilderBase Compile(Query originalQuery, IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x000C8D54 File Offset: 0x000C6F54
		public static ODataEnvironment Create(ServiceDocumentWrapper serviceDoc, Uri metadataUri, Value headers, HttpResource resource, Uri requestUri, ResourceCredentialCollection credentials, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings, bool useCachedCredentials)
		{
			ODataEnvironmentBase.UpdateMaxConcurrentRequests((serviceDoc != null) ? serviceDoc.ServiceUri : null, settings);
			EdmModelProcessorOutput edmModelProcessorOutput = EdmModelProcessor.Build(host, serviceDoc, settings.EdmModel, userSettings, resource);
			return new ODataEnvironment(serviceDoc, metadataUri, edmModelProcessorOutput, headers, resource, host, settings, userSettings, useCachedCredentials ? credentials : null);
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x000C8DA0 File Offset: 0x000C6FA0
		public bool TryCreateQueryTableValue(Uri uri, RecordTypeValue type, out TableValue value)
		{
			if (string.IsNullOrEmpty(uri.Query) && this.EdmModel.EntityContainer != null)
			{
				string[] array = uri.AbsolutePath.Split(new char[] { '/' });
				Microsoft.OData.Edm.IEdmEntitySet edmEntitySet = this.EdmModel.EntityContainer.FindEntitySet(array[array.Length - 1]);
				if (edmEntitySet != null)
				{
					ODataQuery odataQuery = new ODataQuery(this, edmEntitySet, type);
					value = new QueryTableValue(new OptimizableQuery(odataQuery));
					value = ODataRelationship.AddRelationships(value, edmEntitySet, this);
					return true;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x000C8E20 File Offset: 0x000C7020
		public override bool OtherCanFoldToThis(EnvironmentBase other)
		{
			ODataEnvironment odataEnvironment = other as ODataEnvironment;
			return odataEnvironment != null && (this.serviceDocument == odataEnvironment.serviceDocument && this.EdmModel == odataEnvironment.EdmModel && base.TypeCatalog == odataEnvironment.TypeCatalog && this.headers == odataEnvironment.headers) && this.output.CollectionUrls == odataEnvironment.output.CollectionUrls;
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x000C8E8C File Offset: 0x000C708C
		public override Value BuildFunctionValue(string functionName, TypeValue typeValue, ODataEntry entry, Microsoft.OData.Edm.IEdmEntitySetBase entryEntitySet)
		{
			Dictionary<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue> dictionary;
			if (this.ProcessedModel.BoundFunctionOverloads.TryGetValue(functionName, out dictionary))
			{
				return new BoundFunctionValue(this, typeValue.AsFunctionType, dictionary, entry, entryEntitySet);
			}
			return base.BuildFunctionValue(functionName, typeValue, entry, entryEntitySet);
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x000C8ECC File Offset: 0x000C70CC
		public ODataRequest GetRequest(Uri uri, bool catch404)
		{
			return new ODataRequest(base.HttpResource, this.ServiceUri, uri, base.Headers, base.Credentials, base.SettingsBase.ProposedResultContentTypes, false, catch404, base.Host, this.Settings, base.UserSettings, this.Settings.ServerVersion, true);
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x000C8F24 File Offset: 0x000C7124
		protected override Value BuildFunctionImportValue(string functionName, TypeValue typeValue)
		{
			string functionSignature = typeValue.AsFunctionType.CreateSignature();
			Tuple<string, Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue> tuple = this.ProcessedModel.UnboundFunctionOverloads.Where((Tuple<string, Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue> e) => e.Item1.Equals(functionSignature, StringComparison.Ordinal) && e.Item2.Name.Equals(functionName, StringComparison.Ordinal)).SingleOrDefault<Tuple<string, Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue>>();
			if (tuple != null)
			{
				return new UnboundFunctionValue(this, typeValue.AsFunctionType, tuple);
			}
			return base.BuildFunctionImportValue(functionName, typeValue);
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x000C8F90 File Offset: 0x000C7190
		protected override Value BuildTableForTableValueKind(string tableName, TypeValue typeValue)
		{
			EdmModelProcessorOutput edmModelProcessorOutput = this.output as EdmModelProcessorOutput;
			Microsoft.OData.Edm.IEdmEntitySet edmEntitySet = this.EdmModel.EntityContainer.FindEntitySet(tableName);
			if (edmEntitySet != null)
			{
				TableValue tableValue = new QueryTableValue(new OptimizableQuery(new ODataQuery(this, edmEntitySet, typeValue.AsTableType.ItemType.AsRecordType)), typeValue);
				if (edmModelProcessorOutput != null)
				{
					Capabilities elementCapability = edmModelProcessorOutput.Annotations.GetElementCapability(edmEntitySet);
					tableValue = BinaryOperator.AddMeta.Invoke(tableValue, RecordValue.New(elementCapability.DisplayAnnotations.ToArray())).AsTable;
				}
				return ODataRelationship.AddRelationships(tableValue, edmEntitySet, this);
			}
			Microsoft.OData.Edm.IEdmSingleton edmSingleton = this.EdmModel.EntityContainer.FindSingleton(tableName);
			TableValue tableValue2 = new QueryTableValue(new OptimizableQuery(new ODataQuery(this, edmSingleton, typeValue.AsTableType.ItemType.AsRecordType)), typeValue);
			if (edmModelProcessorOutput != null)
			{
				Capabilities elementCapability2 = edmModelProcessorOutput.Annotations.GetElementCapability(edmSingleton);
				tableValue2 = BinaryOperator.AddMeta.Invoke(tableValue2, RecordValue.New(elementCapability2.DisplayAnnotations.ToArray())).AsTable;
			}
			return ODataRelationship.AddRelationships(tableValue2, edmSingleton, this);
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x000C9093 File Offset: 0x000C7293
		public Lazy<ODataReaderWrapper> GetRequestReader(GetReaderArgs args)
		{
			return new Lazy<ODataReaderWrapper>(() => ODataReaderWrapper.CreateFromHttpResponseData(this.host, this.GetRequest(args.Uri, args.Catch404).GetResponseStream(), args.Uri, this.Resource.Kind, args.IsFeed, this.Settings.EdmModel));
		}

		// Token: 0x04002064 RID: 8292
		private readonly ServiceDocumentWrapper serviceDocument;

		// Token: 0x04002065 RID: 8293
		private readonly ODataSettings v4Settings;

		// Token: 0x04002066 RID: 8294
		private readonly Uri metadataUri;

		// Token: 0x04002067 RID: 8295
		private readonly Uri serviceRoot;
	}
}
