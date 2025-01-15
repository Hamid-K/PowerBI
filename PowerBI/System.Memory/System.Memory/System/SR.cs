using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.Memory;

namespace System
{
	// Token: 0x02000018 RID: 24
	internal static class SR
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00009BC2 File Offset: 0x00007DC2
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

		// Token: 0x0600014C RID: 332 RVA: 0x00009BDD File Offset: 0x00007DDD
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00009BE0 File Offset: 0x00007DE0
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

		// Token: 0x0600014E RID: 334 RVA: 0x00009C20 File Offset: 0x00007E20
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

		// Token: 0x0600014F RID: 335 RVA: 0x00009C47 File Offset: 0x00007E47
		internal static string Format(string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00009C70 File Offset: 0x00007E70
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00009C9E File Offset: 0x00007E9E
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00009CD1 File Offset: 0x00007ED1
		internal static Type ResourceType { get; } = typeof(FxResources.System.Memory.SR);

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00009CD8 File Offset: 0x00007ED8
		internal static string NotSupported_CannotCallEqualsOnSpan
		{
			get
			{
				return global::System.SR.GetResourceString("NotSupported_CannotCallEqualsOnSpan", null);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00009CE5 File Offset: 0x00007EE5
		internal static string NotSupported_CannotCallGetHashCodeOnSpan
		{
			get
			{
				return global::System.SR.GetResourceString("NotSupported_CannotCallGetHashCodeOnSpan", null);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00009CF2 File Offset: 0x00007EF2
		internal static string Argument_InvalidTypeWithPointersNotSupported
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_InvalidTypeWithPointersNotSupported", null);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00009CFF File Offset: 0x00007EFF
		internal static string Argument_DestinationTooShort
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_DestinationTooShort", null);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00009D0C File Offset: 0x00007F0C
		internal static string MemoryDisposed
		{
			get
			{
				return global::System.SR.GetResourceString("MemoryDisposed", null);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00009D19 File Offset: 0x00007F19
		internal static string OutstandingReferences
		{
			get
			{
				return global::System.SR.GetResourceString("OutstandingReferences", null);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00009D26 File Offset: 0x00007F26
		internal static string Argument_BadFormatSpecifier
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_BadFormatSpecifier", null);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00009D33 File Offset: 0x00007F33
		internal static string Argument_GWithPrecisionNotSupported
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_GWithPrecisionNotSupported", null);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00009D40 File Offset: 0x00007F40
		internal static string Argument_CannotParsePrecision
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_CannotParsePrecision", null);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00009D4D File Offset: 0x00007F4D
		internal static string Argument_PrecisionTooLarge
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_PrecisionTooLarge", null);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00009D5A File Offset: 0x00007F5A
		internal static string Argument_OverlapAlignmentMismatch
		{
			get
			{
				return global::System.SR.GetResourceString("Argument_OverlapAlignmentMismatch", null);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00009D67 File Offset: 0x00007F67
		internal static string EndPositionNotReached
		{
			get
			{
				return global::System.SR.GetResourceString("EndPositionNotReached", null);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00009D74 File Offset: 0x00007F74
		internal static string UnexpectedSegmentType
		{
			get
			{
				return global::System.SR.GetResourceString("UnexpectedSegmentType", null);
			}
		}

		// Token: 0x0400006A RID: 106
		private static ResourceManager s_resourceManager;
	}
}
