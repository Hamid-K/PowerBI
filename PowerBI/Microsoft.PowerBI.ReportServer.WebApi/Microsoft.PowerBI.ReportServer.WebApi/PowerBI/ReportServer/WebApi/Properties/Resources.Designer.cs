using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.ReportServer.WebApi.Properties
{
	// Token: 0x02000010 RID: 16
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002C9D File Offset: 0x00000E9D
		internal Resources()
		{
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002CCC File Offset: 0x00000ECC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Microsoft.PowerBI.ReportServer.WebApi.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002CF8 File Offset: 0x00000EF8
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002CFF File Offset: 0x00000EFF
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002D07 File Offset: 0x00000F07
		internal static byte[] Template
		{
			get
			{
				return (byte[])Resources.ResourceManager.GetObject("Template", Resources.resourceCulture);
			}
		}

		// Token: 0x0400003A RID: 58
		private static ResourceManager resourceMan;

		// Token: 0x0400003B RID: 59
		private static CultureInfo resourceCulture;
	}
}
