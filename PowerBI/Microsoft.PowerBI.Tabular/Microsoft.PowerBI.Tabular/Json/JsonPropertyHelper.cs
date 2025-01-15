using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Json
{
	// Token: 0x020001B9 RID: 441
	internal static class JsonPropertyHelper
	{
		// Token: 0x06001B20 RID: 6944 RVA: 0x000B8212 File Offset: 0x000B6412
		public static object ConvertPrimitiveToJsonValue<T>(T value) where T : struct
		{
			return value;
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x000B821A File Offset: 0x000B641A
		public static object ConvertEnumToJsonValue<T>(T value) where T : struct
		{
			Type typeFromHandle = typeof(T);
			Utils.Verify(typeFromHandle.IsEnum, "Type must be Enum");
			return Enum.GetName(typeFromHandle, value).ToJsonCase();
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x000B8248 File Offset: 0x000B6448
		public static T ConvertJsonValueToPrimitive<T>(JToken rawJsonValue) where T : struct
		{
			Type typeFromHandle = typeof(T);
			if (typeFromHandle == typeof(bool))
			{
				if (rawJsonValue.Type != 9)
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotConvertToType(rawJsonValue.ToString(), typeFromHandle.Name), rawJsonValue, null);
				}
			}
			else if (typeFromHandle == typeof(int) && rawJsonValue.Type != 6)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotConvertToType(rawJsonValue.ToString(), typeFromHandle.Name), rawJsonValue, null);
			}
			return JsonPropertyHelper.WrapJsonValueConversion<T>(rawJsonValue);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x000B82D0 File Offset: 0x000B64D0
		public static T ConvertJsonValueToEnum<T>(JToken rawJsonValue)
		{
			Type typeFromHandle = typeof(T);
			Utils.Verify(typeFromHandle.IsEnum, "Type must be enum");
			JValue jvalue = rawJsonValue as JValue;
			if (jvalue == null)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotConvertToType(rawJsonValue.ToString(), typeFromHandle.Name), rawJsonValue, null);
			}
			T t;
			try
			{
				t = ConvertHelper.ParseRawEnumValue<T>(jvalue.Value, true, false);
			}
			catch (ArgumentException ex)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotConvertToType(jvalue.Value.ToString(), typeFromHandle.Name), rawJsonValue.Path, rawJsonValue, ex);
			}
			catch (OverflowException ex2)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotConvertToType(jvalue.Value.ToString(), typeFromHandle.Name), rawJsonValue.Path, rawJsonValue, ex2);
			}
			return t;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000B8398 File Offset: 0x000B6598
		public static T ConvertStringToEnum<T>(string rawValue, JsonTextReader reader)
		{
			Type typeFromHandle = typeof(T);
			Utils.Verify(typeFromHandle.IsEnum, "Type must be enum");
			T t;
			try
			{
				t = (T)((object)Enum.Parse(typeFromHandle, rawValue.ToCSharpCase(), false));
			}
			catch (ArgumentException ex)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotConvertToType(rawValue, typeFromHandle.Name), reader.Path, reader, ex);
			}
			catch (OverflowException ex2)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotConvertToType(rawValue, typeFromHandle.Name), reader.Path, reader, ex2);
			}
			return t;
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x000B8428 File Offset: 0x000B6628
		public static object ConvertStringToJsonValue(string s, SplitMultilineOptions splitOptions = SplitMultilineOptions.None)
		{
			if (splitOptions == SplitMultilineOptions.None)
			{
				return s;
			}
			if (splitOptions != SplitMultilineOptions.Split)
			{
				throw TomInternalException.Create("Unsupported SplitMultilineOptions value : {0}", new object[] { splitOptions });
			}
			string[] array = s.Split(new char[] { JsonPropertyHelper.LineBreaker }, StringSplitOptions.None);
			if (array.Length > 1)
			{
				return array;
			}
			return s;
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x000B8478 File Offset: 0x000B6678
		public static string ConvertJsonValueToString(JToken rawJsonValue)
		{
			if (rawJsonValue.Type == 8)
			{
				return JsonPropertyHelper.ConvertJsonStringToString(rawJsonValue);
			}
			if (rawJsonValue.Type == 2)
			{
				JArray jarray = rawJsonValue as JArray;
				Utils.Verify(jarray != null);
				StringBuilder stringBuilder = new StringBuilder();
				List<JToken> list = jarray.Children().ToList<JToken>();
				for (int i = 0; i < list.Count; i++)
				{
					string text = JsonPropertyHelper.ConvertJsonStringToString(list[i]);
					stringBuilder.Append(text);
					if (i < list.Count - 1)
					{
						stringBuilder.Append(JsonPropertyHelper.LineBreaker);
					}
				}
				return stringBuilder.ToString();
			}
			if (rawJsonValue.Type == 1)
			{
				JObject jobject = rawJsonValue as JObject;
				Utils.Verify(jobject != null);
				StringBuilder stringBuilder2 = new StringBuilder();
				using (StringWriter stringWriter = new StringWriter(stringBuilder2, CultureInfo.InvariantCulture))
				{
					jobject.WriteTo(new JsonTextWriter(stringWriter)
					{
						Formatting = 1
					}, Array.Empty<JsonConverter>());
				}
				return stringBuilder2.ToString();
			}
			throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotConvertToType(rawJsonValue.ToString(), typeof(string).Name), rawJsonValue, null);
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x000B85A0 File Offset: 0x000B67A0
		internal static string ConvertJsonStringToString(JToken rawJsonValue)
		{
			if (rawJsonValue.Type != 8)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotConvertToType(rawJsonValue.ToString(), typeof(string).Name), rawJsonValue, null);
			}
			return JsonPropertyHelper.WrapJsonValueConversion<string>(rawJsonValue);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x000B85D4 File Offset: 0x000B67D4
		public static JObject ConvertStringToJsonObject(string rawString, string propName)
		{
			JObject jobject;
			try
			{
				jobject = JObject.Parse(rawString);
			}
			catch (JsonReaderException ex)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_PropertyHasInvalidJsonContent(propName, ex.Message), ex);
			}
			catch (JsonException ex2)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_PropertyHasInvalidJsonContent(propName, ex2.Message), ex2);
			}
			return jobject;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x000B8630 File Offset: 0x000B6830
		public static string ConvertJsonContentToString(JToken rawJsonContent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (JsonTextWriter jsonTextWriter = new JsonTextWriter(new StringWriter(stringBuilder)))
			{
				jsonTextWriter.Formatting = 1;
				rawJsonContent.WriteTo(jsonTextWriter, Array.Empty<JsonConverter>());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x000B8684 File Offset: 0x000B6884
		private static T WrapJsonValueConversion<T>(JToken rawJsonValue)
		{
			Type typeFromHandle = typeof(T);
			JValue jvalue = rawJsonValue as JValue;
			if (jvalue == null)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotConvertToType(rawJsonValue.ToString(), typeFromHandle.Name), rawJsonValue, null);
			}
			T t;
			try
			{
				t = jvalue.ToObject<T>();
			}
			catch (ArgumentException ex)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotConvertToType(jvalue.ToString(), typeFromHandle.Name), jvalue, ex);
			}
			catch (JsonException ex2)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotConvertToType(jvalue.ToString(), typeFromHandle.Name), jvalue, ex2);
			}
			return t;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x000B871C File Offset: 0x000B691C
		public static bool IsEmptyObjectCollection(JToken jsonArray)
		{
			if (jsonArray.Type != 2)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotProbeMetadataObjectCollectionFromJson, jsonArray, null);
			}
			JArray jarray = jsonArray as JArray;
			Utils.Verify(jarray != null);
			if (jarray.Count == 0)
			{
				return true;
			}
			foreach (JToken jtoken in jarray)
			{
				if (jtoken.Type != 5)
				{
					if (jtoken.Type != 1)
					{
						throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotProbeMetadataObjectCollectionFromJson, jtoken, null);
					}
					Utils.Verify(jtoken is JObject);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x000B87C4 File Offset: 0x000B69C4
		public static void ReadObjectCollection(IMetadataObjectCollection collection, JToken jsonArray, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (jsonArray.Type != 2)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadMetadataObjectCollectionWithTypeFromJson(collection.ItemType.ToString()), jsonArray, null);
			}
			JArray jarray = jsonArray as JArray;
			Utils.Verify(jarray != null);
			foreach (object obj in jarray)
			{
				JToken jtoken = (JToken)obj;
				if (jtoken.Type != 5)
				{
					if (jtoken.Type != 1)
					{
						throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotReadMetadataObjectCollectionWithTypeFromJson(collection.ItemType.ToString()), jtoken, null);
					}
					JObject jobject = jtoken as JObject;
					Utils.Verify(jobject != null);
					MetadataObject metadataObject = ObjectFactory.CreateMetadataObjectFromJsonObject(collection.ItemType, jobject);
					metadataObject.DeserializeFromJsonObject(jobject, options, mode, dbCompatibilityLevel);
					collection.Add(metadataObject);
				}
			}
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x000B88B4 File Offset: 0x000B6AB4
		public static NamedMetadataObject ParseObjectFromJson(ObjectType objectType, JsonTextReader jsonReader, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			Utils.Verify(jsonReader.TokenType == 1);
			JObject jobject = null;
			try
			{
				jobject = JObject.Load(jsonReader);
			}
			catch (JsonReaderException ex)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotDeserializeObjectMalformedInput(Utils.GetUserFriendlyNameOfObjectType(objectType)), ex);
			}
			catch (JsonException ex2)
			{
				throw JsonSerializationUtil.CreateCannotDeserializeObjectException(objectType, ex2.Message, ex2);
			}
			MetadataObject metadataObject = ObjectFactory.CreateMetadataObjectFromJsonObject(objectType, jobject);
			if (!(metadataObject is NamedMetadataObject))
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_CannotDeserializeObjectWrongType(metadataObject.GetType().Name, objectType.ToString()), null);
			}
			try
			{
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
			metadataObject.TryResolveAllCrossLinksInTreeByObjectPath(null);
			return metadataObject as NamedMetadataObject;
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x000B89A0 File Offset: 0x000B6BA0
		public static void ParseArrayOfObjects(JsonTextReader jsonReader, Action<JsonTextReader> parseObjectAction)
		{
			Utils.Verify(parseObjectAction != null);
			jsonReader.VerifyToken(2);
			jsonReader.Read();
			while (jsonReader.TokenType != 14)
			{
				jsonReader.VerifyToken(1);
				parseObjectAction(jsonReader);
			}
			jsonReader.VerifyToken(14);
			jsonReader.Read();
		}

		// Token: 0x04000533 RID: 1331
		private static readonly char LineBreaker = '\n';
	}
}
