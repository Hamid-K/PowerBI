using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200000F RID: 15
	[Key("Id")]
	[EntitySet("AlertSubscriptions")]
	[OriginalName("AlertSubscription")]
	public class AlertSubscription : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00002BD4 File Offset: 0x00000DD4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static AlertSubscription CreateAlertSubscription(long ID, Guid itemId, string alertType)
		{
			return new AlertSubscription
			{
				Id = ID,
				ItemId = itemId,
				AlertType = alertType
			};
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002BF0 File Offset: 0x00000DF0
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002BF8 File Offset: 0x00000DF8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Id")]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.OnPropertyChanged("Id");
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002C0C File Offset: 0x00000E0C
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002C14 File Offset: 0x00000E14
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ItemId")]
		public Guid ItemId
		{
			get
			{
				return this._ItemId;
			}
			set
			{
				this._ItemId = value;
				this.OnPropertyChanged("ItemId");
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002C28 File Offset: 0x00000E28
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002C30 File Offset: 0x00000E30
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("AlertType")]
		public string AlertType
		{
			get
			{
				return this._AlertType;
			}
			set
			{
				this._AlertType = value;
				this.OnPropertyChanged("AlertType");
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000083 RID: 131 RVA: 0x00002C44 File Offset: 0x00000E44
		// (remove) Token: 0x06000084 RID: 132 RVA: 0x00002C7C File Offset: 0x00000E7C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000085 RID: 133 RVA: 0x00002CB1 File Offset: 0x00000EB1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000068 RID: 104
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private long _Id;

		// Token: 0x04000069 RID: 105
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _ItemId;

		// Token: 0x0400006A RID: 106
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _AlertType;
	}
}
