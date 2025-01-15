using System;
using System.Data;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Data
{
	// Token: 0x02000057 RID: 87
	[Serializable]
	internal class NullDataRecord : DataRecordImplBase
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x000154D8 File Offset: 0x000136D8
		public NullDataRecord(DataTable schemaTable)
		{
			base.Schema = schemaTable;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000154E7 File Offset: 0x000136E7
		public override DataTable GetSchemaTable()
		{
			return base.Schema;
		}

		// Token: 0x17000073 RID: 115
		public override object this[int i]
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}
	}
}
