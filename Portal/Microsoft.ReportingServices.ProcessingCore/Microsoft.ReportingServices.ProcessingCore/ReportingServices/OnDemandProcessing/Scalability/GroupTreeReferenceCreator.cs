using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200083B RID: 2107
	internal class GroupTreeReferenceCreator : IReferenceCreator
	{
		// Token: 0x060075E8 RID: 30184 RVA: 0x001E8F06 File Offset: 0x001E7106
		private GroupTreeReferenceCreator()
		{
		}

		// Token: 0x060075E9 RID: 30185 RVA: 0x001E8F10 File Offset: 0x001E7110
		public bool TryCreateReference(IStorable refTarget, out BaseReference newReference)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType = refTarget.GetObjectType();
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType2;
			if (this.TryMapObjectTypeToReferenceType(objectType, out objectType2))
			{
				return this.TryCreateReference(objectType2, out newReference);
			}
			newReference = null;
			return false;
		}

		// Token: 0x060075EA RID: 30186 RVA: 0x001E8F3C File Offset: 0x001E713C
		public bool TryCreateReference(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType referenceObjectType, out BaseReference reference)
		{
			if (referenceObjectType > Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None)
			{
				switch (referenceObjectType)
				{
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegionInstanceReference:
					reference = new DataRegionInstanceReference();
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReportInstanceReference:
					reference = new SubReportInstanceReference();
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportInstanceReference:
					reference = new ReportInstanceReference();
					break;
				default:
					reference = null;
					return false;
				}
				return true;
			}
			Global.Tracer.Assert(false, "Cannot create reference to Nothing or Null");
			reference = null;
			return false;
		}

		// Token: 0x060075EB RID: 30187 RVA: 0x001E8F9A File Offset: 0x001E719A
		private bool TryMapObjectTypeToReferenceType(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType targetType, out Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType referenceType)
		{
			if (targetType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportInstance)
			{
				if (targetType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReportInstance)
				{
					if (targetType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegionInstance)
					{
						referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None;
						return false;
					}
					referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegionInstanceReference;
				}
				else
				{
					referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReportInstanceReference;
				}
			}
			else
			{
				referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportInstanceReference;
			}
			return true;
		}

		// Token: 0x170027A2 RID: 10146
		// (get) Token: 0x060075EC RID: 30188 RVA: 0x001E8FCC File Offset: 0x001E71CC
		internal static GroupTreeReferenceCreator Instance
		{
			get
			{
				if (GroupTreeReferenceCreator.m_instance == null)
				{
					GroupTreeReferenceCreator.m_instance = new GroupTreeReferenceCreator();
				}
				return GroupTreeReferenceCreator.m_instance;
			}
		}

		// Token: 0x04003BA8 RID: 15272
		private static GroupTreeReferenceCreator m_instance;
	}
}
