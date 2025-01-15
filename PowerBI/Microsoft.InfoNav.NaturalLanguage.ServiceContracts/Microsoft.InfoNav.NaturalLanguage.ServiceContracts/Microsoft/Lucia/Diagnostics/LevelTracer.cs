using System;

namespace Microsoft.Lucia.Diagnostics
{
	// Token: 0x0200003B RID: 59
	public abstract class LevelTracer
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00003CAC File Offset: 0x00001EAC
		protected LevelTracer([Nullable] IFormatProvider formatProvider = null)
		{
			this._formatProvider = DiagnosticFormatProvider.GetInstanceOrDefault(formatProvider);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003CC0 File Offset: 0x00001EC0
		public void Trace(IFormattable message)
		{
			this.TraceStringCore(message.ToString(null, this._formatProvider));
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003CD5 File Offset: 0x00001ED5
		public void SanitizedTrace(IFormattable message)
		{
			this.SanitizedTraceStringCore(message.ToString(null, this._formatProvider));
		}

		// Token: 0x060000F0 RID: 240
		protected internal abstract void TraceStringCore(string message);

		// Token: 0x060000F1 RID: 241
		protected internal abstract void SanitizedTraceStringCore(string message);

		// Token: 0x04000064 RID: 100
		private readonly DiagnosticFormatProvider _formatProvider;
	}
}
