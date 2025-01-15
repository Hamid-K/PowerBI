using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000358 RID: 856
	internal static class ModelUtil
	{
		// Token: 0x06001C3D RID: 7229 RVA: 0x00072300 File Offset: 0x00070500
		public static ModelAttribute GetModelAttribute(Expression expr)
		{
			ExpressionPath expressionPath;
			return ModelUtil.GetModelAttribute(expr, out expressionPath);
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x00072318 File Offset: 0x00070518
		public static ModelAttribute GetModelAttribute(Expression expr, out ExpressionPath pathToAttrib)
		{
			pathToAttrib = null;
			for (int i = 0; i < 100; i++)
			{
				if (!expr.Path.IsEmpty)
				{
					if (pathToAttrib == null)
					{
						pathToAttrib = new ExpressionPath();
					}
					pathToAttrib.AddRange(expr.Path);
				}
				FunctionNode nodeAsFunction = expr.NodeAsFunction;
				if (nodeAsFunction == null)
				{
					break;
				}
				switch (nodeAsFunction.FunctionName)
				{
				case FunctionName.Filter:
					expr = nodeAsFunction.Arguments[0];
					break;
				case FunctionName.Evaluate:
					expr = nodeAsFunction.Arguments[0];
					break;
				case FunctionName.Aggregate:
					expr = nodeAsFunction.Arguments[0];
					break;
				default:
					goto IL_008E;
				}
			}
			IL_008E:
			AttributeRefNode nodeAsAttributeRef = expr.NodeAsAttributeRef;
			if (nodeAsAttributeRef != null)
			{
				return nodeAsAttributeRef.ModelAttribute;
			}
			return null;
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x000723C8 File Offset: 0x000705C8
		public static bool IsLookupRole(ModelRole role)
		{
			return !role.IsInvalidRefTarget && (role.Cardinality == Cardinality.One && role.RelatedEntity.IsLookup && role.RelatedEntity.IdentifyingAttributes.Count == 1) && role.RelatedEntity.IdentifyingAttributes[0].Path.IsEmpty;
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x00072424 File Offset: 0x00070624
		public static List<ExpressionPath> GetPromotedLookupPaths(ModelRole lookupRole)
		{
			if (lookupRole == null)
			{
				throw new ArgumentNullException();
			}
			if (!ModelUtil.IsLookupRole(lookupRole))
			{
				throw new ArgumentException();
			}
			List<ExpressionPath> list = new List<ExpressionPath>();
			ModelUtil.GetPromotedLookupsInternal(new ExpressionPath(new PathItem[]
			{
				new RolePathItem(lookupRole)
			}), list);
			return list;
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x0007246C File Offset: 0x0007066C
		private static void GetPromotedLookupsInternal(ExpressionPath currentPath, List<ExpressionPath> lookupPaths)
		{
			foreach (ModelFieldFolderItem modelFieldFolderItem in currentPath.LastItem.TargetEntity.ModelEntity.Fields)
			{
				ModelRole modelRole = modelFieldFolderItem as ModelRole;
				if (modelRole != null && modelRole.PromoteLookup && ModelUtil.IsLookupRole(modelRole) && !ModelUtil.BeenThereDoneThat(currentPath, modelRole.RelatedEntity))
				{
					ExpressionPath expressionPath = new ExpressionPath(currentPath, new PathItem[]
					{
						new RolePathItem(modelRole)
					});
					lookupPaths.Add(expressionPath);
					ModelUtil.GetPromotedLookupsInternal(expressionPath, lookupPaths);
				}
			}
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x00072514 File Offset: 0x00070714
		public static ModelRole PopLastRole(ExpressionPath path)
		{
			if (path.IsEmpty)
			{
				throw new ArgumentException();
			}
			ModelRole role = ((RolePathItem)path.LastItem).Role;
			path.RemoveAt(path.Length - 1);
			return role;
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x00072542 File Offset: 0x00070742
		public static ModelRole GetLastRole(ExpressionPath pathToItem)
		{
			if (pathToItem != null && !pathToItem.IsEmpty)
			{
				return ((RolePathItem)pathToItem.LastItem).Role;
			}
			return null;
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x00072564 File Offset: 0x00070764
		public static bool IsHiddenBasedOnContext(ExpressionPath pathToItem, ModelItem item)
		{
			ModelFieldFolderItem modelFieldFolderItem = item as ModelFieldFolderItem;
			ModelRole lastRole = ModelUtil.GetLastRole(pathToItem);
			return modelFieldFolderItem != null && lastRole != null && lastRole.HiddenFields.Contains(modelFieldFolderItem);
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x00072598 File Offset: 0x00070798
		public static string GetContextualName(ExpressionPath pathToItem, ModelFieldFolderItem item, string mergeFormat)
		{
			ModelAttribute modelAttribute = item as ModelAttribute;
			ModelRole modelRole = item as ModelRole;
			string text = item.Name;
			pathToItem = pathToItem ?? ExpressionPath.Empty;
			if (modelRole != null)
			{
				if (modelRole.Cardinality == Cardinality.Many || pathToItem.GetCardinality() == Cardinality.Many)
				{
					text = modelRole.Linguistics.PluralName;
				}
				else
				{
					text = modelRole.Linguistics.SingularName;
				}
			}
			ModelRole lastRole = ModelUtil.GetLastRole(pathToItem);
			if (lastRole != null)
			{
				bool flag = false;
				if (lastRole.ContextualName == RoleContextualName.Role || (modelAttribute != null && modelAttribute.ContextualName == AttributeContextualName.Role))
				{
					if (modelAttribute != null && lastRole.RelatedEntity.IdentifyingAttributes.Count == 1 && lastRole.RelatedEntity.IdentifyingAttributes[0].Attribute == modelAttribute)
					{
						text = lastRole.Name;
						if (ModelUtil.IsLookupRole(lastRole))
						{
							int i = pathToItem.Length - 1;
							while (i > 0)
							{
								ModelRole role = ((RolePathItem)pathToItem[i]).Role;
								ModelRole role2 = ((RolePathItem)pathToItem[i - 1]).Role;
								if (!role.PromoteLookup || !ModelUtil.IsLookupRole(role2))
								{
									if (role2.ContextualName == RoleContextualName.Role || role2.ContextualName == RoleContextualName.Merge)
									{
										text = StringUtil.FormatInvariant(mergeFormat, new object[] { role2.Name, text });
										break;
									}
									break;
								}
								else
								{
									i--;
								}
							}
						}
					}
					else
					{
						flag = true;
					}
				}
				if (flag || lastRole.ContextualName == RoleContextualName.Merge || (modelAttribute != null && modelAttribute.ContextualName == AttributeContextualName.Merge))
				{
					text = StringUtil.FormatInvariant(mergeFormat, new object[] { lastRole.Name, text });
				}
			}
			return text;
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x00072724 File Offset: 0x00070924
		public static bool IsRelatedByInheritance(ModelEntity e1, ModelEntity e2, bool includeIndirect)
		{
			if (e1 == e2)
			{
				return true;
			}
			if (includeIndirect)
			{
				return !new Expression(new EntityRefNode(e2)).Validate(e1, ExpressionCompilationFlags.None, false).HasErrors;
			}
			List<ModelEntity> directAncestors = ModelUtil.GetDirectAncestors(e1, false);
			List<ModelEntity> directAncestors2 = ModelUtil.GetDirectAncestors(e2, false);
			return directAncestors.Contains(e2) || directAncestors2.Contains(e1);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x0007277C File Offset: 0x0007097C
		public static List<ModelEntity> GetRelatedByInheritance(ModelEntity entity)
		{
			List<ModelEntity> list = new List<ModelEntity>();
			if (!entity.IsInvalidRefTarget)
			{
				foreach (ModelEntity modelEntity in entity.Model.GetAllEntities())
				{
					if (modelEntity != entity && ModelUtil.IsRelatedByInheritance(entity, modelEntity, true))
					{
						list.Add(modelEntity);
					}
				}
			}
			return list;
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x000727EC File Offset: 0x000709EC
		public static List<ModelEntity> GetDescendents(ModelEntity entity, bool recursive)
		{
			List<ModelEntity> list = new List<ModelEntity>();
			if (!entity.IsInvalidRefTarget)
			{
				foreach (ModelEntity modelEntity in entity.Model.GetAllEntities())
				{
					if (modelEntity.Inheritance != null && modelEntity.Inheritance.InheritsFrom == entity)
					{
						list.Add(modelEntity);
						if (recursive)
						{
							list.AddRange(ModelUtil.GetDescendents(modelEntity, true));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x00072874 File Offset: 0x00070A74
		public static List<ModelEntity> GetDirectAncestors(ModelEntity entity, bool includeSelf)
		{
			List<ModelEntity> list = new List<ModelEntity>();
			if (includeSelf)
			{
				list.Add(entity);
			}
			while (entity.Inheritance != null)
			{
				entity = entity.Inheritance.InheritsFrom;
				list.Add(entity);
			}
			list.Reverse();
			return list;
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x000728B6 File Offset: 0x00070AB6
		public static bool ShouldExpandInline(ExpressionPath pathToRole, ModelRole role)
		{
			return role.ExpandInline && role.Cardinality == Cardinality.One && !ModelUtil.BeenThereDoneThat(pathToRole, role.RelatedEntity);
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x000728DC File Offset: 0x00070ADC
		public static bool BeenThereDoneThat(ExpressionPath path, ModelEntity entity)
		{
			if (!path.IsEmpty && ModelUtil.IsRelatedByInheritance(path[0].SourceEntity.ModelEntity, entity, false))
			{
				return true;
			}
			using (List<PathItem>.Enumerator enumerator = path.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (ModelUtil.IsRelatedByInheritance(((RolePathItem)enumerator.Current).TargetEntity.ModelEntity, entity, false))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
