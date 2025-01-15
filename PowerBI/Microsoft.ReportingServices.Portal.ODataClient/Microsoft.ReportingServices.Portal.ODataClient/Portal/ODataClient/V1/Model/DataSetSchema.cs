using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000C6 RID: 198
	[OriginalName("DataSetSchema")]
	public class DataSetSchema : INotifyPropertyChanged
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x000122C0 File Offset: 0x000104C0
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x000122C8 File Offset: 0x000104C8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Name")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.OnPropertyChanged("Name");
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x000122DC File Offset: 0x000104DC
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x000122E4 File Offset: 0x000104E4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Fields")]
		public ObservableCollection<DataSetField> Fields
		{
			get
			{
				return this._Fields;
			}
			set
			{
				this._Fields = value;
				this.OnPropertyChanged("Fields");
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x000122F8 File Offset: 0x000104F8
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x00012300 File Offset: 0x00010500
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Parameters")]
		public ObservableCollection<DataSetParameterInfo> Parameters
		{
			get
			{
				return this._Parameters;
			}
			set
			{
				this._Parameters = value;
				this.OnPropertyChanged("Parameters");
			}
		}

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x060008D0 RID: 2256 RVA: 0x00012314 File Offset: 0x00010514
		// (remove) Token: 0x060008D1 RID: 2257 RVA: 0x0001234C File Offset: 0x0001054C
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060008D2 RID: 2258 RVA: 0x00012381 File Offset: 0x00010581
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400042F RID: 1071
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x04000430 RID: 1072
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<DataSetField> _Fields = new ObservableCollection<DataSetField>();

		// Token: 0x04000431 RID: 1073
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<DataSetParameterInfo> _Parameters = new ObservableCollection<DataSetParameterInfo>();
	}
}
