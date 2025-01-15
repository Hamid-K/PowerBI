using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x020008A9 RID: 2217
	[SkipStaticValidation]
	internal abstract class SyntheticReferenceBase<T> : IReference<T>, IReference, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007937 RID: 31031
		public abstract T Value();

		// Token: 0x06007938 RID: 31032 RVA: 0x001F35B4 File Offset: 0x001F17B4
		public IDisposable PinValue()
		{
			Global.Tracer.Assert(false, "PinValue() may not be used on a synthetic reference.");
			throw new InvalidOperationException();
		}

		// Token: 0x06007939 RID: 31033 RVA: 0x001F35CB File Offset: 0x001F17CB
		public void UnPinValue()
		{
			Global.Tracer.Assert(false, "UnPinValue() may not be used on a synthetic reference.");
			throw new InvalidOperationException();
		}

		// Token: 0x0600793A RID: 31034 RVA: 0x001F35E2 File Offset: 0x001F17E2
		public void Free()
		{
			Global.Tracer.Assert(false, "Free() may not be used on a synthetic reference.");
			throw new InvalidOperationException();
		}

		// Token: 0x0600793B RID: 31035 RVA: 0x001F35F9 File Offset: 0x001F17F9
		public void UpdateSize(int sizeDeltaBytes)
		{
			Global.Tracer.Assert(false, "UpdateSize(int) may not be used on a synthetic reference.");
			throw new InvalidOperationException();
		}

		// Token: 0x0600793C RID: 31036 RVA: 0x001F3610 File Offset: 0x001F1810
		public IReference TransferTo(IScalabilityCache scaleCache)
		{
			Global.Tracer.Assert(false, "TransferTo(IScalabilityCache) may not be used on a synthetic reference.");
			throw new InvalidOperationException();
		}

		// Token: 0x17002827 RID: 10279
		// (get) Token: 0x0600793D RID: 31037 RVA: 0x001F3627 File Offset: 0x001F1827
		public ReferenceID Id
		{
			get
			{
				Global.Tracer.Assert(false, "Id may not be used on a synthetic reference.");
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17002828 RID: 10280
		// (get) Token: 0x0600793E RID: 31038 RVA: 0x001F363E File Offset: 0x001F183E
		public int Size
		{
			get
			{
				Global.Tracer.Assert(false, "Size may not be used on a synthetic reference.");
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600793F RID: 31039 RVA: 0x001F3655 File Offset: 0x001F1855
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			Global.Tracer.Assert(false, "Serialize may not be used on a synthetic reference.");
			throw new InvalidOperationException();
		}

		// Token: 0x06007940 RID: 31040 RVA: 0x001F366C File Offset: 0x001F186C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(false, "Deserialize may not be used on a synthetic reference.");
			throw new InvalidOperationException();
		}

		// Token: 0x06007941 RID: 31041 RVA: 0x001F3683 File Offset: 0x001F1883
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "ResolveReferences may not be used on a synthetic reference.");
			throw new InvalidOperationException();
		}

		// Token: 0x06007942 RID: 31042
		public abstract Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType();
	}
}
