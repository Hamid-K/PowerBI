using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000576 RID: 1398
	internal class BasicCellRelation : CellRelation
	{
		// Token: 0x060043D0 RID: 17360 RVA: 0x000ECE32 File Offset: 0x000EB032
		internal BasicCellRelation(CellQuery cellQuery, ViewCellRelation viewCellRelation, IEnumerable<MemberProjectedSlot> slots)
			: base(viewCellRelation.CellNumber)
		{
			this.m_cellQuery = cellQuery;
			this.m_slots = new List<MemberProjectedSlot>(slots);
			this.m_viewCellRelation = viewCellRelation;
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x060043D1 RID: 17361 RVA: 0x000ECE5A File Offset: 0x000EB05A
		internal ViewCellRelation ViewCellRelation
		{
			get
			{
				return this.m_viewCellRelation;
			}
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x000ECE62 File Offset: 0x000EB062
		internal void PopulateKeyConstraints(SchemaConstraints<BasicKeyConstraint> constraints)
		{
			if (this.m_cellQuery.Extent is EntitySet)
			{
				this.PopulateKeyConstraintsForEntitySet(constraints);
				return;
			}
			this.PopulateKeyConstraintsForRelationshipSet(constraints);
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x000ECE88 File Offset: 0x000EB088
		private void PopulateKeyConstraintsForEntitySet(SchemaConstraints<BasicKeyConstraint> constraints)
		{
			MemberPath memberPath = new MemberPath(this.m_cellQuery.Extent);
			EntityType entityType = (EntityType)this.m_cellQuery.Extent.ElementType;
			List<ExtentKey> keysForEntityType = ExtentKey.GetKeysForEntityType(memberPath, entityType);
			this.AddKeyConstraints(keysForEntityType, constraints);
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x000ECECC File Offset: 0x000EB0CC
		private void PopulateKeyConstraintsForRelationshipSet(SchemaConstraints<BasicKeyConstraint> constraints)
		{
			AssociationSet associationSet = this.m_cellQuery.Extent as AssociationSet;
			Set<MemberPath> set = new Set<MemberPath>(MemberPath.EqualityComparer);
			bool flag = false;
			foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
			{
				AssociationEndMember correspondingAssociationEndMember = associationSetEnd.CorrespondingAssociationEndMember;
				List<ExtentKey> keysForEntityType = ExtentKey.GetKeysForEntityType(new MemberPath(associationSet, correspondingAssociationEndMember), associationSetEnd.EntitySet.ElementType);
				if (MetadataHelper.DoesEndFormKey(associationSet, correspondingAssociationEndMember))
				{
					this.AddKeyConstraints(keysForEntityType, constraints);
					flag = true;
				}
				set.AddRange(keysForEntityType[0].KeyFields);
			}
			if (!flag)
			{
				ExtentKey extentKey = new ExtentKey(set);
				ExtentKey[] array = new ExtentKey[] { extentKey };
				this.AddKeyConstraints(array, constraints);
			}
		}

		// Token: 0x060043D5 RID: 17365 RVA: 0x000ECFA4 File Offset: 0x000EB1A4
		private void AddKeyConstraints(IEnumerable<ExtentKey> keys, SchemaConstraints<BasicKeyConstraint> constraints)
		{
			foreach (ExtentKey extentKey in keys)
			{
				List<MemberProjectedSlot> slots = MemberProjectedSlot.GetSlots(this.m_slots, extentKey.KeyFields);
				if (slots != null)
				{
					BasicKeyConstraint basicKeyConstraint = new BasicKeyConstraint(this, slots);
					constraints.Add(basicKeyConstraint);
				}
			}
		}

		// Token: 0x060043D6 RID: 17366 RVA: 0x000ED00C File Offset: 0x000EB20C
		protected override int GetHash()
		{
			return this.m_cellQuery.GetHashCode();
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x000ED019 File Offset: 0x000EB219
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append("BasicRel: ");
			StringUtil.FormatStringBuilder(builder, "{0}", new object[] { this.m_slots[0] });
		}

		// Token: 0x0400187E RID: 6270
		private readonly CellQuery m_cellQuery;

		// Token: 0x0400187F RID: 6271
		private readonly List<MemberProjectedSlot> m_slots;

		// Token: 0x04001880 RID: 6272
		private readonly ViewCellRelation m_viewCellRelation;
	}
}
