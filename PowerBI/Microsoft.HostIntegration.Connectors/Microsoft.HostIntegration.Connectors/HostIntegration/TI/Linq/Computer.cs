using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI.Linq
{
	// Token: 0x0200075D RID: 1885
	[Table(Name = "dbo.Computers")]
	[DataContract]
	public class Computer : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x06003C0E RID: 15374 RVA: 0x000CCADE File Offset: 0x000CACDE
		public Computer()
		{
			this.Initialize();
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06003C0F RID: 15375 RVA: 0x000CCAEC File Offset: 0x000CACEC
		// (set) Token: 0x06003C10 RID: 15376 RVA: 0x000CCAF4 File Offset: 0x000CACF4
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

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06003C11 RID: 15377 RVA: 0x000CCB17 File Offset: 0x000CAD17
		// (set) Token: 0x06003C12 RID: 15378 RVA: 0x000CCB1F File Offset: 0x000CAD1F
		[Column(Storage = "_Name", DbType = "NVarChar(15) NOT NULL", CanBeNull = false)]
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

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06003C13 RID: 15379 RVA: 0x000CCB47 File Offset: 0x000CAD47
		// (set) Token: 0x06003C14 RID: 15380 RVA: 0x000CCB4F File Offset: 0x000CAD4F
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

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x000CCB77 File Offset: 0x000CAD77
		// (set) Token: 0x06003C16 RID: 15382 RVA: 0x000CCB96 File Offset: 0x000CAD96
		[Association(Name = "Computers_Applications_FK1", Storage = "_Applications", ThisKey = "Identity", OtherKey = "ComputerID", DeleteRule = "NO ACTION")]
		[DataMember(Order = 4, EmitDefaultValue = false)]
		public EntitySet<Application> Applications
		{
			get
			{
				if (this.serializing && !this._Applications.HasLoadedOrAssignedValues)
				{
					return null;
				}
				return this._Applications;
			}
			set
			{
				this._Applications.Assign(value);
			}
		}

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06003C17 RID: 15383 RVA: 0x000CCBA4 File Offset: 0x000CADA4
		// (remove) Token: 0x06003C18 RID: 15384 RVA: 0x000CCBDC File Offset: 0x000CADDC
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06003C19 RID: 15385 RVA: 0x000CCC14 File Offset: 0x000CAE14
		// (remove) Token: 0x06003C1A RID: 15386 RVA: 0x000CCC4C File Offset: 0x000CAE4C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06003C1B RID: 15387 RVA: 0x000CCC81 File Offset: 0x000CAE81
		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, Computer.emptyChangingEventArgs);
			}
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x000CCC9C File Offset: 0x000CAE9C
		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x06003C1D RID: 15389 RVA: 0x000CCCB8 File Offset: 0x000CAEB8
		private void attach_Applications(Application entity)
		{
			this.SendPropertyChanging();
			entity.Computer = this;
		}

		// Token: 0x06003C1E RID: 15390 RVA: 0x000CCCC7 File Offset: 0x000CAEC7
		private void detach_Applications(Application entity)
		{
			this.SendPropertyChanging();
			entity.Computer = null;
		}

		// Token: 0x06003C1F RID: 15391 RVA: 0x000CCCD6 File Offset: 0x000CAED6
		private void Initialize()
		{
			this._Applications = new EntitySet<Application>(new Action<Application>(this.attach_Applications), new Action<Application>(this.detach_Applications));
		}

		// Token: 0x06003C20 RID: 15392 RVA: 0x000CCCFB File Offset: 0x000CAEFB
		[OnDeserializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}

		// Token: 0x06003C21 RID: 15393 RVA: 0x000CCD03 File Offset: 0x000CAF03
		[OnSerializing]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerializing(StreamingContext context)
		{
			this.serializing = true;
		}

		// Token: 0x06003C22 RID: 15394 RVA: 0x000CCD0C File Offset: 0x000CAF0C
		[OnSerialized]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnSerialized(StreamingContext context)
		{
			this.serializing = false;
		}

		// Token: 0x040023EE RID: 9198
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

		// Token: 0x040023EF RID: 9199
		private int _Identity;

		// Token: 0x040023F0 RID: 9200
		private string _Name;

		// Token: 0x040023F1 RID: 9201
		private string _Comment;

		// Token: 0x040023F2 RID: 9202
		private EntitySet<Application> _Applications;

		// Token: 0x040023F3 RID: 9203
		private bool serializing;
	}
}
