using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000172 RID: 370
	public static class ExtendedAppDomain
	{
		// Token: 0x060009B0 RID: 2480 RVA: 0x00021458 File Offset: 0x0001F658
		public static void RunWithPrivateAppDomainWorker<TWorker>(Action<TWorker> action) where TWorker : MarshalByRefObject
		{
			string typeName = typeof(TWorker).FullName;
			ExtendedAppDomain.RunWithPrivateAppDomain("AppDomain_{0}_{1}".FormatWithInvariantCulture(new object[]
			{
				typeName,
				Guid.NewGuid()
			}), delegate(AppDomain appDomain)
			{
				TWorker tworker = (TWorker)((object)appDomain.CreateInstanceAndUnwrap(typeof(TWorker).Assembly.FullName, typeName));
				action(tworker);
			});
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x000214C0 File Offset: 0x0001F6C0
		public static void RunWithPrivateAppDomainWorker<TWorker>(Action<TWorker> action, string loadFromPath) where TWorker : MarshalByRefObject
		{
			string typeName = typeof(TWorker).FullName;
			string text = "AppDomain_{0}_{1}".FormatWithInvariantCulture(new object[]
			{
				typeName,
				Guid.NewGuid()
			});
			string location = typeof(TWorker).Assembly.Location;
			ExtendedDiagnostics.EnsureNotNull<string>(location, "Assembly of TWorker type must be not null. If it's null, it can be due to TWorker type's assembly created by generated code or loaded from stream");
			string assemblyLoadingPath = Path.Combine(loadFromPath, Path.GetFileName(location));
			ExtendedDiagnostics.EnsureOperation(File.Exists(assemblyLoadingPath), "Assembly loading path {0} must exist prior to loading type from it.".FormatWithInvariantCulture(new object[] { assemblyLoadingPath }));
			ExtendedAppDomain.RunWithPrivateAppDomain(text, loadFromPath, delegate(AppDomain appDomain)
			{
				TWorker tworker = (TWorker)((object)appDomain.CreateInstanceFromAndUnwrap(assemblyLoadingPath, typeName, true, BindingFlags.CreateInstance, null, null, CultureInfo.InvariantCulture, null));
				action(tworker);
			});
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00021584 File Offset: 0x0001F784
		public static void RunWithPrivateAppDomain(string name, string basePath, Action<AppDomain> action)
		{
			AppDomain appDomain = null;
			try
			{
				UtilsContext.Current.RunWithClearContext(delegate
				{
					appDomain = AppDomain.CreateDomain(name, AppDomain.CurrentDomain.Evidence, basePath, null, true);
					action(appDomain);
				});
			}
			finally
			{
				if (appDomain != null)
				{
					AppDomain.Unload(appDomain);
				}
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x000215F0 File Offset: 0x0001F7F0
		public static void RunWithPrivateAppDomain(string name, Action<AppDomain> action)
		{
			AppDomain appDomain = null;
			try
			{
				UtilsContext.Current.RunWithClearContext(delegate
				{
					appDomain = AppDomain.CreateDomain(name, AppDomain.CurrentDomain.Evidence, AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath, AppDomain.CurrentDomain.ShadowCopyFiles);
					action(appDomain);
				});
			}
			finally
			{
				if (appDomain != null)
				{
					AppDomain.Unload(appDomain);
				}
			}
		}
	}
}
