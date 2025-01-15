using System;
using System.Resources;
using FxResources.System.Security.AccessControl;

namespace System
{
	// Token: 0x02000003 RID: 3
	internal static class SR
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static bool UsingResourceKeys()
		{
			return global::System.SR.s_usingResourceKeys;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		internal static string GetResourceString(string resourceKey)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return resourceKey;
			}
			string text = null;
			try
			{
				text = global::System.SR.ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			return text;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002094 File Offset: 0x00000294
		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string resourceString = global::System.SR.GetResourceString(resourceKey);
			if (!(resourceKey == resourceString) && resourceString != null)
			{
				return resourceString;
			}
			return defaultString;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B7 File Offset: 0x000002B7
		internal static string Format(string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E0 File Offset: 0x000002E0
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000210E File Offset: 0x0000030E
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002141 File Offset: 0x00000341
		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (global::System.SR.UsingResourceKeys())
			{
				return resourceFormat + ", " + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000216D File Offset: 0x0000036D
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(provider, resourceFormat, p1);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002197 File Offset: 0x00000397
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(provider, resourceFormat, p1, p2);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021C6 File Offset: 0x000003C6
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(provider, resourceFormat, p1, p2, p3);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021FC File Offset: 0x000003FC
		internal static string Format(IFormatProvider provider, string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (global::System.SR.UsingResourceKeys())
			{
				return resourceFormat + ", " + string.Join(", ", args);
			}
			return string.Format(provider, resourceFormat, args);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002229 File Offset: 0x00000429
		internal static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager resourceManager;
				if ((resourceManager = global::System.SR.s_resourceManager) == null)
				{
					resourceManager = (global::System.SR.s_resourceManager = new ResourceManager(typeof(FxResources.System.Security.AccessControl.SR)));
				}
				return resourceManager;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002249 File Offset: 0x00000449
		internal static string AccessControl_AclTooLong
		{
			get
			{
				return global::System.SR.GetResourceString("AccessControl_AclTooLong");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002255 File Offset: 0x00000455
		internal static string AccessControl_InvalidAccessRuleType
		{
			get
			{
				return global::System.SR.GetResourceString("AccessControl_InvalidAccessRuleType");
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002261 File Offset: 0x00000461
		internal static string AccessControl_InvalidAuditRuleType
		{
			get
			{
				return global::System.SR.GetResourceString("AccessControl_InvalidAuditRuleType");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000226D File Offset: 0x0000046D
		internal static string AccessControl_InvalidOwner
		{
			get
			{
				return global::System.SR.GetResourceString("AccessControl_InvalidOwner");
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002279 File Offset: 0x00000479
		internal static string AccessControl_InvalidGroup
		{
			get
			{
				return global::System.SR.GetResourceString("AccessControl_InvalidGroup");
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002285 File Offset: 0x00000485
		internal static string AccessControl_InvalidHandle
		{
			get
			{
				return global::System.SR.GetResourceString("AccessControl_InvalidHandle");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002291 File Offset: 0x00000491
		internal static string AccessControl_InvalidSecurityDescriptorRevision
		{
			get
			{
				return global::System.SR.GetResourceString("AccessControl_InvalidSecurityDescriptorRevision");
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000229D File Offset: 0x0000049D
		internal static string AccessControl_InvalidSecurityDescriptorSelfRelativeForm
		{
			get
			{
				return global::System.SR.GetResourceString("AccessControl_InvalidSecurityDescriptorSelfRelativeForm");
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022A9 File Offset: 0x000004A9
		internal static string AccessControl_InvalidSidInSDDLString
		{
			get
			{
				return global::System.SR.GetResourceString("AccessControl_InvalidSidInSDDLString");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022B5 File Offset: 0x000004B5
		internal static string AccessControl_MustSpecifyContainerAcl
		{
			get
			{
				return global::System.SR.GetResourceString("AccessControl_MustSpecifyContainerAcl");
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022C1 File Offset: 0x000004C1
		internal static string AccessControl_MustSpecifyDirectoryObjectAcl
		{
			get
			{
				return global::System.SR.GetResourceString("AccessControl_MustSpecifyDirectoryObjectAcl");
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022CD File Offset: 0x000004CD
		internal static string AccessControl_MustSpecifyLeafObjectAcl
		{
			get
			{
				return global::System.SR.GetResourceString("AccessControl_MustSpecifyLeafObjectAcl");
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022D9 File Offset: 0x000004D9
		internal static string AccessControl_MustSpecifyNonDirectoryObjectAcl
		{
			get
			{
				return global::System.SR.GetResourceString("AccessControl_MustSpecifyNonDirectoryObjectAcl");
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022E5 File Offset: 0x000004E5
		internal static string AccessControl_NoAssociatedSecurity
		{
			get
			{
				return global::System.SR.GetResourceString("AccessControl_NoAssociatedSecurity");
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000022F1 File Offset: 0x000004F1
		internal static string AccessControl_UnexpectedError
		{
			get
			{
				return global::System.SR.GetResourceString("AccessControl_UnexpectedError");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022FD File Offset: 0x000004FD
		internal static string Arg_EnumAtLeastOneFlag
		{
			get
			{
				return global::System.SR.GetResourceString("Arg_EnumAtLeastOneFlag");
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002309 File Offset: 0x00000509
		internal static string Arg_EnumIllegalVal
		{
			get
			{
				return global::System.SR.GetResourceString("Arg_EnumIllegalVal");
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002315 File Offset: 0x00000515
		internal static string Arg_InvalidOperationException
		{
			get
			{
				return global::System.SR.GetResourceString("Arg_InvalidOperationException");
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002321 File Offset: 0x00000521
		internal static string Arg_MustBeIdentityReferenceType
		{
			get
			{
				return global::System.SR.GetResourceString("Arg_MustBeIdentityReferenceType");
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000232D File Offset: 0x0000052D
		internal static string Argument_ArgumentZero
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_ArgumentZero");
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002339 File Offset: 0x00000539
		internal static string Argument_InvalidAnyFlag
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_InvalidAnyFlag");
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002345 File Offset: 0x00000545
		internal static string Argument_InvalidEnumValue
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_InvalidEnumValue");
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002351 File Offset: 0x00000551
		internal static string Argument_InvalidName
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_InvalidName");
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000235D File Offset: 0x0000055D
		internal static string Argument_InvalidPrivilegeName
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_InvalidPrivilegeName");
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002369 File Offset: 0x00000569
		internal static string Argument_InvalidSafeHandle
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_InvalidSafeHandle");
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002375 File Offset: 0x00000575
		internal static string ArgumentException_InvalidAceBinaryForm
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentException_InvalidAceBinaryForm");
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002381 File Offset: 0x00000581
		internal static string ArgumentException_InvalidAclBinaryForm
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentException_InvalidAclBinaryForm");
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000238D File Offset: 0x0000058D
		internal static string ArgumentException_InvalidSDSddlForm
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentException_InvalidSDSddlForm");
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002399 File Offset: 0x00000599
		internal static string ArgumentOutOfRange_ArrayLength
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentOutOfRange_ArrayLength");
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000023A5 File Offset: 0x000005A5
		internal static string ArgumentOutOfRange_ArrayLengthMultiple
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentOutOfRange_ArrayLengthMultiple");
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000023B1 File Offset: 0x000005B1
		internal static string ArgumentOutOfRange_ArrayTooSmall
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentOutOfRange_ArrayTooSmall");
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000023BD File Offset: 0x000005BD
		internal static string ArgumentOutOfRange_Enum
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentOutOfRange_Enum");
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000023C9 File Offset: 0x000005C9
		internal static string ArgumentOutOfRange_InvalidUserDefinedAceType
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentOutOfRange_InvalidUserDefinedAceType");
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000023D5 File Offset: 0x000005D5
		internal static string ArgumentOutOfRange_NeedNonNegNum
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentOutOfRange_NeedNonNegNum");
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000023E1 File Offset: 0x000005E1
		internal static string InvalidOperation_ModificationOfNonCanonicalAcl
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidOperation_ModificationOfNonCanonicalAcl");
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000023ED File Offset: 0x000005ED
		internal static string InvalidOperation_MustBeSameThread
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidOperation_MustBeSameThread");
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000023F9 File Offset: 0x000005F9
		internal static string InvalidOperation_MustLockForReadOrWrite
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidOperation_MustLockForReadOrWrite");
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002405 File Offset: 0x00000605
		internal static string InvalidOperation_MustLockForWrite
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidOperation_MustLockForWrite");
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002411 File Offset: 0x00000611
		internal static string InvalidOperation_MustRevertPrivilege
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidOperation_MustRevertPrivilege");
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000241D File Offset: 0x0000061D
		internal static string InvalidOperation_NoSecurityDescriptor
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidOperation_NoSecurityDescriptor");
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002429 File Offset: 0x00000629
		internal static string InvalidOperation_OnlyValidForDS
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidOperation_OnlyValidForDS");
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002435 File Offset: 0x00000635
		internal static string InvalidOperation_DisconnectedPipe
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidOperation_DisconnectedPipe");
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002441 File Offset: 0x00000641
		internal static string NotSupported_SetMethod
		{
			get
			{
				return global::System.SR.GetResourceString("NotSupported_SetMethod");
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000244D File Offset: 0x0000064D
		internal static string PrivilegeNotHeld_Default
		{
			get
			{
				return global::System.SR.GetResourceString("PrivilegeNotHeld_Default");
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002459 File Offset: 0x00000659
		internal static string PrivilegeNotHeld_Named
		{
			get
			{
				return global::System.SR.GetResourceString("PrivilegeNotHeld_Named");
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002465 File Offset: 0x00000665
		internal static string Rank_MultiDimNotSupported
		{
			get
			{
				return global::System.SR.GetResourceString("Rank_MultiDimNotSupported");
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002471 File Offset: 0x00000671
		internal static string PlatformNotSupported_AccessControl
		{
			get
			{
				return global::System.SR.GetResourceString("PlatformNotSupported_AccessControl");
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002480 File Offset: 0x00000680
		// Note: this type is marked as 'beforefieldinit'.
		static SR()
		{
			bool flag;
			global::System.SR.s_usingResourceKeys = AppContext.TryGetSwitch("System.Resources.UseSystemResourceKeys", out flag) && flag;
		}

		// Token: 0x04000001 RID: 1
		private static readonly bool s_usingResourceKeys;

		// Token: 0x04000002 RID: 2
		private static ResourceManager s_resourceManager;
	}
}
