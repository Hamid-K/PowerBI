using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000D6 RID: 214
	[OriginalName("ScheduleReference")]
	public class ScheduleReference : INotifyPropertyChanged
	{
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x000133A9 File Offset: 0x000115A9
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x000133B1 File Offset: 0x000115B1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ScheduleID")]
		public string ScheduleID
		{
			get
			{
				return this._ScheduleID;
			}
			set
			{
				this._ScheduleID = value;
				this.OnPropertyChanged("ScheduleID");
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x000133C5 File Offset: 0x000115C5
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x000133CD File Offset: 0x000115CD
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Definition")]
		public ScheduleDefinition Definition
		{
			get
			{
				return this._Definition;
			}
			set
			{
				this._Definition = value;
				this.OnPropertyChanged("Definition");
			}
		}

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06000987 RID: 2439 RVA: 0x000133E4 File Offset: 0x000115E4
		// (remove) Token: 0x06000988 RID: 2440 RVA: 0x0001341C File Offset: 0x0001161C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000989 RID: 2441 RVA: 0x00013451 File Offset: 0x00011651
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000477 RID: 1143
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ScheduleID;

		// Token: 0x04000478 RID: 1144
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleDefinition _Definition;
	}
}
