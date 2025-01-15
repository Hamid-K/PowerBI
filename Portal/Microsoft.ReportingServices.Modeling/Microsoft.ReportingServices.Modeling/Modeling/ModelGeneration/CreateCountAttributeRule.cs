using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000CC RID: 204
	public sealed class CreateCountAttributeRule : AttributeRuleBase, ITableProcessingRule
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00025B0F File Offset: 0x00023D0F
		public override int ProcessOnPass
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00025B14 File Offset: 0x00023D14
		RuleProcessResult ITableProcessingRule.Process(DsvTable table, ExistingTableBindingInfo existingInfo)
		{
			if (existingInfo.Entity == null)
			{
				return base.ProcessSkipped(new string[] { SR.Rules_ParentEntityDoesNotExist });
			}
			if (!existingInfo.EvaluateDependentRules)
			{
				return base.ProcessDependentRulesSkipped(existingInfo.Entity);
			}
			if (existingInfo.CountAttribute != null)
			{
				return base.ProcessFoundExistingModelItem(existingInfo.CountAttribute);
			}
			ModelEntity entity = existingInfo.Entity;
			ModelAttribute modelAttribute = this.CreateCountAttribute(entity);
			bool flag = false;
			if (entity.DefaultAggregateAttributes.Count == 0)
			{
				entity.DefaultAggregateAttributes.Add(new AttributeReference(modelAttribute));
				flag = true;
			}
			existingInfo.CountAttribute = modelAttribute;
			return base.ProcessCreatedModelItem(modelAttribute, flag ? entity : null);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00025BB0 File Offset: 0x00023DB0
		private ModelAttribute CreateCountAttribute(ModelEntity entity)
		{
			ModelAttribute modelAttribute = new ModelAttribute();
			entity.Fields.Insert(0, modelAttribute);
			modelAttribute.Name = base.ApplyRenamers(entity.CollectionName);
			Expression expression = new Expression(new EntityRefNode(entity));
			modelAttribute.Expression = new Expression(new FunctionNode(FunctionName.Count, new Expression[] { expression }));
			modelAttribute.IsAggregate = true;
			modelAttribute.UpdateFromExpression();
			base.FinalizeModelItem(modelAttribute, base.AttributeFragment, base.FolderFragment);
			return modelAttribute;
		}
	}
}
