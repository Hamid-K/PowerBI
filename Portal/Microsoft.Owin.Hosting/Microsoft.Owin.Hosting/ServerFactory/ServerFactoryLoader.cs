using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Microsoft.Owin.Hosting.ServerFactory
{
	// Token: 0x02000022 RID: 34
	public class ServerFactoryLoader : IServerFactoryLoader
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00004009 File Offset: 0x00002209
		public ServerFactoryLoader(IServerFactoryActivator activator)
		{
			this._activator = activator;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004018 File Offset: 0x00002218
		public virtual IServerFactoryAdapter Load(string serverName)
		{
			if (string.IsNullOrWhiteSpace(serverName))
			{
				return null;
			}
			Type serverFactoryType = ServerFactoryLoader.GetTypeAndMethodNameForConfigurationString(serverName);
			if (serverFactoryType == null)
			{
				return null;
			}
			return new ServerFactoryAdapter(serverFactoryType, this._activator);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004050 File Offset: 0x00002250
		private static Type GetTypeAndMethodNameForConfigurationString(string configuration)
		{
			foreach (Tuple<string, Assembly> hit in ServerFactoryLoader.HuntForAssemblies(configuration))
			{
				string longestPossibleName = hit.Item1;
				Assembly assembly = hit.Item2;
				foreach (string typeName in ServerFactoryLoader.DotByDot(longestPossibleName).Take(2))
				{
					Type type = assembly.GetType(typeName, false);
					if (!(type == null))
					{
						return type;
					}
				}
			}
			return null;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004104 File Offset: 0x00002304
		private static IEnumerable<Tuple<string, Assembly>> HuntForAssemblies(string configurationString)
		{
			if (configurationString == null)
			{
				yield break;
			}
			int commaIndex = configurationString.IndexOf(',');
			if (commaIndex >= 0)
			{
				string methodOrTypeName = ServerFactoryLoader.DotByDot(configurationString.Substring(0, commaIndex)).FirstOrDefault<string>();
				string assemblyName = configurationString.Substring(commaIndex + 1).Trim();
				Assembly assembly = ServerFactoryLoader.TryAssemblyLoad(assemblyName);
				if (assembly != null)
				{
					yield return Tuple.Create<string, Assembly>(methodOrTypeName, assembly);
				}
			}
			else
			{
				string typeName = ServerFactoryLoader.DotByDot(configurationString).FirstOrDefault<string>();
				foreach (string assemblyName2 in ServerFactoryLoader.DotByDot(typeName))
				{
					Assembly assembly2 = ServerFactoryLoader.TryAssemblyLoad(assemblyName2);
					if (assembly2 != null)
					{
						if (assemblyName2.Length == typeName.Length)
						{
							yield return Tuple.Create<string, Assembly>(assemblyName2 + ".OwinServerFactory", assembly2);
						}
						else
						{
							yield return Tuple.Create<string, Assembly>(typeName, assembly2);
						}
					}
				}
				IEnumerator<string> enumerator = null;
				typeName = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004114 File Offset: 0x00002314
		private static Assembly TryAssemblyLoad(string assemblyName)
		{
			Assembly assembly;
			try
			{
				assembly = Assembly.Load(assemblyName);
			}
			catch (FileNotFoundException)
			{
				assembly = null;
			}
			return assembly;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004140 File Offset: 0x00002340
		private static IEnumerable<string> DotByDot(string text)
		{
			if (text == null)
			{
				yield break;
			}
			text = text.Trim(new char[] { '.' });
			for (int length = text.Length; length > 0; length = text.LastIndexOf('.', length - 1, length - 1))
			{
				yield return text.Substring(0, length);
			}
			yield break;
		}

		// Token: 0x0400003B RID: 59
		private readonly IServerFactoryActivator _activator;
	}
}
