using System;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009B4 RID: 2484
	internal sealed class MdxKpi
	{
		// Token: 0x060046EB RID: 18155 RVA: 0x000EDEF8 File Offset: 0x000EC0F8
		public MdxKpi(MdxCube cube, string mdxIdentifier, string caption, string measureGroupName, string displayFolders, MdxMeasure goalMeasure, MdxMeasure statusMeasure, MdxMeasure trendMeasure, MdxMeasure valueMeasure)
		{
			this.cube = cube;
			this.mdxIdentifier = mdxIdentifier;
			this.caption = caption;
			this.measureGroupName = measureGroupName;
			this.displayFolders = displayFolders;
			this.goalMeasure = goalMeasure;
			this.statusMeasure = statusMeasure;
			this.trendMeasure = trendMeasure;
			this.valueMeasure = valueMeasure;
		}

		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x060046EC RID: 18156 RVA: 0x000EDF50 File Offset: 0x000EC150
		public string MdxIdentifier
		{
			get
			{
				return this.mdxIdentifier;
			}
		}

		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x060046ED RID: 18157 RVA: 0x000EDF58 File Offset: 0x000EC158
		public string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x060046EE RID: 18158 RVA: 0x000EDF60 File Offset: 0x000EC160
		public MdxMeasureGroup MeasureGroup
		{
			get
			{
				return this.cube.GetMeasureGroup(this.measureGroupName);
			}
		}

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x060046EF RID: 18159 RVA: 0x000EDF73 File Offset: 0x000EC173
		public string DisplayFolders
		{
			get
			{
				return this.displayFolders;
			}
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x060046F0 RID: 18160 RVA: 0x000EDF7B File Offset: 0x000EC17B
		public MdxMeasure GoalMeasure
		{
			get
			{
				return this.goalMeasure;
			}
		}

		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x060046F1 RID: 18161 RVA: 0x000EDF83 File Offset: 0x000EC183
		public MdxMeasure StatusMeasure
		{
			get
			{
				return this.statusMeasure;
			}
		}

		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x060046F2 RID: 18162 RVA: 0x000EDF8B File Offset: 0x000EC18B
		public MdxMeasure TrendMeasure
		{
			get
			{
				return this.trendMeasure;
			}
		}

		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x060046F3 RID: 18163 RVA: 0x000EDF93 File Offset: 0x000EC193
		public MdxMeasure ValueMeasure
		{
			get
			{
				return this.valueMeasure;
			}
		}

		// Token: 0x040025AF RID: 9647
		private readonly MdxCube cube;

		// Token: 0x040025B0 RID: 9648
		private readonly string mdxIdentifier;

		// Token: 0x040025B1 RID: 9649
		private readonly string caption;

		// Token: 0x040025B2 RID: 9650
		private readonly string measureGroupName;

		// Token: 0x040025B3 RID: 9651
		private readonly string displayFolders;

		// Token: 0x040025B4 RID: 9652
		private readonly MdxMeasure goalMeasure;

		// Token: 0x040025B5 RID: 9653
		private readonly MdxMeasure statusMeasure;

		// Token: 0x040025B6 RID: 9654
		private readonly MdxMeasure trendMeasure;

		// Token: 0x040025B7 RID: 9655
		private readonly MdxMeasure valueMeasure;
	}
}
