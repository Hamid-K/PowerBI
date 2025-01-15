using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Utilities;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004B8 RID: 1208
	public class EntitySet : EntitySetBase
	{
		// Token: 0x06003BB4 RID: 15284 RVA: 0x000C6270 File Offset: 0x000C4470
		internal EntitySet()
		{
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x000C6278 File Offset: 0x000C4478
		internal EntitySet(string name, string schema, string table, string definingQuery, EntityType entityType)
			: base(name, schema, table, definingQuery, entityType)
		{
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x000C6287 File Offset: 0x000C4487
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EntitySet;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06003BB7 RID: 15287 RVA: 0x000C628B File Offset: 0x000C448B
		public new virtual EntityType ElementType
		{
			get
			{
				return (EntityType)base.ElementType;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06003BB8 RID: 15288 RVA: 0x000C6298 File Offset: 0x000C4498
		internal ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> ForeignKeyDependents
		{
			get
			{
				if (this._foreignKeyDependents == null)
				{
					this.InitializeForeignKeyLists();
				}
				return this._foreignKeyDependents;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06003BB9 RID: 15289 RVA: 0x000C62AE File Offset: 0x000C44AE
		internal ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> ForeignKeyPrincipals
		{
			get
			{
				if (this._foreignKeyPrincipals == null)
				{
					this.InitializeForeignKeyLists();
				}
				return this._foreignKeyPrincipals;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x000C62C4 File Offset: 0x000C44C4
		internal ReadOnlyCollection<AssociationSet> AssociationSets
		{
			get
			{
				if (this._foreignKeyPrincipals == null)
				{
					this.InitializeForeignKeyLists();
				}
				return this._associationSets;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06003BBB RID: 15291 RVA: 0x000C62DA File Offset: 0x000C44DA
		internal bool HasForeignKeyRelationships
		{
			get
			{
				if (this._foreignKeyPrincipals == null)
				{
					this.InitializeForeignKeyLists();
				}
				return this._hasForeignKeyRelationships;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x000C62F2 File Offset: 0x000C44F2
		internal bool HasIndependentRelationships
		{
			get
			{
				if (this._foreignKeyPrincipals == null)
				{
					this.InitializeForeignKeyLists();
				}
				return this._hasIndependentRelationships;
			}
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x000C630C File Offset: 0x000C450C
		private void InitializeForeignKeyLists()
		{
			List<Tuple<AssociationSet, ReferentialConstraint>> list = new List<Tuple<AssociationSet, ReferentialConstraint>>();
			List<Tuple<AssociationSet, ReferentialConstraint>> list2 = new List<Tuple<AssociationSet, ReferentialConstraint>>();
			bool flag = false;
			bool flag2 = false;
			ReadOnlyCollection<AssociationSet> readOnlyCollection = new ReadOnlyCollection<AssociationSet>(MetadataHelper.GetAssociationsForEntitySet(this));
			foreach (AssociationSet associationSet in readOnlyCollection)
			{
				if (associationSet.ElementType.IsForeignKey)
				{
					flag = true;
					ReferentialConstraint referentialConstraint = associationSet.ElementType.ReferentialConstraints[0];
					if (referentialConstraint.ToRole.GetEntityType().IsAssignableFrom(this.ElementType) || this.ElementType.IsAssignableFrom(referentialConstraint.ToRole.GetEntityType()))
					{
						list.Add(new Tuple<AssociationSet, ReferentialConstraint>(associationSet, referentialConstraint));
					}
					if (referentialConstraint.FromRole.GetEntityType().IsAssignableFrom(this.ElementType) || this.ElementType.IsAssignableFrom(referentialConstraint.FromRole.GetEntityType()))
					{
						list2.Add(new Tuple<AssociationSet, ReferentialConstraint>(associationSet, referentialConstraint));
					}
				}
				else
				{
					flag2 = true;
				}
			}
			this._hasForeignKeyRelationships = flag;
			this._hasIndependentRelationships = flag2;
			ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> readOnlyCollection2 = new ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>>(list);
			ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> readOnlyCollection3 = new ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>>(list2);
			Interlocked.CompareExchange<ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>>>(ref this._foreignKeyDependents, readOnlyCollection2, null);
			Interlocked.CompareExchange<ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>>>(ref this._foreignKeyPrincipals, readOnlyCollection3, null);
			Interlocked.CompareExchange<ReadOnlyCollection<AssociationSet>>(ref this._associationSets, readOnlyCollection, null);
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x000C6474 File Offset: 0x000C4674
		public static EntitySet Create(string name, string schema, string table, string definingQuery, EntityType entityType, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<EntityType>(entityType, "entityType");
			EntitySet entitySet = new EntitySet(name, schema, table, definingQuery, entityType);
			if (metadataProperties != null)
			{
				entitySet.AddMetadataProperties(metadataProperties);
			}
			entitySet.SetReadOnly();
			return entitySet;
		}

		// Token: 0x04001491 RID: 5265
		private ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> _foreignKeyDependents;

		// Token: 0x04001492 RID: 5266
		private ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> _foreignKeyPrincipals;

		// Token: 0x04001493 RID: 5267
		private ReadOnlyCollection<AssociationSet> _associationSets;

		// Token: 0x04001494 RID: 5268
		private volatile bool _hasForeignKeyRelationships;

		// Token: 0x04001495 RID: 5269
		private volatile bool _hasIndependentRelationships;
	}
}
