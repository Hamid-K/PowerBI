using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200107F RID: 4223
	internal class DbTableWithPageReaderWrapper : WrappingTableValue
	{
		// Token: 0x06006EA1 RID: 28321 RVA: 0x0017E0EC File Offset: 0x0017C2EC
		public DbTableWithPageReaderWrapper(TableValue table, DbEnvironment environment)
			: base(table)
		{
			this.environment = environment;
		}

		// Token: 0x06006EA2 RID: 28322 RVA: 0x0017E0FC File Offset: 0x0017C2FC
		protected override TableValue New(TableValue table)
		{
			return new DbTableWithPageReaderWrapper(table, this.environment);
		}

		// Token: 0x06006EA3 RID: 28323 RVA: 0x0017E10A File Offset: 0x0017C30A
		public override IPageReader GetReader()
		{
			return new DataReaderPageReader(new TableDataReader(this.Type.AsTableType, new TableValueDataReader(this, true), null), new DataReaderPageReader.ExceptionPropertyGetter(this.environment.TryGetPageReaderExceptionProperties));
		}

		// Token: 0x04003D61 RID: 15713
		private readonly DbEnvironment environment;
	}
}
