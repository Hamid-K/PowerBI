using System;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x02000423 RID: 1059
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class InductionPluginAttribute : Attribute
	{
		// Token: 0x060020A6 RID: 8358 RVA: 0x0007ACF8 File Offset: 0x00078EF8
		public InductionPluginAttribute(string configurationType, string pluginAssembly, string pluginType)
		{
			this.ConfigurationType = configurationType;
			this.PluginAssembly = pluginAssembly;
			this.PluginType = pluginType;
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x0007AD15 File Offset: 0x00078F15
		// (set) Token: 0x060020A8 RID: 8360 RVA: 0x0007AD1D File Offset: 0x00078F1D
		public string ConfigurationType { get; private set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x0007AD26 File Offset: 0x00078F26
		// (set) Token: 0x060020AA RID: 8362 RVA: 0x0007AD2E File Offset: 0x00078F2E
		public string PluginAssembly { get; private set; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x0007AD37 File Offset: 0x00078F37
		// (set) Token: 0x060020AC RID: 8364 RVA: 0x0007AD3F File Offset: 0x00078F3F
		public string PluginType { get; private set; }
	}
}
