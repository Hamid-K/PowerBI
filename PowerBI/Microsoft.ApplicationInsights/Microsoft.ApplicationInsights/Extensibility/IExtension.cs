using System;

namespace Microsoft.ApplicationInsights.Extensibility
{
	// Token: 0x02000053 RID: 83
	public interface IExtension : ISerializableWithWriter
	{
		// Token: 0x06000290 RID: 656
		IExtension DeepClone();
	}
}
