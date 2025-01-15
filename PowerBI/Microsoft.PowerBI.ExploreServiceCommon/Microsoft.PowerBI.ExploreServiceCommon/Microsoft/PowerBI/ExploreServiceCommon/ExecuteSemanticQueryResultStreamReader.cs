using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;
using Microsoft.InfoNav.Data.PrimitiveValues;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.ExploreServiceCommon
{
	// Token: 0x02000023 RID: 35
	public sealed class ExecuteSemanticQueryResultStreamReader : IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x06000102 RID: 258 RVA: 0x000042B4 File Offset: 0x000024B4
		internal ExecuteSemanticQueryResultStreamReader(IEnumerable<Tuple<string, string>> primarySelectsMap, QueryBindingDescriptor bindingDescriptor, Stream dsrStream)
		{
			this.m_dsrStream = dsrStream;
			this.m_bindingDescriptor = bindingDescriptor;
			this.Initialize(primarySelectsMap);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000042E7 File Offset: 0x000024E7
		internal ExecuteSemanticQueryResultStreamReader(IEnumerable<Tuple<string, string>> primarySelectsMap, Stream descriptorStream, Stream dsrStream)
			: this(primarySelectsMap, ExecuteSemanticQueryResultStreamReader.ParseBindingDescriptor(descriptorStream), dsrStream)
		{
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000042F7 File Offset: 0x000024F7
		// (set) Token: 0x06000105 RID: 261 RVA: 0x000042FF File Offset: 0x000024FF
		internal string DataShapeId { get; private set; }

		// Token: 0x17000024 RID: 36
		public object this[string name]
		{
			get
			{
				int num = this.m_columnNameToColumnIndexMap[name];
				return this.m_currentDataRow[num];
			}
		}

		// Token: 0x17000025 RID: 37
		public object this[int i]
		{
			get
			{
				return this.m_currentDataRow[i];
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004334 File Offset: 0x00002534
		public int Depth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004337 File Offset: 0x00002537
		public int RecordsAffected
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000433A File Offset: 0x0000253A
		public int FieldCount
		{
			get
			{
				return this.m_schemaTable.Columns.Count;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000434C File Offset: 0x0000254C
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00004354 File Offset: 0x00002554
		public long RowsRead { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000435D File Offset: 0x0000255D
		public long TotalTimeMilliseconds
		{
			get
			{
				return this.m_totalTimeStopWatch.ElapsedMilliseconds;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000436A File Offset: 0x0000256A
		public long ParsingTimeMilliseconds
		{
			get
			{
				return this.m_parsingTimeStopWatch.ElapsedMilliseconds;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00004377 File Offset: 0x00002577
		// (set) Token: 0x06000110 RID: 272 RVA: 0x0000437F File Offset: 0x0000257F
		public bool IsODataError { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004388 File Offset: 0x00002588
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00004390 File Offset: 0x00002590
		public bool IsClosed { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00004399 File Offset: 0x00002599
		// (set) Token: 0x06000114 RID: 276 RVA: 0x000043A1 File Offset: 0x000025A1
		public IList<string> ExceededDataLimitsIds { get; private set; }

		// Token: 0x06000115 RID: 277 RVA: 0x000043AC File Offset: 0x000025AC
		private void Initialize(IEnumerable<Tuple<string, string>> primarySelectsMap)
		{
			this.m_totalTimeStopWatch.Start();
			this.m_parsingTimeStopWatch.Start();
			this.RowsRead = 0L;
			ExecuteSemanticQueryResultStreamReader.ValidateBindingDescriptorIsSupported(this.m_bindingDescriptor);
			this.CreateSchemaTable(primarySelectsMap);
			this.m_currentDataRow = new object[this.m_schemaTable.Columns.Count];
			this.m_jsonDataShapeResultReader = new JsonTextReader(new StreamReader(this.m_dsrStream))
			{
				CloseInput = false
			};
			this.m_jsonDataShapeResultReader.Read();
			this.ReadObjectStart();
			this.ReadArrayPropertyHead("DataShapes");
			this.ReadObjectStart();
			if (this.IsPropertyName("Id"))
			{
				this.DataShapeId = this.ReadProperty("Id");
			}
			if (this.IsODataErrorProperty())
			{
				this.IsODataError = true;
				return;
			}
			if (this.m_bindingDescriptor == null)
			{
				throw new SemanticQueryNotSupportedForPostProcessing();
			}
			while (!this.ReadPrimaryHierachyHead())
			{
				this.ReadDataShapeMessageCollection();
				this.ReadCalculationCollection();
				this.ReadDataShapeCollection();
				this.ReadSecondaryHierachy();
			}
			this.m_parsingTimeStopWatch.Stop();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000044AB File Offset: 0x000026AB
		private bool IsODataErrorProperty()
		{
			return this.IsPropertyName("odata.error");
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000044B8 File Offset: 0x000026B8
		public static void ValidateBindingDescriptorIsSupported(QueryBindingDescriptor bindingDescriptor)
		{
			if (bindingDescriptor == null || bindingDescriptor.Expressions == null)
			{
				return;
			}
			if (bindingDescriptor.Expressions.Primary == null || bindingDescriptor.Expressions.Primary.Groupings == null)
			{
				throw new SemanticQueryNotSupportedForPostProcessing();
			}
			if (bindingDescriptor.Expressions.Primary.Groupings.Count != 1)
			{
				throw new SemanticQueryNotSupportedForPostProcessing();
			}
			if (bindingDescriptor.Expressions.Secondary != null)
			{
				throw new SemanticQueryNotSupportedForPostProcessing();
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004527 File Offset: 0x00002727
		private static QueryBindingDescriptor ParseBindingDescriptor(Stream descriptorStream)
		{
			return (QueryBindingDescriptor)new DataContractJsonSerializer(typeof(QueryBindingDescriptor)).ReadObject(descriptorStream);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004544 File Offset: 0x00002744
		private void CreateSchemaTable(IEnumerable<Tuple<string, string>> primarySelectsMap)
		{
			this.m_schemaTable = new DataTable();
			this.m_calculationIdToColumnIndexMap = new Dictionary<string, IList<int>>();
			this.m_columnNameToColumnIndexMap = new Dictionary<string, int>();
			foreach (Tuple<string, string> tuple in primarySelectsMap)
			{
				DataColumn dataColumn = this.m_schemaTable.Columns.Add(tuple.Item2);
				this.m_columnNameToColumnIndexMap[dataColumn.ColumnName] = dataColumn.Ordinal;
				IList<int> list;
				if (!this.m_calculationIdToColumnIndexMap.TryGetValue(tuple.Item1, out list))
				{
					list = new List<int>();
					this.m_calculationIdToColumnIndexMap[tuple.Item1] = list;
				}
				list.Add(dataColumn.Ordinal);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004610 File Offset: 0x00002810
		private bool ReadPrimaryHierachyHead()
		{
			if (!this.IsPropertyName("PrimaryHierarchy"))
			{
				return false;
			}
			this.ReadArrayPropertyHead("PrimaryHierarchy");
			for (;;)
			{
				this.ReadObjectStart();
				string memberId = this.ReadProperty("Id");
				if (this.m_bindingDescriptor.Expressions == null || !this.m_bindingDescriptor.Expressions.Primary.Groupings.Any((DataShapeExpressionsAxisGrouping ag) => memberId.Equals(ag.SubtotalMember)))
				{
					break;
				}
				this.EatObjectToEnd();
			}
			this.ReadArrayPropertyHead("Instances");
			return true;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000046A0 File Offset: 0x000028A0
		private void EatObjectToEnd()
		{
			this.m_jsonDataShapeResultReader.Read();
			int num = 1;
			while (this.m_jsonDataShapeResultReader.Read() && num != 0)
			{
				if (this.m_jsonDataShapeResultReader.TokenType == JsonToken.StartObject)
				{
					num++;
				}
				if (this.m_jsonDataShapeResultReader.TokenType == JsonToken.EndObject)
				{
					num--;
				}
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000046F2 File Offset: 0x000028F2
		private void ReadSecondaryHierachy()
		{
			this.EatArrayProperty("SecondaryHierarchy");
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000046FF File Offset: 0x000028FF
		private void ReadDataShapeCollection()
		{
			this.EatArrayProperty("DataShapes");
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000470C File Offset: 0x0000290C
		private void ReadCalculationCollection()
		{
			this.EatArrayProperty("Calculations");
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004719 File Offset: 0x00002919
		private void ReadDataShapeMessageCollection()
		{
			this.EatArrayProperty("DataShapeMessages");
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004726 File Offset: 0x00002926
		private void EatArrayProperty(string propertyName)
		{
			if (this.m_jsonDataShapeResultReader.TokenType != JsonToken.PropertyName)
			{
				throw new InvalidDataShapeResultJsonFormat();
			}
			if (!propertyName.Equals(this.m_jsonDataShapeResultReader.Value))
			{
				return;
			}
			this.EatArrayToEnd();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004758 File Offset: 0x00002958
		private void EatArrayToEnd()
		{
			int num = 1;
			if (this.m_jsonDataShapeResultReader.TokenType == JsonToken.EndArray)
			{
				num = 0;
			}
			this.m_jsonDataShapeResultReader.Read();
			while (num != 0 && this.m_jsonDataShapeResultReader.Read())
			{
				if (this.m_jsonDataShapeResultReader.TokenType == JsonToken.StartArray)
				{
					num++;
				}
				if (this.m_jsonDataShapeResultReader.TokenType == JsonToken.EndArray)
				{
					num--;
				}
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000047BB File Offset: 0x000029BB
		private void ReadArrayPropertyHead(string propertyName)
		{
			if (!this.IsPropertyName(propertyName))
			{
				throw new InvalidDataShapeResultJsonFormat();
			}
			this.m_jsonDataShapeResultReader.Read();
			if (this.m_jsonDataShapeResultReader.TokenType != JsonToken.StartArray)
			{
				throw new InvalidDataShapeResultJsonFormat();
			}
			this.m_jsonDataShapeResultReader.Read();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000047F8 File Offset: 0x000029F8
		private bool IsPropertyName(string propertyName)
		{
			return this.m_jsonDataShapeResultReader.TokenType == JsonToken.PropertyName && propertyName.Equals(this.m_jsonDataShapeResultReader.Value);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000481C File Offset: 0x00002A1C
		private string ReadProperty(string propertyName)
		{
			if (!this.IsPropertyName(propertyName))
			{
				throw new InvalidDataShapeResultJsonFormat();
			}
			this.m_jsonDataShapeResultReader.Read();
			if (this.m_jsonDataShapeResultReader.TokenType != JsonToken.String)
			{
				throw new InvalidDataShapeResultJsonFormat();
			}
			string text = this.m_jsonDataShapeResultReader.Value.ToString();
			this.m_jsonDataShapeResultReader.Read();
			return text;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004875 File Offset: 0x00002A75
		private void ReadObjectStart()
		{
			if (this.m_jsonDataShapeResultReader.TokenType != JsonToken.StartObject)
			{
				throw new InvalidDataShapeResultJsonFormat();
			}
			this.m_jsonDataShapeResultReader.Read();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004897 File Offset: 0x00002A97
		private void ReadObjectEnd()
		{
			if (this.m_jsonDataShapeResultReader.TokenType != JsonToken.EndObject)
			{
				throw new InvalidDataShapeResultJsonFormat();
			}
			this.m_jsonDataShapeResultReader.Read();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000048BA File Offset: 0x00002ABA
		private void ReadArrayStart()
		{
			if (this.m_jsonDataShapeResultReader.TokenType != JsonToken.StartArray)
			{
				throw new InvalidDataShapeResultJsonFormat();
			}
			this.m_jsonDataShapeResultReader.Read();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000048DC File Offset: 0x00002ADC
		private void ReadArrayEnd()
		{
			if (this.m_jsonDataShapeResultReader.TokenType != JsonToken.EndArray)
			{
				throw new InvalidDataShapeResultJsonFormat();
			}
			this.m_jsonDataShapeResultReader.Read();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004900 File Offset: 0x00002B00
		private void ReadNextRow()
		{
			if (this.IsClosed || this.IsODataError || this.m_reachedEnd)
			{
				this.m_totalTimeStopWatch.Stop();
				this.m_parsingTimeStopWatch.Stop();
				return;
			}
			this.m_parsingTimeStopWatch.Start();
			if (this.m_jsonDataShapeResultReader.TokenType != JsonToken.StartObject)
			{
				this.m_reachedEnd = true;
				this.ReadDataShapeEpilog();
				this.m_totalTimeStopWatch.Stop();
				this.m_parsingTimeStopWatch.Stop();
				return;
			}
			JObject jobject = JObject.Load(this.m_jsonDataShapeResultReader);
			this.ReadObjectEnd();
			Array.Clear(this.m_currentDataRow, 0, this.m_currentDataRow.Length);
			foreach (JToken jtoken in ((IEnumerable<JToken>)jobject["Calculations"]))
			{
				string text = jtoken["Id"].ToString();
				IList<int> list;
				if (this.m_calculationIdToColumnIndexMap.TryGetValue(text, out list))
				{
					foreach (int num in list)
					{
						this.SetColumnValue(num, jtoken);
					}
				}
			}
			long num2 = this.RowsRead + 1L;
			this.RowsRead = num2;
			this.m_parsingTimeStopWatch.Stop();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004A5C File Offset: 0x00002C5C
		private void ReadDataShapeEpilog()
		{
			this.ReadArrayEnd();
			this.ReadObjectEnd();
			this.EatArrayToEnd();
			while (this.m_jsonDataShapeResultReader.TokenType == JsonToken.PropertyName && !"DataLimitsExceeded".Equals(this.m_jsonDataShapeResultReader.Value))
			{
				this.m_jsonDataShapeResultReader.Read();
				if (this.m_jsonDataShapeResultReader.TokenType == JsonToken.StartObject)
				{
					this.EatObjectToEnd();
				}
				else if (this.m_jsonDataShapeResultReader.TokenType == JsonToken.StartArray)
				{
					this.EatArrayToEnd();
				}
				else
				{
					this.m_jsonDataShapeResultReader.Read();
				}
			}
			if (this.IsPropertyName("DataLimitsExceeded"))
			{
				this.m_jsonDataShapeResultReader.Read();
				JArray jarray = JArray.Load(this.m_jsonDataShapeResultReader);
				this.ExceededDataLimitsIds = jarray.Select((JToken dataLimit) => dataLimit["Id"].ToString()).ToList<string>();
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004B3C File Offset: 0x00002D3C
		private void SetColumnValue(int columnIndex, JToken calculation)
		{
			JValue jvalue = calculation["Value"] as JValue;
			object obj = null;
			if (jvalue != null && jvalue.Type != JTokenType.Null)
			{
				string text = jvalue.ToString(CultureInfo.InvariantCulture);
				PrimitiveValue primitiveValue;
				obj = (PrimitiveValueEncoding.TryParseTypeEncodedString(text, out primitiveValue) ? primitiveValue.GetValueAsObject() : text);
			}
			this.m_currentDataRow[columnIndex] = obj;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004B92 File Offset: 0x00002D92
		public void Close()
		{
			this.m_jsonDataShapeResultReader.Close();
			this.IsClosed = true;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004BA6 File Offset: 0x00002DA6
		public DataTable GetSchemaTable()
		{
			return new DataTableReader(this.m_schemaTable).GetSchemaTable();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004BB8 File Offset: 0x00002DB8
		public bool NextResult()
		{
			this.ReadNextRow();
			return !this.m_reachedEnd && !this.IsClosed && !this.IsODataError;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004BDB File Offset: 0x00002DDB
		public bool Read()
		{
			return this.NextResult();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004BE3 File Offset: 0x00002DE3
		public void Dispose()
		{
			if (!this.IsClosed)
			{
				this.Close();
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004BF3 File Offset: 0x00002DF3
		public string GetDataTypeName(int i)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { this.GetFieldType(i) });
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004C14 File Offset: 0x00002E14
		public Type GetFieldType(int i)
		{
			if (this[i] == null)
			{
				return null;
			}
			return this[i].GetType();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004C2D File Offset: 0x00002E2D
		public string GetName(int i)
		{
			return this.m_schemaTable.Columns[i].ColumnName;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004C45 File Offset: 0x00002E45
		public int GetOrdinal(string name)
		{
			return this.m_columnNameToColumnIndexMap[name];
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004C53 File Offset: 0x00002E53
		public object GetValue(int i)
		{
			return this[i];
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004C5C File Offset: 0x00002E5C
		public bool IsDBNull(int i)
		{
			return this[i] == null;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004C68 File Offset: 0x00002E68
		public string GetString(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004C6F File Offset: 0x00002E6F
		public float GetFloat(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004C76 File Offset: 0x00002E76
		public Guid GetGuid(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004C7D File Offset: 0x00002E7D
		public short GetInt16(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004C84 File Offset: 0x00002E84
		public int GetInt32(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004C8B File Offset: 0x00002E8B
		public long GetInt64(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004C92 File Offset: 0x00002E92
		public bool GetBoolean(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004C99 File Offset: 0x00002E99
		public byte GetByte(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004CA0 File Offset: 0x00002EA0
		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004CA7 File Offset: 0x00002EA7
		public char GetChar(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004CAE File Offset: 0x00002EAE
		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004CB5 File Offset: 0x00002EB5
		public IDataReader GetData(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004CBC File Offset: 0x00002EBC
		public DateTime GetDateTime(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004CC3 File Offset: 0x00002EC3
		public decimal GetDecimal(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004CCA File Offset: 0x00002ECA
		public double GetDouble(int i)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004CD1 File Offset: 0x00002ED1
		public int GetValues(object[] values)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004CD8 File Offset: 0x00002ED8
		public static IList<Tuple<string, string>> CreatePrimarySelectsMap(QueryBindingDescriptor queryBindingDescriptor, ScriptInput scriptInput, SemanticQueryDataShapeCommand command)
		{
			return ExecuteSemanticQueryResultStreamReader.CreatePrimarySelectsMap(queryBindingDescriptor, (scriptInput != null) ? scriptInput.Columns : null, command);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004CF4 File Offset: 0x00002EF4
		public static IList<Tuple<string, string>> CreatePrimarySelectsMap(QueryBindingDescriptor queryBindingDescriptor, IEnumerable<DataQueryColumn> columns, SemanticQueryDataShapeCommand command)
		{
			ExecuteSemanticQueryResultStreamReader.ValidateBindingDescriptorIsSupported(queryBindingDescriptor);
			if (columns == null)
			{
				throw new ArgumentNullException("columns");
			}
			return columns.Select(delegate(DataQueryColumn dataQueryColumn)
			{
				int num = command.Query.Select.FindIndex((QueryExpressionContainer select) => select.Name == dataQueryColumn.QueryName);
				return Tuple.Create<string, string>(queryBindingDescriptor.Select[num].Value, dataQueryColumn.Name);
			}).ToList<Tuple<string, string>>();
		}

		// Token: 0x040000D0 RID: 208
		private readonly QueryBindingDescriptor m_bindingDescriptor;

		// Token: 0x040000D1 RID: 209
		private readonly Stream m_dsrStream;

		// Token: 0x040000D2 RID: 210
		private JsonReader m_jsonDataShapeResultReader;

		// Token: 0x040000D3 RID: 211
		private DataTable m_schemaTable;

		// Token: 0x040000D4 RID: 212
		private Dictionary<string, IList<int>> m_calculationIdToColumnIndexMap;

		// Token: 0x040000D5 RID: 213
		private Dictionary<string, int> m_columnNameToColumnIndexMap;

		// Token: 0x040000D6 RID: 214
		private object[] m_currentDataRow;

		// Token: 0x040000D7 RID: 215
		private bool m_reachedEnd;

		// Token: 0x040000D8 RID: 216
		private Stopwatch m_totalTimeStopWatch = new Stopwatch();

		// Token: 0x040000D9 RID: 217
		private Stopwatch m_parsingTimeStopWatch = new Stopwatch();
	}
}
