using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006E8 RID: 1768
	public class DbExpressionRebinder : DefaultExpressionVisitor
	{
		// Token: 0x060051EF RID: 20975 RVA: 0x001251ED File Offset: 0x001233ED
		internal DbExpressionRebinder()
		{
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x001251F5 File Offset: 0x001233F5
		protected DbExpressionRebinder(MetadataWorkspace targetWorkspace)
		{
			this._metadata = targetWorkspace;
			this._perspective = new ModelPerspective(targetWorkspace);
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x00125210 File Offset: 0x00123410
		protected override EntitySetBase VisitEntitySet(EntitySetBase entitySet)
		{
			EntityContainer entityContainer;
			if (!this._metadata.TryGetEntityContainer(entitySet.EntityContainer.Name, entitySet.EntityContainer.DataSpace, out entityContainer))
			{
				throw new ArgumentException(Strings.Cqt_Copier_EntityContainerNotFound(entitySet.EntityContainer.Name));
			}
			EntitySetBase entitySetBase = null;
			if (entityContainer.BaseEntitySets.TryGetValue(entitySet.Name, false, out entitySetBase) && entitySetBase != null && entitySet.BuiltInTypeKind == entitySetBase.BuiltInTypeKind)
			{
				return entitySetBase;
			}
			throw new ArgumentException(Strings.Cqt_Copier_EntitySetNotFound(entitySet.EntityContainer.Name, entitySet.Name));
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x001252A0 File Offset: 0x001234A0
		protected override EdmFunction VisitFunction(EdmFunction functionMetadata)
		{
			List<TypeUsage> list = new List<TypeUsage>(functionMetadata.Parameters.Count);
			foreach (FunctionParameter functionParameter in functionMetadata.Parameters)
			{
				TypeUsage typeUsage = this.VisitTypeUsage(functionParameter.TypeUsage);
				list.Add(typeUsage);
			}
			IList<EdmFunction> list2;
			if (DataSpace.SSpace == functionMetadata.DataSpace)
			{
				EdmFunction edmFunction = null;
				if (this._metadata.TryGetFunction(functionMetadata.Name, functionMetadata.NamespaceName, list.ToArray(), false, functionMetadata.DataSpace, out edmFunction) && edmFunction != null)
				{
					return edmFunction;
				}
			}
			else if (this._perspective.TryGetFunctionByName(functionMetadata.NamespaceName, functionMetadata.Name, false, out list2))
			{
				bool flag;
				EdmFunction edmFunction2 = FunctionOverloadResolver.ResolveFunctionOverloads(list2, list, false, out flag);
				if (!flag && edmFunction2 != null)
				{
					return edmFunction2;
				}
			}
			throw new ArgumentException(Strings.Cqt_Copier_FunctionNotFound(TypeHelpers.GetFullName(functionMetadata.NamespaceName, functionMetadata.Name)));
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x0012539C File Offset: 0x0012359C
		protected override EdmType VisitType(EdmType type)
		{
			EdmType edmType = type;
			if (BuiltInTypeKind.RefType == type.BuiltInTypeKind)
			{
				RefType refType = (RefType)type;
				EntityType entityType = (EntityType)this.VisitType(refType.ElementType);
				if (refType.ElementType != entityType)
				{
					edmType = new RefType(entityType);
				}
			}
			else if (BuiltInTypeKind.CollectionType == type.BuiltInTypeKind)
			{
				CollectionType collectionType = (CollectionType)type;
				TypeUsage typeUsage = this.VisitTypeUsage(collectionType.TypeUsage);
				if (collectionType.TypeUsage != typeUsage)
				{
					edmType = new CollectionType(typeUsage);
				}
			}
			else if (BuiltInTypeKind.RowType == type.BuiltInTypeKind)
			{
				RowType rowType = (RowType)type;
				List<KeyValuePair<string, TypeUsage>> list = null;
				for (int i = 0; i < rowType.Properties.Count; i++)
				{
					EdmProperty edmProperty = rowType.Properties[i];
					TypeUsage typeUsage2 = this.VisitTypeUsage(edmProperty.TypeUsage);
					if (edmProperty.TypeUsage != typeUsage2)
					{
						if (list == null)
						{
							list = new List<KeyValuePair<string, TypeUsage>>(rowType.Properties.Select((EdmProperty prop) => new KeyValuePair<string, TypeUsage>(prop.Name, prop.TypeUsage)));
						}
						list[i] = new KeyValuePair<string, TypeUsage>(edmProperty.Name, typeUsage2);
					}
				}
				if (list != null)
				{
					edmType = new RowType(list.Select((KeyValuePair<string, TypeUsage> propInfo) => new EdmProperty(propInfo.Key, propInfo.Value)), rowType.InitializerMetadata);
				}
			}
			else if (!this._metadata.TryGetType(type.Name, type.NamespaceName, type.DataSpace, out edmType) || edmType == null)
			{
				throw new ArgumentException(Strings.Cqt_Copier_TypeNotFound(TypeHelpers.GetFullName(type.NamespaceName, type.Name)));
			}
			return edmType;
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x00125548 File Offset: 0x00123748
		protected override TypeUsage VisitTypeUsage(TypeUsage type)
		{
			EdmType edmType = this.VisitType(type.EdmType);
			if (edmType == type.EdmType)
			{
				return type;
			}
			Facet[] array = new Facet[type.Facets.Count];
			int num = 0;
			foreach (Facet facet in type.Facets)
			{
				array[num] = facet;
				num++;
			}
			return TypeUsage.Create(edmType, array);
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x001255D0 File Offset: 0x001237D0
		private static bool TryGetMember<TMember>(DbExpression instance, string memberName, out TMember member) where TMember : EdmMember
		{
			member = default(TMember);
			StructuralType structuralType = instance.ResultType.EdmType as StructuralType;
			if (structuralType != null)
			{
				EdmMember edmMember = null;
				if (structuralType.Members.TryGetValue(memberName, false, out edmMember))
				{
					member = edmMember as TMember;
				}
			}
			return member != null;
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x0012562C File Offset: 0x0012382C
		public override DbExpression Visit(DbPropertyExpression expression)
		{
			Check.NotNull<DbPropertyExpression>(expression, "expression");
			DbExpression dbExpression = expression;
			DbExpression dbExpression2 = this.VisitExpression(expression.Instance);
			if (expression.Instance != dbExpression2)
			{
				if (Helper.IsRelationshipEndMember(expression.Property))
				{
					RelationshipEndMember relationshipEndMember;
					if (!DbExpressionRebinder.TryGetMember<RelationshipEndMember>(dbExpression2, expression.Property.Name, out relationshipEndMember))
					{
						EdmType edmType = dbExpression2.ResultType.EdmType;
						throw new ArgumentException(Strings.Cqt_Copier_EndNotFound(expression.Property.Name, TypeHelpers.GetFullName(edmType.NamespaceName, edmType.Name)));
					}
					dbExpression = dbExpression2.Property(relationshipEndMember);
				}
				else if (Helper.IsNavigationProperty(expression.Property))
				{
					NavigationProperty navigationProperty;
					if (!DbExpressionRebinder.TryGetMember<NavigationProperty>(dbExpression2, expression.Property.Name, out navigationProperty))
					{
						EdmType edmType2 = dbExpression2.ResultType.EdmType;
						throw new ArgumentException(Strings.Cqt_Copier_NavPropertyNotFound(expression.Property.Name, TypeHelpers.GetFullName(edmType2.NamespaceName, edmType2.Name)));
					}
					dbExpression = dbExpression2.Property(navigationProperty);
				}
				else
				{
					EdmProperty edmProperty;
					if (!DbExpressionRebinder.TryGetMember<EdmProperty>(dbExpression2, expression.Property.Name, out edmProperty))
					{
						EdmType edmType3 = dbExpression2.ResultType.EdmType;
						throw new ArgumentException(Strings.Cqt_Copier_PropertyNotFound(expression.Property.Name, TypeHelpers.GetFullName(edmType3.NamespaceName, edmType3.Name)));
					}
					dbExpression = dbExpression2.Property(edmProperty);
				}
			}
			return dbExpression;
		}

		// Token: 0x04001DD6 RID: 7638
		private readonly MetadataWorkspace _metadata;

		// Token: 0x04001DD7 RID: 7639
		private readonly Perspective _perspective;
	}
}
