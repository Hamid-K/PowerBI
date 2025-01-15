using System;
using System.ComponentModel;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000476 RID: 1142
	[DataContract(IsReference = true)]
	[Serializable]
	public abstract class EntityObject : StructuralObject, IEntityWithKey, IEntityWithChangeTracker, IEntityWithRelationships
	{
		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060037D5 RID: 14293 RVA: 0x000B6C5F File Offset: 0x000B4E5F
		// (set) Token: 0x060037D6 RID: 14294 RVA: 0x000B6C7A File Offset: 0x000B4E7A
		private IEntityChangeTracker EntityChangeTracker
		{
			get
			{
				if (this._entityChangeTracker == null)
				{
					this._entityChangeTracker = EntityObject._detachedEntityChangeTracker;
				}
				return this._entityChangeTracker;
			}
			set
			{
				this._entityChangeTracker = value;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060037D7 RID: 14295 RVA: 0x000B6C83 File Offset: 0x000B4E83
		[Browsable(false)]
		[XmlIgnore]
		public EntityState EntityState
		{
			get
			{
				return this.EntityChangeTracker.EntityState;
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060037D8 RID: 14296 RVA: 0x000B6C90 File Offset: 0x000B4E90
		// (set) Token: 0x060037D9 RID: 14297 RVA: 0x000B6C98 File Offset: 0x000B4E98
		[Browsable(false)]
		[DataMember]
		public EntityKey EntityKey
		{
			get
			{
				return this._entityKey;
			}
			set
			{
				this.EntityChangeTracker.EntityMemberChanging("-EntityKey-");
				this._entityKey = value;
				this.EntityChangeTracker.EntityMemberChanged("-EntityKey-");
			}
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x000B6CC4 File Offset: 0x000B4EC4
		void IEntityWithChangeTracker.SetChangeTracker(IEntityChangeTracker changeTracker)
		{
			if (changeTracker != null && this.EntityChangeTracker != EntityObject._detachedEntityChangeTracker && changeTracker != this.EntityChangeTracker)
			{
				EntityEntry entityEntry = this.EntityChangeTracker as EntityEntry;
				if (entityEntry == null || !entityEntry.ObjectStateManager.IsDisposed)
				{
					throw new InvalidOperationException(Strings.Entity_EntityCantHaveMultipleChangeTrackers);
				}
			}
			this.EntityChangeTracker = changeTracker;
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060037DB RID: 14299 RVA: 0x000B6D18 File Offset: 0x000B4F18
		RelationshipManager IEntityWithRelationships.RelationshipManager
		{
			get
			{
				if (this._relationships == null)
				{
					this._relationships = RelationshipManager.Create(this);
				}
				return this._relationships;
			}
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x000B6D34 File Offset: 0x000B4F34
		protected sealed override void ReportPropertyChanging(string property)
		{
			Check.NotEmpty(property, "property");
			base.ReportPropertyChanging(property);
			this.EntityChangeTracker.EntityMemberChanging(property);
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000B6D55 File Offset: 0x000B4F55
		protected sealed override void ReportPropertyChanged(string property)
		{
			Check.NotEmpty(property, "property");
			this.EntityChangeTracker.EntityMemberChanged(property);
			base.ReportPropertyChanged(property);
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060037DE RID: 14302 RVA: 0x000B6D76 File Offset: 0x000B4F76
		internal sealed override bool IsChangeTracked
		{
			get
			{
				return this.EntityState != EntityState.Detached;
			}
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x000B6D84 File Offset: 0x000B4F84
		internal sealed override void ReportComplexPropertyChanging(string entityMemberName, ComplexObject complexObject, string complexMemberName)
		{
			this.EntityChangeTracker.EntityComplexMemberChanging(entityMemberName, complexObject, complexMemberName);
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x000B6D94 File Offset: 0x000B4F94
		internal sealed override void ReportComplexPropertyChanged(string entityMemberName, ComplexObject complexObject, string complexMemberName)
		{
			this.EntityChangeTracker.EntityComplexMemberChanged(entityMemberName, complexObject, complexMemberName);
		}

		// Token: 0x040012DB RID: 4827
		private RelationshipManager _relationships;

		// Token: 0x040012DC RID: 4828
		private EntityKey _entityKey;

		// Token: 0x040012DD RID: 4829
		[NonSerialized]
		private IEntityChangeTracker _entityChangeTracker = EntityObject._detachedEntityChangeTracker;

		// Token: 0x040012DE RID: 4830
		[NonSerialized]
		private static readonly EntityObject.DetachedEntityChangeTracker _detachedEntityChangeTracker = new EntityObject.DetachedEntityChangeTracker();

		// Token: 0x02000AB3 RID: 2739
		private class DetachedEntityChangeTracker : IEntityChangeTracker
		{
			// Token: 0x060062AF RID: 25263 RVA: 0x0015694F File Offset: 0x00154B4F
			void IEntityChangeTracker.EntityMemberChanging(string entityMemberName)
			{
			}

			// Token: 0x060062B0 RID: 25264 RVA: 0x00156951 File Offset: 0x00154B51
			void IEntityChangeTracker.EntityMemberChanged(string entityMemberName)
			{
			}

			// Token: 0x060062B1 RID: 25265 RVA: 0x00156953 File Offset: 0x00154B53
			void IEntityChangeTracker.EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexMemberName)
			{
			}

			// Token: 0x060062B2 RID: 25266 RVA: 0x00156955 File Offset: 0x00154B55
			void IEntityChangeTracker.EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexMemberName)
			{
			}

			// Token: 0x170010C2 RID: 4290
			// (get) Token: 0x060062B3 RID: 25267 RVA: 0x00156957 File Offset: 0x00154B57
			EntityState IEntityChangeTracker.EntityState
			{
				get
				{
					return EntityState.Detached;
				}
			}
		}
	}
}
