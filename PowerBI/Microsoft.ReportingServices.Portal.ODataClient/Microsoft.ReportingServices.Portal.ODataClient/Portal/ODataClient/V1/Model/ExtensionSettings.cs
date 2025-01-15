using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000113 RID: 275
	[OriginalName("ExtensionSettings")]
	public class ExtensionSettings : INotifyPropertyChanged
	{
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00017282 File Offset: 0x00015482
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x0001728A File Offset: 0x0001548A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Extension")]
		public string Extension
		{
			get
			{
				return this._Extension;
			}
			set
			{
				this._Extension = value;
				this.OnPropertyChanged("Extension");
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0001729E File Offset: 0x0001549E
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x000172A6 File Offset: 0x000154A6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("ParameterValues")]
		public ObservableCollection<ParameterValue> ParameterValues
		{
			get
			{
				return this._ParameterValues;
			}
			set
			{
				this._ParameterValues = value;
				this.OnPropertyChanged("ParameterValues");
			}
		}

		// Token: 0x14000084 RID: 132
		// (add) Token: 0x06000BEF RID: 3055 RVA: 0x000172BC File Offset: 0x000154BC
		// (remove) Token: 0x06000BF0 RID: 3056 RVA: 0x000172F4 File Offset: 0x000154F4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00017329 File Offset: 0x00015529
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000569 RID: 1385
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Extension;

		// Token: 0x0400056A RID: 1386
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ParameterValue> _ParameterValues = new ObservableCollection<ParameterValue>();
	}
}
