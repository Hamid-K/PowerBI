using System;
using System.Data.Common;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x0200017A RID: 378
	internal abstract class ValueConversion
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000728 RID: 1832
		public abstract Type ResultType { get; }

		// Token: 0x06000729 RID: 1833
		public abstract object GetValue(DbDataReader reader, int ordinal);

		// Token: 0x0600072A RID: 1834
		public abstract object GetValue(object[] inputArray, int index);

		// Token: 0x0600072B RID: 1835
		public abstract string GetString(DbDataReader reader, int ordinal);
	}
}
