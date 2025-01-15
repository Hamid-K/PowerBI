using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SharedResourceNamespace
{
	// Token: 0x0200002E RID: 46
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class LoaderResources
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00004AEC File Offset: 0x00002CEC
		internal LoaderResources()
		{
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004AF4 File Offset: 0x00002CF4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (LoaderResources.resourceMan == null)
				{
					ResourceManager temp = new ResourceManager("Microsoft.Owin.Hosting.App_Packages.Owin.Loader.LoaderResources", typeof(LoaderResources).Assembly);
					LoaderResources.resourceMan = temp;
				}
				return LoaderResources.resourceMan;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004B2D File Offset: 0x00002D2D
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004B34 File Offset: 0x00002D34
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return LoaderResources.resourceCulture;
			}
			set
			{
				LoaderResources.resourceCulture = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004B3C File Offset: 0x00002D3C
		internal static string AssemblyNotFound
		{
			get
			{
				return LoaderResources.ResourceManager.GetString("AssemblyNotFound", LoaderResources.resourceCulture);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004B52 File Offset: 0x00002D52
		internal static string ClassNotFoundInAssembly
		{
			get
			{
				return LoaderResources.ResourceManager.GetString("ClassNotFoundInAssembly", LoaderResources.resourceCulture);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004B68 File Offset: 0x00002D68
		internal static string Exception_AttributeNameConflict
		{
			get
			{
				return LoaderResources.ResourceManager.GetString("Exception_AttributeNameConflict", LoaderResources.resourceCulture);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004B7E File Offset: 0x00002D7E
		internal static string Exception_StartupTypeConflict
		{
			get
			{
				return LoaderResources.ResourceManager.GetString("Exception_StartupTypeConflict", LoaderResources.resourceCulture);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004B94 File Offset: 0x00002D94
		internal static string FriendlyNameMismatch
		{
			get
			{
				return LoaderResources.ResourceManager.GetString("FriendlyNameMismatch", LoaderResources.resourceCulture);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004BAA File Offset: 0x00002DAA
		internal static string MethodNotFoundInClass
		{
			get
			{
				return LoaderResources.ResourceManager.GetString("MethodNotFoundInClass", LoaderResources.resourceCulture);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004BC0 File Offset: 0x00002DC0
		internal static string NoAssemblyWithStartupClass
		{
			get
			{
				return LoaderResources.ResourceManager.GetString("NoAssemblyWithStartupClass", LoaderResources.resourceCulture);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004BD6 File Offset: 0x00002DD6
		internal static string NoOwinStartupAttribute
		{
			get
			{
				return LoaderResources.ResourceManager.GetString("NoOwinStartupAttribute", LoaderResources.resourceCulture);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004BEC File Offset: 0x00002DEC
		internal static string StartupTypePropertyEmpty
		{
			get
			{
				return LoaderResources.ResourceManager.GetString("StartupTypePropertyEmpty", LoaderResources.resourceCulture);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004C02 File Offset: 0x00002E02
		internal static string StartupTypePropertyMissing
		{
			get
			{
				return LoaderResources.ResourceManager.GetString("StartupTypePropertyMissing", LoaderResources.resourceCulture);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004C18 File Offset: 0x00002E18
		internal static string TypeOrMethodNotFound
		{
			get
			{
				return LoaderResources.ResourceManager.GetString("TypeOrMethodNotFound", LoaderResources.resourceCulture);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004C2E File Offset: 0x00002E2E
		internal static string UnexpectedMethodSignature
		{
			get
			{
				return LoaderResources.ResourceManager.GetString("UnexpectedMethodSignature", LoaderResources.resourceCulture);
			}
		}

		// Token: 0x0400004B RID: 75
		private static ResourceManager resourceMan;

		// Token: 0x0400004C RID: 76
		private static CultureInfo resourceCulture;
	}
}
