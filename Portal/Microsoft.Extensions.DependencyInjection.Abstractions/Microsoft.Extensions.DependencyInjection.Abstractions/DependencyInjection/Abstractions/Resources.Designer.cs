using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Microsoft.Extensions.DependencyInjection.Abstractions
{
	// Token: 0x0200000D RID: 13
	internal static class Resources
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002934 File Offset: 0x00000B34
		internal static string AmbiguousConstructorMatch
		{
			get
			{
				return Resources.GetString("AmbiguousConstructorMatch", new string[0]);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002946 File Offset: 0x00000B46
		internal static string FormatAmbiguousConstructorMatch(object p0)
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.GetString("AmbiguousConstructorMatch", new string[0]), new object[] { p0 });
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000296C File Offset: 0x00000B6C
		internal static string CannotLocateImplementation
		{
			get
			{
				return Resources.GetString("CannotLocateImplementation", new string[0]);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000297E File Offset: 0x00000B7E
		internal static string FormatCannotLocateImplementation(object p0, object p1)
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.GetString("CannotLocateImplementation", new string[0]), new object[] { p0, p1 });
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000029A8 File Offset: 0x00000BA8
		internal static string CannotResolveService
		{
			get
			{
				return Resources.GetString("CannotResolveService", new string[0]);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000029BA File Offset: 0x00000BBA
		internal static string FormatCannotResolveService(object p0, object p1)
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.GetString("CannotResolveService", new string[0]), new object[] { p0, p1 });
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000029E4 File Offset: 0x00000BE4
		internal static string NoConstructorMatch
		{
			get
			{
				return Resources.GetString("NoConstructorMatch", new string[0]);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000029F6 File Offset: 0x00000BF6
		internal static string FormatNoConstructorMatch(object p0)
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.GetString("NoConstructorMatch", new string[0]), new object[] { p0 });
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002A1C File Offset: 0x00000C1C
		internal static string NoServiceRegistered
		{
			get
			{
				return Resources.GetString("NoServiceRegistered", new string[0]);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002A2E File Offset: 0x00000C2E
		internal static string FormatNoServiceRegistered(object p0)
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.GetString("NoServiceRegistered", new string[0]), new object[] { p0 });
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002A54 File Offset: 0x00000C54
		internal static string TryAddIndistinguishableTypeToEnumerable
		{
			get
			{
				return Resources.GetString("TryAddIndistinguishableTypeToEnumerable", new string[0]);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002A66 File Offset: 0x00000C66
		internal static string FormatTryAddIndistinguishableTypeToEnumerable(object p0, object p1)
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.GetString("TryAddIndistinguishableTypeToEnumerable", new string[0]), new object[] { p0, p1 });
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002A90 File Offset: 0x00000C90
		private static string GetString(string name, params string[] formatterNames)
		{
			string text = Resources._resourceManager.GetString(name);
			if (formatterNames != null)
			{
				for (int i = 0; i < formatterNames.Length; i++)
				{
					text = text.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
				}
			}
			return text;
		}

		// Token: 0x0400000A RID: 10
		private static readonly ResourceManager _resourceManager = new ResourceManager("Microsoft.Extensions.DependencyInjection.Abstractions.Resources", typeof(Resources).GetTypeInfo().Assembly);
	}
}
