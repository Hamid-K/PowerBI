using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012FE RID: 4862
	internal class PermuteColumnsFieldReader<T> : IFieldReader<T>, IDisposable
	{
		// Token: 0x06008088 RID: 32904 RVA: 0x001B6C7D File Offset: 0x001B4E7D
		public PermuteColumnsFieldReader(IFieldReader<T> reader, int[] columns)
		{
			this.reader = reader;
			this.columns = columns;
			this.row = new T[columns.Length];
		}

		// Token: 0x170022D1 RID: 8913
		// (get) Token: 0x06008089 RID: 32905 RVA: 0x001B6CA1 File Offset: 0x001B4EA1
		public T Current
		{
			get
			{
				return this.row[this.column];
			}
		}

		// Token: 0x0600808A RID: 32906 RVA: 0x001B6CB4 File Offset: 0x001B4EB4
		public bool MoveNextRow()
		{
			this.column = -1;
			return this.reader.MoveNextRow();
		}

		// Token: 0x0600808B RID: 32907 RVA: 0x001B6CC8 File Offset: 0x001B4EC8
		public bool MoveNextField()
		{
			if (this.column == -1)
			{
				for (int i = 0; i < this.columns.Length; i++)
				{
					if (!this.reader.MoveNextField())
					{
						throw new InvalidOperationException();
					}
					this.row[this.columns[i]] = this.reader.Current;
				}
				if (this.reader.MoveNextField())
				{
					throw new InvalidOperationException();
				}
			}
			if (this.column + 1 == this.columns.Length)
			{
				return false;
			}
			this.column++;
			return true;
		}

		// Token: 0x0600808C RID: 32908 RVA: 0x001B6D58 File Offset: 0x001B4F58
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x04004608 RID: 17928
		private IFieldReader<T> reader;

		// Token: 0x04004609 RID: 17929
		private int[] columns;

		// Token: 0x0400460A RID: 17930
		private T[] row;

		// Token: 0x0400460B RID: 17931
		private int column;
	}
}
