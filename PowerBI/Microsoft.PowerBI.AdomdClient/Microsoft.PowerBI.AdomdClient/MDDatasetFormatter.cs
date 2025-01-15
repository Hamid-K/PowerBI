using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Xml;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000052 RID: 82
	internal class MDDatasetFormatter : ResultsetFormatter
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0001E8D6 File Offset: 0x0001CAD6
		internal IDSFAxisCollection AxesList
		{
			get
			{
				return this.axesList;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0001E8DE File Offset: 0x0001CADE
		internal DataTable CellTable
		{
			get
			{
				return this.cellTable;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0001E8E6 File Offset: 0x0001CAE6
		internal IDSFDataSet FilterAxis
		{
			get
			{
				return this.filterAxis;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0001E8EE File Offset: 0x0001CAEE
		internal DataTable CubesInfos
		{
			get
			{
				return this.cubesInfos;
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001E8F6 File Offset: 0x0001CAF6
		internal MDDatasetFormatter()
		{
			this.axesList = new MDDatasetFormatter.DataSetFormatterAxisCollection();
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001E90C File Offset: 0x0001CB0C
		public void ReadMDDataset(XmlReader reader)
		{
			if (reader.IsStartElement("schema", "http://www.w3.org/2001/XMLSchema"))
			{
				this.ReadXSD(reader);
			}
			if (reader.IsStartElement("OlapInfo", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
			{
				this.ReadOlapInfo(reader);
			}
			this.ReadMembers(reader);
			this.ReadCells(reader);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001E95C File Offset: 0x0001CB5C
		private void ReadMembers(XmlReader reader)
		{
			if (!reader.IsStartElement("Axes", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
			{
				if (reader.IsEmptyElement)
				{
					FormattersHelpers.CheckException(reader);
				}
				return;
			}
			if (reader.IsEmptyElement)
			{
				FormattersHelpers.CheckException(reader);
				reader.Skip();
				return;
			}
			this.BeginLoadAxisTables();
			reader.ReadStartElement("Axes", "urn:schemas-microsoft-com:xml-analysis:mddataset");
			int num = 0;
			while (reader.IsStartElement("Axis", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
			{
				string attribute = reader.GetAttribute("name");
				reader.ReadStartElement();
				IDSFDataSet idsfdataSet;
				if (attribute != "SlicerAxis")
				{
					idsfdataSet = this.axesList[num++];
				}
				else
				{
					idsfdataSet = this.filterAxis;
				}
				reader.MoveToContent();
				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement("Tuples", "urn:schemas-microsoft-com:xml-analysis:mddataset");
					while (reader.IsStartElement("Tuple", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
					{
						reader.ReadStartElement();
						int num2 = 0;
						while (reader.IsStartElement("Member", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
						{
							DataTable dataTable;
							if (reader.HasAttributes)
							{
								dataTable = idsfdataSet[reader.GetAttribute("Hierarchy")];
							}
							else
							{
								dataTable = idsfdataSet[num2];
							}
							num2++;
							reader.ReadStartElement();
							DataRow dataRow = dataTable.NewRow();
							DataColumnCollection columns = dataTable.Columns;
							while (reader.IsStartElement())
							{
								FormattersHelpers.CheckException(reader);
								DataColumn dataColumn = columns[reader.Name];
								if (dataColumn == null)
								{
									throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Unexpected element {0}", reader.Name));
								}
								if (!FormattersHelpers.IsNullContentElement(reader))
								{
									Type elementType = FormattersHelpers.GetElementType(reader, "http://www.w3.org/2001/XMLSchema-instance", dataColumn);
									dataRow[dataColumn] = FormattersHelpers.ReadDataSetProperty(reader, elementType);
								}
								else
								{
									reader.Skip();
								}
							}
							dataTable.Rows.Add(dataRow);
							reader.ReadEndElement();
						}
						FormattersHelpers.CheckException(reader);
						reader.ReadEndElement();
					}
					FormattersHelpers.CheckException(reader);
					reader.ReadEndElement();
				}
				else
				{
					FormattersHelpers.CheckException(reader);
					reader.ReadStartElement();
				}
				reader.ReadEndElement();
			}
			FormattersHelpers.CheckException(reader);
			reader.ReadEndElement();
			this.EndLoadAxisTables();
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001EB68 File Offset: 0x0001CD68
		private void BeginLoadAxisTables()
		{
			foreach (object obj in ((IEnumerable)this.axesList))
			{
				foreach (object obj2 in ((IDSFDataSet)obj))
				{
					((DataTable)obj2).BeginLoadData();
				}
			}
			if (this.filterAxis != null)
			{
				foreach (object obj3 in ((IEnumerable)this.filterAxis))
				{
					((DataTable)obj3).BeginLoadData();
				}
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001EC48 File Offset: 0x0001CE48
		private void EndLoadAxisTables()
		{
			foreach (object obj in ((IEnumerable)this.axesList))
			{
				foreach (object obj2 in ((IDSFDataSet)obj))
				{
					((DataTable)obj2).EndLoadData();
				}
			}
			if (this.filterAxis != null)
			{
				foreach (object obj3 in ((IEnumerable)this.filterAxis))
				{
					((DataTable)obj3).EndLoadData();
				}
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001ED28 File Offset: 0x0001CF28
		private void ReadXSD(XmlReader reader)
		{
			reader.Skip();
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001ED30 File Offset: 0x0001CF30
		private void ReadOlapInfo(XmlReader reader)
		{
			reader.ReadStartElement();
			this.ReadCubeInfo(reader);
			this.ReadAxesInfo(reader);
			this.ReadCellInfo(reader);
			reader.ReadEndElement();
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001ED54 File Offset: 0x0001CF54
		private void ReadAxesInfo(XmlReader reader)
		{
			reader.MoveToContent();
			if (reader.IsEmptyElement)
			{
				FormattersHelpers.CheckException(reader);
				if (reader.IsStartElement("AxesInfo", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
				{
					reader.Skip();
				}
				return;
			}
			reader.ReadStartElement("AxesInfo", "urn:schemas-microsoft-com:xml-analysis:mddataset");
			while (reader.IsStartElement("AxisInfo", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
			{
				MDDatasetFormatter.DataSetFormatterAxis dataSetFormatterAxis = new MDDatasetFormatter.DataSetFormatterAxis(reader.GetAttribute("name"));
				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();
					while (reader.IsStartElement("HierarchyInfo", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
					{
						DataTable dataTable = new DataTable(reader.GetAttribute("name"));
						dataTable.Locale = CultureInfo.InvariantCulture;
						dataSetFormatterAxis.AddTable(dataTable);
						Collection<int> collection = new Collection<int>();
						dataTable.ExtendedProperties["MemberProperties"] = collection;
						reader.ReadStartElement();
						DataColumnCollection columns = dataTable.Columns;
						while (reader.IsStartElement())
						{
							FormattersHelpers.CheckException(reader);
							try
							{
								DataColumn dataColumn = new DataColumn(reader.Name, typeof(object));
								dataColumn.Namespace = reader.NamespaceURI;
								FormattersHelpers.SetColumnType(dataColumn, FormattersHelpers.GetElementType(reader));
								dataColumn.Caption = reader.GetAttribute("name");
								columns.Add(dataColumn);
								collection.Add(columns.Count - 1);
							}
							catch (DuplicateNameException)
							{
								collection.Add(columns.IndexOf(reader.Name));
							}
							reader.Skip();
						}
						reader.ReadEndElement();
					}
					FormattersHelpers.CheckException(reader);
					reader.ReadEndElement();
				}
				else
				{
					FormattersHelpers.CheckException(reader);
					reader.Skip();
				}
				if ("SlicerAxis" == dataSetFormatterAxis.DataSetName)
				{
					this.filterAxis = dataSetFormatterAxis;
				}
				else
				{
					this.axesList.Add(dataSetFormatterAxis);
				}
			}
			FormattersHelpers.CheckException(reader);
			reader.ReadEndElement();
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001EF2C File Offset: 0x0001D12C
		private void ReadCellInfo(XmlReader reader)
		{
			this.cellTable = new DataTable();
			this.cellTable.Locale = CultureInfo.InvariantCulture;
			Collection<int> collection = new Collection<int>();
			this.cellTable.ExtendedProperties["MemberProperties"] = collection;
			DataColumn dataColumn = new DataColumn("CellOrdinal", typeof(int));
			dataColumn.Caption = "CellOrdinal";
			dataColumn.Namespace = "urn:schemas-microsoft-com:xml-analysis:mddataset";
			this.cellTable.Columns.Add(dataColumn);
			collection.Add(this.cellTable.Columns.Count - 1);
			reader.MoveToContent();
			if (!reader.IsEmptyElement)
			{
				reader.ReadStartElement("CellInfo", "urn:schemas-microsoft-com:xml-analysis:mddataset");
				while (reader.IsStartElement())
				{
					FormattersHelpers.CheckException(reader);
					if (reader.Name != "CellOrdinal" || reader.NamespaceURI != "urn:schemas-microsoft-com:xml-analysis:mddataset")
					{
						try
						{
							dataColumn = new DataColumn(reader.Name, typeof(object));
							dataColumn.Namespace = reader.NamespaceURI;
							FormattersHelpers.SetColumnType(dataColumn, FormattersHelpers.GetElementType(reader));
							dataColumn.Caption = reader.GetAttribute("name");
							this.cellTable.Columns.Add(dataColumn);
							collection.Add(this.cellTable.Columns.Count - 1);
						}
						catch (DuplicateNameException)
						{
							collection.Add(this.cellTable.Columns.IndexOf(reader.Name));
						}
					}
					reader.Skip();
				}
				reader.ReadEndElement();
				return;
			}
			FormattersHelpers.CheckException(reader);
			if (reader.IsStartElement("CellInfo", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
			{
				reader.Skip();
				return;
			}
			throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected element {0}:{1}, got {2}", "urn:schemas-microsoft-com:xml-analysis:mddataset", "CellInfo", reader.Name));
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001F110 File Offset: 0x0001D310
		private void ReadCubeInfo(XmlReader reader)
		{
			if (!reader.IsStartElement("CubeInfo", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
			{
				FormattersHelpers.CheckException(reader);
				return;
			}
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return;
			}
			reader.ReadStartElement();
			this.CreateCubesInfosTable();
			this.cubesInfos.BeginLoadData();
			while (reader.IsStartElement("Cube", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
			{
				reader.ReadStartElement();
				DataRow dataRow = this.cubesInfos.NewRow();
				if (reader.IsStartElement("CubeName", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
				{
					dataRow["CubeName"] = FormattersHelpers.ReadDataSetProperty(reader, typeof(string));
				}
				if (reader.IsStartElement("LastDataUpdate", "http://schemas.microsoft.com/analysisservices/2003/engine"))
				{
					dataRow["LastDataUpdate"] = MDDatasetFormatter.GetDateTimeIfValid(reader);
				}
				if (reader.IsStartElement("LastSchemaUpdate", "http://schemas.microsoft.com/analysisservices/2003/engine"))
				{
					dataRow["LastSchemaUpdate"] = MDDatasetFormatter.GetDateTimeIfValid(reader);
				}
				this.cubesInfos.Rows.Add(dataRow);
				FormattersHelpers.CheckException(reader);
				reader.ReadEndElement();
			}
			this.cubesInfos.EndLoadData();
			FormattersHelpers.CheckException(reader);
			reader.ReadEndElement();
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001F234 File Offset: 0x0001D434
		private void ReadCells(XmlReader reader)
		{
			if (!reader.IsStartElement("CellData", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
			{
				if (reader.IsEmptyElement)
				{
					FormattersHelpers.CheckException(reader);
				}
				return;
			}
			if (reader.IsEmptyElement)
			{
				FormattersHelpers.CheckException(reader);
				reader.Skip();
				return;
			}
			this.cellTable.BeginLoadData();
			DataColumn dataColumn = this.cellTable.Columns["CellOrdinal"];
			reader.ReadStartElement("CellData", "urn:schemas-microsoft-com:xml-analysis:mddataset");
			while (reader.IsStartElement("Cell", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
			{
				DataRow dataRow = this.cellTable.NewRow();
				string attribute = reader.GetAttribute("CellOrdinal");
				dataRow[dataColumn] = XmlConvert.ToInt32(attribute);
				if (!reader.IsEmptyElement)
				{
					reader.ReadStartElement();
					while (reader.IsStartElement())
					{
						FormattersHelpers.CheckException(reader);
						DataColumn dataColumn2 = this.cellTable.Columns[reader.Name];
						if (dataColumn2 == null)
						{
							throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Unexpected element {0}", reader.Name));
						}
						if (!FormattersHelpers.IsNullContentElement(reader))
						{
							Type elementType = FormattersHelpers.GetElementType(reader, "http://www.w3.org/2001/XMLSchema-instance", dataColumn2);
							dataRow[dataColumn2] = FormattersHelpers.ReadDataSetProperty(reader, elementType);
						}
						else
						{
							reader.Skip();
						}
					}
					reader.ReadEndElement();
				}
				else
				{
					FormattersHelpers.CheckException(reader);
					reader.ReadStartElement();
				}
				this.cellTable.Rows.Add(dataRow);
			}
			FormattersHelpers.CheckException(reader);
			reader.ReadEndElement();
			this.cellTable.EndLoadData();
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0001F3B2 File Offset: 0x0001D5B2
		internal string CubeName
		{
			get
			{
				if (this.cubesInfos == null || this.cubesInfos.Rows.Count == 0)
				{
					return null;
				}
				return this.cubesInfos.Rows[0]["CubeName"] as string;
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001F3F0 File Offset: 0x0001D5F0
		private void CreateCubesInfosTable()
		{
			this.cubesInfos = new DataTable();
			this.cubesInfos.Locale = CultureInfo.InvariantCulture;
			this.cubesInfos.Columns.Add("CubeName", typeof(string));
			this.cubesInfos.Columns.Add("LastSchemaUpdate", typeof(DateTime));
			this.cubesInfos.Columns.Add("LastDataUpdate", typeof(DateTime));
			this.cubesInfos.Columns[1].DateTimeMode = DataSetDateTime.Local;
			this.cubesInfos.Columns[2].DateTimeMode = DataSetDateTime.Local;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001F4A8 File Offset: 0x0001D6A8
		private static object GetDateTimeIfValid(XmlReader reader)
		{
			object obj = DBNull.Value;
			try
			{
				obj = ((DateTime)FormattersHelpers.ReadDataSetProperty(reader, typeof(DateTime))).ToLocalTime();
			}
			catch (ArgumentNullException)
			{
			}
			catch (FormatException)
			{
			}
			return obj;
		}

		// Token: 0x040003CD RID: 973
		private const string dataSetNamespace = "urn:schemas-microsoft-com:xml-analysis:mddataset";

		// Token: 0x040003CE RID: 974
		private const string xsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x040003CF RID: 975
		private const string ddlNamespace = "http://schemas.microsoft.com/analysisservices/2003/engine";

		// Token: 0x040003D0 RID: 976
		private const string olapInfoElement = "OlapInfo";

		// Token: 0x040003D1 RID: 977
		private const string axesInfoElement = "AxesInfo";

		// Token: 0x040003D2 RID: 978
		private const string axisInfoElement = "AxisInfo";

		// Token: 0x040003D3 RID: 979
		private const string hierarchyInfoElement = "HierarchyInfo";

		// Token: 0x040003D4 RID: 980
		private const string elementCellInfo = "CellInfo";

		// Token: 0x040003D5 RID: 981
		private const string slicerAxisName = "SlicerAxis";

		// Token: 0x040003D6 RID: 982
		private const string axesElement = "Axes";

		// Token: 0x040003D7 RID: 983
		private const string axisElement = "Axis";

		// Token: 0x040003D8 RID: 984
		private const string tuplesElement = "Tuples";

		// Token: 0x040003D9 RID: 985
		private const string tupleElement = "Tuple";

		// Token: 0x040003DA RID: 986
		private const string memberElement = "Member";

		// Token: 0x040003DB RID: 987
		private const string cellDataElement = "CellData";

		// Token: 0x040003DC RID: 988
		private const string cellElement = "Cell";

		// Token: 0x040003DD RID: 989
		internal const string cubeInfoElement = "CubeInfo";

		// Token: 0x040003DE RID: 990
		internal const string cubeElement = "Cube";

		// Token: 0x040003DF RID: 991
		internal const string cubeNameElement = "CubeName";

		// Token: 0x040003E0 RID: 992
		internal const string lastDataUpdateElement = "LastDataUpdate";

		// Token: 0x040003E1 RID: 993
		internal const string lastSchemaUpdateElement = "LastSchemaUpdate";

		// Token: 0x040003E2 RID: 994
		private const string nameAtribute = "name";

		// Token: 0x040003E3 RID: 995
		private const string hierarchyAttribute = "Hierarchy";

		// Token: 0x040003E4 RID: 996
		internal const string cellOrdinalAttribute = "CellOrdinal";

		// Token: 0x040003E5 RID: 997
		internal const string valueProperty = "Value";

		// Token: 0x040003E6 RID: 998
		internal const string frmValueProperty = "FmtValue";

		// Token: 0x040003E7 RID: 999
		internal const string uniqueNameProperty = "UName";

		// Token: 0x040003E8 RID: 1000
		internal const string levelNameProperty = "LName";

		// Token: 0x040003E9 RID: 1001
		internal const string levelNumberProperty = "LNum";

		// Token: 0x040003EA RID: 1002
		internal const string captionProperty = "Caption";

		// Token: 0x040003EB RID: 1003
		internal const string parentProperty = "Parent";

		// Token: 0x040003EC RID: 1004
		internal const string descriptionProperty = "Description";

		// Token: 0x040003ED RID: 1005
		internal const string displayInfoProperty = "DisplayInfo";

		// Token: 0x040003EE RID: 1006
		internal const string MemberPropertiesIndexMapProp = "MemberProperties";

		// Token: 0x040003EF RID: 1007
		private MDDatasetFormatter.DataSetFormatterAxis filterAxis;

		// Token: 0x040003F0 RID: 1008
		private MDDatasetFormatter.DataSetFormatterAxisCollection axesList;

		// Token: 0x040003F1 RID: 1009
		private DataTable cellTable;

		// Token: 0x040003F2 RID: 1010
		private DataTable cubesInfos;

		// Token: 0x0200019D RID: 413
		private class DataSetFormatterAxis : IDSFDataSet, ICollection, IEnumerable
		{
			// Token: 0x06001258 RID: 4696 RVA: 0x00040660 File Offset: 0x0003E860
			internal DataSetFormatterAxis(string datasetName)
			{
				this.name = datasetName;
				this.tables = new ArrayList();
				this.hash = new Dictionary<string, DataTable>(StringComparer.OrdinalIgnoreCase);
			}

			// Token: 0x06001259 RID: 4697 RVA: 0x0004068C File Offset: 0x0003E88C
			internal int AddTable(DataTable table)
			{
				if (this.hash.ContainsKey(table.TableName))
				{
					throw new AdomdUnknownResponseException(SR.DatasetResponse_HierarchyWithSameNameOnSameAxis(table.TableName, this.name), "");
				}
				this.tables.Add(table);
				this.hash[table.TableName] = table;
				return this.tables.Count - 1;
			}

			// Token: 0x1700065E RID: 1630
			// (get) Token: 0x0600125A RID: 4698 RVA: 0x000406F4 File Offset: 0x0003E8F4
			public string DataSetName
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x1700065F RID: 1631
			// (get) Token: 0x0600125B RID: 4699 RVA: 0x000406FC File Offset: 0x0003E8FC
			int ICollection.Count
			{
				get
				{
					return this.tables.Count;
				}
			}

			// Token: 0x17000660 RID: 1632
			DataTable IDSFDataSet.this[int index]
			{
				get
				{
					return (DataTable)this.tables[index];
				}
			}

			// Token: 0x17000661 RID: 1633
			DataTable IDSFDataSet.this[string index]
			{
				get
				{
					return this.hash[index];
				}
			}

			// Token: 0x0600125E RID: 4702 RVA: 0x0004072A File Offset: 0x0003E92A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.tables.GetEnumerator();
			}

			// Token: 0x0600125F RID: 4703 RVA: 0x00040737 File Offset: 0x0003E937
			bool IDSFDataSet.Contains(string tableName)
			{
				return this.hash.ContainsKey(tableName);
			}

			// Token: 0x06001260 RID: 4704 RVA: 0x00040745 File Offset: 0x0003E945
			void ICollection.CopyTo(Array array, int index)
			{
				this.tables.CopyTo(array, index);
			}

			// Token: 0x17000662 RID: 1634
			// (get) Token: 0x06001261 RID: 4705 RVA: 0x00040754 File Offset: 0x0003E954
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000663 RID: 1635
			// (get) Token: 0x06001262 RID: 4706 RVA: 0x00040757 File Offset: 0x0003E957
			object ICollection.SyncRoot
			{
				get
				{
					return this.tables.SyncRoot;
				}
			}

			// Token: 0x04000C74 RID: 3188
			private ArrayList tables;

			// Token: 0x04000C75 RID: 3189
			private Dictionary<string, DataTable> hash;

			// Token: 0x04000C76 RID: 3190
			private string name;
		}

		// Token: 0x0200019E RID: 414
		private class DataSetFormatterAxisCollection : IDSFAxisCollection, ICollection, IEnumerable
		{
			// Token: 0x06001263 RID: 4707 RVA: 0x00040764 File Offset: 0x0003E964
			internal DataSetFormatterAxisCollection()
			{
				this.collection = new ArrayList();
			}

			// Token: 0x06001264 RID: 4708 RVA: 0x00040777 File Offset: 0x0003E977
			internal int Add(IDSFDataSet set)
			{
				return this.collection.Add(set);
			}

			// Token: 0x17000664 RID: 1636
			public IDSFDataSet this[int index]
			{
				get
				{
					return (IDSFDataSet)this.collection[index];
				}
			}

			// Token: 0x17000665 RID: 1637
			// (get) Token: 0x06001266 RID: 4710 RVA: 0x00040798 File Offset: 0x0003E998
			int ICollection.Count
			{
				get
				{
					return this.collection.Count;
				}
			}

			// Token: 0x17000666 RID: 1638
			// (get) Token: 0x06001267 RID: 4711 RVA: 0x000407A5 File Offset: 0x0003E9A5
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000667 RID: 1639
			// (get) Token: 0x06001268 RID: 4712 RVA: 0x000407A8 File Offset: 0x0003E9A8
			object ICollection.SyncRoot
			{
				get
				{
					return this.collection.SyncRoot;
				}
			}

			// Token: 0x06001269 RID: 4713 RVA: 0x000407B5 File Offset: 0x0003E9B5
			void ICollection.CopyTo(Array array, int index)
			{
				this.collection.CopyTo(array, index);
			}

			// Token: 0x0600126A RID: 4714 RVA: 0x000407C4 File Offset: 0x0003E9C4
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.collection.GetEnumerator();
			}

			// Token: 0x04000C77 RID: 3191
			private ArrayList collection;
		}
	}
}
