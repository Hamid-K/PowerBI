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
	// Token: 0x020003F9 RID: 1017
	[Serializable]
	internal sealed class GaugeTickMarks : TickMarkStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002AFF RID: 11007 RVA: 0x000C7692 File Offset: 0x000C5892
		internal GaugeTickMarks()
		{
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000C769A File Offset: 0x000C589A
		internal GaugeTickMarks(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		// Token: 0x17001508 RID: 5384
		// (get) Token: 0x06002B01 RID: 11009 RVA: 0x000C76A3 File Offset: 0x000C58A3
		// (set) Token: 0x06002B02 RID: 11010 RVA: 0x000C76AB File Offset: 0x000C58AB
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

		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x06002B03 RID: 11011 RVA: 0x000C76B4 File Offset: 0x000C58B4
		// (set) Token: 0x06002B04 RID: 11012 RVA: 0x000C76BC File Offset: 0x000C58BC
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

		// Token: 0x06002B05 RID: 11013 RVA: 0x000C76C8 File Offset: 0x000C58C8
		internal void Initialize(InitializationContext context, bool isMajor)
		{
			context.ExprHostBuilder.GaugeTickMarksStart(isMajor);
			base.InitializeInternal(context);
			if (this.m_interval != null)
			{
				this.m_interval.Initialize("Interval", context);
				context.ExprHostBuilder.GaugeTickMarksInterval(this.m_interval);
			}
			if (this.m_intervalOffset != null)
			{
				this.m_intervalOffset.Initialize("IntervalOffset", context);
				context.ExprHostBuilder.GaugeTickMarksIntervalOffset(this.m_intervalOffset);
			}
			context.ExprHostBuilder.GaugeTickMarksEnd(isMajor);
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000C774C File Offset: 0x000C594C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			GaugeTickMarks gaugeTickMarks = (GaugeTickMarks)base.PublishClone(context);
			if (this.m_interval != null)
			{
				gaugeTickMarks.m_interval = (ExpressionInfo)this.m_interval.PublishClone(context);
			}
			if (this.m_intervalOffset != null)
			{
				gaugeTickMarks.m_intervalOffset = (ExpressionInfo)this.m_intervalOffset.PublishClone(context);
			}
			return gaugeTickMarks;
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000C77A5 File Offset: 0x000C59A5
		internal void SetExprHost(GaugeTickMarksExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000C77CC File Offset: 0x000C59CC
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeTickMarks, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TickMarkStyle, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Interval, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntervalOffset, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000C781C File Offset: 0x000C5A1C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(GaugeTickMarks.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Interval)
				{
					if (memberName != MemberName.IntervalOffset)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_intervalOffset);
					}
				}
				else
				{
					writer.Write(this.m_interval);
				}
			}
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x000C7890 File Offset: 0x000C5A90
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(GaugeTickMarks.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Interval)
				{
					if (memberName != MemberName.IntervalOffset)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_intervalOffset = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_interval = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x000C790D File Offset: 0x000C5B0D
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeTickMarks;
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000C7914 File Offset: 0x000C5B14
		internal double EvaluateInterval(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeTickMarksIntervalExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000C793A File Offset: 0x000C5B3A
		internal double EvaluateIntervalOffset(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeTickMarksIntervalOffsetExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x0400177B RID: 6011
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = GaugeTickMarks.GetDeclaration();

		// Token: 0x0400177C RID: 6012
		private ExpressionInfo m_interval;

		// Token: 0x0400177D RID: 6013
		private ExpressionInfo m_intervalOffset;
	}
}
