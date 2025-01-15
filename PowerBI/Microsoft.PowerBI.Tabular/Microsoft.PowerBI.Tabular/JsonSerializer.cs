using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Converters;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000E6 RID: 230
	public static class JsonSerializer
	{
		// Token: 0x06000F2C RID: 3884 RVA: 0x000747C8 File Offset: 0x000729C8
		public static string SerializeObject(MetadataObject metadataObject)
		{
			if (metadataObject == null)
			{
				throw new ArgumentNullException("metadataObject");
			}
			if (!ObjectTreeHelper.SupportsSerialization(metadataObject.ObjectType))
			{
				throw new ArgumentException(TomSR.Exception_JsonSerializationNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(metadataObject.ObjectType)), "metadataObject");
			}
			return JsonSerializer.SerializeObjectImpl(metadataObject, null, -3, CompatibilityMode.Unknown);
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00074818 File Offset: 0x00072A18
		public static string SerializeObject(MetadataObject metadataObject, SerializeOptions options)
		{
			if (metadataObject == null)
			{
				throw new ArgumentNullException("metadataObject");
			}
			if (!ObjectTreeHelper.SupportsSerialization(metadataObject.ObjectType))
			{
				throw new ArgumentException(TomSR.Exception_JsonSerializationNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(metadataObject.ObjectType)), "metadataObject");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return JsonSerializer.SerializeObjectImpl(metadataObject, options, -3, CompatibilityMode.Unknown);
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00074874 File Offset: 0x00072A74
		public static string SerializeObject(MetadataObject metadataObject, SerializeOptions options, int dbCompatibilityLevel)
		{
			if (metadataObject == null)
			{
				throw new ArgumentNullException("metadataObject");
			}
			if (!ObjectTreeHelper.SupportsSerialization(metadataObject.ObjectType))
			{
				throw new ArgumentException(TomSR.Exception_JsonSerializationNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(metadataObject.ObjectType)), "metadataObject");
			}
			if (dbCompatibilityLevel < 1200)
			{
				throw new ArgumentOutOfRangeException("dbCompatibilityLevel");
			}
			return JsonSerializer.SerializeObjectImpl(metadataObject, options, dbCompatibilityLevel, CompatibilityMode.Unknown);
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x000748D4 File Offset: 0x00072AD4
		public static string SerializeObject(MetadataObject metadataObject, SerializeOptions options, int dbCompatibilityLevel, CompatibilityMode mode)
		{
			if (metadataObject == null)
			{
				throw new ArgumentNullException("metadataObject");
			}
			if (!ObjectTreeHelper.SupportsSerialization(metadataObject.ObjectType))
			{
				throw new ArgumentException(TomSR.Exception_JsonSerializationNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(metadataObject.ObjectType)), "metadataObject");
			}
			if (dbCompatibilityLevel < 1200)
			{
				throw new ArgumentOutOfRangeException("dbCompatibilityLevel");
			}
			if (mode == CompatibilityMode.Unknown)
			{
				throw new ArgumentOutOfRangeException("mode");
			}
			return JsonSerializer.SerializeObjectImpl(metadataObject, options, dbCompatibilityLevel, mode);
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00074944 File Offset: 0x00072B44
		public static string SerializeDatabase(Database database, SerializeOptions options = null)
		{
			if (database == null)
			{
				throw new ArgumentNullException("database");
			}
			if (options == null)
			{
				options = new SerializeOptions();
			}
			IDictionary<string, object> dictionary = database.SerializeToNewJsonObject(options).ToDictObject();
			string text;
			try
			{
				text = JsonConvert.SerializeObject(dictionary, JsonSerializer.CreateSettings());
			}
			catch (JsonException ex)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotSerializeObject(TomSR.ObjectType_Database), ex);
			}
			return text;
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x000749A8 File Offset: 0x00072BA8
		public static T DeserializeObject<T>(string json) where T : MetadataObject
		{
			return (T)((object)JsonSerializer.DeserializeObject(typeof(T), json));
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x000749BF File Offset: 0x00072BBF
		public static T DeserializeObject<T>(string json, DeserializeOptions options) where T : MetadataObject
		{
			return (T)((object)JsonSerializer.DeserializeObject(typeof(T), json, options));
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x000749D7 File Offset: 0x00072BD7
		public static T DeserializeObject<T>(string json, DeserializeOptions options, int dbCompatibilityLevel) where T : MetadataObject
		{
			return (T)((object)JsonSerializer.DeserializeObject(typeof(T), json, options, dbCompatibilityLevel));
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x000749F0 File Offset: 0x00072BF0
		public static T DeserializeObject<T>(string json, DeserializeOptions options, int dbCompatibilityLevel, CompatibilityMode mode) where T : MetadataObject
		{
			return (T)((object)JsonSerializer.DeserializeObject(typeof(T), json, options, dbCompatibilityLevel, mode));
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00074A0C File Offset: 0x00072C0C
		public static MetadataObject DeserializeObject(Type objectType, string json)
		{
			if (objectType == null)
			{
				throw new ArgumentNullException("objectType");
			}
			if (string.IsNullOrEmpty(json))
			{
				throw new ArgumentNullException("json");
			}
			ObjectType objectType2;
			if (!Utils.TryGetObjectTypeByType(objectType, out objectType2))
			{
				throw new ArgumentException(TomSR.Exception_JsonDeserializeObjectInvalidType(objectType.FullName), "objectType");
			}
			if (!ObjectTreeHelper.SupportsSerialization(objectType2))
			{
				throw new ArgumentException(TomSR.Exception_JsonSerializationNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(objectType2)), "objectType");
			}
			MetadataObject metadataObject = JsonSerializer.DeserializeObjectImpl(objectType2, json, null, -3, CompatibilityMode.Unknown);
			if (!objectType.IsAssignableFrom(metadataObject.GetType()))
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotDeserializeObjectWrongType(metadataObject.GetType().Name, objectType.Name), null);
			}
			return metadataObject;
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00074AB8 File Offset: 0x00072CB8
		public static MetadataObject DeserializeObject(Type objectType, string json, DeserializeOptions options)
		{
			if (objectType == null)
			{
				throw new ArgumentNullException("objectType");
			}
			if (string.IsNullOrEmpty(json))
			{
				throw new ArgumentNullException("json");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			ObjectType objectType2;
			if (!Utils.TryGetObjectTypeByType(objectType, out objectType2))
			{
				throw new ArgumentException(TomSR.Exception_JsonDeserializeObjectInvalidType(objectType.FullName), "objectType");
			}
			if (!ObjectTreeHelper.SupportsSerialization(objectType2))
			{
				throw new ArgumentException(TomSR.Exception_JsonSerializationNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(objectType2)), "objectType");
			}
			MetadataObject metadataObject = JsonSerializer.DeserializeObjectImpl(objectType2, json, options, -3, CompatibilityMode.Unknown);
			if (!objectType.IsAssignableFrom(metadataObject.GetType()))
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotDeserializeObjectWrongType(metadataObject.GetType().Name, objectType.Name), null);
			}
			return metadataObject;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00074B70 File Offset: 0x00072D70
		public static MetadataObject DeserializeObject(Type objectType, string json, DeserializeOptions options, int dbCompatibilityLevel)
		{
			if (objectType == null)
			{
				throw new ArgumentNullException("objectType");
			}
			if (string.IsNullOrEmpty(json))
			{
				throw new ArgumentNullException("json");
			}
			if (dbCompatibilityLevel < 1200)
			{
				throw new ArgumentOutOfRangeException("dbCompatibilityLevel");
			}
			ObjectType objectType2;
			if (!Utils.TryGetObjectTypeByType(objectType, out objectType2))
			{
				throw new ArgumentException(TomSR.Exception_JsonDeserializeObjectInvalidType(objectType.FullName), "objectType");
			}
			if (!ObjectTreeHelper.SupportsSerialization(objectType2))
			{
				throw new ArgumentException(TomSR.Exception_JsonSerializationNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(objectType2)), "objectType");
			}
			MetadataObject metadataObject = JsonSerializer.DeserializeObjectImpl(objectType2, json, options, dbCompatibilityLevel, CompatibilityMode.Unknown);
			if (!objectType.IsAssignableFrom(metadataObject.GetType()))
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotDeserializeObjectWrongType(metadataObject.GetType().Name, objectType.Name), null);
			}
			return metadataObject;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00074C2C File Offset: 0x00072E2C
		public static MetadataObject DeserializeObject(Type objectType, string json, DeserializeOptions options, int dbCompatibilityLevel, CompatibilityMode mode)
		{
			if (objectType == null)
			{
				throw new ArgumentNullException("objectType");
			}
			if (string.IsNullOrEmpty(json))
			{
				throw new ArgumentNullException("json");
			}
			if (dbCompatibilityLevel < 1200)
			{
				throw new ArgumentOutOfRangeException("dbCompatibilityLevel");
			}
			if (mode == CompatibilityMode.Unknown)
			{
				throw new ArgumentException("mode");
			}
			ObjectType objectType2;
			if (!Utils.TryGetObjectTypeByType(objectType, out objectType2))
			{
				throw new ArgumentException(TomSR.Exception_JsonDeserializeObjectInvalidType(objectType.FullName), "objectType");
			}
			if (!ObjectTreeHelper.SupportsSerialization(objectType2))
			{
				throw new ArgumentException(TomSR.Exception_JsonSerializationNotSupportedForObjectType(Utils.GetUserFriendlyNameOfObjectType(objectType2)), "objectType");
			}
			MetadataObject metadataObject = JsonSerializer.DeserializeObjectImpl(objectType2, json, options, dbCompatibilityLevel, mode);
			if (!objectType.IsAssignableFrom(metadataObject.GetType()))
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotDeserializeObjectWrongType(metadataObject.GetType().Name, objectType.Name), null);
			}
			return metadataObject;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00074CF8 File Offset: 0x00072EF8
		public static Database DeserializeDatabase(string json, DeserializeOptions options = null, CompatibilityMode mode = CompatibilityMode.Unknown)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw new ArgumentNullException("json");
			}
			if (mode == CompatibilityMode.Unknown)
			{
				mode = CompatibilityMode.PowerBI;
			}
			if (options == null)
			{
				options = new DeserializeOptions();
			}
			JObject jobject;
			try
			{
				jobject = JObject.Parse(json);
			}
			catch (JsonReaderException ex)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotDeserializeObjectMalformedInput(TomSR.ObjectType_Database), ex);
			}
			catch (JsonException ex2)
			{
				throw JsonSerializationUtil.CreateCannotDeserializeObjectException(ObjectType.Database, ex2.Message, ex2);
			}
			Database database = new Database();
			try
			{
				database.DeserializeFromJsonObject(jobject, options, mode);
			}
			catch (JsonSerializationException)
			{
				throw;
			}
			catch (Exception ex3)
			{
				if (!Utils.IsSafeException(ex3))
				{
					throw;
				}
				throw JsonSerializationUtil.CreateCannotDeserializeObjectException(ObjectType.Database, ex3.Message, ex3);
			}
			if (database.Model != null)
			{
				List<string> list = new List<string>();
				if (!database.Model.TryResolveAllCrossLinksInTreeByObjectPath(list) && options.ErrorOnUnresolvedLinks)
				{
					throw JsonSerializationUtil.CreateCannotResolvePathsWhileDeserializeObjectException(ObjectType.Database, list, null);
				}
			}
			return database;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00074DF8 File Offset: 0x00072FF8
		public static string GenerateSchema<T>() where T : MetadataObject
		{
			return JsonSerializer.GenerateSchema(typeof(T), null, 1000000);
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00074E0F File Offset: 0x0007300F
		public static string GenerateSchema<T>(SerializeOptions options) where T : MetadataObject
		{
			return JsonSerializer.GenerateSchema(typeof(T), options, 1000000);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00074E26 File Offset: 0x00073026
		public static string GenerateSchema<T>(SerializeOptions options, int dbCompatibilityLevel) where T : MetadataObject
		{
			return JsonSerializer.GenerateSchema(typeof(T), options, dbCompatibilityLevel);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00074E39 File Offset: 0x00073039
		public static string GenerateSchema(Type objectType)
		{
			return JsonSerializer.GenerateSchemaImpl(objectType, null, -3, CompatibilityMode.Unknown);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00074E45 File Offset: 0x00073045
		public static string GenerateSchema(Type objectType, SerializeOptions options)
		{
			return JsonSerializer.GenerateSchemaImpl(objectType, options, -3, CompatibilityMode.Unknown);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00074E51 File Offset: 0x00073051
		public static string GenerateSchema(Type objectType, SerializeOptions options, int dbCompatibilityLevel)
		{
			if (dbCompatibilityLevel < 1200)
			{
				throw new ArgumentOutOfRangeException("dbCompatibilityLevel");
			}
			return JsonSerializer.GenerateSchemaImpl(objectType, options, dbCompatibilityLevel, CompatibilityMode.Unknown);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00074E6F File Offset: 0x0007306F
		public static string GenerateSchema(Type objectType, SerializeOptions options, int dbCompatibilityLevel, CompatibilityMode mode)
		{
			if (dbCompatibilityLevel < 1200)
			{
				throw new ArgumentOutOfRangeException("dbCompatibilityLevel");
			}
			if (mode == CompatibilityMode.Unknown)
			{
				throw new ArgumentException("mode");
			}
			return JsonSerializer.GenerateSchemaImpl(objectType, options, dbCompatibilityLevel, mode);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00074E9C File Offset: 0x0007309C
		internal static string SerializeObjectImpl(MetadataObject metadataObject, SerializeOptions options, int dbCompatibilityLevel, CompatibilityMode mode)
		{
			if (mode == CompatibilityMode.Unknown && !metadataObject.TryGetCurrentMode(out mode))
			{
				mode = CompatibilityMode.PowerBI;
			}
			if (dbCompatibilityLevel == -3)
			{
				dbCompatibilityLevel = metadataObject.GetCompatibilityRequirementLevel(mode);
				if (dbCompatibilityLevel == -1)
				{
					dbCompatibilityLevel = 1200;
				}
			}
			if (options == null)
			{
				options = new SerializeOptions();
			}
			int num;
			string text;
			metadataObject.GetCompatibilityRequirement(mode, out num, out text);
			if (num == -2 || dbCompatibilityLevel < num)
			{
				throw new CompatibilityViolationException(mode, dbCompatibilityLevel, num, text);
			}
			IDictionary<string, object> dictionary = metadataObject.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject();
			string text2;
			try
			{
				text2 = JsonConvert.SerializeObject(dictionary, JsonSerializer.CreateSettings());
			}
			catch (JsonException ex)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotSerializeObject(Utils.GetUserFriendlyNameOfObjectType(metadataObject.ObjectType)), ex);
			}
			return text2;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00074F44 File Offset: 0x00073144
		internal static string SerializeDatabase(Database database, SerializeOptions options = null)
		{
			if (database == null)
			{
				throw new ArgumentNullException("database");
			}
			if (options == null)
			{
				options = new SerializeOptions();
			}
			JsonObject jsonObject = new JsonObject();
			database.SerializeToJsonObject(jsonObject, options);
			IDictionary<string, object> dictionary = jsonObject.ToDictObject();
			string text;
			try
			{
				text = JsonConvert.SerializeObject(dictionary, JsonSerializer.CreateSettings());
			}
			catch (JsonException ex)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotSerializeObject(TomSR.ObjectType_Database), ex);
			}
			return text;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00074FB0 File Offset: 0x000731B0
		internal static void DeserializeDatabase(Database db, JsonTextReader jsonReader, DeserializeOptions options, CompatibilityMode mode)
		{
			Utils.Verify(jsonReader.TokenType == 1);
			JObject jobject = null;
			try
			{
				jobject = JObject.Load(jsonReader);
			}
			catch (JsonReaderException ex)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotDeserializeObjectMalformedInput(Utils.GetUserFriendlyNameOfObjectType(ObjectType.Database)), ex);
			}
			catch (JsonException ex2)
			{
				throw JsonSerializationUtil.CreateCannotDeserializeObjectException(ObjectType.Database, jsonReader, ex2.Message, ex2);
			}
			try
			{
				db.DeserializeFromJsonObject(jobject, options, mode);
			}
			catch (JsonSerializationException)
			{
				throw;
			}
			catch (Exception ex3)
			{
				if (!Utils.IsSafeException(ex3))
				{
					throw;
				}
				throw JsonSerializationUtil.CreateCannotDeserializeObjectException(ObjectType.Database, ex3.Message, ex3);
			}
			if (db.Model != null)
			{
				List<string> list = new List<string>();
				if (!db.Model.TryResolveAllCrossLinksInTreeByObjectPath(list) && options.ErrorOnUnresolvedLinks)
				{
					throw JsonSerializationUtil.CreateCannotResolvePathsWhileDeserializeObjectException(ObjectType.Database, list, null);
				}
			}
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00075098 File Offset: 0x00073298
		private static MetadataObject DeserializeObjectImpl(ObjectType objectType, string json, DeserializeOptions options, int dbCompatibilityLevel, CompatibilityMode mode)
		{
			if (dbCompatibilityLevel == -3)
			{
				dbCompatibilityLevel = int.MaxValue;
			}
			if (mode == CompatibilityMode.Unknown)
			{
				mode = CompatibilityMode.PowerBI;
			}
			if (options == null)
			{
				options = new DeserializeOptions();
			}
			JObject jobject;
			try
			{
				jobject = JObject.Parse(json);
			}
			catch (JsonReaderException ex)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotDeserializeObjectMalformedInput(Utils.GetUserFriendlyNameOfObjectType(objectType)), ex);
			}
			catch (JsonException ex2)
			{
				throw JsonSerializationUtil.CreateCannotDeserializeObjectException(objectType, ex2.Message, ex2);
			}
			MetadataObject metadataObject;
			try
			{
				metadataObject = ObjectFactory.CreateMetadataObjectFromJsonObject(objectType, jobject);
				metadataObject.DeserializeFromJsonObject(jobject, options, mode, dbCompatibilityLevel);
			}
			catch (JsonSerializationException)
			{
				throw;
			}
			catch (Exception ex3)
			{
				if (!Utils.IsSafeException(ex3))
				{
					throw;
				}
				throw JsonSerializationUtil.CreateCannotDeserializeObjectException(objectType, ex3.Message, ex3);
			}
			List<string> list = new List<string>();
			if (!metadataObject.TryResolveAllCrossLinksInTreeByObjectPath(list) && options.ErrorOnUnresolvedLinks)
			{
				throw JsonSerializationUtil.CreateCannotResolvePathsWhileDeserializeObjectException(objectType, list, null);
			}
			return metadataObject;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0007517C File Offset: 0x0007337C
		private static string GenerateSchemaImpl(Type objectType, SerializeOptions options, int dbCompatibilityLevel, CompatibilityMode mode)
		{
			if (mode == CompatibilityMode.Unknown)
			{
				mode = CompatibilityMode.PowerBI;
			}
			if (dbCompatibilityLevel == -3)
			{
				dbCompatibilityLevel = 1000000;
			}
			if (options == null)
			{
				options = new SerializeOptions();
			}
			if (options.IncludeTranslatablePropertiesOnly)
			{
				throw new NotSupportedException(TomSR.Exception_Json_GenerateSchemaWithTranslatablePropertiesNotSupported);
			}
			StringBuilder stringBuilder = new StringBuilder();
			using (TextWriter textWriter = new StringWriter(stringBuilder))
			{
				using (JsonWriter jsonWriter = new JsonTextWriter(textWriter))
				{
					jsonWriter.Formatting = 1;
					if (objectType == typeof(Database))
					{
						JsonSchemaWriter.WriteSchemaForDatabase(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(Model))
					{
						JsonSchemaWriter.WriteSchemaForModel(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(DataSource))
					{
						JsonSchemaWriter.WriteSchemaForDataSource(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(ProviderDataSource))
					{
						JsonSchemaWriter.WriteSchemaForProviderDataSource(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(StructuredDataSource))
					{
						if (CompatibilityRestrictions.StructuredDataSource.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForStructuredDataSource(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(Table))
					{
						JsonSchemaWriter.WriteSchemaForTable(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(Column))
					{
						JsonSchemaWriter.WriteSchemaForColumn(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(DataColumn))
					{
						JsonSchemaWriter.WriteSchemaForDataColumn(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(RowNumberColumn))
					{
						JsonSchemaWriter.WriteSchemaForRowNumberColumn(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(CalculatedTableColumn))
					{
						JsonSchemaWriter.WriteSchemaForCalculatedTableColumn(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(CalculatedColumn))
					{
						JsonSchemaWriter.WriteSchemaForCalculatedColumn(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(AttributeHierarchy))
					{
						JsonSchemaWriter.WriteSchemaForAttributeHierarchy(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(Partition))
					{
						JsonSchemaWriter.WriteSchemaForPartition(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(Relationship))
					{
						JsonSchemaWriter.WriteSchemaForRelationship(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(SingleColumnRelationship))
					{
						JsonSchemaWriter.WriteSchemaForSingleColumnRelationship(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(Measure))
					{
						JsonSchemaWriter.WriteSchemaForMeasure(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(Hierarchy))
					{
						JsonSchemaWriter.WriteSchemaForHierarchy(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(Level))
					{
						JsonSchemaWriter.WriteSchemaForLevel(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(Annotation))
					{
						JsonSchemaWriter.WriteSchemaForAnnotation(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(KPI))
					{
						JsonSchemaWriter.WriteSchemaForKPI(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(Culture))
					{
						JsonSchemaWriter.WriteSchemaForCulture(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(LinguisticMetadata))
					{
						JsonSchemaWriter.WriteSchemaForLinguisticMetadata(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(Perspective))
					{
						JsonSchemaWriter.WriteSchemaForPerspective(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(PerspectiveTable))
					{
						JsonSchemaWriter.WriteSchemaForPerspectiveTable(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(PerspectiveColumn))
					{
						JsonSchemaWriter.WriteSchemaForPerspectiveColumn(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(PerspectiveHierarchy))
					{
						JsonSchemaWriter.WriteSchemaForPerspectiveHierarchy(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(PerspectiveMeasure))
					{
						JsonSchemaWriter.WriteSchemaForPerspectiveMeasure(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(ModelRole))
					{
						JsonSchemaWriter.WriteSchemaForModelRole(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(ModelRoleMember))
					{
						JsonSchemaWriter.WriteSchemaForModelRoleMember(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(WindowsModelRoleMember))
					{
						JsonSchemaWriter.WriteSchemaForWindowsModelRoleMember(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(ExternalModelRoleMember))
					{
						JsonSchemaWriter.WriteSchemaForExternalModelRoleMember(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(TablePermission))
					{
						JsonSchemaWriter.WriteSchemaForTablePermission(jsonWriter, options, mode, dbCompatibilityLevel);
					}
					else if (objectType == typeof(Variation))
					{
						if (CompatibilityRestrictions.Variation.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForVariation(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(Set))
					{
						if (CompatibilityRestrictions.Set.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForSet(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(PerspectiveSet))
					{
						if (CompatibilityRestrictions.PerspectiveSet.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForPerspectiveSet(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(ExtendedProperty))
					{
						if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForExtendedProperty(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(StringExtendedProperty))
					{
						if (CompatibilityRestrictions.StringExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForStringExtendedProperty(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(JsonExtendedProperty))
					{
						if (CompatibilityRestrictions.JsonExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForJsonExtendedProperty(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(NamedExpression))
					{
						if (CompatibilityRestrictions.NamedExpression.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForNamedExpression(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(ColumnPermission))
					{
						if (CompatibilityRestrictions.ColumnPermission.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForColumnPermission(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(DetailRowsDefinition))
					{
						if (CompatibilityRestrictions.DetailRowsDefinition.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForDetailRowsDefinition(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(RelatedColumnDetails))
					{
						if (CompatibilityRestrictions.RelatedColumnDetails.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForRelatedColumnDetails(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(GroupByColumn))
					{
						if (CompatibilityRestrictions.GroupByColumn.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForGroupByColumn(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(CalculationGroup))
					{
						if (CompatibilityRestrictions.CalculationGroup.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForCalculationGroup(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(CalculationItem))
					{
						if (CompatibilityRestrictions.CalculationItem.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForCalculationItem(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(AlternateOf))
					{
						if (CompatibilityRestrictions.AlternateOf.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForAlternateOf(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(RefreshPolicy))
					{
						if (CompatibilityRestrictions.RefreshPolicy.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForRefreshPolicy(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(BasicRefreshPolicy))
					{
						if (CompatibilityRestrictions.BasicRefreshPolicy.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForBasicRefreshPolicy(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(FormatStringDefinition))
					{
						if (CompatibilityRestrictions.FormatStringDefinition.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForFormatStringDefinition(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(QueryGroup))
					{
						if (CompatibilityRestrictions.QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForQueryGroup(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(AnalyticsAIMetadata))
					{
						if (CompatibilityRestrictions.AnalyticsAIMetadata.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForAnalyticsAIMetadata(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(ChangedProperty))
					{
						if (CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForChangedProperty(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(ExcludedArtifact))
					{
						if (CompatibilityRestrictions.ExcludedArtifact.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForExcludedArtifact(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(DataCoverageDefinition))
					{
						if (CompatibilityRestrictions.DataCoverageDefinition.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForDataCoverageDefinition(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(CalculationGroupExpression))
					{
						if (CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForCalculationGroupExpression(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(Calendar))
					{
						if (CompatibilityRestrictions.Calendar.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForCalendar(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(TimeUnitColumnAssociation))
					{
						if (CompatibilityRestrictions.TimeUnitColumnAssociation.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForTimeUnitColumnAssociation(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(Function))
					{
						if (CompatibilityRestrictions.Function.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForFunction(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else if (objectType == typeof(BindingInfo))
					{
						if (CompatibilityRestrictions.BindingInfo.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForBindingInfo(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
					else
					{
						if (!(objectType == typeof(DataBindingHint)))
						{
							throw new ArgumentException(TomSR.Exception_CannotGenerateSchemaForUnknownType(objectType.FullName));
						}
						if (CompatibilityRestrictions.DataBindingHint.IsCompatible(mode, dbCompatibilityLevel))
						{
							JsonSchemaWriter.WriteSchemaForDataBindingHint(jsonWriter, options, mode, dbCompatibilityLevel);
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00075B90 File Offset: 0x00073D90
		private static JsonSerializerSettings CreateSettings()
		{
			return new JsonSerializerSettings
			{
				ReferenceLoopHandling = 1,
				Formatting = 1,
				MaxDepth = new int?(128),
				Converters = 
				{
					new IsoDateTimeConverter()
				}
			};
		}

		// Token: 0x040001CB RID: 459
		private const int DefaultSerializationMaxDepth = 128;
	}
}
