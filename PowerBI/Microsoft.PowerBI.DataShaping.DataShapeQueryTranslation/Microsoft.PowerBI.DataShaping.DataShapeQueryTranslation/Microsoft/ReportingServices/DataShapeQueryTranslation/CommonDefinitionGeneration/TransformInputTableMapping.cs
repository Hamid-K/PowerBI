using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDefinitionGeneration
{
	// Token: 0x02000121 RID: 289
	internal sealed class TransformInputTableMapping
	{
		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002AFA4 File Offset: 0x000291A4
		internal TransformInputTableMapping(DataTransformTable table, Dictionary<Identifier, string> columnMappings)
		{
			this.m_table = table;
			this.m_columnMappings = columnMappings;
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0002AFBA File Offset: 0x000291BA
		internal DataTransformTable Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002AFC2 File Offset: 0x000291C2
		internal bool TryGetDataFieldForColumn(Identifier columnId, out string dataField)
		{
			return this.m_columnMappings.TryGetValue(columnId, out dataField);
		}

		// Token: 0x04000591 RID: 1425
		private readonly DataTransformTable m_table;

		// Token: 0x04000592 RID: 1426
		private readonly Dictionary<Identifier, string> m_columnMappings;
	}
}
