using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000008 RID: 8
	[OriginalName("DataSetParameter")]
	public class DataSetParameter : INotifyPropertyChanged
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002868 File Offset: 0x00000A68
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002870 File Offset: 0x00000A70
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002884 File Offset: 0x00000A84
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000288C File Offset: 0x00000A8C
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

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000053 RID: 83 RVA: 0x000028A0 File Offset: 0x00000AA0
		// (remove) Token: 0x06000054 RID: 84 RVA: 0x000028D8 File Offset: 0x00000AD8
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000055 RID: 85 RVA: 0x0000290D File Offset: 0x00000B0D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x04000059 RID: 89
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Name;

		// Token: 0x0400005A RID: 90
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Value;
	}
}
