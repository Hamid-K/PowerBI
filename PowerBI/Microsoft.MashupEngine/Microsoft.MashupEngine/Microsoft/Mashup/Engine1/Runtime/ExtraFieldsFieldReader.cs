using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012FC RID: 4860
	internal class ExtraFieldsFieldReader<T> : IFieldReader<T>, IDisposable where T : class
	{
		// Token: 0x0600807E RID: 32894 RVA: 0x001B6A94 File Offset: 0x001B4C94
		public ExtraFieldsFieldReader(IFieldReader<T> reader, int width, T defaultValue, Func<T[], T> coalesce)
		{
			this.reader = reader;
			this.width = width;
			this.defaultValue = defaultValue;
			this.coalesce = coalesce;
		}

		// Token: 0x170022CF RID: 8911
		// (get) Token: 0x0600807F RID: 32895 RVA: 0x001B6AB9 File Offset: 0x001B4CB9
		public T Current
		{
			get
			{
				if (this.extra != null)
				{
					return this.extra;
				}
				if (this.eol)
				{
					return this.defaultValue;
				}
				return this.reader.Current;
			}
		}

		// Token: 0x06008080 RID: 32896 RVA: 0x001B6AE9 File Offset: 0x001B4CE9
		public bool MoveNextRow()
		{
			this.column = 0;
			this.extra = default(T);
			this.eol = false;
			return this.reader.MoveNextRow();
		}

		// Token: 0x06008081 RID: 32897 RVA: 0x001B6B10 File Offset: 0x001B4D10
		public bool MoveNextField()
		{
			if (this.extra != null)
			{
				return false;
			}
			if (!this.eol && !this.reader.MoveNextField())
			{
				this.eol = true;
			}
			if (this.column < this.width - 1)
			{
				this.column++;
				return true;
			}
			if (this.eol)
			{
				this.extra = this.coalesce(new T[0]);
			}
			else
			{
				List<T> list = new List<T>();
				do
				{
					list.Add(this.reader.Current);
				}
				while (this.reader.MoveNextField());
				this.extra = this.coalesce(list.ToArray());
			}
			return true;
		}

		// Token: 0x06008082 RID: 32898 RVA: 0x001B6BC5 File Offset: 0x001B4DC5
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x040045FD RID: 17917
		private IFieldReader<T> reader;

		// Token: 0x040045FE RID: 17918
		private int width;

		// Token: 0x040045FF RID: 17919
		private T defaultValue;

		// Token: 0x04004600 RID: 17920
		private int column;

		// Token: 0x04004601 RID: 17921
		private T extra;

		// Token: 0x04004602 RID: 17922
		private bool eol;

		// Token: 0x04004603 RID: 17923
		private Func<T[], T> coalesce;
	}
}
