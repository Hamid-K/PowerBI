using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012FD RID: 4861
	internal class SelectColumnsFieldReader<T> : IFieldReader<T>, IDisposable
	{
		// Token: 0x06008083 RID: 32899 RVA: 0x001B6BD2 File Offset: 0x001B4DD2
		public SelectColumnsFieldReader(IFieldReader<T> reader, int[] columns)
		{
			this.reader = reader;
			this.columns = columns;
		}

		// Token: 0x170022D0 RID: 8912
		// (get) Token: 0x06008084 RID: 32900 RVA: 0x001B6BE8 File Offset: 0x001B4DE8
		public T Current
		{
			get
			{
				return this.reader.Current;
			}
		}

		// Token: 0x06008085 RID: 32901 RVA: 0x001B6BF5 File Offset: 0x001B4DF5
		public bool MoveNextRow()
		{
			this.column = -1;
			this.index = 0;
			return this.reader.MoveNextRow();
		}

		// Token: 0x06008086 RID: 32902 RVA: 0x001B6C10 File Offset: 0x001B4E10
		public bool MoveNextField()
		{
			if (this.index == this.columns.Length)
			{
				return false;
			}
			while (this.column != this.columns[this.index])
			{
				if (!this.reader.MoveNextField())
				{
					return false;
				}
				this.column++;
			}
			this.index++;
			return true;
		}

		// Token: 0x06008087 RID: 32903 RVA: 0x001B6C70 File Offset: 0x001B4E70
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x04004604 RID: 17924
		private IFieldReader<T> reader;

		// Token: 0x04004605 RID: 17925
		private int[] columns;

		// Token: 0x04004606 RID: 17926
		private int column;

		// Token: 0x04004607 RID: 17927
		private int index;
	}
}
