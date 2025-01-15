using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.StrictResources.TracingGlobals
{
	// Token: 0x02000668 RID: 1640
	internal class SR
	{
		// Token: 0x060036C2 RID: 14018 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x060036C3 RID: 14019 RVA: 0x000B8F10 File Offset: 0x000B7110
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.StrictResources.TracingGlobals.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x060036C4 RID: 14020 RVA: 0x000B8F3C File Offset: 0x000B713C
		// (set) Token: 0x060036C5 RID: 14021 RVA: 0x000B8F43 File Offset: 0x000B7143
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

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x060036C6 RID: 14022 RVA: 0x000B8F4B File Offset: 0x000B714B
		internal static string AndOrNodesHaveChildren
		{
			get
			{
				return SR.ResourceManager.GetString("AndOrNodesHaveChildren", SR.Culture);
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x060036C7 RID: 14023 RVA: 0x000B8F61 File Offset: 0x000B7161
		internal static string AndOrNodesNotAttribute
		{
			get
			{
				return SR.ResourceManager.GetString("AndOrNodesNotAttribute", SR.Culture);
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x060036C8 RID: 14024 RVA: 0x000B8F77 File Offset: 0x000B7177
		internal static string EqualNodePropertyAttribute
		{
			get
			{
				return SR.ResourceManager.GetString("EqualNodePropertyAttribute", SR.Culture);
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x060036C9 RID: 14025 RVA: 0x000B8F8D File Offset: 0x000B718D
		internal static string EqualNodeValueAttribute
		{
			get
			{
				return SR.ResourceManager.GetString("EqualNodeValueAttribute", SR.Culture);
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x060036CA RID: 14026 RVA: 0x000B8FA3 File Offset: 0x000B71A3
		internal static string EqualNodeAllAttributes
		{
			get
			{
				return SR.ResourceManager.GetString("EqualNodeAllAttributes", SR.Culture);
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x060036CB RID: 14027 RVA: 0x000B8FB9 File Offset: 0x000B71B9
		internal static string EqualNodeNoChildren
		{
			get
			{
				return SR.ResourceManager.GetString("EqualNodeNoChildren", SR.Culture);
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x060036CC RID: 14028 RVA: 0x000B8FCF File Offset: 0x000B71CF
		internal static string TracepointNameAttribute
		{
			get
			{
				return SR.ResourceManager.GetString("TracepointNameAttribute", SR.Culture);
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x060036CD RID: 14029 RVA: 0x000B8FE5 File Offset: 0x000B71E5
		internal static string TraceLevelAttribute
		{
			get
			{
				return SR.ResourceManager.GetString("TraceLevelAttribute", SR.Culture);
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x060036CE RID: 14030 RVA: 0x000B8FFB File Offset: 0x000B71FB
		internal static string ContainerMaximumDataBytesAttribute
		{
			get
			{
				return SR.ResourceManager.GetString("ContainerMaximumDataBytesAttribute", SR.Culture);
			}
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x000B9011 File Offset: 0x000B7211
		internal static string EnumerationValueProperty(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("EnumerationValueProperty", SR.Culture), param0, param1);
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x000B9033 File Offset: 0x000B7233
		internal static string EnumerationValuePropertyInteger(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("EnumerationValuePropertyInteger", SR.Culture), param0, param1);
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x000B9055 File Offset: 0x000B7255
		internal static string PropertyNonStringIncorrectlySet(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("PropertyNonStringIncorrectlySet", SR.Culture), param0);
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x000B9076 File Offset: 0x000B7276
		internal static string PropertyStringIncorrectlySet(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("PropertyStringIncorrectlySet", SR.Culture), param0);
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x000B9097 File Offset: 0x000B7297
		internal static string UnknownDecisionTreeNode(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("UnknownDecisionTreeNode", SR.Culture), param0);
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000B90B8 File Offset: 0x000B72B8
		internal static string TopLevelTracepoint(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("TopLevelTracepoint", SR.Culture), param0);
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x000B90D9 File Offset: 0x000B72D9
		internal static string UnknownTracepointInContainer(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("UnknownTracepointInContainer", SR.Culture), param0, param1);
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x000B90FB File Offset: 0x000B72FB
		internal static string TracepointTraceLevelChildren(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("TracepointTraceLevelChildren", SR.Culture), param0);
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x000B911C File Offset: 0x000B731C
		internal static string TraceLevelString(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("TraceLevelString", SR.Culture), param0);
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x000B913D File Offset: 0x000B733D
		internal static string TraceFlagsString(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("TraceFlagsString", SR.Culture), param0);
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x000B915E File Offset: 0x000B735E
		internal static string LevelNodeOneChild(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("LevelNodeOneChild", SR.Culture), param0);
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x000B917F File Offset: 0x000B737F
		internal static string LevelNoneEmptyDecisionTree(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("LevelNoneEmptyDecisionTree", SR.Culture), param0);
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x000B91A0 File Offset: 0x000B73A0
		internal static string LevelNoneEmptyDecisionTreeInsertOr(object param0)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("LevelNoneEmptyDecisionTreeInsertOr", SR.Culture), param0);
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x000B91C1 File Offset: 0x000B73C1
		internal static string DataTraceTruncated(object param0, object param1)
		{
			return string.Format(SR.Culture, SR.ResourceManager.GetString("DataTraceTruncated", SR.Culture), param0, param1);
		}

		// Token: 0x04001F7D RID: 8061
		private static ResourceManager resourceManager;

		// Token: 0x04001F7E RID: 8062
		private static CultureInfo resourceCulture;
	}
}
