using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x02000409 RID: 1033
	internal sealed class ConfigurationContainer : IConfigurationContainer
	{
		// Token: 0x06001F7A RID: 8058 RVA: 0x00075E83 File Offset: 0x00074083
		public ConfigurationContainer([NotNull] CcsEventHandler callback, [NotNull] Dictionary<Type, IConfigurationClass> configurations)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<CcsEventHandler>(callback, "callback");
			ExtendedDiagnostics.EnsureArgumentNotNull<Dictionary<Type, IConfigurationClass>>(configurations, "configurations");
			this.m_callback = callback;
			this.m_configurations = new Dictionary<Type, IConfigurationClass>(configurations);
			this.m_callbackLock = new object();
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x00075EC0 File Offset: 0x000740C0
		public T GetConfiguration<T>()
		{
			Type typeFromHandle = typeof(T);
			if (typeof(IConfigurationParser).IsAssignableFrom(typeFromHandle))
			{
				IConfigurationParser configurationParser = (IConfigurationParser)typeFromHandle.InvokeMember(null, BindingFlags.CreateInstance, null, null, new object[0], CultureInfo.InvariantCulture);
				configurationParser.Initialize(this);
				return (T)((object)configurationParser);
			}
			IConfigurationClass configurationClass;
			if (!this.m_configurations.TryGetValue(typeFromHandle, out configurationClass))
			{
				throw new CcsNoConfigurationDataExistException("The requested configuration type of '{0}' does not belong to the configuration group.\r\n                    Did you forget to subscribe to the configuration you're requesting?".FormatWithInvariantCulture(new object[] { typeFromHandle }));
			}
			return (T)((object)configurationClass);
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x00075F48 File Offset: 0x00074148
		public void RaiseEvent()
		{
			object callbackLock = this.m_callbackLock;
			lock (callbackLock)
			{
				this.m_callback(this);
			}
		}

		// Token: 0x04000B05 RID: 2821
		private readonly Dictionary<Type, IConfigurationClass> m_configurations;

		// Token: 0x04000B06 RID: 2822
		private readonly CcsEventHandler m_callback;

		// Token: 0x04000B07 RID: 2823
		private readonly object m_callbackLock;
	}
}
