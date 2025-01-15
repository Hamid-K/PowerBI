using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000645 RID: 1605
	[Serializable]
	public class OnDemandDocumentMapNode
	{
		// Token: 0x06005781 RID: 22401 RVA: 0x0016FB05 File Offset: 0x0016DD05
		internal OnDemandDocumentMapNode(string aLabel, string aId, int aLevel)
		{
			this.m_label = aLabel;
			this.m_id = aId;
			this.m_level = aLevel;
		}

		// Token: 0x17002002 RID: 8194
		// (get) Token: 0x06005782 RID: 22402 RVA: 0x0016FB29 File Offset: 0x0016DD29
		public string Label
		{
			get
			{
				return this.m_label;
			}
		}

		// Token: 0x17002003 RID: 8195
		// (get) Token: 0x06005783 RID: 22403 RVA: 0x0016FB31 File Offset: 0x0016DD31
		public string Id
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17002004 RID: 8196
		// (get) Token: 0x06005784 RID: 22404 RVA: 0x0016FB39 File Offset: 0x0016DD39
		// (set) Token: 0x06005785 RID: 22405 RVA: 0x0016FB41 File Offset: 0x0016DD41
		public int Level
		{
			get
			{
				return this.m_level;
			}
			internal set
			{
				this.m_level = value;
			}
		}

		// Token: 0x04002E55 RID: 11861
		private string m_label;

		// Token: 0x04002E56 RID: 11862
		private string m_id;

		// Token: 0x04002E57 RID: 11863
		private int m_level = -1;
	}
}
