using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200003F RID: 63
	internal sealed class XmlaDataReader : Disposable, IDataReader, IDisposable, IDataRecord
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x0001981E File Offset: 0x00017A1E
		internal XmlaDataReader(XmlReader xmlReader, CommandBehavior commandBehavior = CommandBehavior.Default)
			: this(xmlReader, commandBehavior, false, null)
		{
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001982C File Offset: 0x00017A2C
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
							throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, "Expected at least one root element");
						}
						this.StartEmptyAffectedObjects();
					}
				}
				else
				{
					if (XmlaClient.IsDatasetResponseS(xmlReader))
					{
						throw new AdomdUnknownResponseException(XmlaSR.Resultset_IsNotRowset, string.Format(CultureInfo.InvariantCulture, "Got {0}:{1}", "urn:schemas-microsoft-com:xml-analysis:mddataset", "root"));
					}
					if (!XmlaClient.IsEmptyResultS(xmlReader))
					{
						throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1}, got {2}", "urn:schemas-microsoft-com:xml-analysis:empty", "root", xmlReader.Name));
					}
					XmlaClient.ReadEmptyRootS(xmlReader);
					throw new AdomdUnknownResponseException(XmlaSR.Resultset_IsNotRowset, string.Format(CultureInfo.InvariantCulture, "Unexpected node {0}", xmlReader.Name));
				}
			}
			catch (AdomdUnknownResponseException)
			{
				xmlReader.Close();
				throw;
			}
			catch (AdomdConnectionException)
			{
				throw;
			}
			catch (AdomdErrorResponseException)
			{
				xmlReader.Close();
				throw;
			}
			catch (XmlException ex)
			{
				xmlReader.Close();
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			catch (IOException ex2)
			{
				if (this.owner != null)
				{
					owner.CloseConnection(false);
				}
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex2);
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

		// Token: 0x060003B4 RID: 948 RVA: 0x00019AE0 File Offset: 0x00017CE0
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

		// Token: 0x060003B5 RID: 949 RVA: 0x00019B50 File Offset: 0x00017D50
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

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00019CB4 File Offset: 0x00017EB4
		public int Depth
		{
			get
			{
				return this.depth;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00019CBC File Offset: 0x00017EBC
		public int FieldCount
		{
			get
			{
				return this.columnCount;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00019CC4 File Offset: 0x00017EC4
		public bool IsClosed
		{
			get
			{
				return this.isClosed;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00019CCC File Offset: 0x00017ECC
		public int RecordsAffected
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170000CE RID: 206
		public object this[int index]
		{
			get
			{
				return this.GetValue(index);
			}
		}

		// Token: 0x170000CF RID: 207
		public object this[string columnName]
		{
			get
			{
				int ordinal = this.GetOrdinal(columnName);
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00019CF4 File Offset: 0x00017EF4
		// (set) Token: 0x060003BD RID: 957 RVA: 0x00019CFC File Offset: 0x00017EFC
		internal bool IsAffectedObjects { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00019D05 File Offset: 0x00017F05
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00019D0D File Offset: 0x00017F0D
		internal string RowsetName { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00019D16 File Offset: 0x00017F16
		internal List<string> RowsetNames
		{
			get
			{
				return this.rowsetNames;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00019D1E File Offset: 0x00017F1E
		internal XmlReader XmlReader
		{
			get
			{
				return this.xmlReader;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00019D26 File Offset: 0x00017F26
		internal CommandBehavior CommandBehavior
		{
			get
			{
				return this.commandBehavior;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00019D2E File Offset: 0x00017F2E
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x00019D36 File Offset: 0x00017F36
		internal XmlaResultCollection Results { get; private set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x00019D3F File Offset: 0x00017F3F
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x00019D47 File Offset: 0x00017F47
		internal Dictionary<XName, string> TopLevelAttributes { get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00019D50 File Offset: 0x00017F50
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

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00019D62 File Offset: 0x00017F62
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

		// Token: 0x060003C9 RID: 969 RVA: 0x00019D90 File Offset: 0x00017F90
		public DataTable GetSchemaTable()
		{
			return this.schemaTable;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00019D98 File Offset: 0x00017F98
		public Type GetFieldType(int ordinal)
		{
			return (Type)this.schemaTable.Rows[ordinal]["DataType"];
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00019DBA File Offset: 0x00017FBA
		public string GetName(int ordinal)
		{
			return (string)this.schemaTable.Rows[ordinal]["ColumnName"];
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00019DDC File Offset: 0x00017FDC
		public int GetOrdinal(string name)
		{
			object obj = this.columnNameLookup[name];
			if (obj == null)
			{
				throw new ArgumentException(XmlaSR.InvalidArgument);
			}
			return (int)obj;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00019DFD File Offset: 0x00017FFD
		public bool NextResult()
		{
			return this.isMultipleResult && this.InternalNextResult(false);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00019E10 File Offset: 0x00018010
		public bool IsDBNull(int ordinal)
		{
			object value = this.GetValue(ordinal);
			return value == null || Convert.IsDBNull(value);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00019E30 File Offset: 0x00018030
		public IDataReader GetData(int ordinal)
		{
			return (IDataReader)this.GetValue(ordinal);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00019E3E File Offset: 0x0001803E
		public string GetDataTypeName(int index)
		{
			return this.GetFieldType(index).Name;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00019E4C File Offset: 0x0001804C
		public bool GetBoolean(int ordinal)
		{
			return (bool)this.InternalGetValue(ordinal);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00019E5A File Offset: 0x0001805A
		public byte GetByte(int ordinal)
		{
			return (byte)this.InternalGetValue(ordinal);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00019E68 File Offset: 0x00018068
		public long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00019E6F File Offset: 0x0001806F
		public char GetChar(int ordinal)
		{
			return (char)this.InternalGetValue(ordinal);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00019E80 File Offset: 0x00018080
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

		// Token: 0x060003D6 RID: 982 RVA: 0x00019EF3 File Offset: 0x000180F3
		public DateTime GetDateTime(int ordinal)
		{
			return (DateTime)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(DateTime), CultureInfo.InvariantCulture);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00019F15 File Offset: 0x00018115
		public decimal GetDecimal(int ordinal)
		{
			return (decimal)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(decimal), CultureInfo.InvariantCulture);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00019F37 File Offset: 0x00018137
		public double GetDouble(int ordinal)
		{
			return (double)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(double), CultureInfo.InvariantCulture);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00019F59 File Offset: 0x00018159
		public float GetFloat(int ordinal)
		{
			return (float)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(float), CultureInfo.InvariantCulture);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00019F7B File Offset: 0x0001817B
		public Guid GetGuid(int ordinal)
		{
			return (Guid)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(Guid), CultureInfo.InvariantCulture);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00019F9D File Offset: 0x0001819D
		public short GetInt16(int ordinal)
		{
			return (short)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(short), CultureInfo.InvariantCulture);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00019FBF File Offset: 0x000181BF
		public int GetInt32(int ordinal)
		{
			return (int)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(int), CultureInfo.InvariantCulture);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00019FE1 File Offset: 0x000181E1
		public long GetInt64(int ordinal)
		{
			return (long)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(long), CultureInfo.InvariantCulture);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001A003 File Offset: 0x00018203
		public string GetString(int ordinal)
		{
			return this.InternalGetValue(ordinal).ToString();
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001A011 File Offset: 0x00018211
		public TimeSpan GetTimeSpan(int ordinal)
		{
			return (TimeSpan)Convert.ChangeType(this.InternalGetValue(ordinal), typeof(TimeSpan), CultureInfo.InvariantCulture);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0001A034 File Offset: 0x00018234
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

		// Token: 0x060003E1 RID: 993 RVA: 0x0001A088 File Offset: 0x00018288
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

		// Token: 0x060003E2 RID: 994 RVA: 0x0001A13C File Offset: 0x0001833C
		public XmlaDataReader GetDataReader(int ordinal)
		{
			return (XmlaDataReader)this.InternalGetValue(ordinal);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001A14C File Offset: 0x0001834C
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

		// Token: 0x060003E4 RID: 996 RVA: 0x0001A1F4 File Offset: 0x000183F4
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

		// Token: 0x060003E5 RID: 997 RVA: 0x0001A24A File Offset: 0x0001844A
		public XmlaDataReader.Enumerator GetEnumerator()
		{
			return new XmlaDataReader.Enumerator(this);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001A252 File Offset: 0x00018452
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0001A264 File Offset: 0x00018464
		private static void ThrowIfInlineError(object columnValue)
		{
			if (columnValue is XmlaError)
			{
				throw new AdomdErrorResponseException((XmlaError)columnValue);
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001A27C File Offset: 0x0001847C
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

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001A2C4 File Offset: 0x000184C4
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

		// Token: 0x060003EA RID: 1002 RVA: 0x0001A36B File Offset: 0x0001856B
		private void StartEmptyAffectedObjects()
		{
			this.EnsureResultForNewRowset();
			this.CheckForMessages();
			this.isMultipleResult = false;
			this.emptyResult = true;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001A388 File Offset: 0x00018588
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

		// Token: 0x060003EC RID: 1004 RVA: 0x0001A43B File Offset: 0x0001863B
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

		// Token: 0x060003ED RID: 1005 RVA: 0x0001A46C File Offset: 0x0001866C
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

		// Token: 0x060003EE RID: 1006 RVA: 0x0001A4E4 File Offset: 0x000186E4
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
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, "Got an empty result inside AffectedObjects");
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
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, "Expected at least one root element");
				}
				this.CheckForMessages();
				XmlaClient.EndElementS(this.xmlReader, this.IsAffectedObjects ? "AffectedObjects" : "results", "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults");
				this.isMultipleResult = false;
				return false;
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001A604 File Offset: 0x00018804
		private int GetOrdinalFromXmlName(string xmlName)
		{
			object obj = this.columnXmlNameLookup[xmlName];
			if (obj == null)
			{
				return -1;
			}
			return (int)obj;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001A62C File Offset: 0x0001882C
		private int GetRowXmlValues(object[] xmlValues)
		{
			int fieldCount = this.FieldCount;
			for (int i = 0; i < fieldCount; i++)
			{
				xmlValues[i] = this.SequentialReadXmlValue(i);
			}
			return fieldCount;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001A658 File Offset: 0x00018858
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
						throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Unexpected element {0}", this.xmlReader.Name));
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
			catch (AdomdUnknownResponseException)
			{
				if (this.xmlReader != null)
				{
					this.xmlReader.Close();
				}
				throw;
			}
			catch (AdomdConnectionException)
			{
				throw;
			}
			catch (AdomdErrorResponseException)
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
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			catch (IOException ex2)
			{
				if (this.owner != null)
				{
					this.owner.CloseConnection(false);
				}
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex2);
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

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001A910 File Offset: 0x00018B10
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

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001AA84 File Offset: 0x00018C84
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
			catch (AdomdUnknownResponseException)
			{
				if (this.xmlReader != null)
				{
					this.xmlReader.Close();
				}
				throw;
			}
			catch (AdomdConnectionException)
			{
				throw;
			}
			catch (AdomdErrorResponseException)
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
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			catch (IOException ex2)
			{
				if (this.owner != null)
				{
					this.owner.CloseConnection(false);
				}
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex2);
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

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001ABBC File Offset: 0x00018DBC
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
							throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Unexpected element {0}", this.xmlReader.Name));
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
			catch (AdomdUnknownResponseException)
			{
				if (this.xmlReader != null)
				{
					this.xmlReader.Close();
				}
				throw;
			}
			catch (AdomdConnectionException)
			{
				throw;
			}
			catch (AdomdErrorResponseException)
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
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			catch (IOException ex2)
			{
				if (this.owner != null)
				{
					this.owner.CloseConnection(false);
				}
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex2);
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

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001AD94 File Offset: 0x00018F94
		private void GenerateSchemaForEmptyResult()
		{
			this.columnCount = 0;
			this.dtStore = new DataTable();
			this.dtStore.Locale = CultureInfo.InvariantCulture;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001ADB8 File Offset: 0x00018FB8
		private void LoadResponseSchema()
		{
			ColumnDefinitionDelegate columnDefinitionDelegate = new ColumnDefinitionDelegate(this.ColumnDef);
			FormattersHelpers.LoadSchema(this.xmlReader, columnDefinitionDelegate, true);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001ADE0 File Offset: 0x00018FE0
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

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001B0A8 File Offset: 0x000192A8
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

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001B190 File Offset: 0x00019390
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

		// Token: 0x060003FA RID: 1018 RVA: 0x0001B1FC File Offset: 0x000193FC
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

		// Token: 0x060003FB RID: 1019 RVA: 0x0001B303 File Offset: 0x00019503
		private void InitResultData()
		{
			this.nestedDataReaders = null;
			this.schemaTable = XmlaDataReader.CreateSchemaTable();
			this.currentRow = -1;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001B320 File Offset: 0x00019520
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

		// Token: 0x060003FD RID: 1021 RVA: 0x0001B37E File Offset: 0x0001957E
		private void ReOpen()
		{
			if (this.IsClosed)
			{
				this.isClosed = false;
			}
		}

		// Token: 0x04000365 RID: 869
		private const int schemaTableColumnCount = 18;

		// Token: 0x04000366 RID: 870
		private static readonly string[] schemaTableColumnNames = new string[]
		{
			"ColumnName", "ColumnOrdinal", "ColumnSize", "NumericPrecision", "NumericScale", "DataType", "ProviderType", "IsLong", "AllowDBNull", "IsReadOnly",
			"IsRowVersion", "IsUnique", "IsKeyColumn", "IsAutoIncrement", "BaseSchemaName", "BaseCatalogName", "BaseTableName", "BaseColumnName"
		};

		// Token: 0x04000367 RID: 871
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

		// Token: 0x04000368 RID: 872
		private IXmlaDataReaderOwner owner;

		// Token: 0x04000369 RID: 873
		private XmlReader xmlReader;

		// Token: 0x0400036A RID: 874
		private CommandBehavior commandBehavior;

		// Token: 0x0400036B RID: 875
		private DataTable schemaTable;

		// Token: 0x0400036C RID: 876
		private bool sequentialAccess;

		// Token: 0x0400036D RID: 877
		private int columnCount;

		// Token: 0x0400036E RID: 878
		private int currentColumn;

		// Token: 0x0400036F RID: 879
		private bool dataReady;

		// Token: 0x04000370 RID: 880
		private Hashtable columnNameLookup;

		// Token: 0x04000371 RID: 881
		private Hashtable columnXmlNameLookup;

		// Token: 0x04000372 RID: 882
		private int depth;

		// Token: 0x04000373 RID: 883
		private bool isClosed;

		// Token: 0x04000374 RID: 884
		private const string rootTag = "root";

		// Token: 0x04000375 RID: 885
		private bool isMultipleResult;

		// Token: 0x04000376 RID: 886
		private bool emptyResult;

		// Token: 0x04000377 RID: 887
		private List<string> rowsetNames;

		// Token: 0x04000378 RID: 888
		private XmlaDataReader[] nestedDataReaders;

		// Token: 0x04000379 RID: 889
		private XmlaDataReader parentReader;

		// Token: 0x0400037A RID: 890
		private DataTable dtStore;

		// Token: 0x0400037B RID: 891
		private int currentRow;

		// Token: 0x0400037C RID: 892
		private int currentParentRow;

		// Token: 0x0400037D RID: 893
		private string rowElement;

		// Token: 0x0400037E RID: 894
		private string rowNamespace;

		// Token: 0x0400037F RID: 895
		private int readersXmlDepth;

		// Token: 0x02000194 RID: 404
		private static class SchemaColumnName
		{
			// Token: 0x04000C56 RID: 3158
			public const string ColumnName = "ColumnName";

			// Token: 0x04000C57 RID: 3159
			public const string ColumnOrdinal = "ColumnOrdinal";

			// Token: 0x04000C58 RID: 3160
			public const string ColumnSize = "ColumnSize";

			// Token: 0x04000C59 RID: 3161
			public const string NumericPrecision = "NumericPrecision";

			// Token: 0x04000C5A RID: 3162
			public const string NumericScale = "NumericScale";

			// Token: 0x04000C5B RID: 3163
			public const string DataType = "DataType";

			// Token: 0x04000C5C RID: 3164
			public const string ProviderType = "ProviderType";

			// Token: 0x04000C5D RID: 3165
			public const string IsLong = "IsLong";

			// Token: 0x04000C5E RID: 3166
			public const string AllowDBNull = "AllowDBNull";

			// Token: 0x04000C5F RID: 3167
			public const string IsReadOnly = "IsReadOnly";

			// Token: 0x04000C60 RID: 3168
			public const string IsRowVersion = "IsRowVersion";

			// Token: 0x04000C61 RID: 3169
			public const string IsUnique = "IsUnique";

			// Token: 0x04000C62 RID: 3170
			public const string IsKeyColumn = "IsKeyColumn";

			// Token: 0x04000C63 RID: 3171
			public const string IsAutoIncrement = "IsAutoIncrement";

			// Token: 0x04000C64 RID: 3172
			public const string BaseSchemaName = "BaseSchemaName";

			// Token: 0x04000C65 RID: 3173
			public const string BaseCatalogName = "BaseCatalogName";

			// Token: 0x04000C66 RID: 3174
			public const string BaseTableName = "BaseTableName";

			// Token: 0x04000C67 RID: 3175
			public const string BaseColumnName = "BaseColumnName";
		}

		// Token: 0x02000195 RID: 405
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001235 RID: 4661 RVA: 0x0003F594 File Offset: 0x0003D794
			internal Enumerator(XmlaDataReader dataReader)
			{
				bool flag = (CommandBehavior.CloseConnection & dataReader.commandBehavior) > CommandBehavior.Default;
				this.enumerator = new DbEnumerator(dataReader, flag);
			}

			// Token: 0x1700065C RID: 1628
			// (get) Token: 0x06001236 RID: 4662 RVA: 0x0003F5BB File Offset: 0x0003D7BB
			public IDataRecord Current
			{
				get
				{
					return (IDataRecord)this.enumerator.Current;
				}
			}

			// Token: 0x1700065D RID: 1629
			// (get) Token: 0x06001237 RID: 4663 RVA: 0x0003F5CD File Offset: 0x0003D7CD
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001238 RID: 4664 RVA: 0x0003F5D5 File Offset: 0x0003D7D5
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x06001239 RID: 4665 RVA: 0x0003F5E2 File Offset: 0x0003D7E2
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x04000C68 RID: 3176
			private DbEnumerator enumerator;
		}
	}
}
