using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace dotless.Core.Plugins
{
	// Token: 0x0200001F RID: 31
	public static class PluginFinder
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00003CD8 File Offset: 0x00001ED8
		public static string GetName(this IPlugin plugin)
		{
			return PluginFinder.GetName(plugin.GetType());
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003CE5 File Offset: 0x00001EE5
		public static string GetDescription(this IPlugin plugin)
		{
			return PluginFinder.GetName(plugin.GetType());
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public static string GetDescription(Type pluginType)
		{
			DescriptionAttribute descriptionAttribute = pluginType.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault<object>() as DescriptionAttribute;
			if (descriptionAttribute != null)
			{
				return descriptionAttribute.Description;
			}
			return "No Description";
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003D2C File Offset: 0x00001F2C
		public static string GetName(Type pluginType)
		{
			DisplayNameAttribute displayNameAttribute = pluginType.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault<object>() as DisplayNameAttribute;
			if (displayNameAttribute != null)
			{
				return displayNameAttribute.DisplayName;
			}
			return pluginType.Name;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003D68 File Offset: 0x00001F68
		public static IEnumerable<IPluginConfigurator> GetConfigurators(bool scanPluginsFolder)
		{
			List<IEnumerable<IPluginConfigurator>> list = new List<IEnumerable<IPluginConfigurator>>();
			list.Add(PluginFinder.GetConfigurators(Assembly.GetAssembly(typeof(PluginFinder))));
			if (scanPluginsFolder)
			{
				string text = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "plugins");
				if (Directory.Exists(text))
				{
					foreach (FileInfo fileInfo in new DirectoryInfo(text).GetFiles("*.dll"))
					{
						list.Add(PluginFinder.GetConfigurators(Assembly.LoadFile(fileInfo.FullName)));
					}
				}
			}
			return list.Aggregate((IEnumerable<IPluginConfigurator> group1, IEnumerable<IPluginConfigurator> group2) => group1.Union(group2));
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003E1C File Offset: 0x0000201C
		public static IEnumerable<IPluginConfigurator> GetConfigurators(Assembly assembly)
		{
			IEnumerable<Type> enumerable = from type in assembly.GetTypes()
				where !type.IsAbstract && !type.IsGenericType && !type.IsInterface
				select type;
			IEnumerable<IPluginConfigurator> enumerable2 = from type in enumerable
				where typeof(IPluginConfigurator).IsAssignableFrom(type)
				select (IPluginConfigurator)type.GetConstructor(new Type[0]).Invoke(new object[0]);
			IEnumerable<Type> pluginsConfigurated = enumerable2.Select((IPluginConfigurator pluginConfigurator) => pluginConfigurator.Configurates);
			Type genericPluginConfiguratorType = typeof(GenericPluginConfigurator<>);
			return (from type in enumerable
				where typeof(IPlugin).IsAssignableFrom(type)
				where !pluginsConfigurated.Contains(type)
				select (IPluginConfigurator)Activator.CreateInstance(genericPluginConfiguratorType.MakeGenericType(new Type[] { type }))).Union(enumerable2);
		}
	}
}
