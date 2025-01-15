using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AspNet.OData.Common
{
	// Token: 0x02000062 RID: 98
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class CommonWebApiResources
	{
		// Token: 0x060002AC RID: 684 RVA: 0x00002557 File Offset: 0x00000757
		internal CommonWebApiResources()
		{
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000AEA0 File Offset: 0x000090A0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (CommonWebApiResources.resourceMan == null)
				{
					Assembly assembly = TypeHelper.GetAssembly(typeof(CommonWebApiResources));
					string text = (from s in assembly.GetManifestResourceNames()
						where s.EndsWith("CommonWebApiResources.resources", StringComparison.OrdinalIgnoreCase)
						select s).Single<string>();
					text = text.Substring(0, text.Length - 10);
					CommonWebApiResources.resourceMan = new ResourceManager(text, assembly);
				}
				return CommonWebApiResources.resourceMan;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000AF16 File Offset: 0x00009116
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000AF1D File Offset: 0x0000911D
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

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000AF25 File Offset: 0x00009125
		internal static string ArgumentInvalidAbsoluteUri
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentInvalidAbsoluteUri", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000AF3B File Offset: 0x0000913B
		internal static string ArgumentInvalidHttpUriScheme
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentInvalidHttpUriScheme", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000AF51 File Offset: 0x00009151
		internal static string ArgumentMustBeGreaterThanOrEqualTo
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentMustBeGreaterThanOrEqualTo", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000AF67 File Offset: 0x00009167
		internal static string ArgumentMustBeLessThanOrEqualTo
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentMustBeLessThanOrEqualTo", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000AF7D File Offset: 0x0000917D
		internal static string ArgumentNullOrEmpty
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentNullOrEmpty", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000AF93 File Offset: 0x00009193
		internal static string ArgumentUriHasQueryOrFragment
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentUriHasQueryOrFragment", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000AFA9 File Offset: 0x000091A9
		internal static string InvalidEnumArgument
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("InvalidEnumArgument", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x040000BD RID: 189
		private static ResourceManager resourceMan;

		// Token: 0x040000BE RID: 190
		private static CultureInfo resourceCulture;
	}
}
