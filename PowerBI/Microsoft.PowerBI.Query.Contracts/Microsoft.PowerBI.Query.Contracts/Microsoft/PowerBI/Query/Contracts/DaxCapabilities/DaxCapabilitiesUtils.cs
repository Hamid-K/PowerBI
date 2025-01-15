using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Microsoft.PowerBI.Query.Contracts.DaxCapabilities
{
	// Token: 0x0200000B RID: 11
	public sealed class DaxCapabilitiesUtils
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000022EC File Offset: 0x000004EC
		public static IList<DaxFunction> ParseDaxFunctions(DataSet functionData)
		{
			DataRelation dataRelation = functionData.Relations[0];
			DataTable dataTable = functionData.Tables[0];
			List<DaxFunction> list = new List<DaxFunction>(dataTable.Rows.Count);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				list.Add(DaxCapabilitiesUtils.GetFunctionFromDataRow(dataRow, dataRelation));
			}
			return list;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002378 File Offset: 0x00000578
		public static Dictionary<string, object> GetFunctionSchemaRestrictions(int functionsOrigin, string catalogName = null)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["ORIGIN"] = functionsOrigin;
			if (!string.IsNullOrEmpty(catalogName))
			{
				dictionary["CATALOG_NAME"] = catalogName;
			}
			return dictionary;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000023B1 File Offset: 0x000005B1
		public static bool SupportsVariations(IList<DaxFunction> functions)
		{
			return functions.FirstOrDefault((DaxFunction func) => func.Name == "CONTAINSROW") != null;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000023DC File Offset: 0x000005DC
		private static DaxFunction GetFunctionFromDataRow(DataRow functionRow, DataRelation parameterInfoRelation)
		{
			PushableToDirectQuery pushableToDirectQuery = DaxCapabilitiesUtils.GetPushableToDirectQuery(functionRow);
			IList<DaxFunctionParameter> parametersFromDataRow = DaxCapabilitiesUtils.GetParametersFromDataRow(functionRow, parameterInfoRelation);
			return new DaxFunction
			{
				Name = (string)functionRow["FUNCTION_NAME"],
				Description = (string)functionRow["DESCRIPTION"],
				Category = DaxCapabilitiesUtils.ConvertFunctionCategory(functionRow["INTERFACE_NAME"] as string),
				PushableToDirectQuery = pushableToDirectQuery,
				Parameters = parametersFromDataRow
			};
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002454 File Offset: 0x00000654
		private static IList<DaxFunctionParameter> GetParametersFromDataRow(DataRow functionRow, DataRelation parameterInfoRelation)
		{
			if (parameterInfoRelation == null)
			{
				return null;
			}
			DataRow[] childRows = functionRow.GetChildRows(parameterInfoRelation);
			List<DaxFunctionParameter> list = new List<DaxFunctionParameter>(childRows.Length);
			foreach (DataRow dataRow in childRows)
			{
				list.Add(new DaxFunctionParameter
				{
					Name = (string)dataRow["NAME"],
					Description = (string)dataRow["DESCRIPTION"],
					Optional = (bool)dataRow["OPTIONAL"],
					Repeatable = (bool)dataRow["REPEATABLE"],
					RepeatGroup = ((dataRow["REPEATGROUP"] is DBNull) ? 0 : ((int)dataRow["REPEATGROUP"]))
				});
			}
			return list;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002520 File Offset: 0x00000720
		private static PushableToDirectQuery GetPushableToDirectQuery(DataRow functionRow)
		{
			if (functionRow.Table.Columns.IndexOf("DIRECTQUERY_PUSHABLE") == -1 || functionRow["DIRECTQUERY_PUSHABLE"] is DBNull)
			{
				return PushableToDirectQuery.ForEverything;
			}
			int num = (int)functionRow["DIRECTQUERY_PUSHABLE"];
			if (!Enum.IsDefined(typeof(PushableToDirectQuery), num))
			{
				throw new InvalidDataException(Utilities.FormatInvariant("The value {0} is not a valid PushableToDirectQuery enum", new object[] { num }));
			}
			return (PushableToDirectQuery)num;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000025A4 File Offset: 0x000007A4
		private static FunctionCategory ConvertFunctionCategory(string engineCategory)
		{
			FunctionCategory functionCategory;
			if (!string.IsNullOrEmpty(engineCategory) && DaxCapabilitiesUtils.FunctionCategories.TryGetValue(engineCategory, out functionCategory))
			{
				return functionCategory;
			}
			return FunctionCategory.None;
		}

		// Token: 0x04000036 RID: 54
		public const string FunctionsRelationParameterInfo = "rowsetTablePARAMETERINFO";

		// Token: 0x04000037 RID: 55
		public const string FunctionsSchema = "MDSCHEMA_FUNCTIONS";

		// Token: 0x04000038 RID: 56
		public const string FunctionsRestrictionOrigin = "ORIGIN";

		// Token: 0x04000039 RID: 57
		public const string FunctionsRestrictionCatalogName = "CATALOG_NAME";

		// Token: 0x0400003A RID: 58
		public const int FunctionsOriginRelational = 3;

		// Token: 0x0400003B RID: 59
		public const int FunctionsOriginScalar = 4;

		// Token: 0x0400003C RID: 60
		public const string FunctionsFieldName = "FUNCTION_NAME";

		// Token: 0x0400003D RID: 61
		public const string FunctionsFieldDescription = "DESCRIPTION";

		// Token: 0x0400003E RID: 62
		public const string FunctionsFieldCategory = "INTERFACE_NAME";

		// Token: 0x0400003F RID: 63
		public const string FunctionsFieldLibrary = "LIBRARY_NAME";

		// Token: 0x04000040 RID: 64
		public const string FunctionsFieldDirectQueryPushable = "DIRECTQUERY_PUSHABLE";

		// Token: 0x04000041 RID: 65
		public const string ParametersFieldName = "NAME";

		// Token: 0x04000042 RID: 66
		public const string ParametersFieldInfo = "PARAMETERINFO";

		// Token: 0x04000043 RID: 67
		public const string ParametersFieldDescription = "DESCRIPTION";

		// Token: 0x04000044 RID: 68
		public const string ParametersFieldOptional = "OPTIONAL";

		// Token: 0x04000045 RID: 69
		public const string ParametersFieldRepeatable = "REPEATABLE";

		// Token: 0x04000046 RID: 70
		public const string ParametersFieldRepeatGroup = "REPEATGROUP";

		// Token: 0x04000047 RID: 71
		public const string ProxyFunctionForSupportsVariation = "CONTAINSROW";

		// Token: 0x04000048 RID: 72
		public const string InOperator = "IN";

		// Token: 0x04000049 RID: 73
		public const string ReturnOperator = "RETURN";

		// Token: 0x0400004A RID: 74
		public const string VarOperator = "VAR";

		// Token: 0x0400004B RID: 75
		public const string NotOperator = "NOT";

		// Token: 0x0400004C RID: 76
		public static readonly IReadOnlyDictionary<string, FunctionCategory> FunctionCategories = new Dictionary<string, FunctionCategory>
		{
			{
				"DATETIME",
				FunctionCategory.DateAndTime
			},
			{
				"DEPRECATED",
				FunctionCategory.Deprecated
			},
			{
				"INFO",
				FunctionCategory.Information
			},
			{
				"LOGICAL",
				FunctionCategory.Logical
			},
			{
				"MATHTRIG",
				FunctionCategory.MathAndTrig
			},
			{
				"STATISTICAL",
				FunctionCategory.Statistical
			},
			{
				"TEXT",
				FunctionCategory.Text
			},
			{
				"FILTER",
				FunctionCategory.Filter
			},
			{
				"PARENTCHILD",
				FunctionCategory.ParentChild
			}
		};
	}
}
