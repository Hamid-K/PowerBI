using System;
using System.Collections.ObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200021D RID: 541
	public sealed class LabelData
	{
		// Token: 0x0600146D RID: 5229 RVA: 0x00053A20 File Offset: 0x00051C20
		internal LabelData(LabelData labelData)
		{
			this.m_labelData = labelData;
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x00053A2F File Offset: 0x00051C2F
		public string DataSetName
		{
			get
			{
				return this.m_labelData.DataSetName;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x00053A3C File Offset: 0x00051C3C
		[Obsolete("Use KeyFields instead.")]
		public string Key
		{
			get
			{
				return this.KeyFields[0];
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00053A4A File Offset: 0x00051C4A
		public ReadOnlyCollection<string> KeyFields
		{
			get
			{
				if (this.m_keyFields == null)
				{
					this.m_keyFields = new ReadOnlyCollection<string>(this.m_labelData.KeyFields);
				}
				return this.m_keyFields;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x00053A70 File Offset: 0x00051C70
		public string Label
		{
			get
			{
				return this.m_labelData.Label;
			}
		}

		// Token: 0x040009A8 RID: 2472
		private readonly LabelData m_labelData;

		// Token: 0x040009A9 RID: 2473
		private ReadOnlyCollection<string> m_keyFields;
	}
}
