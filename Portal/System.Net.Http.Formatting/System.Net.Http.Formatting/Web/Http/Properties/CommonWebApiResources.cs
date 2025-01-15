using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Web.Http.Properties
{
	// Token: 0x0200005F RID: 95
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class CommonWebApiResources
	{
		// Token: 0x0600037B RID: 891 RVA: 0x00004BD2 File Offset: 0x00002DD2
		internal CommonWebApiResources()
		{
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (CommonWebApiResources.resourceMan == null)
				{
					Assembly assembly = typeof(CommonWebApiResources).Assembly;
					string text = (from s in assembly.GetManifestResourceNames()
						where s.EndsWith("CommonWebApiResources.resources", StringComparison.OrdinalIgnoreCase)
						select s).Single<string>();
					text = text.Substring(0, text.Length - 10);
					CommonWebApiResources.resourceMan = new ResourceManager(text, assembly);
				}
				return CommonWebApiResources.resourceMan;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000CA66 File Offset: 0x0000AC66
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000CA6D File Offset: 0x0000AC6D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return CommonWebApiResources.resourceCulture;
			}
			set
			{
				CommonWebApiResources.resourceCulture = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000CA75 File Offset: 0x0000AC75
		internal static string ArgumentInvalidAbsoluteUri
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentInvalidAbsoluteUri", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000CA8B File Offset: 0x0000AC8B
		internal static string ArgumentInvalidHttpUriScheme
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentInvalidHttpUriScheme", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000CAA1 File Offset: 0x0000ACA1
		internal static string ArgumentMustBeGreaterThanOrEqualTo
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentMustBeGreaterThanOrEqualTo", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000CAB7 File Offset: 0x0000ACB7
		internal static string ArgumentMustBeLessThanOrEqualTo
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentMustBeLessThanOrEqualTo", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000CACD File Offset: 0x0000ACCD
		internal static string ArgumentNullOrEmpty
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentNullOrEmpty", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000CAE3 File Offset: 0x0000ACE3
		internal static string ArgumentUriHasQueryOrFragment
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentUriHasQueryOrFragment", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000CAF9 File Offset: 0x0000ACF9
		internal static string InvalidEnumArgument
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("InvalidEnumArgument", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x04000136 RID: 310
		private static ResourceManager resourceMan;

		// Token: 0x04000137 RID: 311
		private static CultureInfo resourceCulture;
	}
}
