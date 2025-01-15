using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012FA RID: 4858
	internal class SkipTakeFieldReader<T> : IFieldReader<T>, IDisposable
	{
		// Token: 0x06008074 RID: 32884 RVA: 0x001B68E7 File Offset: 0x001B4AE7
		public SkipTakeFieldReader(IFieldReader<T> reader, RowRange range)
		{
			this.reader = reader;
			this.skip = range.SkipCount;
			this.take = range.TakeCount;
		}

		// Token: 0x170022CD RID: 8909
		// (get) Token: 0x06008075 RID: 32885 RVA: 0x001B6910 File Offset: 0x001B4B10
		public T Current
		{
			get
			{
				return this.reader.Current;
			}
		}

		// Token: 0x06008076 RID: 32886 RVA: 0x001B6920 File Offset: 0x001B4B20
		public bool MoveNextRow()
		{
			if (this.take.IsZero)
			{
				return false;
			}
			while (!this.skip.IsZero)
			{
				if (!this.reader.MoveNextRow())
				{
					return false;
				}
				this.skip = RowCount.op_Decrement(this.skip);
			}
			if (!this.reader.MoveNextRow())
			{
				return false;
			}
			this.take = RowCount.op_Decrement(this.take);
			return true;
		}

		// Token: 0x06008077 RID: 32887 RVA: 0x001B698A File Offset: 0x001B4B8A
		public bool MoveNextField()
		{
			return this.reader.MoveNextField();
		}

		// Token: 0x06008078 RID: 32888 RVA: 0x001B6997 File Offset: 0x001B4B97
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x040045F4 RID: 17908
		private IFieldReader<T> reader;

		// Token: 0x040045F5 RID: 17909
		private RowCount skip;

		// Token: 0x040045F6 RID: 17910
		private RowCount take;
	}
}
