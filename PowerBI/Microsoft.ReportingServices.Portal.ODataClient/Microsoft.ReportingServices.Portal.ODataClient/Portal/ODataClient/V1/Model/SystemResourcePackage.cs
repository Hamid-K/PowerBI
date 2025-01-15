using System;
using System.CodeDom.Compiler;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x0200011B RID: 283
	[Key("Id")]
	[OriginalName("SystemResourcePackage")]
	public class SystemResourcePackage : SystemResource
	{
		// Token: 0x06000C2D RID: 3117 RVA: 0x0001784F File Offset: 0x00015A4F
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

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0001786B File Offset: 0x00015A6B
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x00017873 File Offset: 0x00015A73
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

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00017887 File Offset: 0x00015A87
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x0001788F File Offset: 0x00015A8F
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

		// Token: 0x04000580 RID: 1408
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private byte[] _Content;

		// Token: 0x04000581 RID: 1409
		[GeneratedCode("Microsoft.OData.Client.Design.T4", "7.5.1")]
		private string _PackageFileName;
	}
}
