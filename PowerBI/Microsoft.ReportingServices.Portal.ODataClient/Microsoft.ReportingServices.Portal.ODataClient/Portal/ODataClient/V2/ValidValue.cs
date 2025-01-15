using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000076 RID: 118
	[OriginalName("ValidValue")]
	public class ValidValue : INotifyPropertyChanged
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0000AA49 File Offset: 0x00008C49
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x0000AA51 File Offset: 0x00008C51
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

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0000AA65 File Offset: 0x00008C65
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x0000AA6D File Offset: 0x00008C6D
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

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000534 RID: 1332 RVA: 0x0000AA84 File Offset: 0x00008C84
		// (remove) Token: 0x06000535 RID: 1333 RVA: 0x0000AABC File Offset: 0x00008CBC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000536 RID: 1334 RVA: 0x0000AAF1 File Offset: 0x00008CF1
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400025E RID: 606
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Label;

		// Token: 0x0400025F RID: 607
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;
	}
}
