using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x020000B0 RID: 176
	[OriginalName("UrlDrillthroughTarget")]
	public class UrlDrillthroughTarget : DrillthroughTarget
	{
		// Token: 0x06000740 RID: 1856 RVA: 0x0000EF9D File Offset: 0x0000D19D
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static UrlDrillthroughTarget CreateUrlDrillthroughTarget(DrillthroughTargetType type, bool directNavigation)
		{
			return new UrlDrillthroughTarget
			{
				Type = type,
				DirectNavigation = directNavigation
			};
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0000EFB2 File Offset: 0x0000D1B2
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x0000EFBA File Offset: 0x0000D1BA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Url")]
		public string Url
		{
			get
			{
				return this._Url;
			}
			set
			{
				this._Url = value;
				this.OnPropertyChanged("Url");
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0000EFCE File Offset: 0x0000D1CE
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x0000EFD6 File Offset: 0x0000D1D6
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("DirectNavigation")]
		public bool DirectNavigation
		{
			get
			{
				return this._DirectNavigation;
			}
			set
			{
				this._DirectNavigation = value;
				this.OnPropertyChanged("DirectNavigation");
			}
		}

		// Token: 0x04000388 RID: 904
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Url;

		// Token: 0x04000389 RID: 905
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _DirectNavigation;
	}
}
