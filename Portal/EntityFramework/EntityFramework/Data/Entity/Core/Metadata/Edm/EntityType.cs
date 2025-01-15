using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004BB RID: 1211
	public class EntityType : EntityTypeBase
	{
		// Token: 0x06003BDA RID: 15322 RVA: 0x000C66D6 File Offset: 0x000C48D6
		internal EntityType(string name, string namespaceName, DataSpace dataSpace)
			: base(name, namespaceName, dataSpace)
		{
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x000C66F7 File Offset: 0x000C48F7
		internal EntityType(string name, string namespaceName, DataSpace dataSpace, IEnumerable<string> keyMemberNames, IEnumerable<EdmMember> members)
			: base(name, namespaceName, dataSpace)
		{
			if (members != null)
			{
				EntityTypeBase.CheckAndAddMembers(members, this);
			}
			if (keyMemberNames != null)
			{
				base.CheckAndAddKeyMembers(keyMemberNames);
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06003BDC RID: 15324 RVA: 0x000C6730 File Offset: 0x000C4930
		internal IEnumerable<ForeignKeyBuilder> ForeignKeyBuilders
		{
			get
			{
				return this._foreignKeyBuilders;
			}
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x000C6738 File Offset: 0x000C4938
		internal void RemoveForeignKey(ForeignKeyBuilder foreignKeyBuilder)
		{
			Util.ThrowIfReadOnly(this);
			foreignKeyBuilder.SetOwner(null);
			this._foreignKeyBuilders.Remove(foreignKeyBuilder);
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x000C6754 File Offset: 0x000C4954
		internal void AddForeignKey(ForeignKeyBuilder foreignKeyBuilder)
		{
			Util.ThrowIfReadOnly(this);
			foreignKeyBuilder.SetOwner(this);
			this._foreignKeyBuilders.Add(foreignKeyBuilder);
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06003BDF RID: 15327 RVA: 0x000C676F File Offset: 0x000C496F
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EntityType;
			}
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x000C6773 File Offset: 0x000C4973
		internal override void ValidateMemberForAdd(EdmMember member)
		{
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06003BE1 RID: 15329 RVA: 0x000C6775 File Offset: 0x000C4975
		public ReadOnlyMetadataCollection<NavigationProperty> DeclaredNavigationProperties
		{
			get
			{
				return base.GetDeclaredOnlyMembers<NavigationProperty>();
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06003BE2 RID: 15330 RVA: 0x000C6780 File Offset: 0x000C4980
		public ReadOnlyMetadataCollection<NavigationProperty> NavigationProperties
		{
			get
			{
				ReadOnlyMetadataCollection<NavigationProperty> readOnlyMetadataCollection = this._navigationPropertiesCache;
				if (readOnlyMetadataCollection == null)
				{
					object navigationPropertiesCacheLock = this._navigationPropertiesCacheLock;
					lock (navigationPropertiesCacheLock)
					{
						if (this._navigationPropertiesCache == null)
						{
							base.Members.SourceAccessed += this.ResetNavigationProperties;
							this._navigationPropertiesCache = new FilteredReadOnlyMetadataCollection<NavigationProperty, EdmMember>(base.Members, new Predicate<EdmMember>(Helper.IsNavigationProperty));
						}
						readOnlyMetadataCollection = this._navigationPropertiesCache;
					}
				}
				return readOnlyMetadataCollection;
			}
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x000C6808 File Offset: 0x000C4A08
		private void ResetNavigationProperties(object sender, EventArgs e)
		{
			if (this._navigationPropertiesCache != null)
			{
				object navigationPropertiesCacheLock = this._navigationPropertiesCacheLock;
				lock (navigationPropertiesCacheLock)
				{
					if (this._navigationPropertiesCache != null)
					{
						this._navigationPropertiesCache = null;
						base.Members.SourceAccessed -= this.ResetNavigationProperties;
					}
				}
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06003BE4 RID: 15332 RVA: 0x000C6870 File Offset: 0x000C4A70
		public ReadOnlyMetadataCollection<EdmProperty> DeclaredProperties
		{
			get
			{
				return base.GetDeclaredOnlyMembers<EdmProperty>();
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06003BE5 RID: 15333 RVA: 0x000C6878 File Offset: 0x000C4A78
		public ReadOnlyMetadataCollection<EdmMember> DeclaredMembers
		{
			get
			{
				return base.GetDeclaredOnlyMembers<EdmMember>();
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06003BE6 RID: 15334 RVA: 0x000C6880 File Offset: 0x000C4A80
		public virtual ReadOnlyMetadataCollection<EdmProperty> Properties
		{
			get
			{
				if (!base.IsReadOnly)
				{
					return new FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember>(base.Members, new Predicate<EdmMember>(Helper.IsEdmProperty));
				}
				if (this._properties == null)
				{
					Interlocked.CompareExchange<ReadOnlyMetadataCollection<EdmProperty>>(ref this._properties, new FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember>(base.Members, new Predicate<EdmMember>(Helper.IsEdmProperty)), null);
				}
				return this._properties;
			}
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x000C68DF File Offset: 0x000C4ADF
		public RefType GetReferenceType()
		{
			if (this._referenceType == null)
			{
				Interlocked.CompareExchange<RefType>(ref this._referenceType, new RefType(this), null);
			}
			return this._referenceType;
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x000C6904 File Offset: 0x000C4B04
		internal RowType GetKeyRowType()
		{
			if (this._keyRow == null)
			{
				List<EdmProperty> list = new List<EdmProperty>(this.KeyMembers.Count);
				list.AddRange(this.KeyMembers.Select((EdmMember keyMember) => new EdmProperty(keyMember.Name, Helper.GetModelTypeUsage(keyMember))));
				Interlocked.CompareExchange<RowType>(ref this._keyRow, new RowType(list), null);
			}
			return this._keyRow;
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x000C6974 File Offset: 0x000C4B74
		internal bool TryGetNavigationProperty(string relationshipType, string fromName, string toName, out NavigationProperty navigationProperty)
		{
			foreach (NavigationProperty navigationProperty2 in this.NavigationProperties)
			{
				if (navigationProperty2.RelationshipType.FullName == relationshipType && navigationProperty2.FromEndMember.Name == fromName && navigationProperty2.ToEndMember.Name == toName)
				{
					navigationProperty = navigationProperty2;
					return true;
				}
			}
			navigationProperty = null;
			return false;
		}

		// Token: 0x06003BEA RID: 15338 RVA: 0x000C6A08 File Offset: 0x000C4C08
		public static EntityType Create(string name, string namespaceName, DataSpace dataSpace, IEnumerable<string> keyMemberNames, IEnumerable<EdmMember> members, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(namespaceName, "namespaceName");
			EntityType entityType = new EntityType(name, namespaceName, dataSpace, keyMemberNames, members);
			if (metadataProperties != null)
			{
				entityType.AddMetadataProperties(metadataProperties);
			}
			entityType.SetReadOnly();
			return entityType;
		}

		// Token: 0x06003BEB RID: 15339 RVA: 0x000C6A4C File Offset: 0x000C4C4C
		public static EntityType Create(string name, string namespaceName, DataSpace dataSpace, EntityType baseType, IEnumerable<string> keyMemberNames, IEnumerable<EdmMember> members, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(namespaceName, "namespaceName");
			Check.NotNull<EntityType>(baseType, "baseType");
			EntityType entityType = new EntityType(name, namespaceName, dataSpace, keyMemberNames, members)
			{
				BaseType = baseType
			};
			if (metadataProperties != null)
			{
				entityType.AddMetadataProperties(metadataProperties);
			}
			entityType.SetReadOnly();
			return entityType;
		}

		// Token: 0x06003BEC RID: 15340 RVA: 0x000C6AA4 File Offset: 0x000C4CA4
		public void AddNavigationProperty(NavigationProperty property)
		{
			Check.NotNull<NavigationProperty>(property, "property");
			base.AddMember(property, true);
		}

		// Token: 0x0400149D RID: 5277
		private ReadOnlyMetadataCollection<EdmProperty> _properties;

		// Token: 0x0400149E RID: 5278
		private RefType _referenceType;

		// Token: 0x0400149F RID: 5279
		private RowType _keyRow;

		// Token: 0x040014A0 RID: 5280
		private readonly List<ForeignKeyBuilder> _foreignKeyBuilders = new List<ForeignKeyBuilder>();

		// Token: 0x040014A1 RID: 5281
		private readonly object _navigationPropertiesCacheLock = new object();

		// Token: 0x040014A2 RID: 5282
		private ReadOnlyMetadataCollection<NavigationProperty> _navigationPropertiesCache;
	}
}
