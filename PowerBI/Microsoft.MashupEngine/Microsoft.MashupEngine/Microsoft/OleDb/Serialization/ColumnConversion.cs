using System;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FAC RID: 8108
	public sealed class ColumnConversion
	{
		// Token: 0x0600C5BD RID: 50621 RVA: 0x00276978 File Offset: 0x00274B78
		public ColumnConversion(Type resultType, Action<object, Column> addValue)
		{
			this.resultType = resultType;
			this.addValue = addValue;
		}

		// Token: 0x17002FFD RID: 12285
		// (get) Token: 0x0600C5BE RID: 50622 RVA: 0x0027698E File Offset: 0x00274B8E
		public Type ResultType
		{
			get
			{
				return this.resultType;
			}
		}

		// Token: 0x17002FFE RID: 12286
		// (get) Token: 0x0600C5BF RID: 50623 RVA: 0x00276996 File Offset: 0x00274B96
		public Action<object, Column> AddValue
		{
			get
			{
				return this.addValue;
			}
		}

		// Token: 0x0400650D RID: 25869
		private readonly Type resultType;

		// Token: 0x0400650E RID: 25870
		private readonly Action<object, Column> addValue;
	}
}
