using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000FE RID: 254
	[OriginalName("ValidValue")]
	public class ValidValue : INotifyPropertyChanged
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00015E75 File Offset: 0x00014075
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x00015E7D File Offset: 0x0001407D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Label")]
		public string Label
		{
			get
			{
				return this._Label;
			}
			set
			{
				this._Label = value;
				this.OnPropertyChanged("Label");
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00015E91 File Offset: 0x00014091
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x00015E99 File Offset: 0x00014099
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Value")]
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				this._Value = value;
				this.OnPropertyChanged("Value");
			}
		}

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x06000B18 RID: 2840 RVA: 0x00015EB0 File Offset: 0x000140B0
		// (remove) Token: 0x06000B19 RID: 2841 RVA: 0x00015EE8 File Offset: 0x000140E8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000B1A RID: 2842 RVA: 0x00015F1D File Offset: 0x0001411D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000511 RID: 1297
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Label;

		// Token: 0x04000512 RID: 1298
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;
	}
}
