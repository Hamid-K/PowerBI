using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000730 RID: 1840
	internal abstract class ODataEnvironmentBase : EnvironmentBase, IODataService
	{
		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x060036C3 RID: 14019 RVA: 0x000AEB0C File Offset: 0x000ACD0C
		public ResourceCredentialCollection Credentials
		{
			get
			{
				if (this.credentials == null)
				{
					if (this.cachedCredentials != null)
					{
						return this.cachedCredentials;
					}
					HttpServices.VerifyPermissionAndGetCredentials(this.host, this.resource.Resource, this.userSettings.ApiKeyName != null, out this.credentials);
				}
				return this.credentials;
			}
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x060036C4 RID: 14020 RVA: 0x000AEB60 File Offset: 0x000ACD60
		public Value Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x060036C5 RID: 14021 RVA: 0x000AEB68 File Offset: 0x000ACD68
		public sealed override IResource Resource
		{
			get
			{
				return this.resource.Resource;
			}
		}

		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x060036C6 RID: 14022 RVA: 0x000AEB75 File Offset: 0x000ACD75
		public HttpResource HttpResource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x060036C7 RID: 14023
		public abstract Uri ServiceUri { get; }

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x060036C8 RID: 14024 RVA: 0x000AEB7D File Offset: 0x000ACD7D
		public bool UseConcurrentRequests
		{
			get
			{
				return this.userSettings.UseConcurrentRequests;
			}
		}

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x060036C9 RID: 14025 RVA: 0x000AEB8A File Offset: 0x000ACD8A
		public bool ForceConcurrentRequests
		{
			get
			{
				return this.settings.ForceConcurrentRequests;
			}
		}

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x060036CA RID: 14026
		public abstract bool SupportJson { get; }

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x060036CB RID: 14027 RVA: 0x000AEB97 File Offset: 0x000ACD97
		public ODataSettingsBase SettingsBase
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x060036CC RID: 14028 RVA: 0x000AEB9F File Offset: 0x000ACD9F
		public ODataUserSettings UserSettings
		{
			get
			{
				return this.userSettings;
			}
		}

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x060036CD RID: 14029 RVA: 0x000AEBA7 File Offset: 0x000ACDA7
		// (set) Token: 0x060036CE RID: 14030 RVA: 0x000AEBB4 File Offset: 0x000ACDB4
		public int ConcurrentRequestsLimit
		{
			get
			{
				return this.settings.ConcurrentRequestsLimit;
			}
			set
			{
				this.settings.ConcurrentRequestsLimit = value;
			}
		}

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x060036CF RID: 14031 RVA: 0x000AEBC2 File Offset: 0x000ACDC2
		// (set) Token: 0x060036D0 RID: 14032 RVA: 0x000AEBCF File Offset: 0x000ACDCF
		public int PageSize
		{
			get
			{
				return this.userSettings.PageSize;
			}
			set
			{
				this.userSettings.PageSize = value;
			}
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x000AEBE0 File Offset: 0x000ACDE0
		protected ODataEnvironmentBase(EdmModelProcessorOutputBase output, Value headers, HttpResource resource, IEngineHost host, ODataSettingsBase settings, ODataUserSettings userSettings, ResourceCredentialCollection cachedCredentials)
			: base(host, "OData/FoldingWarning")
		{
			this.output = output;
			this.resource = resource;
			this.headers = headers;
			this.host = host;
			this.settings = settings;
			this.userSettings = userSettings;
			this.cachedCredentials = cachedCredentials;
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x060036D2 RID: 14034 RVA: 0x000AEC2F File Offset: 0x000ACE2F
		public IDictionary<ODataSchemaItem, TypeValue> TypeCatalog
		{
			get
			{
				return this.output.TypeCatalog;
			}
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x000AEC3C File Offset: 0x000ACE3C
		public override TableValue CreateCatalogTableValue(IEngineHost host)
		{
			Value[] array = new Value[this.TypeCatalog.Count];
			int num = 0;
			using (IEnumerator<KeyValuePair<ODataSchemaItem, TypeValue>> enumerator = this.TypeCatalog.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<ODataSchemaItem, TypeValue> kvp = enumerator.Current;
					IValueReference valueReference;
					if (kvp.Value.TypeKind == ValueKind.Function)
					{
						valueReference = this.BuildFunctionImportValue(kvp.Key.Name, kvp.Value);
					}
					else if (kvp.Value.TypeKind == ValueKind.Record)
					{
						valueReference = this.GetFeedFunctionValue(kvp.Key.Name, host);
					}
					else if (kvp.Value.TypeKind == ValueKind.Table)
					{
						valueReference = this.BuildTableForTableValueKind(kvp.Key.Name, kvp.Value);
					}
					else
					{
						if (kvp.Value.TypeKind != ValueKind.Any)
						{
							throw new InvalidOperationException();
						}
						valueReference = new DelayedValue(delegate
						{
							throw DataSourceException.NewDataSourceError<Message1>(this.Host, Strings.ODataCannotDetermineEntityType(kvp.Key.Name), this.resource.Resource, null, null);
						});
					}
					array[num++] = RecordValue.New(NavigationTableServices.ODataNavigationTableKeys, new IValueReference[]
					{
						TextValue.New(kvp.Key.Name),
						valueReference,
						TextValue.New(kvp.Key.Signature)
					});
				}
			}
			ListValue listValue = ListValue.New(array);
			TableTypeValue tableTypeValue = NavigationTableServices.AddDataColumnIsLeafMetadata(NavigationTableServices.GetODataNavigationTableType()).AsTableType;
			if (this.output.TypeMetaValue != null)
			{
				RecordValue asRecord = tableTypeValue.MetaValue.Concatenate(this.output.TypeMetaValue).AsRecord;
				tableTypeValue = tableTypeValue.NewMeta(asRecord).AsType.AsTableType;
			}
			return new ConnectionTestedTableValue(new ListTableValue(listValue, tableTypeValue));
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000AEE44 File Offset: 0x000AD044
		public void DisableConcurrentRequests()
		{
			this.settings.ConcurrentRequestsLimit = 1;
			this.userSettings.UseConcurrentRequests = false;
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x0007D355 File Offset: 0x0007B555
		public override bool TryGetRelationshipIdentity(SchemaItem schemaItem, out string relationshipIdentity)
		{
			relationshipIdentity = null;
			return false;
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x000AEE5E File Offset: 0x000AD05E
		public virtual Value BuildFunctionValue(string functionName, TypeValue typeValue, ODataEntry entry, Microsoft.OData.Edm.IEdmEntitySetBase entryEntitySet)
		{
			return QueryResultFunctionValue.CreateFunction(this, functionName, this.host, typeValue.AsFunctionType);
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x000AEE5E File Offset: 0x000AD05E
		protected virtual Value BuildFunctionImportValue(string functionName, TypeValue typeValue)
		{
			return QueryResultFunctionValue.CreateFunction(this, functionName, this.host, typeValue.AsFunctionType);
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x000AEE78 File Offset: 0x000AD078
		protected virtual Value BuildTableForTableValueKind(string tableName, TypeValue typeValue)
		{
			string text = this.FindResource(tableName);
			return new QueryResultTableValue(this, text, this.host, typeValue.AsTableType);
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x000AEEA8 File Offset: 0x000AD0A8
		protected virtual RecordValue GetFeedFunctionValue(string name, IEngineHost host)
		{
			ODataSchemaItem odataSchemaItem = new ODataSchemaItem(name, "record");
			return ODataModule.CreateGetFeedFunction(this.resource, TextValue.New(this.output.CollectionUrls[odataSchemaItem]), this.Headers, host, this.Credentials, this.settings, null).Invoke().AsRecord;
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x00002139 File Offset: 0x00000339
		public override bool SupportsSkip(TableTypeValue type)
		{
			return true;
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x00002139 File Offset: 0x00000339
		public override bool SupportsTake(TableTypeValue type)
		{
			return true;
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x000AEF00 File Offset: 0x000AD100
		protected static void UpdateMaxConcurrentRequests(Uri serviceDocumentLocation, ODataSettingsBase settings)
		{
			int num = 1;
			if (serviceDocumentLocation != null)
			{
				ServicePointManager.FindServicePoint(serviceDocumentLocation);
				num = ServicePointManager.FindServicePoint(serviceDocumentLocation).ConnectionLimit;
			}
			settings.ConcurrentRequestsLimit = Math.Min(num, settings.ConcurrentRequestsLimit);
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x000AEF40 File Offset: 0x000AD140
		private string FindResource(string resource)
		{
			ODataSchemaItem odataSchemaItem = new ODataSchemaItem(resource, "table");
			string text;
			if (this.output.CollectionUrls.TryGetValue(odataSchemaItem, out text))
			{
				return text;
			}
			return resource;
		}

		// Token: 0x04001C27 RID: 7207
		protected const int MinConcurrentRequests = 1;

		// Token: 0x04001C28 RID: 7208
		protected readonly IEngineHost host;

		// Token: 0x04001C29 RID: 7209
		protected readonly Value headers;

		// Token: 0x04001C2A RID: 7210
		protected readonly EdmModelProcessorOutputBase output;

		// Token: 0x04001C2B RID: 7211
		protected readonly ResourceCredentialCollection cachedCredentials;

		// Token: 0x04001C2C RID: 7212
		protected readonly HttpResource resource;

		// Token: 0x04001C2D RID: 7213
		private readonly ODataSettingsBase settings;

		// Token: 0x04001C2E RID: 7214
		private readonly ODataUserSettings userSettings;

		// Token: 0x04001C2F RID: 7215
		private ResourceCredentialCollection credentials;
	}
}
