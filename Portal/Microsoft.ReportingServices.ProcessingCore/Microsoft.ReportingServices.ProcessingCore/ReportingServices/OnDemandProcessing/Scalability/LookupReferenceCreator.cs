using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200084A RID: 2122
	internal class LookupReferenceCreator : IReferenceCreator
	{
		// Token: 0x06007675 RID: 30325 RVA: 0x001EB0A8 File Offset: 0x001E92A8
		private LookupReferenceCreator()
		{
		}

		// Token: 0x06007676 RID: 30326 RVA: 0x001EB0B0 File Offset: 0x001E92B0
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

		// Token: 0x06007677 RID: 30327 RVA: 0x001EB0DC File Offset: 0x001E92DC
		public bool TryCreateReference(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType referenceObjectType, out BaseReference reference)
		{
			if (referenceObjectType <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None)
			{
				Global.Tracer.Assert(false, "Cannot create reference to Nothing or Null");
				reference = null;
				return false;
			}
			if (referenceObjectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupTableReference)
			{
				reference = null;
				return false;
			}
			reference = new SimpleReference<LookupTable>(referenceObjectType);
			return true;
		}

		// Token: 0x06007678 RID: 30328 RVA: 0x001EB111 File Offset: 0x001E9311
		private bool TryMapObjectTypeToReferenceType(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType targetType, out Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType referenceType)
		{
			if (targetType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupTable)
			{
				referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupTableReference;
				return true;
			}
			referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None;
			return false;
		}

		// Token: 0x170027B0 RID: 10160
		// (get) Token: 0x06007679 RID: 30329 RVA: 0x001EB12A File Offset: 0x001E932A
		internal static LookupReferenceCreator Instance
		{
			get
			{
				if (LookupReferenceCreator.m_instance == null)
				{
					LookupReferenceCreator.m_instance = new LookupReferenceCreator();
				}
				return LookupReferenceCreator.m_instance;
			}
		}

		// Token: 0x04003C00 RID: 15360
		private static LookupReferenceCreator m_instance;
	}
}
