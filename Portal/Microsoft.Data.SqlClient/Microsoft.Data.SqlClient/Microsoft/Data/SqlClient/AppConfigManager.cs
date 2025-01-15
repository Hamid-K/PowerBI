using System;
using System.Configuration;
using System.Reflection;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000031 RID: 49
	internal sealed class AppConfigManager
	{
		// Token: 0x060006E6 RID: 1766 RVA: 0x0000E4F8 File Offset: 0x0000C6F8
		public static T FetchConfigurationSection<T>(string name)
		{
			string name2 = MethodBase.GetCurrentMethod().Name;
			object obj = null;
			try
			{
				obj = ConfigurationManager.GetSection(name);
			}
			catch (Exception ex)
			{
				SqlClientEventSource.Log.TryTraceEvent<string, string, string, Exception>("<sc.{0}.{1}|INFO>: Unable to load section `{2}`. ConfigurationManager failed to load due to configuration errors: {3}", "AppConfigManager", name2, name, ex);
			}
			if (obj != null)
			{
				Type typeFromHandle = typeof(T);
				ConfigurationSection configurationSection = obj as ConfigurationSection;
				if (configurationSection != null && configurationSection.GetType() == typeFromHandle)
				{
					SqlClientEventSource.Log.TryTraceEvent<string, string, string>("<sc.{0}.{1}|INFO> Successfully loaded the configurable retry logic settings from the configuration file's section '{2}'.", "AppConfigManager", name2, name);
					return (T)((object)obj);
				}
				SqlClientEventSource.Log.TraceEvent<string, string, string, string>("<sc.{0}.{1}|INFO>: Found a custom {2} configuration but it is not of type {3}.", "AppConfigManager", name2, name, typeFromHandle.FullName);
			}
			SqlClientEventSource.Log.TryTraceEvent<string, string, string, string>("<sc.{0}.{1}|INFO>: Unable to load custom `{2}`. Default value of `{3}` type returns.", "AppConfigManager", name2, name, "T");
			return default(T);
		}

		// Token: 0x040000B0 RID: 176
		private const string TypeName = "AppConfigManager";
	}
}
