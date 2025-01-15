using System;

namespace Microsoft.ApplicationInsights.Extensibility
{
	// Token: 0x02000055 RID: 85
	public interface ISerializableWithWriter
	{
		// Token: 0x06000292 RID: 658
		void Serialize(ISerializationWriter serializationWriter);
	}
}
