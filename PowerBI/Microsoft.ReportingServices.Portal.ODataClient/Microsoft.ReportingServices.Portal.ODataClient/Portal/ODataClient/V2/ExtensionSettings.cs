using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000071 RID: 113
	[OriginalName("ExtensionSettings")]
	public class ExtensionSettings : INotifyPropertyChanged
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000A5D4 File Offset: 0x000087D4
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x0000A5DC File Offset: 0x000087DC
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

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0000A5F0 File Offset: 0x000087F0
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x0000A5F8 File Offset: 0x000087F8
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

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000511 RID: 1297 RVA: 0x0000A60C File Offset: 0x0000880C
		// (remove) Token: 0x06000512 RID: 1298 RVA: 0x0000A644 File Offset: 0x00008844
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000513 RID: 1299 RVA: 0x0000A679 File Offset: 0x00008879
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400024F RID: 591
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Extension;

		// Token: 0x04000250 RID: 592
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<ParameterValue> _ParameterValues = new ObservableCollection<ParameterValue>();
	}
}
