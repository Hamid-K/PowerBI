using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000024 RID: 36
	[OriginalName("DataSetSchema")]
	public class DataSetSchema : INotifyPropertyChanged
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000045C9 File Offset: 0x000027C9
		// (set) Token: 0x06000181 RID: 385 RVA: 0x000045D1 File Offset: 0x000027D1
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

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000182 RID: 386 RVA: 0x000045E5 File Offset: 0x000027E5
		// (set) Token: 0x06000183 RID: 387 RVA: 0x000045ED File Offset: 0x000027ED
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

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00004601 File Offset: 0x00002801
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00004609 File Offset: 0x00002809
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

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000186 RID: 390 RVA: 0x00004620 File Offset: 0x00002820
		// (remove) Token: 0x06000187 RID: 391 RVA: 0x00004658 File Offset: 0x00002858
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000188 RID: 392 RVA: 0x0000468D File Offset: 0x0000288D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040000D2 RID: 210
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x040000D3 RID: 211
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<DataSetField> _Fields = new ObservableCollection<DataSetField>();

		// Token: 0x040000D4 RID: 212
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private ObservableCollection<DataSetParameterInfo> _Parameters = new ObservableCollection<DataSetParameterInfo>();
	}
}
