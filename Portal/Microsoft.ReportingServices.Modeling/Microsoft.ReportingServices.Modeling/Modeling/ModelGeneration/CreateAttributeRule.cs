using System;
using System.Globalization;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000C9 RID: 201
	public sealed class CreateAttributeRule : AttributeRuleBase, IColumnProcessingRule
	{
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x000253E2 File Offset: 0x000235E2
		public override int ProcessOnPass
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x000253E8 File Offset: 0x000235E8
		RuleProcessResult IColumnProcessingRule.Process(DsvColumn column, ExistingColumnBindingInfo existingInfo)
		{
			ModelEntity entity = base.BindingContext.GetBindingInfo(column.Table).Entity;
			if (entity == null)
			{
				return base.ProcessSkipped(new string[] { SR.Rules_ParentEntityDoesNotExist });
			}
			if (existingInfo.Entity != null)
			{
				return base.ProcessFoundExistingModelItem(existingInfo.Entity);
			}
			if (existingInfo.Attribute != null)
			{
				return base.ProcessFoundExistingModelItem(existingInfo.Attribute);
			}
			if (column.ModelingDataType == null)
			{
				return base.ProcessSkipped(new string[] { SR.Rules_UnsupportedDataType(column.DataType) });
			}
			ModelAttribute modelAttribute = new ModelAttribute();
			entity.Fields.Add(modelAttribute);
			modelAttribute.Name = base.CreateModelItemName(column);
			modelAttribute.Binding = new ColumnBinding(column.Name);
			modelAttribute.UpdateFromBinding();
			base.MoveFieldSortedByOrdinal(modelAttribute);
			base.FinalizeModelItem(modelAttribute, base.AttributeFragment, base.FolderFragment);
			if (!modelAttribute.IsWidthSet)
			{
				modelAttribute.Width = CreateAttributeRule.CalculateWidth(column, modelAttribute.Format, modelAttribute.DataCulture);
			}
			existingInfo.Attribute = modelAttribute;
			existingInfo.EvaluateDependentRules = true;
			return base.ProcessCreatedModelItem(modelAttribute);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00025500 File Offset: 0x00023700
		internal static int CalculateWidth(DsvColumn column, string format, CultureInfo culture)
		{
			if (column.AvgWidth == null || column.StDevWidth == null || column.MaxWidth == null)
			{
				return 0;
			}
			int num = (int)Math.Ceiling((double)Math.Min(column.AvgWidth.Value + 2f * column.StDevWidth.Value, (float)column.MaxWidth.Value));
			if (DataTypeMapper.IsNumeric(column.DataType))
			{
				num++;
				if (!string.IsNullOrEmpty(format))
				{
					num = FormatUtil.EstimateWidthChars(format, num, culture);
				}
				else if (column.AvgScale != null && column.StDevScale != null && column.MaxScale != null)
				{
					num++;
					num += (int)Math.Min((double)Math.Min(4f, (float)column.MaxScale.Value), Math.Ceiling((double)(column.AvgScale.Value + 2f * column.StDevScale.Value)));
				}
			}
			return num;
		}
	}
}
