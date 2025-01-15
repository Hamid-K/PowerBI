using System;
using System.Xml.XPath;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000CE RID: 206
	public sealed class CreateRoleRule : ProcessingRule, IRelationProcessingRule
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x00025DB3 File Offset: 0x00023FB3
		public override int ProcessOnPass
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x00025DB6 File Offset: 0x00023FB6
		// (set) Token: 0x06000B75 RID: 2933 RVA: 0x00025DBE File Offset: 0x00023FBE
		public IXPathNavigable SourceRoleFragment
		{
			get
			{
				return this.m_sourceRoleFragment;
			}
			set
			{
				this.m_sourceRoleFragment = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00025DC7 File Offset: 0x00023FC7
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x00025DCF File Offset: 0x00023FCF
		public IXPathNavigable TargetRoleFragment
		{
			get
			{
				return this.m_targetRoleFragment;
			}
			set
			{
				this.m_targetRoleFragment = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x00025DD8 File Offset: 0x00023FD8
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x00025DE0 File Offset: 0x00023FE0
		public IXPathNavigable SourceFolderFragment
		{
			get
			{
				return this.m_sourceFolderFragment;
			}
			set
			{
				this.m_sourceFolderFragment = value;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00025DE9 File Offset: 0x00023FE9
		// (set) Token: 0x06000B7B RID: 2939 RVA: 0x00025DF1 File Offset: 0x00023FF1
		public IXPathNavigable TargetFolderFragment
		{
			get
			{
				return this.m_targetFolderFragment;
			}
			set
			{
				this.m_targetFolderFragment = value;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00025DFA File Offset: 0x00023FFA
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x00025E02 File Offset: 0x00024002
		public bool UseColumnNames
		{
			get
			{
				return this.m_useColumnNames;
			}
			set
			{
				this.m_useColumnNames = value;
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00025E0C File Offset: 0x0002400C
		RuleProcessResult IRelationProcessingRule.Process(DsvRelation relation, ExistingRelationBindingInfo existingInfo)
		{
			ModelEntity entity = base.BindingContext.GetBindingInfo(relation.SourceTable).Entity;
			ModelEntity entity2 = base.BindingContext.GetBindingInfo(relation.TargetTable).Entity;
			if (entity == null || entity2 == null)
			{
				return base.ProcessSkipped(new string[] { SR.CreateRoleRule_SourceOrTargetEntityDoesNotExist });
			}
			if (existingInfo.SourceRole != null)
			{
				return base.ProcessFoundExistingModelItem(existingInfo.SourceRole);
			}
			if (existingInfo.TargetRole != null)
			{
				return base.ProcessFoundExistingModelItem(existingInfo.TargetRole);
			}
			if (existingInfo.InheritanceEntity != null)
			{
				return base.ProcessFoundExistingModelItem(existingInfo.InheritanceEntity);
			}
			ModelRole modelRole = this.CreateRole(relation, entity2, entity, this.m_sourceRoleFragment, this.m_sourceFolderFragment, false);
			ModelRole modelRole2 = this.CreateRole(relation, entity, entity2, this.m_targetRoleFragment, this.m_targetFolderFragment, true);
			modelRole.RelatedRole = modelRole2;
			modelRole2.RelatedRole = modelRole;
			existingInfo.SourceRole = modelRole;
			existingInfo.TargetRole = modelRole2;
			existingInfo.EvaluateDependentRules = true;
			return base.ProcessCreatedModelItems(new ModelRole[] { modelRole, modelRole2 });
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00025F08 File Offset: 0x00024108
		private ModelRole CreateRole(DsvRelation relation, ModelEntity fromEntity, ModelEntity toEntity, IXPathNavigable roleFragment, IXPathNavigable folderFragment, bool target)
		{
			ModelRole modelRole = new ModelRole();
			fromEntity.Fields.Add(modelRole);
			bool flag = fromEntity == toEntity;
			if (this.m_useColumnNames || flag)
			{
				string targetName = this.GetTargetName(relation, flag);
				if (targetName != null)
				{
					if (target && targetName != toEntity.Name)
					{
						modelRole.Name = targetName;
					}
					else if (!target)
					{
						ModelRole modelRole2 = null;
						foreach (ModelField modelField in fromEntity.GetAllFields())
						{
							ModelRole modelRole3 = modelField as ModelRole;
							if (modelRole3 != null && modelRole3.RelatedEntity == toEntity)
							{
								modelRole2 = modelRole3;
							}
						}
						if (modelRole2 != null)
						{
							CreateRoleRule.SetSourceRoleNameWithEntity(relation, modelRole, toEntity, targetName);
							if (modelRole2.UseAutoName)
							{
								DsvRelation dsvRelation = ((modelRole2.Binding != null) ? modelRole2.Binding.GetRelation() : null);
								string targetName2 = this.GetTargetName(dsvRelation, false);
								CreateRoleRule.SetSourceRoleNameWithEntity(dsvRelation, modelRole2, toEntity, targetName2);
							}
						}
					}
				}
			}
			modelRole.Cardinality = ((target || relation.OneToOne) ? Cardinality.One : Cardinality.Many);
			if (target)
			{
				modelRole.Optionality = (relation.OptionalTarget ? Optionality.Optional : Optionality.Required);
			}
			else
			{
				modelRole.Optionality = (relation.OptionalSource ? Optionality.Optional : Optionality.Required);
			}
			modelRole.Binding = new RelationBinding(relation.Name, target ? RelationEnd.Target : RelationEnd.Source);
			if (target)
			{
				base.MoveFieldSortedByOrdinal(modelRole);
			}
			base.FinalizeModelItem(modelRole, roleFragment, folderFragment);
			return modelRole;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00026078 File Offset: 0x00024278
		private string GetTargetName(DsvRelation relation, bool selfRelation)
		{
			string text = null;
			if (relation.SourceColumns.Count == 1)
			{
				text = relation.SourceColumns[0].Name;
			}
			else
			{
				for (int i = 0; i < relation.SourceColumns.Count; i++)
				{
					if (relation.SourceColumns[i].Name != relation.TargetColumns[i].Name)
					{
						text = relation.SourceColumns[i].Name;
						break;
					}
				}
				if (text == null && selfRelation)
				{
					throw new InvalidOperationException(SR.CreateRoleRule_SelfRelationWithMatchingColumns);
				}
			}
			if (text != null)
			{
				text = base.CreateModelItemName(text);
			}
			return text;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002611C File Offset: 0x0002431C
		private static void SetSourceRoleNameWithEntity(DsvRelation relation, ModelRole role, ModelEntity toEntity, string targetName)
		{
			string text = SR.CreateRoleRule_RoleNameWithEntity(toEntity.Name, targetName);
			string text2 = SR.CreateRoleRule_RoleNameWithEntity(toEntity.CollectionName, targetName);
			if (relation.OneToOne)
			{
				role.Name = text;
			}
			else
			{
				role.Name = text2;
			}
			role.Linguistics.SingularName = text;
			role.Linguistics.PluralName = text2;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00026173 File Offset: 0x00024373
		internal override bool LoadXmlAttribute(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "useColumnNames")
			{
				this.m_useColumnNames = xr.ReadValueAsBoolean();
				return true;
			}
			return base.LoadXmlAttribute(xr, objectFactory);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x000261A8 File Offset: 0x000243A8
		internal override bool LoadXmlElement(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "source")
				{
					xr.LoadObject(new CreateRoleRule.RoleFragmentLoader(this, RelationEnd.Source));
					return true;
				}
				if (localName == "target")
				{
					xr.LoadObject(new CreateRoleRule.RoleFragmentLoader(this, RelationEnd.Target));
					return true;
				}
			}
			return base.LoadXmlElement(xr, objectFactory);
		}

		// Token: 0x040004A9 RID: 1193
		private const string SourceElem = "source";

		// Token: 0x040004AA RID: 1194
		private const string TargetElem = "target";

		// Token: 0x040004AB RID: 1195
		private const string UseColumnNamesAttr = "useColumnNames";

		// Token: 0x040004AC RID: 1196
		private IXPathNavigable m_sourceRoleFragment;

		// Token: 0x040004AD RID: 1197
		private IXPathNavigable m_targetRoleFragment;

		// Token: 0x040004AE RID: 1198
		private IXPathNavigable m_sourceFolderFragment;

		// Token: 0x040004AF RID: 1199
		private IXPathNavigable m_targetFolderFragment;

		// Token: 0x040004B0 RID: 1200
		private bool m_useColumnNames = true;

		// Token: 0x020001C1 RID: 449
		private class RoleFragmentLoader : ModelingXmlLoaderBase<CreateRoleRule>
		{
			// Token: 0x06001155 RID: 4437 RVA: 0x0003643F File Offset: 0x0003463F
			internal RoleFragmentLoader(CreateRoleRule item, RelationEnd end)
				: base(item)
			{
				this.m_end = end;
			}

			// Token: 0x06001156 RID: 4438 RVA: 0x00036450 File Offset: 0x00034650
			public override bool LoadXmlElement(ModelingXmlReader xr)
			{
				if (xr.NamespaceURI == "http://schemas.microsoft.com/sqlserver/2004/10/semanticmodeling")
				{
					string localName = xr.LocalName;
					if (localName == "Role")
					{
						if (this.m_end == RelationEnd.Source)
						{
							base.Item.SourceRoleFragment = xr.ReadFragment();
						}
						else
						{
							base.Item.TargetRoleFragment = xr.ReadFragment();
						}
						return true;
					}
					if (localName == "FieldFolder")
					{
						if (this.m_end == RelationEnd.Source)
						{
							base.Item.SourceFolderFragment = xr.ReadFragment();
						}
						else
						{
							base.Item.TargetFolderFragment = xr.ReadFragment();
						}
						return true;
					}
				}
				return base.LoadXmlElement(xr);
			}

			// Token: 0x040007CF RID: 1999
			private readonly RelationEnd m_end;
		}
	}
}
