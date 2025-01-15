using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000EA RID: 234
	public interface ITableValue : IValue
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600038A RID: 906
		IEnumerable<IRelationship> Relationships { get; }

		// Token: 0x0600038B RID: 907
		IColumnIdentity ColumnIdentity(int index);

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600038C RID: 908
		long RowCount { get; }

		// Token: 0x17000166 RID: 358
		IValue this[int index] { get; }

		// Token: 0x17000167 RID: 359
		IListValue this[string column] { get; }

		// Token: 0x0600038F RID: 911
		IEnumerator<IValueReference2> GetEnumerator();
	}
}
