using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x020009CD RID: 2509
	internal class FieldReaderDataReader : ITableReader, IDisposable
	{
		// Token: 0x06004765 RID: 18277 RVA: 0x000EF27C File Offset: 0x000ED47C
		public FieldReaderDataReader(IFieldReader<IValueReference> reader, int columns)
		{
			this.reader = reader;
			this.columns = columns;
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x000EF292 File Offset: 0x000ED492
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x06004767 RID: 18279 RVA: 0x000EF29F File Offset: 0x000ED49F
		public int Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x000EF2A7 File Offset: 0x000ED4A7
		public bool MoveNext()
		{
			this.current = -1;
			return this.reader.MoveNextRow();
		}

		// Token: 0x170016CA RID: 5834
		public Value this[int column]
		{
			get
			{
				while (this.current < column)
				{
					this.reader.MoveNextField();
					this.current++;
				}
				return this.reader.Current.Value;
			}
		}

		// Token: 0x040025F8 RID: 9720
		private IFieldReader<IValueReference> reader;

		// Token: 0x040025F9 RID: 9721
		private int columns;

		// Token: 0x040025FA RID: 9722
		private int current;
	}
}
