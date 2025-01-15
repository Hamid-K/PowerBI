using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000074 RID: 116
	internal sealed class QueryDesignQueryDefinition
	{
		// Token: 0x06000247 RID: 583 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		internal QueryDesignQueryDefinition(Dictionary<string, QueryDesignGroup> groups, Dictionary<string, QueryDesignMeasure> measures)
		{
			this._groups = groups;
			this._measures = measures;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000BACA File Offset: 0x00009CCA
		public Dictionary<string, QueryDesignGroup> Groups
		{
			get
			{
				return this._groups;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000BAD2 File Offset: 0x00009CD2
		public Dictionary<string, QueryDesignMeasure> Measures
		{
			get
			{
				return this._measures;
			}
		}

		// Token: 0x0400018A RID: 394
		private readonly Dictionary<string, QueryDesignGroup> _groups;

		// Token: 0x0400018B RID: 395
		private readonly Dictionary<string, QueryDesignMeasure> _measures;
	}
}
