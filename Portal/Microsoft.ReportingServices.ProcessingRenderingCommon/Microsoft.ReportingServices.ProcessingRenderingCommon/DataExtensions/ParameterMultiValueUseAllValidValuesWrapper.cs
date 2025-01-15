using System;
using System.Data;
using Microsoft.ReportingServices.DataProcessing;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x0200001B RID: 27
	internal sealed class ParameterMultiValueUseAllValidValuesWrapper : ParameterMultiValueWrapper, IDataUseAllValidValuesParameter, IDataMultiValueParameter, Microsoft.ReportingServices.DataProcessing.IDataParameter
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00004F15 File Offset: 0x00003115
		public ParameterMultiValueUseAllValidValuesWrapper(global::System.Data.IDataParameter param)
			: base(param)
		{
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004F1E File Offset: 0x0000311E
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00004F26 File Offset: 0x00003126
		public bool UseAllValidValues
		{
			get
			{
				return this.m_useAllValidValues;
			}
			set
			{
				this.m_useAllValidValues = value;
			}
		}

		// Token: 0x04000086 RID: 134
		private bool m_useAllValidValues;
	}
}
