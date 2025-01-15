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
	// Token: 0x020003F3 RID: 1011
	[Serializable]
	internal sealed class LinearScale : GaugeScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002A5F RID: 10847 RVA: 0x000C532A File Offset: 0x000C352A
		internal LinearScale()
		{
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x000C5332 File Offset: 0x000C3532
		internal LinearScale(GaugePanel gaugePanel, int id)
			: base(gaugePanel, id)
		{
		}

		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x06002A61 RID: 10849 RVA: 0x000C533C File Offset: 0x000C353C
		// (set) Token: 0x06002A62 RID: 10850 RVA: 0x000C5344 File Offset: 0x000C3544
		internal List<LinearPointer> GaugePointers
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

		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x06002A63 RID: 10851 RVA: 0x000C534D File Offset: 0x000C354D
		// (set) Token: 0x06002A64 RID: 10852 RVA: 0x000C5355 File Offset: 0x000C3555
		internal ExpressionInfo StartMargin
		{
			get
			{
				return this.m_startMargin;
			}
			set
			{
				this.m_startMargin = value;
			}
		}

		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x06002A65 RID: 10853 RVA: 0x000C535E File Offset: 0x000C355E
		// (set) Token: 0x06002A66 RID: 10854 RVA: 0x000C5366 File Offset: 0x000C3566
		internal ExpressionInfo EndMargin
		{
			get
			{
				return this.m_endMargin;
			}
			set
			{
				this.m_endMargin = value;
			}
		}

		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x06002A67 RID: 10855 RVA: 0x000C536F File Offset: 0x000C356F
		// (set) Token: 0x06002A68 RID: 10856 RVA: 0x000C5377 File Offset: 0x000C3577
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

		// Token: 0x06002A69 RID: 10857 RVA: 0x000C5380 File Offset: 0x000C3580
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.LinearScaleStart(this.m_name);
			base.Initialize(context);
			if (this.m_gaugePointers != null)
			{
				for (int i = 0; i < this.m_gaugePointers.Count; i++)
				{
					this.m_gaugePointers[i].Initialize(context);
				}
			}
			if (this.m_startMargin != null)
			{
				this.m_startMargin.Initialize("StartMargin", context);
				context.ExprHostBuilder.LinearScaleStartMargin(this.m_startMargin);
			}
			if (this.m_endMargin != null)
			{
				this.m_endMargin.Initialize("EndMargin", context);
				context.ExprHostBuilder.LinearScaleEndMargin(this.m_endMargin);
			}
			if (this.m_position != null)
			{
				this.m_position.Initialize("Position", context);
				context.ExprHostBuilder.LinearScalePosition(this.m_position);
			}
			this.m_exprHostID = context.ExprHostBuilder.LinearScaleEnd();
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000C546C File Offset: 0x000C366C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			LinearScale linearScale = (LinearScale)base.PublishClone(context);
			if (this.m_gaugePointers != null)
			{
				linearScale.m_gaugePointers = new List<LinearPointer>(this.m_gaugePointers.Count);
				foreach (LinearPointer linearPointer in this.m_gaugePointers)
				{
					linearScale.m_gaugePointers.Add((LinearPointer)linearPointer.PublishClone(context));
				}
			}
			if (this.m_startMargin != null)
			{
				linearScale.m_startMargin = (ExpressionInfo)this.m_startMargin.PublishClone(context);
			}
			if (this.m_endMargin != null)
			{
				linearScale.m_endMargin = (ExpressionInfo)this.m_endMargin.PublishClone(context);
			}
			if (this.m_position != null)
			{
				linearScale.m_position = (ExpressionInfo)this.m_position.PublishClone(context);
			}
			return linearScale;
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000C5558 File Offset: 0x000C3758
		internal void SetExprHost(LinearScaleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			IList<LinearPointerExprHost> linearPointersHostsRemotable = ((LinearScaleExprHost)this.m_exprHost).LinearPointersHostsRemotable;
			if (this.m_gaugePointers != null && linearPointersHostsRemotable != null)
			{
				for (int i = 0; i < this.m_gaugePointers.Count; i++)
				{
					LinearPointer linearPointer = this.m_gaugePointers[i];
					if (linearPointer != null && linearPointer.ExpressionHostID > -1)
					{
						linearPointer.SetExprHost(linearPointersHostsRemotable[linearPointer.ExpressionHostID], reportObjectModel);
					}
				}
			}
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000C55E8 File Offset: 0x000C37E8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LinearScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeScale, new List<MemberInfo>
			{
				new MemberInfo(MemberName.GaugePointers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LinearPointer),
				new MemberInfo(MemberName.StartMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.EndMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Position, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000C5660 File Offset: 0x000C3860
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(LinearScale.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.StartMargin)
				{
					if (memberName == MemberName.Position)
					{
						writer.Write(this.m_position);
						continue;
					}
					if (memberName == MemberName.StartMargin)
					{
						writer.Write(this.m_startMargin);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.EndMargin)
					{
						writer.Write(this.m_endMargin);
						continue;
					}
					if (memberName == MemberName.GaugePointers)
					{
						writer.Write<LinearPointer>(this.m_gaugePointers);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000C570C File Offset: 0x000C390C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(LinearScale.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.StartMargin)
				{
					if (memberName == MemberName.Position)
					{
						this.m_position = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.StartMargin)
					{
						this.m_startMargin = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.EndMargin)
					{
						this.m_endMargin = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.GaugePointers)
					{
						this.m_gaugePointers = reader.ReadGenericListOfRIFObjects<LinearPointer>();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000C57CA File Offset: 0x000C39CA
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LinearScale;
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000C57D1 File Offset: 0x000C39D1
		internal double EvaluateStartMargin(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateLinearScaleStartMarginExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000C57F7 File Offset: 0x000C39F7
		internal double EvaluateEndMargin(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateLinearScaleEndMarginExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000C581D File Offset: 0x000C3A1D
		internal double EvaluatePosition(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateLinearScalePositionExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x04001752 RID: 5970
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = LinearScale.GetDeclaration();

		// Token: 0x04001753 RID: 5971
		private List<LinearPointer> m_gaugePointers;

		// Token: 0x04001754 RID: 5972
		private ExpressionInfo m_startMargin;

		// Token: 0x04001755 RID: 5973
		private ExpressionInfo m_endMargin;

		// Token: 0x04001756 RID: 5974
		private ExpressionInfo m_position;
	}
}
