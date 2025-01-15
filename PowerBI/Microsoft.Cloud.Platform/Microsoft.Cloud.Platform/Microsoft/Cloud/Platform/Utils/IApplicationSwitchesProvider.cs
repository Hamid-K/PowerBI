using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000179 RID: 377
	public interface IApplicationSwitchesProvider
	{
		// Token: 0x1700018A RID: 394
		string this[string name] { get; }

		// Token: 0x060009DE RID: 2526
		bool GetBoolSwitch(string name, out bool specified);

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060009DF RID: 2527
		string Name { get; }
	}
}
