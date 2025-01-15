using System;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x020008AA RID: 2218
	[SkipStaticValidation]
	internal sealed class SyntheticTriangulatedCellReference : SyntheticReferenceBase<IOnDemandScopeInstance>, IReference<RuntimeCell>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007944 RID: 31044 RVA: 0x001F36A2 File Offset: 0x001F18A2
		public SyntheticTriangulatedCellReference(IReference<IOnDemandMemberInstance> outerGroupLeafRef, IReference<IOnDemandMemberInstance> innerGroupLeafRef)
		{
			this.UpdateGroupLeafReferences(outerGroupLeafRef, innerGroupLeafRef);
		}

		// Token: 0x06007945 RID: 31045 RVA: 0x001F36B2 File Offset: 0x001F18B2
		public void UpdateGroupLeafReferences(IReference<IOnDemandMemberInstance> outerGroupLeafRef, IReference<IOnDemandMemberInstance> innerGroupLeafRef)
		{
			this.m_outerGroupLeafRef = outerGroupLeafRef;
			this.m_innerGroupLeafRef = innerGroupLeafRef;
		}

		// Token: 0x06007946 RID: 31046 RVA: 0x001F36C2 File Offset: 0x001F18C2
		RuntimeCell IReference<RuntimeCell>.Value()
		{
			return (RuntimeCell)this.Value();
		}

		// Token: 0x06007947 RID: 31047 RVA: 0x001F36D0 File Offset: 0x001F18D0
		public override IOnDemandScopeInstance Value()
		{
			IReference<IOnDemandScopeInstance> reference;
			return SyntheticTriangulatedCellReference.GetCellInstance(this.m_outerGroupLeafRef, this.m_innerGroupLeafRef, out reference);
		}

		// Token: 0x06007948 RID: 31048 RVA: 0x001F36F0 File Offset: 0x001F18F0
		internal static IOnDemandScopeInstance GetCellInstance(IReference<IOnDemandMemberInstance> outerGroupLeafRef, IReference<IOnDemandMemberInstance> innerGroupLeafRef, out IReference<IOnDemandScopeInstance> cellRef)
		{
			IOnDemandScopeInstance cellInstance;
			using (innerGroupLeafRef.PinValue())
			{
				cellInstance = innerGroupLeafRef.Value().GetCellInstance((IOnDemandMemberInstanceReference)outerGroupLeafRef, out cellRef);
			}
			return cellInstance;
		}

		// Token: 0x06007949 RID: 31049 RVA: 0x001F3734 File Offset: 0x001F1934
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SyntheticTriangulatedCellReference;
		}

		// Token: 0x0600794A RID: 31050 RVA: 0x001F373C File Offset: 0x001F193C
		public override bool Equals(object obj)
		{
			SyntheticTriangulatedCellReference syntheticTriangulatedCellReference = obj as SyntheticTriangulatedCellReference;
			return syntheticTriangulatedCellReference != null && object.Equals(this.m_outerGroupLeafRef, syntheticTriangulatedCellReference.m_outerGroupLeafRef) && object.Equals(this.m_innerGroupLeafRef, syntheticTriangulatedCellReference.m_innerGroupLeafRef);
		}

		// Token: 0x0600794B RID: 31051 RVA: 0x001F377B File Offset: 0x001F197B
		public override int GetHashCode()
		{
			return this.m_outerGroupLeafRef.GetHashCode() ^ this.m_innerGroupLeafRef.GetHashCode();
		}

		// Token: 0x04003CDF RID: 15583
		private IReference<IOnDemandMemberInstance> m_outerGroupLeafRef;

		// Token: 0x04003CE0 RID: 15584
		private IReference<IOnDemandMemberInstance> m_innerGroupLeafRef;
	}
}
