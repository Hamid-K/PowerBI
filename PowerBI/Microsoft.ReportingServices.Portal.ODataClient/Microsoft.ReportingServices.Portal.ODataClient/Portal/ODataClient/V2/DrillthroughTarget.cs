using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200000A RID: 10
	[OriginalName("DrillthroughTarget")]
	public abstract class DrillthroughTarget : INotifyPropertyChanged
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000295B File Offset: 0x00000B5B
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002963 File Offset: 0x00000B63
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Type")]
		public DrillthroughTargetType Type
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

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600005D RID: 93 RVA: 0x00002978 File Offset: 0x00000B78
		// (remove) Token: 0x0600005E RID: 94 RVA: 0x000029B0 File Offset: 0x00000BB0
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600005F RID: 95 RVA: 0x000029E5 File Offset: 0x00000BE5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400005D RID: 93
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DrillthroughTargetType _Type;
	}
}
