using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000F1 RID: 241
	[Key("Id")]
	[OriginalName("AlertSubscription")]
	public class AlertSubscription : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000AB6 RID: 2742 RVA: 0x00015347 File Offset: 0x00013547
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

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x00015363 File Offset: 0x00013563
		// (set) Token: 0x06000AB8 RID: 2744 RVA: 0x0001536B File Offset: 0x0001356B
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

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0001537F File Offset: 0x0001357F
		// (set) Token: 0x06000ABA RID: 2746 RVA: 0x00015387 File Offset: 0x00013587
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

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0001539B File Offset: 0x0001359B
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x000153A3 File Offset: 0x000135A3
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

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x06000ABD RID: 2749 RVA: 0x000153B8 File Offset: 0x000135B8
		// (remove) Token: 0x06000ABE RID: 2750 RVA: 0x000153F0 File Offset: 0x000135F0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000ABF RID: 2751 RVA: 0x00015425 File Offset: 0x00013625
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040004E8 RID: 1256
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private long _Id;

		// Token: 0x040004E9 RID: 1257
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _ItemId;

		// Token: 0x040004EA RID: 1258
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _AlertType;
	}
}
