using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008DF RID: 2271
	[PersistedWithinRequestOnly]
	internal class RuntimeChartObj : RuntimeChartCriObj
	{
		// Token: 0x06007C28 RID: 31784 RVA: 0x001FE7D1 File Offset: 0x001FC9D1
		internal RuntimeChartObj()
		{
		}

		// Token: 0x06007C29 RID: 31785 RVA: 0x001FE7D9 File Offset: 0x001FC9D9
		internal RuntimeChartObj(IReference<IScope> outerScope, Microsoft.ReportingServices.ReportIntermediateFormat.Chart chartDef, ref DataActions dataAction, OnDemandProcessingContext odpContext, bool onePassProcess)
			: base(outerScope, chartDef, ref dataAction, odpContext, onePassProcess, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart)
		{
		}

		// Token: 0x06007C2A RID: 31786 RVA: 0x001FE7EA File Offset: 0x001FC9EA
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeChartObj.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007C2B RID: 31787 RVA: 0x001FE822 File Offset: 0x001FCA22
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeChartObj.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007C2C RID: 31788 RVA: 0x001FE85A File Offset: 0x001FCA5A
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007C2D RID: 31789 RVA: 0x001FE864 File Offset: 0x001FCA64
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartObj;
		}

		// Token: 0x06007C2E RID: 31790 RVA: 0x001FE868 File Offset: 0x001FCA68
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeChartObj.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriObj, list);
			}
			return RuntimeChartObj.m_declaration;
		}

		// Token: 0x04003D9C RID: 15772
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeChartObj.GetDeclaration();
	}
}
