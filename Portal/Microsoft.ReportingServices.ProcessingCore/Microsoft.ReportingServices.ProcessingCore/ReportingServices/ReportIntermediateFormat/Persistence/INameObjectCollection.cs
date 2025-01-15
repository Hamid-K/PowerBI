using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000542 RID: 1346
	internal interface INameObjectCollection
	{
		// Token: 0x06004979 RID: 18809
		void Add(string key, object value);

		// Token: 0x0600497A RID: 18810
		string GetKey(int index);

		// Token: 0x0600497B RID: 18811
		object GetValue(int index);

		// Token: 0x17001DD2 RID: 7634
		// (get) Token: 0x0600497C RID: 18812
		int Count { get; }
	}
}
