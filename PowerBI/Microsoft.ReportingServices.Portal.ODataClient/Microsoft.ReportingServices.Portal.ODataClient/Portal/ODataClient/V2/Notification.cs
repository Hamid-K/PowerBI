using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200003C RID: 60
	[Key("Id")]
	[EntitySet("Notifications")]
	[OriginalName("Notification")]
	public class Notification : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000288 RID: 648 RVA: 0x000066BC File Offset: 0x000048BC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static Notification CreateNotification(Guid ID, IssueType issueType)
		{
			return new Notification
			{
				Id = ID,
				IssueType = issueType
			};
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000289 RID: 649 RVA: 0x000066D1 File Offset: 0x000048D1
		// (set) Token: 0x0600028A RID: 650 RVA: 0x000066D9 File Offset: 0x000048D9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Id")]
		public Guid Id
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

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600028B RID: 651 RVA: 0x000066ED File Offset: 0x000048ED
		// (set) Token: 0x0600028C RID: 652 RVA: 0x000066F5 File Offset: 0x000048F5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("IssueType")]
		public IssueType IssueType
		{
			get
			{
				return this._IssueType;
			}
			set
			{
				this._IssueType = value;
				this.OnPropertyChanged("IssueType");
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600028D RID: 653 RVA: 0x0000670C File Offset: 0x0000490C
		// (remove) Token: 0x0600028E RID: 654 RVA: 0x00006744 File Offset: 0x00004944
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600028F RID: 655 RVA: 0x00006779 File Offset: 0x00004979
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400014B RID: 331
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x0400014C RID: 332
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private IssueType _IssueType;
	}
}
