using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Owin
{
	// Token: 0x0200001A RID: 26
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000148 RID: 328 RVA: 0x000037B6 File Offset: 0x000019B6
		internal Resources()
		{
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000037C0 File Offset: 0x000019C0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					ResourceManager temp = new ResourceManager("Microsoft.Owin.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = temp;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000037F9 File Offset: 0x000019F9
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00003800 File Offset: 0x00001A00
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

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00003808 File Offset: 0x00001A08
		internal static string Exception_ConversionTakesOneParameter
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_ConversionTakesOneParameter", Resources.resourceCulture);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000381E File Offset: 0x00001A1E
		internal static string Exception_CookieLimitTooSmall
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_CookieLimitTooSmall", Resources.resourceCulture);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00003834 File Offset: 0x00001A34
		internal static string Exception_ImcompleteChunkedCookie
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_ImcompleteChunkedCookie", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000384A File Offset: 0x00001A4A
		internal static string Exception_MiddlewareNotSupported
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_MiddlewareNotSupported", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00003860 File Offset: 0x00001A60
		internal static string Exception_MissingOnSendingHeaders
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_MissingOnSendingHeaders", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00003876 File Offset: 0x00001A76
		internal static string Exception_NoConstructorFound
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_NoConstructorFound", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000388C File Offset: 0x00001A8C
		internal static string Exception_NoConversionExists
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_NoConversionExists", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000038A2 File Offset: 0x00001AA2
		internal static string Exception_PathMustNotEndWithSlash
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_PathMustNotEndWithSlash", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000038B8 File Offset: 0x00001AB8
		internal static string Exception_PathMustStartWithSlash
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_PathMustStartWithSlash", Resources.resourceCulture);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000038CE File Offset: 0x00001ACE
		internal static string Exception_PathRequired
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_PathRequired", Resources.resourceCulture);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000038E4 File Offset: 0x00001AE4
		internal static string Exception_QueryStringMustStartWithDelimiter
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_QueryStringMustStartWithDelimiter", Resources.resourceCulture);
			}
		}

		// Token: 0x04000033 RID: 51
		private static ResourceManager resourceMan;

		// Token: 0x04000034 RID: 52
		private static CultureInfo resourceCulture;
	}
}
