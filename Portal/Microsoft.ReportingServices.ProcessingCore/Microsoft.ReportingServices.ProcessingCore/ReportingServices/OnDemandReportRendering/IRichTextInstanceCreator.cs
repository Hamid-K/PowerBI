using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000208 RID: 520
	internal interface IRichTextInstanceCreator
	{
		// Token: 0x060013A2 RID: 5026
		IList<ICompiledParagraphInstance> CreateParagraphInstanceCollection();

		// Token: 0x060013A3 RID: 5027
		ICompiledParagraphInstance CreateParagraphInstance();

		// Token: 0x060013A4 RID: 5028
		ICompiledTextRunInstance CreateTextRunInstance();

		// Token: 0x060013A5 RID: 5029
		IList<ICompiledTextRunInstance> CreateTextRunInstanceCollection();

		// Token: 0x060013A6 RID: 5030
		ICompiledStyleInstance CreateStyleInstance(bool isParagraph);

		// Token: 0x060013A7 RID: 5031
		IActionInstance CreateActionInstance();
	}
}
