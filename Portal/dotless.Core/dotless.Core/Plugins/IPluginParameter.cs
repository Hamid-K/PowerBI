using System;

namespace dotless.Core.Plugins
{
	// Token: 0x0200001B RID: 27
	public interface IPluginParameter
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A3 RID: 163
		string Name { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A4 RID: 164
		bool IsMandatory { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A5 RID: 165
		object Value { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A6 RID: 166
		string TypeDescription { get; }

		// Token: 0x060000A7 RID: 167
		void SetValue(string value);
	}
}
