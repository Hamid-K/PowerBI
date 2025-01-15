using System;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.FuzzyGroup
{
	// Token: 0x02000B61 RID: 2913
	internal class FuzzyGrouping
	{
		// Token: 0x0600509A RID: 20634 RVA: 0x0010DEA7 File Offset: 0x0010C0A7
		public FuzzyGrouping(Keys resultKeys, Keys keyKeys, int[] keyColumns, ColumnConstructor[] constructors, FuzzyGroupOptions fuzzyGroupOptions)
		{
			this.resultKeys = resultKeys;
			this.keyKeys = keyKeys;
			this.keyColumns = keyColumns;
			this.constructors = constructors;
			this.fuzzyGroupOptions = fuzzyGroupOptions;
		}

		// Token: 0x17001920 RID: 6432
		// (get) Token: 0x0600509B RID: 20635 RVA: 0x0010DED4 File Offset: 0x0010C0D4
		public Keys ResultKeys
		{
			get
			{
				return this.resultKeys;
			}
		}

		// Token: 0x17001921 RID: 6433
		// (get) Token: 0x0600509C RID: 20636 RVA: 0x0010DEDC File Offset: 0x0010C0DC
		public Keys KeyKeys
		{
			get
			{
				return this.keyKeys;
			}
		}

		// Token: 0x17001922 RID: 6434
		// (get) Token: 0x0600509D RID: 20637 RVA: 0x0010DEE4 File Offset: 0x0010C0E4
		public int[] KeyColumns
		{
			get
			{
				return this.keyColumns;
			}
		}

		// Token: 0x17001923 RID: 6435
		// (get) Token: 0x0600509E RID: 20638 RVA: 0x0010DEEC File Offset: 0x0010C0EC
		public ColumnConstructor[] Constructors
		{
			get
			{
				return this.constructors;
			}
		}

		// Token: 0x17001924 RID: 6436
		// (get) Token: 0x0600509F RID: 20639 RVA: 0x0010DEF4 File Offset: 0x0010C0F4
		public FuzzyGroupOptions FuzzyGroupOptions
		{
			get
			{
				return this.fuzzyGroupOptions;
			}
		}

		// Token: 0x04002B3E RID: 11070
		private readonly Keys resultKeys;

		// Token: 0x04002B3F RID: 11071
		private readonly Keys keyKeys;

		// Token: 0x04002B40 RID: 11072
		private readonly int[] keyColumns;

		// Token: 0x04002B41 RID: 11073
		private readonly ColumnConstructor[] constructors;

		// Token: 0x04002B42 RID: 11074
		private readonly FuzzyGroupOptions fuzzyGroupOptions;
	}
}
