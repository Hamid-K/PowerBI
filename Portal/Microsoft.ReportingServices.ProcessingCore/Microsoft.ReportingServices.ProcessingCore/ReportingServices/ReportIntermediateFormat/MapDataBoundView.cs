using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000454 RID: 1108
	[Serializable]
	internal sealed class MapDataBoundView : MapView, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060032A7 RID: 12967 RVA: 0x000E1E2C File Offset: 0x000E002C
		internal MapDataBoundView()
		{
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x000E1E34 File Offset: 0x000E0034
		internal MapDataBoundView(Map map)
			: base(map)
		{
		}

		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x060032A9 RID: 12969 RVA: 0x000E1E3D File Offset: 0x000E003D
		internal new MapDataBoundViewExprHost ExprHost
		{
			get
			{
				return (MapDataBoundViewExprHost)this.m_exprHost;
			}
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x000E1E4A File Offset: 0x000E004A
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapDataBoundViewStart();
			base.Initialize(context);
			context.ExprHostBuilder.MapDataBoundViewEnd();
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x000E1E6B File Offset: 0x000E006B
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			return (MapDataBoundView)base.PublishClone(context);
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x000E1E7C File Offset: 0x000E007C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDataBoundView, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapView, list);
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000E1E9F File Offset: 0x000E009F
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapDataBoundView.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000E1ED7 File Offset: 0x000E00D7
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapDataBoundView.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000E1F0F File Offset: 0x000E010F
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDataBoundView;
		}

		// Token: 0x0400198A RID: 6538
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapDataBoundView.GetDeclaration();
	}
}
