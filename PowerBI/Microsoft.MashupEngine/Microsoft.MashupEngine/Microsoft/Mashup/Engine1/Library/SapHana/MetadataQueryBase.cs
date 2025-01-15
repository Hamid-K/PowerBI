using System;
using System.Data;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200041A RID: 1050
	internal abstract class MetadataQueryBase
	{
		// Token: 0x17000EBF RID: 3775
		// (set) Token: 0x060023D8 RID: 9176 RVA: 0x000650AB File Offset: 0x000632AB
		public string[] ColumnNames
		{
			set
			{
				this.columnNames = value;
			}
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000650B4 File Offset: 0x000632B4
		public IDataReader Execute(SapHanaOdbcDataSource dataSource, SapHanaCubeBase cube)
		{
			return this.Execute(dataSource, cube, null);
		}

		// Token: 0x060023DA RID: 9178
		public abstract string GetQuery(SapHanaOdbcDataSource dataSource);

		// Token: 0x060023DB RID: 9179 RVA: 0x000650BF File Offset: 0x000632BF
		public virtual string[] GetColumnNames(SapHanaOdbcDataSource dataSource)
		{
			return this.columnNames;
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000650C8 File Offset: 0x000632C8
		public virtual IDataReader Execute(SapHanaOdbcDataSource dataSource, SapHanaCubeBase cube, string variableName)
		{
			OdbcParameter odbcParameter = new OdbcParameter(cube.CatalogName, OdbcTypeMap.WVarchar);
			OdbcParameter odbcParameter2 = new OdbcParameter(cube.Name, OdbcTypeMap.WVarchar);
			OdbcParameter odbcParameter3 = new OdbcParameter(variableName, OdbcTypeMap.WVarchar);
			string query = this.GetQuery(dataSource);
			string[] array = this.GetColumnNames(dataSource);
			ArrayBuilder<OdbcParameter> arrayBuilder = default(ArrayBuilder<OdbcParameter>);
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < query.Length)
			{
				int num = query.IndexOf("{", i, StringComparison.Ordinal);
				if (num < 0)
				{
					num = query.Length;
				}
				int num2 = num - i;
				stringBuilder.Append(query, i, num2);
				i += num2;
				if (MetadataQueryBase.Equals(query, i, "{columns}"))
				{
					MetadataQueryBase.AppendColumnNames(stringBuilder, array);
					i += "{columns}".Length;
				}
				else if (MetadataQueryBase.Equals(query, i, "{catalog}"))
				{
					arrayBuilder.Add(odbcParameter);
					stringBuilder.Append("?");
					i += "{catalog}".Length;
				}
				else if (MetadataQueryBase.Equals(query, i, "{cube}"))
				{
					arrayBuilder.Add(odbcParameter2);
					stringBuilder.Append("?");
					i += "{cube}".Length;
				}
				else if (variableName != null && MetadataQueryBase.Equals(query, i, "{variable}"))
				{
					arrayBuilder.Add(odbcParameter3);
					stringBuilder.Append("?");
					i += "{variable}".Length;
				}
			}
			return dataSource.Execute(dataSource.Host.GetMetadataCache(), stringBuilder.ToString(), null, arrayBuilder.ToArray(), RowRange.All, array, true, null);
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x00065265 File Offset: 0x00063465
		private static bool Equals(string query, int index, string comparand)
		{
			return string.Compare(query, index, comparand, 0, comparand.Length, StringComparison.Ordinal) == 0;
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x0006527C File Offset: 0x0006347C
		private static void AppendColumnNames(StringBuilder builder, string[] columnNames)
		{
			for (int i = 0; i < columnNames.Length; i++)
			{
				if (i != 0)
				{
					builder.Append(", ");
				}
				builder.Append(columnNames[i]);
			}
		}

		// Token: 0x04000E64 RID: 3684
		protected const string columnsMarker = "{columns}";

		// Token: 0x04000E65 RID: 3685
		private const string catalogMarker = "{catalog}";

		// Token: 0x04000E66 RID: 3686
		private const string cubeMarker = "{cube}";

		// Token: 0x04000E67 RID: 3687
		private const string variableMarker = "{variable}";

		// Token: 0x04000E68 RID: 3688
		private string[] columnNames;
	}
}
