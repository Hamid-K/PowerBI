using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000029 RID: 41
	internal abstract class DataRegion : ReportItem
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x0000D716 File Offset: 0x0000B916
		internal DataRegion(int intUniqueName, ReportItem reportItemDef, ReportItemInstance reportItemInstance, RenderingContext renderingContext)
			: base(null, intUniqueName, reportItemDef, reportItemInstance, renderingContext)
		{
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000D724 File Offset: 0x0000B924
		public virtual bool PageBreakAtEnd
		{
			get
			{
				return ((DataRegion)base.ReportItemDef).PageBreakAtEnd;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000D736 File Offset: 0x0000B936
		public virtual bool PageBreakAtStart
		{
			get
			{
				return ((DataRegion)base.ReportItemDef).PageBreakAtStart;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000D748 File Offset: 0x0000B948
		public virtual bool KeepTogether
		{
			get
			{
				return ((DataRegion)base.ReportItemDef).KeepTogether;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000D75A File Offset: 0x0000B95A
		public virtual bool NoRows
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0000D760 File Offset: 0x0000B960
		public string NoRowMessage
		{
			get
			{
				ExpressionInfo noRows = ((DataRegion)base.ReportItemDef).NoRows;
				if (noRows == null)
				{
					return null;
				}
				if (ExpressionInfo.Types.Constant == noRows.Type)
				{
					return noRows.Value;
				}
				return this.InstanceInfoNoRowMessage;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000471 RID: 1137
		internal abstract string InstanceInfoNoRowMessage { get; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000D799 File Offset: 0x0000B999
		public string DataSetName
		{
			get
			{
				return ((DataRegion)base.ReportItemDef).DataSetName;
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		public int[] GetRepeatSiblings()
		{
			DataRegion dataRegion = (DataRegion)base.ReportItemDef;
			if (dataRegion.RepeatSiblings == null)
			{
				return new int[0];
			}
			int[] array = new int[dataRegion.RepeatSiblings.Count];
			dataRegion.RepeatSiblings.CopyTo(array);
			return array;
		}
	}
}
