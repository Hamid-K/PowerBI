using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Owin.StaticFiles
{
	// Token: 0x0200000E RID: 14
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000027CB File Offset: 0x000009CB
		internal Resources()
		{
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000027D4 File Offset: 0x000009D4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					ResourceManager temp = new ResourceManager("Microsoft.Owin.StaticFiles.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = temp;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000280D File Offset: 0x00000A0D
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002814 File Offset: 0x00000A14
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000281C File Offset: 0x00000A1C
		internal static string Args_NoContentTypeProvider
		{
			get
			{
				return Resources.ResourceManager.GetString("Args_NoContentTypeProvider", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002832 File Offset: 0x00000A32
		internal static string Args_NoFormatter
		{
			get
			{
				return Resources.ResourceManager.GetString("Args_NoFormatter", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002848 File Offset: 0x00000A48
		internal static string Exception_SendFileNotSupported
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_SendFileNotSupported", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000285E File Offset: 0x00000A5E
		internal static string HtmlDir_IndexOf
		{
			get
			{
				return Resources.ResourceManager.GetString("HtmlDir_IndexOf", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002874 File Offset: 0x00000A74
		internal static string HtmlDir_LastModified
		{
			get
			{
				return Resources.ResourceManager.GetString("HtmlDir_LastModified", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000288A File Offset: 0x00000A8A
		internal static string HtmlDir_Modified
		{
			get
			{
				return Resources.ResourceManager.GetString("HtmlDir_Modified", Resources.resourceCulture);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000028A0 File Offset: 0x00000AA0
		internal static string HtmlDir_Name
		{
			get
			{
				return Resources.ResourceManager.GetString("HtmlDir_Name", Resources.resourceCulture);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000028B6 File Offset: 0x00000AB6
		internal static string HtmlDir_Size
		{
			get
			{
				return Resources.ResourceManager.GetString("HtmlDir_Size", Resources.resourceCulture);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000028CC File Offset: 0x00000ACC
		internal static string HtmlDir_TableSummary
		{
			get
			{
				return Resources.ResourceManager.GetString("HtmlDir_TableSummary", Resources.resourceCulture);
			}
		}

		// Token: 0x04000023 RID: 35
		private static ResourceManager resourceMan;

		// Token: 0x04000024 RID: 36
		private static CultureInfo resourceCulture;
	}
}
