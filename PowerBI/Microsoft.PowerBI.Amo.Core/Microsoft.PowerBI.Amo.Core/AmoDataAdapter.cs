using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200006C RID: 108
	public class AmoDataAdapter : DataAdapter
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x000222A1 File Offset: 0x000204A1
		public AmoDataAdapter(AmoDataReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000222B0 File Offset: 0x000204B0
		public override int Fill(DataSet dataSet)
		{
			int num = base.Fill(dataSet, "Table", this.reader, 0, int.MaxValue);
			this.AdjustTableNames(dataSet);
			return num;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000222D4 File Offset: 0x000204D4
		private void AdjustTableNames(DataSet dataSet)
		{
			if (this.reader.XmlaDataReader.RowsetNames.Count == dataSet.Tables.Count)
			{
				for (int i = 0; i < dataSet.Tables.Count; i++)
				{
					if (!string.IsNullOrEmpty(this.reader.XmlaDataReader.RowsetNames[i]))
					{
						dataSet.Tables[i].TableName = this.reader.XmlaDataReader.RowsetNames[i];
					}
				}
			}
		}

		// Token: 0x04000409 RID: 1033
		private AmoDataReader reader;
	}
}
