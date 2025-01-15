using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x02000763 RID: 1891
	[Table(Name = "dbo.Local Environments")]
	[DataContract]
	public class LocalEnvironment : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x06003CD6 RID: 15574 RVA: 0x000CE317 File Offset: 0x000CC517
		public LocalEnvironment()
		{
			this.Initialize();
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06003CD7 RID: 15575 RVA: 0x000CE325 File Offset: 0x000CC525
		// (set) Token: 0x06003CD8 RID: 15576 RVA: 0x000CE32D File Offset: 0x000CC52D
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

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06003CD9 RID: 15577 RVA: 0x000CE350 File Offset: 0x000CC550
		// (set) Token: 0x06003CDA RID: 15578 RVA: 0x000CE358 File Offset: 0x000CC558
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

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06003CDB RID: 15579 RVA: 0x000CE380 File Offset: 0x000CC580
		// (set) Token: 0x06003CDC RID: 15580 RVA: 0x000CE388 File Offset: 0x000CC588
		[Column(Name = "LE Type", Storage = "_LEType", DbType = "SmallInt NOT NULL")]
		[DataMember(Order = 3)]
		public short LEType
		{
			get
			{
				return this._LEType;
			}
			set
			{
				if (this._LEType != value)
				{
					this.SendPropertyChanging();
					this._LEType = value;
					this.SendPropertyChanged("LEType");
				}
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06003CDD RID: 15581 RVA: 0x000CE3AB File Offset: 0x000CC5AB
		// (set) Token: 0x06003CDE RID: 15582 RVA: 0x000CE3B3 File Offset: 0x000CC5B3
		[Column(Storage = "_TransportLibName", DbType = "NVarChar(38) NOT NULL", CanBeNull = false)]
		[DataMember(Order = 4)]
		public string TransportLibName
		{
			get
			{
				return this._TransportLibName;
			}
			set
			{
				if (this._TransportLibName != value)
				{
					this.SendPropertyChanging();
					this._TransportLibName = value;
					this.SendPropertyChanged("TransportLibName");
				}
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06003CDF RID: 15583 RVA: 0x000CE3DB File Offset: 0x000CC5DB
		// (set) Token: 0x06003CE0 RID: 15584 RVA: 0x000CE3E3 File Offset: 0x000CC5E3
		[Column(Storage = "_TransportCoClass", DbType = "NVarChar(38) NOT NULL", CanBeNull = false)]
		[DataMember(Order = 5)]
		public string TransportCoClass
		{
			get
			{
				return this._TransportCoClass;
			}
			set
			{
				if (this._TransportCoClass != value)
				{
					this.SendPropertyChanging();
					this._TransportCoClass = value;
					this.SendPropertyChanged("TransportCoClass");
				}
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06003CE1 RID: 15585 RVA: 0x000CE40B File Offset: 0x000CC60B
		// (set) Token: 0x06003CE2 RID: 15586 RVA: 0x000CE413 File Offset: 0x000CC613
		[Column(Storage = "_LUName", DbType = "NVarChar(8)")]
		[DataMember(Order = 6)]
		public string LUName
		{
			get
			{
				return this._LUName;
			}
			set
			{
				if (this._LUName != value)
				{
					this.SendPropertyChanging();
					this._LUName = value;
					this.SendPropertyChanged("LUName");
				}
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06003CE3 RID: 15587 RVA: 0x000CE43B File Offset: 0x000CC63B
		// (set) Token: 0x06003CE4 RID: 15588 RVA: 0x000CE443 File Offset: 0x000CC643
		[Column(Storage = "_Comment", DbType = "NVarChar(259)")]
		[DataMember(Order = 7)]
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

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06003CE5 RID: 15589 RVA: 0x000CE46B File Offset: 0x000CC66B
		// (set) Token: 0x06003CE6 RID: 15590 RVA: 0x000CE48A File Offset: 0x000CC68A
		[Association(Name = "LE_Views FK1", Storage = "_Views", ThisKey = "Identity", OtherKey = "LEID", DeleteRule = "NO ACTION")]
		[DataMember(Order = 8, EmitDefaultValue = false)]
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

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06003CE7 RID: 15591 RVA: 0x000CE498 File Offset: 0x000CC698
		// (set) Token: 0x06003CE8 RID: 15592 RVA: 0x000CE4B7 File Offset: 0x000CC6B7
		[Association(Name = "Local Environments LE Endpoints FK1", Storage = "_LEEndpoints", ThisKey = "Identity", OtherKey = "LEID", DeleteRule = "NO ACTION")]
		[DataMember(Order = 9, EmitDefaultValue = false)]
		public EntitySet<LEEndpoint> LEEndpoints
		{
			get
			{
				if (this.serializing && !this._LEEndpoints.HasLoadedOrAssignedValues)
				{
					return null;
				}
				return this._LEEndpoints;
			}
			set
			{
				this._LEEndpoints.Assign(value);
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06003CE9 RID: 15593 RVA: 0x000CE4C5 File Offset: 0x000CC6C5
		// (set) Token: 0x06003CEA RID: 15594 RVA: 0x000CE4E4 File Offset: 0x000CC6E4
		[Association(Name = "Local Environments_Listeners FK1", Storage = "_Listeners", ThisKey = "Identity", OtherKey = "LEID", DeleteRule = "NO ACTION")]
		[DataMember(Order = 10, EmitDefaultValue = false)]
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

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06003CEB RID: 15595 RVA: 0x000CE4F4 File Offset: 0x000CC6F4
		// (remove) Token: 0x06003CEC RID: 15596 RVA: 0x000CE52C File Offset: 0x000CC72C
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06003CED RID: 15597 RVA: 0x000CE564 File Offset: 0x000CC764
		// (remove) Token: 0x06003CEE RID: 15598 RVA: 0x000CE59C File Offset: 0x000CC79C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06003CEF RID: 15599 RVA: 0x000CE5D1 File Offset: 0x000CC7D1
		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, LocalEnvironment.emptyChangingEventArgs);
			}
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x000CE5EC File Offset: 0x000CC7EC
		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06003CF1 RID: 15601 RVA: 0x000CE608 File Offset: 0x000CC808
		private void attach_Views(View entity)
		{
			this.SendPropertyChanging();
			entity.LocalEnvironment = this;
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x000CE617 File Offset: 0x000CC817
		private void detach_Views(View entity)
		{
			this.SendPropertyChanging();
			entity.LocalEnvironment = null;
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x000CE626 File Offset: 0x000CC826
		private void attach_LEEndpoints(LEEndpoint entity)
		{
			this.SendPropertyChanging();
			entity.LocalEnvironment = this;
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x000CE635 File Offset: 0x000CC835
		private void detach_LEEndpoints(LEEndpoint entity)
		{
			this.SendPropertyChanging();
			entity.LocalEnvironment = null;
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x000CE644 File Offset: 0x000CC844
		private void attach_Listeners(Listener entity)
		{
			this.SendPropertyChanging();
			entity.LocalEnvironment = this;
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x000CE653 File Offset: 0x000CC853
		private void detach_Listeners(Listener entity)
		{
			this.SendPropertyChanging();
			entity.LocalEnvironment = null;
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x000CE664 File Offset: 0x000CC864
		private void Initialize()
		{
			this._Views = new EntitySet<View>(new Action<View>(this.attach_Views), new Action<View>(this.detach_Views));
			this._LEEndpoints = new EntitySet<LEEndpoint>(new Action<LEEndpoint>(this.attach_LEEndpoints), new Action<LEEndpoint>(this.detach_LEEndpoints));
			this._Listeners = new EntitySet<Listener>(new Action<Listener>(this.attach_Listeners), new Action<Listener>(this.detach_Listeners));
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x000CE6DA File Offset: 0x000CC8DA
		[OnDeserializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x000CE6E2 File Offset: 0x000CC8E2
		[OnSerializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerializing(StreamingContext context)
		{
			this.serializing = true;
		}

		// Token: 0x06003CFA RID: 15610 RVA: 0x000CE6EB File Offset: 0x000CC8EB
		[OnSerialized]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerialized(StreamingContext context)
		{
			this.serializing = false;
		}

		// Token: 0x04002443 RID: 9283
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

		// Token: 0x04002444 RID: 9284
		private int _Identity;

		// Token: 0x04002445 RID: 9285
		private string _Name;

		// Token: 0x04002446 RID: 9286
		private short _LEType;

		// Token: 0x04002447 RID: 9287
		private string _TransportLibName;

		// Token: 0x04002448 RID: 9288
		private string _TransportCoClass;

		// Token: 0x04002449 RID: 9289
		private string _LUName;

		// Token: 0x0400244A RID: 9290
		private string _Comment;

		// Token: 0x0400244B RID: 9291
		private EntitySet<View> _Views;

		// Token: 0x0400244C RID: 9292
		private EntitySet<LEEndpoint> _LEEndpoints;

		// Token: 0x0400244D RID: 9293
		private EntitySet<Listener> _Listeners;

		// Token: 0x0400244E RID: 9294
		private bool serializing;
	}
}
