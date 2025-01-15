using System;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000179 RID: 377
	internal interface IPropertyWriter
	{
		// Token: 0x060017B8 RID: 6072
		void WriteProperty<T>(WriteOptions options, string name, T value);

		// Token: 0x060017B9 RID: 6073
		void WriteProperty(WriteOptions options, string name, Type type, object value);
	}
}
