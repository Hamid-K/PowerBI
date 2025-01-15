using System;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FB7 RID: 8119
	public interface IOleDbColumn : IColumn
	{
		// Token: 0x0600C611 RID: 50705
		unsafe DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength);
	}
}
