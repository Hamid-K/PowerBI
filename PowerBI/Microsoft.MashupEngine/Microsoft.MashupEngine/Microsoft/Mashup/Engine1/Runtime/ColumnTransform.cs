using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200163E RID: 5694
	public struct ColumnTransform
	{
		// Token: 0x06008F5B RID: 36699 RVA: 0x001DD8B8 File Offset: 0x001DBAB8
		public ColumnTransform(FunctionValue function, IValueReference type)
		{
			this.function = function;
			this.type = type;
		}

		// Token: 0x1700258A RID: 9610
		// (get) Token: 0x06008F5C RID: 36700 RVA: 0x001DD8C8 File Offset: 0x001DBAC8
		public FunctionValue Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x1700258B RID: 9611
		// (get) Token: 0x06008F5D RID: 36701 RVA: 0x001DD8D0 File Offset: 0x001DBAD0
		public IValueReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04004D8B RID: 19851
		private readonly FunctionValue function;

		// Token: 0x04004D8C RID: 19852
		private readonly IValueReference type;
	}
}
