using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000013 RID: 19
	[Key("Id")]
	[EntitySet("CacheRefreshPlanHistory")]
	[OriginalName("SubscriptionHistory")]
	public class SubscriptionHistory : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00002FD8 File Offset: 0x000011D8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static SubscriptionHistory CreateSubscriptionHistory(int ID, Guid subscriptionID, SubscriptionExecutionType type, DateTimeOffset startTime, DateTimeOffset endTime, SubscriptionStatus status)
		{
			return new SubscriptionHistory
			{
				Id = ID,
				SubscriptionID = subscriptionID,
				Type = type,
				StartTime = startTime,
				EndTime = endTime,
				Status = status
			};
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000300B File Offset: 0x0000120B
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003013 File Offset: 0x00001213
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Id")]
		public int Id
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003027 File Offset: 0x00001227
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000302F File Offset: 0x0000122F
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("SubscriptionID")]
		public Guid SubscriptionID
		{
			get
			{
				return this._SubscriptionID;
			}
			set
			{
				this._SubscriptionID = value;
				this.OnPropertyChanged("SubscriptionID");
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003043 File Offset: 0x00001243
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000304B File Offset: 0x0000124B
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Type")]
		public SubscriptionExecutionType Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.OnPropertyChanged("Type");
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000305F File Offset: 0x0000125F
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00003067 File Offset: 0x00001267
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("StartTime")]
		public DateTimeOffset StartTime
		{
			get
			{
				return this._StartTime;
			}
			set
			{
				this._StartTime = value;
				this.OnPropertyChanged("StartTime");
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000307B File Offset: 0x0000127B
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003083 File Offset: 0x00001283
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("EndTime")]
		public DateTimeOffset EndTime
		{
			get
			{
				return this._EndTime;
			}
			set
			{
				this._EndTime = value;
				this.OnPropertyChanged("EndTime");
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003097 File Offset: 0x00001297
		// (set) Token: 0x060000BA RID: 186 RVA: 0x0000309F File Offset: 0x0000129F
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Status")]
		public SubscriptionStatus Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				this._Status = value;
				this.OnPropertyChanged("Status");
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000030B3 File Offset: 0x000012B3
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000030BB File Offset: 0x000012BB
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Message")]
		public string Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				this._Message = value;
				this.OnPropertyChanged("Message");
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000030CF File Offset: 0x000012CF
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000030D7 File Offset: 0x000012D7
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Details")]
		public string Details
		{
			get
			{
				return this._Details;
			}
			set
			{
				this._Details = value;
				this.OnPropertyChanged("Details");
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060000BF RID: 191 RVA: 0x000030EC File Offset: 0x000012EC
		// (remove) Token: 0x060000C0 RID: 192 RVA: 0x00003124 File Offset: 0x00001324
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060000C1 RID: 193 RVA: 0x00003159 File Offset: 0x00001359
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400007B RID: 123
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int _Id;

		// Token: 0x0400007C RID: 124
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _SubscriptionID;

		// Token: 0x0400007D RID: 125
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private SubscriptionExecutionType _Type;

		// Token: 0x0400007E RID: 126
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _StartTime;

		// Token: 0x0400007F RID: 127
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DateTimeOffset _EndTime;

		// Token: 0x04000080 RID: 128
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private SubscriptionStatus _Status;

		// Token: 0x04000081 RID: 129
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Message;

		// Token: 0x04000082 RID: 130
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Details;
	}
}
