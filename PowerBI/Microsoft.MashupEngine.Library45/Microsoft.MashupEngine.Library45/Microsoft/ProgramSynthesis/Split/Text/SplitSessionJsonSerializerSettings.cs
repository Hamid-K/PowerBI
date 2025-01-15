using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Session;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x02001316 RID: 4886
	public class SplitSessionJsonSerializerSettings : NonInteractiveSessionJsonSerializerSettings<SplitProgram, StringRegion, SplitCell[]>
	{
		// Token: 0x17001934 RID: 6452
		// (get) Token: 0x060092F3 RID: 37619 RVA: 0x001EE902 File Offset: 0x001ECB02
		protected override IEnumerable<Type> SessionTypes
		{
			get
			{
				return base.SessionTypes.Concat(new Type[] { typeof(SplitSession) });
			}
		}

		// Token: 0x17001935 RID: 6453
		// (get) Token: 0x060092F4 RID: 37620 RVA: 0x001EE924 File Offset: 0x001ECB24
		protected override IEnumerable<Type> ConstraintTypes
		{
			get
			{
				return base.ConstraintTypes.Concat(new Type[]
				{
					typeof(IncludeDelimitersInOutput),
					typeof(DelimiterStringsConstraint),
					typeof(FillStrategyConstraint),
					typeof(FixedWidthConstraint),
					typeof(NthExampleConstraint),
					typeof(SimpleDelimitersOrFixedWidth),
					typeof(SimpleDelimiter)
				});
			}
		}
	}
}
