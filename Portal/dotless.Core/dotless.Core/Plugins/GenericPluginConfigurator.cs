using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace dotless.Core.Plugins
{
	// Token: 0x02000017 RID: 23
	public class GenericPluginConfigurator<T> : IPluginConfigurator where T : IPlugin
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003A44 File Offset: 0x00001C44
		public string Name
		{
			get
			{
				return PluginFinder.GetName(typeof(T));
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003A55 File Offset: 0x00001C55
		public string Description
		{
			get
			{
				return PluginFinder.GetDescription(typeof(T));
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003A66 File Offset: 0x00001C66
		public Type Configurates
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003A74 File Offset: 0x00001C74
		public void SetParameterValues(IEnumerable<IPluginParameter> pluginParameters)
		{
			ConstructorInfo defaultConstructor;
			ConstructorInfo parameterConstructor;
			this.GetConstructorInfos(out parameterConstructor, out defaultConstructor);
			if (pluginParameters != null && pluginParameters.Count<IPluginParameter>() != 0)
			{
				if (!pluginParameters.All((IPluginParameter parameter) => parameter.Value == null))
				{
					object[] constructorArguments = (from parameter in parameterConstructor.GetParameters()
						orderby parameter.Position
						select parameter).Select(delegate(ParameterInfo parameter)
					{
						IPluginParameter pluginParameter2 = pluginParameters.FirstOrDefault((IPluginParameter pluginParameter) => pluginParameter.Name == parameter.Name);
						if (pluginParameter2 != null)
						{
							return pluginParameter2.Value;
						}
						if (parameter.ParameterType.IsValueType)
						{
							return Activator.CreateInstance(parameter.ParameterType);
						}
						return null;
					}).ToArray<object>();
					this._pluginCreator = () => (T)((object)parameterConstructor.Invoke(constructorArguments));
					return;
				}
			}
			if (defaultConstructor == null)
			{
				throw new Exception("No parameters provided but no default constructor");
			}
			this._pluginCreator = () => (T)((object)defaultConstructor.Invoke(new object[0]));
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003B81 File Offset: 0x00001D81
		public IPlugin CreatePlugin()
		{
			if (this._pluginCreator == null)
			{
				this.SetParameterValues(null);
			}
			return this._pluginCreator();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003BA0 File Offset: 0x00001DA0
		private void GetConstructorInfos(out ConstructorInfo parameterConstructor, out ConstructorInfo defaultConstructor)
		{
			List<ConstructorInfo> list = (from constructorInfo in typeof(T).GetConstructors()
				where constructorInfo.IsPublic && !constructorInfo.IsStatic
				select constructorInfo).ToList<ConstructorInfo>();
			if (list.Count > 2 || list.Count == 0)
			{
				throw new Exception("Generic plugin configurator doesn't support less than 1 or more than 2 constructors. Add your own IPluginConfigurator to the assembly.");
			}
			if (list.Count == 2)
			{
				if (list[0].GetParameters().Length == 0)
				{
					defaultConstructor = list[0];
					parameterConstructor = list[1];
					return;
				}
				if (list[1].GetParameters().Length == 0)
				{
					defaultConstructor = list[1];
					parameterConstructor = list[0];
					return;
				}
				throw new Exception("Generic plugin configurator only supports 1 parameterless constructor and 1 with parameters. Add your own IPluginConfigurator to the assembly.");
			}
			else
			{
				if (list[0].GetParameters().Length == 0)
				{
					defaultConstructor = list[0];
					parameterConstructor = null;
					return;
				}
				defaultConstructor = null;
				parameterConstructor = list[0];
				return;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003C84 File Offset: 0x00001E84
		public IEnumerable<IPluginParameter> GetParameters()
		{
			ConstructorInfo defaultConstructor;
			ConstructorInfo constructorInfo;
			this.GetConstructorInfos(out constructorInfo, out defaultConstructor);
			if (constructorInfo == null)
			{
				return new List<IPluginParameter>();
			}
			return (from parameter in constructorInfo.GetParameters()
				select new PluginParameter(parameter.Name, parameter.ParameterType, defaultConstructor == null)).ToList<IPluginParameter>();
		}

		// Token: 0x0400001B RID: 27
		private Func<IPlugin> _pluginCreator;

		// Token: 0x020000D1 RID: 209
		private class ConstructorParameterSet
		{
			// Token: 0x1700010C RID: 268
			// (get) Token: 0x060005DC RID: 1500 RVA: 0x0001881A File Offset: 0x00016A1A
			// (set) Token: 0x060005DD RID: 1501 RVA: 0x00018822 File Offset: 0x00016A22
			public ParameterInfo[] Parameter { get; set; }

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x060005DE RID: 1502 RVA: 0x0001882B File Offset: 0x00016A2B
			// (set) Token: 0x060005DF RID: 1503 RVA: 0x00018833 File Offset: 0x00016A33
			public int Count { get; set; }
		}
	}
}
