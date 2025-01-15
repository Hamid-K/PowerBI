using System;
using System.Data;
using Microsoft.ReportingServices.DataProcessing;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x0200001C RID: 28
	internal class ParameterMultiValueWrapper : ParameterWrapper, IDataMultiValueParameter, Microsoft.ReportingServices.DataProcessing.IDataParameter
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004F2F File Offset: 0x0000312F
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00004F37 File Offset: 0x00003137
		public virtual object[] Values
		{
			get
			{
				return this.m_values;
			}
			set
			{
				this.m_values = value;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004F40 File Offset: 0x00003140
		public ParameterMultiValueWrapper(global::System.Data.IDataParameter param)
			: base(param)
		{
		}

		// Token: 0x04000087 RID: 135
		private object[] m_values;
	}
}
