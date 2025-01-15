using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200002A RID: 42
	[OriginalName("Query")]
	public class Query : INotifyPropertyChanged
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00004D1D File Offset: 0x00002F1D
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00004D25 File Offset: 0x00002F25
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("CommandText")]
		public string CommandText
		{
			get
			{
				return this._CommandText;
			}
			set
			{
				this._CommandText = value;
				this.OnPropertyChanged("CommandText");
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00004D39 File Offset: 0x00002F39
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00004D41 File Offset: 0x00002F41
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Timeout")]
		public int? Timeout
		{
			get
			{
				return this._Timeout;
			}
			set
			{
				this._Timeout = value;
				this.OnPropertyChanged("Timeout");
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060001C8 RID: 456 RVA: 0x00004D58 File Offset: 0x00002F58
		// (remove) Token: 0x060001C9 RID: 457 RVA: 0x00004D90 File Offset: 0x00002F90
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060001CA RID: 458 RVA: 0x00004DC5 File Offset: 0x00002FC5
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040000EF RID: 239
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _CommandText;

		// Token: 0x040000F0 RID: 240
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int? _Timeout;
	}
}
