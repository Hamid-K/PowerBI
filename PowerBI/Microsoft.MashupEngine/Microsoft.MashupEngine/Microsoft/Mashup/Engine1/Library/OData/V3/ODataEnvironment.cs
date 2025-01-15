using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Data.Edm;
using Microsoft.Data.Experimental.OData.Query;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.OData.V3.Compiler;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008C0 RID: 2240
	internal class ODataEnvironment : ODataEnvironmentBase
	{
		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x06004013 RID: 16403 RVA: 0x000D5BDD File Offset: 0x000D3DDD
		public IEdmModel EdmModel
		{
			get
			{
				return this.v3Settings.EdmModel;
			}
		}

		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x06004014 RID: 16404 RVA: 0x000D5BEA File Offset: 0x000D3DEA
		public ODataServiceDocument ServiceDocument
		{
			get
			{
				return this.serviceDocument;
			}
		}

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x06004015 RID: 16405 RVA: 0x000D5BF2 File Offset: 0x000D3DF2
		public override Uri ServiceUri
		{
			get
			{
				return this.serviceDocument.ServiceLocation;
			}
		}

		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x06004016 RID: 16406 RVA: 0x000D5BFF File Offset: 0x000D3DFF
		public IEnumerable<QueryOptionQueryToken> QueryOptions
		{
			get
			{
				return this.serviceDocument.QueryOptions;
			}
		}

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x06004017 RID: 16407 RVA: 0x000D5C0C File Offset: 0x000D3E0C
		public override bool SupportJson
		{
			get
			{
				return this.EdmModel != null && this.v3Settings.ServerVersion == ODataServerVersion.V3;
			}
		}

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x06004018 RID: 16408 RVA: 0x000D5C26 File Offset: 0x000D3E26
		public ODataSettings Settings
		{
			get
			{
				return this.v3Settings;
			}
		}

		// Token: 0x06004019 RID: 16409 RVA: 0x000D5C2E File Offset: 0x000D3E2E
		protected ODataEnvironment(ODataServiceDocument serviceDocument, EdmModelProcessorOutputBase output, Value headers, HttpResource resource, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings, ResourceCredentialCollection cachedCredentials)
			: base(output, headers, resource, host, settings, userSettings, cachedCredentials)
		{
			this.serviceDocument = serviceDocument;
			this.v3Settings = settings;
		}

		// Token: 0x0600401A RID: 16410 RVA: 0x000D5C51 File Offset: 0x000D3E51
		protected override void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			ODataExpressionChecker.Check(expression, cursor, this);
		}

		// Token: 0x0600401B RID: 16411 RVA: 0x000D5C5B File Offset: 0x000D3E5B
		protected override ValueBuilderBase Compile(Query originalQuery, IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			return new ODataValueBuilder(originalQuery, this, ODataQueryCreator.Create(expression, cursor, this));
		}

		// Token: 0x0600401C RID: 16412 RVA: 0x000D5C6C File Offset: 0x000D3E6C
		public static ODataEnvironment Create(ODataServiceDocument serviceDoc, Value headers, HttpResource resource, ResourceCredentialCollection credentials, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings, bool useCachedCredentials)
		{
			Uri uri = ((serviceDoc != null) ? serviceDoc.ServiceLocation : null);
			ODataEnvironmentBase.UpdateMaxConcurrentRequests(uri, settings);
			if (uri != null)
			{
				settings.UpdateModel(uri, null, headers, resource, credentials, userSettings);
			}
			EdmModelProcessorOutput edmModelProcessorOutput = EdmModelProcessor.Build(host, serviceDoc, settings.EdmModel, userSettings, resource);
			return new ODataEnvironment(serviceDoc, edmModelProcessorOutput, headers, resource, host, settings, userSettings, useCachedCredentials ? credentials : null);
		}

		// Token: 0x0600401D RID: 16413 RVA: 0x000D5CCF File Offset: 0x000D3ECF
		public static ODataEnvironment Create(ODataServiceDocument serviceDocument, IDictionary<ODataSchemaItem, TypeValue> typeCatalog, Value headers, HttpResource resource, IEngineHost host, IDictionary<ODataSchemaItem, string> collectionUrls, RecordValue typeMetaValue, ODataSettings settings, ODataUserSettings userSettings)
		{
			return new ODataEnvironment(serviceDocument, new EdmModelProcessorOutput
			{
				TypeCatalog = typeCatalog,
				CollectionUrls = collectionUrls,
				TypeMetaValue = typeMetaValue
			}, headers, resource, host, settings, userSettings, null);
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x000D5CFC File Offset: 0x000D3EFC
		public override bool OtherCanFoldToThis(EnvironmentBase other)
		{
			ODataEnvironment odataEnvironment = other as ODataEnvironment;
			return odataEnvironment != null && (this.serviceDocument == odataEnvironment.serviceDocument && this.EdmModel == odataEnvironment.EdmModel && base.TypeCatalog == odataEnvironment.TypeCatalog && this.headers == odataEnvironment.headers) && this.output.CollectionUrls == odataEnvironment.output.CollectionUrls;
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x000D5D68 File Offset: 0x000D3F68
		protected override Value BuildTableForTableValueKind(string tableName, TypeValue typeValue)
		{
			Value value = base.BuildTableForTableValueKind(tableName, typeValue);
			foreach (IEdmEntityContainer edmEntityContainer in this.EdmModel.EntityContainers())
			{
				IEdmEntitySet edmEntitySet = edmEntityContainer.FindEntitySet(tableName);
				if (edmEntitySet != null)
				{
					value = this.AddRelationships(edmEntitySet, tableName, value.AsTable);
					List<Microsoft.Mashup.Engine1.Runtime.NamedValue> vocabularyAnnotations = EdmModelProcessor.GetVocabularyAnnotations(edmEntitySet, this.EdmModel, base.UserSettings);
					if (vocabularyAnnotations.Count > 0)
					{
						value = BinaryOperator.AddMeta.Invoke(value, RecordValue.New(vocabularyAnnotations.ToArray())).AsTable;
						return value;
					}
				}
			}
			return value;
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x000D5E18 File Offset: 0x000D4018
		private string GetRelationshipIdentity(string navSource)
		{
			return ODataEnvironment.CacheKey.Qualify(this.ServiceUri.OriginalString, navSource);
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x000D5E40 File Offset: 0x000D4040
		private TableValue AddRelationships(IEdmEntitySet set, string tableName, TableValue tableValue)
		{
			using (IEnumerator<IEdmNavigationTargetMapping> enumerator = set.NavigationTargets.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IEdmNavigationTargetMapping target = enumerator.Current;
					if (!target.NavigationProperty.IsPrincipal && target.NavigationProperty.DependentProperties != null)
					{
						KeysBuilder keysBuilder = default(KeysBuilder);
						List<int> list = new List<int>();
						IEnumerator<IEdmStructuralProperty> enumerator2 = target.NavigationProperty.DependentProperties.GetEnumerator();
						IEnumerator<IEdmStructuralProperty> enumerator3 = target.TargetEntitySet.ElementType.Key().GetEnumerator();
						while (enumerator3.MoveNext() && enumerator2.MoveNext())
						{
							keysBuilder.Add(enumerator3.Current.Name);
							int num = tableValue.Columns.IndexOfKey(enumerator2.Current.Name);
							list.Add(num);
						}
						if (enumerator2.MoveNext() || enumerator3.MoveNext())
						{
							using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "V3/ODataEnvironmentV3/AddRelationships", TraceEventType.Information, this.Resource))
							{
								hostTrace.Add("RelationshipPropertyNotFound", (enumerator2.Current != null) ? enumerator2.Current.Name : enumerator3.Current.Name, true);
								break;
							}
						}
						Value value = new LinkTableFunctionValue(delegate
						{
							ODataSchemaItem odataSchemaItem = new ODataSchemaItem(target.TargetEntitySet.Name, "table");
							TypeValue typeValue;
							if (this.TypeCatalog.TryGetValue(odataSchemaItem, out typeValue))
							{
								return this.<>n__0(target.TargetEntitySet.Name, typeValue).AsTable.ReplaceRelationshipIdentity(this.GetRelationshipIdentity(target.TargetEntitySet.Name));
							}
							return TableValue.Empty;
						});
						tableValue = RelatedTablesTableValue.New(tableValue, tableValue.RelatedTables, tableValue.ColumnIdentities, Relationships.NestedJoin(tableValue.Relationships, list.ToArray(), value, keysBuilder.ToKeys()));
					}
				}
			}
			tableValue = tableValue.ReplaceRelationshipIdentity(this.GetRelationshipIdentity(tableName));
			return tableValue;
		}

		// Token: 0x040021B6 RID: 8630
		private static readonly PersistentCacheKey CacheKey = new PersistentCacheKey("OData");

		// Token: 0x040021B7 RID: 8631
		private readonly ODataServiceDocument serviceDocument;

		// Token: 0x040021B8 RID: 8632
		private readonly ODataSettings v3Settings;
	}
}
