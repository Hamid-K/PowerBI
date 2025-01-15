using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.ValueTuple;

namespace System
{
	// Token: 0x0200000E RID: 14
	internal static class SR
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00007F45 File Offset: 0x00006145
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

		// Token: 0x060000D9 RID: 217 RVA: 0x00004307 File Offset: 0x00002507
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00007F60 File Offset: 0x00006160
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

		// Token: 0x060000DB RID: 219 RVA: 0x00007FA0 File Offset: 0x000061A0
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

		// Token: 0x060000DC RID: 220 RVA: 0x00007FC7 File Offset: 0x000061C7
		internal static string Format(string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00007FF0 File Offset: 0x000061F0
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000801E File Offset: 0x0000621E
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00008051 File Offset: 0x00006251
		internal static Type ResourceType { get; } = typeof(FxResources.System.ValueTuple.SR);

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00008058 File Offset: 0x00006258
		internal static string ArgumentException_ValueTupleIncorrectType
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentException_ValueTupleIncorrectType", null);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00008065 File Offset: 0x00006265
		internal static string ArgumentException_ValueTupleLastArgumentNotAValueTuple
		{
			get
			{
				return global::System.SR.GetResourceString("ArgumentException_ValueTupleLastArgumentNotAValueTuple", null);
			}
		}

		// Token: 0x04000049 RID: 73
		private static ResourceManager s_resourceManager;
	}
}
