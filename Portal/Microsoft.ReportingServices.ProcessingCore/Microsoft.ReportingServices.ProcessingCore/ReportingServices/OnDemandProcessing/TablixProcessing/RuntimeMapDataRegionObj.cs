using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008E2 RID: 2274
	[PersistedWithinRequestOnly]
	internal class RuntimeMapDataRegionObj : RuntimeChartCriObj
	{
		// Token: 0x06007C40 RID: 31808 RVA: 0x001FEA48 File Offset: 0x001FCC48
		internal RuntimeMapDataRegionObj()
		{
		}

		// Token: 0x06007C41 RID: 31809 RVA: 0x001FEA50 File Offset: 0x001FCC50
		internal RuntimeMapDataRegionObj(IReference<IScope> outerScope, MapDataRegion mapDataRegionDef, ref DataActions dataAction, OnDemandProcessingContext odpContext, bool onePassProcess)
			: base(outerScope, mapDataRegionDef, ref dataAction, odpContext, onePassProcess, Microsoft.ReportingServices.ReportProcessing.ObjectType.MapDataRegion)
		{
		}

		// Token: 0x06007C42 RID: 31810 RVA: 0x001FEA61 File Offset: 0x001FCC61
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeMapDataRegionObj.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007C43 RID: 31811 RVA: 0x001FEA99 File Offset: 0x001FCC99
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeMapDataRegionObj.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007C44 RID: 31812 RVA: 0x001FEAD1 File Offset: 0x001FCCD1
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007C45 RID: 31813 RVA: 0x001FEADB File Offset: 0x001FCCDB
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMapDataRegionObj;
		}

		// Token: 0x06007C46 RID: 31814 RVA: 0x001FEAE4 File Offset: 0x001FCCE4
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeMapDataRegionObj.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMapDataRegionObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriObj, list);
			}
			return RuntimeMapDataRegionObj.m_declaration;
		}

		// Token: 0x04003D9F RID: 15775
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeMapDataRegionObj.GetDeclaration();
	}
}
