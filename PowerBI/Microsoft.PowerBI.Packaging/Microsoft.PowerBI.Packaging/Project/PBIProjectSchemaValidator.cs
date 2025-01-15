using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.PowerBI.Packaging.Project.Artifacts;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000073 RID: 115
	public static class PBIProjectSchemaValidator
	{
		// Token: 0x06000326 RID: 806 RVA: 0x00008B0B File Offset: 0x00006D0B
		public static Version GetArtifactVersion(JObject artifact, string artifactName)
		{
			JToken jtoken = artifact["version"];
			return PBIProjectSchemaValidator.GetArtifactVersionObtainedFromJsonObject((jtoken != null) ? jtoken.ToString() : null, artifactName);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00008B2A File Offset: 0x00006D2A
		public static ArtifactShortcut ValidateAndReturnArtifactShortcut(string json)
		{
			return PBIProjectSchemaValidator.GenericValidateAndReturnArtifact<ArtifactShortcut>(json, false, true);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00008B34 File Offset: 0x00006D34
		public static ReportLocalSettings ValidateAndReturnReportSettings(string json)
		{
			return PBIProjectSchemaValidator.GenericValidateAndReturnArtifact<ReportLocalSettings>(json, false, true);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00008B3E File Offset: 0x00006D3E
		public static DatasetLocalSettings ValidateAndReturnDatasetSettings(string json)
		{
			return PBIProjectSchemaValidator.GenericValidateAndReturnArtifact<DatasetLocalSettings>(json, false, true);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00008B48 File Offset: 0x00006D48
		public static DatasetDefinition ValidateAndReturnDatasetDefinition(string json)
		{
			return PBIProjectSchemaValidator.GenericValidateAndReturnArtifact<DatasetDefinition>(json, true, true);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00008B52 File Offset: 0x00006D52
		public static DatasetModelReference ValidateAndReturnDatasetModelReference(string json)
		{
			return PBIProjectSchemaValidator.GenericValidateAndReturnArtifact<DatasetModelReference>(json, false, true);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00008B5C File Offset: 0x00006D5C
		public static DatasetEditorSettings ValidateAndReturnDatasetEditorSettings(string json)
		{
			return PBIProjectSchemaValidator.GenericValidateAndReturnArtifact<DatasetEditorSettings>(json, false, true);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00008B66 File Offset: 0x00006D66
		public static ArtifactConfig ValidateAndReturnArtifactConfig(string json)
		{
			return PBIProjectSchemaValidator.GenericValidateAndReturnArtifact<ArtifactConfig>(json, false, true);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00008B70 File Offset: 0x00006D70
		public static ArtifactMetadata ValidateAndReturnArtifactMetadata(string json)
		{
			return PBIProjectSchemaValidator.GenericValidateAndReturnArtifact<ArtifactMetadata>(json, false, false);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00008B7C File Offset: 0x00006D7C
		public static ArtifactDetails ValidateAndReturnArtifactDetails(string json)
		{
			string name = typeof(ArtifactDetailsConfig).Name;
			ArtifactDetails artifactDetails = PBIProjectSchemaValidator.GenericValidateAndReturnArtifact<ArtifactDetails>(json, false, false);
			if (((artifactDetails != null) ? artifactDetails.Config : null) != null)
			{
				Version artifactVersionObtainedFromJsonObject = PBIProjectSchemaValidator.GetArtifactVersionObtainedFromJsonObject(artifactDetails.Config.Version, name);
				Version version = PBIProjectSchemaValidator.ValidateArtifactVersion<ArtifactDetailsConfig>(artifactVersionObtainedFromJsonObject, name);
				if (version != artifactVersionObtainedFromJsonObject)
				{
					artifactDetails.Config.Version = version.ToString();
				}
			}
			return artifactDetails;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00008BE5 File Offset: 0x00006DE5
		public static UnappliedChanges ValidateAndReturnUnappliedChanges(string json)
		{
			return PBIProjectSchemaValidator.GenericValidateAndReturnArtifact<UnappliedChanges>(json, false, true);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00008BF0 File Offset: 0x00006DF0
		public static ReportDefinition ValidateAndReturnReportDefinition(string json)
		{
			ReportDefinition reportDefinition = PBIProjectSchemaValidator.GenericValidateAndReturnArtifact<ReportDefinition>(json, true, true);
			if (reportDefinition.DatasetReference.ByConnection == null && reportDefinition.DatasetReference.ByPath == null)
			{
				throw new PBIProjectValidationException("Either byPath or byConnection must be populated in 'definition.pbir'.", typeof(ReportDefinition).Name, PBIProjectException.PBIProjectErrorCode.ByPathAndByConnectionMissing);
			}
			if (reportDefinition.DatasetReference.ByConnection != null && reportDefinition.DatasetReference.ByPath != null)
			{
				throw new PBIProjectValidationException("Either byPath or byConnection must be populated but not both in 'definition.pbir'.", typeof(ReportDefinition).Name, PBIProjectException.PBIProjectErrorCode.ByPathAndByConnectionNotBoth);
			}
			return reportDefinition;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00008C74 File Offset: 0x00006E74
		private static Version GetArtifactVersionObtainedFromJsonObject(string version, string artifactName)
		{
			if (version == null)
			{
				throw new PBIProjectValidationException((artifactName + ": Missing version from JSON.").ToString(CultureInfo.CurrentCulture), artifactName, PBIProjectException.PBIProjectErrorCode.MissingVersion);
			}
			Version version2;
			if (!Version.TryParse(version, out version2))
			{
				throw new PBIProjectValidationException((artifactName + ": Invalid version from JSON.").ToString(CultureInfo.CurrentCulture), artifactName, PBIProjectException.PBIProjectErrorCode.InvalidVersion);
			}
			return version2;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00008CC9 File Offset: 0x00006EC9
		private static void ThrowUnrecognizedVersionError(Version version, string artifactName)
		{
			throw new PBIProjectValidationException(string.Format("{0}: Unrecognized version '{1}'.", artifactName, version).ToString(CultureInfo.CurrentCulture), artifactName, PBIProjectException.PBIProjectErrorCode.UnrecognizedVersion);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00008CE8 File Offset: 0x00006EE8
		private static T GenericValidateAndReturnArtifact<T>(string json, bool isRequired, bool validateVersion = true)
		{
			if (!isRequired && string.IsNullOrEmpty(json))
			{
				return default(T);
			}
			NewtonsoftValidationLicense.RegisterPowerBILicenseForNewtonsoftJsonSchema();
			string name = typeof(T).Name;
			return PBIProjectSchemaValidator.GenericParseAndValidate<T>(json, name, validateVersion);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00008D28 File Offset: 0x00006F28
		private static T GenericParseAndValidate<T>(string json, string artifactName, bool validateVersion)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw new PBIProjectValidationException((artifactName + ": Required artifact is missing.").ToString(CultureInfo.CurrentCulture), artifactName, PBIProjectException.PBIProjectErrorCode.RequiredArtifactMissing);
			}
			JObject jobject = JObject.Parse(json);
			bool flag = false;
			if (validateVersion)
			{
				Version artifactVersion = PBIProjectSchemaValidator.GetArtifactVersion(jobject, artifactName);
				Version version = PBIProjectSchemaValidator.ValidateArtifactVersion<T>(artifactVersion, artifactName);
				if (version != artifactVersion)
				{
					flag = true;
					jobject["version"] = version.ToString();
				}
			}
			JSchema jschema = PBIProjectSchemaValidator.GenerateSchemaForType(typeof(T));
			PBIProjectSchemaValidator.EnsureJObjIsPerSchema(jobject, jschema, artifactName, flag);
			return jobject.ToObject<T>();
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00008DBC File Offset: 0x00006FBC
		private static Version ValidateArtifactVersion<T>(Version version, string artifactName)
		{
			Func<int, int> verPart = delegate(int v)
			{
				if (v >= 0)
				{
					return v;
				}
				return 0;
			};
			Func<Version, Version, bool> verCmp = (Version v1, Version v2) => verPart(v1.Major) == verPart(v2.Major) && verPart(v1.Minor) == verPart(v2.Minor) && verPart(v1.Build) == verPart(v2.Build) && verPart(v1.Revision) == verPart(v2.Revision);
			if (PBIProjectUtils.GetSupportedVersions<T>().FirstOrDefault((Version vX) => verCmp(vX, version)) == null)
			{
				Version latestVersion = PBIProjectUtils.GetLatestVersion<T>();
				if (latestVersion.Major == version.Major)
				{
					return latestVersion;
				}
				PBIProjectSchemaValidator.ThrowUnrecognizedVersionError(version, artifactName);
			}
			return version;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00008E58 File Offset: 0x00007058
		private static JSchema GenerateSchemaForType(Type type)
		{
			return PBIProjectSchemaValidator.GeneratedSchemas.GetOrAdd(type, new Func<Type, JSchema>(PBIProjectSchemaValidator.DoGenerateSchemaForType));
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00008E71 File Offset: 0x00007071
		internal static JSchema DoGenerateSchemaForType(Type requestedType)
		{
			return new JSchemaGenerator().Generate(requestedType);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00008E80 File Offset: 0x00007080
		private static void EnsureJObjIsPerSchema(JObject jObj, JSchema schema, string artifactName, bool allowAdditionalProperties)
		{
			IList<ValidationError> list;
			if (SchemaExtensions.IsValid(jObj, schema, ref list))
			{
				return;
			}
			if (allowAdditionalProperties)
			{
				if (!list.Where((ValidationError e) => e.ErrorType != 15).Any<ValidationError>())
				{
					return;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Format(CultureInfo.CurrentCulture, "{0}:\n", artifactName));
			foreach (ValidationError validationError in list)
			{
				string text = ((!string.IsNullOrEmpty(validationError.Message)) ? (validationError.Message + " ") : "");
				string text2 = ((!string.IsNullOrEmpty(validationError.Path)) ? ("Path '" + validationError.Path + "', ") : "");
				string text3 = string.Concat(new string[]
				{
					text,
					text2,
					"line ",
					validationError.LineNumber.ToString(),
					", position ",
					validationError.LinePosition.ToString(),
					"."
				});
				stringBuilder.AppendLine(text3);
			}
			throw new PBIProjectJsonSchemaValidationException(stringBuilder.ToString(), artifactName, list, PBIProjectException.PBIProjectErrorCode.ObjectNotPerSchema);
		}

		// Token: 0x040001C8 RID: 456
		private static ConcurrentDictionary<Type, JSchema> GeneratedSchemas = new ConcurrentDictionary<Type, JSchema>();
	}
}
