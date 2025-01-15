using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x02000760 RID: 1888
	[Table(Name = "dbo.Host Environments")]
	[DataContract]
	public class LinqHostEnvironment : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x06003C68 RID: 15464 RVA: 0x000CD6DF File Offset: 0x000CB8DF
		public LinqHostEnvironment()
		{
			this.Initialize();
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06003C69 RID: 15465 RVA: 0x000CD6ED File Offset: 0x000CB8ED
		// (set) Token: 0x06003C6A RID: 15466 RVA: 0x000CD6F5 File Offset: 0x000CB8F5
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

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06003C6B RID: 15467 RVA: 0x000CD718 File Offset: 0x000CB918
		// (set) Token: 0x06003C6C RID: 15468 RVA: 0x000CD720 File Offset: 0x000CB920
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

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06003C6D RID: 15469 RVA: 0x000CD748 File Offset: 0x000CB948
		// (set) Token: 0x06003C6E RID: 15470 RVA: 0x000CD750 File Offset: 0x000CB950
		[Column(Name = "HE Type", Storage = "_HEType", DbType = "Int NOT NULL")]
		[DataMember(Order = 3)]
		public int HEType
		{
			get
			{
				return this._HEType;
			}
			set
			{
				if (this._HEType != value)
				{
					this.SendPropertyChanging();
					this._HEType = value;
					this.SendPropertyChanged("HEType");
				}
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06003C6F RID: 15471 RVA: 0x000CD773 File Offset: 0x000CB973
		// (set) Token: 0x06003C70 RID: 15472 RVA: 0x000CD77B File Offset: 0x000CB97B
		[Column(Storage = "_ConvertLibName", DbType = "NVarChar(38) NOT NULL", CanBeNull = false)]
		[DataMember(Order = 4)]
		public string ConvertLibName
		{
			get
			{
				return this._ConvertLibName;
			}
			set
			{
				if (this._ConvertLibName != value)
				{
					this.SendPropertyChanging();
					this._ConvertLibName = value;
					this.SendPropertyChanged("ConvertLibName");
				}
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06003C71 RID: 15473 RVA: 0x000CD7A3 File Offset: 0x000CB9A3
		// (set) Token: 0x06003C72 RID: 15474 RVA: 0x000CD7AB File Offset: 0x000CB9AB
		[Column(Storage = "_ConvertCoClass", DbType = "NVarChar(38) NOT NULL", CanBeNull = false)]
		[DataMember(Order = 5)]
		public string ConvertCoClass
		{
			get
			{
				return this._ConvertCoClass;
			}
			set
			{
				if (this._ConvertCoClass != value)
				{
					this.SendPropertyChanging();
					this._ConvertCoClass = value;
					this.SendPropertyChanged("ConvertCoClass");
				}
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06003C73 RID: 15475 RVA: 0x000CD7D3 File Offset: 0x000CB9D3
		// (set) Token: 0x06003C74 RID: 15476 RVA: 0x000CD7DB File Offset: 0x000CB9DB
		[Column(Storage = "_CodePage", DbType = "Int NOT NULL")]
		[DataMember(Order = 6)]
		public int CodePage
		{
			get
			{
				return this._CodePage;
			}
			set
			{
				if (this._CodePage != value)
				{
					this.SendPropertyChanging();
					this._CodePage = value;
					this.SendPropertyChanged("CodePage");
				}
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06003C75 RID: 15477 RVA: 0x000CD7FE File Offset: 0x000CB9FE
		// (set) Token: 0x06003C76 RID: 15478 RVA: 0x000CD806 File Offset: 0x000CBA06
		[Column(Storage = "_ApplyToAllMappings", DbType = "SmallInt NOT NULL")]
		[DataMember(Order = 7)]
		public short ApplyToAllMappings
		{
			get
			{
				return this._ApplyToAllMappings;
			}
			set
			{
				if (this._ApplyToAllMappings != value)
				{
					this.SendPropertyChanging();
					this._ApplyToAllMappings = value;
					this.SendPropertyChanged("ApplyToAllMappings");
				}
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06003C77 RID: 15479 RVA: 0x000CD829 File Offset: 0x000CBA29
		// (set) Token: 0x06003C78 RID: 15480 RVA: 0x000CD831 File Offset: 0x000CBA31
		[Column(Storage = "_CredentialsID", DbType = "Int")]
		[DataMember(Order = 8)]
		public int? CredentialsID
		{
			get
			{
				return this._CredentialsID;
			}
			set
			{
				this._CredentialsID = value;
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06003C79 RID: 15481 RVA: 0x000CD83A File Offset: 0x000CBA3A
		// (set) Token: 0x06003C7A RID: 15482 RVA: 0x000CD842 File Offset: 0x000CBA42
		[Column(Storage = "_IPAddress", DbType = "Int NOT NULL")]
		[DataMember(Order = 9)]
		public int IPAddress
		{
			get
			{
				return this._IPAddress;
			}
			set
			{
				if (this._IPAddress != value)
				{
					this.SendPropertyChanging();
					this._IPAddress = value;
					this.SendPropertyChanged("IPAddress");
				}
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06003C7B RID: 15483 RVA: 0x000CD865 File Offset: 0x000CBA65
		// (set) Token: 0x06003C7C RID: 15484 RVA: 0x000CD86D File Offset: 0x000CBA6D
		[Column(Storage = "_HostName", DbType = "NVarChar(1024)")]
		[DataMember(Order = 10)]
		public string HostName
		{
			get
			{
				return this._HostName;
			}
			set
			{
				if (this._HostName != value)
				{
					this.SendPropertyChanging();
					this._HostName = value;
					this.SendPropertyChanged("HostName");
				}
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06003C7D RID: 15485 RVA: 0x000CD895 File Offset: 0x000CBA95
		// (set) Token: 0x06003C7E RID: 15486 RVA: 0x000CD89D File Offset: 0x000CBA9D
		[Column(Storage = "_IsDefault", DbType = "SmallInt NOT NULL")]
		[DataMember(Order = 11)]
		public short IsDefault
		{
			get
			{
				return this._IsDefault;
			}
			set
			{
				if (this._IsDefault != value)
				{
					this.SendPropertyChanging();
					this._IsDefault = value;
					this.SendPropertyChanged("IsDefault");
				}
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06003C7F RID: 15487 RVA: 0x000CD8C0 File Offset: 0x000CBAC0
		// (set) Token: 0x06003C80 RID: 15488 RVA: 0x000CD8C8 File Offset: 0x000CBAC8
		[Column(Storage = "_SendTimeOut", DbType = "Int NOT NULL")]
		[DataMember(Order = 12)]
		public int SendTimeOut
		{
			get
			{
				return this._SendTimeOut;
			}
			set
			{
				if (this._SendTimeOut != value)
				{
					this.SendPropertyChanging();
					this._SendTimeOut = value;
					this.SendPropertyChanged("SendTimeOut");
				}
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06003C81 RID: 15489 RVA: 0x000CD8EB File Offset: 0x000CBAEB
		// (set) Token: 0x06003C82 RID: 15490 RVA: 0x000CD8F3 File Offset: 0x000CBAF3
		[Column(Storage = "_RcvTimeOut", DbType = "Int NOT NULL")]
		[DataMember(Order = 13)]
		public int RcvTimeOut
		{
			get
			{
				return this._RcvTimeOut;
			}
			set
			{
				if (this._RcvTimeOut != value)
				{
					this.SendPropertyChanging();
					this._RcvTimeOut = value;
					this.SendPropertyChanged("RcvTimeOut");
				}
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06003C83 RID: 15491 RVA: 0x000CD916 File Offset: 0x000CBB16
		// (set) Token: 0x06003C84 RID: 15492 RVA: 0x000CD91E File Offset: 0x000CBB1E
		[Column(Storage = "_ResolutionType", DbType = "Int NOT NULL")]
		[DataMember(Order = 14)]
		public int ResolutionType
		{
			get
			{
				return this._ResolutionType;
			}
			set
			{
				if (this._ResolutionType != value)
				{
					this.SendPropertyChanging();
					this._ResolutionType = value;
					this.SendPropertyChanged("ResolutionType");
				}
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06003C85 RID: 15493 RVA: 0x000CD941 File Offset: 0x000CBB41
		// (set) Token: 0x06003C86 RID: 15494 RVA: 0x000CD949 File Offset: 0x000CBB49
		[Column(Storage = "_EnvelopeProcessorProgID", DbType = "NVarChar(39)")]
		[DataMember(Order = 15)]
		public string EnvelopeProcessorProgID
		{
			get
			{
				return this._EnvelopeProcessorProgID;
			}
			set
			{
				if (this._EnvelopeProcessorProgID != value)
				{
					this.SendPropertyChanging();
					this._EnvelopeProcessorProgID = value;
					this.SendPropertyChanged("EnvelopeProcessorProgID");
				}
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06003C87 RID: 15495 RVA: 0x000CD971 File Offset: 0x000CBB71
		// (set) Token: 0x06003C88 RID: 15496 RVA: 0x000CD979 File Offset: 0x000CBB79
		[Column(Storage = "_TRMHandlerProgID", DbType = "NVarChar(39)")]
		[DataMember(Order = 16)]
		public string TRMHandlerProgID
		{
			get
			{
				return this._TRMHandlerProgID;
			}
			set
			{
				if (this._TRMHandlerProgID != value)
				{
					this.SendPropertyChanging();
					this._TRMHandlerProgID = value;
					this.SendPropertyChanged("TRMHandlerProgID");
				}
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06003C89 RID: 15497 RVA: 0x000CD9A1 File Offset: 0x000CBBA1
		// (set) Token: 0x06003C8A RID: 15498 RVA: 0x000CD9A9 File Offset: 0x000CBBA9
		[Column(Storage = "_ELMHandlerProgID", DbType = "NVarChar(39)")]
		[DataMember(Order = 17)]
		public string ELMHandlerProgID
		{
			get
			{
				return this._ELMHandlerProgID;
			}
			set
			{
				if (this._ELMHandlerProgID != value)
				{
					this.SendPropertyChanging();
					this._ELMHandlerProgID = value;
					this.SendPropertyChanged("ELMHandlerProgID");
				}
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06003C8B RID: 15499 RVA: 0x000CD9D1 File Offset: 0x000CBBD1
		// (set) Token: 0x06003C8C RID: 15500 RVA: 0x000CD9D9 File Offset: 0x000CBBD9
		[Column(Storage = "_TRMInputFormat", DbType = "NVarChar(64)")]
		[DataMember(Order = 18)]
		public string TRMInputFormat
		{
			get
			{
				return this._TRMInputFormat;
			}
			set
			{
				if (this._TRMInputFormat != value)
				{
					this.SendPropertyChanging();
					this._TRMInputFormat = value;
					this.SendPropertyChanged("TRMInputFormat");
				}
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06003C8D RID: 15501 RVA: 0x000CDA01 File Offset: 0x000CBC01
		// (set) Token: 0x06003C8E RID: 15502 RVA: 0x000CDA09 File Offset: 0x000CBC09
		[Column(Storage = "_TRMOutputFormat", DbType = "NVarChar(64)")]
		[DataMember(Order = 19)]
		public string TRMOutputFormat
		{
			get
			{
				return this._TRMOutputFormat;
			}
			set
			{
				if (this._TRMOutputFormat != value)
				{
					this.SendPropertyChanging();
					this._TRMOutputFormat = value;
					this.SendPropertyChanged("TRMOutputFormat");
				}
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06003C8F RID: 15503 RVA: 0x000CDA31 File Offset: 0x000CBC31
		// (set) Token: 0x06003C90 RID: 15504 RVA: 0x000CDA39 File Offset: 0x000CBC39
		[Column(Storage = "_LinkTranID", DbType = "NVarChar(64)")]
		[DataMember(Order = 20)]
		public string LinkTranID
		{
			get
			{
				return this._LinkTranID;
			}
			set
			{
				if (this._LinkTranID != value)
				{
					this.SendPropertyChanging();
					this._LinkTranID = value;
					this.SendPropertyChanged("LinkTranID");
				}
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06003C91 RID: 15505 RVA: 0x000CDA61 File Offset: 0x000CBC61
		// (set) Token: 0x06003C92 RID: 15506 RVA: 0x000CDA69 File Offset: 0x000CBC69
		[Column(Storage = "_Comment", DbType = "NVarChar(259)")]
		[DataMember(Order = 21)]
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

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06003C93 RID: 15507 RVA: 0x000CDA91 File Offset: 0x000CBC91
		// (set) Token: 0x06003C94 RID: 15508 RVA: 0x000CDAB0 File Offset: 0x000CBCB0
		[Association(Name = "Host Environments_HE Permission_FK1", Storage = "_HEPermissions", ThisKey = "Identity", OtherKey = "HEID", DeleteRule = "NO ACTION")]
		[DataMember(Order = 22, EmitDefaultValue = false)]
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

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06003C95 RID: 15509 RVA: 0x000CDAC0 File Offset: 0x000CBCC0
		// (remove) Token: 0x06003C96 RID: 15510 RVA: 0x000CDAF8 File Offset: 0x000CBCF8
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06003C97 RID: 15511 RVA: 0x000CDB30 File Offset: 0x000CBD30
		// (remove) Token: 0x06003C98 RID: 15512 RVA: 0x000CDB68 File Offset: 0x000CBD68
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06003C99 RID: 15513 RVA: 0x000CDB9D File Offset: 0x000CBD9D
		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, LinqHostEnvironment.emptyChangingEventArgs);
			}
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x000CDBB8 File Offset: 0x000CBDB8
		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x000CDBD4 File Offset: 0x000CBDD4
		private void attach_HEPermissions(HEPermission entity)
		{
			this.SendPropertyChanging();
			entity.HostEnvironment = this;
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x000CDBE3 File Offset: 0x000CBDE3
		private void detach_HEPermissions(HEPermission entity)
		{
			this.SendPropertyChanging();
			entity.HostEnvironment = null;
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x000CDBF2 File Offset: 0x000CBDF2
		private void Initialize()
		{
			this._HEPermissions = new EntitySet<HEPermission>(new Action<HEPermission>(this.attach_HEPermissions), new Action<HEPermission>(this.detach_HEPermissions));
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x000CDC17 File Offset: 0x000CBE17
		[OnDeserializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x000CDC1F File Offset: 0x000CBE1F
		[OnSerializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerializing(StreamingContext context)
		{
			this.serializing = true;
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x000CDC28 File Offset: 0x000CBE28
		[OnSerialized]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerialized(StreamingContext context)
		{
			this.serializing = false;
		}

		// Token: 0x04002414 RID: 9236
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

		// Token: 0x04002415 RID: 9237
		private int _Identity;

		// Token: 0x04002416 RID: 9238
		private string _Name;

		// Token: 0x04002417 RID: 9239
		private int _HEType;

		// Token: 0x04002418 RID: 9240
		private string _ConvertLibName;

		// Token: 0x04002419 RID: 9241
		private string _ConvertCoClass;

		// Token: 0x0400241A RID: 9242
		private int _CodePage;

		// Token: 0x0400241B RID: 9243
		private short _ApplyToAllMappings;

		// Token: 0x0400241C RID: 9244
		private int? _CredentialsID;

		// Token: 0x0400241D RID: 9245
		private int _IPAddress;

		// Token: 0x0400241E RID: 9246
		private string _HostName;

		// Token: 0x0400241F RID: 9247
		private short _IsDefault;

		// Token: 0x04002420 RID: 9248
		private int _SendTimeOut;

		// Token: 0x04002421 RID: 9249
		private int _RcvTimeOut;

		// Token: 0x04002422 RID: 9250
		private int _ResolutionType;

		// Token: 0x04002423 RID: 9251
		private string _EnvelopeProcessorProgID;

		// Token: 0x04002424 RID: 9252
		private string _TRMHandlerProgID;

		// Token: 0x04002425 RID: 9253
		private string _ELMHandlerProgID;

		// Token: 0x04002426 RID: 9254
		private string _TRMInputFormat;

		// Token: 0x04002427 RID: 9255
		private string _TRMOutputFormat;

		// Token: 0x04002428 RID: 9256
		private string _LinkTranID;

		// Token: 0x04002429 RID: 9257
		private string _Comment;

		// Token: 0x0400242A RID: 9258
		private EntitySet<HEPermission> _HEPermissions;

		// Token: 0x0400242B RID: 9259
		private bool serializing;
	}
}
