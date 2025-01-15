using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000341 RID: 833
	internal class ForeignKeyConstraint
	{
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x00075358 File Offset: 0x00073558
		internal List<string> ParentKeys
		{
			get
			{
				return this.m_parentKeys;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x060027BA RID: 10170 RVA: 0x00075360 File Offset: 0x00073560
		internal List<string> ChildKeys
		{
			get
			{
				return this.m_childKeys;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x060027BB RID: 10171 RVA: 0x00075368 File Offset: 0x00073568
		internal ExtentPair Pair
		{
			get
			{
				return this.m_extentPair;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x060027BC RID: 10172 RVA: 0x00075370 File Offset: 0x00073570
		internal RelationshipMultiplicity ChildMultiplicity
		{
			get
			{
				return this.m_constraint.ToRole.RelationshipMultiplicity;
			}
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x00075382 File Offset: 0x00073582
		internal bool GetParentProperty(string childPropertyName, out string parentPropertyName)
		{
			this.BuildKeyMap();
			return this.m_keyMap.TryGetValue(childPropertyName, out parentPropertyName);
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x00075398 File Offset: 0x00073598
		internal ForeignKeyConstraint(RelationshipSet relationshipSet, ReferentialConstraint constraint)
		{
			AssociationSet associationSet = relationshipSet as AssociationSet;
			AssociationEndMember associationEndMember = constraint.FromRole as AssociationEndMember;
			AssociationEndMember associationEndMember2 = constraint.ToRole as AssociationEndMember;
			if (associationSet == null || associationEndMember == null || associationEndMember2 == null)
			{
				throw new NotSupportedException();
			}
			this.m_constraint = constraint;
			EntitySet entitySetAtEnd = MetadataHelper.GetEntitySetAtEnd(associationSet, associationEndMember);
			EntitySet entitySetAtEnd2 = MetadataHelper.GetEntitySetAtEnd(associationSet, associationEndMember2);
			this.m_extentPair = new ExtentPair(entitySetAtEnd, entitySetAtEnd2);
			this.m_childKeys = new List<string>();
			foreach (EdmProperty edmProperty in constraint.ToProperties)
			{
				this.m_childKeys.Add(edmProperty.Name);
			}
			this.m_parentKeys = new List<string>();
			foreach (EdmProperty edmProperty2 in constraint.FromProperties)
			{
				this.m_parentKeys.Add(edmProperty2.Name);
			}
			PlanCompiler.Assert(associationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne || RelationshipMultiplicity.One == associationEndMember.RelationshipMultiplicity, "from-end of relationship constraint cannot have multiplicity greater than 1");
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x000754D8 File Offset: 0x000736D8
		private void BuildKeyMap()
		{
			if (this.m_keyMap != null)
			{
				return;
			}
			this.m_keyMap = new Dictionary<string, string>();
			IEnumerator<EdmProperty> enumerator = this.m_constraint.FromProperties.GetEnumerator();
			IEnumerator<EdmProperty> enumerator2 = this.m_constraint.ToProperties.GetEnumerator();
			for (;;)
			{
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				PlanCompiler.Assert(flag == flag2, "key count mismatch");
				if (flag)
				{
					break;
				}
				this.m_keyMap[enumerator2.Current.Name] = enumerator.Current.Name;
			}
		}

		// Token: 0x04000DDC RID: 3548
		private readonly ExtentPair m_extentPair;

		// Token: 0x04000DDD RID: 3549
		private readonly List<string> m_parentKeys;

		// Token: 0x04000DDE RID: 3550
		private readonly List<string> m_childKeys;

		// Token: 0x04000DDF RID: 3551
		private readonly ReferentialConstraint m_constraint;

		// Token: 0x04000DE0 RID: 3552
		private Dictionary<string, string> m_keyMap;
	}
}
