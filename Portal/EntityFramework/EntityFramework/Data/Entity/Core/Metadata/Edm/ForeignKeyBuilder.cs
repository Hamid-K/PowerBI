using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C6 RID: 1222
	internal class ForeignKeyBuilder : MetadataItem, INamedDataModelItem
	{
		// Token: 0x06003C58 RID: 15448 RVA: 0x000C8372 File Offset: 0x000C6572
		internal ForeignKeyBuilder()
		{
		}

		// Token: 0x06003C59 RID: 15449 RVA: 0x000C837C File Offset: 0x000C657C
		public ForeignKeyBuilder(EdmModel database, string name)
		{
			Check.NotNull<EdmModel>(database, "database");
			this._database = database;
			this._associationType = new AssociationType(name, "CodeFirstDatabaseSchema", true, DataSpace.SSpace);
			this._associationSet = new AssociationSet(this._associationType.Name, this._associationType);
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06003C5A RID: 15450 RVA: 0x000C83D1 File Offset: 0x000C65D1
		// (set) Token: 0x06003C5B RID: 15451 RVA: 0x000C83DE File Offset: 0x000C65DE
		public string Name
		{
			get
			{
				return this._associationType.Name;
			}
			set
			{
				this._associationType.Name = value;
				this._associationSet.Name = value;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06003C5C RID: 15452 RVA: 0x000C83F8 File Offset: 0x000C65F8
		// (set) Token: 0x06003C5D RID: 15453 RVA: 0x000C840C File Offset: 0x000C660C
		public virtual EntityType PrincipalTable
		{
			get
			{
				return this._associationType.SourceEnd.GetEntityType();
			}
			set
			{
				Check.NotNull<EntityType>(value, "value");
				Util.ThrowIfReadOnly(this);
				this._associationType.SourceEnd = new AssociationEndMember(value.Name, value);
				this._associationSet.SourceSet = this._database.GetEntitySet(value);
				if (this._associationType.TargetEnd != null && value.Name == this._associationType.TargetEnd.Name)
				{
					this._associationType.TargetEnd.Name = value.Name + "Self";
				}
			}
		}

		// Token: 0x06003C5E RID: 15454 RVA: 0x000C84A4 File Offset: 0x000C66A4
		public virtual void SetOwner(EntityType owner)
		{
			Util.ThrowIfReadOnly(this);
			if (owner == null)
			{
				this._database.RemoveAssociationType(this._associationType);
				return;
			}
			this._associationType.TargetEnd = new AssociationEndMember((owner != this.PrincipalTable) ? owner.Name : (owner.Name + "Self"), owner);
			this._associationSet.TargetSet = this._database.GetEntitySet(owner);
			if (!this._database.AssociationTypes.Contains(this._associationType))
			{
				this._database.AddAssociationType(this._associationType);
				this._database.AddAssociationSet(this._associationSet);
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06003C5F RID: 15455 RVA: 0x000C8550 File Offset: 0x000C6750
		// (set) Token: 0x06003C60 RID: 15456 RVA: 0x000C8584 File Offset: 0x000C6784
		public virtual IEnumerable<EdmProperty> DependentColumns
		{
			get
			{
				if (this._associationType.Constraint == null)
				{
					return Enumerable.Empty<EdmProperty>();
				}
				return this._associationType.Constraint.ToProperties;
			}
			set
			{
				Check.NotNull<IEnumerable<EdmProperty>>(value, "value");
				Util.ThrowIfReadOnly(this);
				this._associationType.Constraint = new ReferentialConstraint(this._associationType.SourceEnd, this._associationType.TargetEnd, this.PrincipalTable.KeyProperties, value);
				this.SetMultiplicities();
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06003C61 RID: 15457 RVA: 0x000C85DB File Offset: 0x000C67DB
		// (set) Token: 0x06003C62 RID: 15458 RVA: 0x000C85FC File Offset: 0x000C67FC
		public OperationAction DeleteAction
		{
			get
			{
				if (this._associationType.SourceEnd == null)
				{
					return OperationAction.None;
				}
				return this._associationType.SourceEnd.DeleteBehavior;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this._associationType.SourceEnd.DeleteBehavior = value;
			}
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x000C8618 File Offset: 0x000C6818
		private void SetMultiplicities()
		{
			this._associationType.SourceEnd.RelationshipMultiplicity = RelationshipMultiplicity.ZeroOrOne;
			this._associationType.TargetEnd.RelationshipMultiplicity = RelationshipMultiplicity.Many;
			EntityType dependentTable = this._associationType.TargetEnd.GetEntityType();
			List<EdmProperty> list = dependentTable.KeyProperties.Where((EdmProperty key) => dependentTable.DeclaredMembers.Contains(key)).ToList<EdmProperty>();
			if (list.Count == this.DependentColumns.Count<EdmProperty>() && list.All(new Func<EdmProperty, bool>(this.DependentColumns.Contains<EdmProperty>)))
			{
				this._associationType.SourceEnd.RelationshipMultiplicity = RelationshipMultiplicity.One;
				this._associationType.TargetEnd.RelationshipMultiplicity = RelationshipMultiplicity.ZeroOrOne;
				return;
			}
			if (!this.DependentColumns.Any((EdmProperty p) => p.Nullable))
			{
				this._associationType.SourceEnd.RelationshipMultiplicity = RelationshipMultiplicity.One;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06003C64 RID: 15460 RVA: 0x000C8711 File Offset: 0x000C6911
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06003C65 RID: 15461 RVA: 0x000C8718 File Offset: 0x000C6918
		string INamedDataModelItem.Identity
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06003C66 RID: 15462 RVA: 0x000C8720 File Offset: 0x000C6920
		internal override string Identity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x040014CA RID: 5322
		private const string SelfRefSuffix = "Self";

		// Token: 0x040014CB RID: 5323
		private readonly EdmModel _database;

		// Token: 0x040014CC RID: 5324
		private readonly AssociationType _associationType;

		// Token: 0x040014CD RID: 5325
		private readonly AssociationSet _associationSet;
	}
}
