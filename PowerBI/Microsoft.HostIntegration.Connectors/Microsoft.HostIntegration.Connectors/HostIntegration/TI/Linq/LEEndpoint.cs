using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x02000761 RID: 1889
	[Table(Name = "dbo.LE Endpoints")]
	[DataContract]
	public class LEEndpoint : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x06003CA2 RID: 15522 RVA: 0x000CDC42 File Offset: 0x000CBE42
		public LEEndpoint()
		{
			this.Initialize();
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06003CA3 RID: 15523 RVA: 0x000CDC50 File Offset: 0x000CBE50
		// (set) Token: 0x06003CA4 RID: 15524 RVA: 0x000CDC58 File Offset: 0x000CBE58
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

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06003CA5 RID: 15525 RVA: 0x000CDC7B File Offset: 0x000CBE7B
		// (set) Token: 0x06003CA6 RID: 15526 RVA: 0x000CDC83 File Offset: 0x000CBE83
		[Column(Storage = "_LEID", DbType = "Int NOT NULL")]
		[DataMember(Order = 2)]
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

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06003CA7 RID: 15527 RVA: 0x000CDCB9 File Offset: 0x000CBEB9
		// (set) Token: 0x06003CA8 RID: 15528 RVA: 0x000CDCC1 File Offset: 0x000CBEC1
		[Column(Storage = "_Number", DbType = "Int NOT NULL")]
		[DataMember(Order = 3)]
		public int Number
		{
			get
			{
				return this._Number;
			}
			set
			{
				if (this._Number != value)
				{
					this.SendPropertyChanging();
					this._Number = value;
					this.SendPropertyChanged("Number");
				}
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06003CA9 RID: 15529 RVA: 0x000CDCE4 File Offset: 0x000CBEE4
		// (set) Token: 0x06003CAA RID: 15530 RVA: 0x000CDCEC File Offset: 0x000CBEEC
		[Column(Storage = "_StringInfo", DbType = "NVarChar(64)")]
		[DataMember(Order = 4)]
		public string StringInfo
		{
			get
			{
				return this._StringInfo;
			}
			set
			{
				if (this._StringInfo != value)
				{
					this.SendPropertyChanging();
					this._StringInfo = value;
					this.SendPropertyChanged("StringInfo");
				}
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06003CAB RID: 15531 RVA: 0x000CDD14 File Offset: 0x000CBF14
		// (set) Token: 0x06003CAC RID: 15532 RVA: 0x000CDD1C File Offset: 0x000CBF1C
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

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06003CAD RID: 15533 RVA: 0x000CDD44 File Offset: 0x000CBF44
		// (set) Token: 0x06003CAE RID: 15534 RVA: 0x000CDD63 File Offset: 0x000CBF63
		[Association(Name = "EndPoints_Determinants_FK", Storage = "_Determinants", ThisKey = "Identity", OtherKey = "EndPointID", DeleteRule = "NO ACTION")]
		[DataMember(Order = 6, EmitDefaultValue = false)]
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

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06003CAF RID: 15535 RVA: 0x000CDD71 File Offset: 0x000CBF71
		// (set) Token: 0x06003CB0 RID: 15536 RVA: 0x000CDD80 File Offset: 0x000CBF80
		[Association(Name = "Local Environments LE Endpoints FK1", Storage = "_LocalEnvironments", ThisKey = "LEID", OtherKey = "Identity", IsForeignKey = true)]
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
						entity.LEEndpoints.Remove(this);
					}
					this._LocalEnvironments.Entity = value;
					if (value != null)
					{
						value.LEEndpoints.Add(this);
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

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06003CB1 RID: 15537 RVA: 0x000CDE08 File Offset: 0x000CC008
		// (remove) Token: 0x06003CB2 RID: 15538 RVA: 0x000CDE40 File Offset: 0x000CC040
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06003CB3 RID: 15539 RVA: 0x000CDE78 File Offset: 0x000CC078
		// (remove) Token: 0x06003CB4 RID: 15540 RVA: 0x000CDEB0 File Offset: 0x000CC0B0
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06003CB5 RID: 15541 RVA: 0x000CDEE5 File Offset: 0x000CC0E5
		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, LEEndpoint.emptyChangingEventArgs);
			}
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x000CDF00 File Offset: 0x000CC100
		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06003CB7 RID: 15543 RVA: 0x000CDF1C File Offset: 0x000CC11C
		private void attach_Determinants(Determinant entity)
		{
			this.SendPropertyChanging();
			entity.LEEndpoint = this;
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x000CDF2B File Offset: 0x000CC12B
		private void detach_Determinants(Determinant entity)
		{
			this.SendPropertyChanging();
			entity.LEEndpoint = null;
		}

		// Token: 0x06003CB9 RID: 15545 RVA: 0x000CDF3A File Offset: 0x000CC13A
		private void Initialize()
		{
			this._Determinants = new EntitySet<Determinant>(new Action<Determinant>(this.attach_Determinants), new Action<Determinant>(this.detach_Determinants));
			this._LocalEnvironments = default(EntityRef<LocalEnvironment>);
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x000CDF6B File Offset: 0x000CC16B
		[OnDeserializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x000CDF73 File Offset: 0x000CC173
		[OnSerializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerializing(StreamingContext context)
		{
			this.serializing = true;
		}

		// Token: 0x06003CBC RID: 15548 RVA: 0x000CDF7C File Offset: 0x000CC17C
		[OnSerialized]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerialized(StreamingContext context)
		{
			this.serializing = false;
		}

		// Token: 0x0400242E RID: 9262
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

		// Token: 0x0400242F RID: 9263
		private int _Identity;

		// Token: 0x04002430 RID: 9264
		private int _LEID;

		// Token: 0x04002431 RID: 9265
		private int _Number;

		// Token: 0x04002432 RID: 9266
		private string _StringInfo;

		// Token: 0x04002433 RID: 9267
		private string _Comment;

		// Token: 0x04002434 RID: 9268
		private EntitySet<Determinant> _Determinants;

		// Token: 0x04002435 RID: 9269
		private EntityRef<LocalEnvironment> _LocalEnvironments;

		// Token: 0x04002436 RID: 9270
		private bool serializing;
	}
}
