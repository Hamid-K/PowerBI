using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Owin.Security
{
	// Token: 0x0200000D RID: 13
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000024BA File Offset: 0x000006BA
		internal Resources()
		{
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000024C4 File Offset: 0x000006C4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					ResourceManager temp = new ResourceManager("Microsoft.Owin.Security.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = temp;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000024FD File Offset: 0x000006FD
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002504 File Offset: 0x00000704
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000250C File Offset: 0x0000070C
		internal static string Exception_AuthenticationTokenDoesNotProvideSyncMethods
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_AuthenticationTokenDoesNotProvideSyncMethods", Resources.resourceCulture);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002522 File Offset: 0x00000722
		internal static string Exception_DefaultDpapiRequiresAppNameKey
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_DefaultDpapiRequiresAppNameKey", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002538 File Offset: 0x00000738
		internal static string Exception_MissingDefaultSignInAsAuthenticationType
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_MissingDefaultSignInAsAuthenticationType", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000254E File Offset: 0x0000074E
		internal static string Exception_UnhookAuthenticationStateType
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_UnhookAuthenticationStateType", Resources.resourceCulture);
			}
		}

		// Token: 0x04000010 RID: 16
		private static ResourceManager resourceMan;

		// Token: 0x04000011 RID: 17
		private static CultureInfo resourceCulture;
	}
}
