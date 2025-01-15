using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Mashup.Engine1.Library.Mdx;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000440 RID: 1088
	internal sealed class SapHanaDimensionCollection2 : SapHanaDimensionCollection
	{
		// Token: 0x060024F7 RID: 9463 RVA: 0x000696EA File Offset: 0x000678EA
		public SapHanaDimensionCollection2(SapHanaOdbcDataSource dataSource, SapHanaCubeBase cube)
			: base(dataSource, cube)
		{
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x0006987C File Offset: 0x00067A7C
		protected override Dictionary<string, SapHanaDimension> GetDimensions()
		{
			Dictionary<string, SapHanaDimension> dictionary = new Dictionary<string, SapHanaDimension>();
			SapHanaDimension sapHanaDimension = new SapHanaDimension(MdxIdentifier.QuotePart(this.cube.Name), this.cube.Name);
			using (IDataReader dataReader = SapHanaDimensionCollection2.DimensionsQuery.Execute(this.dataSource, this.cube))
			{
				while (dataReader.Read())
				{
					string @string = dataReader.GetString(0);
					string string2 = dataReader.GetString(1);
					if (@string != "[Measures]")
					{
						SapHanaDimension sapHanaDimension2 = new SapHanaDimension(@string, string2);
						dictionary.Add(@string, sapHanaDimension2);
					}
				}
			}
			using (IDataReader dataReader2 = SapHanaDimensionCollection2.HierarchiesQuery.Execute(this.dataSource, this.cube))
			{
				while (dataReader2.Read())
				{
					string string3 = dataReader2.GetString(0);
					string string4 = dataReader2.GetString(1);
					string string5 = dataReader2.GetString(2);
					SapHanaDimension sapHanaDimension3;
					if (dictionary.TryGetValue(string3, out sapHanaDimension3))
					{
						SapHanaHierarchy sapHanaHierarchy = new SapHanaHierarchy(sapHanaDimension3, string4, string5);
						sapHanaDimension3.Hierarchies.Add(string4, sapHanaHierarchy);
						sapHanaDimension.Hierarchies.Add(string4, sapHanaHierarchy);
					}
				}
			}
			using (IDataReader dataReader3 = SapHanaDimensionCollection2.AttributesQuery.Execute(this.dataSource, this.cube))
			{
				while (dataReader3.Read())
				{
					string string6 = dataReader3.GetString(0);
					string string7 = dataReader3.GetString(1);
					string string8 = dataReader3.GetString(2);
					string string9 = dataReader3.GetString(3);
					string string10 = dataReader3.GetString(4);
					SapHanaDimension sapHanaDimension4;
					if (dictionary.TryGetValue(string6, out sapHanaDimension4))
					{
						string text = SapHanaDimensionCollection2.NewAttributeName(sapHanaDimension.Hierarchies, string6, string7);
						SapHanaColumn columnOrThrow = this.GetColumnOrThrow(string8);
						SapHanaColumn columnOrThrow2 = this.GetColumnOrThrow(string10);
						ColumnSapHanaDimensionAttribute columnSapHanaDimensionAttribute = new ColumnSapHanaDimensionAttribute(sapHanaDimension4, text, string9, columnOrThrow, columnOrThrow2);
						sapHanaDimension4.Attributes.Add(columnSapHanaDimensionAttribute.Name, columnSapHanaDimensionAttribute);
						sapHanaDimension.Attributes.Add(columnSapHanaDimensionAttribute.Name, columnSapHanaDimensionAttribute);
					}
				}
			}
			using (IDataReader dataReader4 = SapHanaDimensionCollection2.parentChildHierarchiesQuery.Execute(this.dataSource, this.cube))
			{
				while (dataReader4.Read())
				{
					string string11 = dataReader4.GetString(0);
					string string12 = dataReader4.GetString(1);
					SapHanaDimension sapHanaDimension5;
					if (dictionary.TryGetValue(string11, out sapHanaDimension5))
					{
						sapHanaDimension5.Hierarchies.Remove(string12);
					}
					sapHanaDimension.Hierarchies.Remove(string12);
				}
			}
			using (IDataReader dataReader5 = SapHanaDimensionCollection2.LevelsQuery.Execute(this.dataSource, this.cube))
			{
				while (dataReader5.Read())
				{
					string string13 = dataReader5.GetString(0);
					string string14 = dataReader5.GetString(1);
					string string15 = dataReader5.GetString(2);
					int @int = dataReader5.GetInt32(3);
					string string16 = dataReader5.GetString(4);
					SapHanaDimension sapHanaDimension6;
					SapHanaHierarchy sapHanaHierarchy2;
					if (dictionary.TryGetValue(string13, out sapHanaDimension6) && sapHanaDimension6.Hierarchies.TryGetValue(string14, out sapHanaHierarchy2))
					{
						SapHanaLevel sapHanaLevel = new SapHanaLevel(sapHanaHierarchy2, string15, @int, string16);
						sapHanaHierarchy2.Levels.Add(sapHanaLevel);
					}
				}
			}
			using (IDataReader dataReader6 = SapHanaDimensionCollection2.levelAttributesQuery.Execute(this.dataSource, this.cube))
			{
				while (dataReader6.Read())
				{
					string string17 = dataReader6.GetString(0);
					string string18 = dataReader6.GetString(1);
					int int2 = dataReader6.GetInt32(2);
					string stringOrNull = dataReader6.GetStringOrNull(3);
					if (stringOrNull != null)
					{
						string text2 = SapHanaDimensionCollection2.NewAttributeName(sapHanaDimension.Hierarchies, string17, stringOrNull);
						SapHanaDimension sapHanaDimension7;
						SapHanaHierarchy sapHanaHierarchy3;
						SapHanaDimensionAttribute sapHanaDimensionAttribute;
						if (dictionary.TryGetValue(string17, out sapHanaDimension7) && sapHanaDimension7.Hierarchies.TryGetValue(string18, out sapHanaHierarchy3) && sapHanaDimension7.Attributes.TryGetValue(text2, out sapHanaDimensionAttribute))
						{
							sapHanaHierarchy3.Levels[int2].Attribute = sapHanaDimensionAttribute;
						}
					}
				}
			}
			foreach (SapHanaDimension sapHanaDimension8 in dictionary.Values)
			{
				foreach (SapHanaHierarchy sapHanaHierarchy4 in sapHanaDimension8.Hierarchies.Values)
				{
					for (int i = sapHanaHierarchy4.Levels.Count - 1; i >= 0; i--)
					{
						if (sapHanaHierarchy4.Levels[i].Attribute == null)
						{
							sapHanaHierarchy4.Levels.RemoveAt(i);
						}
					}
					for (int j = 0; j < sapHanaHierarchy4.Levels.Count; j++)
					{
						sapHanaHierarchy4.Levels[j].Number = j;
					}
				}
			}
			foreach (SapHanaDimension sapHanaDimension9 in dictionary.Values)
			{
				foreach (SapHanaHierarchy sapHanaHierarchy5 in sapHanaDimension9.Hierarchies.Values)
				{
					sapHanaHierarchy5.Levels.Sort((SapHanaLevel x, SapHanaLevel y) => x.Number - y.Number);
				}
			}
			return new Dictionary<string, SapHanaDimension> { { sapHanaDimension.Name, sapHanaDimension } };
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x00069E24 File Offset: 0x00068024
		private static string NewAttributeName(Dictionary<string, SapHanaHierarchy> hierarchies, string dimensionName, string attributeLocalName)
		{
			string text = dimensionName + "." + MdxIdentifier.QuotePart(attributeLocalName);
			if (hierarchies.ContainsKey(text))
			{
				text += ".Attribute";
			}
			return text;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x00069E5C File Offset: 0x0006805C
		private SapHanaColumn GetColumnOrThrow(string columnName)
		{
			OdbcColumnInfo odbcColumnInfo;
			if (!this.cube.Columns.TryGetColumnInfo(columnName, out odbcColumnInfo))
			{
				throw ValueException.NewExpressionError<Message2>(Strings.ValueException_MissingCubeColumn(columnName, this.cube.Name), TextValue.New(columnName), null);
			}
			return new SapHanaColumn(odbcColumnInfo);
		}

		// Token: 0x04000EED RID: 3821
		internal static readonly MdxMetadataQuery DimensionsQuery = new MdxMetadataQuery
		{
			OnlyUseMdxPrefixForV1SPS11AndBelow = true,
			ColumnNames = new string[] { "DIMENSION_UNIQUE_NAME", "DIMENSION_CAPTION" },
			TableName = "BIMC_DIMENSIONS",
			WhereClause = "CATALOG_NAME = {catalog} and CUBE_NAME = {cube}"
		};

		// Token: 0x04000EEE RID: 3822
		internal static readonly MdxMetadataQuery HierarchiesQuery = new MdxMetadataQuery
		{
			ColumnNames = new string[] { "DIMENSION_UNIQUE_NAME", "HIERARCHY_UNIQUE_NAME", "HIERARCHY_CAPTION" },
			TableName = "BIMC_HIERARCHIES",
			WhereClause = "CATALOG_NAME = {catalog} and CUBE_NAME = {cube}"
		};

		// Token: 0x04000EEF RID: 3823
		internal static readonly MdxMetadataQuery LevelsQuery = new MdxMetadataQuery
		{
			ColumnNames = new string[] { "DIMENSION_UNIQUE_NAME", "HIERARCHY_UNIQUE_NAME", "LEVEL_UNIQUE_NAME", "LEVEL_NUMBER", "LEVEL_CAPTION" },
			TableName = "BIMC_LEVELS",
			WhereClause = "CATALOG_NAME = {catalog} and CUBE_NAME = {cube}"
		};

		// Token: 0x04000EF0 RID: 3824
		internal static readonly MdxMetadataQuery AttributesQuery = new MdxMetadataQuery
		{
			OnlyUseMdxPrefixForV1SPS11AndBelow = true,
			ColumnNames = new string[] { "DIMENSION_UNIQUE_NAME", "DIMENSION_COLUMN_NAME", "COLUMN_NAME", "COLUMN_CAPTION", "DESC_NAME" },
			TableName = "BIMC_DIMENSION_VIEW",
			WhereClause = "CATALOG_NAME = {catalog} and CUBE_NAME = {cube} and DIMENSION_UNIQUE_NAME <> '[Measures]'"
		};

		// Token: 0x04000EF1 RID: 3825
		private static readonly PermissionsAwareMetadataQuery levelAttributesQuery = new PermissionsAwareMetadataQuery(new MetadataQuery
		{
			ColumnNames = new string[] { "LEVELS.DIMENSION_UNIQUE_NAME", "LEVELS.HIERARCHY_UNIQUE_NAME", "LEVELS.LEVEL_NUMBER", "DIMENSIONS.COLUMN_NAME" },
			Query = "select {columns}\r\nfrom\r\n(\r\n    select COLUMNOBJECT_NAME, DIMENSION_UNIQUE_NAME, HIERARCHY_UNIQUE_NAME, LEVEL_NUMBER, COLUMN_NAME\r\n    from _SYS_BI.BIMC_PROPERTIES\r\n    where CATALOG_NAME = {catalog} and CUBE_NAME = {cube} and DIMENSION_UNIQUE_NAME <> '[Measures]' and COLUMN_FLAG = 'Level Attribute'\r\n) as LEVELS\r\nleft outer join\r\n(\r\n    select COLUMNOBJECT_NAME, DIMENSION_UNIQUE_NAME, COLUMN_NAME\r\n    from _SYS_BI.BIMC_PROPERTIES\r\n    where CATALOG_NAME = {catalog} and CUBE_NAME = {cube} and COLUMN_FLAG = 'Dimension Attribute'\r\n) as DIMENSIONS\r\non LEVELS.COLUMN_NAME = DIMENSIONS.COLUMN_NAME and LEVELS.COLUMNOBJECT_NAME = DIMENSIONS.COLUMNOBJECT_NAME and LEVELS.DIMENSION_UNIQUE_NAME = DIMENSIONS.DIMENSION_UNIQUE_NAME"
		}, new MetadataQuery
		{
			ColumnNames = new string[] { "DIMENSION_UNIQUE_NAME", "HIERARCHY_UNIQUE_NAME", "LEVEL_NUMBER", "LEVEL_COLUMN_NAME" },
			Query = "select {columns}\r\nfrom _SYS_BI.BIMC_LEVELS\r\nwhere CATALOG_NAME = {catalog} and CUBE_NAME = {cube}\r\n"
		});

		// Token: 0x04000EF2 RID: 3826
		private static readonly PermissionsAwareMetadataQuery parentChildHierarchiesQuery = new PermissionsAwareMetadataQuery(new MetadataQuery
		{
			ColumnNames = new string[] { "DIMENSION_UNIQUE_NAME", "HIERARCHY_UNIQUE_NAME" },
			Query = "select {columns}\r\nfrom _SYS_BI.BIMC_PROPERTIES\r\nwhere CATALOG_NAME = {catalog} and CUBE_NAME = {cube} and PROPERTY_NAME = 'HIERARCHY_PARENT_ATTRIBUTE'"
		}, new MetadataQuery
		{
			ColumnNames = new string[] { "DIMENSION_UNIQUE_NAME", "HIERARCHY_UNIQUE_NAME" },
			Query = "select {columns}\r\nfrom _SYS_BI.BIMC_HIERARCHIES_SQL\r\nwhere CATALOG_NAME = {catalog} and CUBE_NAME = {cube}"
		});
	}
}
