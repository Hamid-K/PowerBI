using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x02000018 RID: 24
	public interface IDataReader : IDisposable
	{
		// Token: 0x06000035 RID: 53
		string GetName(int fieldIndex);

		// Token: 0x06000036 RID: 54
		int GetOrdinal(string fieldName);

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000037 RID: 55
		int FieldCount { get; }

		// Token: 0x06000038 RID: 56
		bool Read();

		// Token: 0x06000039 RID: 57
		Type GetFieldType(int fieldIndex);

		// Token: 0x0600003A RID: 58
		object GetValue(int fieldIndex);
	}
}
