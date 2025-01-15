using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x0200075C RID: 1884
	[Table(Name = "dbo.Applications")]
	[DataContract]
	public class Application : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x06003BD8 RID: 15320 RVA: 0x000CC54B File Offset: 0x000CA74B
		public Application()
		{
			this.Initialize();
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06003BD9 RID: 15321 RVA: 0x000CC559 File Offset: 0x000CA759
		// (set) Token: 0x06003BDA RID: 15322 RVA: 0x000CC561 File Offset: 0x000CA761
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

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06003BDB RID: 15323 RVA: 0x000CC584 File Offset: 0x000CA784
		// (set) Token: 0x06003BDC RID: 15324 RVA: 0x000CC58C File Offset: 0x000CA78C
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

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06003BDD RID: 15325 RVA: 0x000CC5B4 File Offset: 0x000CA7B4
		// (set) Token: 0x06003BDE RID: 15326 RVA: 0x000CC5BC File Offset: 0x000CA7BC
		[Column(Storage = "_ProxyCount", DbType = "Int NOT NULL")]
		[DataMember(Order = 3)]
		public int ProxyCount
		{
			get
			{
				return this._ProxyCount;
			}
			set
			{
				if (this._ProxyCount != value)
				{
					this.SendPropertyChanging();
					this._ProxyCount = value;
					this.SendPropertyChanged("ProxyCount");
				}
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06003BDF RID: 15327 RVA: 0x000CC5DF File Offset: 0x000CA7DF
		// (set) Token: 0x06003BE0 RID: 15328 RVA: 0x000CC5E7 File Offset: 0x000CA7E7
		[Column(Storage = "_TracingMask", DbType = "Int NOT NULL")]
		[DataMember(Order = 4)]
		public int TracingMask
		{
			get
			{
				return this._TracingMask;
			}
			set
			{
				if (this._TracingMask != value)
				{
					this.SendPropertyChanging();
					this._TracingMask = value;
					this.SendPropertyChanged("TracingMask");
				}
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06003BE1 RID: 15329 RVA: 0x000CC60A File Offset: 0x000CA80A
		// (set) Token: 0x06003BE2 RID: 15330 RVA: 0x000CC612 File Offset: 0x000CA812
		[Column(Storage = "_TracingMask2", DbType = "Int NOT NULL")]
		[DataMember(Order = 5)]
		public int TracingMask2
		{
			get
			{
				return this._TracingMask2;
			}
			set
			{
				if (this._TracingMask2 != value)
				{
					this.SendPropertyChanging();
					this._TracingMask2 = value;
					this.SendPropertyChanged("TracingMask2");
				}
			}
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06003BE3 RID: 15331 RVA: 0x000CC635 File Offset: 0x000CA835
		// (set) Token: 0x06003BE4 RID: 15332 RVA: 0x000CC63D File Offset: 0x000CA83D
		[Column(Storage = "_ComputerID", DbType = "Int NOT NULL")]
		[DataMember(Order = 6)]
		public int ComputerID
		{
			get
			{
				return this._ComputerID;
			}
			set
			{
				if (this._ComputerID != value)
				{
					if (this._Computers.HasLoadedOrAssignedValue)
					{
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
					this.SendPropertyChanging();
					this._ComputerID = value;
					this.SendPropertyChanged("ComputerID");
				}
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06003BE5 RID: 15333 RVA: 0x000CC673 File Offset: 0x000CA873
		// (set) Token: 0x06003BE6 RID: 15334 RVA: 0x000CC67B File Offset: 0x000CA87B
		[Column(Storage = "_ServiceCLSID", DbType = "NVarChar(38) NOT NULL", CanBeNull = false)]
		[DataMember(Order = 7)]
		public string ServiceCLSID
		{
			get
			{
				return this._ServiceCLSID;
			}
			set
			{
				if (this._ServiceCLSID != value)
				{
					this.SendPropertyChanging();
					this._ServiceCLSID = value;
					this.SendPropertyChanged("ServiceCLSID");
				}
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06003BE7 RID: 15335 RVA: 0x000CC6A3 File Offset: 0x000CA8A3
		// (set) Token: 0x06003BE8 RID: 15336 RVA: 0x000CC6AB File Offset: 0x000CA8AB
		[Column(Storage = "_ControlCLSID", DbType = "NVarChar(38) NOT NULL", CanBeNull = false)]
		[DataMember(Order = 8)]
		public string ControlCLSID
		{
			get
			{
				return this._ControlCLSID;
			}
			set
			{
				if (this._ControlCLSID != value)
				{
					this.SendPropertyChanging();
					this._ControlCLSID = value;
					this.SendPropertyChanged("ControlCLSID");
				}
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06003BE9 RID: 15337 RVA: 0x000CC6D3 File Offset: 0x000CA8D3
		// (set) Token: 0x06003BEA RID: 15338 RVA: 0x000CC6DB File Offset: 0x000CA8DB
		[Column(Storage = "_WorkEntryCount", DbType = "Int NOT NULL")]
		[DataMember(Order = 9)]
		public int WorkEntryCount
		{
			get
			{
				return this._WorkEntryCount;
			}
			set
			{
				if (this._WorkEntryCount != value)
				{
					this.SendPropertyChanging();
					this._WorkEntryCount = value;
					this.SendPropertyChanged("WorkEntryCount");
				}
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06003BEB RID: 15339 RVA: 0x000CC6FE File Offset: 0x000CA8FE
		// (set) Token: 0x06003BEC RID: 15340 RVA: 0x000CC706 File Offset: 0x000CA906
		[Column(Storage = "_MinThreads", DbType = "Int NOT NULL")]
		[DataMember(Order = 10)]
		public int MinThreads
		{
			get
			{
				return this._MinThreads;
			}
			set
			{
				if (this._MinThreads != value)
				{
					this.SendPropertyChanging();
					this._MinThreads = value;
					this.SendPropertyChanged("MinThreads");
				}
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06003BED RID: 15341 RVA: 0x000CC729 File Offset: 0x000CA929
		// (set) Token: 0x06003BEE RID: 15342 RVA: 0x000CC731 File Offset: 0x000CA931
		[Column(Storage = "_ThreadsCleanupTime", DbType = "Int NOT NULL")]
		[DataMember(Order = 11)]
		public int ThreadsCleanupTime
		{
			get
			{
				return this._ThreadsCleanupTime;
			}
			set
			{
				if (this._ThreadsCleanupTime != value)
				{
					this.SendPropertyChanging();
					this._ThreadsCleanupTime = value;
					this.SendPropertyChanged("ThreadsCleanupTime");
				}
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06003BEF RID: 15343 RVA: 0x000CC754 File Offset: 0x000CA954
		// (set) Token: 0x06003BF0 RID: 15344 RVA: 0x000CC75C File Offset: 0x000CA95C
		[Column(Storage = "_StartThreadsCount", DbType = "Int NOT NULL")]
		[DataMember(Order = 12)]
		public int StartThreadsCount
		{
			get
			{
				return this._StartThreadsCount;
			}
			set
			{
				if (this._StartThreadsCount != value)
				{
					this.SendPropertyChanging();
					this._StartThreadsCount = value;
					this.SendPropertyChanged("StartThreadsCount");
				}
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06003BF1 RID: 15345 RVA: 0x000CC77F File Offset: 0x000CA97F
		// (set) Token: 0x06003BF2 RID: 15346 RVA: 0x000CC787 File Offset: 0x000CA987
		[Column(Storage = "_StartThreadsTime", DbType = "Int NOT NULL")]
		[DataMember(Order = 13)]
		public int StartThreadsTime
		{
			get
			{
				return this._StartThreadsTime;
			}
			set
			{
				if (this._StartThreadsTime != value)
				{
					this.SendPropertyChanging();
					this._StartThreadsTime = value;
					this.SendPropertyChanged("StartThreadsTime");
				}
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06003BF3 RID: 15347 RVA: 0x000CC7AA File Offset: 0x000CA9AA
		// (set) Token: 0x06003BF4 RID: 15348 RVA: 0x000CC7B2 File Offset: 0x000CA9B2
		[Column(Storage = "_IncreaseContextCount", DbType = "Int NOT NULL")]
		[DataMember(Order = 14)]
		public int IncreaseContextCount
		{
			get
			{
				return this._IncreaseContextCount;
			}
			set
			{
				if (this._IncreaseContextCount != value)
				{
					this.SendPropertyChanging();
					this._IncreaseContextCount = value;
					this.SendPropertyChanged("IncreaseContextCount");
				}
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06003BF5 RID: 15349 RVA: 0x000CC7D5 File Offset: 0x000CA9D5
		// (set) Token: 0x06003BF6 RID: 15350 RVA: 0x000CC7DD File Offset: 0x000CA9DD
		[Column(Storage = "_IncreaseContextTime", DbType = "Int NOT NULL")]
		[DataMember(Order = 15)]
		public int IncreaseContextTime
		{
			get
			{
				return this._IncreaseContextTime;
			}
			set
			{
				if (this._IncreaseContextTime != value)
				{
					this.SendPropertyChanging();
					this._IncreaseContextTime = value;
					this.SendPropertyChanged("IncreaseContextTime");
				}
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06003BF7 RID: 15351 RVA: 0x000CC800 File Offset: 0x000CAA00
		// (set) Token: 0x06003BF8 RID: 15352 RVA: 0x000CC808 File Offset: 0x000CAA08
		[Column(Storage = "_UseGAC", DbType = "Int NOT NULL")]
		[DataMember(Order = 16)]
		public int UseGAC
		{
			get
			{
				return this._UseGAC;
			}
			set
			{
				if (this._UseGAC != value)
				{
					this.SendPropertyChanging();
					this._UseGAC = value;
					this.SendPropertyChanged("UseGAC");
				}
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06003BF9 RID: 15353 RVA: 0x000CC82B File Offset: 0x000CAA2B
		// (set) Token: 0x06003BFA RID: 15354 RVA: 0x000CC833 File Offset: 0x000CAA33
		[Column(Storage = "_DotNetPath", DbType = "NVarChar(259)")]
		[DataMember(Order = 17)]
		public string DotNetPath
		{
			get
			{
				return this._DotNetPath;
			}
			set
			{
				if (this._DotNetPath != value)
				{
					this.SendPropertyChanging();
					this._DotNetPath = value;
					this.SendPropertyChanged("DotNetPath");
				}
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06003BFB RID: 15355 RVA: 0x000CC85B File Offset: 0x000CAA5B
		// (set) Token: 0x06003BFC RID: 15356 RVA: 0x000CC863 File Offset: 0x000CAA63
		[Column(Storage = "_Comment", DbType = "NVarChar(259)")]
		[DataMember(Order = 18)]
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

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06003BFD RID: 15357 RVA: 0x000CC88B File Offset: 0x000CAA8B
		// (set) Token: 0x06003BFE RID: 15358 RVA: 0x000CC8AA File Offset: 0x000CAAAA
		[Association(Name = "Applications_Listeners FK1", Storage = "_Listeners", ThisKey = "Identity", OtherKey = "ApplicationID", DeleteRule = "NO ACTION")]
		[DataMember(Order = 19, EmitDefaultValue = false)]
		public EntitySet<Listener> Listeners
		{
			get
			{
				if (this.serializing && !this._Listeners.HasLoadedOrAssignedValues)
				{
					return null;
				}
				return this._Listeners;
			}
			set
			{
				this._Listeners.Assign(value);
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06003BFF RID: 15359 RVA: 0x000CC8B8 File Offset: 0x000CAAB8
		// (set) Token: 0x06003C00 RID: 15360 RVA: 0x000CC8C8 File Offset: 0x000CAAC8
		[Association(Name = "Computers_Applications_FK1", Storage = "_Computers", ThisKey = "ComputerID", OtherKey = "Identity", IsForeignKey = true)]
		public Computer Computer
		{
			get
			{
				return this._Computers.Entity;
			}
			set
			{
				Computer entity = this._Computers.Entity;
				if (entity != value || !this._Computers.HasLoadedOrAssignedValue)
				{
					this.SendPropertyChanging();
					if (entity != null)
					{
						this._Computers.Entity = null;
						entity.Applications.Remove(this);
					}
					this._Computers.Entity = value;
					if (value != null)
					{
						value.Applications.Add(this);
						this._ComputerID = value.Identity;
					}
					else
					{
						this._ComputerID = 0;
					}
					this.SendPropertyChanged("Computers");
				}
			}
		}

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06003C01 RID: 15361 RVA: 0x000CC950 File Offset: 0x000CAB50
		// (remove) Token: 0x06003C02 RID: 15362 RVA: 0x000CC988 File Offset: 0x000CAB88
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06003C03 RID: 15363 RVA: 0x000CC9C0 File Offset: 0x000CABC0
		// (remove) Token: 0x06003C04 RID: 15364 RVA: 0x000CC9F8 File Offset: 0x000CABF8
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06003C05 RID: 15365 RVA: 0x000CCA2D File Offset: 0x000CAC2D
		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, Application.emptyChangingEventArgs);
			}
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x000CCA48 File Offset: 0x000CAC48
		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x000CCA64 File Offset: 0x000CAC64
		private void attach_Listeners(Listener entity)
		{
			this.SendPropertyChanging();
			entity.Application = this;
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x000CCA73 File Offset: 0x000CAC73
		private void detach_Listeners(Listener entity)
		{
			this.SendPropertyChanging();
			entity.Application = null;
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x000CCA82 File Offset: 0x000CAC82
		private void Initialize()
		{
			this._Listeners = new EntitySet<Listener>(new Action<Listener>(this.attach_Listeners), new Action<Listener>(this.detach_Listeners));
			this._Computers = default(EntityRef<Computer>);
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x000CCAB3 File Offset: 0x000CACB3
		[OnDeserializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x000CCABB File Offset: 0x000CACBB
		[OnSerializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerializing(StreamingContext context)
		{
			this.serializing = true;
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x000CCAC4 File Offset: 0x000CACC4
		[OnSerialized]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerialized(StreamingContext context)
		{
			this.serializing = false;
		}

		// Token: 0x040023D6 RID: 9174
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

		// Token: 0x040023D7 RID: 9175
		private int _Identity;

		// Token: 0x040023D8 RID: 9176
		private string _Name;

		// Token: 0x040023D9 RID: 9177
		private int _ProxyCount;

		// Token: 0x040023DA RID: 9178
		private int _TracingMask;

		// Token: 0x040023DB RID: 9179
		private int _TracingMask2;

		// Token: 0x040023DC RID: 9180
		private int _ComputerID;

		// Token: 0x040023DD RID: 9181
		private string _ServiceCLSID;

		// Token: 0x040023DE RID: 9182
		private string _ControlCLSID;

		// Token: 0x040023DF RID: 9183
		private int _WorkEntryCount;

		// Token: 0x040023E0 RID: 9184
		private int _MinThreads;

		// Token: 0x040023E1 RID: 9185
		private int _ThreadsCleanupTime;

		// Token: 0x040023E2 RID: 9186
		private int _StartThreadsCount;

		// Token: 0x040023E3 RID: 9187
		private int _StartThreadsTime;

		// Token: 0x040023E4 RID: 9188
		private int _IncreaseContextCount;

		// Token: 0x040023E5 RID: 9189
		private int _IncreaseContextTime;

		// Token: 0x040023E6 RID: 9190
		private int _UseGAC;

		// Token: 0x040023E7 RID: 9191
		private string _DotNetPath;

		// Token: 0x040023E8 RID: 9192
		private string _Comment;

		// Token: 0x040023E9 RID: 9193
		private EntitySet<Listener> _Listeners;

		// Token: 0x040023EA RID: 9194
		private EntityRef<Computer> _Computers;

		// Token: 0x040023EB RID: 9195
		private bool serializing;
	}
}
