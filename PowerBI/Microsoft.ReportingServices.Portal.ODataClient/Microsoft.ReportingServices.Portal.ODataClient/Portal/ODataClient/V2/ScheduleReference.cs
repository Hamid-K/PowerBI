using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000055 RID: 85
	[OriginalName("ScheduleReference")]
	public class ScheduleReference : INotifyPropertyChanged
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00008989 File Offset: 0x00006B89
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00008991 File Offset: 0x00006B91
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

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x000089A5 File Offset: 0x00006BA5
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x000089AD File Offset: 0x00006BAD
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

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060003C4 RID: 964 RVA: 0x000089C4 File Offset: 0x00006BC4
		// (remove) Token: 0x060003C5 RID: 965 RVA: 0x000089FC File Offset: 0x00006BFC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060003C6 RID: 966 RVA: 0x00008A31 File Offset: 0x00006C31
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040001D1 RID: 465
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _ScheduleID;

		// Token: 0x040001D2 RID: 466
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ScheduleDefinition _Definition;
	}
}
