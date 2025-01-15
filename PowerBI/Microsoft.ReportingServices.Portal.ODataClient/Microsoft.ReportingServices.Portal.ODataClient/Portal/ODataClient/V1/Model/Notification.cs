using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000E2 RID: 226
	[Key("Id")]
	[EntitySet("Notifications")]
	[OriginalName("Notification")]
	public class Notification : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000A18 RID: 2584 RVA: 0x000145C7 File Offset: 0x000127C7
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static Notification CreateNotification(Guid ID, IssueType issueType)
		{
			return new Notification
			{
				Id = ID,
				IssueType = issueType
			};
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x000145DC File Offset: 0x000127DC
		// (set) Token: 0x06000A1A RID: 2586 RVA: 0x000145E4 File Offset: 0x000127E4
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

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x000145F8 File Offset: 0x000127F8
		// (set) Token: 0x06000A1C RID: 2588 RVA: 0x00014600 File Offset: 0x00012800
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

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x06000A1D RID: 2589 RVA: 0x00014614 File Offset: 0x00012814
		// (remove) Token: 0x06000A1E RID: 2590 RVA: 0x0001464C File Offset: 0x0001284C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000A1F RID: 2591 RVA: 0x00014681 File Offset: 0x00012881
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040004AC RID: 1196
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x040004AD RID: 1197
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private IssueType _IssueType;
	}
}
