using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000A8 RID: 168
	public sealed class ReferentialConstraint : MetadataItem
	{
		// Token: 0x06000B5D RID: 2909 RVA: 0x0001D190 File Offset: 0x0001B390
		internal ReferentialConstraint(RelationshipEndMember fromRole, RelationshipEndMember toRole, IEnumerable<EdmProperty> fromProperties, IEnumerable<EdmProperty> toProperties)
		{
			this._fromRole = EntityUtil.GenericCheckArgumentNull<RelationshipEndMember>(fromRole, "fromRole");
			this._toRole = EntityUtil.GenericCheckArgumentNull<RelationshipEndMember>(toRole, "toRole");
			this._fromProperties = new ReadOnlyMetadataCollection<EdmProperty>(new MetadataCollection<EdmProperty>(EntityUtil.GenericCheckArgumentNull<IEnumerable<EdmProperty>>(fromProperties, "fromProperties")));
			this._toProperties = new ReadOnlyMetadataCollection<EdmProperty>(new MetadataCollection<EdmProperty>(EntityUtil.GenericCheckArgumentNull<IEnumerable<EdmProperty>>(toProperties, "toProperties")));
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0001D1FC File Offset: 0x0001B3FC
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.ReferentialConstraint;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0001D200 File Offset: 0x0001B400
		internal override string Identity
		{
			get
			{
				return this.FromRole.Name + "_" + this.ToRole.Name;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0001D222 File Offset: 0x0001B422
		[MetadataProperty(BuiltInTypeKind.RelationshipEndMember, false)]
		public RelationshipEndMember FromRole
		{
			get
			{
				return this._fromRole;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0001D22A File Offset: 0x0001B42A
		[MetadataProperty(BuiltInTypeKind.RelationshipEndMember, false)]
		public RelationshipEndMember ToRole
		{
			get
			{
				return this._toRole;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0001D232 File Offset: 0x0001B432
		[MetadataProperty(BuiltInTypeKind.EdmProperty, true)]
		public ReadOnlyMetadataCollection<EdmProperty> FromProperties
		{
			get
			{
				return this._fromProperties;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0001D23A File Offset: 0x0001B43A
		[MetadataProperty(BuiltInTypeKind.EdmProperty, true)]
		public ReadOnlyMetadataCollection<EdmProperty> ToProperties
		{
			get
			{
				return this._toProperties;
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0001D242 File Offset: 0x0001B442
		public override string ToString()
		{
			return this.FromRole.Name + "_" + this.ToRole.Name;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0001D264 File Offset: 0x0001B464
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				RelationshipEndMember fromRole = this.FromRole;
				if (fromRole != null)
				{
					fromRole.SetReadOnly();
				}
				RelationshipEndMember toRole = this.ToRole;
				if (toRole != null)
				{
					toRole.SetReadOnly();
				}
				this.FromProperties.Source.SetReadOnly();
				this.ToProperties.Source.SetReadOnly();
			}
		}

		// Token: 0x040008A2 RID: 2210
		private RelationshipEndMember _fromRole;

		// Token: 0x040008A3 RID: 2211
		private RelationshipEndMember _toRole;

		// Token: 0x040008A4 RID: 2212
		private readonly ReadOnlyMetadataCollection<EdmProperty> _fromProperties;

		// Token: 0x040008A5 RID: 2213
		private readonly ReadOnlyMetadataCollection<EdmProperty> _toProperties;
	}
}
