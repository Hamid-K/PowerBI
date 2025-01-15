using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Threading;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000087 RID: 135
	public class EntitySet : EntitySetBase
	{
		// Token: 0x060009FE RID: 2558 RVA: 0x00017C74 File Offset: 0x00015E74
		internal EntitySet(string name, string schema, string table, string definingQuery, EntityType entityType)
			: base(name, schema, table, definingQuery, entityType)
		{
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00017C83 File Offset: 0x00015E83
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EntitySet;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x00017C87 File Offset: 0x00015E87
		public new EntityType ElementType
		{
			get
			{
				return (EntityType)base.ElementType;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00017C94 File Offset: 0x00015E94
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

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00017CAA File Offset: 0x00015EAA
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

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00017CC0 File Offset: 0x00015EC0
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

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00017CD8 File Offset: 0x00015ED8
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

		// Token: 0x06000A05 RID: 2565 RVA: 0x00017CF0 File Offset: 0x00015EF0
		private void InitializeForeignKeyLists()
		{
			List<Tuple<AssociationSet, ReferentialConstraint>> list = new List<Tuple<AssociationSet, ReferentialConstraint>>();
			List<Tuple<AssociationSet, ReferentialConstraint>> list2 = new List<Tuple<AssociationSet, ReferentialConstraint>>();
			bool flag = false;
			bool flag2 = false;
			foreach (AssociationSet associationSet in MetadataHelper.GetAssociationsForEntitySet(this))
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
			ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> readOnlyCollection = list.AsReadOnly();
			ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> readOnlyCollection2 = list2.AsReadOnly();
			Interlocked.CompareExchange<ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>>>(ref this._foreignKeyDependents, readOnlyCollection, null);
			Interlocked.CompareExchange<ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>>>(ref this._foreignKeyPrincipals, readOnlyCollection2, null);
		}

		// Token: 0x0400081B RID: 2075
		private ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> _foreignKeyDependents;

		// Token: 0x0400081C RID: 2076
		private ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> _foreignKeyPrincipals;

		// Token: 0x0400081D RID: 2077
		private volatile bool _hasForeignKeyRelationships;

		// Token: 0x0400081E RID: 2078
		private volatile bool _hasIndependentRelationships;
	}
}
