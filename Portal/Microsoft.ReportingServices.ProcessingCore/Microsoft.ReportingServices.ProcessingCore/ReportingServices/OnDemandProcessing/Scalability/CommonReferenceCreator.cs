using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000855 RID: 2133
	internal sealed class CommonReferenceCreator : IReferenceCreator
	{
		// Token: 0x060076FF RID: 30463 RVA: 0x001EC39D File Offset: 0x001EA59D
		private CommonReferenceCreator()
		{
		}

		// Token: 0x06007700 RID: 30464 RVA: 0x001EC3A8 File Offset: 0x001EA5A8
		public bool TryCreateReference(IStorable refTarget, out BaseReference newReference)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType = refTarget.GetObjectType();
			if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNode)
			{
				return this.TryCreateReference(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNodeReference, out newReference);
			}
			if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorableArray)
			{
				return this.TryCreateReference(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorableArrayReference, out newReference);
			}
			newReference = null;
			return false;
		}

		// Token: 0x06007701 RID: 30465 RVA: 0x001EC3DE File Offset: 0x001EA5DE
		public bool TryCreateReference(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType referenceObjectType, out BaseReference reference)
		{
			if (referenceObjectType > Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None)
			{
				if (referenceObjectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNodeReference)
				{
					if (referenceObjectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorableArrayReference)
					{
						reference = null;
						return false;
					}
					reference = new SimpleReference<StorableArray>(referenceObjectType);
				}
				else
				{
					reference = new ScalableDictionaryNodeReference();
				}
				return true;
			}
			Global.Tracer.Assert(false, "Cannot create reference to Nothing or Null");
			reference = null;
			return false;
		}

		// Token: 0x170027CC RID: 10188
		// (get) Token: 0x06007702 RID: 30466 RVA: 0x001EC41E File Offset: 0x001EA61E
		internal static CommonReferenceCreator Instance
		{
			get
			{
				if (CommonReferenceCreator.m_instance == null)
				{
					CommonReferenceCreator.m_instance = new CommonReferenceCreator();
				}
				return CommonReferenceCreator.m_instance;
			}
		}

		// Token: 0x04003C3B RID: 15419
		private static CommonReferenceCreator m_instance;
	}
}
