using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200005C RID: 92
	internal sealed class XmlaDataReader : Disposable, IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x06000477 RID: 1143 RVA: 0x0001D506 File Offset: 0x0001B706
		internal XmlaDataReader(XmlReader xmlReader, CommandBehavior commandBehavior = CommandBehavior.Default)
			: this(xmlReader, commandBehavior, false, null)
		{
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001D514 File Offset: 0x0001B714
		internal XmlaDataReader(XmlReader xmlReader, CommandBehavior commandBehavior, bool isXmlReaderAtRoot, IXmlaDataReaderOwner owner)
		{
			this.columnNameLookup = new Hashtable();
			this.columnXmlNameLookup = new Hashtable();
			this.rowsetNames = new List<string>();
			this.rowElement = FormattersHelpers.RowElement;
			this.rowNamespace = FormattersHelpers.RowElementNamespace;
			this.readersXmlDepth = -1;
			base..ctor();
			try
			{
				this.InternalInitialize(xmlReader, commandBehavior, owner);
				if (!isXmlReaderAtRoot)
				{
					XmlaClient.ReadUptoRoot(xmlReader);
				}
				this.IsAffectedObjects = XmlaClient.IsAffectedObjectsResponseS(xmlReader);
				this.isMultipleResult = this.IsAffectedObjects || XmlaClient.IsMultipleResultResponseS(xmlReader);
				this.Results = new XmlaResultCollection();
				this.CollectTopLevelAttributes();
				if (XmlaClient.IsRowsetResponseS(xmlReader))
				{
					this.RowsetName = xmlReader.GetAttribute("name");
					this.rowsetNames.Add(this.RowsetName);
					this.EnsureResultForNewRowset();
					XmlaClient.StartRowsetResponseS(xmlReader);
					this.LoadResponseSchema();
				}
				else if (XmlaClient.IsMultipleResultResponseS(xmlReader) || XmlaClient.IsAffectedObjectsResponseS(xmlReader))
				{
					XmlaClient.StartElementS(xmlReader, this.IsAffectedObjects ? "AffectedObjects" : "results", "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults");
					if (XmlaClient.IsRootElementS(xmlReader))
					{
						this.InternalNextResult(true);
					}
					else
					{
						if (!this.IsAffectedObjects)
						{
							throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, "Expected at least one root element");
						}
						this.StartEmptyAffectedObjects();
					}
				}
				else
				{
					if (XmlaClient.IsDatasetResponseS(xmlReader))
					{
						throw new ResponseFormatException(XmlaSR.Resultset_IsNotRowset, string.Format(CultureInfo.InvariantCulture, "Got {0}:{1}", "urn:schemas-microsoft-com:xml-analysis:mddataset", "root"));
					}
					if (!XmlaClient.IsEmptyResultS(xmlReader))
					{
						throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1}, got {2}", "urn:schemas-microsoft-com:xml-analysis:empty", "root", xmlReader.Name));
					}
					XmlaClient.ReadEmptyRootS(xmlReader);
					throw new ResponseFormatException(XmlaSR.Resultset_IsNotRowset, string.Format(CultureInfo.InvariantCulture, "Unexpected node {0}", xmlReader.Name));
				}
			}
			catch (ResponseFormatException)
			{
				xmlReader.Close();
				throw;
			}
			catch (ConnectionException)
			{
				throw;
			}
			catch (OperationException)
			{
				xmlReader.Close();
				throw;
			}
			catch (XmlException ex)
			{
				xmlReader.Close();
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			catch (IOException ex2)
			{
				if (this.owner != null)
				{
					owner.CloseConnection(false);
				}
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex2);
			}
			catch
			{
				if (this.owner != null)
				{
					owner.CloseConnection(false);
				}
				throw;
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001D7C8 File Offset: 0x0001B9C8
		private XmlaDataReader(XmlaDataReader parentReader)
		{
			this.columnNameLookup = new Hashtable();
			this.columnXmlNameLookup = new Hashtable();
			this.rowsetNames = new List<string>();
			this.rowElement = FormattersHelpers.RowElement;
			this.rowNamespace = FormattersHelpers.RowElementNamespace;
			this.readersXmlDepth = -1;
			base..ctor();
			this.parentReader = parentReader;
			this.InternalInitialize(parentReader.xmlReader, parentReader.commandBehavior, parentReader.owner);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001D838 File Offset: 0x0001BA38
		private XmlaDataReader(XmlaDataReader parentReader, XmlaDataReader nested)
		{
			this.columnNameLookup = new Hashtable();
			this.columnXmlNameLookup = new Hashtable();
			this.rowsetNames = new List<string>();
			this.rowElement = FormattersHelpers.RowElement;
			this.rowNamespace = FormattersHelpers.RowElementNamespace;
			this.readersXmlDepth = -1;
			base..ctor();
			this.xmlReader = nested.xmlReader;
			this.commandBehavior = nested.commandBehavior & ~CommandBehavior.SequentialAccess;
			this.owner = nested.owner;
			this.schemaTable = nested.schemaTable;
			this.sequentialAccess = false;
			this.columnCount = nested.columnCount;
			this.currentColumn = 0;
			this.dataReady = false;
			this.columnNameLookup = nested.columnNameLookup;
			this.columnXmlNameLookup = nested.columnXmlNameLookup;
			this.depth = nested.depth;
			this.isClosed = false;
			this.parentReader = nested.parentReader;
			this.dtStore = nested.dtStore;
			this.currentRow = -1;
			this.currentParentRow = nested.currentParentRow;
			this.rowElement = nested.rowElement;
			this.rowNamespace = nested.rowNamespace;
			this.nestedDataReaders = new XmlaDataReader[nested.nestedDataReaders.Length];
			for (int i = 0; i < nested.nestedDataReaders.Length; i++)
			{
				if (nested.nestedDataReaders[i] != null)
				{
					this.nestedDataReaders[i] = new XmlaDataReader(nested, nested.nestedDataReaders[i]);
				}
				else
				{
					this.nestedDataReaders[i] = null;
				}
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0001D99C File Offset: 0x0001BB9C
		public int Depth
		{
			get
			{
				return this.depth;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0001D9A4 File Offset: 0x0001BBA4
		public int FieldCount
		{
			get
			{
				return this.columnCount;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0001D9AC File Offset: 0x0001BBAC
		public bool IsClosed
		{
			get
			{
				return this.isClosed;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0001D9B4 File Offset: 0x0001BBB4
		public int RecordsAffected
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170000E2 RID: 226
		public object this[int index]
		{
			get
			{
				return this.GetValue(index);
			}
		}

		// Token: 0x170000E3 RID: 227
		public object this[string columnName]
		{
			get
			{
				int ordinal = this.GetOrdinal(columnName);
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0001D9DC File Offset: 0x0001BBDC
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0001D9E4 File Offset: 0x0001BBE4
		internal bool IsAffectedObjects { get; private set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0001D9ED File Offset: 0x0001BBED
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0001D9F5 File Offset: 0x0001BBF5
		internal string RowsetName { get; private set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0001D9FE File Offset: 0x0001BBFE
		internal List<string> RowsetNames
		{
			get
			{
				return this.rowsetNames;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0001DA06 File Offset: 0x0001BC06
		internal XmlReader XmlReader
		{
			get
			{
				return this.xmlReader;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0001DA0E File Offset: 0x0001BC0E
		internal CommandBehavior CommandBehavior
		{
			get
			{
				return this.commandBehavior;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0001DA16 File Offset: 0x0001BC16
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x0001DA1E File Offset: 0x0001BC1E
		internal XmlaResultCollection Results { get; private set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0001DA27 File Offset: 0x0001BC27
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x0001DA2F File Offset: 0x0001BC2F
		internal Dictionary<XName, string> TopLevelAttributes { get; private set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0001DA38 File Offset: 0x0001BC38
		internal int ParentId
		{
			get
			{
				if (this.parentReader == null)
				{
					return -1;
				}
				return this.FieldCount;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0001DA4A File Offset: 0x0001BC4A
		private XmlaResult CurrentResult
		{
			get
			{
				if (this.parentReader != null)
				{
					return this.parentReader.CurrentResult;
				}
				return this.Results[this.Results.Count - 1];
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001DA78 File Offset: 0x0001BC78
		public DataTable GetSchemaTable()
		{
			return this.schemaTable;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001DA80 File Offset: 0x0001BC80
		public Type GetFieldType(int ordinal)
		{
			return (Type)this.schemaTable.Rows[ordinal]["DataType"];
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001DAA2 File Offset: 0x0001BCA2
		public string GetName(int ordinal)
		{
			return (string)this.schemaTable.Rows[ordinal]["ColumnName"];
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001DAC4 File Offset: 0x0001BCC4
		public int GetOrdinal(string name)
		{
			object obj = this.columnNameLookup[name];
			if (obj == null)
			{
				throw new ArgumentException(XmlaSR.InvalidArgument);
			}
			return (int)obj;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001DAE5 File Offset: 0x0001BCE5
		public bool NextResult()
		{
			return this.isMultipleResult && this.InternalNextResult(false);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001DAF8 File Offset: 0x0001BCF8
		public bool IsDBNull(int ordinal)
		{
			object value = this.GetValue(ordinal);
			return value == null || Convert.IsDBNull(value);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001DB18 File Offset: 0x0001BD18
		public IDataReader GetData(int ordinal)
		{
			return (IDataReader)this.GetValue(ordinal);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001DB26 File Offset: 0x0001BD26
		public string GetDataTypeName(int index)
		{
			return this.GetFieldType(index).Name;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001DB34 File Offset: 0x0001BD34
		public bool GetBoolean(int ordinal)
		{
			return (bool)this.InternalGetValue(ordinal);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001DB42 File Offset: 0x0001BD42
		public byte GetByte(int ordinal)
		{
			return (byte)this.InternalGetValue(ordinal);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001DB50 File Offset: 0x0001BD50
		public long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001DB57 File Offset: 0x0001BD57
		public char GetChar(int ordinal)
		{
			return (char)this.InternalGetValue(ordinal);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001DB68 File Offset: 0x0001BD68
		public long GetChars(int ordinal, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			char[] array = ((string)this.InternalGetValue(ordinal)).ToCharArray();
			int num = array.Length;
			if (dataIndex > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("dataIndex", XmlaSR.DataReader_IndexOutOfRange);
			}
			int num2 = (int)dataIndex;
			if (buffer == null)
			{
				return (long)num;
			}
			if (num2 < 0 || num2 >= num)
			{
				return 0L;
			}
			if (num2 < num)
			{
				if (num2 + length > num)
				{
					num -= num2;
				}
				else
				{
					num = length;
				}
			}
			Array.Copy(array, num2, buffer, bufferIndex, num);
			return (long)num;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001DBDB File Offset: 0x0001BDDB
		public DateTime GetDateTime(int ordinal)
		{
			return (DateTime)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(DateTime), CultureInfo.InvariantCulture);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001DBFD File Offset: 0x0001BDFD
		public decimal GetDecimal(int ordinal)
		{
			return (decimal)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(decimal), CultureInfo.InvariantCulture);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001DC1F File Offset: 0x0001BE1F
		public double GetDouble(int ordinal)
		{
			return (double)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(double), CultureInfo.InvariantCulture);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001DC41 File Offset: 0x0001BE41
		public float GetFloat(int ordinal)
		{
			return (float)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(float), CultureInfo.InvariantCulture);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001DC63 File Offset: 0x0001BE63
		public Guid GetGuid(int ordinal)
		{
			return (Guid)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(Guid), CultureInfo.InvariantCulture);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001DC85 File Offset: 0x0001BE85
		public short GetInt16(int ordinal)
		{
			return (short)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(short), CultureInfo.InvariantCulture);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001DCA7 File Offset: 0x0001BEA7
		public int GetInt32(int ordinal)
		{
			return (int)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(int), CultureInfo.InvariantCulture);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001DCC9 File Offset: 0x0001BEC9
		public long GetInt64(int ordinal)
		{
			return (long)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(long), CultureInfo.InvariantCulture);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001DCEB File Offset: 0x0001BEEB
		public string GetString(int ordinal)
		{
			return this.InternalGetValue(ordinal).ToString();
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001DCF9 File Offset: 0x0001BEF9
		public TimeSpan GetTimeSpan(int ordinal)
		{
			return (TimeSpan)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(TimeSpan), CultureInfo.InvariantCulture);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001DD1C File Offset: 0x0001BF1C
		public object GetValue(int ordinal)
		{
			object obj = this.InternalGetValue(ordinal);
			if (FormattersHelpers.GetColumnXsdTypeName(this.dtStore.Columns[ordinal]) == "xmlDocument")
			{
				ColumnXmlReader columnXmlReader = obj as ColumnXmlReader;
				if (columnXmlReader.IsDataSet)
				{
					obj = columnXmlReader.Dataset;
				}
				else
				{
					obj = columnXmlReader;
				}
			}
			return obj;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001DD70 File Offset: 0x0001BF70
		public int GetValues(object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			int num = Math.Min(values.Length, this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				if (this.sequentialAccess && this.nestedDataReaders[i] != null)
				{
					this.currentColumn = i + 1;
					XmlaDataReader xmlaDataReader = new XmlaDataReader(this, this.nestedDataReaders[i]);
					xmlaDataReader.currentParentRow = this.dtStore.Rows.Count;
					while (xmlaDataReader.InternalRead())
					{
					}
					values[i] = xmlaDataReader;
				}
				else
				{
					values[i] = this.GetValue(i);
					if (this.sequentialAccess && values[i] is ColumnXmlReader)
					{
						values[i] = new ColumnXmlReader(values[i].ToString());
					}
				}
			}
			return num;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001DE24 File Offset: 0x0001C024
		public XmlaDataReader GetDataReader(int ordinal)
		{
			return (XmlaDataReader)this.InternalGetValue(ordinal);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001DE34 File Offset: 0x0001C034
		public bool Read()
		{
			if (this.parentReader == null || this.sequentialAccess)
			{
				this.currentRow = 0;
				this.ResetCurrentRowCache();
				return this.InternalRead();
			}
			this.currentRow++;
			this.dataReady = this.currentRow < this.dtStore.Rows.Count;
			if (this.dataReady)
			{
				int num = (int)this.dtStore.Rows[this.currentRow][this.ParentId];
				this.dataReady = num == this.parentReader.currentRow;
			}
			return this.dataReady;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001DEDC File Offset: 0x0001C0DC
		public void Close()
		{
			this.ResetCurrentRowCache();
			this.dataReady = false;
			if (this.Depth == 0 && this.xmlReader != null)
			{
				this.xmlReader.Close();
				if (this.xmlReader != null)
				{
					((IDisposable)this.xmlReader).Dispose();
				}
				this.xmlReader = null;
			}
			this.isClosed = true;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001DF32 File Offset: 0x0001C132
		public XmlaDataReader.Enumerator GetEnumerator()
		{
			return new XmlaDataReader.Enumerator(this);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001DF3A File Offset: 0x0001C13A
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001DF4C File Offset: 0x0001C14C
		private static void ThrowIfInlineError(object columnValue)
		{
			if (columnValue is XmlaError)
			{
				throw new OperationException((XmlaError)columnValue);
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001DF64 File Offset: 0x0001C164
		private static DataTable CreateSchemaTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.Locale = CultureInfo.InvariantCulture;
			for (int i = 0; i < 18; i++)
			{
				dataTable.Columns.Add(XmlaDataReader.schemaTableColumnNames[i], XmlaDataReader.schemaTableColumnTypes[i]);
			}
			return dataTable;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001DFAC File Offset: 0x0001C1AC
		private void CollectTopLevelAttributes()
		{
			int attributeCount = this.xmlReader.AttributeCount;
			for (int i = 0; i < attributeCount; i++)
			{
				this.xmlReader.MoveToNextAttribute();
				if (this.TopLevelAttributes == null)
				{
					this.TopLevelAttributes = new Dictionary<XName, string>();
				}
				XName xname;
				if (!string.IsNullOrEmpty(this.xmlReader.NamespaceURI))
				{
					xname = this.xmlReader.NamespaceURI + this.xmlReader.LocalName;
				}
				else
				{
					xname = this.xmlReader.LocalName;
				}
				this.TopLevelAttributes.Add(xname, this.xmlReader.Value);
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001E053 File Offset: 0x0001C253
		private void StartEmptyAffectedObjects()
		{
			this.EnsureResultForNewRowset();
			this.CheckForMessages();
			this.isMultipleResult = false;
			this.emptyResult = true;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001E070 File Offset: 0x0001C270
		private void SkipRootContents()
		{
			for (;;)
			{
				XmlNodeType nodeType = this.xmlReader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						if ((this.xmlReader.LocalName == "results" || this.xmlReader.LocalName == "AffectedObjects") && this.xmlReader.NamespaceURI == "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults")
						{
							return;
						}
					}
				}
				else
				{
					if (this.xmlReader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:empty") || this.xmlReader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset"))
					{
						break;
					}
					if (this.CheckForMessages())
					{
						continue;
					}
				}
				if (!this.xmlReader.Read())
				{
					return;
				}
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001E123 File Offset: 0x0001C323
		private void EnsureResultForNewRowset()
		{
			if (this.parentReader != null)
			{
				return;
			}
			if (!this.IsAffectedObjects || this.Results.Count == 0)
			{
				this.Results.Add(new XmlaResult());
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001E154 File Offset: 0x0001C354
		private bool CheckForMessages()
		{
			XmlaMessageCollection xmlaMessageCollection = null;
			if (!XmlaClient.CheckForMessages(this.xmlReader, ref xmlaMessageCollection))
			{
				return false;
			}
			XmlaResult currentResult = this.CurrentResult;
			foreach (object obj in ((IEnumerable)xmlaMessageCollection))
			{
				XmlaMessage xmlaMessage = (XmlaMessage)obj;
				currentResult.Messages.Add(xmlaMessage);
			}
			return true;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001E1CC File Offset: 0x0001C3CC
		private bool InternalNextResult(bool first)
		{
			if (!first)
			{
				this.InitResultData();
				this.SkipRootContents();
			}
			if (XmlaClient.IsEmptyResultS(this.xmlReader))
			{
				if (this.IsAffectedObjects)
				{
					throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, "Got an empty result inside AffectedObjects");
				}
				this.RowsetName = null;
				this.xmlReader.ReadStartElement("root", "urn:schemas-microsoft-com:xml-analysis:empty");
				this.GenerateSchemaForEmptyResult();
				this.emptyResult = true;
				this.EnsureResultForNewRowset();
				return true;
			}
			else
			{
				if (this.xmlReader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset"))
				{
					this.RowsetName = this.xmlReader.GetAttribute("name");
					this.rowsetNames.Add(this.RowsetName);
					XmlaClient.StartRowsetResponseS(this.xmlReader);
					this.LoadResponseSchema();
					this.dataReady = true;
					this.emptyResult = false;
					this.EnsureResultForNewRowset();
					return true;
				}
				if (first)
				{
					throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, "Expected at least one root element");
				}
				this.CheckForMessages();
				XmlaClient.EndElementS(this.xmlReader, this.IsAffectedObjects ? "AffectedObjects" : "results", "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults");
				this.isMultipleResult = false;
				return false;
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001E2EC File Offset: 0x0001C4EC
		private int GetOrdinalFromXmlName(string xmlName)
		{
			object obj = this.columnXmlNameLookup[xmlName];
			if (obj == null)
			{
				return -1;
			}
			return (int)obj;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001E314 File Offset: 0x0001C514
		private int GetRowXmlValues(object[] xmlValues)
		{
			int fieldCount = this.FieldCount;
			for (int i = 0; i < fieldCount; i++)
			{
				xmlValues[i] = this.SequentialReadXmlValue(i);
			}
			return fieldCount;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001E340 File Offset: 0x0001C540
		private object SequentialReadXmlValue(int ordinal)
		{
			if (ordinal < this.currentColumn)
			{
				throw new ArgumentException(XmlaSR.NonSequentialColumnAccessError, "ordinal");
			}
			if (ordinal >= this.FieldCount)
			{
				throw new ArgumentOutOfRangeException("ordinal");
			}
			object obj2;
			try
			{
				this.currentColumn = ordinal + 1;
				object obj = null;
				while (this.xmlReader.IsStartElement() && this.xmlReader.Depth == this.readersXmlDepth)
				{
					int ordinalFromXmlName = this.GetOrdinalFromXmlName(this.xmlReader.LocalName);
					if (ordinalFromXmlName == -1)
					{
						FormattersHelpers.CheckException(this.xmlReader);
						throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Unexpected element {0}", this.xmlReader.Name));
					}
					bool flag = false;
					bool flag2 = false;
					if (FormattersHelpers.GetColumnXsdTypeName(this.dtStore.Columns[ordinalFromXmlName]) == "xmlDocument")
					{
						flag = true;
						flag2 = ((XmlaReader)this.xmlReader).SkipElements;
						((XmlaReader)this.xmlReader).SkipElements = false;
					}
					try
					{
						FormattersHelpers.CheckException(this.xmlReader);
					}
					finally
					{
						if (flag)
						{
							((XmlaReader)this.xmlReader).SkipElements = flag2;
						}
					}
					if (ordinalFromXmlName == ordinal)
					{
						if (!FormattersHelpers.IsNullContentElement(this.xmlReader))
						{
							obj = this.ReadColumnValue(ordinal);
							break;
						}
						this.xmlReader.Skip();
						break;
					}
					else
					{
						if (ordinalFromXmlName >= ordinal)
						{
							break;
						}
						string name = this.xmlReader.Name;
						while (this.xmlReader.IsStartElement(name))
						{
							this.xmlReader.Skip();
						}
					}
				}
				obj2 = obj;
			}
			catch (ResponseFormatException)
			{
				if (this.xmlReader != null)
				{
					this.xmlReader.Close();
				}
				throw;
			}
			catch (ConnectionException)
			{
				throw;
			}
			catch (OperationException)
			{
				if (this.xmlReader != null)
				{
					this.xmlReader.Close();
				}
				throw;
			}
			catch (XmlException ex)
			{
				if (this.xmlReader != null)
				{
					this.xmlReader.Close();
				}
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			catch (IOException ex2)
			{
				if (this.owner != null)
				{
					this.owner.CloseConnection(false);
				}
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex2);
			}
			catch
			{
				if (this.owner != null)
				{
					this.owner.CloseConnection(false);
				}
				throw;
			}
			return obj2;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001E5F8 File Offset: 0x0001C7F8
		private object ReadColumnValue(int ordinal)
		{
			Type type = FormattersHelpers.GetElementType(this.xmlReader, "http://www.w3.org/2001/XMLSchema-instance", null);
			if (type == null)
			{
				type = this.GetFieldType(ordinal);
			}
			XmlaDataReader xmlaDataReader = this.nestedDataReaders[ordinal];
			object obj;
			if (xmlaDataReader == null)
			{
				string columnXsdTypeName = FormattersHelpers.GetColumnXsdTypeName(this.dtStore.Columns[ordinal]);
				bool flag = columnXsdTypeName == "xmlDocument";
				bool flag2 = type.IsArray && columnXsdTypeName != "base64Binary";
				if (!this.sequentialAccess)
				{
					obj = FormattersHelpers.ReadRowsetProperty(this.xmlReader, this.xmlReader.LocalName, this.xmlReader.LookupNamespace(this.xmlReader.Prefix), type, false, flag2, false);
					if (flag)
					{
						obj = new ColumnXmlReader(obj as string);
					}
				}
				else if (flag)
				{
					obj = new ColumnXmlReader(this.xmlReader, this.xmlReader.LocalName, this.xmlReader.NamespaceURI);
				}
				else
				{
					obj = FormattersHelpers.ReadRowsetProperty(this.xmlReader, this.xmlReader.LocalName, this.xmlReader.NamespaceURI, type, false, flag2, false);
				}
			}
			else if (this.sequentialAccess)
			{
				obj = xmlaDataReader;
			}
			else
			{
				if (xmlaDataReader.IsClosed)
				{
					xmlaDataReader.ReOpen();
				}
				int count = xmlaDataReader.dtStore.Rows.Count;
				xmlaDataReader.currentParentRow = this.dtStore.Rows.Count;
				while (xmlaDataReader.InternalRead())
				{
				}
				obj = count;
			}
			return obj;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001E76C File Offset: 0x0001C96C
		private bool BeginNewRow()
		{
			bool flag;
			try
			{
				if (this.xmlReader.IsStartElement(this.rowElement, this.rowNamespace))
				{
					this.readersXmlDepth = this.xmlReader.Depth + 1;
					this.xmlReader.ReadStartElement();
					this.currentColumn = 0;
					flag = true;
				}
				else
				{
					FormattersHelpers.CheckException(this.xmlReader);
					flag = false;
				}
			}
			catch (ResponseFormatException)
			{
				if (this.xmlReader != null)
				{
					this.xmlReader.Close();
				}
				throw;
			}
			catch (ConnectionException)
			{
				throw;
			}
			catch (OperationException)
			{
				if (this.xmlReader != null)
				{
					this.xmlReader.Close();
				}
				throw;
			}
			catch (XmlException ex)
			{
				if (this.xmlReader != null)
				{
					this.xmlReader.Close();
				}
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			catch (IOException ex2)
			{
				if (this.owner != null)
				{
					this.owner.CloseConnection(false);
				}
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex2);
			}
			catch
			{
				if (this.owner != null)
				{
					this.owner.CloseConnection(false);
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001E8A4 File Offset: 0x0001CAA4
		private void CompletePreviousRow()
		{
			try
			{
				if (!this.xmlReader.IsStartElement(this.rowElement, this.rowNamespace))
				{
					while (this.xmlReader.IsStartElement() && this.xmlReader.Depth == this.readersXmlDepth)
					{
						if (this.xmlReader.IsEmptyElement)
						{
							FormattersHelpers.CheckException(this.xmlReader);
						}
						if (this.GetOrdinalFromXmlName(this.xmlReader.LocalName) == -1)
						{
							throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Unexpected element {0}", this.xmlReader.Name));
						}
						this.xmlReader.Skip();
					}
					this.currentColumn = this.FieldCount;
					if (this.xmlReader.MoveToContent() == XmlNodeType.EndElement && this.xmlReader.LocalName == this.rowElement && this.xmlReader.NamespaceURI == this.rowNamespace)
					{
						this.xmlReader.ReadEndElement();
					}
				}
			}
			catch (ResponseFormatException)
			{
				if (this.xmlReader != null)
				{
					this.xmlReader.Close();
				}
				throw;
			}
			catch (ConnectionException)
			{
				throw;
			}
			catch (OperationException)
			{
				if (this.xmlReader != null)
				{
					this.xmlReader.Close();
				}
				throw;
			}
			catch (XmlException ex)
			{
				if (this.xmlReader != null)
				{
					this.xmlReader.Close();
				}
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			catch (IOException ex2)
			{
				if (this.owner != null)
				{
					this.owner.CloseConnection(false);
				}
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex2);
			}
			catch
			{
				if (this.owner != null)
				{
					this.owner.CloseConnection(false);
				}
				throw;
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001EA7C File Offset: 0x0001CC7C
		private void GenerateSchemaForEmptyResult()
		{
			this.columnCount = 0;
			this.dtStore = new DataTable();
			this.dtStore.Locale = CultureInfo.InvariantCulture;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001EAA0 File Offset: 0x0001CCA0
		private void LoadResponseSchema()
		{
			ColumnDefinitionDelegate columnDefinitionDelegate = new ColumnDefinitionDelegate(this.ColumnDef);
			FormattersHelpers.LoadSchema(this.xmlReader, columnDefinitionDelegate, true);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001EAC8 File Offset: 0x0001CCC8
		private object ColumnDef(int ordinal, string name, string colNamespace, string caption, Type type, bool isNested, object parent, string strColumnXsdType)
		{
			if (ordinal == -1)
			{
				this.columnCount = (int)parent;
				this.dtStore = new DataTable();
				this.dtStore.Locale = CultureInfo.InvariantCulture;
				if (this.nestedDataReaders == null)
				{
					this.nestedDataReaders = new XmlaDataReader[this.columnCount];
				}
				return null;
			}
			if (ordinal == -2)
			{
				if (this.parentReader != null)
				{
					this.dtStore.Columns.Add(string.Empty, this.currentRow.GetType());
				}
				return null;
			}
			Type type2 = type;
			ColumnDefinitionDelegate columnDefinitionDelegate = null;
			if (strColumnXsdType == "xmlDocument")
			{
				type2 = typeof(object);
			}
			if (isNested)
			{
				type2 = typeof(XmlaDataReader);
				this.nestedDataReaders[ordinal] = new XmlaDataReader(this);
				this.nestedDataReaders[ordinal].rowElement = name;
				this.nestedDataReaders[ordinal].rowNamespace = colNamespace;
				columnDefinitionDelegate = new ColumnDefinitionDelegate(this.nestedDataReaders[ordinal].ColumnDef);
			}
			DataRow dataRow = this.schemaTable.NewRow();
			this.columnXmlNameLookup[name] = ordinal;
			this.columnNameLookup[caption] = ordinal;
			dataRow["ColumnName"] = caption;
			dataRow["ColumnOrdinal"] = ordinal;
			dataRow["ColumnSize"] = 0;
			if (type2 == typeof(decimal))
			{
				dataRow["NumericPrecision"] = 19;
				dataRow["NumericScale"] = 4;
			}
			else
			{
				dataRow["NumericPrecision"] = 0;
				dataRow["NumericScale"] = 0;
			}
			dataRow["DataType"] = type2;
			dataRow["ProviderType"] = type2;
			dataRow["IsLong"] = false;
			dataRow["AllowDBNull"] = true;
			dataRow["IsReadOnly"] = true;
			dataRow["IsRowVersion"] = false;
			dataRow["IsUnique"] = false;
			dataRow["IsKeyColumn"] = false;
			dataRow["IsAutoIncrement"] = false;
			dataRow["BaseSchemaName"] = null;
			dataRow["BaseCatalogName"] = null;
			dataRow["BaseTableName"] = null;
			dataRow["BaseColumnName"] = null;
			this.schemaTable.Rows.Add(dataRow);
			if (isNested)
			{
				this.dtStore.Columns.Add(name, this.currentRow.GetType());
			}
			else
			{
				FormattersHelpers.SetColumnXsdTypeName(this.dtStore.Columns.Add(name, typeof(object)), strColumnXsdType);
			}
			return columnDefinitionDelegate;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001ED90 File Offset: 0x0001CF90
		private bool InternalRead()
		{
			if (this.isClosed)
			{
				throw new InvalidOperationException(XmlaSR.DataReaderClosedError);
			}
			if (this.xmlReader == null || this.xmlReader.ReadState == ReadState.Closed)
			{
				throw new InvalidOperationException();
			}
			if (this.emptyResult)
			{
				XmlaClient.CheckForException(this.xmlReader, null, true);
				return false;
			}
			if (this.dataReady)
			{
				this.CompletePreviousRow();
			}
			this.dataReady = this.BeginNewRow();
			if (this.dataReady)
			{
				if (!this.sequentialAccess)
				{
					int num = this.FieldCount;
					if (this.parentReader != null)
					{
						num++;
					}
					object[] array = new object[num];
					this.GetRowXmlValues(array);
					if (this.parentReader != null)
					{
						array[this.ParentId] = this.currentParentRow;
					}
					this.dtStore.Rows.Add(array);
				}
			}
			else if (this.Depth == 0)
			{
				XmlaClient.EndRowsetResponseS(this.xmlReader);
			}
			return this.dataReady;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001EE78 File Offset: 0x0001D078
		private void ResetCurrentRowCache()
		{
			try
			{
				if (this.dtStore != null)
				{
					this.dtStore.Rows.Clear();
				}
				if (this.nestedDataReaders != null)
				{
					for (int i = 0; i < this.FieldCount; i++)
					{
						if (this.nestedDataReaders[i] != null)
						{
							this.nestedDataReaders[i].ResetCurrentRowCache();
						}
					}
				}
			}
			catch (NullReferenceException)
			{
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001EEE4 File Offset: 0x0001D0E4
		private object InternalGetValue(int ordinal)
		{
			if (this.isClosed)
			{
				throw new InvalidOperationException(XmlaSR.DataReaderClosedError);
			}
			if (!this.dataReady)
			{
				throw new InvalidOperationException(XmlaSR.DataReaderInvalidRowError);
			}
			if (this.xmlReader == null || this.xmlReader.ReadState == ReadState.Closed)
			{
				throw new InvalidOperationException();
			}
			if (ordinal < 0 || ordinal >= this.FieldCount)
			{
				throw new ArgumentOutOfRangeException("ordinal");
			}
			object obj;
			if (this.sequentialAccess)
			{
				obj = this.SequentialReadXmlValue(ordinal);
			}
			else
			{
				DataRow dataRow = this.dtStore.Rows[this.currentRow];
				if (this.nestedDataReaders[ordinal] == null)
				{
					obj = null;
					if (!(dataRow[ordinal] is DBNull))
					{
						obj = dataRow[ordinal];
					}
				}
				else
				{
					int num = 0;
					if (!(dataRow[ordinal] is DBNull))
					{
						num = (int)dataRow[ordinal];
					}
					XmlaDataReader xmlaDataReader = this.nestedDataReaders[ordinal];
					xmlaDataReader.currentRow = num - 1;
					obj = xmlaDataReader;
				}
			}
			XmlaDataReader.ThrowIfInlineError(obj);
			if (obj is XmlaDataReader)
			{
				XmlaDataReader xmlaDataReader2 = obj as XmlaDataReader;
				if (xmlaDataReader2.IsClosed)
				{
					xmlaDataReader2.ReOpen();
				}
			}
			return obj;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001EFEB File Offset: 0x0001D1EB
		private void InitResultData()
		{
			this.nestedDataReaders = null;
			this.schemaTable = XmlaDataReader.CreateSchemaTable();
			this.currentRow = -1;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001F008 File Offset: 0x0001D208
		private void InternalInitialize(XmlReader xmlReader, CommandBehavior commandBehavior, IXmlaDataReaderOwner owner)
		{
			this.xmlReader = xmlReader;
			this.commandBehavior = commandBehavior;
			this.owner = owner;
			this.sequentialAccess = (commandBehavior & CommandBehavior.SequentialAccess) > CommandBehavior.Default;
			this.InitResultData();
			this.depth = 0;
			this.isClosed = false;
			if (this.parentReader != null)
			{
				this.depth = this.parentReader.Depth + 1;
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001F066 File Offset: 0x0001D266
		private void ReOpen()
		{
			if (this.IsClosed)
			{
				this.isClosed = false;
			}
		}

		// Token: 0x040003A1 RID: 929
		private const int schemaTableColumnCount = 18;

		// Token: 0x040003A2 RID: 930
		private static readonly string[] schemaTableColumnNames = new string[]
		{
			"ColumnName", "ColumnOrdinal", "ColumnSize", "NumericPrecision", "NumericScale", "DataType", "ProviderType", "IsLong", "AllowDBNull", "IsReadOnly",
			"IsRowVersion", "IsUnique", "IsKeyColumn", "IsAutoIncrement", "BaseSchemaName", "BaseCatalogName", "BaseTableName", "BaseColumnName"
		};

		// Token: 0x040003A3 RID: 931
		private static readonly Type[] schemaTableColumnTypes = new Type[]
		{
			typeof(string),
			typeof(int),
			typeof(int),
			typeof(int),
			typeof(int),
			typeof(Type),
			typeof(object),
			typeof(bool),
			typeof(bool),
			typeof(bool),
			typeof(bool),
			typeof(bool),
			typeof(bool),
			typeof(bool),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string)
		};

		// Token: 0x040003A4 RID: 932
		private IXmlaDataReaderOwner owner;

		// Token: 0x040003A5 RID: 933
		private XmlReader xmlReader;

		// Token: 0x040003A6 RID: 934
		private CommandBehavior commandBehavior;

		// Token: 0x040003A7 RID: 935
		private DataTable schemaTable;

		// Token: 0x040003A8 RID: 936
		private bool sequentialAccess;

		// Token: 0x040003A9 RID: 937
		private int columnCount;

		// Token: 0x040003AA RID: 938
		private int currentColumn;

		// Token: 0x040003AB RID: 939
		private bool dataReady;

		// Token: 0x040003AC RID: 940
		private Hashtable columnNameLookup;

		// Token: 0x040003AD RID: 941
		private Hashtable columnXmlNameLookup;

		// Token: 0x040003AE RID: 942
		private int depth;

		// Token: 0x040003AF RID: 943
		private bool isClosed;

		// Token: 0x040003B0 RID: 944
		private const string rootTag = "root";

		// Token: 0x040003B1 RID: 945
		private bool isMultipleResult;

		// Token: 0x040003B2 RID: 946
		private bool emptyResult;

		// Token: 0x040003B3 RID: 947
		private List<string> rowsetNames;

		// Token: 0x040003B4 RID: 948
		private XmlaDataReader[] nestedDataReaders;

		// Token: 0x040003B5 RID: 949
		private XmlaDataReader parentReader;

		// Token: 0x040003B6 RID: 950
		private DataTable dtStore;

		// Token: 0x040003B7 RID: 951
		private int currentRow;

		// Token: 0x040003B8 RID: 952
		private int currentParentRow;

		// Token: 0x040003B9 RID: 953
		private string rowElement;

		// Token: 0x040003BA RID: 954
		private string rowNamespace;

		// Token: 0x040003BB RID: 955
		private int readersXmlDepth;

		// Token: 0x02000190 RID: 400
		private static class SchemaColumnName
		{
			// Token: 0x04000C24 RID: 3108
			public const string ColumnName = "ColumnName";

			// Token: 0x04000C25 RID: 3109
			public const string ColumnOrdinal = "ColumnOrdinal";

			// Token: 0x04000C26 RID: 3110
			public const string ColumnSize = "ColumnSize";

			// Token: 0x04000C27 RID: 3111
			public const string NumericPrecision = "NumericPrecision";

			// Token: 0x04000C28 RID: 3112
			public const string NumericScale = "NumericScale";

			// Token: 0x04000C29 RID: 3113
			public const string DataType = "DataType";

			// Token: 0x04000C2A RID: 3114
			public const string ProviderType = "ProviderType";

			// Token: 0x04000C2B RID: 3115
			public const string IsLong = "IsLong";

			// Token: 0x04000C2C RID: 3116
			public const string AllowDBNull = "AllowDBNull";

			// Token: 0x04000C2D RID: 3117
			public const string IsReadOnly = "IsReadOnly";

			// Token: 0x04000C2E RID: 3118
			public const string IsRowVersion = "IsRowVersion";

			// Token: 0x04000C2F RID: 3119
			public const string IsUnique = "IsUnique";

			// Token: 0x04000C30 RID: 3120
			public const string IsKeyColumn = "IsKeyColumn";

			// Token: 0x04000C31 RID: 3121
			public const string IsAutoIncrement = "IsAutoIncrement";

			// Token: 0x04000C32 RID: 3122
			public const string BaseSchemaName = "BaseSchemaName";

			// Token: 0x04000C33 RID: 3123
			public const string BaseCatalogName = "BaseCatalogName";

			// Token: 0x04000C34 RID: 3124
			public const string BaseTableName = "BaseTableName";

			// Token: 0x04000C35 RID: 3125
			public const string BaseColumnName = "BaseColumnName";
		}

		// Token: 0x02000191 RID: 401
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060012DE RID: 4830 RVA: 0x00042030 File Offset: 0x00040230
			internal Enumerator(XmlaDataReader dataReader)
			{
				bool flag = (CommandBehavior.CloseConnection & dataReader.commandBehavior) > CommandBehavior.Default;
				this.enumerator = new DbEnumerator(dataReader, flag);
			}

			// Token: 0x17000626 RID: 1574
			// (get) Token: 0x060012DF RID: 4831 RVA: 0x00042057 File Offset: 0x00040257
			public IDataRecord Current
			{
				get
				{
					return (IDataRecord)this.enumerator.Current;
				}
			}

			// Token: 0x17000627 RID: 1575
			// (get) Token: 0x060012E0 RID: 4832 RVA: 0x00042069 File Offset: 0x00040269
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060012E1 RID: 4833 RVA: 0x00042071 File Offset: 0x00040271
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x060012E2 RID: 4834 RVA: 0x0004207E File Offset: 0x0004027E
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x04000C36 RID: 3126
			private DbEnumerator enumerator;
		}
	}
}
