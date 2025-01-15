using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.Runtime.InteropServices.RuntimeInformation;

namespace System
{
	// Token: 0x02000004 RID: 4
	internal static class SR
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static ResourceManager ResourceManager
		{
			get
			{
				if (global::System.SR.s_resourceManager == null)
				{
					global::System.SR.s_resourceManager = new ResourceManager(global::System.SR.ResourceType);
				}
				return global::System.SR.s_resourceManager;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000206D File Offset: 0x0000026D
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002070 File Offset: 0x00000270
		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string text = null;
			try
			{
				text = global::System.SR.ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			if (defaultString != null && resourceKey.Equals(text, StringComparison.Ordinal))
			{
				return defaultString;
			}
			return text;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B0 File Offset: 0x000002B0
		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (global::System.SR.UsingResourceKeys())
			{
				return resourceFormat + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D7 File Offset: 0x000002D7
		internal static string Format(string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, new object[] { p1 });
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002109 File Offset: 0x00000309
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, new object[] { p1, p2 });
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002144 File Offset: 0x00000344
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, new object[] { p1, p2, p3 });
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002191 File Offset: 0x00000391
		internal static string Argument_EmptyValue
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_EmptyValue", null);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000219E File Offset: 0x0000039E
		internal static Type ResourceType
		{
			get
			{
				return typeof(FxResources.System.Runtime.InteropServices.RuntimeInformation.SR);
			}
		}

		// Token: 0x04000001 RID: 1
		private static ResourceManager s_resourceManager;

		// Token: 0x04000002 RID: 2
		private const string s_resourcesName = "FxResources.System.Runtime.InteropServices.RuntimeInformation.SR";
	}
}
