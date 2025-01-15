using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x02000765 RID: 1893
	[Table(Name = "dbo.Objects")]
	[DataContract]
	public class Object : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x06003D18 RID: 15640 RVA: 0x000CEA5A File Offset: 0x000CCC5A
		public Object()
		{
			this.Initialize();
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06003D19 RID: 15641 RVA: 0x000CEA68 File Offset: 0x000CCC68
		// (set) Token: 0x06003D1A RID: 15642 RVA: 0x000CEA70 File Offset: 0x000CCC70
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

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06003D1B RID: 15643 RVA: 0x000CEA93 File Offset: 0x000CCC93
		// (set) Token: 0x06003D1C RID: 15644 RVA: 0x000CEA9B File Offset: 0x000CCC9B
		[Column(Storage = "_PrimaryName", DbType = "NVarChar(38) NOT NULL", CanBeNull = false)]
		[DataMember(Order = 2)]
		public string PrimaryName
		{
			get
			{
				return this._PrimaryName;
			}
			set
			{
				if (this._PrimaryName != value)
				{
					this.SendPropertyChanging();
					this._PrimaryName = value;
					this.SendPropertyChanged("PrimaryName");
				}
			}
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06003D1D RID: 15645 RVA: 0x000CEAC3 File Offset: 0x000CCCC3
		// (set) Token: 0x06003D1E RID: 15646 RVA: 0x000CEACB File Offset: 0x000CCCCB
		[Column(Storage = "_ClassName", DbType = "NVarChar(38) NOT NULL", CanBeNull = false)]
		[DataMember(Order = 3)]
		public string ClassName
		{
			get
			{
				return this._ClassName;
			}
			set
			{
				if (this._ClassName != value)
				{
					this.SendPropertyChanging();
					this._ClassName = value;
					this.SendPropertyChanged("ClassName");
				}
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06003D1F RID: 15647 RVA: 0x000CEAF3 File Offset: 0x000CCCF3
		// (set) Token: 0x06003D20 RID: 15648 RVA: 0x000CEAFB File Offset: 0x000CCCFB
		[Column(Storage = "_FileID", DbType = "Int NOT NULL")]
		[DataMember(Order = 4)]
		public int FileID
		{
			get
			{
				return this._FileID;
			}
			set
			{
				if (this._FileID != value)
				{
					if (this._TIMFiles.HasLoadedOrAssignedValue)
					{
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
					this.SendPropertyChanging();
					this._FileID = value;
					this.SendPropertyChanged("FileID");
				}
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06003D21 RID: 15649 RVA: 0x000CEB31 File Offset: 0x000CCD31
		// (set) Token: 0x06003D22 RID: 15650 RVA: 0x000CEB39 File Offset: 0x000CCD39
		[Column(Storage = "_ServerType", DbType = "Int NOT NULL")]
		[DataMember(Order = 5)]
		public int ServerType
		{
			get
			{
				return this._ServerType;
			}
			set
			{
				if (this._ServerType != value)
				{
					this.SendPropertyChanging();
					this._ServerType = value;
					this.SendPropertyChanged("ServerType");
				}
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06003D23 RID: 15651 RVA: 0x000CEB5C File Offset: 0x000CCD5C
		// (set) Token: 0x06003D24 RID: 15652 RVA: 0x000CEB64 File Offset: 0x000CCD64
		[Column(Storage = "_Name", DbType = "NVarChar(259) NOT NULL", CanBeNull = false)]
		[DataMember(Order = 6)]
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

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06003D25 RID: 15653 RVA: 0x000CEB8C File Offset: 0x000CCD8C
		// (set) Token: 0x06003D26 RID: 15654 RVA: 0x000CEB94 File Offset: 0x000CCD94
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

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06003D27 RID: 15655 RVA: 0x000CEBBC File Offset: 0x000CCDBC
		// (set) Token: 0x06003D28 RID: 15656 RVA: 0x000CEBC4 File Offset: 0x000CCDC4
		[Column(Storage = "_ImplementingAssembly", DbType = "NVarChar(260)")]
		[DataMember(Order = 8)]
		public string ImplementingAssembly
		{
			get
			{
				return this._ImplementingAssembly;
			}
			set
			{
				if (this._ImplementingAssembly != value)
				{
					this.SendPropertyChanging();
					this._ImplementingAssembly = value;
					this.SendPropertyChanged("ImplementingAssembly");
				}
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06003D29 RID: 15657 RVA: 0x000CEBEC File Offset: 0x000CCDEC
		// (set) Token: 0x06003D2A RID: 15658 RVA: 0x000CEBF4 File Offset: 0x000CCDF4
		[Column(Storage = "_ImplementingNameSpaceDotClass", DbType = "NVarChar(260)")]
		[DataMember(Order = 9)]
		public string ImplementingNameSpaceDotClass
		{
			get
			{
				return this._ImplementingNameSpaceDotClass;
			}
			set
			{
				if (this._ImplementingNameSpaceDotClass != value)
				{
					this.SendPropertyChanging();
					this._ImplementingNameSpaceDotClass = value;
					this.SendPropertyChanged("ImplementingNameSpaceDotClass");
				}
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06003D2B RID: 15659 RVA: 0x000CEC1C File Offset: 0x000CCE1C
		// (set) Token: 0x06003D2C RID: 15660 RVA: 0x000CEC24 File Offset: 0x000CCE24
		[Column(Storage = "_FullyQualifiedAssemblyName", DbType = "NVarChar(1024)")]
		[DataMember(Order = 10)]
		public string FullyQualifiedAssemblyName
		{
			get
			{
				return this._FullyQualifiedAssemblyName;
			}
			set
			{
				if (this._FullyQualifiedAssemblyName != value)
				{
					this.SendPropertyChanging();
					this._FullyQualifiedAssemblyName = value;
					this.SendPropertyChanged("FullyQualifiedAssemblyName");
				}
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06003D2D RID: 15661 RVA: 0x000CEC4C File Offset: 0x000CCE4C
		// (set) Token: 0x06003D2E RID: 15662 RVA: 0x000CEC6B File Offset: 0x000CCE6B
		[Association(Name = "Objects_Method FK1", Storage = "_Methods", ThisKey = "Identity", OtherKey = "ObjectID", DeleteRule = "NO ACTION")]
		[DataMember(Order = 11, EmitDefaultValue = false)]
		public EntitySet<Method> Methods
		{
			get
			{
				if (this.serializing && !this._Methods.HasLoadedOrAssignedValues)
				{
					return null;
				}
				return this._Methods;
			}
			set
			{
				this._Methods.Assign(value);
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x000CEC79 File Offset: 0x000CCE79
		// (set) Token: 0x06003D30 RID: 15664 RVA: 0x000CEC88 File Offset: 0x000CCE88
		[Association(Name = "Objects_TIM_Files_FK1", Storage = "_TIMFiles", ThisKey = "FileID", OtherKey = "Identity", IsForeignKey = true)]
		public TIMFile TIMFile
		{
			get
			{
				return this._TIMFiles.Entity;
			}
			set
			{
				TIMFile entity = this._TIMFiles.Entity;
				if (entity != value || !this._TIMFiles.HasLoadedOrAssignedValue)
				{
					this.SendPropertyChanging();
					if (entity != null)
					{
						this._TIMFiles.Entity = null;
						entity.Objects.Remove(this);
					}
					this._TIMFiles.Entity = value;
					if (value != null)
					{
						value.Objects.Add(this);
						this._FileID = value.Identity;
					}
					else
					{
						this._FileID = 0;
					}
					this.SendPropertyChanged("TIMFiles");
				}
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06003D31 RID: 15665 RVA: 0x000CED0F File Offset: 0x000CCF0F
		// (set) Token: 0x06003D32 RID: 15666 RVA: 0x000CED2E File Offset: 0x000CCF2E
		[Association(Name = "Objects_Views FK1", Storage = "_Views", ThisKey = "Identity", OtherKey = "ObjectID", DeleteRule = "NO ACTION")]
		[DataMember(Order = 12, EmitDefaultValue = false)]
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

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06003D33 RID: 15667 RVA: 0x000CED3C File Offset: 0x000CCF3C
		// (remove) Token: 0x06003D34 RID: 15668 RVA: 0x000CED74 File Offset: 0x000CCF74
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06003D35 RID: 15669 RVA: 0x000CEDAC File Offset: 0x000CCFAC
		// (remove) Token: 0x06003D36 RID: 15670 RVA: 0x000CEDE4 File Offset: 0x000CCFE4
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06003D37 RID: 15671 RVA: 0x000CEE19 File Offset: 0x000CD019
		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, Object.emptyChangingEventArgs);
			}
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x000CEE34 File Offset: 0x000CD034
		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x000CEE50 File Offset: 0x000CD050
		private void attach_Methods(Method entity)
		{
			this.SendPropertyChanging();
			entity.Object = this;
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x000CEE5F File Offset: 0x000CD05F
		private void detach_Methods(Method entity)
		{
			this.SendPropertyChanging();
			entity.Object = null;
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x000CEE6E File Offset: 0x000CD06E
		private void attach_Views(View entity)
		{
			this.SendPropertyChanging();
			entity.Object = this;
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x000CEE7D File Offset: 0x000CD07D
		private void detach_Views(View entity)
		{
			this.SendPropertyChanging();
			entity.Object = null;
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x000CEE8C File Offset: 0x000CD08C
		private void Initialize()
		{
			this._Methods = new EntitySet<Method>(new Action<Method>(this.attach_Methods), new Action<Method>(this.detach_Methods));
			this._TIMFiles = default(EntityRef<TIMFile>);
			this._Views = new EntitySet<View>(new Action<View>(this.attach_Views), new Action<View>(this.detach_Views));
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x000CEEEB File Offset: 0x000CD0EB
		[OnDeserializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x06003D3F RID: 15679 RVA: 0x000CEEF3 File Offset: 0x000CD0F3
		[OnSerializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerializing(StreamingContext context)
		{
			this.serializing = true;
		}

		// Token: 0x06003D40 RID: 15680 RVA: 0x000CEEFC File Offset: 0x000CD0FC
		[OnSerialized]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerialized(StreamingContext context)
		{
			this.serializing = false;
		}

		// Token: 0x0400245C RID: 9308
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

		// Token: 0x0400245D RID: 9309
		private int _Identity;

		// Token: 0x0400245E RID: 9310
		private string _PrimaryName;

		// Token: 0x0400245F RID: 9311
		private string _ClassName;

		// Token: 0x04002460 RID: 9312
		private int _FileID;

		// Token: 0x04002461 RID: 9313
		private int _ServerType;

		// Token: 0x04002462 RID: 9314
		private string _Name;

		// Token: 0x04002463 RID: 9315
		private string _Comment;

		// Token: 0x04002464 RID: 9316
		private string _ImplementingAssembly;

		// Token: 0x04002465 RID: 9317
		private string _ImplementingNameSpaceDotClass;

		// Token: 0x04002466 RID: 9318
		private string _FullyQualifiedAssemblyName;

		// Token: 0x04002467 RID: 9319
		private EntitySet<Method> _Methods;

		// Token: 0x04002468 RID: 9320
		private EntityRef<TIMFile> _TIMFiles;

		// Token: 0x04002469 RID: 9321
		private EntitySet<View> _Views;

		// Token: 0x0400246A RID: 9322
		private bool serializing;
	}
}
