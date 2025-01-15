using System;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x02000791 RID: 1937
	internal sealed class ParameterImpl : Parameter
	{
		// Token: 0x06006C1E RID: 27678 RVA: 0x001B6CBB File Offset: 0x001B4EBB
		internal ParameterImpl(object[] value, string[] labels, bool isMultiValue)
		{
			this.m_value = value;
			this.m_labels = labels;
			this.m_isMultiValue = isMultiValue;
		}

		// Token: 0x17002599 RID: 9625
		// (get) Token: 0x06006C1F RID: 27679 RVA: 0x001B6CD8 File Offset: 0x001B4ED8
		public override object Value
		{
			get
			{
				if (this.m_value == null)
				{
					return null;
				}
				if (!this.m_isMultiValue)
				{
					return this.m_value[0];
				}
				return this.m_value;
			}
		}

		// Token: 0x1700259A RID: 9626
		// (get) Token: 0x06006C20 RID: 27680 RVA: 0x001B6CFB File Offset: 0x001B4EFB
		public override object Label
		{
			get
			{
				if (this.m_labels == null || this.m_labels.Length == 0)
				{
					return null;
				}
				if (!this.m_isMultiValue)
				{
					return this.m_labels[0];
				}
				return this.m_labels;
			}
		}

		// Token: 0x1700259B RID: 9627
		// (get) Token: 0x06006C21 RID: 27681 RVA: 0x001B6D27 File Offset: 0x001B4F27
		public override int Count
		{
			get
			{
				if (this.m_value == null)
				{
					return 0;
				}
				return this.m_value.Length;
			}
		}

		// Token: 0x1700259C RID: 9628
		// (get) Token: 0x06006C22 RID: 27682 RVA: 0x001B6D3B File Offset: 0x001B4F3B
		public override bool IsMultiValue
		{
			get
			{
				return this.m_isMultiValue;
			}
		}

		// Token: 0x0400364B RID: 13899
		private object[] m_value;

		// Token: 0x0400364C RID: 13900
		private string[] m_labels;

		// Token: 0x0400364D RID: 13901
		private bool m_isMultiValue;
	}
}
