using System;
using System.IO;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x02000082 RID: 130
	public sealed class DataShapeResultParser : IDataShapeResultParser
	{
		// Token: 0x060002F3 RID: 755 RVA: 0x00008188 File Offset: 0x00006388
		public static int GetDataShapesCount(JToken dsr)
		{
			DsrVersion dsrVersion = DataShapeResultParser.GetDsrVersion(dsr);
			JArray jarray;
			if (dsrVersion != DsrVersion.V1)
			{
				if (dsrVersion != DsrVersion.V2)
				{
					throw new InvalidOperationException("Unexpected DSR version");
				}
				jarray = dsr[DsrNames.V2.DataShapes] as JArray;
			}
			else
			{
				jarray = dsr[DsrNames.V1.DataShapes] as JArray;
			}
			if (jarray == null)
			{
				return 0;
			}
			return jarray.Count;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000081EC File Offset: 0x000063EC
		public static bool TryGetErrorObject(JToken dsr, out ODataError odataError)
		{
			DsrVersion dsrVersion = DataShapeResultParser.GetDsrVersion(dsr);
			if (dsrVersion != DsrVersion.V1)
			{
				if (dsrVersion != DsrVersion.V2)
				{
					throw new InvalidOperationException("Unexpected DSR version");
				}
				odataError = null;
			}
			else
			{
				JToken odataError2 = DataShapeResultParser.GetODataError(dsr[DsrNames.V1.DataShapes], DsrNames.V1.OdataError);
				odataError = ((odataError2 != null) ? odataError2.ToObject<ODataError>() : null);
			}
			return odataError != null;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00008250 File Offset: 0x00006450
		public static bool TryGetErrorTracingString(JToken dsr, out string odataErrorString)
		{
			DsrVersion dsrVersion = DataShapeResultParser.GetDsrVersion(dsr);
			JToken jtoken;
			if (dsrVersion != DsrVersion.V1)
			{
				if (dsrVersion != DsrVersion.V2)
				{
					throw new InvalidOperationException("Unexpected DSR version");
				}
				jtoken = null;
			}
			else
			{
				jtoken = DataShapeResultParser.GetODataError(dsr[DsrNames.V1.DataShapes], DsrNames.V1.OdataError);
			}
			if (jtoken != null)
			{
				odataErrorString = jtoken.ToString();
			}
			else
			{
				odataErrorString = null;
			}
			return odataErrorString != null;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000082B4 File Offset: 0x000064B4
		private static JToken GetODataError(JToken dataShapes, string odataErrorPropertyName)
		{
			if (dataShapes == null)
			{
				return null;
			}
			JToken first = dataShapes.First;
			if (first == null)
			{
				return null;
			}
			JToken jtoken = first[odataErrorPropertyName];
			if (jtoken == null)
			{
				return null;
			}
			return jtoken;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000082E0 File Offset: 0x000064E0
		public DataShapeResult Parse(Stream result)
		{
			DsrVersion dsrVersion;
			return this.ParseDataShapeResult(result, true, out dsrVersion);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000082F8 File Offset: 0x000064F8
		public DataShapeResult Parse(JToken result)
		{
			DsrVersion dsrVersion;
			return this.ParseDataShapeResult(result, true, out dsrVersion);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00008310 File Offset: 0x00006510
		internal DataShapeResult ParseDataShapeResult(Stream result, bool fillEmptyIntersections, out DsrVersion dsrVersion)
		{
			DataShapeResult dataShapeResult;
			using (NonClosingStreamProxy nonClosingStreamProxy = new NonClosingStreamProxy(result))
			{
				using (JsonReader jsonReader = DataShapeResultParser.CreateJsonReader(nonClosingStreamProxy))
				{
					dataShapeResult = this.ParseDataShapeResult(jsonReader, DataShapeResultParser.Serializer, fillEmptyIntersections, out dsrVersion);
				}
			}
			return dataShapeResult;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00008370 File Offset: 0x00006570
		internal DataShapeResult ParseDataShapeResult(JToken dataShapes, bool fillEmptyIntersections, out DsrVersion dsrVersion)
		{
			DataShapeResult dataShapeResult;
			using (JTokenReader jtokenReader = new JTokenReader(dataShapes))
			{
				dataShapeResult = this.ParseDataShapeResult(jtokenReader, DataShapeResultParser.Serializer, fillEmptyIntersections, out dsrVersion);
			}
			return dataShapeResult;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000083B0 File Offset: 0x000065B0
		internal DataShapeResult ParseDataShapeResult(JsonReader reader, JsonSerializer serializer, bool fillEmptyIntersections, out DsrVersion dsrVersion)
		{
			DateParseHandling dateParseHandling;
			FloatParseHandling floatParseHandling;
			DataShapeResultParser.SetupJsonReaderForDsrParsing(reader, out dateParseHandling, out floatParseHandling);
			DataShapeResult dataShapeResult;
			try
			{
				dsrVersion = DataShapeResultParser.GetDsrVersion(reader);
				DsrVersion dsrVersion2 = dsrVersion;
				if (dsrVersion2 != DsrVersion.V1)
				{
					if (dsrVersion2 != DsrVersion.V2)
					{
						throw new InvalidOperationException(StringUtil.FormatInvariant("DsrVersion {0} not supported by {1}.", dsrVersion, "DataShapeResultParser"));
					}
					dataShapeResult = new DataShapeResultV2Parser(fillEmptyIntersections).ParseDsr(reader, serializer);
				}
				else
				{
					dataShapeResult = DataShapeResultV1Parser.Instance.ParseDsr(reader, serializer);
				}
			}
			finally
			{
				reader.DateParseHandling = dateParseHandling;
				reader.FloatParseHandling = floatParseHandling;
			}
			return dataShapeResult;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000843C File Offset: 0x0000663C
		public static JsonReader CreateJsonReader(TextReader textReader)
		{
			JsonTextReader jsonTextReader = new JsonTextReader(textReader);
			DateParseHandling dateParseHandling;
			FloatParseHandling floatParseHandling;
			DataShapeResultParser.SetupJsonReaderForDsrParsing(jsonTextReader, out dateParseHandling, out floatParseHandling);
			return jsonTextReader;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000845C File Offset: 0x0000665C
		public static JToken ReadToJToken(string value)
		{
			JToken jtoken;
			using (JsonReader jsonReader = DataShapeResultParser.CreateJsonReader(new StringReader(value)))
			{
				jtoken = JToken.ReadFrom(jsonReader);
			}
			return jtoken;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000849C File Offset: 0x0000669C
		public static JObject LoadJObject(string value)
		{
			JObject jobject;
			using (JsonReader jsonReader = DataShapeResultParser.CreateJsonReader(new StringReader(value)))
			{
				jobject = JObject.Load(jsonReader);
			}
			return jobject;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000084DC File Offset: 0x000066DC
		private static JsonReader CreateJsonReader(Stream stream)
		{
			return DataShapeResultParser.CreateJsonReader(new StreamReader(stream));
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000084E9 File Offset: 0x000066E9
		internal static DsrVersion GetDsrVersion(Stream stream)
		{
			return DataShapeResultParser.GetDsrVersion(DataShapeResultParser.CreateJsonReader(stream));
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000084F8 File Offset: 0x000066F8
		private static DsrVersion GetDsrVersion(JToken token)
		{
			DsrVersion dsrVersion;
			using (JTokenReader jtokenReader = new JTokenReader(token))
			{
				dsrVersion = DataShapeResultParser.GetDsrVersion(jtokenReader);
			}
			return dsrVersion;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00008530 File Offset: 0x00006730
		private static void SetupJsonReaderForDsrParsing(JsonReader reader, out DateParseHandling originalDateParseHandling, out FloatParseHandling originalFloatParseHandling)
		{
			originalDateParseHandling = reader.DateParseHandling;
			reader.DateParseHandling = DateParseHandling.None;
			originalFloatParseHandling = reader.FloatParseHandling;
			reader.FloatParseHandling = FloatParseHandling.Double;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00008550 File Offset: 0x00006750
		private static DsrVersion GetDsrVersion(JsonReader reader)
		{
			if (reader.TokenType == JsonToken.None)
			{
				reader.Read();
			}
			if (reader.Read() && reader.TokenType == JsonToken.PropertyName && (string)reader.Value == DsrNames.V2.Version)
			{
				reader.Read();
				DsrVersion dsrVersion = DataShapeResultParser.Serializer.Deserialize<DsrVersion>(reader);
				reader.Read();
				if (reader.TokenType == JsonToken.PropertyName && (string)reader.Value == DsrNames.V2.MinorVersion)
				{
					reader.Read();
					reader.Read();
				}
				return dsrVersion;
			}
			return DsrVersion.V1;
		}

		// Token: 0x040001B0 RID: 432
		public static readonly DataShapeResultParser Instance = new DataShapeResultParser();

		// Token: 0x040001B1 RID: 433
		private static readonly JsonSerializer Serializer = JsonSerializer.Create();
	}
}
