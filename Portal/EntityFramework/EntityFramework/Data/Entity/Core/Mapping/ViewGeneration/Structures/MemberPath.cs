using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005A7 RID: 1447
	internal sealed class MemberPath : InternalBase, IEquatable<MemberPath>
	{
		// Token: 0x06004637 RID: 17975 RVA: 0x000F7CFB File Offset: 0x000F5EFB
		internal MemberPath(EntitySetBase extent, IEnumerable<EdmMember> path)
		{
			this.m_extent = extent;
			this.m_path = path.ToList<EdmMember>();
		}

		// Token: 0x06004638 RID: 17976 RVA: 0x000F7D16 File Offset: 0x000F5F16
		internal MemberPath(EntitySetBase extent)
			: this(extent, Enumerable.Empty<EdmMember>())
		{
		}

		// Token: 0x06004639 RID: 17977 RVA: 0x000F7D24 File Offset: 0x000F5F24
		internal MemberPath(EntitySetBase extent, EdmMember member)
			: this(extent, Enumerable.Repeat<EdmMember>(member, 1))
		{
		}

		// Token: 0x0600463A RID: 17978 RVA: 0x000F7D34 File Offset: 0x000F5F34
		internal MemberPath(MemberPath prefix, EdmMember last)
		{
			this.m_extent = prefix.m_extent;
			this.m_path = new List<EdmMember>(prefix.m_path);
			this.m_path.Add(last);
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x0600463B RID: 17979 RVA: 0x000F7D65 File Offset: 0x000F5F65
		internal EdmMember RootEdmMember
		{
			get
			{
				if (this.m_path.Count <= 0)
				{
					return null;
				}
				return this.m_path[0];
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x0600463C RID: 17980 RVA: 0x000F7D83 File Offset: 0x000F5F83
		internal EdmMember LeafEdmMember
		{
			get
			{
				if (this.m_path.Count <= 0)
				{
					return null;
				}
				return this.m_path[this.m_path.Count - 1];
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x0600463D RID: 17981 RVA: 0x000F7DAD File Offset: 0x000F5FAD
		internal string LeafName
		{
			get
			{
				if (this.m_path.Count == 0)
				{
					return this.m_extent.Name;
				}
				return this.LeafEdmMember.Name;
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x0600463E RID: 17982 RVA: 0x000F7DD3 File Offset: 0x000F5FD3
		internal bool IsComputed
		{
			get
			{
				return this.m_path.Count != 0 && this.RootEdmMember.IsStoreGeneratedComputed;
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x0600463F RID: 17983 RVA: 0x000F7DF0 File Offset: 0x000F5FF0
		internal object DefaultValue
		{
			get
			{
				if (this.m_path.Count == 0)
				{
					return null;
				}
				Facet facet;
				if (this.LeafEdmMember.TypeUsage.Facets.TryGetValue("DefaultValue", false, out facet))
				{
					return facet.Value;
				}
				return null;
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06004640 RID: 17984 RVA: 0x000F7E33 File Offset: 0x000F6033
		internal bool IsPartOfKey
		{
			get
			{
				return this.m_path.Count != 0 && MetadataHelper.IsPartOfEntityTypeKey(this.LeafEdmMember);
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06004641 RID: 17985 RVA: 0x000F7E4F File Offset: 0x000F604F
		internal bool IsNullable
		{
			get
			{
				return this.m_path.Count != 0 && MetadataHelper.IsMemberNullable(this.LeafEdmMember);
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06004642 RID: 17986 RVA: 0x000F7E6C File Offset: 0x000F606C
		internal EntitySet EntitySet
		{
			get
			{
				if (this.m_path.Count == 0)
				{
					return this.m_extent as EntitySet;
				}
				if (this.m_path.Count == 1)
				{
					AssociationEndMember associationEndMember = this.RootEdmMember as AssociationEndMember;
					if (associationEndMember != null)
					{
						return MetadataHelper.GetEntitySetAtEnd((AssociationSet)this.m_extent, associationEndMember);
					}
				}
				return null;
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06004643 RID: 17987 RVA: 0x000F7EC2 File Offset: 0x000F60C2
		internal EntitySetBase Extent
		{
			get
			{
				return this.m_extent;
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06004644 RID: 17988 RVA: 0x000F7ECA File Offset: 0x000F60CA
		internal EdmType EdmType
		{
			get
			{
				if (this.m_path.Count > 0)
				{
					return this.LeafEdmMember.TypeUsage.EdmType;
				}
				return this.m_extent.ElementType;
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06004645 RID: 17989 RVA: 0x000F7EF8 File Offset: 0x000F60F8
		internal string CqlFieldAlias
		{
			get
			{
				string text = this.PathToString(new bool?(true));
				if (!text.Contains("_"))
				{
					text = text.Replace('.', '_');
				}
				StringBuilder stringBuilder = new StringBuilder();
				CqlWriter.AppendEscapedName(stringBuilder, text);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x000F7F3C File Offset: 0x000F613C
		internal bool IsAlwaysDefined(Dictionary<EntityType, Set<EntityType>> inheritanceGraph)
		{
			if (this.m_path.Count == 0)
			{
				return true;
			}
			EdmMember edmMember = this.m_path.Last<EdmMember>();
			for (int i = 0; i < this.m_path.Count - 1; i++)
			{
				if (MetadataHelper.IsMemberNullable(this.m_path[i]))
				{
					return false;
				}
			}
			if (this.m_path[0].DeclaringType is AssociationType)
			{
				return true;
			}
			EntityType entityType = this.m_extent.ElementType as EntityType;
			if (entityType == null)
			{
				return true;
			}
			EntityType entityType2 = this.m_path[0].DeclaringType as EntityType;
			EntityType entityType3 = entityType2.BaseType as EntityType;
			return entityType.EdmEquals(entityType2) || MetadataHelper.IsParentOf(entityType2, entityType) || entityType3 == null || ((entityType3.Abstract || MetadataHelper.DoesMemberExist(entityType3, edmMember)) && !MemberPath.RecurseToFindMemberAbsentInConcreteType(entityType3, entityType2, edmMember, entityType, inheritanceGraph));
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x000F8020 File Offset: 0x000F6220
		private static bool RecurseToFindMemberAbsentInConcreteType(EntityType current, EntityType avoidEdge, EdmMember member, EntityType entitySetType, Dictionary<EntityType, Set<EntityType>> inheritanceGraph)
		{
			IEnumerable<EntityType> enumerable = inheritanceGraph[current];
			Func<EntityType, bool> <>9__0;
			Func<EntityType, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (EntityType type) => !type.EdmEquals(avoidEdge));
			}
			foreach (EntityType entityType in enumerable.Where(func))
			{
				if (entitySetType.BaseType == null || !entitySetType.BaseType.EdmEquals(entityType))
				{
					if (!entityType.Abstract && !MetadataHelper.DoesMemberExist(entityType, member))
					{
						return true;
					}
					if (MemberPath.RecurseToFindMemberAbsentInConcreteType(entityType, current, member, entitySetType, inheritanceGraph))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x000F80DC File Offset: 0x000F62DC
		internal void GetIdentifiers(CqlIdentifiers identifiers)
		{
			identifiers.AddIdentifier(this.m_extent.Name);
			identifiers.AddIdentifier(this.m_extent.ElementType.Name);
			foreach (EdmMember edmMember in this.m_path)
			{
				identifiers.AddIdentifier(edmMember.Name);
			}
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x000F815C File Offset: 0x000F635C
		internal static bool AreAllMembersNullable(IEnumerable<MemberPath> members)
		{
			foreach (MemberPath memberPath in members)
			{
				if (memberPath.m_path.Count == 0)
				{
					return false;
				}
				if (!memberPath.IsNullable)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x000F81C0 File Offset: 0x000F63C0
		internal static string PropertiesToUserString(IEnumerable<MemberPath> members, bool fullPath)
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MemberPath memberPath in members)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				if (fullPath)
				{
					stringBuilder.Append(memberPath.PathToString(new bool?(false)));
				}
				else
				{
					stringBuilder.Append(memberPath.LeafName);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x000F8248 File Offset: 0x000F6448
		internal StringBuilder AsEsql(StringBuilder inputBuilder, string blockAlias)
		{
			StringBuilder builder = new StringBuilder();
			CqlWriter.AppendEscapedName(builder, blockAlias);
			this.AsCql(delegate(string memberName)
			{
				builder.Append('.');
				CqlWriter.AppendEscapedName(builder, memberName);
			}, delegate
			{
				builder.Insert(0, "Key(");
				builder.Append(")");
			}, delegate(StructuralType treatAsType)
			{
				builder.Insert(0, "TREAT(");
				builder.Append(" AS ");
				CqlWriter.AppendEscapedTypeName(builder, treatAsType);
				builder.Append(')');
			});
			inputBuilder.Append(builder);
			return inputBuilder;
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x000F82AC File Offset: 0x000F64AC
		internal DbExpression AsCqt(DbExpression row)
		{
			this.AsCql(delegate(string memberName)
			{
				row = row.Property(memberName);
			}, delegate
			{
				row = row.GetRefKey();
			}, delegate(StructuralType treatAsType)
			{
				TypeUsage typeUsage = TypeUsage.Create(treatAsType);
				row = row.TreatAs(typeUsage);
			});
			return row;
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x000F82F8 File Offset: 0x000F64F8
		internal void AsCql(Action<string> accessMember, Action getKey, Action<StructuralType> treatAs)
		{
			EdmType edmType = this.m_extent.ElementType;
			foreach (EdmMember edmMember in this.m_path)
			{
				RefType refType;
				StructuralType structuralType;
				if (Helper.IsRefType(edmType))
				{
					refType = (RefType)edmType;
					structuralType = refType.ElementType;
				}
				else
				{
					refType = null;
					structuralType = (StructuralType)edmType;
				}
				bool flag = MetadataHelper.DoesMemberExist(structuralType, edmMember);
				if (refType != null)
				{
					getKey();
				}
				else if (!flag)
				{
					treatAs(edmMember.DeclaringType);
				}
				accessMember(edmMember.Name);
				edmType = edmMember.TypeUsage.EdmType;
			}
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x000F83B4 File Offset: 0x000F65B4
		public bool Equals(MemberPath right)
		{
			return MemberPath.EqualityComparer.Equals(this, right);
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x000F83C4 File Offset: 0x000F65C4
		public override bool Equals(object obj)
		{
			MemberPath memberPath = obj as MemberPath;
			return obj != null && this.Equals(memberPath);
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x000F83E4 File Offset: 0x000F65E4
		public override int GetHashCode()
		{
			return MemberPath.EqualityComparer.GetHashCode(this);
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x000F83F1 File Offset: 0x000F65F1
		internal bool IsScalarType()
		{
			return this.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType || this.EdmType.BuiltInTypeKind == BuiltInTypeKind.EnumType;
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x000F8414 File Offset: 0x000F6614
		internal static IEnumerable<MemberPath> GetKeyMembers(EntitySetBase extent, MemberDomainMap domainMap)
		{
			MemberPath memberPath = new MemberPath(extent);
			return new List<MemberPath>(memberPath.GetMembers(memberPath.Extent.ElementType, null, null, new bool?(true), domainMap));
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x000F8455 File Offset: 0x000F6655
		internal IEnumerable<MemberPath> GetMembers(EdmType edmType, bool? isScalar, bool? isConditional, bool? isPartOfKey, MemberDomainMap domainMap)
		{
			StructuralType structuralType = (StructuralType)edmType;
			foreach (EdmMember edmMember in structuralType.Members)
			{
				if (edmMember is AssociationEndMember)
				{
					foreach (MemberPath memberPath in new MemberPath(this, edmMember).GetMembers(((RefType)edmMember.TypeUsage.EdmType).ElementType, isScalar, isConditional, new bool?(true), domainMap))
					{
						yield return memberPath;
					}
					IEnumerator<MemberPath> enumerator2 = null;
				}
				bool flag = MetadataHelper.IsNonRefSimpleMember(edmMember);
				if (isScalar == null)
				{
					goto IL_0160;
				}
				bool? flag2 = isScalar;
				bool flag3 = flag;
				if ((flag2.GetValueOrDefault() == flag3) & (flag2 != null))
				{
					goto IL_0160;
				}
				IL_0212:
				edmMember = null;
				continue;
				IL_0160:
				EdmProperty edmProperty = edmMember as EdmProperty;
				if (edmProperty != null)
				{
					bool flag4 = MetadataHelper.IsPartOfEntityTypeKey(edmProperty);
					if (isPartOfKey != null)
					{
						flag2 = isPartOfKey;
						flag3 = flag4;
						if (!((flag2.GetValueOrDefault() == flag3) & (flag2 != null)))
						{
							goto IL_0212;
						}
					}
					MemberPath memberPath2 = new MemberPath(this, edmProperty);
					bool flag5 = domainMap.IsConditionMember(memberPath2);
					if (isConditional != null)
					{
						flag2 = isConditional;
						flag3 = flag5;
						if (!((flag2.GetValueOrDefault() == flag3) & (flag2 != null)))
						{
							goto IL_0212;
						}
					}
					yield return memberPath2;
					goto IL_0212;
				}
				goto IL_0212;
			}
			ReadOnlyMetadataCollection<EdmMember>.Enumerator enumerator = default(ReadOnlyMetadataCollection<EdmMember>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x000F848C File Offset: 0x000F668C
		internal bool IsEquivalentViaRefConstraint(MemberPath path1)
		{
			if (this.EdmType is EntityTypeBase || path1.EdmType is EntityTypeBase || !MetadataHelper.IsNonRefSimpleMember(this.LeafEdmMember) || !MetadataHelper.IsNonRefSimpleMember(path1.LeafEdmMember))
			{
				return false;
			}
			AssociationSet associationSet = this.Extent as AssociationSet;
			AssociationSet associationSet2 = path1.Extent as AssociationSet;
			EntitySet entitySet = this.Extent as EntitySet;
			EntitySet entitySet2 = path1.Extent as EntitySet;
			bool flag = false;
			if (associationSet != null && associationSet2 != null)
			{
				if (!associationSet.Equals(associationSet2))
				{
					return false;
				}
				flag = MemberPath.AreAssociationEndPathsEquivalentViaRefConstraint(this, path1, associationSet);
			}
			else
			{
				if (entitySet != null && entitySet2 != null)
				{
					using (List<AssociationSet>.Enumerator enumerator = MetadataHelper.GetAssociationsForEntitySets(entitySet, entitySet2).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							AssociationSet associationSet3 = enumerator.Current;
							MemberPath correspondingAssociationPath = this.GetCorrespondingAssociationPath(associationSet3);
							MemberPath correspondingAssociationPath2 = path1.GetCorrespondingAssociationPath(associationSet3);
							if (MemberPath.AreAssociationEndPathsEquivalentViaRefConstraint(correspondingAssociationPath, correspondingAssociationPath2, associationSet3))
							{
								flag = true;
								break;
							}
						}
						return flag;
					}
				}
				AssociationSet associationSet4 = ((associationSet != null) ? associationSet : associationSet2);
				MemberPath memberPath = ((this.Extent is AssociationSet) ? this : path1);
				MemberPath correspondingAssociationPath3 = ((this.Extent is EntitySet) ? this : path1).GetCorrespondingAssociationPath(associationSet4);
				flag = correspondingAssociationPath3 != null && MemberPath.AreAssociationEndPathsEquivalentViaRefConstraint(memberPath, correspondingAssociationPath3, associationSet4);
			}
			return flag;
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x000F85E4 File Offset: 0x000F67E4
		private static bool AreAssociationEndPathsEquivalentViaRefConstraint(MemberPath assocPath0, MemberPath assocPath1, AssociationSet assocSet)
		{
			AssociationEndMember associationEndMember = assocPath0.RootEdmMember as AssociationEndMember;
			AssociationEndMember associationEndMember2 = assocPath1.RootEdmMember as AssociationEndMember;
			EdmProperty edmProperty = assocPath0.LeafEdmMember as EdmProperty;
			EdmProperty edmProperty2 = assocPath1.LeafEdmMember as EdmProperty;
			if (associationEndMember == null || associationEndMember2 == null || edmProperty == null || edmProperty2 == null)
			{
				return false;
			}
			AssociationType elementType = assocSet.ElementType;
			bool flag = false;
			foreach (ReferentialConstraint referentialConstraint in elementType.ReferentialConstraints)
			{
				bool flag2 = associationEndMember.Name == referentialConstraint.FromRole.Name && associationEndMember2.Name == referentialConstraint.ToRole.Name;
				bool flag3 = associationEndMember2.Name == referentialConstraint.FromRole.Name && associationEndMember.Name == referentialConstraint.ToRole.Name;
				if (flag2 || flag3)
				{
					ReadOnlyMetadataCollection<EdmProperty> readOnlyMetadataCollection = (flag2 ? referentialConstraint.FromProperties : referentialConstraint.ToProperties);
					ReadOnlyMetadataCollection<EdmProperty> readOnlyMetadataCollection2 = (flag2 ? referentialConstraint.ToProperties : referentialConstraint.FromProperties);
					int num = readOnlyMetadataCollection.IndexOf(edmProperty);
					int num2 = readOnlyMetadataCollection2.IndexOf(edmProperty2);
					if (num == num2 && num != -1)
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x000F8740 File Offset: 0x000F6940
		private MemberPath GetCorrespondingAssociationPath(AssociationSet assocSet)
		{
			AssociationEndMember someEndForEntitySet = MetadataHelper.GetSomeEndForEntitySet(assocSet, this.m_extent);
			if (someEndForEntitySet == null)
			{
				return null;
			}
			List<EdmMember> list = new List<EdmMember>();
			list.Add(someEndForEntitySet);
			list.AddRange(this.m_path);
			return new MemberPath(assocSet, list);
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x000F8780 File Offset: 0x000F6980
		internal EntitySet GetScopeOfRelationEnd()
		{
			if (this.m_path.Count == 0)
			{
				return null;
			}
			AssociationEndMember associationEndMember = this.LeafEdmMember as AssociationEndMember;
			if (associationEndMember == null)
			{
				return null;
			}
			return MetadataHelper.GetEntitySetAtEnd((AssociationSet)this.m_extent, associationEndMember);
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x000F87C0 File Offset: 0x000F69C0
		internal string PathToString(bool? forAlias)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (forAlias != null)
			{
				bool? flag = forAlias;
				bool flag2 = true;
				if ((flag.GetValueOrDefault() == flag2) & (flag != null))
				{
					if (this.m_path.Count == 0)
					{
						return this.m_extent.ElementType.Name;
					}
					stringBuilder.Append(this.m_path[0].DeclaringType.Name);
				}
				else
				{
					stringBuilder.Append(this.m_extent.Name);
				}
			}
			for (int i = 0; i < this.m_path.Count; i++)
			{
				stringBuilder.Append('.');
				stringBuilder.Append(this.m_path[i].Name);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x000F8880 File Offset: 0x000F6A80
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.PathToString(new bool?(false)));
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x000F8898 File Offset: 0x000F6A98
		internal void ToCompactString(StringBuilder builder, string instanceToken)
		{
			builder.Append(instanceToken + this.PathToString(null));
		}

		// Token: 0x04001917 RID: 6423
		private readonly EntitySetBase m_extent;

		// Token: 0x04001918 RID: 6424
		private readonly List<EdmMember> m_path;

		// Token: 0x04001919 RID: 6425
		internal static readonly IEqualityComparer<MemberPath> EqualityComparer = new MemberPath.Comparer();

		// Token: 0x02000BD8 RID: 3032
		private sealed class Comparer : IEqualityComparer<MemberPath>
		{
			// Token: 0x06006834 RID: 26676 RVA: 0x001632C0 File Offset: 0x001614C0
			public bool Equals(MemberPath left, MemberPath right)
			{
				if (left == right)
				{
					return true;
				}
				if (left == null || right == null)
				{
					return false;
				}
				if (!left.m_extent.Equals(right.m_extent) || left.m_path.Count != right.m_path.Count)
				{
					return false;
				}
				for (int i = 0; i < left.m_path.Count; i++)
				{
					if (!left.m_path[i].Equals(right.m_path[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06006835 RID: 26677 RVA: 0x00163340 File Offset: 0x00161540
			public int GetHashCode(MemberPath key)
			{
				int num = key.m_extent.GetHashCode();
				foreach (EdmMember edmMember in key.m_path)
				{
					num ^= edmMember.GetHashCode();
				}
				return num;
			}
		}
	}
}
