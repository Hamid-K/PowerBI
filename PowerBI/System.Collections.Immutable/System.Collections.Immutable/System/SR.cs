using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.Collections.Immutable;

namespace System
{
	// Token: 0x02000008 RID: 8
	internal static class SR
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020A5 File Offset: 0x000002A5
		private static bool UsingResourceKeys()
		{
			return false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020A8 File Offset: 0x000002A8
		[NullableContext(1)]
		internal static string GetResourceString(string resourceKey, [Nullable(2)] string defaultString = null)
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

		// Token: 0x06000009 RID: 9 RVA: 0x000020F8 File Offset: 0x000002F8
		[NullableContext(1)]
		internal static string Format(string resourceFormat, [Nullable(2)] object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, new object[] { p1 });
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000212A File Offset: 0x0000032A
		[NullableContext(1)]
		internal static string Format(string resourceFormat, [Nullable(2)] object p1, [Nullable(2)] object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, new object[] { p1, p2 });
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002164 File Offset: 0x00000364
		[NullableContext(2)]
		[return: Nullable(1)]
		internal static string Format([Nullable(1)] string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, new object[] { p1, p2, p3 });
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021B1 File Offset: 0x000003B1
		[NullableContext(1)]
		internal static string Format(string resourceFormat, [Nullable(2)] params object[] args)
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

		// Token: 0x0600000D RID: 13 RVA: 0x000021DD File Offset: 0x000003DD
		[NullableContext(1)]
		internal static string Format([Nullable(2)] IFormatProvider provider, string resourceFormat, [Nullable(2)] object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(provider, resourceFormat, new object[] { p1 });
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002210 File Offset: 0x00000410
		[NullableContext(2)]
		[return: Nullable(1)]
		internal static string Format(IFormatProvider provider, [Nullable(1)] string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(provider, resourceFormat, new object[] { p1, p2 });
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000224C File Offset: 0x0000044C
		[NullableContext(2)]
		[return: Nullable(1)]
		internal static string Format(IFormatProvider provider, [Nullable(1)] string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(provider, resourceFormat, new object[] { p1, p2, p3 });
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000229C File Offset: 0x0000049C
		[NullableContext(1)]
		internal static string Format([Nullable(2)] IFormatProvider provider, string resourceFormat, [Nullable(2)] params object[] args)
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
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022C9 File Offset: 0x000004C9
		internal static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager resourceManager;
				if ((resourceManager = global::System.SR.s_resourceManager) == null)
				{
					resourceManager = (global::System.SR.s_resourceManager = new ResourceManager(typeof(FxResources.System.Collections.Immutable.SR)));
				}
				return resourceManager;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022E9 File Offset: 0x000004E9
		internal static string Arg_KeyNotFoundWithKey
		{
			get
			{
				return global::System.SR.GetResourceString("Arg_KeyNotFoundWithKey", null);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022F6 File Offset: 0x000004F6
		internal static string ArrayInitializedStateNotEqual
		{
			get
			{
				return global::System.SR.GetResourceString("ArrayInitializedStateNotEqual", null);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002303 File Offset: 0x00000503
		internal static string ArrayLengthsNotEqual
		{
			get
			{
				return global::System.SR.GetResourceString("ArrayLengthsNotEqual", null);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002310 File Offset: 0x00000510
		internal static string CannotFindOldValue
		{
			get
			{
				return global::System.SR.GetResourceString("CannotFindOldValue", null);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000231D File Offset: 0x0000051D
		internal static string CapacityMustBeGreaterThanOrEqualToCount
		{
			get
			{
				return global::System.SR.GetResourceString("CapacityMustBeGreaterThanOrEqualToCount", null);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000232A File Offset: 0x0000052A
		internal static string CapacityMustEqualCountOnMove
		{
			get
			{
				return global::System.SR.GetResourceString("CapacityMustEqualCountOnMove", null);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002337 File Offset: 0x00000537
		internal static string CollectionModifiedDuringEnumeration
		{
			get
			{
				return global::System.SR.GetResourceString("CollectionModifiedDuringEnumeration", null);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002344 File Offset: 0x00000544
		internal static string DuplicateKey
		{
			get
			{
				return global::System.SR.GetResourceString("DuplicateKey", null);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002351 File Offset: 0x00000551
		internal static string InvalidEmptyOperation
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidEmptyOperation", null);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000235E File Offset: 0x0000055E
		internal static string InvalidOperationOnDefaultArray
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidOperationOnDefaultArray", null);
			}
		}

		// Token: 0x04000004 RID: 4
		private static ResourceManager s_resourceManager;
	}
}
