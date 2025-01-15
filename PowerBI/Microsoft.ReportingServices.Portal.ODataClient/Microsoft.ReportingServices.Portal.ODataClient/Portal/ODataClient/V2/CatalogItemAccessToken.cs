using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200001D RID: 29
	[OriginalName("CatalogItemAccessToken")]
	public class CatalogItemAccessToken : INotifyPropertyChanged
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00003B90 File Offset: 0x00001D90
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00003B98 File Offset: 0x00001D98
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Token")]
		public byte[] Token
		{
			get
			{
				return this._Token;
			}
			set
			{
				this._Token = value;
				this.OnPropertyChanged("Token");
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000132 RID: 306 RVA: 0x00003BAC File Offset: 0x00001DAC
		// (remove) Token: 0x06000133 RID: 307 RVA: 0x00003BE4 File Offset: 0x00001DE4
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000134 RID: 308 RVA: 0x00003C19 File Offset: 0x00001E19
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x040000B0 RID: 176
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private byte[] _Token;
	}
}
