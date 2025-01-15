using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Shims.Json;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018BC RID: 6332
	internal abstract class DataSourceLocation : IDataSourceLocation, IEquatable<IDataSourceLocation>, IComparable<IDataSourceLocation>
	{
		// Token: 0x0600A14E RID: 41294 RVA: 0x00217470 File Offset: 0x00215670
		protected DataSourceLocation()
		{
			this.address = new Dictionary<string, object>();
		}

		// Token: 0x17002944 RID: 10564
		// (get) Token: 0x0600A14F RID: 41295 RVA: 0x00217483 File Offset: 0x00215683
		// (set) Token: 0x0600A150 RID: 41296 RVA: 0x0021748B File Offset: 0x0021568B
		public string Protocol { get; set; }

		// Token: 0x17002945 RID: 10565
		// (get) Token: 0x0600A151 RID: 41297 RVA: 0x00217494 File Offset: 0x00215694
		// (set) Token: 0x0600A152 RID: 41298 RVA: 0x0021749C File Offset: 0x0021569C
		public string Authentication { get; set; }

		// Token: 0x17002946 RID: 10566
		// (get) Token: 0x0600A153 RID: 41299 RVA: 0x002174A5 File Offset: 0x002156A5
		// (set) Token: 0x0600A154 RID: 41300 RVA: 0x002174AD File Offset: 0x002156AD
		public string Query { get; set; }

		// Token: 0x17002947 RID: 10567
		// (get) Token: 0x0600A155 RID: 41301 RVA: 0x002174B6 File Offset: 0x002156B6
		// (set) Token: 0x0600A156 RID: 41302 RVA: 0x002174BE File Offset: 0x002156BE
		public IDictionary<string, object> Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.AddIfNotNull(value);
			}
		}

		// Token: 0x17002948 RID: 10568
		// (get) Token: 0x0600A157 RID: 41303 RVA: 0x0007E4B5 File Offset: 0x0007C6B5
		public virtual string ResourceKind
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17002949 RID: 10569
		// (get) Token: 0x0600A158 RID: 41304 RVA: 0x002174C7 File Offset: 0x002156C7
		public virtual string FriendlyName
		{
			get
			{
				return this.Protocol;
			}
		}

		// Token: 0x1700294A RID: 10570
		// (get) Token: 0x0600A159 RID: 41305 RVA: 0x002174D0 File Offset: 0x002156D0
		public virtual IEnumerable<string> DisplayAddressFields
		{
			get
			{
				return new string[]
				{
					"server", "database", "schema", "object", "model", "options", "account", "domain", "container", "prefix",
					"name", "path", "url", "resource", "objectId", "objectType", "emailAddress", "loginServer", "class", "provider",
					"itemName", "contentType"
				};
			}
		}

		// Token: 0x0600A15A RID: 41306 RVA: 0x002175A4 File Offset: 0x002157A4
		public virtual void Normalize()
		{
			object obj;
			if (this.Address.TryGetValue("query", out obj))
			{
				this.Query = obj as string;
				this.Address.Remove("query");
			}
		}

		// Token: 0x0600A15B RID: 41307 RVA: 0x002175E2 File Offset: 0x002157E2
		public virtual bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			resolvedLocation = this.Clone();
			return true;
		}

		// Token: 0x0600A15C RID: 41308
		public abstract bool TryGetResource(out IResource resource);

		// Token: 0x1700294B RID: 10571
		// (get) Token: 0x0600A15D RID: 41309 RVA: 0x002175ED File Offset: 0x002157ED
		private string ComparableRepresentation
		{
			get
			{
				if (this.comparableRepresentation == null)
				{
					this.comparableRepresentation = this.ComparableRepresentation();
				}
				return this.comparableRepresentation;
			}
		}

		// Token: 0x0600A15E RID: 41310 RVA: 0x00217609 File Offset: 0x00215809
		public string ToJson()
		{
			return Json.SerializeObject(new DataSourceLocation.JsonDataSourceLocation
			{
				protocol = this.Protocol,
				authentication = this.Authentication,
				address = this.address,
				query = this.Query
			});
		}

		// Token: 0x0600A15F RID: 41311 RVA: 0x00217648 File Offset: 0x00215848
		public string ToJson(DataSourceLocationFormat dataSourceLocationFormat)
		{
			if (dataSourceLocationFormat != DataSourceLocationFormat.Default && dataSourceLocationFormat == DataSourceLocationFormat.SortAddressFields)
			{
				SortedDictionary<string, object> sortedDictionary = new SortedDictionary<string, object>();
				if (this.address != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair in this.address)
					{
						sortedDictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				return Json.SerializeObject(new OrderedDictionary
				{
					{ "protocol", this.Protocol },
					{ "authentication", this.Authentication },
					{ "address", sortedDictionary },
					{ "query", this.Query }
				});
			}
			return this.ToJson();
		}

		// Token: 0x0600A160 RID: 41312
		public abstract IFormulaCreationResult CreateFormula(string optionsJson);

		// Token: 0x0600A161 RID: 41313 RVA: 0x00217714 File Offset: 0x00215914
		public IFormulaCreationResult CreateFormula(bool validateAuthentication = false)
		{
			ResourceKindInfo resourceKindInfo;
			if (validateAuthentication && this.Authentication != null && ((IEngine)Engine.Instance).TryLookupResourceKind(this.ResourceKind, out resourceKindInfo))
			{
				AuthenticationKind? authenticationKind = DataSourceLocation.ToAuthenticationType(this.Authentication);
				if (authenticationKind != null)
				{
					if (resourceKindInfo.AuthenticationInfo.Select((AuthenticationInfo auth) => auth.AuthenticationKind).Contains(authenticationKind.Value))
					{
						goto IL_0074;
					}
				}
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidAuthenticationType, null);
			}
			IL_0074:
			return this.CreateFormula(null);
		}

		// Token: 0x0600A162 RID: 41314 RVA: 0x0021779C File Offset: 0x0021599C
		public bool TryResolve(out IDataSourceLocation resolvedLocation)
		{
			return this.TryResolve(new Func<string, IPHostEntry>(Dns.GetHostEntry), out resolvedLocation);
		}

		// Token: 0x0600A163 RID: 41315 RVA: 0x001EE571 File Offset: 0x001EC771
		public string GetAddressFieldLabel(string addressField)
		{
			return DataSourceLocationOperations.GetAddressFieldLabel(addressField);
		}

		// Token: 0x0600A164 RID: 41316 RVA: 0x001EE6EA File Offset: 0x001EC8EA
		public override int GetHashCode()
		{
			return this.ComputeHashCode();
		}

		// Token: 0x0600A165 RID: 41317 RVA: 0x002177B1 File Offset: 0x002159B1
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IDataSourceLocation);
		}

		// Token: 0x0600A166 RID: 41318 RVA: 0x001EE6F2 File Offset: 0x001EC8F2
		public bool Equals(IDataSourceLocation other)
		{
			return this.AreEqual(other);
		}

		// Token: 0x0600A167 RID: 41319 RVA: 0x002177C0 File Offset: 0x002159C0
		int IComparable<IDataSourceLocation>.CompareTo(IDataSourceLocation other)
		{
			string text = null;
			DataSourceLocation dataSourceLocation = other as DataSourceLocation;
			if (dataSourceLocation != null)
			{
				text = dataSourceLocation.ComparableRepresentation;
			}
			return string.Compare(this.ComparableRepresentation, text, StringComparison.Ordinal);
		}

		// Token: 0x0600A168 RID: 41320 RVA: 0x002177ED File Offset: 0x002159ED
		protected static string GetAzureContainerPath(string account, string domain, string containerName)
		{
			UriBuilder azureAccountUrl = DataSourceLocation.GetAzureAccountUrl(account, domain, null);
			azureAccountUrl.Path = containerName.ToLowerInvariant() + "/";
			return azureAccountUrl.Uri.AbsoluteUri;
		}

		// Token: 0x0600A169 RID: 41321 RVA: 0x00217818 File Offset: 0x00215A18
		protected static UriBuilder GetAzureAccountUrl(string account, string domain, string container = null)
		{
			Uri uri;
			if (!Uri.TryCreate(account, UriKind.Absolute, out uri))
			{
				uri = new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}.{1}", account.ToLowerInvariant(), domain));
			}
			UriBuilder uriBuilder = new UriBuilder(uri);
			uriBuilder.Scheme = Uri.UriSchemeHttps;
			if (container != null)
			{
				uriBuilder.Path = container;
			}
			return uriBuilder;
		}

		// Token: 0x0600A16A RID: 41322 RVA: 0x0021786C File Offset: 0x00215A6C
		protected static UriBuilder GetAzureServerUrl(string server, string path = null)
		{
			Uri uri;
			if (!Uri.TryCreate(server, UriKind.Absolute, out uri))
			{
				uri = new Uri(string.Format(CultureInfo.InvariantCulture, "https://{0}", server.ToLowerInvariant()));
			}
			UriBuilder uriBuilder = new UriBuilder(uri);
			uriBuilder.Scheme = Uri.UriSchemeHttps;
			if (!string.IsNullOrEmpty(path))
			{
				uriBuilder.Path = path;
			}
			return uriBuilder;
		}

		// Token: 0x0600A16B RID: 41323 RVA: 0x002178C1 File Offset: 0x00215AC1
		protected static bool TrySplitAzure(Uri url, out string authority, out string containerName)
		{
			authority = url.GetLeftPart(UriPartial.Authority);
			containerName = url.AbsolutePath.Replace("/", "");
			return !string.IsNullOrEmpty(containerName);
		}

		// Token: 0x0600A16C RID: 41324 RVA: 0x002178F0 File Offset: 0x00215AF0
		protected string GetRelationalSourceFriendlyName()
		{
			string text = this.Protocol;
			string stringOrNull = this.Address.GetStringOrNull("server");
			if (stringOrNull != null)
			{
				text = stringOrNull;
				string stringOrNull2 = this.Address.GetStringOrNull("database");
				if (stringOrNull2 != null)
				{
					text = text + ";" + stringOrNull2;
				}
			}
			return text;
		}

		// Token: 0x0600A16D RID: 41325 RVA: 0x0021793C File Offset: 0x00215B3C
		protected string GetWebSourceFriendlyName()
		{
			return this.Address.GetValueOrDefault("url", this.Protocol) as string;
		}

		// Token: 0x0600A16E RID: 41326 RVA: 0x0021795C File Offset: 0x00215B5C
		protected bool TryResolveHostName(string hostName, Func<string, IPHostEntry> getHostEntry, out string resolvedHostName)
		{
			IPAddress ipaddress;
			if (!string.IsNullOrEmpty(hostName) && hostName != "(local)" && hostName != "localhost" && hostName != "." && (!IPAddress.TryParse(hostName, out ipaddress) || (!ipaddress.Equals(IPAddress.Loopback) && !ipaddress.Equals(IPAddress.IPv6Loopback))))
			{
				if (hostName.Contains("."))
				{
					resolvedHostName = hostName;
					return true;
				}
				try
				{
					IPHostEntry iphostEntry = getHostEntry(hostName);
					resolvedHostName = iphostEntry.HostName;
					return true;
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				catch (SocketException)
				{
				}
				catch (ArgumentException)
				{
				}
			}
			resolvedHostName = null;
			return false;
		}

		// Token: 0x0600A16F RID: 41327 RVA: 0x00217A1C File Offset: 0x00215C1C
		protected bool TryResolveUriHost(string uri, Func<string, IPHostEntry> getHostEntry, out string resolvedUri)
		{
			try
			{
				UriBuilder uriBuilder = new UriBuilder(uri);
				string text;
				if (this.TryResolveHostName(uriBuilder.Host, getHostEntry, out text))
				{
					if (uriBuilder.Uri.IsFile)
					{
						Resource.FileNormalizer normalizer = Resource.FileNormalizer.GetNormalizer();
						resolvedUri = normalizer.ReplaceHost(uri, text);
					}
					else
					{
						uriBuilder.Host = text;
						resolvedUri = uriBuilder.Uri.ToString();
					}
					return true;
				}
			}
			catch (UriFormatException)
			{
			}
			resolvedUri = null;
			return false;
		}

		// Token: 0x0600A170 RID: 41328 RVA: 0x00217A94 File Offset: 0x00215C94
		protected bool TryResolvePath(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			resolvedLocation = this.Clone();
			string stringOrNull = this.Address.GetStringOrNull("path");
			string text;
			if (this.TryResolveUriHost(stringOrNull, getHostEntry, out text))
			{
				resolvedLocation.Address["path"] = text;
				return true;
			}
			return false;
		}

		// Token: 0x0600A171 RID: 41329 RVA: 0x00217ADC File Offset: 0x00215CDC
		protected bool TryResolveUri(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			resolvedLocation = this.Clone();
			string text;
			if (this.TryResolveUriHost(this.Address.GetStringOrNull("url"), getHostEntry, out text))
			{
				resolvedLocation.Address["url"] = text;
				return true;
			}
			return false;
		}

		// Token: 0x0600A172 RID: 41330 RVA: 0x00217B24 File Offset: 0x00215D24
		protected bool TryResolveServer(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			resolvedLocation = this.Clone();
			string text;
			if (this.TryResolveHostName(this.Address.GetStringOrNull("server"), getHostEntry, out text))
			{
				resolvedLocation.Address["server"] = text;
				return true;
			}
			return false;
		}

		// Token: 0x0600A173 RID: 41331 RVA: 0x00217B69 File Offset: 0x00215D69
		protected IDataSourceLocation Clone()
		{
			IDataSourceLocation dataSourceLocation = DataSourceLocationFactory.New(this.Protocol);
			dataSourceLocation.Authentication = this.Authentication;
			dataSourceLocation.Address = this.Address;
			return dataSourceLocation;
		}

		// Token: 0x0600A174 RID: 41332 RVA: 0x00217B90 File Offset: 0x00215D90
		protected FormulaCreationResult ServerFormula(string function, string schema, RecordValue optionsRecord = null)
		{
			string stringOrNull = this.Address.GetStringOrNull("server");
			string stringOrNull2 = this.Address.GetStringOrNull("object");
			if (string.IsNullOrEmpty(stringOrNull))
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			if (this.Query != null)
			{
				if (schema != null || stringOrNull2 != null)
				{
					return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
				}
				optionsRecord = optionsRecord ?? RecordValue.Empty;
				optionsRecord = optionsRecord.Concatenate(RecordValue.New(Keys.New("Query"), new Value[] { TextValue.New(this.Query) })).AsRecord;
			}
			IExpression expression = ExpressionBuilder.Instance.Invoke(function, 1, new object[] { stringOrNull, optionsRecord });
			return this.IndexIntoTable(expression, schema, stringOrNull2, DataSourceLocation.GetHierarchicalNavigation(optionsRecord).GetValueOrDefault());
		}

		// Token: 0x0600A175 RID: 41333 RVA: 0x00217C54 File Offset: 0x00215E54
		protected FormulaCreationResult ServerDatabaseFormula(string function, RecordValue optionsRecord = null)
		{
			string stringOrNull = this.Address.GetStringOrNull("server");
			string stringOrNull2 = this.Address.GetStringOrNull("database");
			string stringOrNull3 = this.Address.GetStringOrNull("schema");
			string stringOrNull4 = this.Address.GetStringOrNull("object");
			if (string.IsNullOrEmpty(stringOrNull) || string.IsNullOrEmpty(stringOrNull2))
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			if (this.Query != null)
			{
				if (stringOrNull3 != null || stringOrNull4 != null)
				{
					return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
				}
				optionsRecord = optionsRecord ?? RecordValue.Empty;
				optionsRecord = optionsRecord.Concatenate(RecordValue.New(Keys.New("Query"), new Value[] { TextValue.New(this.Query) })).AsRecord;
			}
			IExpression expression = ExpressionBuilder.Instance.Invoke(function, 2, new object[] { stringOrNull, stringOrNull2, optionsRecord });
			return this.IndexIntoTable(expression, stringOrNull3, stringOrNull4, DataSourceLocation.GetHierarchicalNavigation(optionsRecord).GetValueOrDefault());
		}

		// Token: 0x0600A176 RID: 41334 RVA: 0x00217D48 File Offset: 0x00215F48
		protected static bool? GetHierarchicalNavigation(RecordValue optionsRecord)
		{
			Value value;
			if (optionsRecord != null && optionsRecord.TryGetValue("HierarchicalNavigation", out value) && value.IsLogical)
			{
				return new bool?(value.AsBoolean);
			}
			return null;
		}

		// Token: 0x0600A177 RID: 41335 RVA: 0x00217D84 File Offset: 0x00215F84
		protected static RecordValue GetAndRemoveHierarchicalNavigation(RecordValue optionsRecord, out bool? hierarchicalNavigation)
		{
			return DataSourceLocation.GetAndRemoveLogicalOption(optionsRecord, "HierarchicalNavigation", out hierarchicalNavigation);
		}

		// Token: 0x0600A178 RID: 41336 RVA: 0x00217D94 File Offset: 0x00215F94
		protected static RecordValue GetAndRemoveLogicalOption(RecordValue optionsRecord, string optionName)
		{
			bool? flag;
			return DataSourceLocation.GetAndRemoveLogicalOption(optionsRecord, optionName, out flag);
		}

		// Token: 0x0600A179 RID: 41337 RVA: 0x00217DAC File Offset: 0x00215FAC
		protected static RecordValue GetAndRemoveLogicalOption(RecordValue optionsRecord, string optionName, out bool? optionValue)
		{
			Value value;
			if (optionsRecord != null && optionsRecord.TryGetValue(optionName, out value) && (value.IsLogical || value.IsNull))
			{
				optionValue = (value.IsNull ? null : new bool?(value.AsBoolean));
				return Library.Record.RemoveFields.Invoke(optionsRecord, TextValue.New(optionName), Library.MissingField.Ignore).AsRecord;
			}
			optionValue = null;
			return optionsRecord;
		}

		// Token: 0x0600A17A RID: 41338 RVA: 0x00217E20 File Offset: 0x00216020
		protected static RecordValue GetAndRemoveTextOption(RecordValue optionsRecord, string optionName, out string optionValue)
		{
			Value value;
			if (optionsRecord != null && optionsRecord.TryGetValue(optionName, out value) && (value.IsText || value.IsNull))
			{
				optionValue = (value.IsNull ? null : value.AsString);
				return Library.Record.RemoveFields.Invoke(optionsRecord, TextValue.New(optionName), Library.MissingField.Ignore).AsRecord;
			}
			optionValue = null;
			return optionsRecord;
		}

		// Token: 0x0600A17B RID: 41339 RVA: 0x00217E80 File Offset: 0x00216080
		private FormulaCreationResult IndexIntoTable(IExpression result, string schema, string dbObject, bool hierarchicalNavigation)
		{
			if (dbObject == null)
			{
				if (schema != null)
				{
					return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
				}
			}
			else
			{
				ExpressionBuilder instance = ExpressionBuilder.Instance;
				if (schema == null)
				{
					result = instance.Navigate(result, "Item", dbObject, "Data");
				}
				else if (!hierarchicalNavigation)
				{
					result = instance.Navigate(result, "Schema", schema, "Item", dbObject, "Data");
				}
				else
				{
					result = instance.Navigate(instance.Navigate(result, "Schema", schema, "Data"), "Name", dbObject, "Data");
				}
			}
			return new FormulaCreationResult(result);
		}

		// Token: 0x0600A17C RID: 41340 RVA: 0x00217F08 File Offset: 0x00216108
		protected bool TryGetAzureResource(string defaultDomain, out IResource resource)
		{
			string stringOrNull = this.Address.GetStringOrNull("account");
			string stringOrNull2 = this.Address.GetStringOrNull("domain");
			if (string.IsNullOrEmpty(stringOrNull))
			{
				resource = null;
				return false;
			}
			string absoluteUri = DataSourceLocation.GetAzureAccountUrl(stringOrNull, stringOrNull2 ?? defaultDomain, null).Uri.AbsoluteUri;
			resource = Resource.New(this.ResourceKind, absoluteUri);
			return true;
		}

		// Token: 0x0600A17D RID: 41341 RVA: 0x00217F6C File Offset: 0x0021616C
		protected bool TryGetDatabaseResource(out IResource resource)
		{
			string stringOrNull = this.Address.GetStringOrNull("server");
			string text = this.Address.GetStringOrNull("database");
			if (string.IsNullOrEmpty(stringOrNull))
			{
				resource = null;
				return false;
			}
			if (string.IsNullOrEmpty(text))
			{
				text = null;
			}
			resource = DatabaseResource.New(this.ResourceKind, stringOrNull, text);
			return true;
		}

		// Token: 0x0600A17E RID: 41342 RVA: 0x00217FC4 File Offset: 0x002161C4
		protected bool TryGetFileResource(out IResource resource)
		{
			string stringOrNull = this.Address.GetStringOrNull("path");
			if (string.IsNullOrEmpty(stringOrNull))
			{
				resource = null;
				return false;
			}
			resource = Resource.New(this.ResourceKind, stringOrNull);
			return true;
		}

		// Token: 0x0600A17F RID: 41343 RVA: 0x00218000 File Offset: 0x00216200
		protected bool TryGetUrlResource(out IResource resource)
		{
			string stringOrNull = this.Address.GetStringOrNull("url");
			if (string.IsNullOrEmpty(stringOrNull))
			{
				resource = null;
				return false;
			}
			resource = Resource.New(this.ResourceKind, stringOrNull);
			return true;
		}

		// Token: 0x0600A180 RID: 41344 RVA: 0x0021803A File Offset: 0x0021623A
		protected bool TryGetSingletonResource(out IResource resource)
		{
			resource = Resource.New(this.ResourceKind, this.ResourceKind);
			return true;
		}

		// Token: 0x0600A181 RID: 41345 RVA: 0x00218050 File Offset: 0x00216250
		protected void NormalizeConnectionString(IConnectionStringService service)
		{
			object obj;
			object obj2;
			Dictionary<string, string> dictionary;
			if (this.Address.TryGetValue("connectionstring", out obj) && !this.Address.TryGetValue("options", out obj2) && obj is string && service.TryParseConnectionString((string)obj, out dictionary))
			{
				this.Address.Remove("connectionstring");
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>(dictionary.Count);
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					dictionary2[keyValuePair.Key] = keyValuePair.Value;
				}
				this.Address.Add("options", dictionary2);
			}
		}

		// Token: 0x0600A182 RID: 41346 RVA: 0x00218124 File Offset: 0x00216324
		protected static FormulaCreationResult CreateContentTypeFormula(string contentType, FormulaCreationResult innerExpression)
		{
			if (contentType == null)
			{
				return innerExpression;
			}
			ExpressionBuilder instance = ExpressionBuilder.Instance;
			IExpression formulaExpression = innerExpression.FormulaExpression;
			if (contentType != null)
			{
				int length = contentType.Length;
				IExpression expression;
				if (length <= 27)
				{
					switch (length)
					{
					case 8:
					{
						char c = contentType[5];
						if (c != 'c')
						{
							if (c != 'x')
							{
								goto IL_02D8;
							}
							if (!(contentType == "text/xml"))
							{
								goto IL_02D8;
							}
							goto IL_0260;
						}
						else
						{
							if (!(contentType == "text/csv"))
							{
								goto IL_02D8;
							}
							expression = instance.Let(new VariableInitializer[]
							{
								instance.Declare("Source", instance.Invoke("Csv.Document", new object[] { formulaExpression }), true),
								instance.Declare("PromoteHeaders", instance.Invoke("Table.PromoteHeaders", new object[] { Identifier.New("Source") }), true)
							});
							goto IL_02E0;
						}
						break;
					}
					case 9:
						if (!(contentType == "text/html"))
						{
							goto IL_02D8;
						}
						break;
					case 10:
						if (!(contentType == "text/plain"))
						{
							goto IL_02D8;
						}
						expression = instance.Invoke("Lines.FromBinary", new object[] { formulaExpression });
						goto IL_02E0;
					case 11:
						if (!(contentType == "text/x-json"))
						{
							goto IL_02D8;
						}
						goto IL_0278;
					case 12:
					case 13:
					case 14:
					case 17:
					case 18:
					case 19:
					case 22:
					case 23:
						goto IL_02D8;
					case 15:
						if (!(contentType == "application/xml"))
						{
							goto IL_02D8;
						}
						goto IL_0260;
					case 16:
						if (!(contentType == "application/json"))
						{
							goto IL_02D8;
						}
						goto IL_0278;
					case 20:
						if (!(contentType == "application/msaccess"))
						{
							goto IL_02D8;
						}
						expression = instance.Invoke("Access.Database", new object[] { formulaExpression });
						goto IL_02E0;
					case 21:
						if (!(contentType == "application/xhtml+xml"))
						{
							goto IL_02D8;
						}
						break;
					case 24:
						if (!(contentType == "application/vnd.ms-excel"))
						{
							goto IL_02D8;
						}
						goto IL_02C0;
					default:
						if (length != 27)
						{
							goto IL_02D8;
						}
						if (!(contentType == "application/vnd.ms-excel.12"))
						{
							goto IL_02D8;
						}
						goto IL_02C0;
					}
					expression = instance.Invoke("Web.Page", new object[] { formulaExpression });
					goto IL_02E0;
					IL_0260:
					expression = instance.Invoke("Xml.Tables", new object[] { formulaExpression });
					goto IL_02E0;
					IL_0278:
					expression = instance.Invoke("Json.Document", new object[] { formulaExpression });
					goto IL_02E0;
				}
				if (length != 46)
				{
					if (length != 53)
					{
						if (length != 65)
						{
							goto IL_02D8;
						}
						if (!(contentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
						{
							goto IL_02D8;
						}
					}
					else if (!(contentType == "application/vnd.ms-excel.sheet.binary.macroEnabled.12"))
					{
						goto IL_02D8;
					}
				}
				else if (!(contentType == "application/vnd.ms-excel.sheet.macroEnabled.12"))
				{
					goto IL_02D8;
				}
				IL_02C0:
				expression = instance.Invoke("Excel.Workbook", new object[] { formulaExpression });
				IL_02E0:
				return new FormulaCreationResult(expression);
			}
			IL_02D8:
			return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.UnrecognizedContentType, null);
		}

		// Token: 0x0600A183 RID: 41347 RVA: 0x00218418 File Offset: 0x00216618
		protected static FormulaCreationResult FormatInvocation(string functionName, int minArgs, params object[] args)
		{
			FormulaCreationResult formulaCreationResult;
			try
			{
				formulaCreationResult = new FormulaCreationResult(ExpressionBuilder.Instance.Invoke(functionName, minArgs, args));
			}
			catch (InvalidOperationException)
			{
				formulaCreationResult = new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			return formulaCreationResult;
		}

		// Token: 0x0600A184 RID: 41348 RVA: 0x00218458 File Offset: 0x00216658
		private void AddIfNotNull(IDictionary<string, object> value)
		{
			this.address = new Dictionary<string, object>();
			if (value != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in value)
				{
					if (keyValuePair.Value != null)
					{
						this.address.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x0600A185 RID: 41349 RVA: 0x002184CC File Offset: 0x002166CC
		private static AuthenticationKind? ToAuthenticationType(string authenticationType)
		{
			if (authenticationType != null)
			{
				int length = authenticationType.Length;
				switch (length)
				{
				case 4:
					if (!(authenticationType == "none"))
					{
						goto IL_0124;
					}
					return new AuthenticationKind?(AuthenticationKind.Implicit);
				case 5:
				{
					char c = authenticationType[2];
					if (c != 'g')
					{
						if (c != 's')
						{
							if (c != 'u')
							{
								goto IL_0124;
							}
							if (!(authenticationType == "oauth"))
							{
								goto IL_0124;
							}
							goto IL_010F;
						}
						else
						{
							if (!(authenticationType == "basic"))
							{
								goto IL_0124;
							}
							goto IL_0108;
						}
					}
					else
					{
						if (!(authenticationType == "orgid"))
						{
							goto IL_0124;
						}
						goto IL_010F;
					}
					break;
				}
				case 6:
				case 9:
					goto IL_0124;
				case 7:
				{
					char c = authenticationType[0];
					if (c != 'a')
					{
						if (c != 'w')
						{
							goto IL_0124;
						}
						if (!(authenticationType == "windows"))
						{
							goto IL_0124;
						}
						return new AuthenticationKind?(AuthenticationKind.Windows);
					}
					else if (!(authenticationType == "api-key"))
					{
						goto IL_0124;
					}
					break;
				}
				case 8:
					if (!(authenticationType == "protocol"))
					{
						goto IL_0124;
					}
					goto IL_0108;
				case 10:
					if (!(authenticationType == "ms-account"))
					{
						goto IL_0124;
					}
					goto IL_010F;
				default:
					if (length != 16)
					{
						goto IL_0124;
					}
					if (!(authenticationType == "azure-access-key"))
					{
						goto IL_0124;
					}
					break;
				}
				return new AuthenticationKind?(AuthenticationKind.Key);
				IL_0108:
				return new AuthenticationKind?(AuthenticationKind.UsernamePassword);
				IL_010F:
				return new AuthenticationKind?(AuthenticationKind.OAuth2);
			}
			IL_0124:
			return null;
		}

		// Token: 0x04005483 RID: 21635
		private Dictionary<string, object> address;

		// Token: 0x04005484 RID: 21636
		private string comparableRepresentation;

		// Token: 0x020018BD RID: 6333
		private class JsonDataSourceLocation
		{
			// Token: 0x04005488 RID: 21640
			public string protocol;

			// Token: 0x04005489 RID: 21641
			public Dictionary<string, object> address;

			// Token: 0x0400548A RID: 21642
			public string authentication;

			// Token: 0x0400548B RID: 21643
			public string query;
		}
	}
}
