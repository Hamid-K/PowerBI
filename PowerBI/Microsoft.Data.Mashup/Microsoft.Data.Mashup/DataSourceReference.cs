using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.EngineHost;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200001A RID: 26
	public sealed class DataSourceReference : IEquatable<DataSourceReference>
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00005E88 File Offset: 0x00004088
		public DataSourceReference(string json)
		{
			if (json == null)
			{
				throw new ArgumentNullException("json");
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			DataSourceReference.JsonDataSourceLocation jsonDataSourceLocation;
			try
			{
				jsonDataSourceLocation = javaScriptSerializer.Deserialize<DataSourceReference.JsonDataSourceLocation>(json);
			}
			catch (ArgumentException ex)
			{
				throw new ArgumentException(ProviderErrorStrings.DataSourceLocation_JsonFormatInner, "json", ex);
			}
			catch (InvalidOperationException ex2)
			{
				throw new ArgumentException(ProviderErrorStrings.DataSourceLocation_JsonFormatInner, "json", ex2);
			}
			if (jsonDataSourceLocation == null)
			{
				throw new ArgumentException(ProviderErrorStrings.DataSourceLocation_JsonFormat, "json");
			}
			if (string.IsNullOrEmpty(jsonDataSourceLocation.protocol))
			{
				throw new ArgumentException(ProviderErrorStrings.DataSourceLocation_Missing_Protocol, "json");
			}
			if (jsonDataSourceLocation.address == null)
			{
				throw new ArgumentException(ProviderErrorStrings.DataSourceLocation_Missing_Address, "json");
			}
			if (jsonDataSourceLocation.protocol == "x-datasource")
			{
				string text;
				if (!DataSourceReference.TryGetString(jsonDataSourceLocation.address, "kind", out text) || string.IsNullOrEmpty(text))
				{
					throw new ArgumentException(ProviderErrorStrings.DataSourceLocation_Address_Missing_Kind, "json");
				}
				string text2;
				if (!DataSourceReference.TryGetString(jsonDataSourceLocation.address, "path", out text2) || string.IsNullOrEmpty(text2))
				{
					throw new ArgumentException(ProviderErrorStrings.DataSourceLocation_Address_Missing_Path, "json");
				}
				if (!MashupEngines.Version1.TryLookupResourceKind(text, out this.resourceKindInfo))
				{
					throw new NotSupportedException(ProviderErrorStrings.UnsupportedDataSourceKind(text));
				}
				this.sourceInfo = new DataSourceReference.DataSourceInfo(text, text2, this.resourceKindInfo, null);
				return;
			}
			else
			{
				this.sourceInfo = new DataSourceReference.LocationSourceInfo(MashupEngines.Version1.NewLocation(jsonDataSourceLocation.protocol, jsonDataSourceLocation.authentication, jsonDataSourceLocation.address, jsonDataSourceLocation.query), this);
				if (!MashupEngines.Version1.TryLookupResourceKind(this.sourceInfo.NonNormalizedResource.Kind, out this.resourceKindInfo))
				{
					throw new NotSupportedException(ProviderErrorStrings.UnsupportedDataSourceKind(this.sourceInfo.NonNormalizedResource.Kind));
				}
				return;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006054 File Offset: 0x00004254
		internal DataSourceReference(IDataSourceLocation dataSourceLocation)
		{
			if (!MashupEngines.Version1.TryLookupResourceKind(dataSourceLocation.ResourceKind, out this.resourceKindInfo))
			{
				throw new NotSupportedException(ProviderErrorStrings.UnsupportedDataSourceKind(dataSourceLocation.ResourceKind));
			}
			this.sourceInfo = new DataSourceReference.LocationSourceInfo(dataSourceLocation, this);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006092 File Offset: 0x00004292
		public DataSourceReference(string kind, string path)
			: this(kind, path, null)
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000609D File Offset: 0x0000429D
		internal DataSourceReference(string kind, string path, string query)
		{
			if (!MashupEngines.Version1.TryLookupResourceKind(kind, out this.resourceKindInfo))
			{
				throw new NotSupportedException(ProviderErrorStrings.UnsupportedDataSourceKind(kind));
			}
			this.sourceInfo = new DataSourceReference.DataSourceInfo(kind, path, this.resourceKindInfo, query);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000060D8 File Offset: 0x000042D8
		private DataSourceReference(ResourceKindInfo resourceKindInfo, DataSourceReference.ISourceInfo sourceInfo)
		{
			this.resourceKindInfo = resourceKindInfo;
			this.sourceInfo = sourceInfo;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000060EE File Offset: 0x000042EE
		public DataSource DataSource
		{
			get
			{
				return new DataSource(this.sourceInfo.NonNormalizedResource);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00006100 File Offset: 0x00004300
		public IEnumerable<AddressPart> Address
		{
			get
			{
				return this.sourceInfo.Address;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000610D File Offset: 0x0000430D
		public string Authentication
		{
			get
			{
				if (this.Location != null)
				{
					return this.Location.Authentication;
				}
				return null;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006124 File Offset: 0x00004324
		public string Query
		{
			get
			{
				return this.sourceInfo.Query;
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006131 File Offset: 0x00004331
		public bool TryGetHostName(out string hostName)
		{
			return this.resourceKindInfo.TryGetHostName(this.NonNormalizedResource.NonNormalizedPath, out hostName);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000614A File Offset: 0x0000434A
		public string ToJson()
		{
			return this.sourceInfo.ToJson();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006157 File Offset: 0x00004357
		public string ToJson(DataSourceReferenceFormat dataSourceReferenceFormat)
		{
			return this.sourceInfo.ToJson((DataSourceLocationFormat)dataSourceReferenceFormat);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006165 File Offset: 0x00004365
		public bool Equals(DataSourceReference other)
		{
			return this.sourceInfo.Equals(other);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006173 File Offset: 0x00004373
		internal IResource NonNormalizedResource
		{
			get
			{
				return this.sourceInfo.NonNormalizedResource;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00006180 File Offset: 0x00004380
		internal IDataSourceLocation Location
		{
			get
			{
				return this.sourceInfo.Location;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000618D File Offset: 0x0000438D
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataSourceReference);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000619B File Offset: 0x0000439B
		public override int GetHashCode()
		{
			return this.sourceInfo.GetHashCode();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000061A8 File Offset: 0x000043A8
		public override string ToString()
		{
			return this.ToJson();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000061B0 File Offset: 0x000043B0
		public void TestConnection(DataSourceSetting dataSourceSetting)
		{
			this.TestConnection(dataSourceSetting, null, null);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000061BB File Offset: 0x000043BB
		public void TestConnection(DataSourceSetting dataSourceSetting, MashupConnectionStringBuilder builder)
		{
			this.TestConnection(dataSourceSetting, null, builder);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000061C6 File Offset: 0x000043C6
		public void TestConnection(DataSourceSetting dataSourceSetting, string options)
		{
			this.TestConnection(dataSourceSetting, options, null);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000061D4 File Offset: 0x000043D4
		public void TestConnection(DataSourceSetting dataSourceSetting, string options, MashupConnectionStringBuilder builder)
		{
			if (dataSourceSetting == null)
			{
				throw new ArgumentNullException("dataSourceSetting");
			}
			MashupConnectionStringBuilder mashupConnectionStringBuilder = new MashupConnectionStringBuilder();
			if (builder != null)
			{
				mashupConnectionStringBuilder.ConnectionString = builder.ConnectionString;
			}
			mashupConnectionStringBuilder.Package = MashupPackage.ToBase64String("Query1", this.GetTestConnectionFormulaText(options));
			mashupConnectionStringBuilder.Mashup = null;
			mashupConnectionStringBuilder.Location = "Query1";
			Dictionary<DataSource, DataSourceSetting> dictionary = DataSourceSettings.ToDictionary(mashupConnectionStringBuilder.DataSourceSettings);
			dictionary[this.DataSource] = dataSourceSetting;
			mashupConnectionStringBuilder.DataSourceSettings = DataSourceSettings.Create(dictionary);
			this.TestConnection(mashupConnectionStringBuilder.ToString());
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006260 File Offset: 0x00004460
		private void TestConnection(string connectionString)
		{
			using (MashupConnection mashupConnection = new MashupConnection(connectionString))
			{
				mashupConnection.OptionalModules.Add("DataSource");
				mashupConnection.Open();
				using (MashupCommand mashupCommand = mashupConnection.CreateCommand())
				{
					mashupCommand.CommandText = "Query1";
					mashupCommand.CommandType = CommandType.TableDirect;
					using (MashupReader mashupReader = mashupCommand.ExecuteReader())
					{
						while (mashupReader.Read())
						{
						}
					}
				}
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00006300 File Offset: 0x00004500
		internal ResourceKindInfo ResourceKindInfo
		{
			get
			{
				return this.resourceKindInfo;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006308 File Offset: 0x00004508
		internal string GetFormulaText(string optionsJson = null)
		{
			return this.sourceInfo.GetFormulaText(optionsJson);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006316 File Offset: 0x00004516
		internal string GetTestConnectionFormulaText(string optionsJson)
		{
			return string.Format(CultureInfo.InvariantCulture, "DataSource.TestConnection({0})", this.GetFormulaText(optionsJson));
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000632E File Offset: 0x0000452E
		internal DataSourceReference AddQuery(string query)
		{
			return new DataSourceReference(this.resourceKindInfo, this.sourceInfo.AddQuery(query));
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006347 File Offset: 0x00004547
		internal DataSourceReference RemoveQuery()
		{
			return new DataSourceReference(this.resourceKindInfo, this.sourceInfo.RemoveQuery());
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006360 File Offset: 0x00004560
		private static bool TryGetString(IDictionary<string, object> address, string key, out string value)
		{
			object obj;
			if (address.TryGetValue(key, out obj))
			{
				value = obj as string;
			}
			else
			{
				value = null;
			}
			return value != null;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000638C File Offset: 0x0000458C
		private static IEnumerable<AddressPart> GetAddress(ResourceKindInfo info, string resourcePath)
		{
			IEnumerable<KeyValuePair<string, string>> partLabels = info.GetPartLabels(resourcePath);
			if (partLabels != null)
			{
				return partLabels.Select((KeyValuePair<string, string> kvp) => new AddressPart(kvp.Key, kvp.Key, kvp.Value));
			}
			IResource resource;
			string text;
			if (!info.Validate(resourcePath, out resource, out text))
			{
				throw new NotSupportedException(text);
			}
			IDataSourceLocation location = null;
			if (info.DslFactories != null && info.DslFactories.Any((IDataSourceLocationFactory dslf) => dslf.TryCreateFromResource(resource, false, out location)))
			{
				return DataSourceReference.GetAddressParts(location, location.Address, EmptyArray<string>.Instance, EmptyArray<string>.Instance);
			}
			throw new NotSupportedException(ProviderErrorStrings.UnsupportedDataSourceKind(info.Kind));
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006441 File Offset: 0x00004641
		private static IEnumerable<AddressPart> GetAddressParts(IDataSourceLocation location, object value, IEnumerable<string> names, IEnumerable<string> labels)
		{
			string text = value as string;
			if (text != null)
			{
				yield return new AddressPart(string.Join(".", names.ToArray<string>()), string.Join(" ", labels.ToArray<string>()), text);
				yield break;
			}
			IDictionary<string, object> dict = value as IDictionary<string, object>;
			if (dict != null)
			{
				IEnumerable<string> enumerable;
				if (!names.Any<string>())
				{
					enumerable = location.DisplayAddressFields;
				}
				else
				{
					IEnumerable<string> keys = dict.Keys;
					enumerable = keys;
				}
				foreach (string text2 in enumerable)
				{
					object obj;
					if (dict.TryGetValue(text2, out obj))
					{
						foreach (AddressPart addressPart in DataSourceReference.GetAddressParts(location, obj, names.ConcatSingle(text2), labels.ConcatSingle(location.GetAddressFieldLabel(text2))))
						{
							yield return addressPart;
						}
						IEnumerator<AddressPart> enumerator2 = null;
					}
				}
				IEnumerator<string> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0400009A RID: 154
		private const string kindKey = "kind";

		// Token: 0x0400009B RID: 155
		private const string pathKey = "path";

		// Token: 0x0400009C RID: 156
		private const string testConnectionFormat = "DataSource.TestConnection({0})";

		// Token: 0x0400009D RID: 157
		private const string queryName = "Query1";

		// Token: 0x0400009E RID: 158
		private readonly DataSourceReference.ISourceInfo sourceInfo;

		// Token: 0x0400009F RID: 159
		private readonly ResourceKindInfo resourceKindInfo;

		// Token: 0x0200006C RID: 108
		private class JsonDataSourceLocation
		{
			// Token: 0x04000230 RID: 560
			public string protocol;

			// Token: 0x04000231 RID: 561
			public Dictionary<string, object> address;

			// Token: 0x04000232 RID: 562
			public string authentication;

			// Token: 0x04000233 RID: 563
			public string query;
		}

		// Token: 0x0200006D RID: 109
		private interface ISourceInfo
		{
			// Token: 0x17000126 RID: 294
			// (get) Token: 0x06000472 RID: 1138
			IResource NonNormalizedResource { get; }

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x06000473 RID: 1139
			IEnumerable<AddressPart> Address { get; }

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x06000474 RID: 1140
			string Query { get; }

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x06000475 RID: 1141
			IDataSourceLocation Location { get; }

			// Token: 0x06000476 RID: 1142
			string GetFormulaText(string optionsJson);

			// Token: 0x06000477 RID: 1143
			string ToJson();

			// Token: 0x06000478 RID: 1144
			string ToJson(DataSourceLocationFormat dataSourceLocationFormat);

			// Token: 0x06000479 RID: 1145
			bool Equals(DataSourceReference other);

			// Token: 0x0600047A RID: 1146
			int GetHashCode();

			// Token: 0x0600047B RID: 1147
			DataSourceReference.ISourceInfo AddQuery(string query);

			// Token: 0x0600047C RID: 1148
			DataSourceReference.ISourceInfo RemoveQuery();
		}

		// Token: 0x0200006E RID: 110
		private class DataSourceInfo : DataSourceReference.ISourceInfo
		{
			// Token: 0x0600047D RID: 1149 RVA: 0x00010B64 File Offset: 0x0000ED64
			public DataSourceInfo(string kind, string path, ResourceKindInfo info, string query = null)
			{
				this.kind = kind;
				this.path = path;
				this.info = info;
				this.query = query;
				string text;
				if (!this.info.Validate(path, out this.resource, out text))
				{
					throw new MashupException(text);
				}
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x0600047E RID: 1150 RVA: 0x00010BB1 File Offset: 0x0000EDB1
			public IResource NonNormalizedResource
			{
				get
				{
					return this.resource;
				}
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x0600047F RID: 1151 RVA: 0x00010BB9 File Offset: 0x0000EDB9
			public IEnumerable<AddressPart> Address
			{
				get
				{
					return DataSourceReference.GetAddress(this.info, this.path);
				}
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000480 RID: 1152 RVA: 0x00010BCC File Offset: 0x0000EDCC
			public string Query
			{
				get
				{
					return this.query;
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x06000481 RID: 1153 RVA: 0x00010BD4 File Offset: 0x0000EDD4
			public IDataSourceLocation Location
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06000482 RID: 1154 RVA: 0x00010BD7 File Offset: 0x0000EDD7
			public string GetFormulaText(string optionsJson)
			{
				if (optionsJson != null)
				{
					throw new NotSupportedException();
				}
				string text = this.info.CreateTestFormula(this.path);
				if (text == null)
				{
					throw new NotSupportedException(ProviderErrorStrings.UnsupportedDataSourceKind(this.kind));
				}
				return text;
			}

			// Token: 0x06000483 RID: 1155 RVA: 0x00010C08 File Offset: 0x0000EE08
			public string ToJson()
			{
				return new JavaScriptSerializer().Serialize(new DataSourceReference.JsonDataSourceLocation
				{
					protocol = "x-datasource",
					address = new Dictionary<string, object>
					{
						{ "kind", this.kind },
						{ "path", this.path }
					},
					query = this.query
				});
			}

			// Token: 0x06000484 RID: 1156 RVA: 0x00010C68 File Offset: 0x0000EE68
			public string ToJson(DataSourceLocationFormat dataSourceLocationFormat)
			{
				if (dataSourceLocationFormat != DataSourceLocationFormat.Default && dataSourceLocationFormat == DataSourceLocationFormat.SortAddressFields)
				{
					return new JavaScriptSerializer().Serialize(new OrderedDictionary
					{
						{ "protocol", "x-datasource" },
						{
							"address",
							new SortedDictionary<string, object>
							{
								{ "kind", this.kind },
								{ "path", this.path }
							}
						},
						{ "query", this.query }
					});
				}
				return this.ToJson();
			}

			// Token: 0x06000485 RID: 1157 RVA: 0x00010CE5 File Offset: 0x0000EEE5
			public DataSourceReference.ISourceInfo AddQuery(string query)
			{
				if (this.query != null || query == null)
				{
					return this;
				}
				return new DataSourceReference.DataSourceInfo(this.kind, this.path, this.info, query);
			}

			// Token: 0x06000486 RID: 1158 RVA: 0x00010D0C File Offset: 0x0000EF0C
			public DataSourceReference.ISourceInfo RemoveQuery()
			{
				if (this.query == null)
				{
					return this;
				}
				return new DataSourceReference.DataSourceInfo(this.kind, this.path, this.info, null);
			}

			// Token: 0x06000487 RID: 1159 RVA: 0x00010D30 File Offset: 0x0000EF30
			public bool Equals(DataSourceReference other)
			{
				if (other == null)
				{
					return false;
				}
				DataSourceReference.DataSourceInfo dataSourceInfo = other.sourceInfo as DataSourceReference.DataSourceInfo;
				return dataSourceInfo != null && this.kind == dataSourceInfo.kind && this.resource.Path == dataSourceInfo.resource.Path && ((this.query == null && dataSourceInfo.query == null) || (this.query != null && dataSourceInfo.query != null && this.query == dataSourceInfo.query));
			}

			// Token: 0x06000488 RID: 1160 RVA: 0x00010DB8 File Offset: 0x0000EFB8
			public override int GetHashCode()
			{
				return this.kind.GetHashCode() ^ this.resource.Path.GetHashCode();
			}

			// Token: 0x04000234 RID: 564
			private readonly string kind;

			// Token: 0x04000235 RID: 565
			private readonly string path;

			// Token: 0x04000236 RID: 566
			private readonly IResource resource;

			// Token: 0x04000237 RID: 567
			private readonly ResourceKindInfo info;

			// Token: 0x04000238 RID: 568
			private readonly string query;
		}

		// Token: 0x0200006F RID: 111
		private class LocationSourceInfo : DataSourceReference.ISourceInfo
		{
			// Token: 0x06000489 RID: 1161 RVA: 0x00010DD8 File Offset: 0x0000EFD8
			public LocationSourceInfo(IDataSourceLocation location, DataSourceReference dataSourceReference)
			{
				this.location = location;
				this.dataSourceReference = dataSourceReference;
				try
				{
					if (!location.TryGetResource(out this.resource))
					{
						throw new NotSupportedException(ProviderErrorStrings.DataSourceReferenceNoDataSource)
						{
							Data = { 
							{
								"DSR",
								location.ToJson()
							} }
						};
					}
				}
				catch (ValueException2 valueException)
				{
					throw new MashupException(valueException.Message, valueException);
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x0600048A RID: 1162 RVA: 0x00010E48 File Offset: 0x0000F048
			public IResource NonNormalizedResource
			{
				get
				{
					return this.resource;
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x0600048B RID: 1163 RVA: 0x00010E50 File Offset: 0x0000F050
			public string Query
			{
				get
				{
					return this.location.Query;
				}
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x0600048C RID: 1164 RVA: 0x00010E5D File Offset: 0x0000F05D
			public IDataSourceLocation Location
			{
				get
				{
					return this.location;
				}
			}

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x0600048D RID: 1165 RVA: 0x00010E68 File Offset: 0x0000F068
			public IEnumerable<AddressPart> Address
			{
				get
				{
					if (this.location.Protocol == "x-datasource")
					{
						return DataSourceReference.GetAddress(this.dataSourceReference.ResourceKindInfo, this.resource.NonNormalizedPath);
					}
					return this.GetAddressParts(this.location.Address, new string[0], new string[0]);
				}
			}

			// Token: 0x0600048E RID: 1166 RVA: 0x00010EC8 File Offset: 0x0000F0C8
			public string GetFormulaText(string optionsJson)
			{
				IFormulaCreationResult formulaCreationResult = this.location.CreateFormula(optionsJson);
				if (!formulaCreationResult.Success)
				{
					throw new NotSupportedException(ProviderErrorStrings.DataSourceReferenceNoGeneratedCode(formulaCreationResult.ErrorMessage ?? formulaCreationResult.FailureReason));
				}
				return formulaCreationResult.Formula;
			}

			// Token: 0x0600048F RID: 1167 RVA: 0x00010F10 File Offset: 0x0000F110
			public string ToJson()
			{
				return this.location.ToJson();
			}

			// Token: 0x06000490 RID: 1168 RVA: 0x00010F1D File Offset: 0x0000F11D
			public string ToJson(DataSourceLocationFormat dataSourceLocationFormat)
			{
				return this.location.ToJson(dataSourceLocationFormat);
			}

			// Token: 0x06000491 RID: 1169 RVA: 0x00010F2C File Offset: 0x0000F12C
			public DataSourceReference.ISourceInfo AddQuery(string query)
			{
				if (this.Query != null || query == null)
				{
					return this;
				}
				return new DataSourceReference.LocationSourceInfo(MashupEngines.Version1.NewLocation(this.location.Protocol, this.location.Authentication, this.location.Address, query), this.dataSourceReference);
			}

			// Token: 0x06000492 RID: 1170 RVA: 0x00010F80 File Offset: 0x0000F180
			public DataSourceReference.ISourceInfo RemoveQuery()
			{
				if (this.Query == null)
				{
					return this;
				}
				return new DataSourceReference.LocationSourceInfo(MashupEngines.Version1.NewLocation(this.location.Protocol, this.location.Authentication, this.location.Address, null), this.dataSourceReference);
			}

			// Token: 0x06000493 RID: 1171 RVA: 0x00010FD0 File Offset: 0x0000F1D0
			public bool Equals(DataSourceReference other)
			{
				if (other == null)
				{
					return false;
				}
				DataSourceReference.LocationSourceInfo locationSourceInfo = other.sourceInfo as DataSourceReference.LocationSourceInfo;
				return locationSourceInfo != null && this.location.Equals(locationSourceInfo.location);
			}

			// Token: 0x06000494 RID: 1172 RVA: 0x00011004 File Offset: 0x0000F204
			public override int GetHashCode()
			{
				return this.location.GetHashCode();
			}

			// Token: 0x06000495 RID: 1173 RVA: 0x00011011 File Offset: 0x0000F211
			private IEnumerable<AddressPart> GetAddressParts(object value, IEnumerable<string> names, IEnumerable<string> labels)
			{
				return DataSourceReference.GetAddressParts(this.location, value, names, labels);
			}

			// Token: 0x04000239 RID: 569
			private readonly IDataSourceLocation location;

			// Token: 0x0400023A RID: 570
			private readonly DataSourceReference dataSourceReference;

			// Token: 0x0400023B RID: 571
			private readonly IResource resource;
		}
	}
}
