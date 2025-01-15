using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008DE RID: 2270
	[PersistedWithinRequestOnly]
	internal class RuntimeChartCriObj : RuntimeDataTablixObj
	{
		// Token: 0x06007C1F RID: 31775 RVA: 0x001FE6E4 File Offset: 0x001FC8E4
		internal RuntimeChartCriObj()
		{
		}

		// Token: 0x06007C20 RID: 31776 RVA: 0x001FE6EC File Offset: 0x001FC8EC
		internal RuntimeChartCriObj(IReference<IScope> outerScope, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef, ref DataActions dataAction, OnDemandProcessingContext odpContext, bool onePassProcess, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(outerScope, dataRegionDef, ref dataAction, odpContext, onePassProcess, objectType)
		{
		}

		// Token: 0x06007C21 RID: 31777 RVA: 0x001FE6FD File Offset: 0x001FC8FD
		internal override RuntimeDataTablixObjReference GetNestedDataRegion(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion rifDataRegion)
		{
			Global.Tracer.Assert(false, "RuntimeChartCriObj cannot have nested data regions");
			throw new InvalidOperationException();
		}

		// Token: 0x06007C22 RID: 31778 RVA: 0x001FE714 File Offset: 0x001FC914
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeChartCriObj.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007C23 RID: 31779 RVA: 0x001FE74C File Offset: 0x001FC94C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeChartCriObj.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007C24 RID: 31780 RVA: 0x001FE784 File Offset: 0x001FC984
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007C25 RID: 31781 RVA: 0x001FE78E File Offset: 0x001FC98E
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriObj;
		}

		// Token: 0x06007C26 RID: 31782 RVA: 0x001FE798 File Offset: 0x001FC998
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeChartCriObj.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixObj, list);
			}
			return RuntimeChartCriObj.m_declaration;
		}

		// Token: 0x04003D9B RID: 15771
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeChartCriObj.GetDeclaration();
	}
}
