using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000CA RID: 202
	[OriginalName("Query")]
	public class Query : INotifyPropertyChanged
	{
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00012843 File Offset: 0x00010A43
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x0001284B File Offset: 0x00010A4B
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

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0001285F File Offset: 0x00010A5F
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x00012867 File Offset: 0x00010A67
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

		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06000903 RID: 2307 RVA: 0x0001287C File Offset: 0x00010A7C
		// (remove) Token: 0x06000904 RID: 2308 RVA: 0x000128B4 File Offset: 0x00010AB4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000905 RID: 2309 RVA: 0x000128E9 File Offset: 0x00010AE9
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000447 RID: 1095
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _CommandText;

		// Token: 0x04000448 RID: 1096
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private int? _Timeout;
	}
}
