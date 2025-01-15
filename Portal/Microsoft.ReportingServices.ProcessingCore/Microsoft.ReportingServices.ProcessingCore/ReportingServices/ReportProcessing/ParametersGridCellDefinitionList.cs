using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000622 RID: 1570
	[Serializable]
	public sealed class ParametersGridCellDefinitionList : ArrayList
	{
		// Token: 0x06005659 RID: 22105 RVA: 0x0016BE86 File Offset: 0x0016A086
		public ParametersGridCellDefinitionList()
		{
		}

		// Token: 0x0600565A RID: 22106 RVA: 0x0016BE8E File Offset: 0x0016A08E
		public ParametersGridCellDefinitionList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001F92 RID: 8082
		public ParameterGridLayoutCellDefinition this[int index]
		{
			get
			{
				return (ParameterGridLayoutCellDefinition)base[index];
			}
		}
	}
}
