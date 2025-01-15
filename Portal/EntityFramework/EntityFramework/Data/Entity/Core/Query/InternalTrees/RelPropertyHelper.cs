using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003D6 RID: 982
	internal sealed class RelPropertyHelper
	{
		// Token: 0x06002EC3 RID: 11971 RVA: 0x00094E78 File Offset: 0x00093078
		private void AddRelProperty(AssociationType associationType, AssociationEndMember fromEnd, AssociationEndMember toEnd)
		{
			if (toEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many)
			{
				return;
			}
			RelProperty relProperty = new RelProperty(associationType, fromEnd, toEnd);
			if (this._interestingRelProperties == null || !this._interestingRelProperties.Contains(relProperty))
			{
				return;
			}
			EntityTypeBase elementType = ((RefType)fromEnd.TypeUsage.EdmType).ElementType;
			List<RelProperty> list;
			if (!this._relPropertyMap.TryGetValue(elementType, out list))
			{
				list = new List<RelProperty>();
				this._relPropertyMap[elementType] = list;
			}
			list.Add(relProperty);
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x00094EF0 File Offset: 0x000930F0
		private void ProcessRelationship(RelationshipType relationshipType)
		{
			AssociationType associationType = relationshipType as AssociationType;
			if (associationType == null)
			{
				return;
			}
			if (associationType.AssociationEndMembers.Count != 2)
			{
				return;
			}
			AssociationEndMember associationEndMember = associationType.AssociationEndMembers[0];
			AssociationEndMember associationEndMember2 = associationType.AssociationEndMembers[1];
			this.AddRelProperty(associationType, associationEndMember, associationEndMember2);
			this.AddRelProperty(associationType, associationEndMember2, associationEndMember);
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x00094F44 File Offset: 0x00093144
		internal RelPropertyHelper(MetadataWorkspace ws, HashSet<RelProperty> interestingRelProperties)
		{
			this._relPropertyMap = new Dictionary<EntityTypeBase, List<RelProperty>>();
			this._interestingRelProperties = interestingRelProperties;
			foreach (RelationshipType relationshipType in ws.GetItems<RelationshipType>(DataSpace.CSpace))
			{
				this.ProcessRelationship(relationshipType);
			}
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x00094FAC File Offset: 0x000931AC
		internal IEnumerable<RelProperty> GetDeclaredOnlyRelProperties(EntityTypeBase entityType)
		{
			List<RelProperty> list;
			if (this._relPropertyMap.TryGetValue(entityType, out list))
			{
				foreach (RelProperty relProperty in list)
				{
					yield return relProperty;
				}
				List<RelProperty>.Enumerator enumerator = default(List<RelProperty>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x00094FC3 File Offset: 0x000931C3
		internal IEnumerable<RelProperty> GetRelProperties(EntityTypeBase entityType)
		{
			IEnumerator<RelProperty> enumerator;
			if (entityType.BaseType != null)
			{
				foreach (RelProperty relProperty in this.GetRelProperties(entityType.BaseType as EntityTypeBase))
				{
					yield return relProperty;
				}
				enumerator = null;
			}
			foreach (RelProperty relProperty2 in this.GetDeclaredOnlyRelProperties(entityType))
			{
				yield return relProperty2;
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000FC4 RID: 4036
		private readonly Dictionary<EntityTypeBase, List<RelProperty>> _relPropertyMap;

		// Token: 0x04000FC5 RID: 4037
		private readonly HashSet<RelProperty> _interestingRelProperties;
	}
}
