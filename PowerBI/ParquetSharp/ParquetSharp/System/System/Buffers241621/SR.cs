using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.Buffers;

namespace System
{
	// Token: 0x020000F1 RID: 241
	internal static class System.Buffers241621.SR
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0002AF90 File Offset: 0x00029190
		private static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager resourceManager;
				if ((resourceManager = System.Buffers241621.SR.s_resourceManager) == null)
				{
					resourceManager = (System.Buffers241621.SR.s_resourceManager = new ResourceManager(System.Buffers241621.SR.ResourceType));
				}
				return resourceManager;
			}
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0002AFB0 File Offset: 0x000291B0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0002AFB4 File Offset: 0x000291B4
		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string text = null;
			try
			{
				text = System.Buffers241621.SR.ResourceManager.GetString(resourceKey);
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

		// Token: 0x060008E4 RID: 2276 RVA: 0x0002B000 File Offset: 0x00029200
		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (System.Buffers241621.SR.UsingResourceKeys())
			{
				return resourceFormat + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0002B030 File Offset: 0x00029230
		internal static string Format(string resourceFormat, object p1)
		{
			if (System.Buffers241621.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0002B05C File Offset: 0x0002925C
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (System.Buffers241621.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0002B090 File Offset: 0x00029290
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (System.Buffers241621.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x0002B0C8 File Offset: 0x000292C8
		internal static Type ResourceType { get; } = typeof(FxResources.System.Buffers.SR);

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0002B0D0 File Offset: 0x000292D0
		internal static string ArgumentException_BufferNotFromPool
		{
			get
			{
				return System.Buffers241621.SR.GetResourceString("ArgumentException_BufferNotFromPool", null);
			}
		}

		// Token: 0x040002AE RID: 686
		private static ResourceManager s_resourceManager;
	}
}
