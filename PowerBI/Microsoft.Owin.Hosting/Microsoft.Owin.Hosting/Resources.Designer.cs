using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Owin.Hosting
{
	// Token: 0x02000006 RID: 6
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002B99 File Offset: 0x00000D99
		internal Resources()
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002BA4 File Offset: 0x00000DA4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					ResourceManager temp = new ResourceManager("Microsoft.Owin.Hosting.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = temp;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002BDD File Offset: 0x00000DDD
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002BE4 File Offset: 0x00000DE4
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

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002BEC File Offset: 0x00000DEC
		internal static string Exception_AppLoadFailure
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_AppLoadFailure", Resources.resourceCulture);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002C02 File Offset: 0x00000E02
		internal static string Exception_FailedToResolveService
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_FailedToResolveService", Resources.resourceCulture);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002C18 File Offset: 0x00000E18
		internal static string Exception_ImproperlyFormattedSettingsFile
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_ImproperlyFormattedSettingsFile", Resources.resourceCulture);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002C2E File Offset: 0x00000E2E
		internal static string Exception_ServerFactoryParameterCount
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_ServerFactoryParameterCount", Resources.resourceCulture);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002C44 File Offset: 0x00000E44
		internal static string Exception_ServerFactoryParameterType
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_ServerFactoryParameterType", Resources.resourceCulture);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002C5A File Offset: 0x00000E5A
		internal static string Exception_ServerNotFound
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_ServerNotFound", Resources.resourceCulture);
			}
		}

		// Token: 0x0400001F RID: 31
		private static ResourceManager resourceMan;

		// Token: 0x04000020 RID: 32
		private static CultureInfo resourceCulture;
	}
}
