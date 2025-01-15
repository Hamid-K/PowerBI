using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200006B RID: 107
	[Key("Action")]
	[OriginalName("AllowedAction")]
	public class AllowedAction : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x0000A027 File Offset: 0x00008227
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static AllowedAction CreateAllowedAction(string action)
		{
			return new AllowedAction
			{
				Action = action
			};
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000A035 File Offset: 0x00008235
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x0000A03D File Offset: 0x0000823D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Action")]
		public string Action
		{
			get
			{
				return this._Action;
			}
			set
			{
				this._Action = value;
				this.OnPropertyChanged("Action");
			}
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060004D0 RID: 1232 RVA: 0x0000A054 File Offset: 0x00008254
		// (remove) Token: 0x060004D1 RID: 1233 RVA: 0x0000A08C File Offset: 0x0000828C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000A0C1 File Offset: 0x000082C1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000237 RID: 567
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Action;
	}
}
