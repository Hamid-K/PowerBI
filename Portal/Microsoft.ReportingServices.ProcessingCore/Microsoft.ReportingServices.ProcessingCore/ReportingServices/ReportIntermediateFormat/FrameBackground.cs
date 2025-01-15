using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003D6 RID: 982
	[Serializable]
	internal sealed class FrameBackground : GaugePanelStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060027B4 RID: 10164 RVA: 0x000BB34B File Offset: 0x000B954B
		internal FrameBackground()
		{
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000BB353 File Offset: 0x000B9553
		internal FrameBackground(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x060027B6 RID: 10166 RVA: 0x000BB35C File Offset: 0x000B955C
		internal string OwnerName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x000BB369 File Offset: 0x000B9569
		internal FrameBackgroundExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000BB371 File Offset: 0x000B9571
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.FrameBackgroundStart();
			base.Initialize(context);
			context.ExprHostBuilder.FrameBackgroundEnd();
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x000BB392 File Offset: 0x000B9592
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			return (FrameBackground)base.PublishClone(context);
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x000BB3A0 File Offset: 0x000B95A0
		internal void SetExprHost(FrameBackgroundExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x000BB3C8 File Offset: 0x000B95C8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FrameBackground, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelStyleContainer, list);
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x000BB3EB File Offset: 0x000B95EB
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(FrameBackground.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x000BB423 File Offset: 0x000B9623
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(FrameBackground.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x000BB45B File Offset: 0x000B965B
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FrameBackground;
		}

		// Token: 0x04001696 RID: 5782
		[NonSerialized]
		private FrameBackgroundExprHost m_exprHost;

		// Token: 0x04001697 RID: 5783
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = FrameBackground.GetDeclaration();
	}
}
