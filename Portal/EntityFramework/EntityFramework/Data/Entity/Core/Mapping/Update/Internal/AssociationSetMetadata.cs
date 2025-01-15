using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005C0 RID: 1472
	internal sealed class AssociationSetMetadata
	{
		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06004743 RID: 18243 RVA: 0x000FBC94 File Offset: 0x000F9E94
		internal bool HasEnds
		{
			get
			{
				return 0 < this.RequiredEnds.Count || 0 < this.OptionalEnds.Count || 0 < this.IncludedValueEnds.Count;
			}
		}

		// Token: 0x06004744 RID: 18244 RVA: 0x000FBCC4 File Offset: 0x000F9EC4
		internal AssociationSetMetadata(Set<EntitySet> affectedTables, AssociationSet associationSet, MetadataWorkspace workspace)
		{
			bool flag = 1 < affectedTables.Count;
			ReadOnlyMetadataCollection<AssociationSetEnd> associationSetEnds = associationSet.AssociationSetEnds;
			foreach (EntitySet entitySet in affectedTables)
			{
				foreach (EntitySet entitySet2 in MetadataHelper.GetInfluencingEntitySetsForTable(entitySet, workspace))
				{
					foreach (AssociationSetEnd associationSetEnd in associationSetEnds)
					{
						if (associationSetEnd.EntitySet.EdmEquals(entitySet2))
						{
							if (flag)
							{
								AssociationSetMetadata.AddEnd(ref this.RequiredEnds, associationSetEnd.CorrespondingAssociationEndMember);
							}
							else if (this.RequiredEnds == null || !this.RequiredEnds.Contains(associationSetEnd.CorrespondingAssociationEndMember))
							{
								AssociationSetMetadata.AddEnd(ref this.OptionalEnds, associationSetEnd.CorrespondingAssociationEndMember);
							}
						}
					}
				}
			}
			AssociationSetMetadata.FixSet(ref this.RequiredEnds);
			AssociationSetMetadata.FixSet(ref this.OptionalEnds);
			foreach (ReferentialConstraint referentialConstraint in associationSet.ElementType.ReferentialConstraints)
			{
				AssociationEndMember associationEndMember = (AssociationEndMember)referentialConstraint.FromRole;
				if (!this.RequiredEnds.Contains(associationEndMember) && !this.OptionalEnds.Contains(associationEndMember))
				{
					AssociationSetMetadata.AddEnd(ref this.IncludedValueEnds, associationEndMember);
				}
			}
			AssociationSetMetadata.FixSet(ref this.IncludedValueEnds);
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x000FBE88 File Offset: 0x000FA088
		internal AssociationSetMetadata(IEnumerable<AssociationEndMember> requiredEnds)
		{
			if (requiredEnds.Any<AssociationEndMember>())
			{
				this.RequiredEnds = new Set<AssociationEndMember>(requiredEnds);
			}
			AssociationSetMetadata.FixSet(ref this.RequiredEnds);
			AssociationSetMetadata.FixSet(ref this.OptionalEnds);
			AssociationSetMetadata.FixSet(ref this.IncludedValueEnds);
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x000FBEC5 File Offset: 0x000FA0C5
		private static void AddEnd(ref Set<AssociationEndMember> set, AssociationEndMember element)
		{
			if (set == null)
			{
				set = new Set<AssociationEndMember>();
			}
			set.Add(element);
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x000FBEDA File Offset: 0x000FA0DA
		private static void FixSet(ref Set<AssociationEndMember> set)
		{
			if (set == null)
			{
				set = Set<AssociationEndMember>.Empty;
				return;
			}
			set.MakeReadOnly();
		}

		// Token: 0x0400194B RID: 6475
		internal readonly Set<AssociationEndMember> RequiredEnds;

		// Token: 0x0400194C RID: 6476
		internal readonly Set<AssociationEndMember> OptionalEnds;

		// Token: 0x0400194D RID: 6477
		internal readonly Set<AssociationEndMember> IncludedValueEnds;
	}
}
