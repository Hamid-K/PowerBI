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
	// Token: 0x020003F4 RID: 1012
	[Serializable]
	internal sealed class RadialScale : GaugeScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002A74 RID: 10868 RVA: 0x000C584F File Offset: 0x000C3A4F
		internal RadialScale()
		{
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000C5857 File Offset: 0x000C3A57
		internal RadialScale(GaugePanel gaugePanel, int id)
			: base(gaugePanel, id)
		{
		}

		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x06002A76 RID: 10870 RVA: 0x000C5861 File Offset: 0x000C3A61
		// (set) Token: 0x06002A77 RID: 10871 RVA: 0x000C5869 File Offset: 0x000C3A69
		internal List<RadialPointer> GaugePointers
		{
			get
			{
				return this.m_gaugePointers;
			}
			set
			{
				this.m_gaugePointers = value;
			}
		}

		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x06002A78 RID: 10872 RVA: 0x000C5872 File Offset: 0x000C3A72
		// (set) Token: 0x06002A79 RID: 10873 RVA: 0x000C587A File Offset: 0x000C3A7A
		internal ExpressionInfo Radius
		{
			get
			{
				return this.m_radius;
			}
			set
			{
				this.m_radius = value;
			}
		}

		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x06002A7A RID: 10874 RVA: 0x000C5883 File Offset: 0x000C3A83
		// (set) Token: 0x06002A7B RID: 10875 RVA: 0x000C588B File Offset: 0x000C3A8B
		internal ExpressionInfo StartAngle
		{
			get
			{
				return this.m_startAngle;
			}
			set
			{
				this.m_startAngle = value;
			}
		}

		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x06002A7C RID: 10876 RVA: 0x000C5894 File Offset: 0x000C3A94
		// (set) Token: 0x06002A7D RID: 10877 RVA: 0x000C589C File Offset: 0x000C3A9C
		internal ExpressionInfo SweepAngle
		{
			get
			{
				return this.m_sweepAngle;
			}
			set
			{
				this.m_sweepAngle = value;
			}
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000C58A8 File Offset: 0x000C3AA8
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.RadialScaleStart(this.m_name);
			base.Initialize(context);
			if (this.m_gaugePointers != null)
			{
				for (int i = 0; i < this.m_gaugePointers.Count; i++)
				{
					this.m_gaugePointers[i].Initialize(context);
				}
			}
			if (this.m_radius != null)
			{
				this.m_radius.Initialize("Radius", context);
				context.ExprHostBuilder.RadialScaleRadius(this.m_radius);
			}
			if (this.m_startAngle != null)
			{
				this.m_startAngle.Initialize("StartAngle", context);
				context.ExprHostBuilder.RadialScaleStartAngle(this.m_startAngle);
			}
			if (this.m_sweepAngle != null)
			{
				this.m_sweepAngle.Initialize("SweepAngle", context);
				context.ExprHostBuilder.RadialScaleSweepAngle(this.m_sweepAngle);
			}
			this.m_exprHostID = context.ExprHostBuilder.RadialScaleEnd();
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000C5994 File Offset: 0x000C3B94
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			RadialScale radialScale = (RadialScale)base.PublishClone(context);
			if (this.m_gaugePointers != null)
			{
				radialScale.m_gaugePointers = new List<RadialPointer>(this.m_gaugePointers.Count);
				foreach (RadialPointer radialPointer in this.m_gaugePointers)
				{
					radialScale.m_gaugePointers.Add((RadialPointer)radialPointer.PublishClone(context));
				}
			}
			if (this.m_radius != null)
			{
				radialScale.m_radius = (ExpressionInfo)this.m_radius.PublishClone(context);
			}
			if (this.m_startAngle != null)
			{
				radialScale.m_startAngle = (ExpressionInfo)this.m_startAngle.PublishClone(context);
			}
			if (this.m_sweepAngle != null)
			{
				radialScale.m_sweepAngle = (ExpressionInfo)this.m_sweepAngle.PublishClone(context);
			}
			return radialScale;
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000C5A80 File Offset: 0x000C3C80
		internal void SetExprHost(RadialScaleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			IList<RadialPointerExprHost> radialPointersHostsRemotable = ((RadialScaleExprHost)this.m_exprHost).RadialPointersHostsRemotable;
			if (this.m_gaugePointers != null && radialPointersHostsRemotable != null)
			{
				for (int i = 0; i < this.m_gaugePointers.Count; i++)
				{
					RadialPointer radialPointer = this.m_gaugePointers[i];
					if (radialPointer != null && radialPointer.ExpressionHostID > -1)
					{
						radialPointer.SetExprHost(radialPointersHostsRemotable[radialPointer.ExpressionHostID], reportObjectModel);
					}
				}
			}
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x000C5B10 File Offset: 0x000C3D10
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RadialScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeScale, new List<MemberInfo>
			{
				new MemberInfo(MemberName.GaugePointers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RadialPointer),
				new MemberInfo(MemberName.Radius, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.StartAngle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.SweepAngle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000C5B88 File Offset: 0x000C3D88
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RadialScale.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.GaugePointers:
					writer.Write<RadialPointer>(this.m_gaugePointers);
					break;
				case MemberName.Radius:
					writer.Write(this.m_radius);
					break;
				case MemberName.StartAngle:
					writer.Write(this.m_startAngle);
					break;
				case MemberName.SweepAngle:
					writer.Write(this.m_sweepAngle);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x000C5C28 File Offset: 0x000C3E28
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RadialScale.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.GaugePointers:
					this.m_gaugePointers = reader.ReadGenericListOfRIFObjects<RadialPointer>();
					break;
				case MemberName.Radius:
					this.m_radius = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.StartAngle:
					this.m_startAngle = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.SweepAngle:
					this.m_sweepAngle = (ExpressionInfo)reader.ReadRIFObject();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x000C5CDA File Offset: 0x000C3EDA
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RadialScale;
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x000C5CE1 File Offset: 0x000C3EE1
		internal double EvaluateRadius(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateRadialScaleRadiusExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x000C5D07 File Offset: 0x000C3F07
		internal double EvaluateStartAngle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateRadialScaleStartAngleExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x000C5D2D File Offset: 0x000C3F2D
		internal double EvaluateSweepAngle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateRadialScaleSweepAngleExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x04001757 RID: 5975
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = RadialScale.GetDeclaration();

		// Token: 0x04001758 RID: 5976
		private List<RadialPointer> m_gaugePointers;

		// Token: 0x04001759 RID: 5977
		private ExpressionInfo m_radius;

		// Token: 0x0400175A RID: 5978
		private ExpressionInfo m_startAngle;

		// Token: 0x0400175B RID: 5979
		private ExpressionInfo m_sweepAngle;
	}
}
