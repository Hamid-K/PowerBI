using System;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000097 RID: 151
	[Guid("69A3B7FE-4E35-4238-BAD2-EA107BE0694F")]
	public interface IProcessable
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000740 RID: 1856
		DateTime LastProcessed { get; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000741 RID: 1857
		AnalysisState State { get; }

		// Token: 0x06000742 RID: 1858
		bool CanProcess(ProcessType processType);

		// Token: 0x06000743 RID: 1859
		void Process();

		// Token: 0x06000744 RID: 1860
		void Process(ProcessType processType);

		// Token: 0x06000745 RID: 1861
		void Process(ProcessType processType, ErrorConfiguration errorConfiguration);

		// Token: 0x06000746 RID: 1862
		void Process(ProcessType processType, ErrorConfiguration errorConfiguration, XmlaWarningCollection warnings);

		// Token: 0x06000747 RID: 1863
		void Process(ProcessType processType, ErrorConfiguration errorConfiguration, XmlaWarningCollection warnings, ImpactDetailCollection impactResult);

		// Token: 0x06000748 RID: 1864
		void Process(ProcessType processType, ErrorConfiguration errorConfiguration, XmlaWarningCollection warnings, ImpactDetailCollection impactResult, bool analyzeImpactOnly);
	}
}
