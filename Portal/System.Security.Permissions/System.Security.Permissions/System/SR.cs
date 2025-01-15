using System;
using System.Resources;
using FxResources.System.Security.Permissions;

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
					resourceManager = (global::System.SR.s_resourceManager = new ResourceManager(typeof(FxResources.System.Security.Permissions.SR)));
				}
				return resourceManager;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002249 File Offset: 0x00000449
		internal static string Argument_InvalidPermissionState
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_InvalidPermissionState");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002255 File Offset: 0x00000455
		internal static string Argument_NotAPermissionElement
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_NotAPermissionElement");
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002261 File Offset: 0x00000461
		internal static string Argument_InvalidXMLBadVersion
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_InvalidXMLBadVersion");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000226D File Offset: 0x0000046D
		internal static string Argument_WrongType
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_WrongType");
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002279 File Offset: 0x00000479
		internal static string HostProtection_ProtectedResources
		{
			get
			{
				return global::System.SR.GetResourceString("HostProtection_ProtectedResources");
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002285 File Offset: 0x00000485
		internal static string HostProtection_DemandedResources
		{
			get
			{
				return global::System.SR.GetResourceString("HostProtection_DemandedResources");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002291 File Offset: 0x00000491
		internal static string Security_PrincipalPermission
		{
			get
			{
				return global::System.SR.GetResourceString("Security_PrincipalPermission");
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000229D File Offset: 0x0000049D
		internal static string PlatformNotSupported_CAS
		{
			get
			{
				return global::System.SR.GetResourceString("PlatformNotSupported_CAS");
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022AC File Offset: 0x000004AC
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
