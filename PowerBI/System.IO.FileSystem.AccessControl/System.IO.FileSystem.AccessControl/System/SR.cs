using System;
using System.Resources;
using FxResources.System.IO.FileSystem.AccessControl;

namespace System
{
	// Token: 0x02000007 RID: 7
	internal static class SR
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000209D File Offset: 0x0000029D
		private static bool UsingResourceKeys()
		{
			return global::System.SR.s_usingResourceKeys;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020A4 File Offset: 0x000002A4
		internal static string GetResourceString(string resourceKey, string defaultString = null)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return defaultString ?? resourceKey;
			}
			string text = null;
			try
			{
				text = global::System.SR.ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			if (defaultString != null && resourceKey.Equals(text))
			{
				return defaultString;
			}
			return text;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020F4 File Offset: 0x000002F4
		internal static string Format(string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000211D File Offset: 0x0000031D
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000214B File Offset: 0x0000034B
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000217E File Offset: 0x0000037E
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

		// Token: 0x0600000C RID: 12 RVA: 0x000021AA File Offset: 0x000003AA
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(provider, resourceFormat, p1);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021D4 File Offset: 0x000003D4
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(provider, resourceFormat, p1, p2);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002203 File Offset: 0x00000403
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(provider, resourceFormat, p1, p2, p3);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002239 File Offset: 0x00000439
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
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002266 File Offset: 0x00000466
		internal static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager resourceManager;
				if ((resourceManager = global::System.SR.s_resourceManager) == null)
				{
					resourceManager = (global::System.SR.s_resourceManager = new ResourceManager(typeof(FxResources.System.IO.FileSystem.AccessControl.SR)));
				}
				return resourceManager;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002286 File Offset: 0x00000486
		internal static string AccessControl_InvalidHandle
		{
			get
			{
				return global::System.SR.GetResourceString("AccessControl_InvalidHandle", null);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002293 File Offset: 0x00000493
		internal static string Arg_MustBeIdentityReferenceType
		{
			get
			{
				return global::System.SR.GetResourceString("Arg_MustBeIdentityReferenceType", null);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022A0 File Offset: 0x000004A0
		internal static string Argument_InvalidEnumValue
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_InvalidEnumValue", null);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022AD File Offset: 0x000004AD
		internal static string Argument_InvalidName
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_InvalidName", null);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022BA File Offset: 0x000004BA
		internal static string ArgumentOutOfRange_Enum
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentOutOfRange_Enum", null);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022C7 File Offset: 0x000004C7
		internal static string ObjectDisposed_FileClosed
		{
			get
			{
				return global::System.SR.GetResourceString("ObjectDisposed_FileClosed", null);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022D4 File Offset: 0x000004D4
		internal static string PlatformNotSupported_AccessControl
		{
			get
			{
				return global::System.SR.GetResourceString("PlatformNotSupported_AccessControl", null);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022E1 File Offset: 0x000004E1
		internal static string TypeUnrecognized_AccessControl
		{
			get
			{
				return global::System.SR.GetResourceString("TypeUnrecognized_AccessControl", null);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022EE File Offset: 0x000004EE
		internal static string InvalidOperation_RemoveFail
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidOperation_RemoveFail", null);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022FB File Offset: 0x000004FB
		internal static string Arg_MustBeDriveLetterOrRootDir
		{
			get
			{
				return global::System.SR.GetResourceString("Arg_MustBeDriveLetterOrRootDir", null);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002308 File Offset: 0x00000508
		internal static string IO_AlreadyExists_Name
		{
			get
			{
				return global::System.SR.GetResourceString("IO_AlreadyExists_Name", null);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002315 File Offset: 0x00000515
		internal static string IO_FileExists_Name
		{
			get
			{
				return global::System.SR.GetResourceString("IO_FileExists_Name", null);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002322 File Offset: 0x00000522
		internal static string IO_FileNotFound
		{
			get
			{
				return global::System.SR.GetResourceString("IO_FileNotFound", null);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000232F File Offset: 0x0000052F
		internal static string IO_FileNotFound_FileName
		{
			get
			{
				return global::System.SR.GetResourceString("IO_FileNotFound_FileName", null);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000233C File Offset: 0x0000053C
		internal static string IO_PathNotFound_NoPathName
		{
			get
			{
				return global::System.SR.GetResourceString("IO_PathNotFound_NoPathName", null);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002349 File Offset: 0x00000549
		internal static string IO_PathNotFound_Path
		{
			get
			{
				return global::System.SR.GetResourceString("IO_PathNotFound_Path", null);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002356 File Offset: 0x00000556
		internal static string IO_PathTooLong
		{
			get
			{
				return global::System.SR.GetResourceString("IO_PathTooLong", null);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002363 File Offset: 0x00000563
		internal static string IO_PathTooLong_Path
		{
			get
			{
				return global::System.SR.GetResourceString("IO_PathTooLong_Path", null);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002370 File Offset: 0x00000570
		internal static string IO_SharingViolation_File
		{
			get
			{
				return global::System.SR.GetResourceString("IO_SharingViolation_File", null);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000237D File Offset: 0x0000057D
		internal static string IO_SharingViolation_NoFileName
		{
			get
			{
				return global::System.SR.GetResourceString("IO_SharingViolation_NoFileName", null);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000238A File Offset: 0x0000058A
		internal static string UnauthorizedAccess_IODenied_NoPathName
		{
			get
			{
				return global::System.SR.GetResourceString("UnauthorizedAccess_IODenied_NoPathName", null);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002397 File Offset: 0x00000597
		internal static string UnauthorizedAccess_IODenied_Path
		{
			get
			{
				return global::System.SR.GetResourceString("UnauthorizedAccess_IODenied_Path", null);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000023A4 File Offset: 0x000005A4
		internal static string Argument_InvalidPathChars
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_InvalidPathChars", null);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023B1 File Offset: 0x000005B1
		internal static string Arg_PathEmpty
		{
			get
			{
				return global::System.SR.GetResourceString("Arg_PathEmpty", null);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000023BE File Offset: 0x000005BE
		internal static string ArgumentOutOfRange_NeedPosNum
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentOutOfRange_NeedPosNum", null);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000023CB File Offset: 0x000005CB
		internal static string Argument_InvalidFileModeAndFileSystemRightsCombo
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_InvalidFileModeAndFileSystemRightsCombo", null);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000023D8 File Offset: 0x000005D8
		// Note: this type is marked as 'beforefieldinit'.
		static SR()
		{
			bool flag;
			global::System.SR.s_usingResourceKeys = AppContext.TryGetSwitch("System.Resources.UseSystemResourceKeys", out flag) && flag;
		}

		// Token: 0x04000004 RID: 4
		private static readonly bool s_usingResourceKeys;

		// Token: 0x04000005 RID: 5
		private static ResourceManager s_resourceManager;
	}
}
