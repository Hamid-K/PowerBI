using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200163D RID: 5693
	public class ComputedColumn
	{
		// Token: 0x06008F56 RID: 36694 RVA: 0x001DD874 File Offset: 0x001DBA74
		public ComputedColumn(int column, FunctionValue function, IValueReference type)
		{
			this.column = column;
			this.function = function;
			this.type = type;
		}

		// Token: 0x17002587 RID: 9607
		// (get) Token: 0x06008F57 RID: 36695 RVA: 0x001DD891 File Offset: 0x001DBA91
		public int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x17002588 RID: 9608
		// (get) Token: 0x06008F58 RID: 36696 RVA: 0x001DD899 File Offset: 0x001DBA99
		public FunctionValue Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x17002589 RID: 9609
		// (get) Token: 0x06008F59 RID: 36697 RVA: 0x001DD8A1 File Offset: 0x001DBAA1
		public IValueReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06008F5A RID: 36698 RVA: 0x001DD8A9 File Offset: 0x001DBAA9
		public ComputedColumn SelectColumns(int newColumn, FunctionValue newFunction)
		{
			return new ComputedColumn(newColumn, newFunction, this.type);
		}

		// Token: 0x04004D88 RID: 19848
		private readonly int column;

		// Token: 0x04004D89 RID: 19849
		private readonly FunctionValue function;

		// Token: 0x04004D8A RID: 19850
		private readonly IValueReference type;
	}
}
