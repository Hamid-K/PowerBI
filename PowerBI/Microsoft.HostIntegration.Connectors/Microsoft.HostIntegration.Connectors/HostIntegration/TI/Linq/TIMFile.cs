using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x02000767 RID: 1895
	[Table(Name = "dbo.TIM Files")]
	[DataContract]
	public class TIMFile : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x06003D66 RID: 15718 RVA: 0x000CF2BA File Offset: 0x000CD4BA
		public TIMFile()
		{
			this.Initialize();
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06003D67 RID: 15719 RVA: 0x000CF2C8 File Offset: 0x000CD4C8
		// (set) Token: 0x06003D68 RID: 15720 RVA: 0x000CF2D0 File Offset: 0x000CD4D0
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

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06003D69 RID: 15721 RVA: 0x000CF2F3 File Offset: 0x000CD4F3
		// (set) Token: 0x06003D6A RID: 15722 RVA: 0x000CF2FB File Offset: 0x000CD4FB
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

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06003D6B RID: 15723 RVA: 0x000CF323 File Offset: 0x000CD523
		// (set) Token: 0x06003D6C RID: 15724 RVA: 0x000CF32B File Offset: 0x000CD52B
		[Column(Storage = "_Comment", DbType = "NVarChar(259)")]
		[DataMember(Order = 3)]
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

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06003D6D RID: 15725 RVA: 0x000CF353 File Offset: 0x000CD553
		// (set) Token: 0x06003D6E RID: 15726 RVA: 0x000CF35B File Offset: 0x000CD55B
		[Column(Storage = "_File", DbType = "Image", CanBeNull = true, UpdateCheck = UpdateCheck.Never)]
		[DataMember(Order = 4)]
		public Binary File
		{
			get
			{
				return this._File;
			}
			set
			{
				if (this._File != value)
				{
					this.SendPropertyChanging();
					this._File = value;
					this.SendPropertyChanged("File");
				}
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06003D6F RID: 15727 RVA: 0x000CF383 File Offset: 0x000CD583
		// (set) Token: 0x06003D70 RID: 15728 RVA: 0x000CF3A2 File Offset: 0x000CD5A2
		[Association(Name = "Objects_TIM_Files_FK1", Storage = "_Objects", ThisKey = "Identity", OtherKey = "FileID", DeleteRule = "NO ACTION")]
		[DataMember(Order = 5, EmitDefaultValue = false)]
		public EntitySet<Object> Objects
		{
			get
			{
				if (this.serializing && !this._Objects.HasLoadedOrAssignedValues)
				{
					return null;
				}
				return this._Objects;
			}
			set
			{
				this._Objects.Assign(value);
			}
		}

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06003D71 RID: 15729 RVA: 0x000CF3B0 File Offset: 0x000CD5B0
		// (remove) Token: 0x06003D72 RID: 15730 RVA: 0x000CF3E8 File Offset: 0x000CD5E8
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06003D73 RID: 15731 RVA: 0x000CF420 File Offset: 0x000CD620
		// (remove) Token: 0x06003D74 RID: 15732 RVA: 0x000CF458 File Offset: 0x000CD658
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06003D75 RID: 15733 RVA: 0x000CF48D File Offset: 0x000CD68D
		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, TIMFile.emptyChangingEventArgs);
			}
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x000CF4A8 File Offset: 0x000CD6A8
		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x000CF4C4 File Offset: 0x000CD6C4
		private void attach_Objects(Object entity)
		{
			this.SendPropertyChanging();
			entity.TIMFile = this;
		}

		// Token: 0x06003D78 RID: 15736 RVA: 0x000CF4D3 File Offset: 0x000CD6D3
		private void detach_Objects(Object entity)
		{
			this.SendPropertyChanging();
			entity.TIMFile = null;
		}

		// Token: 0x06003D79 RID: 15737 RVA: 0x000CF4E2 File Offset: 0x000CD6E2
		private void Initialize()
		{
			this._Objects = new EntitySet<Object>(new Action<Object>(this.attach_Objects), new Action<Object>(this.detach_Objects));
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x000CF507 File Offset: 0x000CD707
		[OnDeserializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x000CF50F File Offset: 0x000CD70F
		[OnSerializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerializing(StreamingContext context)
		{
			this.serializing = true;
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x000CF518 File Offset: 0x000CD718
		[OnSerialized]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerialized(StreamingContext context)
		{
			this.serializing = false;
		}

		// Token: 0x0400247B RID: 9339
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

		// Token: 0x0400247C RID: 9340
		private int _Identity;

		// Token: 0x0400247D RID: 9341
		private string _Name;

		// Token: 0x0400247E RID: 9342
		private string _Comment;

		// Token: 0x0400247F RID: 9343
		private Binary _File;

		// Token: 0x04002480 RID: 9344
		private EntitySet<Object> _Objects;

		// Token: 0x04002481 RID: 9345
		private bool serializing;
	}
}
