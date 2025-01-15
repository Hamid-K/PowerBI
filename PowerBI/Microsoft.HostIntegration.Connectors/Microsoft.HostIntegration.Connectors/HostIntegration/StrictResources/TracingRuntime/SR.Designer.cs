using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.StrictResources.TracingRuntime
{
	// Token: 0x0200066A RID: 1642
	internal class SR
	{
		// Token: 0x060036DE RID: 14046 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x060036DF RID: 14047 RVA: 0x000B921C File Offset: 0x000B741C
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.StrictResources.TracingRuntime.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x060036E0 RID: 14048 RVA: 0x000B9248 File Offset: 0x000B7448
		// (set) Token: 0x060036E1 RID: 14049 RVA: 0x000B924F File Offset: 0x000B744F
		internal static CultureInfo Culture
		{
			get
			{
				return SR.resourceCulture;
			}
			set
			{
				SR.resourceCulture = value;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x060036E2 RID: 14050 RVA: 0x000B9257 File Offset: 0x000B7457
		internal static string ContainerNodeNameAttribute
		{
			get
			{
				return SR.ResourceManager.GetString("ContainerNodeNameAttribute", SR.Culture);
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x060036E3 RID: 14051 RVA: 0x000B926D File Offset: 0x000B746D
		internal static string ContainerNodeInstanceNameAttribute
		{
			get
			{
				return SR.ResourceManager.GetString("ContainerNodeInstanceNameAttribute", SR.Culture);
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x060036E4 RID: 14052 RVA: 0x000B9283 File Offset: 0x000B7483
		internal static string ContainerNodeInstanceNameAttributeIllegal
		{
			get
			{
				return SR.ResourceManager.GetString("ContainerNodeInstanceNameAttributeIllegal", SR.Culture);
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x060036E5 RID: 14053 RVA: 0x000B9299 File Offset: 0x000B7499
		internal static string EmptyFolderName
		{
			get
			{
				return SR.ResourceManager.GetString("EmptyFolderName", SR.Culture);
			}
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000B92AF File Offset: 0x000B74AF
		internal static string UnknownTracepointPropertyInContainer(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("UnknownTracepointPropertyInContainer", SR.Culture), param0, param1);
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000B92D1 File Offset: 0x000B74D1
		internal static string UnknownTracepointPropertyIdentifierInContainer(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("UnknownTracepointPropertyIdentifierInContainer", SR.Culture), param0, param1);
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000B92F3 File Offset: 0x000B74F3
		internal static string UnknownTracepointIdentifierInContainer(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("UnknownTracepointIdentifierInContainer", SR.Culture), param0, param1);
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x000B9315 File Offset: 0x000B7515
		internal static string CantOpenHitd(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("CantOpenHitd", SR.Culture), param0, param1);
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x000B9337 File Offset: 0x000B7537
		internal static string ProblemsWithTraceFileFolder(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("ProblemsWithTraceFileFolder", SR.Culture), param0, param1);
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000B9359 File Offset: 0x000B7559
		internal static string DirectoryNameInvalid(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DirectoryNameInvalid", SR.Culture), param0);
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000B937A File Offset: 0x000B757A
		internal static string DirectoryDoesntExist(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DirectoryDoesntExist", SR.Culture), param0);
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x000B939B File Offset: 0x000B759B
		internal static string WritePermissions(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("WritePermissions", SR.Culture), param0);
		}

		// Token: 0x04001F7F RID: 8063
		private static ResourceManager resourceManager;

		// Token: 0x04001F80 RID: 8064
		private static CultureInfo resourceCulture;
	}
}
