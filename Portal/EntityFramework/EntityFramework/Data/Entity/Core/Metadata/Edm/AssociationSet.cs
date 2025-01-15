using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000487 RID: 1159
	public sealed class AssociationSet : RelationshipSet
	{
		// Token: 0x06003985 RID: 14725 RVA: 0x000BD6FC File Offset: 0x000BB8FC
		internal AssociationSet(string name, AssociationType associationType)
			: base(name, null, null, null, associationType)
		{
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06003986 RID: 14726 RVA: 0x000BD719 File Offset: 0x000BB919
		public new AssociationType ElementType
		{
			get
			{
				return (AssociationType)base.ElementType;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06003987 RID: 14727 RVA: 0x000BD726 File Offset: 0x000BB926
		[MetadataProperty(BuiltInTypeKind.AssociationSetEnd, true)]
		public ReadOnlyMetadataCollection<AssociationSetEnd> AssociationSetEnds
		{
			get
			{
				return this._associationSetEnds;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06003988 RID: 14728 RVA: 0x000BD730 File Offset: 0x000BB930
		// (set) Token: 0x06003989 RID: 14729 RVA: 0x000BD754 File Offset: 0x000BB954
		internal EntitySet SourceSet
		{
			get
			{
				AssociationSetEnd associationSetEnd = this.AssociationSetEnds.FirstOrDefault<AssociationSetEnd>();
				if (associationSetEnd == null)
				{
					return null;
				}
				return associationSetEnd.EntitySet;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				AssociationSetEnd associationSetEnd = new AssociationSetEnd(value, this, this.ElementType.SourceEnd);
				if (this.AssociationSetEnds.Count == 0)
				{
					this.AddAssociationSetEnd(associationSetEnd);
					return;
				}
				this.AssociationSetEnds.Source[0] = associationSetEnd;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x0600398A RID: 14730 RVA: 0x000BD7A4 File Offset: 0x000BB9A4
		// (set) Token: 0x0600398B RID: 14731 RVA: 0x000BD7CC File Offset: 0x000BB9CC
		internal EntitySet TargetSet
		{
			get
			{
				AssociationSetEnd associationSetEnd = this.AssociationSetEnds.ElementAtOrDefault(1);
				if (associationSetEnd == null)
				{
					return null;
				}
				return associationSetEnd.EntitySet;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				AssociationSetEnd associationSetEnd = new AssociationSetEnd(value, this, this.ElementType.TargetEnd);
				if (this.AssociationSetEnds.Count == 1)
				{
					this.AddAssociationSetEnd(associationSetEnd);
					return;
				}
				this.AssociationSetEnds.Source[1] = associationSetEnd;
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x0600398C RID: 14732 RVA: 0x000BD81C File Offset: 0x000BBA1C
		internal AssociationEndMember SourceEnd
		{
			get
			{
				AssociationSetEnd associationSetEnd = this.AssociationSetEnds.FirstOrDefault<AssociationSetEnd>();
				if (associationSetEnd == null)
				{
					return null;
				}
				return this.ElementType.KeyMembers.OfType<AssociationEndMember>().SingleOrDefault((AssociationEndMember e) => e.Name == associationSetEnd.Name);
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x0600398D RID: 14733 RVA: 0x000BD86C File Offset: 0x000BBA6C
		internal AssociationEndMember TargetEnd
		{
			get
			{
				AssociationSetEnd associationSetEnd = this.AssociationSetEnds.ElementAtOrDefault(1);
				if (associationSetEnd == null)
				{
					return null;
				}
				return this.ElementType.KeyMembers.OfType<AssociationEndMember>().SingleOrDefault((AssociationEndMember e) => e.Name == associationSetEnd.Name);
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x0600398E RID: 14734 RVA: 0x000BD8BC File Offset: 0x000BBABC
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.AssociationSet;
			}
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x000BD8BF File Offset: 0x000BBABF
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.AssociationSetEnds.Source.SetReadOnly();
			}
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x000BD8E0 File Offset: 0x000BBAE0
		internal void AddAssociationSetEnd(AssociationSetEnd associationSetEnd)
		{
			this.AssociationSetEnds.Source.Add(associationSetEnd);
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x000BD8F4 File Offset: 0x000BBAF4
		public static AssociationSet Create(string name, AssociationType type, EntitySet sourceSet, EntitySet targetSet, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<AssociationType>(type, "type");
			if (!AssociationSet.CheckEntitySetAgainstEndMember(sourceSet, type.SourceEnd) || !AssociationSet.CheckEntitySetAgainstEndMember(targetSet, type.TargetEnd))
			{
				throw new ArgumentException(Strings.AssociationSet_EndEntityTypeMismatch);
			}
			AssociationSet associationSet = new AssociationSet(name, type);
			if (sourceSet != null)
			{
				associationSet.SourceSet = sourceSet;
			}
			if (targetSet != null)
			{
				associationSet.TargetSet = targetSet;
			}
			if (metadataProperties != null)
			{
				associationSet.AddMetadataProperties(metadataProperties);
			}
			associationSet.SetReadOnly();
			return associationSet;
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x000BD96F File Offset: 0x000BBB6F
		private static bool CheckEntitySetAgainstEndMember(EntitySet entitySet, AssociationEndMember endMember)
		{
			return (entitySet == null && endMember == null) || (entitySet != null && endMember != null && entitySet.ElementType == endMember.GetEntityType());
		}

		// Token: 0x0400130F RID: 4879
		private readonly ReadOnlyMetadataCollection<AssociationSetEnd> _associationSetEnds = new ReadOnlyMetadataCollection<AssociationSetEnd>(new MetadataCollection<AssociationSetEnd>());
	}
}
