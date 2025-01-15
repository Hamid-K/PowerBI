using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008DD RID: 2269
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeDataShapeObj : RuntimeDataTablixWithScopedItemsObj, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007C16 RID: 31766 RVA: 0x001FE655 File Offset: 0x001FC855
		internal RuntimeDataShapeObj()
		{
		}

		// Token: 0x06007C17 RID: 31767 RVA: 0x001FE65D File Offset: 0x001FC85D
		internal RuntimeDataShapeObj(IReference<IScope> outerScope, DataShape rifDataShape, ref DataActions dataAction, OnDemandProcessingContext odpContext, bool onePassProcess)
			: base(outerScope, rifDataShape, ref dataAction, odpContext, onePassProcess, Microsoft.ReportingServices.ReportProcessing.ObjectType.DataShape)
		{
		}

		// Token: 0x06007C18 RID: 31768 RVA: 0x001FE66E File Offset: 0x001FC86E
		protected override List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> GetDataRegionScopedItems()
		{
			return ((DataShape)this.m_dataRegionDef).DataRegionScopedItemsForDataProcessing;
		}

		// Token: 0x06007C19 RID: 31769 RVA: 0x001FE680 File Offset: 0x001FC880
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeObj;
		}

		// Token: 0x06007C1A RID: 31770 RVA: 0x001FE687 File Offset: 0x001FC887
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
		}

		// Token: 0x06007C1B RID: 31771 RVA: 0x001FE690 File Offset: 0x001FC890
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
		}

		// Token: 0x06007C1C RID: 31772 RVA: 0x001FE699 File Offset: 0x001FC899
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007C1D RID: 31773 RVA: 0x001FE6A4 File Offset: 0x001FC8A4
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeDataShapeObj.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				RuntimeDataShapeObj.m_declaration = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixWithScopedItemsObj, list);
			}
			return RuntimeDataShapeObj.m_declaration;
		}

		// Token: 0x04003D9A RID: 15770
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeDataShapeObj.GetDeclaration();
	}
}
