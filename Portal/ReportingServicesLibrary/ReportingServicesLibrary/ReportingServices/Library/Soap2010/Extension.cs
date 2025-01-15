using System;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library.Soap2010
{
	// Token: 0x020002F0 RID: 752
	public class Extension
	{
		// Token: 0x06001ADF RID: 6879 RVA: 0x0006CB50 File Offset: 0x0006AD50
		public Extension()
		{
			this.ExtensionTypeName = ExtensionTypeEnum.All.ToString();
			this.Name = null;
			this.LocalizedName = null;
			this.Visible = false;
			this.IsModelGenerationSupported = false;
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0006CB94 File Offset: 0x0006AD94
		internal static Microsoft.ReportingServices.Library.Soap2010.Extension Soap2005ExtensionToThis(Microsoft.ReportingServices.Library.Soap2005.Extension ext2005)
		{
			if (ext2005 == null)
			{
				return null;
			}
			return new Microsoft.ReportingServices.Library.Soap2010.Extension
			{
				ExtensionTypeName = ext2005.ExtensionType.ToString(),
				Name = ext2005.Name,
				LocalizedName = ext2005.LocalizedName,
				Visible = ext2005.Visible,
				IsModelGenerationSupported = ext2005.IsModelGenerationSupported
			};
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0006CBF4 File Offset: 0x0006ADF4
		internal static Microsoft.ReportingServices.Library.Soap2010.Extension[] Soap2005ExtensionToThisArray(Microsoft.ReportingServices.Library.Soap2005.Extension[] exts)
		{
			if (exts == null)
			{
				return null;
			}
			Microsoft.ReportingServices.Library.Soap2010.Extension[] array = new Microsoft.ReportingServices.Library.Soap2010.Extension[exts.Length];
			for (int i = 0; i < exts.Length; i++)
			{
				array[i] = Microsoft.ReportingServices.Library.Soap2010.Extension.Soap2005ExtensionToThis(exts[i]);
			}
			return array;
		}

		// Token: 0x040009D0 RID: 2512
		public string ExtensionTypeName;

		// Token: 0x040009D1 RID: 2513
		public string Name;

		// Token: 0x040009D2 RID: 2514
		public string LocalizedName;

		// Token: 0x040009D3 RID: 2515
		public bool Visible;

		// Token: 0x040009D4 RID: 2516
		public bool IsModelGenerationSupported;
	}
}
