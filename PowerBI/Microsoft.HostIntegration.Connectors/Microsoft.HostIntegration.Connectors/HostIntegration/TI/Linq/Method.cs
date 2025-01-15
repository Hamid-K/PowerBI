using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x02000764 RID: 1892
	[Table(Name = "dbo.Methods")]
	[DataContract]
	public class Method : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x06003CFC RID: 15612 RVA: 0x000CE705 File Offset: 0x000CC905
		public Method()
		{
			this.Initialize();
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06003CFD RID: 15613 RVA: 0x000CE713 File Offset: 0x000CC913
		// (set) Token: 0x06003CFE RID: 15614 RVA: 0x000CE71B File Offset: 0x000CC91B
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

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06003CFF RID: 15615 RVA: 0x000CE73E File Offset: 0x000CC93E
		// (set) Token: 0x06003D00 RID: 15616 RVA: 0x000CE746 File Offset: 0x000CC946
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

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x000CE76E File Offset: 0x000CC96E
		// (set) Token: 0x06003D02 RID: 15618 RVA: 0x000CE776 File Offset: 0x000CC976
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

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06003D03 RID: 15619 RVA: 0x000CE7AC File Offset: 0x000CC9AC
		// (set) Token: 0x06003D04 RID: 15620 RVA: 0x000CE7B4 File Offset: 0x000CC9B4
		[Column(Storage = "_DispID", DbType = "Int NOT NULL")]
		[DataMember(Order = 4)]
		public int DispID
		{
			get
			{
				return this._DispID;
			}
			set
			{
				if (this._DispID != value)
				{
					this.SendPropertyChanging();
					this._DispID = value;
					this.SendPropertyChanged("DispID");
				}
			}
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06003D05 RID: 15621 RVA: 0x000CE7D7 File Offset: 0x000CC9D7
		// (set) Token: 0x06003D06 RID: 15622 RVA: 0x000CE7DF File Offset: 0x000CC9DF
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

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06003D07 RID: 15623 RVA: 0x000CE807 File Offset: 0x000CCA07
		// (set) Token: 0x06003D08 RID: 15624 RVA: 0x000CE826 File Offset: 0x000CCA26
		[Association(Name = "Method_Determinants_FK1", Storage = "_Determinants", ThisKey = "Identity", OtherKey = "MethodID", DeleteRule = "NO ACTION")]
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

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06003D09 RID: 15625 RVA: 0x000CE834 File Offset: 0x000CCA34
		// (set) Token: 0x06003D0A RID: 15626 RVA: 0x000CE844 File Offset: 0x000CCA44
		[Association(Name = "Objects_Method FK1", Storage = "_Objects", ThisKey = "ObjectID", OtherKey = "Identity", IsForeignKey = true)]
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
						entity.Methods.Remove(this);
					}
					this._Objects.Entity = value;
					if (value != null)
					{
						value.Methods.Add(this);
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

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06003D0B RID: 15627 RVA: 0x000CE8CC File Offset: 0x000CCACC
		// (remove) Token: 0x06003D0C RID: 15628 RVA: 0x000CE904 File Offset: 0x000CCB04
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06003D0D RID: 15629 RVA: 0x000CE93C File Offset: 0x000CCB3C
		// (remove) Token: 0x06003D0E RID: 15630 RVA: 0x000CE974 File Offset: 0x000CCB74
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06003D0F RID: 15631 RVA: 0x000CE9A9 File Offset: 0x000CCBA9
		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, Method.emptyChangingEventArgs);
			}
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x000CE9C4 File Offset: 0x000CCBC4
		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x000CE9E0 File Offset: 0x000CCBE0
		private void attach_Determinants(Determinant entity)
		{
			this.SendPropertyChanging();
			entity.Method = this;
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x000CE9EF File Offset: 0x000CCBEF
		private void detach_Determinants(Determinant entity)
		{
			this.SendPropertyChanging();
			entity.Method = null;
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x000CE9FE File Offset: 0x000CCBFE
		private void Initialize()
		{
			this._Determinants = new EntitySet<Determinant>(new Action<Determinant>(this.attach_Determinants), new Action<Determinant>(this.detach_Determinants));
			this._Objects = default(EntityRef<Object>);
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x000CEA2F File Offset: 0x000CCC2F
		[OnDeserializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x000CEA37 File Offset: 0x000CCC37
		[OnSerializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerializing(StreamingContext context)
		{
			this.serializing = true;
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x000CEA40 File Offset: 0x000CCC40
		[OnSerialized]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerialized(StreamingContext context)
		{
			this.serializing = false;
		}

		// Token: 0x04002451 RID: 9297
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

		// Token: 0x04002452 RID: 9298
		private int _Identity;

		// Token: 0x04002453 RID: 9299
		private string _Name;

		// Token: 0x04002454 RID: 9300
		private int _ObjectID;

		// Token: 0x04002455 RID: 9301
		private int _DispID;

		// Token: 0x04002456 RID: 9302
		private string _Comment;

		// Token: 0x04002457 RID: 9303
		private EntitySet<Determinant> _Determinants;

		// Token: 0x04002458 RID: 9304
		private EntityRef<Object> _Objects;

		// Token: 0x04002459 RID: 9305
		private bool serializing;
	}
}
