using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200112B RID: 4395
	internal class SkipTakeDataReader : DelegatingDataReaderWithTableSchema
	{
		// Token: 0x060072D8 RID: 29400 RVA: 0x0018A914 File Offset: 0x00188B14
		public SkipTakeDataReader(IDataReaderWithTableSchema reader, RowRange range)
			: base(reader)
		{
			this.skip = range.SkipCount.Value;
			this.take = (range.TakeCount.IsInfinite ? long.MaxValue : range.TakeCount.Value);
		}

		// Token: 0x060072D9 RID: 29401 RVA: 0x0018A970 File Offset: 0x00188B70
		public override bool Read()
		{
			if (this.take == 0L)
			{
				return false;
			}
			this.take -= 1L;
			while (this.skip != 0L)
			{
				if (!base.Read())
				{
					this.take = 0L;
					return false;
				}
				this.skip -= 1L;
			}
			return base.Read();
		}

		// Token: 0x04003F4A RID: 16202
		private long skip;

		// Token: 0x04003F4B RID: 16203
		private long take;
	}
}
