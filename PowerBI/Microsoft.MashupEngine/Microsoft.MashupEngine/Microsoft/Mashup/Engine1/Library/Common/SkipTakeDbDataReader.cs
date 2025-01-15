using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200112C RID: 4396
	internal class SkipTakeDbDataReader : DelegatingDbDataReaderWithTableSchema
	{
		// Token: 0x060072DA RID: 29402 RVA: 0x0018A9C7 File Offset: 0x00188BC7
		public SkipTakeDbDataReader(DbDataReaderWithTableSchema reader, RowRange range)
			: base(reader)
		{
			this.reader = new SkipTakeDataReader(reader, range);
		}

		// Token: 0x060072DB RID: 29403 RVA: 0x0018A9DD File Offset: 0x00188BDD
		public override bool Read()
		{
			return this.reader.Read();
		}

		// Token: 0x04003F4C RID: 16204
		private SkipTakeDataReader reader;
	}
}
