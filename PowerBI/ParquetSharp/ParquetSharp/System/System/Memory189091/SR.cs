using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.Memory;

namespace System
{
	// Token: 0x020000D6 RID: 214
	internal static class System.Memory189091.SR
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x00021068 File Offset: 0x0001F268
		private static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager resourceManager;
				if ((resourceManager = System.Memory189091.SR.s_resourceManager) == null)
				{
					resourceManager = (System.Memory189091.SR.s_resourceManager = new ResourceManager(System.Memory189091.SR.ResourceType));
				}
				return resourceManager;
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00021088 File Offset: 0x0001F288
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0002108C File Offset: 0x0001F28C
		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string text = null;
			try
			{
				text = System.Memory189091.SR.ResourceManager.GetString(resourceKey);
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

		// Token: 0x0600078E RID: 1934 RVA: 0x000210D8 File Offset: 0x0001F2D8
		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (System.Memory189091.SR.UsingResourceKeys())
			{
				return resourceFormat + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00021108 File Offset: 0x0001F308
		internal static string Format(string resourceFormat, object p1)
		{
			if (System.Memory189091.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00021134 File Offset: 0x0001F334
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (System.Memory189091.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00021168 File Offset: 0x0001F368
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (System.Memory189091.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x000211A0 File Offset: 0x0001F3A0
		internal static Type ResourceType { get; } = typeof(FxResources.System.Memory.SR);

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x000211A8 File Offset: 0x0001F3A8
		internal static string NotSupported_CannotCallEqualsOnSpan
		{
			get
			{
				return System.Memory189091.SR.GetResourceString("NotSupported_CannotCallEqualsOnSpan", null);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x000211B8 File Offset: 0x0001F3B8
		internal static string NotSupported_CannotCallGetHashCodeOnSpan
		{
			get
			{
				return System.Memory189091.SR.GetResourceString("NotSupported_CannotCallGetHashCodeOnSpan", null);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x000211C8 File Offset: 0x0001F3C8
		internal static string Argument_InvalidTypeWithPointersNotSupported
		{
			get
			{
				return System.Memory189091.SR.GetResourceString("Argument_InvalidTypeWithPointersNotSupported", null);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x000211D8 File Offset: 0x0001F3D8
		internal static string Argument_DestinationTooShort
		{
			get
			{
				return System.Memory189091.SR.GetResourceString("Argument_DestinationTooShort", null);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x000211E8 File Offset: 0x0001F3E8
		internal static string MemoryDisposed
		{
			get
			{
				return System.Memory189091.SR.GetResourceString("MemoryDisposed", null);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x000211F8 File Offset: 0x0001F3F8
		internal static string OutstandingReferences
		{
			get
			{
				return System.Memory189091.SR.GetResourceString("OutstandingReferences", null);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x00021208 File Offset: 0x0001F408
		internal static string Argument_BadFormatSpecifier
		{
			get
			{
				return System.Memory189091.SR.GetResourceString("Argument_BadFormatSpecifier", null);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00021218 File Offset: 0x0001F418
		internal static string Argument_GWithPrecisionNotSupported
		{
			get
			{
				return System.Memory189091.SR.GetResourceString("Argument_GWithPrecisionNotSupported", null);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00021228 File Offset: 0x0001F428
		internal static string Argument_CannotParsePrecision
		{
			get
			{
				return System.Memory189091.SR.GetResourceString("Argument_CannotParsePrecision", null);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00021238 File Offset: 0x0001F438
		internal static string Argument_PrecisionTooLarge
		{
			get
			{
				return System.Memory189091.SR.GetResourceString("Argument_PrecisionTooLarge", null);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x00021248 File Offset: 0x0001F448
		internal static string Argument_OverlapAlignmentMismatch
		{
			get
			{
				return System.Memory189091.SR.GetResourceString("Argument_OverlapAlignmentMismatch", null);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00021258 File Offset: 0x0001F458
		internal static string EndPositionNotReached
		{
			get
			{
				return System.Memory189091.SR.GetResourceString("EndPositionNotReached", null);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00021268 File Offset: 0x0001F468
		internal static string UnexpectedSegmentType
		{
			get
			{
				return System.Memory189091.SR.GetResourceString("UnexpectedSegmentType", null);
			}
		}

		// Token: 0x04000242 RID: 578
		private static ResourceManager s_resourceManager;
	}
}
