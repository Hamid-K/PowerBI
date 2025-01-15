using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Microsoft.Extensions.DependencyInjection
{
	// Token: 0x02000007 RID: 7
	internal static class Resources
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002598 File Offset: 0x00000798
		internal static string AmbigiousConstructorException
		{
			get
			{
				return Resources.GetString("AmbigiousConstructorException", new string[0]);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025AA File Offset: 0x000007AA
		internal static string FormatAmbigiousConstructorException(object p0)
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.GetString("AmbigiousConstructorException", new string[0]), new object[] { p0 });
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000025D0 File Offset: 0x000007D0
		internal static string CannotResolveService
		{
			get
			{
				return Resources.GetString("CannotResolveService", new string[0]);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025E2 File Offset: 0x000007E2
		internal static string FormatCannotResolveService(object p0, object p1)
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.GetString("CannotResolveService", new string[0]), new object[] { p0, p1 });
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000260C File Offset: 0x0000080C
		internal static string CircularDependencyException
		{
			get
			{
				return Resources.GetString("CircularDependencyException", new string[0]);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000261E File Offset: 0x0000081E
		internal static string FormatCircularDependencyException(object p0)
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.GetString("CircularDependencyException", new string[0]), new object[] { p0 });
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002644 File Offset: 0x00000844
		internal static string UnableToActivateTypeException
		{
			get
			{
				return Resources.GetString("UnableToActivateTypeException", new string[0]);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002656 File Offset: 0x00000856
		internal static string FormatUnableToActivateTypeException(object p0)
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.GetString("UnableToActivateTypeException", new string[0]), new object[] { p0 });
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000267C File Offset: 0x0000087C
		internal static string OpenGenericServiceRequiresOpenGenericImplementation
		{
			get
			{
				return Resources.GetString("OpenGenericServiceRequiresOpenGenericImplementation", new string[0]);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000268E File Offset: 0x0000088E
		internal static string FormatOpenGenericServiceRequiresOpenGenericImplementation(object p0)
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.GetString("OpenGenericServiceRequiresOpenGenericImplementation", new string[0]), new object[] { p0 });
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000026B4 File Offset: 0x000008B4
		internal static string TypeCannotBeActivated
		{
			get
			{
				return Resources.GetString("TypeCannotBeActivated", new string[0]);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026C6 File Offset: 0x000008C6
		internal static string FormatTypeCannotBeActivated(object p0, object p1)
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.GetString("TypeCannotBeActivated", new string[0]), new object[] { p0, p1 });
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000026F0 File Offset: 0x000008F0
		internal static string NoConstructorMatch
		{
			get
			{
				return Resources.GetString("NoConstructorMatch", new string[0]);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002702 File Offset: 0x00000902
		internal static string FormatNoConstructorMatch(object p0)
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.GetString("NoConstructorMatch", new string[0]), new object[] { p0 });
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002728 File Offset: 0x00000928
		internal static string ScopedInSingletonException
		{
			get
			{
				return Resources.GetString("ScopedInSingletonException", new string[0]);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000273A File Offset: 0x0000093A
		internal static string FormatScopedInSingletonException(object p0, object p1, object p2, object p3)
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.GetString("ScopedInSingletonException", new string[0]), new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000276C File Offset: 0x0000096C
		internal static string ScopedResolvedFromRootException
		{
			get
			{
				return Resources.GetString("ScopedResolvedFromRootException", new string[0]);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000277E File Offset: 0x0000097E
		internal static string FormatScopedResolvedFromRootException(object p0, object p1, object p2)
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.GetString("ScopedResolvedFromRootException", new string[0]), new object[] { p0, p1, p2 });
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000027AC File Offset: 0x000009AC
		internal static string DirectScopedResolvedFromRootException
		{
			get
			{
				return Resources.GetString("DirectScopedResolvedFromRootException", new string[0]);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027BE File Offset: 0x000009BE
		internal static string FormatDirectScopedResolvedFromRootException(object p0, object p1)
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.GetString("DirectScopedResolvedFromRootException", new string[0]), new object[] { p0, p1 });
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027E8 File Offset: 0x000009E8
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
		private static readonly ResourceManager _resourceManager = new ResourceManager("Microsoft.Extensions.DependencyInjection.Resources", typeof(Resources).GetTypeInfo().Assembly);
	}
}
