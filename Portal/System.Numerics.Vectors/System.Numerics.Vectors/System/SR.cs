using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.Numerics.Vectors;

namespace System
{
	// Token: 0x02000004 RID: 4
	internal static class SR
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020A2 File Offset: 0x000002A2
		private static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager resourceManager;
				if ((resourceManager = global::System.SR.s_resourceManager) == null)
				{
					resourceManager = (global::System.SR.s_resourceManager = new ResourceManager(global::System.SR.ResourceType));
				}
				return resourceManager;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020BD File Offset: 0x000002BD
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020C0 File Offset: 0x000002C0
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

		// Token: 0x0600000C RID: 12 RVA: 0x00002100 File Offset: 0x00000300
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

		// Token: 0x0600000D RID: 13 RVA: 0x00002127 File Offset: 0x00000327
		internal static string Format(string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002150 File Offset: 0x00000350
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000217E File Offset: 0x0000037E
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021B1 File Offset: 0x000003B1
		internal static Type ResourceType { get; } = typeof(FxResources.System.Numerics.Vectors.SR);

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021B8 File Offset: 0x000003B8
		internal static string Arg_ArgumentOutOfRangeException
		{
			get
			{
				return global::System.SR.GetResourceString("Arg_ArgumentOutOfRangeException", null);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021C5 File Offset: 0x000003C5
		internal static string Arg_ElementsInSourceIsGreaterThanDestination
		{
			get
			{
				return global::System.SR.GetResourceString("Arg_ElementsInSourceIsGreaterThanDestination", null);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021D2 File Offset: 0x000003D2
		internal static string Arg_NullArgumentNullRef
		{
			get
			{
				return global::System.SR.GetResourceString("Arg_NullArgumentNullRef", null);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000021DF File Offset: 0x000003DF
		internal static string Arg_TypeNotSupported
		{
			get
			{
				return global::System.SR.GetResourceString("Arg_TypeNotSupported", null);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021EC File Offset: 0x000003EC
		internal static string Arg_InsufficientNumberOfElements
		{
			get
			{
				return global::System.SR.GetResourceString("Arg_InsufficientNumberOfElements", null);
			}
		}

		// Token: 0x04000002 RID: 2
		private static ResourceManager s_resourceManager;
	}
}
