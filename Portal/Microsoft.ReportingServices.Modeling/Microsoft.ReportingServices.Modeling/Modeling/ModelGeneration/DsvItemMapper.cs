using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000D0 RID: 208
	internal sealed class DsvItemMapper
	{
		// Token: 0x06000B8D RID: 2957 RVA: 0x00026404 File Offset: 0x00024604
		private DsvItemMapper(SemanticModel model, ICollection<ModelEntity> scopeEntities, ICollection<ModelAttribute> scopeAttrs, ExistingBindingContext bindingContext)
		{
			this.m_model = model;
			this.m_scopeEntities = scopeEntities ?? new ModelEntity[0];
			this.m_scopeAttrs = scopeAttrs ?? new ModelAttribute[0];
			this.m_bindingContext = bindingContext;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002643D File Offset: 0x0002463D
		public static void FillExistingBindingInfo(SemanticModel model, ICollection<ModelEntity> scopeEntities, ICollection<ModelAttribute> scopeAttrs, ExistingBindingContext bindingContext)
		{
			new DsvItemMapper(model, scopeEntities, scopeAttrs, bindingContext).FillAll();
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00026450 File Offset: 0x00024650
		private void FillAll()
		{
			foreach (ModelEntity modelEntity in this.m_model.GetAllEntities())
			{
				this.Fill(modelEntity);
			}
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x000264A4 File Offset: 0x000246A4
		private void Fill(ModelEntity entity)
		{
			ExistingBindingInfo existingBindingInfo = null;
			if (entity.Binding is TableBinding)
			{
				DsvTable table = ((TableBinding)entity.Binding).GetTable();
				if (table != null)
				{
					ExistingTableBindingInfo bindingInfo = this.m_bindingContext.GetBindingInfo(table);
					bindingInfo.Entity = entity;
					existingBindingInfo = bindingInfo;
				}
			}
			else if (entity.Binding is ColumnBinding)
			{
				DsvColumn column = ((ColumnBinding)entity.Binding).GetColumn();
				if (column != null)
				{
					ExistingColumnBindingInfo bindingInfo2 = this.m_bindingContext.GetBindingInfo(column);
					bindingInfo2.Entity = entity;
					existingBindingInfo = bindingInfo2;
				}
			}
			else if (entity.Binding != null)
			{
				string text = "Unrecognized Entity binding: ";
				Binding binding = entity.Binding;
				throw new InternalModelingException(text + ((binding != null) ? binding.ToString() : null));
			}
			if (existingBindingInfo != null)
			{
				existingBindingInfo.EvaluateDependentRules = entity.Fields.Count == 0;
			}
			foreach (ModelField modelField in entity.GetAllFields())
			{
				if (modelField is ModelAttribute)
				{
					this.Fill((ModelAttribute)modelField, existingBindingInfo);
				}
				else
				{
					if (!(modelField is ModelRole))
					{
						string text2 = "Unknown ModelField: ";
						ModelField modelField2 = modelField;
						throw new InternalModelingException(text2 + ((modelField2 != null) ? modelField2.ToString() : null));
					}
					this.Fill((ModelRole)modelField);
				}
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x000265EC File Offset: 0x000247EC
		private void Fill(ModelAttribute attr, ExistingBindingInfo entityBindingInfo)
		{
			if (attr.Binding != null)
			{
				DsvColumn column = attr.Binding.GetColumn();
				if (column != null)
				{
					ExistingColumnBindingInfo bindingInfo = this.m_bindingContext.GetBindingInfo(column);
					bindingInfo.Attribute = attr;
					bindingInfo.EvaluateDependentRules = false;
				}
			}
			ExistingTableBindingInfo existingTableBindingInfo = entityBindingInfo as ExistingTableBindingInfo;
			if (existingTableBindingInfo != null && attr.Expression != null && attr.Expression.Path.IsEmpty && attr.Expression.NodeAsFunction != null && attr.Expression.NodeAsFunction.FunctionName == FunctionName.Count && attr.Expression.NodeAsFunction.Arguments.Count == 1 && attr.Expression.NodeAsFunction.Arguments[0].Path.IsEmpty && attr.Expression.NodeAsFunction.Arguments[0].NodeAsEntityRef != null && attr.Expression.NodeAsFunction.Arguments[0].NodeAsEntityRef.Entity == attr.Entity)
			{
				existingTableBindingInfo.CountAttribute = attr;
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00026704 File Offset: 0x00024904
		private void Fill(ModelRole role)
		{
			if (role.Binding != null)
			{
				DsvRelation relation = role.Binding.GetRelation();
				if (relation != null)
				{
					ExistingRelationBindingInfo bindingInfo = this.m_bindingContext.GetBindingInfo(relation);
					if (role.Binding.RelationEnd == RelationEnd.Source)
					{
						bindingInfo.SourceRole = role;
						return;
					}
					bindingInfo.TargetRole = role;
				}
			}
		}

		// Token: 0x040004B6 RID: 1206
		private readonly SemanticModel m_model;

		// Token: 0x040004B7 RID: 1207
		private readonly ICollection<ModelEntity> m_scopeEntities;

		// Token: 0x040004B8 RID: 1208
		private readonly ICollection<ModelAttribute> m_scopeAttrs;

		// Token: 0x040004B9 RID: 1209
		private readonly ExistingBindingContext m_bindingContext;
	}
}
