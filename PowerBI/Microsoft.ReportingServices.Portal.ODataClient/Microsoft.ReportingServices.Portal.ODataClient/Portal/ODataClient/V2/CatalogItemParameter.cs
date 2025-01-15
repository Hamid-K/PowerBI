using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200000C RID: 12
	[OriginalName("CatalogItemParameter")]
	public class CatalogItemParameter : INotifyPropertyChanged
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002AA0 File Offset: 0x00000CA0
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002AA8 File Offset: 0x00000CA8
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

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002ABC File Offset: 0x00000CBC
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002AC4 File Offset: 0x00000CC4
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

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600006F RID: 111 RVA: 0x00002AD8 File Offset: 0x00000CD8
		// (remove) Token: 0x06000070 RID: 112 RVA: 0x00002B10 File Offset: 0x00000D10
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000071 RID: 113 RVA: 0x00002B45 File Offset: 0x00000D45
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000063 RID: 99
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x04000064 RID: 100
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;
	}
}
