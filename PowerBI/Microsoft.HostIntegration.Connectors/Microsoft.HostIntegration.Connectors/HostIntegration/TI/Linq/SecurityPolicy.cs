using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x02000766 RID: 1894
	[Table(Name = "dbo.Security Policies")]
	[DataContract]
	public class SecurityPolicy : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x06003D42 RID: 15682 RVA: 0x000CEF16 File Offset: 0x000CD116
		public SecurityPolicy()
		{
			this.Initialize();
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06003D43 RID: 15683 RVA: 0x000CEF24 File Offset: 0x000CD124
		// (set) Token: 0x06003D44 RID: 15684 RVA: 0x000CEF2C File Offset: 0x000CD12C
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

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06003D45 RID: 15685 RVA: 0x000CEF4F File Offset: 0x000CD14F
		// (set) Token: 0x06003D46 RID: 15686 RVA: 0x000CEF57 File Offset: 0x000CD157
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

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06003D47 RID: 15687 RVA: 0x000CEF7F File Offset: 0x000CD17F
		// (set) Token: 0x06003D48 RID: 15688 RVA: 0x000CEF87 File Offset: 0x000CD187
		[Column(Storage = "_Source", DbType = "Int NOT NULL")]
		[DataMember(Order = 3)]
		public int Source
		{
			get
			{
				return this._Source;
			}
			set
			{
				if (this._Source != value)
				{
					this.SendPropertyChanging();
					this._Source = value;
					this.SendPropertyChanged("Source");
				}
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06003D49 RID: 15689 RVA: 0x000CEFAA File Offset: 0x000CD1AA
		// (set) Token: 0x06003D4A RID: 15690 RVA: 0x000CEFB2 File Offset: 0x000CD1B2
		[Column(Storage = "_UseClientAppCredentials", DbType = "Int NOT NULL")]
		[DataMember(Order = 4)]
		public int UseClientAppCredentials
		{
			get
			{
				return this._UseClientAppCredentials;
			}
			set
			{
				if (this._UseClientAppCredentials != value)
				{
					this.SendPropertyChanging();
					this._UseClientAppCredentials = value;
					this.SendPropertyChanged("UseClientAppCredentials");
				}
			}
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06003D4B RID: 15691 RVA: 0x000CEFD5 File Offset: 0x000CD1D5
		// (set) Token: 0x06003D4C RID: 15692 RVA: 0x000CEFDD File Offset: 0x000CD1DD
		[Column(Storage = "_UseDefaultCredentials", DbType = "Int NOT NULL")]
		[DataMember(Order = 5)]
		public int UseDefaultCredentials
		{
			get
			{
				return this._UseDefaultCredentials;
			}
			set
			{
				if (this._UseDefaultCredentials != value)
				{
					this.SendPropertyChanging();
					this._UseDefaultCredentials = value;
					this.SendPropertyChanged("UseDefaultCredentials");
				}
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06003D4D RID: 15693 RVA: 0x000CF000 File Offset: 0x000CD200
		// (set) Token: 0x06003D4E RID: 15694 RVA: 0x000CF008 File Offset: 0x000CD208
		[Column(Storage = "_DefaulUserApp", DbType = "NVarChar(259)")]
		[DataMember(Order = 6)]
		public string DefaulUserApp
		{
			get
			{
				return this._DefaulUserApp;
			}
			set
			{
				if (this._DefaulUserApp != value)
				{
					this.SendPropertyChanging();
					this._DefaulUserApp = value;
					this.SendPropertyChanged("DefaulUserApp");
				}
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06003D4F RID: 15695 RVA: 0x000CF030 File Offset: 0x000CD230
		// (set) Token: 0x06003D50 RID: 15696 RVA: 0x000CF038 File Offset: 0x000CD238
		[Column(Storage = "_DefaulUserID", DbType = "NVarChar(259)")]
		[DataMember(Order = 7)]
		public string DefaulUserID
		{
			get
			{
				return this._DefaulUserID;
			}
			set
			{
				if (this._DefaulUserID != value)
				{
					this.SendPropertyChanging();
					this._DefaulUserID = value;
					this.SendPropertyChanged("DefaulUserID");
				}
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06003D51 RID: 15697 RVA: 0x000CF060 File Offset: 0x000CD260
		// (set) Token: 0x06003D52 RID: 15698 RVA: 0x000CF068 File Offset: 0x000CD268
		[Column(Storage = "_Comment", DbType = "NVarChar(259)")]
		[DataMember(Order = 8)]
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

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06003D53 RID: 15699 RVA: 0x000CF090 File Offset: 0x000CD290
		// (set) Token: 0x06003D54 RID: 15700 RVA: 0x000CF0AF File Offset: 0x000CD2AF
		[Association(Name = "Apps_Policies", Storage = "_AffiliatedApplications", ThisKey = "Identity", OtherKey = "PolicyID", DeleteRule = "NO ACTION")]
		[DataMember(Order = 9, EmitDefaultValue = false)]
		public EntitySet<AffiliatedApplication> AffiliatedApplications
		{
			get
			{
				if (this.serializing && !this._AffiliatedApplications.HasLoadedOrAssignedValues)
				{
					return null;
				}
				return this._AffiliatedApplications;
			}
			set
			{
				this._AffiliatedApplications.Assign(value);
			}
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06003D55 RID: 15701 RVA: 0x000CF0BD File Offset: 0x000CD2BD
		// (set) Token: 0x06003D56 RID: 15702 RVA: 0x000CF0DC File Offset: 0x000CD2DC
		[Association(Name = "Views_Security_Policies_FK1", Storage = "_Views", ThisKey = "Identity", OtherKey = "SecurityPolicyID", DeleteRule = "NO ACTION")]
		[DataMember(Order = 10, EmitDefaultValue = false)]
		public EntitySet<View> Views
		{
			get
			{
				if (this.serializing && !this._Views.HasLoadedOrAssignedValues)
				{
					return null;
				}
				return this._Views;
			}
			set
			{
				this._Views.Assign(value);
			}
		}

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06003D57 RID: 15703 RVA: 0x000CF0EC File Offset: 0x000CD2EC
		// (remove) Token: 0x06003D58 RID: 15704 RVA: 0x000CF124 File Offset: 0x000CD324
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06003D59 RID: 15705 RVA: 0x000CF15C File Offset: 0x000CD35C
		// (remove) Token: 0x06003D5A RID: 15706 RVA: 0x000CF194 File Offset: 0x000CD394
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06003D5B RID: 15707 RVA: 0x000CF1C9 File Offset: 0x000CD3C9
		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, SecurityPolicy.emptyChangingEventArgs);
			}
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x000CF1E4 File Offset: 0x000CD3E4
		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x000CF200 File Offset: 0x000CD400
		private void attach_AffiliatedApplications(AffiliatedApplication entity)
		{
			this.SendPropertyChanging();
			entity.SecurityPolicy = this;
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x000CF20F File Offset: 0x000CD40F
		private void detach_AffiliatedApplications(AffiliatedApplication entity)
		{
			this.SendPropertyChanging();
			entity.SecurityPolicy = null;
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x000CF21E File Offset: 0x000CD41E
		private void attach_Views(View entity)
		{
			this.SendPropertyChanging();
			entity.SecurityPolicy = this;
		}

		// Token: 0x06003D60 RID: 15712 RVA: 0x000CF22D File Offset: 0x000CD42D
		private void detach_Views(View entity)
		{
			this.SendPropertyChanging();
			entity.SecurityPolicy = null;
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x000CF23C File Offset: 0x000CD43C
		private void Initialize()
		{
			this._AffiliatedApplications = new EntitySet<AffiliatedApplication>(new Action<AffiliatedApplication>(this.attach_AffiliatedApplications), new Action<AffiliatedApplication>(this.detach_AffiliatedApplications));
			this._Views = new EntitySet<View>(new Action<View>(this.attach_Views), new Action<View>(this.detach_Views));
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x000CF28F File Offset: 0x000CD48F
		[OnDeserializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x000CF297 File Offset: 0x000CD497
		[OnSerializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerializing(StreamingContext context)
		{
			this.serializing = true;
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x000CF2A0 File Offset: 0x000CD4A0
		[OnSerialized]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerialized(StreamingContext context)
		{
			this.serializing = false;
		}

		// Token: 0x0400246D RID: 9325
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

		// Token: 0x0400246E RID: 9326
		private int _Identity;

		// Token: 0x0400246F RID: 9327
		private string _Name;

		// Token: 0x04002470 RID: 9328
		private int _Source;

		// Token: 0x04002471 RID: 9329
		private int _UseClientAppCredentials;

		// Token: 0x04002472 RID: 9330
		private int _UseDefaultCredentials;

		// Token: 0x04002473 RID: 9331
		private string _DefaulUserApp;

		// Token: 0x04002474 RID: 9332
		private string _DefaulUserID;

		// Token: 0x04002475 RID: 9333
		private string _Comment;

		// Token: 0x04002476 RID: 9334
		private EntitySet<AffiliatedApplication> _AffiliatedApplications;

		// Token: 0x04002477 RID: 9335
		private EntitySet<View> _Views;

		// Token: 0x04002478 RID: 9336
		private bool serializing;
	}
}
