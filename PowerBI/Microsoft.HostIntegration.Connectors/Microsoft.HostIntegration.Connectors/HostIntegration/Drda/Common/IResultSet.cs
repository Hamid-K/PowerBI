using System;
using System.Collections;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000820 RID: 2080
	public interface IResultSet
	{
		// Token: 0x060041C4 RID: 16836
		bool ReadFirst();

		// Token: 0x060041C5 RID: 16837
		bool ReadLast();

		// Token: 0x060041C6 RID: 16838
		bool Read();

		// Token: 0x060041C7 RID: 16839
		bool ReadPrior();

		// Token: 0x060041C8 RID: 16840
		bool ReadAbsolute(int rowIndex);

		// Token: 0x060041C9 RID: 16841
		bool ReadRelative(int rowInterval);

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x060041CA RID: 16842
		// (set) Token: 0x060041CB RID: 16843
		ISchemaTable SchemaTable { get; set; }

		// Token: 0x060041CC RID: 16844
		ArrayList GetExtDtaObjects();

		// Token: 0x060041CD RID: 16845
		int GetPrecision(int index);

		// Token: 0x060041CE RID: 16846
		int GetScale(int index);

		// Token: 0x060041CF RID: 16847
		DrdaType GetDrdaType(int index);

		// Token: 0x060041D0 RID: 16848
		int GetLength(int index);

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x060041D1 RID: 16849
		int RowSetIndex { get; }

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x060041D2 RID: 16850
		uint RowSetCount { get; }

		// Token: 0x060041D3 RID: 16851
		void AddRowSet(IRowSet row);
	}
}
