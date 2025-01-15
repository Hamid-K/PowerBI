using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x0200075E RID: 1886
	[Table(Name = "dbo.Determinants")]
	[DataContract]
	public class Determinant : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x06003C24 RID: 15396 RVA: 0x000CCD26 File Offset: 0x000CAF26
		public Determinant()
		{
			this.Initialize();
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06003C25 RID: 15397 RVA: 0x000CCD34 File Offset: 0x000CAF34
		// (set) Token: 0x06003C26 RID: 15398 RVA: 0x000CCD3C File Offset: 0x000CAF3C
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

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06003C27 RID: 15399 RVA: 0x000CCD5F File Offset: 0x000CAF5F
		// (set) Token: 0x06003C28 RID: 15400 RVA: 0x000CCD67 File Offset: 0x000CAF67
		[Column(Storage = "_MethodID", DbType = "Int NOT NULL")]
		[DataMember(Order = 2)]
		public int MethodID
		{
			get
			{
				return this._MethodID;
			}
			set
			{
				if (this._MethodID != value)
				{
					if (this._Methods.HasLoadedOrAssignedValue)
					{
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
					this.SendPropertyChanging();
					this._MethodID = value;
					this.SendPropertyChanged("MethodID");
				}
			}
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06003C29 RID: 15401 RVA: 0x000CCD9D File Offset: 0x000CAF9D
		// (set) Token: 0x06003C2A RID: 15402 RVA: 0x000CCDA5 File Offset: 0x000CAFA5
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

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06003C2B RID: 15403 RVA: 0x000CCDDB File Offset: 0x000CAFDB
		// (set) Token: 0x06003C2C RID: 15404 RVA: 0x000CCDE3 File Offset: 0x000CAFE3
		[Column(Storage = "_EndPointID", DbType = "Int NOT NULL")]
		[DataMember(Order = 4)]
		public int EndPointID
		{
			get
			{
				return this._EndPointID;
			}
			set
			{
				if (this._EndPointID != value)
				{
					if (this._LEEndpoints.HasLoadedOrAssignedValue)
					{
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
					this.SendPropertyChanging();
					this._EndPointID = value;
					this.SendPropertyChanged("EndPointID");
				}
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06003C2D RID: 15405 RVA: 0x000CCE19 File Offset: 0x000CB019
		// (set) Token: 0x06003C2E RID: 15406 RVA: 0x000CCE21 File Offset: 0x000CB021
		[Column(Storage = "_Type", DbType = "Int NOT NULL")]
		[DataMember(Order = 5)]
		public int Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if (this._Type != value)
				{
					this.SendPropertyChanging();
					this._Type = value;
					this.SendPropertyChanged("Type");
				}
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x000CCE44 File Offset: 0x000CB044
		// (set) Token: 0x06003C30 RID: 15408 RVA: 0x000CCE4C File Offset: 0x000CB04C
		[Column(Storage = "_ResolutionString", DbType = "NVarChar(512)")]
		[DataMember(Order = 6)]
		public string ResolutionString
		{
			get
			{
				return this._ResolutionString;
			}
			set
			{
				if (this._ResolutionString != value)
				{
					this.SendPropertyChanging();
					this._ResolutionString = value;
					this.SendPropertyChanged("ResolutionString");
				}
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06003C31 RID: 15409 RVA: 0x000CCE74 File Offset: 0x000CB074
		// (set) Token: 0x06003C32 RID: 15410 RVA: 0x000CCE7C File Offset: 0x000CB07C
		[Column(Storage = "_DeterminantPos", DbType = "Int")]
		[DataMember(Order = 7)]
		public int? DeterminantPos
		{
			get
			{
				return this._DeterminantPos;
			}
			set
			{
				int? determinantPos = this._DeterminantPos;
				int? num = value;
				if (!((determinantPos.GetValueOrDefault() == num.GetValueOrDefault()) & (determinantPos != null == (num != null))))
				{
					this.SendPropertyChanging();
					this._DeterminantPos = value;
					this.SendPropertyChanged("DeterminantPos");
				}
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06003C33 RID: 15411 RVA: 0x000CCECD File Offset: 0x000CB0CD
		// (set) Token: 0x06003C34 RID: 15412 RVA: 0x000CCED8 File Offset: 0x000CB0D8
		[Column(Storage = "_DeterminantLen", DbType = "Int")]
		[DataMember(Order = 8)]
		public int? DeterminantLen
		{
			get
			{
				return this._DeterminantLen;
			}
			set
			{
				int? determinantLen = this._DeterminantLen;
				int? num = value;
				if (!((determinantLen.GetValueOrDefault() == num.GetValueOrDefault()) & (determinantLen != null == (num != null))))
				{
					this.SendPropertyChanging();
					this._DeterminantLen = value;
					this.SendPropertyChanged("DeterminantLen");
				}
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06003C35 RID: 15413 RVA: 0x000CCF29 File Offset: 0x000CB129
		// (set) Token: 0x06003C36 RID: 15414 RVA: 0x000CCF31 File Offset: 0x000CB131
		[Column(Storage = "_ContinuationCounter", DbType = "Int NOT NULL")]
		[DataMember(Order = 9)]
		public int ContinuationCounter
		{
			get
			{
				return this._ContinuationCounter;
			}
			set
			{
				if (this._ContinuationCounter != value)
				{
					this.SendPropertyChanging();
					this._ContinuationCounter = value;
					this.SendPropertyChanged("ContinuationCounter");
				}
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06003C37 RID: 15415 RVA: 0x000CCF54 File Offset: 0x000CB154
		// (set) Token: 0x06003C38 RID: 15416 RVA: 0x000CCF5C File Offset: 0x000CB15C
		[Column(Storage = "_EnvelopeProcessorProgID", DbType = "NVarChar(39)")]
		[DataMember(Order = 10)]
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

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06003C39 RID: 15417 RVA: 0x000CCF84 File Offset: 0x000CB184
		// (set) Token: 0x06003C3A RID: 15418 RVA: 0x000CCF8C File Offset: 0x000CB18C
		[Column(Storage = "_TRMHandlerProgID", DbType = "NVarChar(39)")]
		[DataMember(Order = 11)]
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

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06003C3B RID: 15419 RVA: 0x000CCFB4 File Offset: 0x000CB1B4
		// (set) Token: 0x06003C3C RID: 15420 RVA: 0x000CCFBC File Offset: 0x000CB1BC
		[Column(Storage = "_ELMHandlerProgID", DbType = "NVarChar(39)")]
		[DataMember(Order = 12)]
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

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06003C3D RID: 15421 RVA: 0x000CCFE4 File Offset: 0x000CB1E4
		// (set) Token: 0x06003C3E RID: 15422 RVA: 0x000CCFEC File Offset: 0x000CB1EC
		[Column(Storage = "_TRMInputFormat", DbType = "NVarChar(64)")]
		[DataMember(Order = 13)]
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

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06003C3F RID: 15423 RVA: 0x000CD014 File Offset: 0x000CB214
		// (set) Token: 0x06003C40 RID: 15424 RVA: 0x000CD01C File Offset: 0x000CB21C
		[Column(Storage = "_TRMOutputFormat", DbType = "NVarChar(64)")]
		[DataMember(Order = 14)]
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

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06003C41 RID: 15425 RVA: 0x000CD044 File Offset: 0x000CB244
		// (set) Token: 0x06003C42 RID: 15426 RVA: 0x000CD04C File Offset: 0x000CB24C
		[Column(Storage = "_Comment", DbType = "NVarChar(259)")]
		[DataMember(Order = 15)]
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

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06003C43 RID: 15427 RVA: 0x000CD074 File Offset: 0x000CB274
		// (set) Token: 0x06003C44 RID: 15428 RVA: 0x000CD084 File Offset: 0x000CB284
		[Association(Name = "EndPoints_Determinants_FK", Storage = "_LEEndpoints", ThisKey = "EndPointID", OtherKey = "Identity", IsForeignKey = true)]
		public LEEndpoint LEEndpoint
		{
			get
			{
				return this._LEEndpoints.Entity;
			}
			set
			{
				LEEndpoint entity = this._LEEndpoints.Entity;
				if (entity != value || !this._LEEndpoints.HasLoadedOrAssignedValue)
				{
					this.SendPropertyChanging();
					if (entity != null)
					{
						this._LEEndpoints.Entity = null;
						entity.Determinants.Remove(this);
					}
					this._LEEndpoints.Entity = value;
					if (value != null)
					{
						value.Determinants.Add(this);
						this._EndPointID = value.Identity;
					}
					else
					{
						this._EndPointID = 0;
					}
					this.SendPropertyChanged("LEEndpoints");
				}
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06003C45 RID: 15429 RVA: 0x000CD10B File Offset: 0x000CB30B
		// (set) Token: 0x06003C46 RID: 15430 RVA: 0x000CD118 File Offset: 0x000CB318
		[Association(Name = "Method_Determinants_FK1", Storage = "_Methods", ThisKey = "MethodID", OtherKey = "Identity", IsForeignKey = true)]
		public Method Method
		{
			get
			{
				return this._Methods.Entity;
			}
			set
			{
				Method entity = this._Methods.Entity;
				if (entity != value || !this._Methods.HasLoadedOrAssignedValue)
				{
					this.SendPropertyChanging();
					if (entity != null)
					{
						this._Methods.Entity = null;
						entity.Determinants.Remove(this);
					}
					this._Methods.Entity = value;
					if (value != null)
					{
						value.Determinants.Add(this);
						this._MethodID = value.Identity;
					}
					else
					{
						this._MethodID = 0;
					}
					this.SendPropertyChanged("Methods");
				}
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06003C47 RID: 15431 RVA: 0x000CD19F File Offset: 0x000CB39F
		// (set) Token: 0x06003C48 RID: 15432 RVA: 0x000CD1AC File Offset: 0x000CB3AC
		[Association(Name = "Views_Determinants_FK1", Storage = "_Views", ThisKey = "ViewID", OtherKey = "Identity", IsForeignKey = true)]
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
						entity.Determinants.Remove(this);
					}
					this._Views.Entity = value;
					if (value != null)
					{
						value.Determinants.Add(this);
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

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06003C49 RID: 15433 RVA: 0x000CD234 File Offset: 0x000CB434
		// (remove) Token: 0x06003C4A RID: 15434 RVA: 0x000CD26C File Offset: 0x000CB46C
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06003C4B RID: 15435 RVA: 0x000CD2A4 File Offset: 0x000CB4A4
		// (remove) Token: 0x06003C4C RID: 15436 RVA: 0x000CD2DC File Offset: 0x000CB4DC
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06003C4D RID: 15437 RVA: 0x000CD311 File Offset: 0x000CB511
		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, Determinant.emptyChangingEventArgs);
			}
		}

		// Token: 0x06003C4E RID: 15438 RVA: 0x000CD32C File Offset: 0x000CB52C
		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06003C4F RID: 15439 RVA: 0x000CD348 File Offset: 0x000CB548
		private void Initialize()
		{
			this._LEEndpoints = default(EntityRef<LEEndpoint>);
			this._Methods = default(EntityRef<Method>);
			this._Views = default(EntityRef<View>);
		}

		// Token: 0x06003C50 RID: 15440 RVA: 0x000CD36E File Offset: 0x000CB56E
		[OnDeserializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x040023F6 RID: 9206
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

		// Token: 0x040023F7 RID: 9207
		private int _Identity;

		// Token: 0x040023F8 RID: 9208
		private int _MethodID;

		// Token: 0x040023F9 RID: 9209
		private int _ViewID;

		// Token: 0x040023FA RID: 9210
		private int _EndPointID;

		// Token: 0x040023FB RID: 9211
		private int _Type;

		// Token: 0x040023FC RID: 9212
		private string _ResolutionString;

		// Token: 0x040023FD RID: 9213
		private int? _DeterminantPos;

		// Token: 0x040023FE RID: 9214
		private int? _DeterminantLen;

		// Token: 0x040023FF RID: 9215
		private int _ContinuationCounter;

		// Token: 0x04002400 RID: 9216
		private string _EnvelopeProcessorProgID;

		// Token: 0x04002401 RID: 9217
		private string _TRMHandlerProgID;

		// Token: 0x04002402 RID: 9218
		private string _ELMHandlerProgID;

		// Token: 0x04002403 RID: 9219
		private string _TRMInputFormat;

		// Token: 0x04002404 RID: 9220
		private string _TRMOutputFormat;

		// Token: 0x04002405 RID: 9221
		private string _Comment;

		// Token: 0x04002406 RID: 9222
		private EntityRef<LEEndpoint> _LEEndpoints;

		// Token: 0x04002407 RID: 9223
		private EntityRef<Method> _Methods;

		// Token: 0x04002408 RID: 9224
		private EntityRef<View> _Views;
	}
}
