using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Owin.Security.ActiveDirectory.Properties
{
	// Token: 0x02000007 RID: 7
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002451 File Offset: 0x00000651
		internal Resources()
		{
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000245C File Offset: 0x0000065C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					ResourceManager temp = new ResourceManager("Microsoft.Owin.Security.ActiveDirectory.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = temp;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002495 File Offset: 0x00000695
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000249C File Offset: 0x0000069C
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000024A4 File Offset: 0x000006A4
		internal static string Exception_MissingDescriptor
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_MissingDescriptor", Resources.resourceCulture);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000024BA File Offset: 0x000006BA
		internal static string Exception_OptionMustBeProvided
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_OptionMustBeProvided", Resources.resourceCulture);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000024D0 File Offset: 0x000006D0
		internal static string Exception_ValidatorHandlerMismatch
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_ValidatorHandlerMismatch", Resources.resourceCulture);
			}
		}

		// Token: 0x0400001E RID: 30
		private static ResourceManager resourceMan;

		// Token: 0x0400001F RID: 31
		private static CultureInfo resourceCulture;
	}
}
