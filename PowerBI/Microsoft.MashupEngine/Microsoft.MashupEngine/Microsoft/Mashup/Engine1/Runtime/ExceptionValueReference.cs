using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012EF RID: 4847
	internal class ExceptionValueReference : IValueReference
	{
		// Token: 0x0600804F RID: 32847 RVA: 0x001B5BF4 File Offset: 0x001B3DF4
		public ExceptionValueReference(ValueException exception)
		{
			this.exception = exception;
		}

		// Token: 0x170022C6 RID: 8902
		// (get) Token: 0x06008050 RID: 32848 RVA: 0x00002105 File Offset: 0x00000305
		public bool Evaluated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170022C7 RID: 8903
		// (get) Token: 0x06008051 RID: 32849 RVA: 0x001B5C03 File Offset: 0x001B3E03
		public Value Value
		{
			get
			{
				throw this.exception;
			}
		}

		// Token: 0x040045DF RID: 17887
		private ValueException exception;
	}
}
