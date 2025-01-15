using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200008A RID: 138
	[Key("Id")]
	[OriginalName("SystemResourcePackage")]
	public class SystemResourcePackage : SystemResource
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x0000BBCA File Offset: 0x00009DCA
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		public static SystemResourcePackage CreateSystemResourcePackage(Guid ID, SystemResourceType type, bool isEmbedded)
		{
			return new SystemResourcePackage
			{
				Id = ID,
				Type = type,
				IsEmbedded = isEmbedded
			};
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0000BBE6 File Offset: 0x00009DE6
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x0000BBEE File Offset: 0x00009DEE
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("Content")]
		public byte[] Content
		{
			get
			{
				return this._Content;
			}
			set
			{
				this._Content = value;
				this.OnPropertyChanged("Content");
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0000BC02 File Offset: 0x00009E02
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x0000BC0A File Offset: 0x00009E0A
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		[OriginalName("PackageFileName")]
		public string PackageFileName
		{
			get
			{
				return this._PackageFileName;
			}
			set
			{
				this._PackageFileName = value;
				this.OnPropertyChanged("PackageFileName");
			}
		}

		// Token: 0x040002AC RID: 684
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private byte[] _Content;

		// Token: 0x040002AD RID: 685
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _PackageFileName;
	}
}
