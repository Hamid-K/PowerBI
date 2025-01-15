using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000483 RID: 1155
	[Serializable]
	internal class ChartTitle : ChartTitleBase, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IActionOwner
	{
		// Token: 0x060035AD RID: 13741 RVA: 0x000EB1DA File Offset: 0x000E93DA
		internal ChartTitle()
		{
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x000EB1E2 File Offset: 0x000E93E2
		internal ChartTitle(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
			: base(chart)
		{
			this.m_chart = chart;
		}

		// Token: 0x170017C8 RID: 6088
		// (get) Token: 0x060035AF RID: 13743 RVA: 0x000EB1F2 File Offset: 0x000E93F2
		// (set) Token: 0x060035B0 RID: 13744 RVA: 0x000EB1FA File Offset: 0x000E93FA
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Action Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x170017C9 RID: 6089
		// (get) Token: 0x060035B1 RID: 13745 RVA: 0x000EB203 File Offset: 0x000E9403
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x170017CA RID: 6090
		// (get) Token: 0x060035B2 RID: 13746 RVA: 0x000EB20B File Offset: 0x000E940B
		// (set) Token: 0x060035B3 RID: 13747 RVA: 0x000EB20E File Offset: 0x000E940E
		List<string> IActionOwner.FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170017CB RID: 6091
		// (get) Token: 0x060035B4 RID: 13748 RVA: 0x000EB210 File Offset: 0x000E9410
		// (set) Token: 0x060035B5 RID: 13749 RVA: 0x000EB218 File Offset: 0x000E9418
		internal string TitleName
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x060035B6 RID: 13750 RVA: 0x000EB221 File Offset: 0x000E9421
		// (set) Token: 0x060035B7 RID: 13751 RVA: 0x000EB229 File Offset: 0x000E9429
		internal ExpressionInfo Position
		{
			get
			{
				return this.m_position;
			}
			set
			{
				this.m_position = value;
			}
		}

		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x060035B8 RID: 13752 RVA: 0x000EB232 File Offset: 0x000E9432
		// (set) Token: 0x060035B9 RID: 13753 RVA: 0x000EB23A File Offset: 0x000E943A
		internal ExpressionInfo Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		// Token: 0x170017CE RID: 6094
		// (get) Token: 0x060035BA RID: 13754 RVA: 0x000EB243 File Offset: 0x000E9443
		// (set) Token: 0x060035BB RID: 13755 RVA: 0x000EB24B File Offset: 0x000E944B
		internal ExpressionInfo Docking
		{
			get
			{
				return this.m_docking;
			}
			set
			{
				this.m_docking = value;
			}
		}

		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x060035BC RID: 13756 RVA: 0x000EB254 File Offset: 0x000E9454
		// (set) Token: 0x060035BD RID: 13757 RVA: 0x000EB25C File Offset: 0x000E945C
		internal string DockToChartArea
		{
			get
			{
				return this.m_dockToChartArea;
			}
			set
			{
				this.m_dockToChartArea = value;
			}
		}

		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x060035BE RID: 13758 RVA: 0x000EB265 File Offset: 0x000E9465
		// (set) Token: 0x060035BF RID: 13759 RVA: 0x000EB26D File Offset: 0x000E946D
		internal ExpressionInfo DockOutsideChartArea
		{
			get
			{
				return this.m_dockOutsideChartArea;
			}
			set
			{
				this.m_dockOutsideChartArea = value;
			}
		}

		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x060035C0 RID: 13760 RVA: 0x000EB276 File Offset: 0x000E9476
		// (set) Token: 0x060035C1 RID: 13761 RVA: 0x000EB27E File Offset: 0x000E947E
		internal ExpressionInfo DockOffset
		{
			get
			{
				return this.m_dockOffset;
			}
			set
			{
				this.m_dockOffset = value;
			}
		}

		// Token: 0x170017D2 RID: 6098
		// (get) Token: 0x060035C2 RID: 13762 RVA: 0x000EB287 File Offset: 0x000E9487
		// (set) Token: 0x060035C3 RID: 13763 RVA: 0x000EB28F File Offset: 0x000E948F
		internal ExpressionInfo ToolTip
		{
			get
			{
				return this.m_toolTip;
			}
			set
			{
				this.m_toolTip = value;
			}
		}

		// Token: 0x170017D3 RID: 6099
		// (get) Token: 0x060035C4 RID: 13764 RVA: 0x000EB298 File Offset: 0x000E9498
		// (set) Token: 0x060035C5 RID: 13765 RVA: 0x000EB2A0 File Offset: 0x000E94A0
		internal ExpressionInfo TextOrientation
		{
			get
			{
				return this.m_textOrientation;
			}
			set
			{
				this.m_textOrientation = value;
			}
		}

		// Token: 0x170017D4 RID: 6100
		// (get) Token: 0x060035C6 RID: 13766 RVA: 0x000EB2A9 File Offset: 0x000E94A9
		// (set) Token: 0x060035C7 RID: 13767 RVA: 0x000EB2B1 File Offset: 0x000E94B1
		internal ChartElementPosition ChartElementPosition
		{
			get
			{
				return this.m_chartElementPosition;
			}
			set
			{
				this.m_chartElementPosition = value;
			}
		}

		// Token: 0x170017D5 RID: 6101
		// (get) Token: 0x060035C8 RID: 13768 RVA: 0x000EB2BA File Offset: 0x000E94BA
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000EB2C2 File Offset: 0x000E94C2
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartTitleStart(this.m_name);
			this.InitializeInternal(context);
			this.m_exprHostID = context.ExprHostBuilder.ChartTitleEnd();
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000EB2F0 File Offset: 0x000E94F0
		protected void InitializeInternal(InitializationContext context)
		{
			base.Initialize(context);
			if (this.m_position != null)
			{
				this.m_position.Initialize("Position", context);
				context.ExprHostBuilder.ChartTitlePosition(this.m_position);
			}
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.ChartTitleHidden(this.m_hidden);
			}
			if (this.m_docking != null)
			{
				this.m_docking.Initialize("Docking", context);
				context.ExprHostBuilder.ChartTitleDocking(this.m_docking);
			}
			string dockToChartArea = this.m_dockToChartArea;
			if (this.m_dockOutsideChartArea != null)
			{
				this.m_dockOutsideChartArea.Initialize("DockOutsideChartArea", context);
				context.ExprHostBuilder.ChartTitleDockOutsideChartArea(this.m_dockOutsideChartArea);
			}
			if (this.m_dockOffset != null)
			{
				this.m_dockOffset.Initialize("DockOffset", context);
				context.ExprHostBuilder.ChartTitleDockOffset(this.m_dockOffset);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.ChartTitleToolTip(this.m_toolTip);
			}
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_textOrientation != null)
			{
				this.m_textOrientation.Initialize("TextOrientation", context);
				context.ExprHostBuilder.ChartTitleTextOrientation(this.m_textOrientation);
			}
			if (this.m_chartElementPosition != null)
			{
				this.m_chartElementPosition.Initialize(context);
			}
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000EB460 File Offset: 0x000E9660
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle chartTitle = (Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle)base.PublishClone(context);
			if (this.m_position != null)
			{
				chartTitle.m_position = (ExpressionInfo)this.m_position.PublishClone(context);
			}
			if (this.m_hidden != null)
			{
				chartTitle.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
			}
			if (this.m_docking != null)
			{
				chartTitle.m_docking = (ExpressionInfo)this.m_docking.PublishClone(context);
			}
			if (this.m_dockToChartArea != null)
			{
				chartTitle.m_dockToChartArea = (string)this.m_dockToChartArea.Clone();
			}
			if (this.m_dockOutsideChartArea != null)
			{
				chartTitle.m_dockOutsideChartArea = (ExpressionInfo)this.m_dockOutsideChartArea.PublishClone(context);
			}
			if (this.m_dockOffset != null)
			{
				chartTitle.m_dockOffset = (ExpressionInfo)this.m_dockOffset.PublishClone(context);
			}
			if (this.m_toolTip != null)
			{
				chartTitle.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			if (this.m_action != null)
			{
				chartTitle.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_textOrientation != null)
			{
				chartTitle.m_textOrientation = (ExpressionInfo)this.m_textOrientation.PublishClone(context);
			}
			if (this.m_chartElementPosition != null)
			{
				chartTitle.m_chartElementPosition = (ChartElementPosition)this.m_chartElementPosition.PublishClone(context);
			}
			return chartTitle;
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000EB5B0 File Offset: 0x000E97B0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartTitle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartTitleBase, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Position, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Docking, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DockToChartArea, Token.String),
				new MemberInfo(MemberName.DockOutsideChartArea, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DockOffset, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.TextOrientation, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ChartElementPosition, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartElementPosition)
			});
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000EB6CC File Offset: 0x000E98CC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Hidden)
				{
					if (memberName <= MemberName.Position)
					{
						if (memberName == MemberName.Name)
						{
							writer.Write(this.m_name);
							continue;
						}
						if (memberName == MemberName.Position)
						{
							writer.Write(this.m_position);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ToolTip)
						{
							writer.Write(this.m_toolTip);
							continue;
						}
						if (memberName == MemberName.Hidden)
						{
							writer.Write(this.m_hidden);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.Action)
				{
					if (memberName == MemberName.ExprHostID)
					{
						writer.Write(this.m_exprHostID);
						continue;
					}
					if (memberName == MemberName.Action)
					{
						writer.Write(this.m_action);
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.Docking:
						writer.Write(this.m_docking);
						continue;
					case MemberName.DockToChartArea:
						writer.Write(this.m_dockToChartArea);
						continue;
					case MemberName.DockOutsideChartArea:
						writer.Write(this.m_dockOutsideChartArea);
						continue;
					case MemberName.DockOffset:
						writer.Write(this.m_dockOffset);
						continue;
					default:
						if (memberName == MemberName.TextOrientation)
						{
							writer.Write(this.m_textOrientation);
							continue;
						}
						if (memberName == MemberName.ChartElementPosition)
						{
							writer.Write(this.m_chartElementPosition);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000EB870 File Offset: 0x000E9A70
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Hidden)
				{
					if (memberName <= MemberName.Position)
					{
						if (memberName == MemberName.Name)
						{
							this.m_name = reader.ReadString();
							continue;
						}
						if (memberName == MemberName.Position)
						{
							this.m_position = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ToolTip)
						{
							this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.Hidden)
						{
							this.m_hidden = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.Action)
				{
					if (memberName == MemberName.ExprHostID)
					{
						this.m_exprHostID = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.Action)
					{
						this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.Docking:
						this.m_docking = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.DockToChartArea:
						this.m_dockToChartArea = reader.ReadString();
						continue;
					case MemberName.DockOutsideChartArea:
						this.m_dockOutsideChartArea = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.DockOffset:
						this.m_dockOffset = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.TextOrientation)
						{
							this.m_textOrientation = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.ChartElementPosition)
						{
							this.m_chartElementPosition = (ChartElementPosition)reader.ReadRIFObject();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000EBA46 File Offset: 0x000E9C46
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x000EBA50 File Offset: 0x000E9C50
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartTitle;
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000EBA58 File Offset: 0x000E9C58
		internal override void SetExprHost(StyleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_action != null && ((ChartTitleExprHost)exprHost).ActionInfoHost != null)
			{
				this.m_action.SetExprHost(((ChartTitleExprHost)exprHost).ActionInfoHost, reportObjectModel);
			}
			if (this.m_chartElementPosition != null && ((ChartTitleExprHost)exprHost).ChartElementPositionHost != null)
			{
				this.m_chartElementPosition.SetExprHost(((ChartTitleExprHost)exprHost).ChartElementPositionHost, reportObjectModel);
			}
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x000EBADE File Offset: 0x000E9CDE
		internal bool EvaluateHidden(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateEvaluateChartTitleHiddenExpression(this, base.Name, "Hidden");
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000EBB04 File Offset: 0x000E9D04
		internal ChartTitleDockings EvaluateDocking(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return EnumTranslator.TranslateChartTitleDocking(context.ReportRuntime.EvaluateChartTitleDockingExpression(this, base.Name, "Docking"), context.ReportRuntime);
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000EBB35 File Offset: 0x000E9D35
		internal ChartTitlePositions EvaluatePosition(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return EnumTranslator.TranslateChartTitlePosition(context.ReportRuntime.EvaluateChartTitlePositionExpression(this, base.Name, "Position"), context.ReportRuntime);
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000EBB66 File Offset: 0x000E9D66
		internal bool EvaluateDockOutsideChartArea(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartTitleDockOutsideChartAreaExpression(this, base.Name, "DockOutsideChartArea");
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x000EBB8C File Offset: 0x000E9D8C
		internal int EvaluateDockOffset(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartTitleDockOffsetExpression(this, base.Name, "DockOffset");
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000EBBB2 File Offset: 0x000E9DB2
		internal string EvaluateToolTip(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartTitleToolTipExpression(this, base.Name, "ToolTip");
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000EBBD8 File Offset: 0x000E9DD8
		internal TextOrientations EvaluateTextOrientation(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateTextOrientations(context.ReportRuntime.EvaluateChartTitleTextOrientationExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x04001A5B RID: 6747
		private string m_name;

		// Token: 0x04001A5C RID: 6748
		private ExpressionInfo m_position;

		// Token: 0x04001A5D RID: 6749
		protected int m_exprHostID;

		// Token: 0x04001A5E RID: 6750
		private ExpressionInfo m_hidden;

		// Token: 0x04001A5F RID: 6751
		private ExpressionInfo m_docking;

		// Token: 0x04001A60 RID: 6752
		private string m_dockToChartArea;

		// Token: 0x04001A61 RID: 6753
		private ExpressionInfo m_dockOutsideChartArea;

		// Token: 0x04001A62 RID: 6754
		private ExpressionInfo m_dockOffset;

		// Token: 0x04001A63 RID: 6755
		private ExpressionInfo m_toolTip;

		// Token: 0x04001A64 RID: 6756
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001A65 RID: 6757
		private ExpressionInfo m_textOrientation;

		// Token: 0x04001A66 RID: 6758
		private ChartElementPosition m_chartElementPosition;

		// Token: 0x04001A67 RID: 6759
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle.GetDeclaration();
	}
}
