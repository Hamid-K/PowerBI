using System;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200009D RID: 157
	internal sealed class SourceRefColumnAdapter : QueryColumnAdapter<IIntermediateTableSchemaItem>
	{
		// Token: 0x060005D4 RID: 1492 RVA: 0x000167A7 File Offset: 0x000149A7
		internal override IConceptualColumn GetConceptualColumn(IIntermediateTableSchemaItem column)
		{
			return column.LineageProperty as IConceptualColumn;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000167B4 File Offset: 0x000149B4
		internal override string GetFormatString(IIntermediateTableSchemaItem column)
		{
			return column.FormatString;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000167BC File Offset: 0x000149BC
		internal override IIntermediateTableSchemaItem GetOrCreateColumn(IConceptualColumn newColumn, IIntermediateTableSchemaItem existingItem, DsqExpressionGenerator expressionGenerator, bool propagateRoleAndOmitFromOutput)
		{
			IIntermediateTableSchemaItem intermediateTableSchemaItem;
			if (existingItem.TryGetRelatedItem(newColumn, out intermediateTableSchemaItem))
			{
				return intermediateTableSchemaItem;
			}
			string text = "{0} should only be called for reference to an existing column's identity or sort information.  Existing column: {1}; Requested Column {2}";
			object obj = "GetOrCreateColumn";
			IConceptualProperty lineageProperty = existingItem.LineageProperty;
			object obj2 = ((lineageProperty != null) ? lineageProperty.Name.MarkAsModelInfo() : null);
			string name = newColumn.Name;
			Contract.RetailFail(text, obj, obj2, (name != null) ? name.MarkAsModelInfo() : null);
			throw new InvalidOperationException();
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00016813 File Offset: 0x00014A13
		internal override ExpressionNode ToDsqExpression(IIntermediateTableSchemaItem column)
		{
			return column.ValueCalculationId.StructureReference();
		}

		// Token: 0x04000331 RID: 817
		internal static readonly SourceRefColumnAdapter Instance = new SourceRefColumnAdapter();
	}
}
