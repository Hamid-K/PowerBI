using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000019 RID: 25
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Microsoft_DataIntegration_Common_Resources
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00002656 File Offset: 0x00000856
		internal Microsoft_DataIntegration_Common_Resources()
		{
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000265E File Offset: 0x0000085E
		[EditorBrowsable(2)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Microsoft_DataIntegration_Common_Resources.resourceMan == null)
				{
					Microsoft_DataIntegration_Common_Resources.resourceMan = new ResourceManager("Microsoft.DataIntegration.FuzzyMatchingCommon.Microsoft.DataIntegration.Common.Resources", typeof(Microsoft_DataIntegration_Common_Resources).Assembly);
				}
				return Microsoft_DataIntegration_Common_Resources.resourceMan;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000268A File Offset: 0x0000088A
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002691 File Offset: 0x00000891
		[EditorBrowsable(2)]
		internal static CultureInfo Culture
		{
			get
			{
				return Microsoft_DataIntegration_Common_Resources.resourceCulture;
			}
			set
			{
				Microsoft_DataIntegration_Common_Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002699 File Offset: 0x00000899
		internal static string HashKeyAlreadyPresent
		{
			get
			{
				return Microsoft_DataIntegration_Common_Resources.ResourceManager.GetString("HashKeyAlreadyPresent", Microsoft_DataIntegration_Common_Resources.resourceCulture);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000026AF File Offset: 0x000008AF
		internal static string HashKeyNotPresent
		{
			get
			{
				return Microsoft_DataIntegration_Common_Resources.ResourceManager.GetString("HashKeyNotPresent", Microsoft_DataIntegration_Common_Resources.resourceCulture);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000026C5 File Offset: 0x000008C5
		internal static string HashSetSizeTooLarge
		{
			get
			{
				return Microsoft_DataIntegration_Common_Resources.ResourceManager.GetString("HashSetSizeTooLarge", Microsoft_DataIntegration_Common_Resources.resourceCulture);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000026DB File Offset: 0x000008DB
		internal static string SqlNameIsNotValid
		{
			get
			{
				return Microsoft_DataIntegration_Common_Resources.ResourceManager.GetString("SqlNameIsNotValid", Microsoft_DataIntegration_Common_Resources.resourceCulture);
			}
		}

		// Token: 0x04000010 RID: 16
		private static ResourceManager resourceMan;

		// Token: 0x04000011 RID: 17
		private static CultureInfo resourceCulture;
	}
}
