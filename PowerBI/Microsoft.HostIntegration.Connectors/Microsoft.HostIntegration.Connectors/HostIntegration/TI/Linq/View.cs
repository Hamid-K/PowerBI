using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x02000768 RID: 1896
	[Table(Name = "dbo.Views")]
	[DataContract]
	public class View : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x06003D7E RID: 15742 RVA: 0x000CF532 File Offset: 0x000CD732
		public View()
		{
			this.Initialize();
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06003D7F RID: 15743 RVA: 0x000CF540 File Offset: 0x000CD740
		// (set) Token: 0x06003D80 RID: 15744 RVA: 0x000CF548 File Offset: 0x000CD748
		[Column(Storage = "_Identity", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
		[DataMember(Order = 1)]
		public int Identity
		{
			get
			{
				return this._Identity;
			}
			set
			{
				if (this._Identity != value)
				{
					this.SendPropertyChanging();
					this._Identity = value;
					this.SendPropertyChanged("Identity");
				}
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06003D81 RID: 15745 RVA: 0x000CF56B File Offset: 0x000CD76B
		// (set) Token: 0x06003D82 RID: 15746 RVA: 0x000CF573 File Offset: 0x000CD773
		[Column(Storage = "_Name", DbType = "NVarChar(259) NOT NULL", CanBeNull = false)]
		[DataMember(Order = 2)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if (this._Name != value)
				{
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
				}
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06003D83 RID: 15747 RVA: 0x000CF59B File Offset: 0x000CD79B
		// (set) Token: 0x06003D84 RID: 15748 RVA: 0x000CF5A3 File Offset: 0x000CD7A3
		[Column(Storage = "_ObjectID", DbType = "Int NOT NULL")]
		[DataMember(Order = 3)]
		public int ObjectID
		{
			get
			{
				return this._ObjectID;
			}
			set
			{
				if (this._ObjectID != value)
				{
					if (this._Objects.HasLoadedOrAssignedValue)
					{
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
					this.SendPropertyChanging();
					this._ObjectID = value;
					this.SendPropertyChanged("ObjectID");
				}
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06003D85 RID: 15749 RVA: 0x000CF5D9 File Offset: 0x000CD7D9
		// (set) Token: 0x06003D86 RID: 15750 RVA: 0x000CF5E1 File Offset: 0x000CD7E1
		[Column(Storage = "_LEID", DbType = "Int NOT NULL")]
		[DataMember(Order = 4)]
		public int LEID
		{
			get
			{
				return this._LEID;
			}
			set
			{
				if (this._LEID != value)
				{
					if (this._LocalEnvironments.HasLoadedOrAssignedValue)
					{
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
					this.SendPropertyChanging();
					this._LEID = value;
					this.SendPropertyChanged("LEID");
				}
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06003D87 RID: 15751 RVA: 0x000CF617 File Offset: 0x000CD817
		// (set) Token: 0x06003D88 RID: 15752 RVA: 0x000CF61F File Offset: 0x000CD81F
		[Column(Storage = "_SecurityPolicyID", DbType = "Int NOT NULL")]
		[DataMember(Order = 5)]
		public int SecurityPolicyID
		{
			get
			{
				return this._SecurityPolicyID;
			}
			set
			{
				if (this._SecurityPolicyID != value)
				{
					if (this._SecurityPolicies.HasLoadedOrAssignedValue)
					{
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
					this.SendPropertyChanging();
					this._SecurityPolicyID = value;
					this.SendPropertyChanged("SecurityPolicyID");
				}
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06003D89 RID: 15753 RVA: 0x000CF655 File Offset: 0x000CD855
		// (set) Token: 0x06003D8A RID: 15754 RVA: 0x000CF65D File Offset: 0x000CD85D
		[Column(Storage = "_Comment", DbType = "NVarChar(259)")]
		[DataMember(Order = 6)]
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if (this._Comment != value)
				{
					this.SendPropertyChanging();
					this._Comment = value;
					this.SendPropertyChanged("Comment");
				}
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06003D8B RID: 15755 RVA: 0x000CF685 File Offset: 0x000CD885
		// (set) Token: 0x06003D8C RID: 15756 RVA: 0x000CF694 File Offset: 0x000CD894
		[Association(Name = "LE_Views FK1", Storage = "_LocalEnvironments", ThisKey = "LEID", OtherKey = "Identity", IsForeignKey = true)]
		public LocalEnvironment LocalEnvironment
		{
			get
			{
				return this._LocalEnvironments.Entity;
			}
			set
			{
				LocalEnvironment entity = this._LocalEnvironments.Entity;
				if (entity != value || !this._LocalEnvironments.HasLoadedOrAssignedValue)
				{
					this.SendPropertyChanging();
					if (entity != null)
					{
						this._LocalEnvironments.Entity = null;
						entity.Views.Remove(this);
					}
					this._LocalEnvironments.Entity = value;
					if (value != null)
					{
						value.Views.Add(this);
						this._LEID = value.Identity;
					}
					else
					{
						this._LEID = 0;
					}
					this.SendPropertyChanged("LocalEnvironments");
				}
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06003D8D RID: 15757 RVA: 0x000CF71B File Offset: 0x000CD91B
		// (set) Token: 0x06003D8E RID: 15758 RVA: 0x000CF728 File Offset: 0x000CD928
		[Association(Name = "Objects_Views FK1", Storage = "_Objects", ThisKey = "ObjectID", OtherKey = "Identity", IsForeignKey = true)]
		public Object Object
		{
			get
			{
				return this._Objects.Entity;
			}
			set
			{
				Object entity = this._Objects.Entity;
				if (entity != value || !this._Objects.HasLoadedOrAssignedValue)
				{
					this.SendPropertyChanging();
					if (entity != null)
					{
						this._Objects.Entity = null;
						entity.Views.Remove(this);
					}
					this._Objects.Entity = value;
					if (value != null)
					{
						value.Views.Add(this);
						this._ObjectID = value.Identity;
					}
					else
					{
						this._ObjectID = 0;
					}
					this.SendPropertyChanged("Objects");
				}
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06003D8F RID: 15759 RVA: 0x000CF7AF File Offset: 0x000CD9AF
		// (set) Token: 0x06003D90 RID: 15760 RVA: 0x000CF7CE File Offset: 0x000CD9CE
		[Association(Name = "Views_Determinants_FK1", Storage = "_Determinants", ThisKey = "Identity", OtherKey = "ViewID", DeleteRule = "NO ACTION")]
		[DataMember(Order = 7, EmitDefaultValue = false)]
		public EntitySet<Determinant> Determinants
		{
			get
			{
				if (this.serializing && !this._Determinants.HasLoadedOrAssignedValues)
				{
					return null;
				}
				return this._Determinants;
			}
			set
			{
				this._Determinants.Assign(value);
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06003D91 RID: 15761 RVA: 0x000CF7DC File Offset: 0x000CD9DC
		// (set) Token: 0x06003D92 RID: 15762 RVA: 0x000CF7FB File Offset: 0x000CD9FB
		[Association(Name = "Views_HE Permission_FK1", Storage = "_HEPermissions", ThisKey = "Identity", OtherKey = "ViewID", DeleteRule = "NO ACTION")]
		[DataMember(Order = 8, EmitDefaultValue = false)]
		public EntitySet<HEPermission> HEPermissions
		{
			get
			{
				if (this.serializing && !this._HEPermissions.HasLoadedOrAssignedValues)
				{
					return null;
				}
				return this._HEPermissions;
			}
			set
			{
				this._HEPermissions.Assign(value);
			}
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06003D93 RID: 15763 RVA: 0x000CF809 File Offset: 0x000CDA09
		// (set) Token: 0x06003D94 RID: 15764 RVA: 0x000CF818 File Offset: 0x000CDA18
		[Association(Name = "Views_Security_Policies_FK1", Storage = "_SecurityPolicies", ThisKey = "SecurityPolicyID", OtherKey = "Identity", IsForeignKey = true)]
		public SecurityPolicy SecurityPolicy
		{
			get
			{
				return this._SecurityPolicies.Entity;
			}
			set
			{
				SecurityPolicy entity = this._SecurityPolicies.Entity;
				if (entity != value || !this._SecurityPolicies.HasLoadedOrAssignedValue)
				{
					this.SendPropertyChanging();
					if (entity != null)
					{
						this._SecurityPolicies.Entity = null;
						entity.Views.Remove(this);
					}
					this._SecurityPolicies.Entity = value;
					if (value != null)
					{
						value.Views.Add(this);
						this._SecurityPolicyID = value.Identity;
					}
					else
					{
						this._SecurityPolicyID = 0;
					}
					this.SendPropertyChanged("SecurityPolicies");
				}
			}
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06003D95 RID: 15765 RVA: 0x000CF8A0 File Offset: 0x000CDAA0
		// (remove) Token: 0x06003D96 RID: 15766 RVA: 0x000CF8D8 File Offset: 0x000CDAD8
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06003D97 RID: 15767 RVA: 0x000CF910 File Offset: 0x000CDB10
		// (remove) Token: 0x06003D98 RID: 15768 RVA: 0x000CF948 File Offset: 0x000CDB48
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06003D99 RID: 15769 RVA: 0x000CF97D File Offset: 0x000CDB7D
		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, View.emptyChangingEventArgs);
			}
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x000CF998 File Offset: 0x000CDB98
		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x000CF9B4 File Offset: 0x000CDBB4
		private void attach_Determinants(Determinant entity)
		{
			this.SendPropertyChanging();
			entity.View = this;
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x000CF9C3 File Offset: 0x000CDBC3
		private void detach_Determinants(Determinant entity)
		{
			this.SendPropertyChanging();
			entity.View = null;
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x000CF9D2 File Offset: 0x000CDBD2
		private void attach_HEPermissions(HEPermission entity)
		{
			this.SendPropertyChanging();
			entity.View = this;
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x000CF9E1 File Offset: 0x000CDBE1
		private void detach_HEPermissions(HEPermission entity)
		{
			this.SendPropertyChanging();
			entity.View = null;
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x000CF9F0 File Offset: 0x000CDBF0
		private void Initialize()
		{
			this._LocalEnvironments = default(EntityRef<LocalEnvironment>);
			this._Objects = default(EntityRef<Object>);
			this._Determinants = new EntitySet<Determinant>(new Action<Determinant>(this.attach_Determinants), new Action<Determinant>(this.detach_Determinants));
			this._HEPermissions = new EntitySet<HEPermission>(new Action<HEPermission>(this.attach_HEPermissions), new Action<HEPermission>(this.detach_HEPermissions));
			this._SecurityPolicies = default(EntityRef<SecurityPolicy>);
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x000CFA67 File Offset: 0x000CDC67
		[OnDeserializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x000CFA6F File Offset: 0x000CDC6F
		[OnSerializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerializing(StreamingContext context)
		{
			this.serializing = true;
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x000CFA78 File Offset: 0x000CDC78
		[OnSerialized]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerialized(StreamingContext context)
		{
			this.serializing = false;
		}

		// Token: 0x04002484 RID: 9348
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

		// Token: 0x04002485 RID: 9349
		private int _Identity;

		// Token: 0x04002486 RID: 9350
		private string _Name;

		// Token: 0x04002487 RID: 9351
		private int _ObjectID;

		// Token: 0x04002488 RID: 9352
		private int _LEID;

		// Token: 0x04002489 RID: 9353
		private int _SecurityPolicyID;

		// Token: 0x0400248A RID: 9354
		private string _Comment;

		// Token: 0x0400248B RID: 9355
		private EntityRef<LocalEnvironment> _LocalEnvironments;

		// Token: 0x0400248C RID: 9356
		private EntityRef<Object> _Objects;

		// Token: 0x0400248D RID: 9357
		private EntitySet<Determinant> _Determinants;

		// Token: 0x0400248E RID: 9358
		private EntitySet<HEPermission> _HEPermissions;

		// Token: 0x0400248F RID: 9359
		private EntityRef<SecurityPolicy> _SecurityPolicies;

		// Token: 0x04002490 RID: 9360
		private bool serializing;
	}
}
