using System;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x0200067C RID: 1660
	internal sealed class OdbcTypeMatchCriteria
	{
		// Token: 0x0600342D RID: 13357 RVA: 0x000A79EC File Offset: 0x000A5BEC
		public OdbcTypeMatchCriteria(Odbc32.SQL_TYPE sqlType, int size)
		{
			this.sqlType = sqlType;
			this.size = size;
		}

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x0600342E RID: 13358 RVA: 0x000A7A02 File Offset: 0x000A5C02
		public Odbc32.SQL_TYPE SqlType
		{
			get
			{
				return this.sqlType;
			}
		}

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x0600342F RID: 13359 RVA: 0x000A7A0A File Offset: 0x000A5C0A
		public int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x0400175F RID: 5983
		private readonly Odbc32.SQL_TYPE sqlType;

		// Token: 0x04001760 RID: 5984
		private readonly int size;
	}
}
