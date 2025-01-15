using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000CB RID: 203
	public sealed class CreateColumnEntityRule : EntityRuleBase, IColumnProcessingRule
	{
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x000258C0 File Offset: 0x00023AC0
		public override int ProcessOnPass
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x000258C4 File Offset: 0x00023AC4
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
			ModelEntity modelEntity = this.CreateEntity(column, entity);
			ModelAttribute modelAttribute = this.CreateAttribute(column, modelEntity);
			List<ModelItem> list = new List<ModelItem>();
			list.Add(modelEntity);
			list.Add(modelAttribute);
			list.AddRange(this.CreateRoles(column, modelEntity, entity));
			existingInfo.Entity = modelEntity;
			existingInfo.Attribute = modelAttribute;
			existingInfo.EvaluateDependentRules = true;
			return base.ProcessCreatedModelItems(list, true);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002597C File Offset: 0x00023B7C
		private ModelEntity CreateEntity(DsvColumn column, ModelEntity parentEntity)
		{
			ModelEntity modelEntity = new ModelEntity();
			modelEntity.Name = SR.CreateColumnEntityRule_ColumnEntityName(parentEntity.Name, base.CreateModelItemName(column));
			modelEntity.Binding = new ColumnBinding(column.Name, column.Table.Name);
			base.InsertItemSortedByName(modelEntity, base.Model.Entities);
			base.FinalizeModelItem(modelEntity, base.EntityFragment, base.FolderFragment);
			return modelEntity;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x000259EC File Offset: 0x00023BEC
		private ModelAttribute CreateAttribute(DsvColumn column, ModelEntity columnEntity)
		{
			ModelAttribute modelAttribute = new ModelAttribute();
			columnEntity.Fields.Add(modelAttribute);
			columnEntity.IdentifyingAttributes.Add(new AttributeReference(modelAttribute));
			modelAttribute.Name = base.CreateModelItemName(column);
			modelAttribute.Binding = new ColumnBinding(column.Name);
			modelAttribute.UpdateFromBinding();
			modelAttribute.Width = CreateAttributeRule.CalculateWidth(column, modelAttribute.Format, modelAttribute.DataCulture);
			modelAttribute.ContextualName = AttributeContextualName.Role;
			base.FinalizeModelItem(modelAttribute, null, null);
			return modelAttribute;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00025A6C File Offset: 0x00023C6C
		private ICollection<ModelItem> CreateRoles(DsvColumn column, ModelEntity columnEntity, ModelEntity parentEntity)
		{
			ModelRole modelRole = new ModelRole();
			parentEntity.Fields.Add(modelRole);
			modelRole.Name = base.CreateModelItemName(column);
			modelRole.Cardinality = Cardinality.One;
			modelRole.Optionality = Optionality.Required;
			ModelRole modelRole2 = new ModelRole();
			columnEntity.Fields.Add(modelRole2);
			modelRole2.Name = parentEntity.CollectionName;
			modelRole2.Cardinality = Cardinality.Many;
			modelRole2.Optionality = Optionality.Required;
			modelRole.RelatedRole = modelRole2;
			modelRole2.RelatedRole = modelRole;
			base.MoveFieldSortedByOrdinal(modelRole);
			base.FinalizeModelItem(modelRole, null, null);
			base.FinalizeModelItem(modelRole2, null, null);
			return new ModelRole[] { modelRole, modelRole2 };
		}
	}
}
