using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000585 RID: 1413
	internal class FragmentQueryKB : KnowledgeBase<DomainConstraint<BoolLiteral, Constant>>
	{
		// Token: 0x06004448 RID: 17480 RVA: 0x000F00CE File Offset: 0x000EE2CE
		internal override void AddFact(BoolExpr<DomainConstraint<BoolLiteral, Constant>> fact)
		{
			base.AddFact(fact);
			this._kbExpression = new AndExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[] { this._kbExpression, fact });
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06004449 RID: 17481 RVA: 0x000F00F5 File Offset: 0x000EE2F5
		internal BoolExpr<DomainConstraint<BoolLiteral, Constant>> KbExpression
		{
			get
			{
				return this._kbExpression;
			}
		}

		// Token: 0x0600444A RID: 17482 RVA: 0x000F00FD File Offset: 0x000EE2FD
		internal void CreateVariableConstraints(EntitySetBase extent, MemberDomainMap domainMap, EdmItemCollection edmItemCollection)
		{
			this.CreateVariableConstraintsRecursion(extent.ElementType, new MemberPath(extent), domainMap, edmItemCollection);
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x000F0114 File Offset: 0x000EE314
		internal void CreateAssociationConstraints(EntitySetBase extent, MemberDomainMap domainMap, EdmItemCollection edmItemCollection)
		{
			AssociationSet associationSet = extent as AssociationSet;
			if (associationSet != null)
			{
				BoolExpression boolExpression = BoolExpression.CreateLiteral(new RoleBoolean(associationSet), domainMap);
				HashSet<Pair<EdmMember, EntityType>> associationkeys = new HashSet<Pair<EdmMember, EntityType>>();
				foreach (AssociationEndMember associationEndMember in associationSet.ElementType.AssociationEndMembers)
				{
					EntityType type = (EntityType)((RefType)associationEndMember.TypeUsage.EdmType).ElementType;
					type.KeyMembers.All((EdmMember member) => associationkeys.Add(new Pair<EdmMember, EntityType>(member, type)) || true);
				}
				foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
				{
					HashSet<EdmType> hashSet = new HashSet<EdmType>();
					hashSet.UnionWith(MetadataHelper.GetTypeAndSubtypesOf(associationSetEnd.CorrespondingAssociationEndMember.TypeUsage.EdmType, edmItemCollection, false));
					BoolExpression boolExpression2 = FragmentQueryKB.CreateIsOfTypeCondition(new MemberPath(associationSetEnd.EntitySet), hashSet, domainMap);
					BoolExpression boolExpression3 = BoolExpression.CreateLiteral(new RoleBoolean(associationSetEnd), domainMap);
					BoolExpression boolExpression4 = BoolExpression.CreateAnd(new BoolExpression[]
					{
						BoolExpression.CreateLiteral(new RoleBoolean(associationSetEnd.EntitySet), domainMap),
						boolExpression2
					});
					base.AddImplication(boolExpression3.Tree, boolExpression4.Tree);
					if (MetadataHelper.IsEveryOtherEndAtLeastOne(associationSet, associationSetEnd.CorrespondingAssociationEndMember))
					{
						base.AddImplication(boolExpression4.Tree, boolExpression3.Tree);
					}
					if (MetadataHelper.DoesEndKeySubsumeAssociationSetKey(associationSet, associationSetEnd.CorrespondingAssociationEndMember, associationkeys))
					{
						base.AddEquivalence(boolExpression3.Tree, boolExpression.Tree);
					}
				}
				foreach (ReferentialConstraint referentialConstraint in associationSet.ElementType.ReferentialConstraints)
				{
					AssociationEndMember associationEndMember2 = (AssociationEndMember)referentialConstraint.ToRole;
					EntitySet entitySetAtEnd = MetadataHelper.GetEntitySetAtEnd(associationSet, associationEndMember2);
					if (Helpers.IsSetEqual<EdmMember>(Helpers.AsSuperTypeList<EdmProperty, EdmMember>(referentialConstraint.ToProperties), entitySetAtEnd.ElementType.KeyMembers, EqualityComparer<EdmMember>.Default) && referentialConstraint.FromRole.RelationshipMultiplicity.Equals(RelationshipMultiplicity.One))
					{
						BoolExpression boolExpression5 = BoolExpression.CreateLiteral(new RoleBoolean(associationSet.AssociationSetEnds[0]), domainMap);
						BoolExpression boolExpression6 = BoolExpression.CreateLiteral(new RoleBoolean(associationSet.AssociationSetEnds[1]), domainMap);
						base.AddEquivalence(boolExpression5.Tree, boolExpression6.Tree);
					}
				}
			}
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x000F03E4 File Offset: 0x000EE5E4
		internal void CreateEquivalenceConstraintForOneToOneForeignKeyAssociation(AssociationSet assocSet, MemberDomainMap domainMap)
		{
			foreach (ReferentialConstraint referentialConstraint in assocSet.ElementType.ReferentialConstraints)
			{
				AssociationEndMember associationEndMember = (AssociationEndMember)referentialConstraint.ToRole;
				AssociationEndMember associationEndMember2 = (AssociationEndMember)referentialConstraint.FromRole;
				EntitySet entitySetAtEnd = MetadataHelper.GetEntitySetAtEnd(assocSet, associationEndMember);
				EntitySet entitySetAtEnd2 = MetadataHelper.GetEntitySetAtEnd(assocSet, associationEndMember2);
				if (Helpers.IsSetEqual<EdmMember>(Helpers.AsSuperTypeList<EdmProperty, EdmMember>(referentialConstraint.ToProperties), entitySetAtEnd.ElementType.KeyMembers, EqualityComparer<EdmMember>.Default))
				{
					BoolExpression boolExpression = BoolExpression.CreateLiteral(new RoleBoolean(entitySetAtEnd2), domainMap);
					BoolExpression boolExpression2 = BoolExpression.CreateLiteral(new RoleBoolean(entitySetAtEnd), domainMap);
					base.AddEquivalence(boolExpression.Tree, boolExpression2.Tree);
				}
			}
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x000F04B8 File Offset: 0x000EE6B8
		private void CreateVariableConstraintsRecursion(EdmType edmType, MemberPath currentPath, MemberDomainMap domainMap, EdmItemCollection edmItemCollection)
		{
			HashSet<EdmType> hashSet = new HashSet<EdmType>();
			hashSet.UnionWith(MetadataHelper.GetTypeAndSubtypesOf(edmType, edmItemCollection, true));
			foreach (EdmType edmType2 in hashSet)
			{
				HashSet<EdmType> hashSet2 = new HashSet<EdmType>();
				hashSet2.UnionWith(MetadataHelper.GetTypeAndSubtypesOf(edmType2, edmItemCollection, false));
				if (hashSet2.Count != 0)
				{
					BoolExpression boolExpression = BoolExpression.CreateNot(FragmentQueryKB.CreateIsOfTypeCondition(currentPath, hashSet2, domainMap));
					if (boolExpression.IsSatisfiable())
					{
						foreach (EdmProperty edmProperty in ((StructuralType)edmType2).GetDeclaredOnlyMembers<EdmProperty>())
						{
							MemberPath memberPath = new MemberPath(currentPath, edmProperty);
							bool flag = MetadataHelper.IsNonRefSimpleMember(edmProperty);
							if (domainMap.IsConditionMember(memberPath) || domainMap.IsProjectedConditionMember(memberPath))
							{
								List<Constant> list = new List<Constant>(domainMap.GetDomain(memberPath));
								BoolExpression boolExpression2;
								if (flag)
								{
									boolExpression2 = BoolExpression.CreateLiteral(new ScalarRestriction(new MemberProjectedSlot(memberPath), new Domain(Constant.Undefined, list)), domainMap);
								}
								else
								{
									boolExpression2 = BoolExpression.CreateLiteral(new TypeRestriction(new MemberProjectedSlot(memberPath), new Domain(Constant.Undefined, list)), domainMap);
								}
								base.AddEquivalence(boolExpression.Tree, boolExpression2.Tree);
							}
							if (!flag)
							{
								this.CreateVariableConstraintsRecursion(memberPath.EdmType, memberPath, domainMap, edmItemCollection);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x000F0658 File Offset: 0x000EE858
		private static BoolExpression CreateIsOfTypeCondition(MemberPath currentPath, IEnumerable<EdmType> derivedTypes, MemberDomainMap domainMap)
		{
			Domain domain = new Domain(derivedTypes.Select((EdmType derivedType) => new TypeConstant(derivedType)), domainMap.GetDomain(currentPath));
			return BoolExpression.CreateLiteral(new TypeRestriction(new MemberProjectedSlot(currentPath), domain), domainMap);
		}

		// Token: 0x04001899 RID: 6297
		private BoolExpr<DomainConstraint<BoolLiteral, Constant>> _kbExpression = TrueExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
	}
}
