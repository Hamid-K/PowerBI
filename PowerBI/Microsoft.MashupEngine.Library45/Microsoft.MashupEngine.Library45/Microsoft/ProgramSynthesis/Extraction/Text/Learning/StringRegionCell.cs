using System;
using System.Diagnostics;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning
{
	// Token: 0x02000F57 RID: 3927
	[DebuggerDisplay("{StringValue}, {Start}, {End}")]
	internal class StringRegionCell : Cell<StringRegion>
	{
		// Token: 0x06006D4F RID: 27983 RVA: 0x00164752 File Offset: 0x00162952
		public StringRegionCell(StringRegion value, bool isUserSpecified = true)
			: base(value, isUserSpecified)
		{
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06006D50 RID: 27984 RVA: 0x0016475C File Offset: 0x0016295C
		public int Length
		{
			get
			{
				if (!(base.Value == null))
				{
					return (int)base.Value.Length;
				}
				return 0;
			}
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06006D51 RID: 27985 RVA: 0x00164779 File Offset: 0x00162979
		public uint Start
		{
			get
			{
				StringRegion value = base.Value;
				if (value == null)
				{
					return 0U;
				}
				return value.Start;
			}
		}

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06006D52 RID: 27986 RVA: 0x0016478C File Offset: 0x0016298C
		public uint End
		{
			get
			{
				StringRegion value = base.Value;
				if (value == null)
				{
					return 0U;
				}
				return value.End;
			}
		}

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x06006D53 RID: 27987 RVA: 0x0016479F File Offset: 0x0016299F
		public string StringValue
		{
			get
			{
				StringRegion value = base.Value;
				if (value == null)
				{
					return null;
				}
				return value.Value;
			}
		}
	}
}
