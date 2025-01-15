using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000012 RID: 18
	public static class CsvUtilities
	{
		// Token: 0x0600006F RID: 111 RVA: 0x000035D0 File Offset: 0x000017D0
		public static string[] ParseCsvRow(string r, char delimiter = ',')
		{
			List<string> list = new List<string>();
			bool flag = false;
			string text = "";
			foreach (string text2 in r.Split(new char[] { delimiter }, 0))
			{
				if (flag)
				{
					if (text2.EndsWith("\""))
					{
						text = text + delimiter.ToString() + text2.Substring(0, text2.Length - 1);
						list.Add(text);
						text = "";
						flag = false;
					}
					else
					{
						text = text + delimiter.ToString() + text2;
					}
				}
				else if (text2.StartsWith("\"") && text2.EndsWith("\""))
				{
					if (text2.EndsWith("\"\"") && !text2.EndsWith("\"\"\"") && text2 != "\"\"")
					{
						flag = true;
						text = text2;
					}
					else
					{
						list.Add(text2.Substring(1, text2.Length - 2));
					}
				}
				else if (text2.StartsWith("\"") && !text2.EndsWith("\""))
				{
					flag = true;
					text += text2.Substring(1);
				}
				else
				{
					list.Add(text2);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003718 File Offset: 0x00001918
		public static DataTable ToDataTable(TextReader stream, bool firstRowContainsHeader = false, char delimiter = ',')
		{
			DataTable dataTable = null;
			int num = 0;
			if (firstRowContainsHeader)
			{
				dataTable = CsvFileDataReader.LoadSchemaTable(stream, delimiter);
				num = dataTable.Rows.Count;
			}
			DataTable dataTable2 = new DataTable();
			bool flag = true;
			try
			{
				string text;
				while ((text = stream.ReadLine()) != null)
				{
					string[] array = CsvUtilities.ParseCsvRow(text, delimiter);
					if (num != 0 && num != array.Length)
					{
						throw new Exception("Inconsistent number of columns detected!");
					}
					num = array.Length;
					if (flag)
					{
						for (int i = 0; i < num; i++)
						{
							if (dataTable != null)
							{
								dataTable2.Columns.Add(dataTable.Rows[i][SchemaTableColumn.ColumnName].ToString(), typeof(string));
							}
							else
							{
								dataTable2.Columns.Add("Column" + i, typeof(string));
							}
						}
						flag = false;
					}
					DataRowCollection rows = dataTable2.Rows;
					object[] array2 = array;
					rows.Add(array2);
				}
			}
			catch (EndOfStreamException)
			{
			}
			return dataTable2;
		}
	}
}
