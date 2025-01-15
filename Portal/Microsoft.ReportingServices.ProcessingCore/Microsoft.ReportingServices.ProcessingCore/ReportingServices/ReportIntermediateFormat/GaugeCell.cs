using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003EA RID: 1002
	[Serializable]
	internal sealed class GaugeCell : Cell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002943 RID: 10563 RVA: 0x000C1125 File Offset: 0x000BF325
		internal GaugeCell()
		{
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x000C112D File Offset: 0x000BF32D
		internal GaugeCell(int id, GaugePanel gaugePanel)
			: base(id, gaugePanel)
		{
		}

		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x000C1137 File Offset: 0x000BF337
		protected override bool IsDataRegionBodyCell
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x000C113A File Offset: 0x000BF33A
		public Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugeCell;
			}
		}

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x000C113E File Offset: 0x000BF33E
		public override Microsoft.ReportingServices.ReportProcessing.ObjectType DataScopeObjectType
		{
			get
			{
				return this.ObjectType;
			}
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x000C1148 File Offset: 0x000BF348
		internal override void InternalInitialize(int parentRowID, int parentColumnID, int rowindex, int colIndex, InitializationContext context)
		{
			Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder exprHostBuilder = context.ExprHostBuilder;
			List<GaugeInputValue> gaugeInputValues = this.GetGaugeInputValues();
			if (gaugeInputValues != null)
			{
				for (int i = 0; i < gaugeInputValues.Count; i++)
				{
					gaugeInputValues[i].Initialize(context, i);
				}
			}
		}

		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x06002949 RID: 10569 RVA: 0x000C1187 File Offset: 0x000BF387
		protected override Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode ExprHostDataRegionMode
		{
			get
			{
				return Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.GaugePanel;
			}
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000C118C File Offset: 0x000BF38C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeCell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Cell, list);
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000C11B0 File Offset: 0x000BF3B0
		internal void SetExprHost(GaugeCellExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			IList<GaugeInputValueExprHost> gaugeInputValueHostsRemotable = this.m_exprHost.GaugeInputValueHostsRemotable;
			List<GaugeInputValue> gaugeInputValues = this.GetGaugeInputValues();
			if (gaugeInputValues != null && gaugeInputValueHostsRemotable != null)
			{
				for (int i = 0; i < gaugeInputValues.Count; i++)
				{
					GaugeInputValue gaugeInputValue = gaugeInputValues[i];
					if (gaugeInputValue != null && gaugeInputValue.ExpressionHostID > -1)
					{
						gaugeInputValue.SetExprHost(gaugeInputValueHostsRemotable[gaugeInputValue.ExpressionHostID], reportObjectModel);
					}
				}
			}
			base.BaseSetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x000C123D File Offset: 0x000BF43D
		private List<GaugeInputValue> GetGaugeInputValues()
		{
			if (this.m_gaugeInputValues == null)
			{
				this.m_gaugeInputValues = ((GaugePanel)base.DataRegionDef).GetGaugeInputValues();
			}
			return this.m_gaugeInputValues;
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x000C1263 File Offset: 0x000BF463
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeCell;
		}

		// Token: 0x040016F7 RID: 5879
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = GaugeCell.GetDeclaration();

		// Token: 0x040016F8 RID: 5880
		[NonSerialized]
		private List<GaugeInputValue> m_gaugeInputValues;

		// Token: 0x040016F9 RID: 5881
		[NonSerialized]
		private GaugeCellExprHost m_exprHost;
	}
}
