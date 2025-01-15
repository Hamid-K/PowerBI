using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;
using Microsoft.InfoNav.Utils;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x0200000E RID: 14
	internal static class DefinitionConversionExtensions
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000022E0 File Offset: 0x000004E0
		internal static RawDataDefinition ToRawDataDefinition(this DataShapeDefinition dsd)
		{
			if (dsd.DataSets.Count != 1)
			{
				throw new RawDataException("DataShapeDefinition must have exactly 1 dataset.", ErrorSource.PowerBI);
			}
			DataSet dataSet = dsd.DataSets.Single<DataSet>();
			if (dataSet.ResultTables.Count != 1)
			{
				throw new RawDataException("DataSet must have exactly 1 ResultTable.", ErrorSource.PowerBI);
			}
			ResultTable resultTable = dataSet.ResultTables.Single<ResultTable>();
			if (dsd.DataShape.PrimaryHierarchy == null || dsd.DataShape.PrimaryHierarchy.Count != 1)
			{
				throw new RawDataException("DataShapeDefinition must have exactly 1 PrimaryHierarchy.", ErrorSource.PowerBI);
			}
			IList<Calculation> calculations = dsd.DataShape.PrimaryHierarchy.Single<DataMember>().Calculations;
			if (calculations.IsNullOrEmpty<Calculation>())
			{
				throw new RawDataException("DataShapeDefinition must have at least 1 Calculation.", ErrorSource.PowerBI);
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (Calculation calculation in calculations)
			{
				string text = DefinitionConversionExtensions.CalculationToFieldId(calculation, resultTable.Fields);
				if (dictionary.ContainsKey(text))
				{
					throw new RawDataException(StringUtil.FormatInvariant("Found duplicate calculation field {0}. DaxQuery: {1}.", new object[]
					{
						text.MarkAsCustomerContent(),
						dataSet.Query.MarkAsCustomerContent()
					}), ErrorSource.PowerBI);
				}
				dictionary.Add(text, calculation.Id);
			}
			string query = dataSet.Query;
			return new RawDataDefinition
			{
				DaxCommand = query,
				ColumnMapping = dictionary
			};
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002440 File Offset: 0x00000640
		private static string CalculationToFieldId(Calculation calculation, IEnumerable<Field> fields)
		{
			string fieldName = DefinitionConversionExtensions.ExpressionNodeToFieldName(calculation.Value);
			List<Field> list = fields.Where((Field field) => field.Id == fieldName).ToList<Field>();
			if (list.Count != 1)
			{
				throw new RawDataException("Calculation must have exactly 1 corresponding field.", ErrorSource.PowerBI);
			}
			return list.First<Field>().DataField;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000249A File Offset: 0x0000069A
		private static string ExpressionNodeToFieldName(ExpressionNode expressionNode)
		{
			FieldValueExpressionNode fieldValueExpressionNode = expressionNode as FieldValueExpressionNode;
			if (fieldValueExpressionNode == null)
			{
				throw new RawDataException("Failed to cast ExpressionNode to FieldValueExpressionNode.", ErrorSource.PowerBI);
			}
			return fieldValueExpressionNode.FieldId;
		}
	}
}
