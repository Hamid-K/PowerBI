using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200042E RID: 1070
	internal sealed class RelationshipWrapper : IEquatable<RelationshipWrapper>
	{
		// Token: 0x06003410 RID: 13328 RVA: 0x000A812E File Offset: 0x000A632E
		internal RelationshipWrapper(AssociationSet extent, EntityKey key)
		{
			this.AssociationSet = extent;
			this.Key0 = key;
			this.Key1 = key;
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x000A814B File Offset: 0x000A634B
		internal RelationshipWrapper(RelationshipWrapper wrapper, int ordinal, EntityKey key)
		{
			this.AssociationSet = wrapper.AssociationSet;
			this.Key0 = ((ordinal == 0) ? key : wrapper.Key0);
			this.Key1 = ((ordinal == 0) ? wrapper.Key1 : key);
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x000A8183 File Offset: 0x000A6383
		internal RelationshipWrapper(AssociationSet extent, KeyValuePair<string, EntityKey> roleAndKey1, KeyValuePair<string, EntityKey> roleAndKey2)
			: this(extent, roleAndKey1.Key, roleAndKey1.Value, roleAndKey2.Key, roleAndKey2.Value)
		{
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x000A81A8 File Offset: 0x000A63A8
		internal RelationshipWrapper(AssociationSet extent, string role0, EntityKey key0, string role1, EntityKey key1)
		{
			this.AssociationSet = extent;
			if (extent.ElementType.AssociationEndMembers[0].Name == role0)
			{
				this.Key0 = key0;
				this.Key1 = key1;
				return;
			}
			this.Key0 = key1;
			this.Key1 = key0;
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06003414 RID: 13332 RVA: 0x000A81FF File Offset: 0x000A63FF
		internal ReadOnlyMetadataCollection<AssociationEndMember> AssociationEndMembers
		{
			get
			{
				return this.AssociationSet.ElementType.AssociationEndMembers;
			}
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x000A8211 File Offset: 0x000A6411
		internal AssociationEndMember GetAssociationEndMember(EntityKey key)
		{
			return this.AssociationEndMembers[(this.Key0 != key) ? 1 : 0];
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x000A8230 File Offset: 0x000A6430
		internal EntityKey GetOtherEntityKey(EntityKey key)
		{
			if (this.Key0 == key)
			{
				return this.Key1;
			}
			if (!(this.Key1 == key))
			{
				return null;
			}
			return this.Key0;
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x000A825D File Offset: 0x000A645D
		internal EntityKey GetEntityKey(int ordinal)
		{
			if (ordinal == 0)
			{
				return this.Key0;
			}
			if (ordinal != 1)
			{
				throw new ArgumentOutOfRangeException("ordinal");
			}
			return this.Key1;
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000A8280 File Offset: 0x000A6480
		public override int GetHashCode()
		{
			return this.AssociationSet.Name.GetHashCode() ^ (this.Key0.GetHashCode() + this.Key1.GetHashCode());
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000A82AA File Offset: 0x000A64AA
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RelationshipWrapper);
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000A82B8 File Offset: 0x000A64B8
		public bool Equals(RelationshipWrapper wrapper)
		{
			return this == wrapper || (wrapper != null && this.AssociationSet == wrapper.AssociationSet && this.Key0.Equals(wrapper.Key0) && this.Key1.Equals(wrapper.Key1));
		}

		// Token: 0x040010CC RID: 4300
		internal readonly AssociationSet AssociationSet;

		// Token: 0x040010CD RID: 4301
		internal readonly EntityKey Key0;

		// Token: 0x040010CE RID: 4302
		internal readonly EntityKey Key1;
	}
}
