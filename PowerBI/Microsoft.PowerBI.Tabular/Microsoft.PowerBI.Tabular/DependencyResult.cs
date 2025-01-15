using System;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000F8 RID: 248
	internal sealed class DependencyResult
	{
		// Token: 0x06001038 RID: 4152 RVA: 0x00078486 File Offset: 0x00076686
		internal DependencyResult(object dependent, DependencyType type, string description)
		{
			if (dependent == null)
			{
				throw new ArgumentNullException("dependent");
			}
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			this.dependent = dependent;
			this.type = type;
			this.description = description;
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x000784BF File Offset: 0x000766BF
		public object Dependent
		{
			get
			{
				return this.dependent;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x000784C7 File Offset: 0x000766C7
		public DependencyType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x000784CF File Offset: 0x000766CF
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x04000244 RID: 580
		private object dependent;

		// Token: 0x04000245 RID: 581
		private DependencyType type;

		// Token: 0x04000246 RID: 582
		private string description;
	}
}
