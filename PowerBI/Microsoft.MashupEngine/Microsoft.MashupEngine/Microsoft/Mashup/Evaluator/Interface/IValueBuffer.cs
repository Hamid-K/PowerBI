using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E34 RID: 7732
	public interface IValueBuffer : IDisposable
	{
		// Token: 0x0600BE49 RID: 48713
		IDataReaderSource GetDataReaderSource(string key);

		// Token: 0x0600BE4A RID: 48714
		void SetDataReaderSource(string key, IDataReaderSource dataReaderSource, Action<int> writeCallback);

		// Token: 0x0600BE4B RID: 48715
		void SetException(string key, ValueException2 exception, Action<int> writeCallback);
	}
}
