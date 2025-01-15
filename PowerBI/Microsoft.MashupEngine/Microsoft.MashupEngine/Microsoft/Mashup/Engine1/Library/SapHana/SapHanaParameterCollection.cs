using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200046F RID: 1135
	internal sealed class SapHanaParameterCollection : IEnumerable<SapHanaParameter>, IEnumerable
	{
		// Token: 0x060025C2 RID: 9666 RVA: 0x0006D056 File Offset: 0x0006B256
		public SapHanaParameterCollection(SapHanaOdbcDataSource dataSource, SapHanaCubeBase cube)
		{
			this.dataSource = dataSource;
			this.cube = cube;
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x0006D06C File Offset: 0x0006B26C
		public IEnumerator<SapHanaParameter> GetEnumerator()
		{
			this.EnsureInitialized();
			foreach (SapHanaParameter sapHanaParameter in this.parameters.Values.OrderBy((SapHanaParameter p) => p.Order))
			{
				yield return sapHanaParameter;
			}
			IEnumerator<SapHanaParameter> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x0006D07B File Offset: 0x0006B27B
		public bool TryGetParameter(string name, out SapHanaParameter parameter)
		{
			this.EnsureInitialized();
			return this.parameters.TryGetValue(name, out parameter);
		}

		// Token: 0x17000F33 RID: 3891
		public SapHanaParameter this[string name]
		{
			get
			{
				SapHanaParameter sapHanaParameter;
				if (!this.TryGetParameter(name, out sapHanaParameter))
				{
					throw new ArgumentOutOfRangeException(name);
				}
				return sapHanaParameter;
			}
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x0006D0B0 File Offset: 0x0006B2B0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x0006D0B8 File Offset: 0x0006B2B8
		private void EnsureInitialized()
		{
			if (this.parameters == null)
			{
				this.parameters = new Dictionary<string, SapHanaParameter>();
				if (!this.cube.HasParameters)
				{
					return;
				}
				this.LoadParameters(SapHanaParameterCollection.parametersQuery, (IDataReader r) => null);
				this.LoadParameters(SapHanaParameterCollection.variablesQuery, (IDataReader r) => r.GetStringOrNull(13));
			}
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x0006D13C File Offset: 0x0006B33C
		private void LoadParameters(MetadataQueryBase query, Func<IDataReader, string> getModelElementName)
		{
			Dictionary<string, List<Value>> dictionary = new Dictionary<string, List<Value>>();
			using (IDataReader dataReader = SapHanaParameterCollection.DefaultValueQuery.Execute(this.dataSource, this.cube))
			{
				while (dataReader.Read())
				{
					string @string = dataReader.GetString(0);
					string string2 = dataReader.GetString(1);
					if (dataReader.GetInt32(2) == 1)
					{
						List<Value> multiLineDefaults = this.GetMultiLineDefaults(@string);
						if (multiLineDefaults.Any<Value>())
						{
							dictionary.Add(@string, multiLineDefaults);
						}
					}
					else if (!string.IsNullOrEmpty(string2))
					{
						dictionary.Add(@string, new List<Value> { TextValue.New(string2) });
					}
				}
			}
			using (IDataReader dataReader2 = query.Execute(this.dataSource, this.cube))
			{
				while (dataReader2.Read())
				{
					string string3 = dataReader2.GetString(0);
					string uniqueName = SapHanaParameterCollection.GetUniqueName(this.parameters, string3);
					string text = getModelElementName(dataReader2);
					string text2 = dataReader2.GetStringOrNull(12);
					string text3 = dataReader2[2] as string;
					string text4;
					if (!SapHanaParameterCollection.TryGetDataType(this.cube, text, text3, out text4))
					{
						text4 = this.ExtractParameterType(dataReader2.GetString(1));
					}
					if (text4.Equals("DAYDATE"))
					{
						text4 = "DATE";
					}
					if (!string.IsNullOrEmpty(text2))
					{
						text2 = SapHanaParameterCollection.QuotedAttributeSource(text2);
					}
					SapHanaParameter sapHanaParameter = new SapHanaParameter(this.dataSource, this.cube, uniqueName, text4, SapHanaParameterCollection.GetValueType(dataReader2.GetString(3)), SapHanaParameterCollection.GetSelectionType(dataReader2.GetString(5)), dataReader2.GetInt32(7) == 1, dataReader2.GetInt32(8), dataReader2[9] as string, text3, dataReader2[4] as string, dataReader2.GetInt32(6) == 1, dataReader2.GetStringOrNull(10), dataReader2.GetStringOrNull(11), dictionary.GetValueOrDefault(string3, null), text2, text);
					this.parameters.Add(sapHanaParameter.Name, sapHanaParameter);
				}
			}
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x0006D360 File Offset: 0x0006B560
		private static bool TryGetDataType(SapHanaCubeBase cube, string modelElementName, string parameterPlaceholderName, out string newDataType)
		{
			OdbcColumnInfo odbcColumnInfo;
			if (string.IsNullOrEmpty(parameterPlaceholderName) && modelElementName != null && cube.Columns.TryGetColumnInfo(modelElementName, out odbcColumnInfo))
			{
				newDataType = odbcColumnInfo.TypeName;
				return true;
			}
			newDataType = null;
			return false;
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x0006D396 File Offset: 0x0006B596
		private static string QuotedAttributeSource(string attributeSource)
		{
			return string.Join(".", SapHanaParameterCollection.ExtractParts(attributeSource).Select(delegate(string x)
			{
				if (!x.StartsWith("\"", StringComparison.Ordinal))
				{
					return "\"" + x + "\"";
				}
				return x;
			}).ToArray<string>());
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x0006D3D1 File Offset: 0x0006B5D1
		private static IEnumerable<string> ExtractParts(string input)
		{
			int schemaDelimiterIdx = input.IndexOf(".", StringComparison.Ordinal);
			if (schemaDelimiterIdx == -1 || input.Length <= schemaDelimiterIdx + 1)
			{
				yield return input;
			}
			else
			{
				yield return input.Substring(0, schemaDelimiterIdx);
				yield return input.Substring(schemaDelimiterIdx + 1);
			}
			yield break;
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x0006D3E4 File Offset: 0x0006B5E4
		private List<Value> GetMultiLineDefaults(string variableName)
		{
			List<Value> list = new List<Value>();
			using (IDataReader dataReader = SapHanaParameterCollection.DefaultMultiValueQuery.Execute(this.dataSource, this.cube, variableName))
			{
				while (dataReader.Read())
				{
					string @string = dataReader.GetString(0);
					if (!string.IsNullOrEmpty(@string))
					{
						list.Add(TextValue.New(@string));
					}
				}
			}
			return list;
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x0006D454 File Offset: 0x0006B654
		private static string GetUniqueName(Dictionary<string, SapHanaParameter> parameters, string columnName)
		{
			string text = columnName;
			int num = 2;
			while (parameters.ContainsKey(text))
			{
				text = columnName + "_" + num.ToString();
				num++;
			}
			return text;
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x0006D488 File Offset: 0x0006B688
		private static SapHanaValueType GetValueType(string valueType)
		{
			if (valueType == "StaticList")
			{
				return SapHanaValueType.StaticList;
			}
			if (!(valueType == "AttributeValue"))
			{
				return SapHanaValueType.None;
			}
			return SapHanaValueType.AttributeValue;
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x0006D4AC File Offset: 0x0006B6AC
		private static SapHanaSelectionType GetSelectionType(string selectionType)
		{
			if (selectionType == "SingleValue" || selectionType == "Single")
			{
				return SapHanaSelectionType.SingleValue;
			}
			if (selectionType == "Interval")
			{
				return SapHanaSelectionType.Interval;
			}
			if (!(selectionType == "Range"))
			{
				throw new InvalidOperationException("Unexpected value for SELECTION_TYPE: " + selectionType + ".");
			}
			return SapHanaSelectionType.Range;
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x0006D50C File Offset: 0x0006B70C
		private string ExtractParameterType(string typeName)
		{
			int num = typeName.IndexOf("(", StringComparison.Ordinal);
			if (num >= 0)
			{
				return typeName.Substring(0, num);
			}
			return typeName;
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x0006D534 File Offset: 0x0006B734
		private static string BuildParametersQuery(string queryFormat, string attributeSource)
		{
			return string.Format(CultureInfo.InvariantCulture, queryFormat, new object[]
			{
				string.Join(", ", SapHanaParameterCollection.baseParametersColumnNames.Concat(new string[] { attributeSource }).ToArray<string>()),
				"not",
				"and",
				"!="
			});
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x0006D590 File Offset: 0x0006B790
		private static string BuildVariablesQuery(string queryFormat, string attributeSource)
		{
			return string.Format(CultureInfo.InvariantCulture, queryFormat, new object[]
			{
				string.Join(", ", SapHanaParameterCollection.baseParametersColumnNames.Concat(new string[] { attributeSource, "\"MODEL_ELEMENT_NAME\"" }).ToArray<string>()),
				string.Empty,
				"or",
				"="
			});
		}

		// Token: 0x04000FB7 RID: 4023
		private static readonly string undocumentedAttributeSourceColumn = "CASE WHEN VALUE_TYPE = 'AttributeValue' AND VALUE_ENTITY is not NULL AND VALUE_ENTITY != '' THEN VALUE_ENTITY\r\nELSE (select DISTINCT COLUMNOBJECT_NAME from ( \r\nselect dvi_inner.*\r\nfrom (select * from \"_SYS_BI\".\"BIMC_PROPERTIES\" where PROPERTY_TYPE = 4 and COLUMN_FLAG = 'Dimension Attribute') as dvi_inner \r\ninner join (SELECT C.CATALOG_NAME, C.SCHEMA_NAME, C.CUBE_NAME from \"_SYS_BI\".\"BIMC_ALL_CUBES\" as C where ISAUTHORIZED(CURRENT_USER, 'SELECT', C.SCHEMA_NAME, C.VIEW_NAME, 'VIEW') = 1) as CUBES on \r\n( dvi_inner.CUBE_NAME = cubes.CUBE_NAME \r\nand dvi_inner.SCHEMA_NAME = cubes.SCHEMA_NAME \r\nand dvi_inner.CATALOG_NAME = cubes.CATALOG_NAME ) \r\nunion all \r\nselect * from \"_SYS_BI\".\"BIMC_PROPERTIES\"\r\nwhere \r\nCUBE_NAME = '$ATTRIBUTE' \r\nand PROPERTY_TYPE = 4 \r\nand COLUMN_FLAG = 'Dimension Attribute'\r\nand ISAUTHORIZED(CURRENT_USER, 'SELECT', SCHEMA_NAME, concat(concat(CATALOG_NAME, '/'), DIMENSION_NAME), 'VIEW') = 1 ) as dvi \r\nWHERE dvi.CUBE_NAME = _SYS_BI.BIMC_VARIABLE_VIEW.CUBE_NAME AND \r\ndvi.CATALOG_NAME = _SYS_BI.BIMC_VARIABLE_VIEW.CATALOG_NAME and \r\ndvi.COLUMN_NAME_2 = VALUE_ATTRIBUTE) END AS ATTRIBUTE_SOURCE";

		// Token: 0x04000FB8 RID: 4024
		private static readonly string permissionsAwareAttributeSourceColumn = "CASE WHEN VALUE_TYPE = 'AttributeValue' AND VALUE_ENTITY is not NULL AND VALUE_ENTITY != '' THEN VALUE_ENTITY\r\nELSE (select DISTINCT COLUMNOBJECT_NAME from \r\n\"_SYS_BI\".\"BIMC_DIMENSION_VIEW\"\r\nwhere CUBE_NAME = V.CUBE_NAME\r\nand CATALOG_NAME = V.CATALOG_NAME\r\nand COLUMN_NAME = VALUE_ATTRIBUTE\r\n) END AS ATTRIBUTE_SOURCE";

		// Token: 0x04000FB9 RID: 4025
		private static readonly string[] baseParametersColumnNames = new string[]
		{
			"\"VARIABLE_NAME\"", "\"COLUMN_SQL_TYPE\"", "\"PLACEHOLDER_NAME\"", "\"VALUE_TYPE\"", "\"VALUE_ATTRIBUTE\"", "\"SELECTION_TYPE\"", "\"MULTILINE\"", "\"MANDATORY\"", "\"ORDER\"", "\"DESCRIPTION\"",
			"\"VALUE_ENTITY\"", "\"DESC_ATTRIBUTE\""
		};

		// Token: 0x04000FBA RID: 4026
		private const string AttributeSource = "\"ATTRIBUTE_SOURCE\"";

		// Token: 0x04000FBB RID: 4027
		private const string ModelElementName = "\"MODEL_ELEMENT_NAME\"";

		// Token: 0x04000FBC RID: 4028
		private const string undocumentedQueryFormat = "select distinct {0} from _SYS_BI.BIMC_VARIABLE_VIEW\r\nwhere CATALOG_NAME = {{catalog}} and CUBE_NAME = {{cube}} and (PLACEHOLDER_NAME is {1} NULL {2} PLACEHOLDER_NAME {3} '')\r\norder by MANDATORY DESC, \"ORDER\" ASC";

		// Token: 0x04000FBD RID: 4029
		private const string permissionsAwareQueryFormat = "select distinct {0} from _SYS_BI.BIMC_VARIABLE_VIEW as V\r\nwhere CATALOG_NAME = {{catalog}} and CUBE_NAME = {{cube}} and (PLACEHOLDER_NAME is {1} NULL {2} PLACEHOLDER_NAME {3} '')\r\norder by MANDATORY DESC, \"ORDER\" ASC";

		// Token: 0x04000FBE RID: 4030
		private static readonly string[] parametersColumnNames = SapHanaParameterCollection.baseParametersColumnNames.Add("\"ATTRIBUTE_SOURCE\"");

		// Token: 0x04000FBF RID: 4031
		private static readonly PermissionsAwareMetadataQuery parametersQuery = new PermissionsAwareMetadataQuery(new MetadataQuery
		{
			ColumnNames = SapHanaParameterCollection.parametersColumnNames,
			Query = SapHanaParameterCollection.BuildParametersQuery("select distinct {0} from _SYS_BI.BIMC_VARIABLE_VIEW\r\nwhere CATALOG_NAME = {{catalog}} and CUBE_NAME = {{cube}} and (PLACEHOLDER_NAME is {1} NULL {2} PLACEHOLDER_NAME {3} '')\r\norder by MANDATORY DESC, \"ORDER\" ASC", SapHanaParameterCollection.undocumentedAttributeSourceColumn)
		}, new MetadataQuery
		{
			ColumnNames = SapHanaParameterCollection.parametersColumnNames,
			Query = SapHanaParameterCollection.BuildParametersQuery("select distinct {0} from _SYS_BI.BIMC_VARIABLE_VIEW as V\r\nwhere CATALOG_NAME = {{catalog}} and CUBE_NAME = {{cube}} and (PLACEHOLDER_NAME is {1} NULL {2} PLACEHOLDER_NAME {3} '')\r\norder by MANDATORY DESC, \"ORDER\" ASC", SapHanaParameterCollection.permissionsAwareAttributeSourceColumn)
		});

		// Token: 0x04000FC0 RID: 4032
		private static readonly string[] variablesColumnNames = SapHanaParameterCollection.baseParametersColumnNames.Add("\"ATTRIBUTE_SOURCE\"").Add("\"MODEL_ELEMENT_NAME\"");

		// Token: 0x04000FC1 RID: 4033
		private static readonly PermissionsAwareMetadataQuery variablesQuery = new PermissionsAwareMetadataQuery(new MetadataQuery
		{
			ColumnNames = SapHanaParameterCollection.variablesColumnNames,
			Query = SapHanaParameterCollection.BuildVariablesQuery("select distinct {0} from _SYS_BI.BIMC_VARIABLE_VIEW\r\nwhere CATALOG_NAME = {{catalog}} and CUBE_NAME = {{cube}} and (PLACEHOLDER_NAME is {1} NULL {2} PLACEHOLDER_NAME {3} '')\r\norder by MANDATORY DESC, \"ORDER\" ASC", SapHanaParameterCollection.undocumentedAttributeSourceColumn)
		}, new MetadataQuery
		{
			ColumnNames = SapHanaParameterCollection.variablesColumnNames,
			Query = SapHanaParameterCollection.BuildVariablesQuery("select distinct {0} from _SYS_BI.BIMC_VARIABLE_VIEW as V\r\nwhere CATALOG_NAME = {{catalog}} and CUBE_NAME = {{cube}} and (PLACEHOLDER_NAME is {1} NULL {2} PLACEHOLDER_NAME {3} '')\r\norder by MANDATORY DESC, \"ORDER\" ASC", SapHanaParameterCollection.permissionsAwareAttributeSourceColumn)
		});

		// Token: 0x04000FC2 RID: 4034
		internal static readonly MdxMetadataQuery DefaultValueQuery = new MdxMetadataQuery
		{
			ColumnNames = new string[] { "VARIABLE_NAME", "DEFAULT_VALUE", "RANGE_DEFAULTS_EXIST" },
			TableName = "BIMC_VARIABLE",
			WhereClause = "CATALOG_NAME = {catalog} and CUBE_NAME = {cube} AND SELECTION_TYPE NOT IN ('Interval', 'Range')"
		};

		// Token: 0x04000FC3 RID: 4035
		internal static readonly MdxMetadataQuery DefaultMultiValueQuery = new MdxMetadataQuery
		{
			ColumnNames = new string[] { "LOW" },
			TableName = "BIMC_VARIABLE_RANGE_DEFAULTS",
			WhereClause = "CATALOG_NAME = {catalog} and CUBE_NAME = {cube} and VARIABLE_NAME = {variable}",
			OrderByClause = "LINE_NUMBER"
		};

		// Token: 0x04000FC4 RID: 4036
		private readonly SapHanaOdbcDataSource dataSource;

		// Token: 0x04000FC5 RID: 4037
		private readonly SapHanaCubeBase cube;

		// Token: 0x04000FC6 RID: 4038
		private Dictionary<string, SapHanaParameter> parameters;
	}
}
