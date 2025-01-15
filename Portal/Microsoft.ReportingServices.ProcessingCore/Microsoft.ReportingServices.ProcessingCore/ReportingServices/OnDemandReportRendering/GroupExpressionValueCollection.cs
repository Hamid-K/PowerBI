using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200029F RID: 671
	public sealed class GroupExpressionValueCollection
	{
		// Token: 0x060019E3 RID: 6627 RVA: 0x00068F86 File Offset: 0x00067186
		internal GroupExpressionValueCollection()
		{
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00068F8E File Offset: 0x0006718E
		internal void UpdateValues(object exprValue)
		{
			this.m_values = new object[1];
			this.m_values[0] = exprValue;
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00068FA5 File Offset: 0x000671A5
		internal void UpdateValues(object[] exprValues)
		{
			this.m_values = exprValues;
		}

		// Token: 0x17000EDC RID: 3804
		public object this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_values[index];
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x00069003 File Offset: 0x00067203
		public int Count
		{
			get
			{
				if (this.m_values != null)
				{
					return this.m_values.Length;
				}
				return 0;
			}
		}

		// Token: 0x04000CF0 RID: 3312
		private object[] m_values;
	}
}
