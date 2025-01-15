using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000453 RID: 1107
	[Serializable]
	internal sealed class MapCustomView : MapView, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003297 RID: 12951 RVA: 0x000E1B69 File Offset: 0x000DFD69
		internal MapCustomView()
		{
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x000E1B71 File Offset: 0x000DFD71
		internal MapCustomView(Map map)
			: base(map)
		{
		}

		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x000E1B7A File Offset: 0x000DFD7A
		// (set) Token: 0x0600329A RID: 12954 RVA: 0x000E1B82 File Offset: 0x000DFD82
		internal ExpressionInfo CenterX
		{
			get
			{
				return this.m_centerX;
			}
			set
			{
				this.m_centerX = value;
			}
		}

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x000E1B8B File Offset: 0x000DFD8B
		// (set) Token: 0x0600329C RID: 12956 RVA: 0x000E1B93 File Offset: 0x000DFD93
		internal ExpressionInfo CenterY
		{
			get
			{
				return this.m_centerY;
			}
			set
			{
				this.m_centerY = value;
			}
		}

		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x0600329D RID: 12957 RVA: 0x000E1B9C File Offset: 0x000DFD9C
		internal new MapCustomViewExprHost ExprHost
		{
			get
			{
				return (MapCustomViewExprHost)this.m_exprHost;
			}
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x000E1BAC File Offset: 0x000DFDAC
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapCustomViewStart();
			base.Initialize(context);
			if (this.m_centerX != null)
			{
				this.m_centerX.Initialize("CenterX", context);
				context.ExprHostBuilder.MapCustomViewCenterX(this.m_centerX);
			}
			if (this.m_centerY != null)
			{
				this.m_centerY.Initialize("CenterY", context);
				context.ExprHostBuilder.MapCustomViewCenterY(this.m_centerY);
			}
			context.ExprHostBuilder.MapCustomViewEnd();
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x000E1C30 File Offset: 0x000DFE30
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapCustomView mapCustomView = (MapCustomView)base.PublishClone(context);
			if (this.m_centerX != null)
			{
				mapCustomView.m_centerX = (ExpressionInfo)this.m_centerX.PublishClone(context);
			}
			if (this.m_centerY != null)
			{
				mapCustomView.m_centerY = (ExpressionInfo)this.m_centerY.PublishClone(context);
			}
			return mapCustomView;
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x000E1C8C File Offset: 0x000DFE8C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapCustomView, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapView, new List<MemberInfo>
			{
				new MemberInfo(MemberName.CenterX, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.CenterY, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x000E1CDC File Offset: 0x000DFEDC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapCustomView.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.CenterX)
				{
					if (memberName != MemberName.CenterY)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_centerY);
					}
				}
				else
				{
					writer.Write(this.m_centerX);
				}
			}
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x000E1D50 File Offset: 0x000DFF50
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapCustomView.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.CenterX)
				{
					if (memberName != MemberName.CenterY)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_centerY = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_centerX = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x000E1DCD File Offset: 0x000DFFCD
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapCustomView;
		}

		// Token: 0x060032A4 RID: 12964 RVA: 0x000E1DD4 File Offset: 0x000DFFD4
		internal double EvaluateCenterX(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapCustomViewCenterXExpression(this, this.m_map.Name);
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x000E1DFA File Offset: 0x000DFFFA
		internal double EvaluateCenterY(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapCustomViewCenterYExpression(this, this.m_map.Name);
		}

		// Token: 0x04001987 RID: 6535
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapCustomView.GetDeclaration();

		// Token: 0x04001988 RID: 6536
		private ExpressionInfo m_centerX;

		// Token: 0x04001989 RID: 6537
		private ExpressionInfo m_centerY;
	}
}
