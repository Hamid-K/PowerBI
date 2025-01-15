using System;

namespace Microsoft.ReportingServices.DataProcessing
{
	// Token: 0x0200002A RID: 42
	public interface IDataReader : IDisposable
	{
		// Token: 0x060000AB RID: 171
		string GetName(int fieldIndex);

		// Token: 0x060000AC RID: 172
		int GetOrdinal(string fieldName);

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000AD RID: 173
		int FieldCount { get; }

		// Token: 0x060000AE RID: 174
		bool Read();

		// Token: 0x060000AF RID: 175
		Type GetFieldType(int fieldIndex);

		// Token: 0x060000B0 RID: 176
		object GetValue(int fieldIndex);
	}
}
