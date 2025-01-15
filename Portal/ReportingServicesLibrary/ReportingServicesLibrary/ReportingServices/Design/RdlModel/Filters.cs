using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003C1 RID: 961
	[Editor("FiltersDialog", typeof(UITypeEditor))]
	[SRDescription("Filters")]
	[Category("Data")]
	[Browsable(true)]
	public sealed class Filters : List<Filter>
	{
	}
}
