using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002A6 RID: 678
	internal sealed class DataSet
	{
		// Token: 0x06001A17 RID: 6679 RVA: 0x00069A9A File Offset: 0x00067C9A
		internal DataSet(DataSet dataSetDef, RenderingContext renderingContext)
		{
			this.m_dataSetDef = dataSetDef;
			this.m_renderingContext = renderingContext;
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x00069AB0 File Offset: 0x00067CB0
		public string Name
		{
			get
			{
				return this.m_dataSetDef.Name;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x00069ABD File Offset: 0x00067CBD
		public DataSetInstance Instance
		{
			get
			{
				if (this.m_renderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new DataSetInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x00069AE8 File Offset: 0x00067CE8
		public FieldCollection NonCalculatedFields
		{
			get
			{
				if (this.m_fields == null)
				{
					this.m_fields = new FieldCollection(this.m_dataSetDef);
				}
				return this.m_fields;
			}
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00069B09 File Offset: 0x00067D09
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00069B1E File Offset: 0x00067D1E
		internal DataSet DataSetDef
		{
			get
			{
				return this.m_dataSetDef;
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x00069B26 File Offset: 0x00067D26
		internal RenderingContext RenderingContext
		{
			get
			{
				return this.m_renderingContext;
			}
		}

		// Token: 0x04000D00 RID: 3328
		private FieldCollection m_fields;

		// Token: 0x04000D01 RID: 3329
		private DataSetInstance m_instance;

		// Token: 0x04000D02 RID: 3330
		private DataSet m_dataSetDef;

		// Token: 0x04000D03 RID: 3331
		private RenderingContext m_renderingContext;
	}
}
