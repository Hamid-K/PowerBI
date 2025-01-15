using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Microsoft.InfoNav;
using Microsoft.Lucia.Json;
using Microsoft.Lucia.Yaml;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using YamlDotNet.Core;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001A1 RID: 417
	public sealed class LsdlDocument
	{
		// Token: 0x06000878 RID: 2168 RVA: 0x00011130 File Offset: 0x0000F330
		public LsdlDocument()
		{
			this.Version = LsdlVersion.Latest;
			this.Language = LanguageIdentifier.en_US.ToLanguageName();
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x000111A2 File Offset: 0x0000F3A2
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x000111B0 File Offset: 0x0000F3B0
		[JsonProperty(Required = Required.Always)]
		[JsonConverter(typeof(VersionConverter))]
		public Version Version
		{
			get
			{
				return this._version.Value;
			}
			set
			{
				LsdlVersion lsdlVersion;
				if (!LsdlVersion.SupportedVersions.TryGetValue(value, out lsdlVersion))
				{
					throw new ArgumentOutOfRangeException();
				}
				this._version = lsdlVersion;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x000111D9 File Offset: 0x0000F3D9
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x000111E1 File Offset: 0x0000F3E1
		[JsonProperty(Required = Required.Always)]
		public string Language { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x000111EA File Offset: 0x0000F3EA
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x000111F2 File Offset: 0x0000F3F2
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public LsdlDynamicImprovement DynamicImprovement { get; set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x000111FB File Offset: 0x0000F3FB
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x00011203 File Offset: 0x0000F403
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public LsdlMinResultConfidence MinResultConfidence { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0001120C File Offset: 0x0000F40C
		[JsonProperty]
		public Dictionary<string, LsdlReference> Namespaces { get; } = new Dictionary<string, LsdlReference>();

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x00011214 File Offset: 0x0000F414
		[JsonProperty]
		public Dictionary<string, Entity> Entities { get; } = new Dictionary<string, Entity>();

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0001121C File Offset: 0x0000F41C
		[JsonProperty]
		public Dictionary<string, Relationship> Relationships { get; } = new Dictionary<string, Relationship>();

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00011224 File Offset: 0x0000F424
		[JsonProperty]
		public List<GlobalSubstitution> GlobalSubstitutions { get; } = new CustomSerializableList<GlobalSubstitution>(YamlSerializationOptions.LineBreakAfter);

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0001122C File Offset: 0x0000F42C
		[JsonProperty]
		public List<Example> Examples { get; } = new CustomSerializableList<Example>(YamlSerializationOptions.LineBreakAfter);

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x00011234 File Offset: 0x0000F434
		[JsonProperty]
		public Dictionary<string, AgentProperties> Agents { get; } = new Dictionary<string, AgentProperties>();

		// Token: 0x06000887 RID: 2183 RVA: 0x0001123C File Offset: 0x0000F43C
		public bool ShouldSerializeNamespaces()
		{
			return this.Namespaces.Count > 0;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001124C File Offset: 0x0000F44C
		public bool ShouldSerializeEntities()
		{
			return this.Entities.Count > 0;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001125C File Offset: 0x0000F45C
		public bool ShouldSerializeRelationships()
		{
			return this.Relationships.Count > 0;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001126C File Offset: 0x0000F46C
		public bool ShouldSerializeGlobalSubstitutions()
		{
			return this.GlobalSubstitutions.Count > 0;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001127C File Offset: 0x0000F47C
		public bool ShouldSerializeExamples()
		{
			return this.Examples.Count > 0;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001128C File Offset: 0x0000F48C
		public bool ShouldSerializeAgents()
		{
			return this.Agents.Count > 0;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001129C File Offset: 0x0000F49C
		public string ToJsonString(Formatting formatting = Formatting.None)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.WriteJson(stringWriter, formatting);
			return stringWriter.ToString();
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000112C4 File Offset: 0x0000F4C4
		public string ToYamlString(Version minRequestedVersion, LsdlSerializerSettings settings = null)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			Version version;
			this.WriteYaml(stringWriter, minRequestedVersion, out version, settings);
			return stringWriter.ToString();
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000112F0 File Offset: 0x0000F4F0
		public static bool TryReadJson(JsonReader reader, IDomainModelDiagnosticContext diagnosticContext, out LsdlDocument lsdlDocument)
		{
			bool flag;
			try
			{
				flag = LsdlDocument.TryReadJson(JObject.Load(reader), diagnosticContext, out lsdlDocument);
			}
			catch (JsonReaderException ex)
			{
				diagnosticContext.Register(DomainModelDiagnosticMessageFactory.LinguisticSchemaDeserializationErrorJson(ex));
				lsdlDocument = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00011334 File Offset: 0x0000F534
		public static bool TryReadJson(JsonReader reader, out LsdlDocument lsdlDocument, out IReadOnlyList<DomainModelDiagnosticMessage> diagnosticMessages)
		{
			DomainModelDiagnosticMessageCollector domainModelDiagnosticMessageCollector = new DomainModelDiagnosticMessageCollector(null);
			bool flag = LsdlDocument.TryReadJson(reader, domainModelDiagnosticMessageCollector, out lsdlDocument);
			diagnosticMessages = domainModelDiagnosticMessageCollector.Messages;
			return flag;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00011358 File Offset: 0x0000F558
		private static bool TryReadJson(JObject schema, IDomainModelDiagnosticContext diagnosticContext, out LsdlDocument lsdlDocument)
		{
			bool flag;
			try
			{
				if (!LsdlJsonValidator.TryValidateJson(schema, diagnosticContext))
				{
					lsdlDocument = null;
					flag = false;
				}
				else if (!LsdlVersionTransforms.TryUpgradeJson(schema, LsdlVersion.Latest, diagnosticContext))
				{
					lsdlDocument = null;
					flag = false;
				}
				else
				{
					lsdlDocument = LsdlJsonSerializer.ReadJson(schema.CreateReader());
					flag = true;
				}
			}
			catch (JsonReaderException ex)
			{
				diagnosticContext.Register(DomainModelDiagnosticMessageFactory.LinguisticSchemaDeserializationErrorJson(ex));
				lsdlDocument = null;
				flag = false;
			}
			catch (JsonSerializationException ex2)
			{
				diagnosticContext.Register(DomainModelDiagnosticMessageFactory.LinguisticSchemaDeserializationError(ex2));
				lsdlDocument = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x000113E0 File Offset: 0x0000F5E0
		public static LsdlDocument FromJsonString(string json, bool upgrade = false)
		{
			return LsdlJsonSerializer.ReadJson(LsdlDocument.CreateJsonReader(json, upgrade));
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x000113EE File Offset: 0x0000F5EE
		public static LsdlDocument FromInternalJsonString(string json, bool upgrade = false)
		{
			return LsdlJsonSerializer.ReadInternalJson(LsdlDocument.CreateJsonReader(json, upgrade));
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x000113FC File Offset: 0x0000F5FC
		public static LsdlDocument FromYamlString(string yaml, bool upgrade = false)
		{
			JsonReader jsonReader = JsonReaderFactory.FromYamlString(yaml);
			if (upgrade)
			{
				jsonReader = LsdlDocument.UpgradeJsonReader(jsonReader);
			}
			return LsdlJsonSerializer.ReadJson(jsonReader);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00011420 File Offset: 0x0000F620
		public static bool TryReadYaml(TextReader reader, IDomainModelDiagnosticContext diagnosticContext, out LsdlDocument lsdlDocument)
		{
			bool flag;
			try
			{
				flag = LsdlDocument.TryReadJson(JsonReaderFactory.CreateFromYaml(reader, false), diagnosticContext, out lsdlDocument);
			}
			catch (YamlException ex)
			{
				diagnosticContext.Register(DomainModelDiagnosticMessageFactory.LinguisticSchemaDeserializationErrorYaml(ex));
				lsdlDocument = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00011464 File Offset: 0x0000F664
		public static bool TryReadYaml(TextReader reader, out LsdlDocument lsdlDocument, out IReadOnlyList<DomainModelDiagnosticMessage> diagnosticMessages)
		{
			DomainModelDiagnosticMessageCollector domainModelDiagnosticMessageCollector = new DomainModelDiagnosticMessageCollector(null);
			bool flag = LsdlDocument.TryReadYaml(reader, domainModelDiagnosticMessageCollector, out lsdlDocument);
			diagnosticMessages = domainModelDiagnosticMessageCollector.Messages;
			return flag;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00011488 File Offset: 0x0000F688
		public static bool TryReadXml(XDocument schemaDocument, IDomainModelDiagnosticContext diagnosticContext, out LsdlDocument lsdlDocument)
		{
			JObject jobject;
			if (!LinguisticSchemaXmlToJsonUpgrader.TryUpgrade(schemaDocument, diagnosticContext, out jobject))
			{
				lsdlDocument = null;
				return false;
			}
			return LsdlDocument.TryReadJson(jobject, diagnosticContext, out lsdlDocument);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000114B0 File Offset: 0x0000F6B0
		public static bool TryReadXml(XDocument schemaDocument, out LsdlDocument lsdlDocument, out IReadOnlyList<DomainModelDiagnosticMessage> diagnosticMessages)
		{
			DomainModelDiagnosticMessageCollector domainModelDiagnosticMessageCollector = new DomainModelDiagnosticMessageCollector(null);
			bool flag = LsdlDocument.TryReadXml(schemaDocument, domainModelDiagnosticMessageCollector, out lsdlDocument);
			diagnosticMessages = domainModelDiagnosticMessageCollector.Messages;
			return flag;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000114D4 File Offset: 0x0000F6D4
		public void WriteJson(JsonWriter writer, Version minRequestedVersion, out Version actualVersion)
		{
			if (this.IsDowngradeTransformNeeded(minRequestedVersion, out actualVersion))
			{
				JObject jobject = JsonWriterFactory.To<JObject>(delegate(JsonWriter w)
				{
					this.WriteJson(w, Formatting.None);
				});
				LsdlVersionTransforms.DowngradeJson(jobject, actualVersion);
				jobject.WriteTo(writer, Array.Empty<JsonConverter>());
				return;
			}
			Version version = this.Version;
			try
			{
				this.Version = actualVersion;
				this.WriteJson(writer, writer.Formatting);
			}
			finally
			{
				this.Version = version;
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00011548 File Offset: 0x0000F748
		public void WriteYaml(TextWriter writer, Version minRequestedVersion, out Version actualVersion, LsdlSerializerSettings settings = null)
		{
			LsdlDocument lsdlDocument;
			if (this.IsDowngradeTransformNeeded(minRequestedVersion, out actualVersion))
			{
				if (minRequestedVersion < LsdlVersion.LatestPublic)
				{
					throw new ArgumentException("Cannot serialize YAML with downgrade transform below latest public version");
				}
				JObject jobject = JsonWriterFactory.To<JObject>(delegate(JsonWriter w)
				{
					this.WriteJson(w, Formatting.None);
				});
				LsdlVersionTransforms.DowngradeJson(jobject, actualVersion);
				lsdlDocument = LsdlJsonSerializer.ReadInternalJson(jobject.CreateReader());
			}
			else
			{
				lsdlDocument = this;
			}
			Version version = lsdlDocument.Version;
			try
			{
				lsdlDocument.Version = actualVersion;
				lsdlDocument.WriteYaml(writer, settings);
			}
			finally
			{
				lsdlDocument.Version = version;
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x000115D4 File Offset: 0x0000F7D4
		public void ChangeLanguage(LanguageIdentifier language)
		{
			this.Language = language.ToLanguageName();
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x000115E4 File Offset: 0x0000F7E4
		internal static JsonReader UpgradeJsonReader(JsonReader reader)
		{
			JObject jobject = JObject.Load(reader);
			DomainModelDiagnosticMessageCollector domainModelDiagnosticMessageCollector = new DomainModelDiagnosticMessageCollector(null);
			if (!LsdlVersionTransforms.TryUpgradeJson(jobject, LsdlVersion.Latest, domainModelDiagnosticMessageCollector))
			{
				throw new ArgumentException(domainModelDiagnosticMessageCollector.Messages.ToFormattedString(true, false), "reader");
			}
			return jobject.CreateReader();
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001162C File Offset: 0x0000F82C
		private static JsonReader CreateJsonReader(string json, bool upgrade)
		{
			JsonReader jsonReader = JsonReaderFactory.FromString(json);
			if (!upgrade)
			{
				return jsonReader;
			}
			return LsdlDocument.UpgradeJsonReader(jsonReader);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001164C File Offset: 0x0000F84C
		private bool IsDowngradeTransformNeeded(Version minRequestedVersion, out Version actualVersion)
		{
			Version minimumRequiredVersion = LsdlVersion.GetMinimumRequiredVersion(this);
			actualVersion = ((minimumRequiredVersion < minRequestedVersion) ? minRequestedVersion : minimumRequiredVersion);
			return LsdlVersionTransforms.IsDowngradeTransformNeeded(LsdlVersion.Latest, actualVersion);
		}

		// Token: 0x04000731 RID: 1841
		private LsdlVersion _version;
	}
}
