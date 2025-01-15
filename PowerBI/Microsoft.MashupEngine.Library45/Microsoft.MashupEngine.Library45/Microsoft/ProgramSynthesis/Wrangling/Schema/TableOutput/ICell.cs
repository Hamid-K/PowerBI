using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput
{
	// Token: 0x02000138 RID: 312
	public interface ICell<TRegion> where TRegion : IRegion<TRegion>
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060006F2 RID: 1778
		TRegion CellValue { get; }
	}
}
