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
	// Token: 0x02000491 RID: 1169
	[Serializable]
	internal sealed class ChartStripLine : ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IActionOwner
	{
		// Token: 0x06003791 RID: 14225 RVA: 0x000F2497 File Offset: 0x000F0697
		internal ChartStripLine()
		{
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x000F249F File Offset: 0x000F069F
		internal ChartStripLine(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, int id)
			: base(chart)
		{
			this.m_id = id;
		}

		// Token: 0x17001854 RID: 6228
		// (get) Token: 0x06003793 RID: 14227 RVA: 0x000F24AF File Offset: 0x000F06AF
		internal ChartStripLineExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001855 RID: 6229
		// (get) Token: 0x06003794 RID: 14228 RVA: 0x000F24B7 File Offset: 0x000F06B7
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x17001856 RID: 6230
		// (get) Token: 0x06003795 RID: 14229 RVA: 0x000F24BF File Offset: 0x000F06BF
		internal int ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17001857 RID: 6231
		// (get) Token: 0x06003796 RID: 14230 RVA: 0x000F24C7 File Offset: 0x000F06C7
		// (set) Token: 0x06003797 RID: 14231 RVA: 0x000F24CF File Offset: 0x000F06CF
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

		// Token: 0x17001858 RID: 6232
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x000F24D8 File Offset: 0x000F06D8
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x17001859 RID: 6233
		// (get) Token: 0x06003799 RID: 14233 RVA: 0x000F24E0 File Offset: 0x000F06E0
		// (set) Token: 0x0600379A RID: 14234 RVA: 0x000F24E8 File Offset: 0x000F06E8
		List<string> IActionOwner.FieldsUsedInValueExpression
		{
			get
			{
				return this.m_fieldsUsedInValueExpression;
			}
			set
			{
				this.m_fieldsUsedInValueExpression = value;
			}
		}

		// Token: 0x1700185A RID: 6234
		// (get) Token: 0x0600379B RID: 14235 RVA: 0x000F24F1 File Offset: 0x000F06F1
		// (set) Token: 0x0600379C RID: 14236 RVA: 0x000F24F9 File Offset: 0x000F06F9
		internal ExpressionInfo Title
		{
			get
			{
				return this.m_title;
			}
			set
			{
				this.m_title = value;
			}
		}

		// Token: 0x1700185B RID: 6235
		// (get) Token: 0x0600379D RID: 14237 RVA: 0x000F2502 File Offset: 0x000F0702
		// (set) Token: 0x0600379E RID: 14238 RVA: 0x000F250A File Offset: 0x000F070A
		internal ExpressionInfo TitleAngle
		{
			get
			{
				return this.m_titleAngle;
			}
			set
			{
				this.m_titleAngle = value;
			}
		}

		// Token: 0x1700185C RID: 6236
		// (get) Token: 0x0600379F RID: 14239 RVA: 0x000F2513 File Offset: 0x000F0713
		// (set) Token: 0x060037A0 RID: 14240 RVA: 0x000F251B File Offset: 0x000F071B
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

		// Token: 0x1700185D RID: 6237
		// (get) Token: 0x060037A1 RID: 14241 RVA: 0x000F2524 File Offset: 0x000F0724
		// (set) Token: 0x060037A2 RID: 14242 RVA: 0x000F252C File Offset: 0x000F072C
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

		// Token: 0x1700185E RID: 6238
		// (get) Token: 0x060037A3 RID: 14243 RVA: 0x000F2535 File Offset: 0x000F0735
		// (set) Token: 0x060037A4 RID: 14244 RVA: 0x000F253D File Offset: 0x000F073D
		internal ExpressionInfo Interval
		{
			get
			{
				return this.m_interval;
			}
			set
			{
				this.m_interval = value;
			}
		}

		// Token: 0x1700185F RID: 6239
		// (get) Token: 0x060037A5 RID: 14245 RVA: 0x000F2546 File Offset: 0x000F0746
		// (set) Token: 0x060037A6 RID: 14246 RVA: 0x000F254E File Offset: 0x000F074E
		internal ExpressionInfo IntervalType
		{
			get
			{
				return this.m_intervalType;
			}
			set
			{
				this.m_intervalType = value;
			}
		}

		// Token: 0x17001860 RID: 6240
		// (get) Token: 0x060037A7 RID: 14247 RVA: 0x000F2557 File Offset: 0x000F0757
		// (set) Token: 0x060037A8 RID: 14248 RVA: 0x000F255F File Offset: 0x000F075F
		internal ExpressionInfo IntervalOffset
		{
			get
			{
				return this.m_intervalOffset;
			}
			set
			{
				this.m_intervalOffset = value;
			}
		}

		// Token: 0x17001861 RID: 6241
		// (get) Token: 0x060037A9 RID: 14249 RVA: 0x000F2568 File Offset: 0x000F0768
		// (set) Token: 0x060037AA RID: 14250 RVA: 0x000F2570 File Offset: 0x000F0770
		internal ExpressionInfo IntervalOffsetType
		{
			get
			{
				return this.m_intervalOffsetType;
			}
			set
			{
				this.m_intervalOffsetType = value;
			}
		}

		// Token: 0x17001862 RID: 6242
		// (get) Token: 0x060037AB RID: 14251 RVA: 0x000F2579 File Offset: 0x000F0779
		// (set) Token: 0x060037AC RID: 14252 RVA: 0x000F2581 File Offset: 0x000F0781
		internal ExpressionInfo StripWidth
		{
			get
			{
				return this.m_stripWidth;
			}
			set
			{
				this.m_stripWidth = value;
			}
		}

		// Token: 0x17001863 RID: 6243
		// (get) Token: 0x060037AD RID: 14253 RVA: 0x000F258A File Offset: 0x000F078A
		// (set) Token: 0x060037AE RID: 14254 RVA: 0x000F2592 File Offset: 0x000F0792
		internal ExpressionInfo StripWidthType
		{
			get
			{
				return this.m_stripWidthType;
			}
			set
			{
				this.m_stripWidthType = value;
			}
		}

		// Token: 0x060037AF RID: 14255 RVA: 0x000F259C File Offset: 0x000F079C
		internal void SetExprHost(ChartStripLineExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_action != null && this.m_exprHost.ActionInfoHost != null)
			{
				this.m_action.SetExprHost(exprHost.ActionInfoHost, reportObjectModel);
			}
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000F25F8 File Offset: 0x000F07F8
		internal void Initialize(InitializationContext context, int index)
		{
			context.ExprHostBuilder.ChartStripLineStart(index);
			base.Initialize(context);
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_title != null)
			{
				this.m_title.Initialize("Title", context);
				context.ExprHostBuilder.ChartStripLineTitle(this.m_title);
			}
			if (this.m_titleAngle != null)
			{
				this.m_titleAngle.Initialize("TitleAngle", context);
				context.ExprHostBuilder.ChartStripLineTitleAngle(this.m_titleAngle);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.ChartStripLineToolTip(this.m_toolTip);
			}
			if (this.m_interval != null)
			{
				this.m_interval.Initialize("Interval", context);
				context.ExprHostBuilder.ChartStripLineInterval(this.m_interval);
			}
			if (this.m_intervalType != null)
			{
				this.m_intervalType.Initialize("IntervalType", context);
				context.ExprHostBuilder.ChartStripLineIntervalType(this.m_intervalType);
			}
			if (this.m_intervalOffset != null)
			{
				this.m_intervalOffset.Initialize("IntervalOffset", context);
				context.ExprHostBuilder.ChartStripLineIntervalOffset(this.m_intervalOffset);
			}
			if (this.m_intervalOffsetType != null)
			{
				this.m_intervalOffsetType.Initialize("IntervalOffsetType", context);
				context.ExprHostBuilder.ChartStripLineIntervalOffsetType(this.m_intervalOffsetType);
			}
			if (this.m_stripWidth != null)
			{
				this.m_stripWidth.Initialize("StripWidth", context);
				context.ExprHostBuilder.ChartStripLineStripWidth(this.m_stripWidth);
			}
			if (this.m_stripWidthType != null)
			{
				this.m_stripWidthType.Initialize("StripWidthType", context);
				context.ExprHostBuilder.ChartStripLineStripWidthType(this.m_stripWidthType);
			}
			if (this.m_textOrientation != null)
			{
				this.m_textOrientation.Initialize("TextOrientation", context);
				context.ExprHostBuilder.ChartStripLineTextOrientation(this.m_textOrientation);
			}
			this.m_exprHostID = context.ExprHostBuilder.ChartStripLineEnd();
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000F27F0 File Offset: 0x000F09F0
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartStripLine chartStripLine = (ChartStripLine)base.PublishClone(context);
			if (this.m_action != null)
			{
				chartStripLine.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_title != null)
			{
				chartStripLine.m_title = (ExpressionInfo)this.m_title.PublishClone(context);
			}
			if (this.m_titleAngle != null)
			{
				chartStripLine.m_titleAngle = (ExpressionInfo)this.m_titleAngle.PublishClone(context);
			}
			if (this.m_toolTip != null)
			{
				chartStripLine.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			if (this.m_interval != null)
			{
				chartStripLine.m_interval = (ExpressionInfo)this.m_interval.PublishClone(context);
			}
			if (this.m_intervalType != null)
			{
				chartStripLine.m_intervalType = (ExpressionInfo)this.m_intervalType.PublishClone(context);
			}
			if (this.m_intervalOffset != null)
			{
				chartStripLine.m_intervalOffset = (ExpressionInfo)this.m_intervalOffset.PublishClone(context);
			}
			if (this.m_intervalOffsetType != null)
			{
				chartStripLine.m_intervalOffsetType = (ExpressionInfo)this.m_intervalOffsetType.PublishClone(context);
			}
			if (this.m_stripWidth != null)
			{
				chartStripLine.m_stripWidth = (ExpressionInfo)this.m_stripWidth.PublishClone(context);
			}
			if (this.m_stripWidthType != null)
			{
				chartStripLine.m_stripWidthType = (ExpressionInfo)this.m_stripWidthType.PublishClone(context);
			}
			if (this.m_textOrientation != null)
			{
				chartStripLine.m_textOrientation = (ExpressionInfo)this.m_textOrientation.PublishClone(context);
			}
			return chartStripLine;
		}

		// Token: 0x060037B2 RID: 14258 RVA: 0x000F2960 File Offset: 0x000F0B60
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStripLine, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.Title, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TitleAngle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Interval, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntervalType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntervalOffset, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntervalOffsetType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.StripWidth, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.StripWidthType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.TextOrientation, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ID, Token.Int32)
			});
		}

		// Token: 0x060037B3 RID: 14259 RVA: 0x000F2A90 File Offset: 0x000F0C90
		internal string EvaluateTitle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartStripLineTitleExpression(this, this.m_chart.Name);
		}

		// Token: 0x060037B4 RID: 14260 RVA: 0x000F2AB6 File Offset: 0x000F0CB6
		internal int EvaluateTitleAngle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartStripLineTitleAngleExpression(this, this.m_chart.Name);
		}

		// Token: 0x060037B5 RID: 14261 RVA: 0x000F2ADC File Offset: 0x000F0CDC
		internal TextOrientations EvaluateTextOrientation(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateTextOrientations(context.ReportRuntime.EvaluateChartStripLineTextOrientationExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x060037B6 RID: 14262 RVA: 0x000F2B0D File Offset: 0x000F0D0D
		internal string EvaluateToolTip(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartStripLineToolTipExpression(this, this.m_chart.Name);
		}

		// Token: 0x060037B7 RID: 14263 RVA: 0x000F2B33 File Offset: 0x000F0D33
		internal double EvaluateInterval(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartStripLineIntervalExpression(this, this.m_chart.Name);
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x000F2B59 File Offset: 0x000F0D59
		internal ChartIntervalType EvaluateIntervalType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartIntervalType(context.ReportRuntime.EvaluateChartStripLineIntervalTypeExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x000F2B8A File Offset: 0x000F0D8A
		internal double EvaluateIntervalOffset(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartStripLineIntervalOffsetExpression(this, this.m_chart.Name);
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x000F2BB0 File Offset: 0x000F0DB0
		internal ChartIntervalType EvaluateIntervalOffsetType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartIntervalType(context.ReportRuntime.EvaluateChartStripLineIntervalOffsetTypeExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x000F2BE1 File Offset: 0x000F0DE1
		internal double EvaluateStripWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartStripLineStripWidthExpression(this, this.m_chart.Name);
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x000F2C07 File Offset: 0x000F0E07
		internal ChartIntervalType EvaluateStripWidthType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartIntervalType(context.ReportRuntime.EvaluateChartStripLineStripWidthTypeExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000F2C38 File Offset: 0x000F0E38
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartStripLine.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ToolTip)
				{
					if (memberName == MemberName.ID)
					{
						writer.Write(this.m_id);
						continue;
					}
					switch (memberName)
					{
					case MemberName.Title:
						writer.Write(this.m_title);
						continue;
					case MemberName.TitleAngle:
						writer.Write(this.m_titleAngle);
						continue;
					case MemberName.StripWidth:
						writer.Write(this.m_stripWidth);
						continue;
					case MemberName.StripWidthType:
						writer.Write(this.m_stripWidthType);
						continue;
					default:
						if (memberName == MemberName.ToolTip)
						{
							writer.Write(this.m_toolTip);
							continue;
						}
						break;
					}
				}
				else if (memberName <= MemberName.ExprHostID)
				{
					switch (memberName)
					{
					case MemberName.Interval:
						writer.Write(this.m_interval);
						continue;
					case MemberName.IntervalType:
						writer.Write(this.m_intervalType);
						continue;
					case MemberName.IntervalOffset:
						writer.Write(this.m_intervalOffset);
						continue;
					case MemberName.IntervalOffsetType:
						writer.Write(this.m_intervalOffsetType);
						continue;
					default:
						if (memberName == MemberName.ExprHostID)
						{
							writer.Write(this.m_exprHostID);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.Action)
					{
						writer.Write(this.m_action);
						continue;
					}
					if (memberName == MemberName.TextOrientation)
					{
						writer.Write(this.m_textOrientation);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x000F2DD8 File Offset: 0x000F0FD8
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartStripLine.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ToolTip)
				{
					if (memberName == MemberName.ID)
					{
						this.m_id = reader.ReadInt32();
						continue;
					}
					switch (memberName)
					{
					case MemberName.Title:
						this.m_title = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.TitleAngle:
						this.m_titleAngle = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.StripWidth:
						this.m_stripWidth = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.StripWidthType:
						this.m_stripWidthType = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.ToolTip)
						{
							this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						break;
					}
				}
				else if (memberName <= MemberName.ExprHostID)
				{
					switch (memberName)
					{
					case MemberName.Interval:
						this.m_interval = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.IntervalType:
						this.m_intervalType = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.IntervalOffset:
						this.m_intervalOffset = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.IntervalOffsetType:
						this.m_intervalOffsetType = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.ExprHostID)
						{
							this.m_exprHostID = reader.ReadInt32();
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.Action)
					{
						this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.TextOrientation)
					{
						this.m_textOrientation = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x000F2FB0 File Offset: 0x000F11B0
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
			if (this.m_id == 0)
			{
				this.m_id = this.m_chart.GenerateActionOwnerID();
			}
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x000F2FD3 File Offset: 0x000F11D3
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStripLine;
		}

		// Token: 0x04001AF2 RID: 6898
		private int m_exprHostID;

		// Token: 0x04001AF3 RID: 6899
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001AF4 RID: 6900
		private ExpressionInfo m_title;

		// Token: 0x04001AF5 RID: 6901
		private ExpressionInfo m_titleAngle;

		// Token: 0x04001AF6 RID: 6902
		private ExpressionInfo m_textOrientation;

		// Token: 0x04001AF7 RID: 6903
		private ExpressionInfo m_toolTip;

		// Token: 0x04001AF8 RID: 6904
		private ExpressionInfo m_interval;

		// Token: 0x04001AF9 RID: 6905
		private ExpressionInfo m_intervalType;

		// Token: 0x04001AFA RID: 6906
		private ExpressionInfo m_intervalOffset;

		// Token: 0x04001AFB RID: 6907
		private ExpressionInfo m_intervalOffsetType;

		// Token: 0x04001AFC RID: 6908
		private ExpressionInfo m_stripWidth;

		// Token: 0x04001AFD RID: 6909
		private ExpressionInfo m_stripWidthType;

		// Token: 0x04001AFE RID: 6910
		private int m_id;

		// Token: 0x04001AFF RID: 6911
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartStripLine.GetDeclaration();

		// Token: 0x04001B00 RID: 6912
		[NonSerialized]
		private ChartStripLineExprHost m_exprHost;

		// Token: 0x04001B01 RID: 6913
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;
	}
}
