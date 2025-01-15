using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008E1 RID: 2273
	[PersistedWithinRequestOnly]
	internal class RuntimeGaugePanelObj : RuntimeChartCriObj
	{
		// Token: 0x06007C38 RID: 31800 RVA: 0x001FE971 File Offset: 0x001FCB71
		internal RuntimeGaugePanelObj()
		{
		}

		// Token: 0x06007C39 RID: 31801 RVA: 0x001FE979 File Offset: 0x001FCB79
		internal RuntimeGaugePanelObj(IReference<IScope> outerScope, GaugePanel gaugePanelDef, ref DataActions dataAction, OnDemandProcessingContext odpContext, bool onePassProcess)
			: base(outerScope, gaugePanelDef, ref dataAction, odpContext, onePassProcess, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel)
		{
		}

		// Token: 0x06007C3A RID: 31802 RVA: 0x001FE98A File Offset: 0x001FCB8A
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeGaugePanelObj.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007C3B RID: 31803 RVA: 0x001FE9C2 File Offset: 0x001FCBC2
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeGaugePanelObj.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007C3C RID: 31804 RVA: 0x001FE9FA File Offset: 0x001FCBFA
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007C3D RID: 31805 RVA: 0x001FEA04 File Offset: 0x001FCC04
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGaugePanelObj;
		}

		// Token: 0x06007C3E RID: 31806 RVA: 0x001FEA0C File Offset: 0x001FCC0C
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeGaugePanelObj.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGaugePanelObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriObj, list);
			}
			return RuntimeGaugePanelObj.m_declaration;
		}

		// Token: 0x04003D9E RID: 15774
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeGaugePanelObj.GetDeclaration();
	}
}
