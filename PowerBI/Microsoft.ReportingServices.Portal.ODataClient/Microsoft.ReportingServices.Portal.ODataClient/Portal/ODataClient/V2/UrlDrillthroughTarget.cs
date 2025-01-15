using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200000D RID: 13
	[OriginalName("UrlDrillthroughTarget")]
	public class UrlDrillthroughTarget : DrillthroughTarget
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00002B61 File Offset: 0x00000D61
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static UrlDrillthroughTarget CreateUrlDrillthroughTarget(DrillthroughTargetType type, bool directNavigation)
		{
			return new UrlDrillthroughTarget
			{
				Type = type,
				DirectNavigation = directNavigation
			};
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002B76 File Offset: 0x00000D76
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002B7E File Offset: 0x00000D7E
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

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002B92 File Offset: 0x00000D92
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002B9A File Offset: 0x00000D9A
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

		// Token: 0x04000066 RID: 102
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _Url;

		// Token: 0x04000067 RID: 103
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private bool _DirectNavigation;
	}
}
