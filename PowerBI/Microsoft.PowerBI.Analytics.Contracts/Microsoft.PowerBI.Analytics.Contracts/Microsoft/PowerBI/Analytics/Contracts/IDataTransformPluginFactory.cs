using System;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x02000012 RID: 18
	public interface IDataTransformPluginFactory
	{
		// Token: 0x0600002D RID: 45
		IDataTransformPlugin Create(string name);

		// Token: 0x0600002E RID: 46
		bool HasTransform(string name);
	}
}
