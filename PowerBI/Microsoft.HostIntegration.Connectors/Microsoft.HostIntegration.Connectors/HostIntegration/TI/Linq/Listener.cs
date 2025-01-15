using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x02000762 RID: 1890
	[Table(Name = "dbo.Listeners")]
	[DataContract]
	public class Listener : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x06003CBE RID: 15550 RVA: 0x000CDF96 File Offset: 0x000CC196
		public Listener()
		{
			this.Initialize();
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06003CBF RID: 15551 RVA: 0x000CDFA4 File Offset: 0x000CC1A4
		// (set) Token: 0x06003CC0 RID: 15552 RVA: 0x000CDFAC File Offset: 0x000CC1AC
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

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x000CDFCF File Offset: 0x000CC1CF
		// (set) Token: 0x06003CC2 RID: 15554 RVA: 0x000CDFD7 File Offset: 0x000CC1D7
		[Column(Storage = "_ApplicationID", DbType = "Int NOT NULL")]
		[DataMember(Order = 2)]
		public int ApplicationID
		{
			get
			{
				return this._ApplicationID;
			}
			set
			{
				if (this._ApplicationID != value)
				{
					if (this._Applications.HasLoadedOrAssignedValue)
					{
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
					this.SendPropertyChanging();
					this._ApplicationID = value;
					this.SendPropertyChanged("ApplicationID");
				}
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06003CC3 RID: 15555 RVA: 0x000CE00D File Offset: 0x000CC20D
		// (set) Token: 0x06003CC4 RID: 15556 RVA: 0x000CE015 File Offset: 0x000CC215
		[Column(Storage = "_LEID", DbType = "Int NOT NULL")]
		[DataMember(Order = 3)]
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

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06003CC5 RID: 15557 RVA: 0x000CE04B File Offset: 0x000CC24B
		// (set) Token: 0x06003CC6 RID: 15558 RVA: 0x000CE053 File Offset: 0x000CC253
		[Column(Storage = "_BeginListeningAtStartUp", DbType = "SmallInt NOT NULL")]
		[DataMember(Order = 4)]
		public short BeginListeningAtStartUp
		{
			get
			{
				return this._BeginListeningAtStartUp;
			}
			set
			{
				if (this._BeginListeningAtStartUp != value)
				{
					this.SendPropertyChanging();
					this._BeginListeningAtStartUp = value;
					this.SendPropertyChanged("BeginListeningAtStartUp");
				}
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06003CC7 RID: 15559 RVA: 0x000CE076 File Offset: 0x000CC276
		// (set) Token: 0x06003CC8 RID: 15560 RVA: 0x000CE07E File Offset: 0x000CC27E
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

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x000CE0A6 File Offset: 0x000CC2A6
		// (set) Token: 0x06003CCA RID: 15562 RVA: 0x000CE0B4 File Offset: 0x000CC2B4
		[Association(Name = "Applications_Listeners FK1", Storage = "_Applications", ThisKey = "ApplicationID", OtherKey = "Identity", IsForeignKey = true)]
		public Application Application
		{
			get
			{
				return this._Applications.Entity;
			}
			set
			{
				Application entity = this._Applications.Entity;
				if (entity != value || !this._Applications.HasLoadedOrAssignedValue)
				{
					this.SendPropertyChanging();
					if (entity != null)
					{
						this._Applications.Entity = null;
						entity.Listeners.Remove(this);
					}
					this._Applications.Entity = value;
					if (value != null)
					{
						value.Listeners.Add(this);
						this._ApplicationID = value.Identity;
					}
					else
					{
						this._ApplicationID = 0;
					}
					this.SendPropertyChanged("Applications");
				}
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06003CCB RID: 15563 RVA: 0x000CE13B File Offset: 0x000CC33B
		// (set) Token: 0x06003CCC RID: 15564 RVA: 0x000CE148 File Offset: 0x000CC348
		[Association(Name = "Local Environments_Listeners FK1", Storage = "_LocalEnvironments", ThisKey = "LEID", OtherKey = "Identity", IsForeignKey = true)]
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
						entity.Listeners.Remove(this);
					}
					this._LocalEnvironments.Entity = value;
					if (value != null)
					{
						value.Listeners.Add(this);
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

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06003CCD RID: 15565 RVA: 0x000CE1D0 File Offset: 0x000CC3D0
		// (remove) Token: 0x06003CCE RID: 15566 RVA: 0x000CE208 File Offset: 0x000CC408
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06003CCF RID: 15567 RVA: 0x000CE240 File Offset: 0x000CC440
		// (remove) Token: 0x06003CD0 RID: 15568 RVA: 0x000CE278 File Offset: 0x000CC478
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06003CD1 RID: 15569 RVA: 0x000CE2AD File Offset: 0x000CC4AD
		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, Listener.emptyChangingEventArgs);
			}
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x000CE2C8 File Offset: 0x000CC4C8
		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x000CE2E4 File Offset: 0x000CC4E4
		private void Initialize()
		{
			this._Applications = default(EntityRef<Application>);
			this._LocalEnvironments = default(EntityRef<LocalEnvironment>);
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x000CE2FE File Offset: 0x000CC4FE
		[OnDeserializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x04002439 RID: 9273
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

		// Token: 0x0400243A RID: 9274
		private int _Identity;

		// Token: 0x0400243B RID: 9275
		private int _ApplicationID;

		// Token: 0x0400243C RID: 9276
		private int _LEID;

		// Token: 0x0400243D RID: 9277
		private short _BeginListeningAtStartUp;

		// Token: 0x0400243E RID: 9278
		private string _Comment;

		// Token: 0x0400243F RID: 9279
		private EntityRef<Application> _Applications;

		// Token: 0x04002440 RID: 9280
		private EntityRef<LocalEnvironment> _LocalEnvironments;
	}
}
