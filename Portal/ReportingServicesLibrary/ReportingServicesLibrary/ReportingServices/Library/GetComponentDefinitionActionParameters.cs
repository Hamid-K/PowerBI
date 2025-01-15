using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000DD RID: 221
	internal sealed class GetComponentDefinitionActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00025CF7 File Offset: 0x00023EF7
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x00025CFF File Offset: 0x00023EFF
		public string ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00025D08 File Offset: 0x00023F08
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x00025D10 File Offset: 0x00023F10
		public byte[] ComponentDefinition
		{
			get
			{
				return this.m_componentDefinition;
			}
			set
			{
				this.m_componentDefinition = value;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00025D19 File Offset: 0x00023F19
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00025D21 File Offset: 0x00023F21
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
		}

		// Token: 0x04000467 RID: 1127
		private string m_itemPath;

		// Token: 0x04000468 RID: 1128
		private byte[] m_componentDefinition;
	}
}
