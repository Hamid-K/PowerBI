using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000C67 RID: 3175
	[NullableContext(1)]
	public interface IBoundedList<out TCell> where TCell : class, IWordAmalgamation<TCell>
	{
		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x060051D0 RID: 20944
		Axis Axis { get; }

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x060051D1 RID: 20945
		IReadOnlyList<TCell> Cells { get; }
	}
}
