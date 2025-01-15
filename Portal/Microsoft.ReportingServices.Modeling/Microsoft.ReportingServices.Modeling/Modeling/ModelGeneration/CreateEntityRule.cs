using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000CD RID: 205
	public sealed class CreateEntityRule : EntityRuleBase, ITableProcessingRule
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x00025C33 File Offset: 0x00023E33
		public override int ProcessOnPass
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00025C38 File Offset: 0x00023E38
		RuleProcessResult ITableProcessingRule.Process(DsvTable table, ExistingTableBindingInfo existingInfo)
		{
			if (table.DataSourceID != null && table.DataSourceID != base.Model.DataSourceView.DataSourceID)
			{
				return base.ProcessFailed(new string[] { SR.CreateEntityRule_NonPrimaryDataSource });
			}
			if (table.PrimaryKey.Count == 0)
			{
				return base.ProcessFailed(new string[] { SR.CreateEntityRule_NoPrimaryKey });
			}
			foreach (DsvColumn dsvColumn in table.PrimaryKey)
			{
				if (dsvColumn.ModelingDataType == null)
				{
					return base.ProcessFailed(new string[] { SR.Rules_UnsupportedDataType(dsvColumn.DataType) });
				}
			}
			if (existingInfo.Entity != null)
			{
				return base.ProcessFoundExistingModelItem(existingInfo.Entity);
			}
			ModelEntity modelEntity = new ModelEntity();
			modelEntity.Name = StringManipulation.GetSingular(base.CreateModelItemName(table), base.StringCulture);
			modelEntity.CollectionName = StringManipulation.GetPlural(modelEntity.Name, base.StringCulture);
			modelEntity.Binding = new TableBinding(table.Name);
			base.InsertItemSortedByName(modelEntity, base.Model.Entities);
			base.FinalizeModelItem(modelEntity, base.EntityFragment, base.FolderFragment);
			existingInfo.Entity = modelEntity;
			existingInfo.EvaluateDependentRules = true;
			return base.ProcessCreatedModelItem(modelEntity);
		}
	}
}
