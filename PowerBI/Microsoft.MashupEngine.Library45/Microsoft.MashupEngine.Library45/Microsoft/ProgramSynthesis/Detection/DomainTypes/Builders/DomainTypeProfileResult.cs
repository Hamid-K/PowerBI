using System;

namespace Microsoft.ProgramSynthesis.Detection.DomainTypes.Builders
{
	// Token: 0x02000AFD RID: 2813
	public class DomainTypeProfileResult
	{
		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06004678 RID: 18040 RVA: 0x000DC710 File Offset: 0x000DA910
		// (set) Token: 0x06004679 RID: 18041 RVA: 0x000DC718 File Offset: 0x000DA918
		public IColumnInfo TargetColumn { get; private set; }

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x0600467A RID: 18042 RVA: 0x000DC721 File Offset: 0x000DA921
		// (set) Token: 0x0600467B RID: 18043 RVA: 0x000DC729 File Offset: 0x000DA929
		public string DominantDomainTypeName { get; private set; }

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x0600467C RID: 18044 RVA: 0x000DC732 File Offset: 0x000DA932
		// (set) Token: 0x0600467D RID: 18045 RVA: 0x000DC73A File Offset: 0x000DA93A
		public int DominantDomainTypeCount { get; private set; }

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x0600467E RID: 18046 RVA: 0x000DC743 File Offset: 0x000DA943
		// (set) Token: 0x0600467F RID: 18047 RVA: 0x000DC74B File Offset: 0x000DA94B
		public int NullStringCount { get; private set; }

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06004680 RID: 18048 RVA: 0x000DC754 File Offset: 0x000DA954
		// (set) Token: 0x06004681 RID: 18049 RVA: 0x000DC75C File Offset: 0x000DA95C
		public int EmptyStringCount { get; private set; }

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06004682 RID: 18050 RVA: 0x000DC765 File Offset: 0x000DA965
		// (set) Token: 0x06004683 RID: 18051 RVA: 0x000DC76D File Offset: 0x000DA96D
		public int SampleCount { get; private set; }

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06004684 RID: 18052 RVA: 0x000DC776 File Offset: 0x000DA976
		public string ColumnName
		{
			get
			{
				return this.TargetColumn.ColumnName;
			}
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x000DC783 File Offset: 0x000DA983
		public DomainTypeProfileResult(IColumnInfo targetColumn, string dominantDomainTypeName, int dominantDomainTypeCount, int nullStringCount, int emptyStringCount, int sampleCount)
		{
			this.TargetColumn = targetColumn;
			this.DominantDomainTypeName = dominantDomainTypeName;
			this.DominantDomainTypeCount = dominantDomainTypeCount;
			this.NullStringCount = nullStringCount;
			this.EmptyStringCount = emptyStringCount;
			this.SampleCount = sampleCount;
		}
	}
}
