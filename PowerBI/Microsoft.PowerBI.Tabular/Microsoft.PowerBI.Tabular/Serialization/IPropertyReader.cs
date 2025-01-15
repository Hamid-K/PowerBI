using System;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000178 RID: 376
	internal interface IPropertyReader
	{
		// Token: 0x060017B7 RID: 6071
		bool TryReadProperty<T>(string propName, out T propValue);
	}
}
