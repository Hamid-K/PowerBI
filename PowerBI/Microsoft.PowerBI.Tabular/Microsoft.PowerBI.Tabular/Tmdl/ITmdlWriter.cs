using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000131 RID: 305
	internal interface ITmdlWriter
	{
		// Token: 0x060014A3 RID: 5283
		IDisposable Indent(int count = 1);

		// Token: 0x060014A4 RID: 5284
		string FormatKeyword(string keyword);

		// Token: 0x060014A5 RID: 5285
		void Write(object value);

		// Token: 0x060014A6 RID: 5286
		void Write(string format, params object[] args);

		// Token: 0x060014A7 RID: 5287
		void WriteLine(object value);

		// Token: 0x060014A8 RID: 5288
		void WriteLine(string format, params object[] args);

		// Token: 0x060014A9 RID: 5289
		void WriteLine();
	}
}
