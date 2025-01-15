using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.Collections.Immutable;

namespace System
{
	// Token: 0x02002055 RID: 8277
	internal static class SR
	{
		// Token: 0x06011399 RID: 70553 RVA: 0x003B5164 File Offset: 0x003B3364
		private static bool UsingResourceKeys()
		{
			return global::System.SR.s_usingResourceKeys;
		}

		// Token: 0x0601139A RID: 70554 RVA: 0x003B516C File Offset: 0x003B336C
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

		// Token: 0x0601139B RID: 70555 RVA: 0x003B51BC File Offset: 0x003B33BC
		[NullableContext(1)]
		internal static string Format(string resourceFormat, [Nullable(2)] object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x0601139C RID: 70556 RVA: 0x003B51E5 File Offset: 0x003B33E5
		[NullableContext(1)]
		internal static string Format(string resourceFormat, [Nullable(2)] object p1, [Nullable(2)] object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x0601139D RID: 70557 RVA: 0x003B5213 File Offset: 0x003B3413
		[NullableContext(2)]
		[return: Nullable(1)]
		internal static string Format([Nullable(1)] string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x0601139E RID: 70558 RVA: 0x003B5246 File Offset: 0x003B3446
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

		// Token: 0x0601139F RID: 70559 RVA: 0x003B5272 File Offset: 0x003B3472
		[NullableContext(1)]
		internal static string Format([Nullable(2)] IFormatProvider provider, string resourceFormat, [Nullable(2)] object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(provider, resourceFormat, p1);
		}

		// Token: 0x060113A0 RID: 70560 RVA: 0x003B529C File Offset: 0x003B349C
		[NullableContext(2)]
		[return: Nullable(1)]
		internal static string Format(IFormatProvider provider, [Nullable(1)] string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(provider, resourceFormat, p1, p2);
		}

		// Token: 0x060113A1 RID: 70561 RVA: 0x003B52CB File Offset: 0x003B34CB
		[NullableContext(2)]
		[return: Nullable(1)]
		internal static string Format(IFormatProvider provider, [Nullable(1)] string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(provider, resourceFormat, p1, p2, p3);
		}

		// Token: 0x060113A2 RID: 70562 RVA: 0x003B5301 File Offset: 0x003B3501
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

		// Token: 0x17002DFE RID: 11774
		// (get) Token: 0x060113A3 RID: 70563 RVA: 0x003B532E File Offset: 0x003B352E
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

		// Token: 0x17002DFF RID: 11775
		// (get) Token: 0x060113A4 RID: 70564 RVA: 0x003B534E File Offset: 0x003B354E
		internal static string Arg_KeyNotFoundWithKey
		{
			get
			{
				return global::System.SR.GetResourceString("Arg_KeyNotFoundWithKey", null);
			}
		}

		// Token: 0x17002E00 RID: 11776
		// (get) Token: 0x060113A5 RID: 70565 RVA: 0x003B535B File Offset: 0x003B355B
		internal static string ArrayInitializedStateNotEqual
		{
			get
			{
				return global::System.SR.GetResourceString("ArrayInitializedStateNotEqual", null);
			}
		}

		// Token: 0x17002E01 RID: 11777
		// (get) Token: 0x060113A6 RID: 70566 RVA: 0x003B5368 File Offset: 0x003B3568
		internal static string ArrayLengthsNotEqual
		{
			get
			{
				return global::System.SR.GetResourceString("ArrayLengthsNotEqual", null);
			}
		}

		// Token: 0x17002E02 RID: 11778
		// (get) Token: 0x060113A7 RID: 70567 RVA: 0x003B5375 File Offset: 0x003B3575
		internal static string CannotFindOldValue
		{
			get
			{
				return global::System.SR.GetResourceString("CannotFindOldValue", null);
			}
		}

		// Token: 0x17002E03 RID: 11779
		// (get) Token: 0x060113A8 RID: 70568 RVA: 0x003B5382 File Offset: 0x003B3582
		internal static string CapacityMustBeGreaterThanOrEqualToCount
		{
			get
			{
				return global::System.SR.GetResourceString("CapacityMustBeGreaterThanOrEqualToCount", null);
			}
		}

		// Token: 0x17002E04 RID: 11780
		// (get) Token: 0x060113A9 RID: 70569 RVA: 0x003B538F File Offset: 0x003B358F
		internal static string CapacityMustEqualCountOnMove
		{
			get
			{
				return global::System.SR.GetResourceString("CapacityMustEqualCountOnMove", null);
			}
		}

		// Token: 0x17002E05 RID: 11781
		// (get) Token: 0x060113AA RID: 70570 RVA: 0x003B539C File Offset: 0x003B359C
		internal static string CollectionModifiedDuringEnumeration
		{
			get
			{
				return global::System.SR.GetResourceString("CollectionModifiedDuringEnumeration", null);
			}
		}

		// Token: 0x17002E06 RID: 11782
		// (get) Token: 0x060113AB RID: 70571 RVA: 0x003B53A9 File Offset: 0x003B35A9
		internal static string DuplicateKey
		{
			get
			{
				return global::System.SR.GetResourceString("DuplicateKey", null);
			}
		}

		// Token: 0x17002E07 RID: 11783
		// (get) Token: 0x060113AC RID: 70572 RVA: 0x003B53B6 File Offset: 0x003B35B6
		internal static string InvalidEmptyOperation
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidEmptyOperation", null);
			}
		}

		// Token: 0x17002E08 RID: 11784
		// (get) Token: 0x060113AD RID: 70573 RVA: 0x003B53C3 File Offset: 0x003B35C3
		internal static string InvalidOperationOnDefaultArray
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidOperationOnDefaultArray", null);
			}
		}

		// Token: 0x060113AE RID: 70574 RVA: 0x003B53D0 File Offset: 0x003B35D0
		// Note: this type is marked as 'beforefieldinit'.
		static SR()
		{
			bool flag;
			global::System.SR.s_usingResourceKeys = AppContext.TryGetSwitch("System.Resources.UseSystemResourceKeys", out flag) && flag;
		}

		// Token: 0x0400688A RID: 26762
		private static readonly bool s_usingResourceKeys;

		// Token: 0x0400688B RID: 26763
		private static ResourceManager s_resourceManager;
	}
}
