using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008E0 RID: 2272
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeCriObj : RuntimeChartCriObj
	{
		// Token: 0x06007C30 RID: 31792 RVA: 0x001FE8A1 File Offset: 0x001FCAA1
		internal RuntimeCriObj()
		{
		}

		// Token: 0x06007C31 RID: 31793 RVA: 0x001FE8A9 File Offset: 0x001FCAA9
		internal RuntimeCriObj(IReference<IScope> outerScope, Microsoft.ReportingServices.ReportIntermediateFormat.CustomReportItem criDef, ref DataActions dataAction, OnDemandProcessingContext odpContext, bool onePassProcess)
			: base(outerScope, criDef, ref dataAction, odpContext, onePassProcess, Microsoft.ReportingServices.ReportProcessing.ObjectType.CustomReportItem)
		{
		}

		// Token: 0x06007C32 RID: 31794 RVA: 0x001FE8BA File Offset: 0x001FCABA
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeCriObj.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007C33 RID: 31795 RVA: 0x001FE8F2 File Offset: 0x001FCAF2
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeCriObj.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007C34 RID: 31796 RVA: 0x001FE92A File Offset: 0x001FCB2A
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007C35 RID: 31797 RVA: 0x001FE934 File Offset: 0x001FCB34
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCriObj;
		}

		// Token: 0x06007C36 RID: 31798 RVA: 0x001FE938 File Offset: 0x001FCB38
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeCriObj.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCriObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriObj, list);
			}
			return RuntimeCriObj.m_declaration;
		}

		// Token: 0x04003D9D RID: 15773
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeCriObj.GetDeclaration();
	}
}
