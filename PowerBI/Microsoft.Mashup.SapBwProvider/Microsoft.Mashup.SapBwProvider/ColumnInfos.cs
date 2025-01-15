using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200000B RID: 11
	public class ColumnInfos
	{
		// Token: 0x0600009A RID: 154 RVA: 0x0000392E File Offset: 0x00001B2E
		private ColumnInfos(HashSet<string> columnNames, Dictionary<string, ColumnInfo> columnInfos)
		{
			this.columnNames = columnNames;
			this.columnInfos = columnInfos;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003944 File Offset: 0x00001B44
		public HashSet<string> ColumnNames
		{
			get
			{
				return this.columnNames;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000394C File Offset: 0x00001B4C
		public static ColumnInfos NewOrNull(SapBwConnection connection, object[][] infos)
		{
			if (infos == null || connection == null)
			{
				return null;
			}
			HashSet<string> hashSet = new HashSet<string>();
			Dictionary<string, ColumnInfo> dictionary = new Dictionary<string, ColumnInfo>();
			foreach (object[] array in infos)
			{
				if (array != null && array.Length == 7)
				{
					string text = array[2] as string;
					if (text != null)
					{
						hashSet.Add(text);
						if ((int)array[0] == 0)
						{
							string text2 = array[5] as string;
							SapBwDataType sapBwDataType;
							if (!string.IsNullOrEmpty(text2) && MdxColumn.DataTypesByName.TryGetValue(text2, out sapBwDataType))
							{
								dictionary[text] = new ColumnInfo(sapBwDataType, null, array[6] as int?);
							}
						}
						else
						{
							string text3 = array[3] as string;
							if (text3 != null)
							{
								hashSet.Add(text3);
								OleDbType oleDbType = (OleDbType)((int)array[1]);
								SapBwDataType sapBwDataType;
								if ((bool)array[4] && MdxColumn.OleDbTypeToSapBwType.TryGetValue(oleDbType, out sapBwDataType))
								{
									dictionary[text] = new ColumnInfo(sapBwDataType, text3, null);
								}
							}
						}
					}
				}
			}
			if (hashSet.Count <= 0)
			{
				return null;
			}
			return new ColumnInfos(hashSet, dictionary);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003A71 File Offset: 0x00001C71
		public bool TryGetColumnInfo(string columnName, out ColumnInfo columnInfo)
		{
			return this.columnInfos.TryGetValue(columnName, out columnInfo);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003A80 File Offset: 0x00001C80
		public bool ContainsCellProperty(string measureUniqueName, string cellProperty)
		{
			return this.ColumnNames.Contains(Utils.BuildColumnName(measureUniqueName, cellProperty));
		}

		// Token: 0x04000018 RID: 24
		public const int CubeObjectKindIndex = 0;

		// Token: 0x04000019 RID: 25
		public const int OleDbTypeIndex = 1;

		// Token: 0x0400001A RID: 26
		public const int ValueColumnNameIndex = 2;

		// Token: 0x0400001B RID: 27
		public const int MdxIdentifierColumnNameIndex = 3;

		// Token: 0x0400001C RID: 28
		public const int IsKeyDimensionProperty = 4;

		// Token: 0x0400001D RID: 29
		public const int MeasureValueType = 5;

		// Token: 0x0400001E RID: 30
		public const int MeasurePrecision = 6;

		// Token: 0x0400001F RID: 31
		private readonly HashSet<string> columnNames;

		// Token: 0x04000020 RID: 32
		private readonly Dictionary<string, ColumnInfo> columnInfos;
	}
}
