using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x0200037C RID: 892
	internal class OpenApiDocument : OpenApiSpecObject
	{
		// Token: 0x06001F6E RID: 8046 RVA: 0x000518B8 File Offset: 0x0004FAB8
		public OpenApiDocument(RecordValue rawDocument, OpenApiUserSettings userSettings, IEngineHost engineHost)
			: base(rawDocument ?? RecordValue.Empty, userSettings)
		{
			this.engineHost = engineHost;
			this.referenceSchema = new Dictionary<TextValue, OpenApiSchema>();
			this.referenceResponse = new Dictionary<TextValue, OpenApiResponseDefinition>();
			this.referencePathItem = new Dictionary<TextValue, OpenApiPathItem>();
			this.referenceParameters = new Dictionary<TextValue, OpenApiParameterDefinition>();
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x00051909 File Offset: 0x0004FB09
		public IEngineHost EngineHost
		{
			get
			{
				return this.engineHost;
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x00051911 File Offset: 0x0004FB11
		public IResource Resource
		{
			get
			{
				return Microsoft.Mashup.Engine1.Library.Resource.New("Web", this.ServiceUrl.OriginalString);
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x00051928 File Offset: 0x0004FB28
		public Uri ServiceUrl
		{
			get
			{
				if (this.serviceUrl == null)
				{
					UriBuilder uriBuilder = new UriBuilder(this.Host);
					uriBuilder.Scheme = this.Schemes[0];
					if (this.Schemes.Count > 1)
					{
						uriBuilder.Scheme = (this.Schemes.Contains("https") ? "https" : this.Schemes[0]);
					}
					uriBuilder.Path = this.BasePath;
					string[] array = this.Host.Split(new char[] { ':' });
					uriBuilder.Host = array[0];
					int num;
					if (array.Length > 1 && int.TryParse(array[1], out num))
					{
						uriBuilder.Port = num;
					}
					else
					{
						uriBuilder.Port = -1;
					}
					this.serviceUrl = uriBuilder.Uri;
				}
				return this.serviceUrl;
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06001F72 RID: 8050 RVA: 0x000519FD File Offset: 0x0004FBFD
		public Value Swagger
		{
			get
			{
				if (this.swagger == null && !base.RawObject.TryGetValue("swagger", out this.swagger))
				{
					this.swagger = Value.Null;
				}
				return this.swagger;
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x00051A30 File Offset: 0x0004FC30
		public Value Info
		{
			get
			{
				if (this.info == null && !base.RawObject.TryGetValue("info", out this.info))
				{
					this.info = Value.Null;
				}
				return this.info;
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x00051A64 File Offset: 0x0004FC64
		public string Host
		{
			get
			{
				if (this.host == null)
				{
					Value value;
					if (!base.RawObject.TryGetValue("host", out value))
					{
						throw ValueException.NewDataSourceError<Message0>(Strings.OpenApiHostNotDefined, Value.Null, null);
					}
					this.host = value.AsString;
				}
				return this.host;
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x00051AB0 File Offset: 0x0004FCB0
		public string BasePath
		{
			get
			{
				if (this.basePath == null)
				{
					Value value;
					this.basePath = (base.RawObject.TryGetValue("basePath", out value) ? value.AsString : string.Empty);
				}
				return this.basePath;
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x00051AF4 File Offset: 0x0004FCF4
		public IList<string> Schemes
		{
			get
			{
				if (this.schemes == null)
				{
					this.schemes = new List<string>();
					Value empty;
					if (!base.RawObject.TryGetValue("schemes", out empty))
					{
						empty = ListValue.Empty;
					}
					this.AppendToSchemeList(this.schemes, empty.AsList);
				}
				return this.schemes;
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x00051B48 File Offset: 0x0004FD48
		public IList<string> Produces
		{
			get
			{
				if (this.produces == null)
				{
					this.produces = new List<string>();
					Value value;
					if (base.RawObject.TryGetValue("produces", out value))
					{
						OpenApiHelper.AppendToMimeList(this.produces, value, this.EngineHost);
					}
				}
				return this.produces;
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x00051B94 File Offset: 0x0004FD94
		public IDictionary<string, OpenApiPathItem> Paths
		{
			get
			{
				if (this.paths == null)
				{
					this.paths = new Dictionary<string, OpenApiPathItem>();
					Value value;
					if (base.RawObject.TryGetValue("paths", out value))
					{
						this.AddRecordValuesToMap<OpenApiPathItem>(this.paths, value.AsRecord, (RecordValue r) => this.GetOrCreatePathItem(r));
					}
				}
				return this.paths;
			}
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x00051BEC File Offset: 0x0004FDEC
		public IDictionary<string, OpenApiSecurityDefinition> SecurityDefinitions
		{
			get
			{
				if (this.securityDefinitions == null)
				{
					this.securityDefinitions = new Dictionary<string, OpenApiSecurityDefinition>();
					Value value;
					if (base.RawObject.TryGetValue("securityDefinitions", out value))
					{
						this.AddRecordValuesToMap<OpenApiSecurityDefinition>(this.securityDefinitions, value.AsRecord, (RecordValue r) => new OpenApiSecurityDefinition(r, base.UserSettings));
					}
				}
				return this.securityDefinitions;
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x00051C44 File Offset: 0x0004FE44
		public IList<IDictionary<string, IEnumerable<string>>> Security
		{
			get
			{
				if (this.security == null)
				{
					this.security = new List<IDictionary<string, IEnumerable<string>>>();
					Value value;
					if (base.RawObject.TryGetValue("security", out value))
					{
						OpenApiHelper.AppendToSecurityList(this.security, value.AsList, this.EngineHost);
					}
				}
				return this.security;
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x00051C95 File Offset: 0x0004FE95
		public Value Tags
		{
			get
			{
				if (this.tags == null && !base.RawObject.TryGetValue("tags", out this.tags))
				{
					this.tags = Value.Null;
				}
				return this.tags;
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x00051CC8 File Offset: 0x0004FEC8
		public Value ExternalDocs
		{
			get
			{
				if (this.externalDocs == null && !base.RawObject.TryGetValue("externalDocs", out this.externalDocs))
				{
					this.externalDocs = Value.Null;
				}
				return this.externalDocs;
			}
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x00051CFC File Offset: 0x0004FEFC
		public static bool TryGetReferenceUrl(RecordValue refRecord, out TextValue url)
		{
			Value value;
			if (!refRecord.TryGetValue("$ref", out value))
			{
				url = TextValue.Empty;
				return false;
			}
			if (value.IsText)
			{
				url = value.AsText;
				return true;
			}
			throw ValueException.NewDataSourceError<Message1>(Strings.OpenApiReferenceNotTextType(value), refRecord, null);
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x00051D40 File Offset: 0x0004FF40
		public OpenApiSchema GetOrCreateSchema(RecordValue schemaRecord)
		{
			return this.GetOrCreate<OpenApiSchema>(this.referenceSchema, (RecordValue r) => new OpenApiSchema(r, this), delegate(TextValue t, out Value o)
			{
				return this.TryGetUrlValue(t, "definitions", out o);
			}, schemaRecord);
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x00051D67 File Offset: 0x0004FF67
		public OpenApiResponseDefinition GetOrCreateResponse(RecordValue responseRecord)
		{
			return this.GetOrCreate<OpenApiResponseDefinition>(this.referenceResponse, (RecordValue r) => new OpenApiResponseDefinition(r, this), delegate(TextValue t, out Value o)
			{
				return this.TryGetUrlValue(t, "responses", out o);
			}, responseRecord);
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x00051D8E File Offset: 0x0004FF8E
		public OpenApiParameterDefinition GetOrCreateParameter(RecordValue parameterRecord)
		{
			return this.GetOrCreate<OpenApiParameterDefinition>(this.referenceParameters, (RecordValue r) => new OpenApiParameterDefinition(r, this), delegate(TextValue t, out Value o)
			{
				return this.TryGetUrlValue(t, "parameters", out o);
			}, parameterRecord);
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x00051DB5 File Offset: 0x0004FFB5
		public OpenApiPathItem GetOrCreatePathItem(RecordValue pathItemRecord)
		{
			return this.GetOrCreate<OpenApiPathItem>(this.referencePathItem, (RecordValue r) => new OpenApiPathItem(r, this), delegate(TextValue t, out Value o)
			{
				return this.TryGetUrlValue(t, out o);
			}, pathItemRecord);
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x00051DDC File Offset: 0x0004FFDC
		protected override List<RecordKeyDefinition> GetMetaDataRecords()
		{
			List<RecordKeyDefinition> metaDataRecords = base.GetMetaDataRecords();
			if (this.Swagger != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("swagger", this.Swagger, TypeValue.Any));
			}
			if (this.Info != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("info", this.Info, TypeValue.Any));
			}
			if (this.Tags != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("tags", this.Tags, TypeValue.Any));
			}
			if (this.ExternalDocs != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("externalDocs", this.ExternalDocs, TypeValue.Any));
			}
			return metaDataRecords;
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x00051E94 File Offset: 0x00050094
		private bool TryGetUrlValue(TextValue url, string level1Name, out Value value)
		{
			string[] array = url.ToString().Split(new char[] { '/' });
			if (array.Length != 3 || array[0] != "#" || array[1] != level1Name)
			{
				value = null;
				return false;
			}
			return this.TryGetUrlValue(array, out value);
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x00051EE8 File Offset: 0x000500E8
		private bool TryGetUrlValue(TextValue url, out Value value)
		{
			string[] array = url.ToString().Split(new char[] { '/' });
			if (array.Length <= 1 || array[0] != "#")
			{
				value = null;
				return false;
			}
			return this.TryGetUrlValue(array, out value);
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x00051F30 File Offset: 0x00050130
		private bool TryGetUrlValue(string[] urlParts, out Value value)
		{
			Value value2 = base.RawObject;
			int num = 1;
			while (num < urlParts.Length && value2 != null)
			{
				Value value3;
				if (value2.TryGetValue(urlParts[num], out value3))
				{
					value2 = value3;
				}
				else
				{
					value2 = null;
				}
				num++;
			}
			value = value2;
			return value != null;
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x00051F70 File Offset: 0x00050170
		private void AppendToSchemeList(IList<string> schemeList, ListValue schemesListValue)
		{
			foreach (string text in OpenApiHelper.ToStringList(schemesListValue, this.EngineHost))
			{
				schemeList.Add(text);
			}
			if (schemeList.Count == 0)
			{
				schemeList.Add("http");
			}
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x00051FD8 File Offset: 0x000501D8
		private void AddRecordValuesToMap<T>(IDictionary<string, T> map, RecordValue record, Func<RecordValue, T> createT)
		{
			foreach (NamedValue namedValue in record.GetFields())
			{
				map.Add(namedValue.Key, createT(namedValue.Value.AsRecord));
			}
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00052048 File Offset: 0x00050248
		private T GetOrCreate<T>(IDictionary<TextValue, T> dictionary, Func<RecordValue, T> createT, OpenApiDocument.TryGet<TextValue, Value> tryGetUrlValue, RecordValue rawRecord) where T : class
		{
			TextValue textValue;
			if (!OpenApiDocument.TryGetReferenceUrl(rawRecord, out textValue))
			{
				return createT(rawRecord);
			}
			Value rawValue;
			T t;
			if (dictionary.TryGetValue(textValue, out t))
			{
				if (t != null)
				{
					return t;
				}
			}
			else if (tryGetUrlValue(textValue, out rawValue) && this.CreateAndCacheToDictionary<T>(dictionary, textValue, () => this.GetOrCreate<T>(dictionary, createT, tryGetUrlValue, rawValue.AsRecord), out t))
			{
				return t;
			}
			throw ValueException.NewDataSourceError<Message1>(Strings.OpenApiReferenceDefinitionNotFound(textValue), rawRecord, null);
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x000520EC File Offset: 0x000502EC
		private bool CreateAndCacheToDictionary<T>(IDictionary<TextValue, T> dictionary, TextValue key, Func<T> createValue, out T value) where T : class
		{
			dictionary.Add(key, default(T));
			value = createValue();
			if (value != null)
			{
				dictionary[key] = value;
			}
			return value != null;
		}

		// Token: 0x04000BD0 RID: 3024
		private readonly IEngineHost engineHost;

		// Token: 0x04000BD1 RID: 3025
		private readonly IDictionary<TextValue, OpenApiSchema> referenceSchema;

		// Token: 0x04000BD2 RID: 3026
		private readonly IDictionary<TextValue, OpenApiResponseDefinition> referenceResponse;

		// Token: 0x04000BD3 RID: 3027
		private readonly IDictionary<TextValue, OpenApiPathItem> referencePathItem;

		// Token: 0x04000BD4 RID: 3028
		private readonly IDictionary<TextValue, OpenApiParameterDefinition> referenceParameters;

		// Token: 0x04000BD5 RID: 3029
		private Value swagger;

		// Token: 0x04000BD6 RID: 3030
		private Value info;

		// Token: 0x04000BD7 RID: 3031
		private Uri serviceUrl;

		// Token: 0x04000BD8 RID: 3032
		private string host;

		// Token: 0x04000BD9 RID: 3033
		private string basePath;

		// Token: 0x04000BDA RID: 3034
		private IList<string> schemes;

		// Token: 0x04000BDB RID: 3035
		private IList<string> produces;

		// Token: 0x04000BDC RID: 3036
		private IDictionary<string, OpenApiPathItem> paths;

		// Token: 0x04000BDD RID: 3037
		private IDictionary<string, OpenApiSecurityDefinition> securityDefinitions;

		// Token: 0x04000BDE RID: 3038
		private IList<IDictionary<string, IEnumerable<string>>> security;

		// Token: 0x04000BDF RID: 3039
		private Value tags;

		// Token: 0x04000BE0 RID: 3040
		private Value externalDocs;

		// Token: 0x0200037D RID: 893
		// (Invoke) Token: 0x06001F95 RID: 8085
		private delegate bool TryGet<T, U>(T input, out U output);
	}
}
