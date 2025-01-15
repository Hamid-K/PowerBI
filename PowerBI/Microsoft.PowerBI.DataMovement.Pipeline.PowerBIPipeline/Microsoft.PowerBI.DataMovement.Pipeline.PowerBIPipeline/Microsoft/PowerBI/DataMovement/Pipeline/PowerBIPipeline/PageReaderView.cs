using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline
{
	// Token: 0x0200000C RID: 12
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class PageReaderView : IRawDataPageReader, IPageReader, IDisposable
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00003264 File Offset: 0x00001464
		internal PageReaderView(IPageReader pageReader, IReadOnlyDictionary<string, string> columnRenameMap)
			: this(pageReader, PageReaderView.GetColumnIndexByUpdatedColumnName(pageReader, columnRenameMap), null)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003275 File Offset: 0x00001475
		internal PageReaderView(IPageReader pageReader, IEnumerable<Tuple<string, string>> columnRenameView, IEnumerable<Tuple<string, List<string>>> dataRoles = null)
			: this(pageReader, PageReaderView.GetColumnIndexByUpdatedColumnName(pageReader, columnRenameView), dataRoles)
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003288 File Offset: 0x00001488
		private PageReaderView(IPageReader pageReader, IList<Tuple<string, int>> columnIndexInOriginalSchemaTableByUpdatedColumnName, IEnumerable<Tuple<string, List<string>>> dataRoles = null)
		{
			if (columnIndexInOriginalSchemaTableByUpdatedColumnName.Distinct(new PageReaderView.ColumnViewComparer()).Count<Tuple<string, int>>() != columnIndexInOriginalSchemaTableByUpdatedColumnName.Count)
			{
				throw new ArgumentException("Columns View contains duplicate columns");
			}
			this.m_underlyingPageReader = pageReader;
			this.m_columnNames = columnIndexInOriginalSchemaTableByUpdatedColumnName.Select((Tuple<string, int> columnTuple) => columnTuple.Item1).ToList<string>();
			if (dataRoles == null)
			{
				dataRoles = new List<Tuple<string, List<string>>> { Tuple.Create<string, List<string>>(PageReaderView.DefaultDataRoleName, this.m_columnNames) };
			}
			List<List<string>> list = dataRoles.Select((Tuple<string, List<string>> dataRoleWithColumnNames) => dataRoleWithColumnNames.Item2).ToList<List<string>>();
			if (list.Any((List<string> streamColumnNames) => streamColumnNames == null || streamColumnNames.Count == 0))
			{
				throw new ArgumentException("tablesColumnNames should contain non empty lists of columns names", "tablesColumnNames");
			}
			if (list.Any((List<string> streamColumnNames) => streamColumnNames.Any((string columnName) => string.IsNullOrWhiteSpace(columnName))))
			{
				throw new ArgumentException("tablesColumnNames should contain non empty column names", "tablesColumnNames");
			}
			this.m_columnIndexInOriginalSchemaTableByUpdatedColumnNameLookup = columnIndexInOriginalSchemaTableByUpdatedColumnName.ToDictionary((Tuple<string, int> columnNameTuple) => columnNameTuple.Item1, (Tuple<string, int> columnNameTuple) => columnNameTuple.Item2, StringComparer.OrdinalIgnoreCase);
			int value = 0;
			Func<string, bool> <>9__9;
			if (list.Any(delegate(List<string> tableColumnNames)
			{
				Func<string, bool> func;
				if ((func = <>9__9) == null)
				{
					func = (<>9__9 = (string columnName) => !this.m_columnIndexInOriginalSchemaTableByUpdatedColumnNameLookup.TryGetValue(columnName, out value));
				}
				return tableColumnNames.Any(func);
			}))
			{
				throw new ArgumentException("Column names from tablesColumnNames should exist in columnsViewLookup", "tablesColumnNames");
			}
			this.m_dataRoles = dataRoles;
			this.m_schemaTables = this.m_dataRoles.SelectWithSafeDispose((Tuple<string, List<string>> dataRoleWithColumnNames) => this.CreateSchemaTableForDataRole(dataRoleWithColumnNames));
			this.cancelIssued = false;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003460 File Offset: 0x00001660
		public DataTable SchemaTable
		{
			get
			{
				return this.m_schemaTables.FirstOrDefault<DataTable>();
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000346D File Offset: 0x0000166D
		public IEnumerable<DataTable> SchemaTables
		{
			get
			{
				return this.m_schemaTables;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003475 File Offset: 0x00001675
		public IProgress Progress
		{
			get
			{
				return this.m_underlyingPageReader.Progress;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003482 File Offset: 0x00001682
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000348C File Offset: 0x0000168C
		public bool CancelIssued
		{
			get
			{
				return this.cancelIssued;
			}
			set
			{
				this.cancelIssued = value;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003498 File Offset: 0x00001698
		private static IList<Tuple<string, int>> GetColumnIndexByUpdatedColumnName(IPageReader pageReader, IReadOnlyDictionary<string, string> columnRenameMap)
		{
			DataTable schemaTable = pageReader.SchemaTable;
			List<Tuple<string, int>> list = new List<Tuple<string, int>>();
			for (int i = 0; i < schemaTable.Rows.Count; i++)
			{
				string text = (string)schemaTable.Rows[i]["ColumnName"];
				string text2;
				if (!columnRenameMap.TryGetValue(text, out text2))
				{
					text2 = text;
				}
				list.Add(Tuple.Create<string, int>(text2, i));
			}
			return list;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003504 File Offset: 0x00001704
		private static IList<Tuple<string, int>> GetColumnIndexByUpdatedColumnName(IPageReader underlyingPageReader, IEnumerable<Tuple<string, string>> columnRenameView)
		{
			DataTable schemaTable = underlyingPageReader.SchemaTable;
			Dictionary<string, int> columnIndexByColumnName = new Dictionary<string, int>();
			for (int i = 0; i < schemaTable.Rows.Count; i++)
			{
				string text = (string)schemaTable.Rows[i]["ColumnName"];
				columnIndexByColumnName[text] = i;
			}
			return columnRenameView.Select((Tuple<string, string> c) => c.MapColumn(columnIndexByColumnName)).ToList<Tuple<string, int>>();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003580 File Offset: 0x00001780
		private DataTable CreateSchemaTableForDataRole(Tuple<string, List<string>> dataRoleWithColumnNames)
		{
			DataTable schemaTable = this.m_underlyingPageReader.SchemaTable;
			DataTable dataTable = null;
			try
			{
				dataTable = schemaTable.Clone();
				string item = dataRoleWithColumnNames.Item1;
				List<string> item2 = dataRoleWithColumnNames.Item2;
				for (int i = 0; i < item2.Count; i++)
				{
					string text = item2[i];
					int num = 0;
					if (!this.m_columnIndexInOriginalSchemaTableByUpdatedColumnNameLookup.TryGetValue(text, out num))
					{
						throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Column '{0}' is associated with data role '{1}' but it is not available in the original schema table", text, item));
					}
					if (!string.IsNullOrWhiteSpace(item))
					{
						int num2 = item.Length + 1;
						if (text.Length > num2 && text.StartsWith(item + ".", StringComparison.OrdinalIgnoreCase))
						{
							text = text.Substring(num2);
						}
					}
					DataRow dataRow = schemaTable.Rows[num].CloneToTable(dataTable);
					dataRow["ColumnName"] = text;
					dataRow["ColumnGuid"] = Guid.NewGuid();
					dataTable.Rows.Add(dataRow);
				}
				if (dataTable.Rows.Count == 0)
				{
					string text2 = string.Join(", ", item2.Select((string columnName) => "'" + columnName + "'"));
					throw new ArgumentException("Schema table creation failure. The following columns are not available in the schema: " + text2, "tableColumnNames");
				}
			}
			catch
			{
				if (dataTable != null)
				{
					try
					{
						dataTable.Dispose();
					}
					catch (Exception ex)
					{
						TraceSourceBase<PowerBIRawDataTraceSource>.Tracer.TraceError("Dispose failed: {0}", new object[] { ex });
					}
				}
				throw;
			}
			return dataTable;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000373C File Offset: 0x0000193C
		public IPage CreatePage()
		{
			return this.m_underlyingPageReader.CreatePage();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003749 File Offset: 0x00001949
		public void Read(IPage page)
		{
			this.m_underlyingPageReader.Read(page);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003757 File Offset: 0x00001957
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003766 File Offset: 0x00001966
		protected virtual void Dispose(bool disposing)
		{
			if (this.m_disposed)
			{
				return;
			}
			if (disposing)
			{
				this.m_schemaTables.ConcatElement(this.m_underlyingPageReader).ForEachDispose<IDisposable>();
			}
			this.m_disposed = true;
		}

		// Token: 0x04000037 RID: 55
		private static readonly string DefaultDataRoleName = string.Empty;

		// Token: 0x04000038 RID: 56
		private readonly IPageReader m_underlyingPageReader;

		// Token: 0x04000039 RID: 57
		private readonly List<string> m_columnNames;

		// Token: 0x0400003A RID: 58
		private readonly IDictionary<string, int> m_columnIndexInOriginalSchemaTableByUpdatedColumnNameLookup;

		// Token: 0x0400003B RID: 59
		private readonly IEnumerable<DataTable> m_schemaTables;

		// Token: 0x0400003C RID: 60
		private readonly IEnumerable<Tuple<string, List<string>>> m_dataRoles;

		// Token: 0x0400003D RID: 61
		private bool m_disposed;

		// Token: 0x0400003E RID: 62
		private volatile bool cancelIssued;

		// Token: 0x02000034 RID: 52
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal class ColumnViewComparer : IEqualityComparer<Tuple<string, int>>
		{
			// Token: 0x060000F1 RID: 241 RVA: 0x000074A2 File Offset: 0x000056A2
			public bool Equals(Tuple<string, int> x, Tuple<string, int> y)
			{
				if (x == null || y == null)
				{
					return x == y;
				}
				return string.Equals(x.Item1, y.Item1, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x000074C1 File Offset: 0x000056C1
			public int GetHashCode(Tuple<string, int> obj)
			{
				if (obj == null || string.IsNullOrEmpty(obj.Item1))
				{
					return 0;
				}
				return obj.Item1.ToLowerInvariant().GetHashCode();
			}
		}
	}
}
