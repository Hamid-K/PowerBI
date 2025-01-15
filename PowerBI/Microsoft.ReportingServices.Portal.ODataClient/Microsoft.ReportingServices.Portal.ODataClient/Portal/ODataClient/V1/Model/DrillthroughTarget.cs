using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000AD RID: 173
	[OriginalName("DrillthroughTarget")]
	public abstract class DrillthroughTarget : INotifyPropertyChanged
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0000ED97 File Offset: 0x0000CF97
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x0000ED9F File Offset: 0x0000CF9F
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Type")]
		public DrillthroughTargetType Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.OnPropertyChanged("Type");
			}
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x0600072A RID: 1834 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		// (remove) Token: 0x0600072B RID: 1835 RVA: 0x0000EDEC File Offset: 0x0000CFEC
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600072C RID: 1836 RVA: 0x0000EE21 File Offset: 0x0000D021
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0400037F RID: 895
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private DrillthroughTargetType _Type;
	}
}
