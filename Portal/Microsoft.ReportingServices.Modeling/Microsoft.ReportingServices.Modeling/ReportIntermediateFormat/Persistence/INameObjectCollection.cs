using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000025 RID: 37
	internal interface INameObjectCollection
	{
		// Token: 0x06000194 RID: 404
		void Add(string key, object value);

		// Token: 0x06000195 RID: 405
		string GetKey(int index);

		// Token: 0x06000196 RID: 406
		object GetValue(int index);

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000197 RID: 407
		int Count { get; }
	}
}
