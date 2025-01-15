using System;
using System.Collections.Generic;

namespace dotless.Core.Plugins
{
	// Token: 0x0200001A RID: 26
	public interface IPluginConfigurator
	{
		// Token: 0x0600009D RID: 157
		IPlugin CreatePlugin();

		// Token: 0x0600009E RID: 158
		IEnumerable<IPluginParameter> GetParameters();

		// Token: 0x0600009F RID: 159
		void SetParameterValues(IEnumerable<IPluginParameter> parameters);

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A0 RID: 160
		string Name { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A1 RID: 161
		string Description { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A2 RID: 162
		Type Configurates { get; }
	}
}
