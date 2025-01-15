using System;
using System.Resources;
using FxResources.System.Text.Encodings.Web;

namespace System
{
	// Token: 0x0200000C RID: 12
	internal static class SR
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002426 File Offset: 0x00000626
		internal static bool UsingResourceKeys()
		{
			return global::System.SR.s_usingResourceKeys;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002430 File Offset: 0x00000630
		private static string GetResourceString(string resourceKey)
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

		// Token: 0x0600001C RID: 28 RVA: 0x0000246C File Offset: 0x0000066C
		private static string GetResourceString(string resourceKey, string defaultString)
		{
			string resourceString = global::System.SR.GetResourceString(resourceKey);
			if (!(resourceKey == resourceString) && resourceString != null)
			{
				return resourceString;
			}
			return defaultString;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000248F File Offset: 0x0000068F
		internal static string Format(string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024B8 File Offset: 0x000006B8
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024E6 File Offset: 0x000006E6
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002519 File Offset: 0x00000719
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

		// Token: 0x06000021 RID: 33 RVA: 0x00002545 File Offset: 0x00000745
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(provider, resourceFormat, p1);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000256F File Offset: 0x0000076F
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(provider, resourceFormat, p1, p2);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000259E File Offset: 0x0000079E
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(provider, resourceFormat, p1, p2, p3);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025D4 File Offset: 0x000007D4
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

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002601 File Offset: 0x00000801
		internal static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager resourceManager;
				if ((resourceManager = global::System.SR.s_resourceManager) == null)
				{
					resourceManager = (global::System.SR.s_resourceManager = new ResourceManager(typeof(FxResources.System.Text.Encodings.Web.SR)));
				}
				return resourceManager;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002621 File Offset: 0x00000821
		internal static string TextEncoderDoesNotImplementMaxOutputCharsPerInputChar
		{
			get
			{
				return global::System.SR.GetResourceString("TextEncoderDoesNotImplementMaxOutputCharsPerInputChar");
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002630 File Offset: 0x00000830
		// Note: this type is marked as 'beforefieldinit'.
		static SR()
		{
			bool flag;
			global::System.SR.s_usingResourceKeys = AppContext.TryGetSwitch("System.Resources.UseSystemResourceKeys", out flag) && flag;
		}

		// Token: 0x04000006 RID: 6
		private static readonly bool s_usingResourceKeys;

		// Token: 0x04000007 RID: 7
		private static ResourceManager s_resourceManager;
	}
}
