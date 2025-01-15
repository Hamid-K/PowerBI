using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Web.Cors.Properties
{
	// Token: 0x02000008 RID: 8
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class SRResources
	{
		// Token: 0x06000041 RID: 65 RVA: 0x000026A8 File Offset: 0x000008A8
		internal SRResources()
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002C31 File Offset: 0x00000E31
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SRResources.resourceMan == null)
				{
					SRResources.resourceMan = new ResourceManager("System.Web.Cors.Properties.SRResources", typeof(SRResources).Assembly);
				}
				return SRResources.resourceMan;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002C5D File Offset: 0x00000E5D
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002C64 File Offset: 0x00000E64
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return SRResources.resourceCulture;
			}
			set
			{
				SRResources.resourceCulture = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002C6C File Offset: 0x00000E6C
		internal static string HeadersNotAllowed
		{
			get
			{
				return SRResources.ResourceManager.GetString("HeadersNotAllowed", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002C82 File Offset: 0x00000E82
		internal static string MethodNotAllowed
		{
			get
			{
				return SRResources.ResourceManager.GetString("MethodNotAllowed", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002C98 File Offset: 0x00000E98
		internal static string NoOriginHeader
		{
			get
			{
				return SRResources.ResourceManager.GetString("NoOriginHeader", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002CAE File Offset: 0x00000EAE
		internal static string OriginNotAllowed
		{
			get
			{
				return SRResources.ResourceManager.GetString("OriginNotAllowed", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002CC4 File Offset: 0x00000EC4
		internal static string PreflightMaxAgeOutOfRange
		{
			get
			{
				return SRResources.ResourceManager.GetString("PreflightMaxAgeOutOfRange", SRResources.resourceCulture);
			}
		}

		// Token: 0x04000026 RID: 38
		private static ResourceManager resourceMan;

		// Token: 0x04000027 RID: 39
		private static CultureInfo resourceCulture;
	}
}
