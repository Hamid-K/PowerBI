using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x0200075F RID: 1887
	[Table(Name = "dbo.HE Permissions")]
	[DataContract]
	public class HEPermission : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x06003C52 RID: 15442 RVA: 0x000CD387 File Offset: 0x000CB587
		public HEPermission()
		{
			this.Initialize();
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06003C53 RID: 15443 RVA: 0x000CD395 File Offset: 0x000CB595
		// (set) Token: 0x06003C54 RID: 15444 RVA: 0x000CD39D File Offset: 0x000CB59D
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

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06003C55 RID: 15445 RVA: 0x000CD3C0 File Offset: 0x000CB5C0
		// (set) Token: 0x06003C56 RID: 15446 RVA: 0x000CD3C8 File Offset: 0x000CB5C8
		[Column(Storage = "_HEID", DbType = "Int NOT NULL")]
		[DataMember(Order = 2)]
		public int HEID
		{
			get
			{
				return this._HEID;
			}
			set
			{
				if (this._HEID != value)
				{
					if (this._HostEnvironments.HasLoadedOrAssignedValue)
					{
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
					this.SendPropertyChanging();
					this._HEID = value;
					this.SendPropertyChanged("HEID");
				}
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06003C57 RID: 15447 RVA: 0x000CD3FE File Offset: 0x000CB5FE
		// (set) Token: 0x06003C58 RID: 15448 RVA: 0x000CD406 File Offset: 0x000CB606
		[Column(Storage = "_ViewID", DbType = "Int NOT NULL")]
		[DataMember(Order = 3)]
		public int ViewID
		{
			get
			{
				return this._ViewID;
			}
			set
			{
				if (this._ViewID != value)
				{
					if (this._Views.HasLoadedOrAssignedValue)
					{
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
					this.SendPropertyChanging();
					this._ViewID = value;
					this.SendPropertyChanged("ViewID");
				}
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06003C59 RID: 15449 RVA: 0x000CD43C File Offset: 0x000CB63C
		// (set) Token: 0x06003C5A RID: 15450 RVA: 0x000CD444 File Offset: 0x000CB644
		[Column(Storage = "_Comment", DbType = "NVarChar(259)")]
		[DataMember(Order = 4)]
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

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06003C5B RID: 15451 RVA: 0x000CD46C File Offset: 0x000CB66C
		// (set) Token: 0x06003C5C RID: 15452 RVA: 0x000CD47C File Offset: 0x000CB67C
		[Association(Name = "Host Environments_HE Permission_FK1", Storage = "_HostEnvironments", ThisKey = "HEID", OtherKey = "Identity", IsForeignKey = true)]
		public LinqHostEnvironment HostEnvironment
		{
			get
			{
				return this._HostEnvironments.Entity;
			}
			set
			{
				LinqHostEnvironment entity = this._HostEnvironments.Entity;
				if (entity != value || !this._HostEnvironments.HasLoadedOrAssignedValue)
				{
					this.SendPropertyChanging();
					if (entity != null)
					{
						this._HostEnvironments.Entity = null;
						entity.HEPermissions.Remove(this);
					}
					this._HostEnvironments.Entity = value;
					if (value != null)
					{
						value.HEPermissions.Add(this);
						this._HEID = value.Identity;
					}
					else
					{
						this._HEID = 0;
					}
					this.SendPropertyChanged("HostEnvironments");
				}
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06003C5D RID: 15453 RVA: 0x000CD503 File Offset: 0x000CB703
		// (set) Token: 0x06003C5E RID: 15454 RVA: 0x000CD510 File Offset: 0x000CB710
		[Association(Name = "Views_HE Permission_FK1", Storage = "_Views", ThisKey = "ViewID", OtherKey = "Identity", IsForeignKey = true)]
		public View View
		{
			get
			{
				return this._Views.Entity;
			}
			set
			{
				View entity = this._Views.Entity;
				if (entity != value || !this._Views.HasLoadedOrAssignedValue)
				{
					this.SendPropertyChanging();
					if (entity != null)
					{
						this._Views.Entity = null;
						entity.HEPermissions.Remove(this);
					}
					this._Views.Entity = value;
					if (value != null)
					{
						value.HEPermissions.Add(this);
						this._ViewID = value.Identity;
					}
					else
					{
						this._ViewID = 0;
					}
					this.SendPropertyChanged("Views");
				}
			}
		}

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06003C5F RID: 15455 RVA: 0x000CD598 File Offset: 0x000CB798
		// (remove) Token: 0x06003C60 RID: 15456 RVA: 0x000CD5D0 File Offset: 0x000CB7D0
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06003C61 RID: 15457 RVA: 0x000CD608 File Offset: 0x000CB808
		// (remove) Token: 0x06003C62 RID: 15458 RVA: 0x000CD640 File Offset: 0x000CB840
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06003C63 RID: 15459 RVA: 0x000CD675 File Offset: 0x000CB875
		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, HEPermission.emptyChangingEventArgs);
			}
		}

		// Token: 0x06003C64 RID: 15460 RVA: 0x000CD690 File Offset: 0x000CB890
		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06003C65 RID: 15461 RVA: 0x000CD6AC File Offset: 0x000CB8AC
		private void Initialize()
		{
			this._HostEnvironments = default(EntityRef<LinqHostEnvironment>);
			this._Views = default(EntityRef<View>);
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x000CD6C6 File Offset: 0x000CB8C6
		[OnDeserializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x0400240B RID: 9227
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

		// Token: 0x0400240C RID: 9228
		private int _Identity;

		// Token: 0x0400240D RID: 9229
		private int _HEID;

		// Token: 0x0400240E RID: 9230
		private int _ViewID;

		// Token: 0x0400240F RID: 9231
		private string _Comment;

		// Token: 0x04002410 RID: 9232
		private EntityRef<LinqHostEnvironment> _HostEnvironments;

		// Token: 0x04002411 RID: 9233
		private EntityRef<View> _Views;
	}
}
