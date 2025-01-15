using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x02000103 RID: 259
	public sealed class SetEntityPropertiesRule : EntityRuleBase, ITableProcessingRule
	{
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0002AB5A File Offset: 0x00028D5A
		public override int ProcessOnPass
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0002AB60 File Offset: 0x00028D60
		RuleProcessResult ITableProcessingRule.Process(DsvTable table, ExistingTableBindingInfo existingInfo)
		{
			ModelEntity entity = existingInfo.Entity;
			if (entity == null)
			{
				return base.ProcessSkipped(new string[] { SR.Rules_EntityDoesNotExist });
			}
			if (!existingInfo.EvaluateDependentRules)
			{
				return base.ProcessDependentRulesSkipped(entity);
			}
			if (base.EntityFragment != null)
			{
				entity.LoadFragment(base.EntityFragment);
			}
			if (base.FolderFragment != null)
			{
				base.MoveToFolder(entity, base.FolderFragment);
			}
			return base.ProcessModifiedItem(entity);
		}
	}
}
