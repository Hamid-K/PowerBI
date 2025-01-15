using System;
using System.Resources;
using FxResources.System.Diagnostics.DiagnosticSource;

namespace System
{
	// Token: 0x0200000D RID: 13
	internal static class SR
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000027FD File Offset: 0x000009FD
		private static bool UsingResourceKeys()
		{
			return global::System.SR.s_usingResourceKeys;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002804 File Offset: 0x00000A04
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

		// Token: 0x0600002F RID: 47 RVA: 0x00002840 File Offset: 0x00000A40
		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string resourceString = global::System.SR.GetResourceString(resourceKey);
			if (!(resourceKey == resourceString) && resourceString != null)
			{
				return resourceString;
			}
			return defaultString;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002863 File Offset: 0x00000A63
		internal static string Format(string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000288C File Offset: 0x00000A8C
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028BA File Offset: 0x00000ABA
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028ED File Offset: 0x00000AED
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

		// Token: 0x06000034 RID: 52 RVA: 0x00002919 File Offset: 0x00000B19
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(provider, resourceFormat, p1);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002943 File Offset: 0x00000B43
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(provider, resourceFormat, p1, p2);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002972 File Offset: 0x00000B72
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(provider, resourceFormat, p1, p2, p3);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000029A8 File Offset: 0x00000BA8
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000029D5 File Offset: 0x00000BD5
		internal static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager resourceManager;
				if ((resourceManager = global::System.SR.s_resourceManager) == null)
				{
					resourceManager = (global::System.SR.s_resourceManager = new ResourceManager(typeof(FxResources.System.Diagnostics.DiagnosticSource.SR)));
				}
				return resourceManager;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000029F5 File Offset: 0x00000BF5
		internal static string ActivityIdFormatInvalid
		{
			get
			{
				return global::System.SR.GetResourceString("ActivityIdFormatInvalid");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002A01 File Offset: 0x00000C01
		internal static string ActivityNotRunning
		{
			get
			{
				return global::System.SR.GetResourceString("ActivityNotRunning");
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A0D File Offset: 0x00000C0D
		internal static string ActivityNotStarted
		{
			get
			{
				return global::System.SR.GetResourceString("ActivityNotStarted");
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002A19 File Offset: 0x00000C19
		internal static string ActivityStartAlreadyStarted
		{
			get
			{
				return global::System.SR.GetResourceString("ActivityStartAlreadyStarted");
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A25 File Offset: 0x00000C25
		internal static string EndTimeNotUtc
		{
			get
			{
				return global::System.SR.GetResourceString("EndTimeNotUtc");
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002A31 File Offset: 0x00000C31
		internal static string OperationNameInvalid
		{
			get
			{
				return global::System.SR.GetResourceString("OperationNameInvalid");
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002A3D File Offset: 0x00000C3D
		internal static string ParentIdAlreadySet
		{
			get
			{
				return global::System.SR.GetResourceString("ParentIdAlreadySet");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002A49 File Offset: 0x00000C49
		internal static string ParentIdInvalid
		{
			get
			{
				return global::System.SR.GetResourceString("ParentIdInvalid");
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002A55 File Offset: 0x00000C55
		internal static string SetFormatOnStartedActivity
		{
			get
			{
				return global::System.SR.GetResourceString("SetFormatOnStartedActivity");
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002A61 File Offset: 0x00000C61
		internal static string SetLinkInvalid
		{
			get
			{
				return global::System.SR.GetResourceString("SetLinkInvalid");
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002A6D File Offset: 0x00000C6D
		internal static string SetParentIdOnActivityWithParent
		{
			get
			{
				return global::System.SR.GetResourceString("SetParentIdOnActivityWithParent");
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002A79 File Offset: 0x00000C79
		internal static string StartTimeNotUtc
		{
			get
			{
				return global::System.SR.GetResourceString("StartTimeNotUtc");
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002A85 File Offset: 0x00000C85
		internal static string KeyAlreadyExist
		{
			get
			{
				return global::System.SR.GetResourceString("KeyAlreadyExist");
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002A91 File Offset: 0x00000C91
		internal static string InvalidTraceParent
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidTraceParent");
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002A9D File Offset: 0x00000C9D
		internal static string UnableAccessServicePointTable
		{
			get
			{
				return global::System.SR.GetResourceString("UnableAccessServicePointTable");
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002AA9 File Offset: 0x00000CA9
		internal static string UnableToInitialize
		{
			get
			{
				return global::System.SR.GetResourceString("UnableToInitialize");
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002AB5 File Offset: 0x00000CB5
		internal static string UnsupportedType
		{
			get
			{
				return global::System.SR.GetResourceString("UnsupportedType");
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002AC1 File Offset: 0x00000CC1
		internal static string Arg_BufferTooSmall
		{
			get
			{
				return global::System.SR.GetResourceString("Arg_BufferTooSmall");
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002AD0 File Offset: 0x00000CD0
		// Note: this type is marked as 'beforefieldinit'.
		static SR()
		{
			bool flag;
			global::System.SR.s_usingResourceKeys = AppContext.TryGetSwitch("System.Resources.UseSystemResourceKeys", out flag) && flag;
		}

		// Token: 0x0400000B RID: 11
		private static readonly bool s_usingResourceKeys;

		// Token: 0x0400000C RID: 12
		private static ResourceManager s_resourceManager;
	}
}
