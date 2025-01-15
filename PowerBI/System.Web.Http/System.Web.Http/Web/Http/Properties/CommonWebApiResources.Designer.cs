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
	// Token: 0x020000BE RID: 190
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class CommonWebApiResources
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x00003AA7 File Offset: 0x00001CA7
		internal CommonWebApiResources()
		{
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
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

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0000D96E File Offset: 0x0000BB6E
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x0000D975 File Offset: 0x0000BB75
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

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0000D97D File Offset: 0x0000BB7D
		internal static string ArgumentInvalidAbsoluteUri
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentInvalidAbsoluteUri", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0000D993 File Offset: 0x0000BB93
		internal static string ArgumentInvalidHttpUriScheme
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentInvalidHttpUriScheme", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0000D9A9 File Offset: 0x0000BBA9
		internal static string ArgumentMustBeGreaterThanOrEqualTo
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentMustBeGreaterThanOrEqualTo", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0000D9BF File Offset: 0x0000BBBF
		internal static string ArgumentMustBeLessThanOrEqualTo
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentMustBeLessThanOrEqualTo", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0000D9D5 File Offset: 0x0000BBD5
		internal static string ArgumentNullOrEmpty
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentNullOrEmpty", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000D9EB File Offset: 0x0000BBEB
		internal static string ArgumentUriHasQueryOrFragment
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentUriHasQueryOrFragment", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0000DA01 File Offset: 0x0000BC01
		internal static string InvalidEnumArgument
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("InvalidEnumArgument", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x0400012B RID: 299
		private static ResourceManager resourceMan;

		// Token: 0x0400012C RID: 300
		private static CultureInfo resourceCulture;
	}
}
