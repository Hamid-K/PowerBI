using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008DC RID: 2268
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeTablixObj : RuntimeDataTablixWithScopedItemsObj, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007C0D RID: 31757 RVA: 0x001FE5CB File Offset: 0x001FC7CB
		internal RuntimeTablixObj()
		{
		}

		// Token: 0x06007C0E RID: 31758 RVA: 0x001FE5D3 File Offset: 0x001FC7D3
		internal RuntimeTablixObj(IReference<IScope> outerScope, Microsoft.ReportingServices.ReportIntermediateFormat.Tablix tablixDef, ref DataActions dataAction, OnDemandProcessingContext odpContext, bool onePassProcess)
			: base(outerScope, tablixDef, ref dataAction, odpContext, onePassProcess, Microsoft.ReportingServices.ReportProcessing.ObjectType.Tablix)
		{
		}

		// Token: 0x06007C0F RID: 31759 RVA: 0x001FE5E4 File Offset: 0x001FC7E4
		protected override List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> GetDataRegionScopedItems()
		{
			return ((Microsoft.ReportingServices.ReportIntermediateFormat.Tablix)this.m_dataRegionDef).DataRegionScopedItemsForDataProcessing;
		}

		// Token: 0x06007C10 RID: 31760 RVA: 0x001FE5F6 File Offset: 0x001FC7F6
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixObj;
		}

		// Token: 0x06007C11 RID: 31761 RVA: 0x001FE5FA File Offset: 0x001FC7FA
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
		}

		// Token: 0x06007C12 RID: 31762 RVA: 0x001FE603 File Offset: 0x001FC803
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
		}

		// Token: 0x06007C13 RID: 31763 RVA: 0x001FE60C File Offset: 0x001FC80C
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007C14 RID: 31764 RVA: 0x001FE618 File Offset: 0x001FC818
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeTablixObj.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				RuntimeTablixObj.m_declaration = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixWithScopedItemsObj, list);
			}
			return RuntimeTablixObj.m_declaration;
		}

		// Token: 0x04003D99 RID: 15769
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeTablixObj.GetDeclaration();
	}
}
