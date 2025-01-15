using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200005F RID: 95
	[Key("Id")]
	[EntitySet("UserSettings")]
	[OriginalName("UserSettings")]
	public class UserSettings : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x00009509 File Offset: 0x00007709
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static UserSettings CreateUserSettings(Guid ID)
		{
			return new UserSettings
			{
				Id = ID
			};
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00009517 File Offset: 0x00007717
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x0000951F File Offset: 0x0000771F
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

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00009533 File Offset: 0x00007733
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x0000953B File Offset: 0x0000773B
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("EmailAddress")]
		public string EmailAddress
		{
			get
			{
				return this._EmailAddress;
			}
			set
			{
				this._EmailAddress = value;
				this.OnPropertyChanged("EmailAddress");
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600044E RID: 1102 RVA: 0x00009550 File Offset: 0x00007750
		// (remove) Token: 0x0600044F RID: 1103 RVA: 0x00009588 File Offset: 0x00007788
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000450 RID: 1104 RVA: 0x000095BD File Offset: 0x000077BD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000208 RID: 520
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private Guid _Id;

		// Token: 0x04000209 RID: 521
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _EmailAddress;
	}
}
