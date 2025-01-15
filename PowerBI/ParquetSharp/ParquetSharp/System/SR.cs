using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.ValueTuple;

namespace System
{
	// Token: 0x020000BA RID: 186
	internal static class SR
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0001865C File Offset: 0x0001685C
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

		// Token: 0x0600060F RID: 1551 RVA: 0x0001867C File Offset: 0x0001687C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00018680 File Offset: 0x00016880
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

		// Token: 0x06000611 RID: 1553 RVA: 0x000186CC File Offset: 0x000168CC
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

		// Token: 0x06000612 RID: 1554 RVA: 0x000186FC File Offset: 0x000168FC
		internal static string Format(string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00018728 File Offset: 0x00016928
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001875C File Offset: 0x0001695C
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00018794 File Offset: 0x00016994
		internal static Type ResourceType { get; } = typeof(FxResources.System.ValueTuple.SR);

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0001879C File Offset: 0x0001699C
		internal static string ArgumentException_ValueTupleIncorrectType
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentException_ValueTupleIncorrectType", null);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x000187AC File Offset: 0x000169AC
		internal static string ArgumentException_ValueTupleLastArgumentNotAValueTuple
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentException_ValueTupleLastArgumentNotAValueTuple", null);
			}
		}

		// Token: 0x040001D5 RID: 469
		private static ResourceManager s_resourceManager;
	}
}
