using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Owin.Host.HttpListener
{
	// Token: 0x0200000A RID: 10
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002C27 File Offset: 0x00000E27
		internal Resources()
		{
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002C30 File Offset: 0x00000E30
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					ResourceManager temp = new ResourceManager("Microsoft.Owin.Host.HttpListener.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = temp;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002C69 File Offset: 0x00000E69
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002C70 File Offset: 0x00000E70
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002C78 File Offset: 0x00000E78
		internal static string Exception_DuplicateKey
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_DuplicateKey", Resources.resourceCulture);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002C8E File Offset: 0x00000E8E
		internal static string Exception_ResponseAlreadySent
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_ResponseAlreadySent", Resources.resourceCulture);
			}
		}

		// Token: 0x04000049 RID: 73
		private static ResourceManager resourceMan;

		// Token: 0x0400004A RID: 74
		private static CultureInfo resourceCulture;
	}
}
