using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x0200075B RID: 1883
	[Table(Name = "dbo.Affiliated Applications")]
	[DataContract]
	public class AffiliatedApplication : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x06003BC2 RID: 15298 RVA: 0x000CC279 File Offset: 0x000CA479
		public AffiliatedApplication()
		{
			this.Initialize();
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06003BC3 RID: 15299 RVA: 0x000CC287 File Offset: 0x000CA487
		// (set) Token: 0x06003BC4 RID: 15300 RVA: 0x000CC28F File Offset: 0x000CA48F
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

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06003BC5 RID: 15301 RVA: 0x000CC2B2 File Offset: 0x000CA4B2
		// (set) Token: 0x06003BC6 RID: 15302 RVA: 0x000CC2BA File Offset: 0x000CA4BA
		[Column(Storage = "_Name", DbType = "NVarChar(259)")]
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

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06003BC7 RID: 15303 RVA: 0x000CC2E2 File Offset: 0x000CA4E2
		// (set) Token: 0x06003BC8 RID: 15304 RVA: 0x000CC2EA File Offset: 0x000CA4EA
		[Column(Storage = "_PolicyID", DbType = "Int NOT NULL")]
		[DataMember(Order = 3)]
		public int PolicyID
		{
			get
			{
				return this._PolicyID;
			}
			set
			{
				if (this._PolicyID != value)
				{
					if (this._SecurityPolicies.HasLoadedOrAssignedValue)
					{
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
					this.SendPropertyChanging();
					this._PolicyID = value;
					this.SendPropertyChanged("PolicyID");
				}
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06003BC9 RID: 15305 RVA: 0x000CC320 File Offset: 0x000CA520
		// (set) Token: 0x06003BCA RID: 15306 RVA: 0x000CC328 File Offset: 0x000CA528
		[Column(Storage = "_SearchOrder", DbType = "Int NOT NULL")]
		[DataMember(Order = 4)]
		public int SearchOrder
		{
			get
			{
				return this._SearchOrder;
			}
			set
			{
				if (this._SearchOrder != value)
				{
					this.SendPropertyChanging();
					this._SearchOrder = value;
					this.SendPropertyChanged("SearchOrder");
				}
			}
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06003BCB RID: 15307 RVA: 0x000CC34B File Offset: 0x000CA54B
		// (set) Token: 0x06003BCC RID: 15308 RVA: 0x000CC353 File Offset: 0x000CA553
		[Column(Storage = "_Comment", DbType = "NVarChar(259)")]
		[DataMember(Order = 5)]
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

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06003BCD RID: 15309 RVA: 0x000CC37B File Offset: 0x000CA57B
		// (set) Token: 0x06003BCE RID: 15310 RVA: 0x000CC388 File Offset: 0x000CA588
		[Association(Name = "Apps_Policies", Storage = "_SecurityPolicies", ThisKey = "PolicyID", OtherKey = "Identity", IsForeignKey = true)]
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
						entity.AffiliatedApplications.Remove(this);
					}
					this._SecurityPolicies.Entity = value;
					if (value != null)
					{
						value.AffiliatedApplications.Add(this);
						this._PolicyID = value.Identity;
					}
					else
					{
						this._PolicyID = 0;
					}
					this.SendPropertyChanged("SecurityPolicies");
				}
			}
		}

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06003BCF RID: 15311 RVA: 0x000CC410 File Offset: 0x000CA610
		// (remove) Token: 0x06003BD0 RID: 15312 RVA: 0x000CC448 File Offset: 0x000CA648
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06003BD1 RID: 15313 RVA: 0x000CC480 File Offset: 0x000CA680
		// (remove) Token: 0x06003BD2 RID: 15314 RVA: 0x000CC4B8 File Offset: 0x000CA6B8
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06003BD3 RID: 15315 RVA: 0x000CC4ED File Offset: 0x000CA6ED
		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, AffiliatedApplication.emptyChangingEventArgs);
			}
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x000CC508 File Offset: 0x000CA708
		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06003BD5 RID: 15317 RVA: 0x000CC524 File Offset: 0x000CA724
		private void Initialize()
		{
			this._SecurityPolicies = default(EntityRef<SecurityPolicy>);
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x000CC532 File Offset: 0x000CA732
		[OnDeserializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x040023CD RID: 9165
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

		// Token: 0x040023CE RID: 9166
		private int _Identity;

		// Token: 0x040023CF RID: 9167
		private string _Name;

		// Token: 0x040023D0 RID: 9168
		private int _PolicyID;

		// Token: 0x040023D1 RID: 9169
		private int _SearchOrder;

		// Token: 0x040023D2 RID: 9170
		private string _Comment;

		// Token: 0x040023D3 RID: 9171
		private EntityRef<SecurityPolicy> _SecurityPolicies;
	}
}
