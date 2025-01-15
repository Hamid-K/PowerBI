using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000F7 RID: 247
	internal sealed class GetDataSetDefinitionActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0002743A File Offset: 0x0002563A
		// (set) Token: 0x06000A35 RID: 2613 RVA: 0x00027442 File Offset: 0x00025642
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

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0002744B File Offset: 0x0002564B
		// (set) Token: 0x06000A37 RID: 2615 RVA: 0x00027453 File Offset: 0x00025653
		public byte[] DataSetDefinition
		{
			get
			{
				return this.m_dataSetDefinition;
			}
			set
			{
				this.m_dataSetDefinition = value;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0002745C File Offset: 0x0002565C
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x00027464 File Offset: 0x00025664
		public bool InternalUsePermissionForExecution
		{
			get
			{
				return this.m_internalUsePermissionForExecution;
			}
			set
			{
				this.m_internalUsePermissionForExecution = value;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0002746D File Offset: 0x0002566D
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00027475 File Offset: 0x00025675
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
		}

		// Token: 0x04000483 RID: 1155
		private string m_itemPath;

		// Token: 0x04000484 RID: 1156
		private byte[] m_dataSetDefinition;

		// Token: 0x04000485 RID: 1157
		private bool m_internalUsePermissionForExecution;
	}
}
