using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000F3 RID: 243
	[Key("Action")]
	[OriginalName("AllowedAction")]
	public class AllowedAction : BaseEntityType, INotifyPropertyChanged
	{
		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001545F File Offset: 0x0001365F
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static AllowedAction CreateAllowedAction(string action)
		{
			return new AllowedAction
			{
				Action = action
			};
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0001546D File Offset: 0x0001366D
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x00015475 File Offset: 0x00013675
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

		// Token: 0x14000076 RID: 118
		// (add) Token: 0x06000AC7 RID: 2759 RVA: 0x0001548C File Offset: 0x0001368C
		// (remove) Token: 0x06000AC8 RID: 2760 RVA: 0x000154C4 File Offset: 0x000136C4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000154F9 File Offset: 0x000136F9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040004EC RID: 1260
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Action;
	}
}
