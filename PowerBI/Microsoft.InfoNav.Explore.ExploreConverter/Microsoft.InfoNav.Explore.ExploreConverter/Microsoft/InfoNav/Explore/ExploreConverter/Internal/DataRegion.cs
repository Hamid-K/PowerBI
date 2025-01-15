using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000093 RID: 147
	internal class DataRegion : ReportItem
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x0000CDAF File Offset: 0x0000AFAF
		internal DataRegion(string rdlTagName, string name, ReportItemRect rect, int zIndex, ReportParsingDiagnosticContext diagnosticContext, string dataSetName)
			: base(rdlTagName, name, rect, zIndex, diagnosticContext)
		{
			this._dataSetName = dataSetName;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000CDC6 File Offset: 0x0000AFC6
		public string DataSetName
		{
			get
			{
				return this._dataSetName;
			}
		}

		// Token: 0x040001EE RID: 494
		private readonly string _dataSetName;
	}
}
