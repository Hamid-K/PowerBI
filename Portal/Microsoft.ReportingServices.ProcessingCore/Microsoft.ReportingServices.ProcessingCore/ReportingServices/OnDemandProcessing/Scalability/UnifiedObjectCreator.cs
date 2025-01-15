using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000856 RID: 2134
	internal sealed class UnifiedObjectCreator : IRIFObjectCreator
	{
		// Token: 0x06007703 RID: 30467 RVA: 0x001EC438 File Offset: 0x001EA638
		internal UnifiedObjectCreator(IScalabilityObjectCreator appObjectCreator, IReferenceCreator appReferenceCreator)
		{
			this.m_objectCreators = new IScalabilityObjectCreator[2];
			this.m_objectCreators[0] = CommonObjectCreator.Instance;
			this.m_objectCreators[1] = appObjectCreator;
			this.m_referenceCreators = new IReferenceCreator[2];
			this.m_referenceCreators[0] = CommonReferenceCreator.Instance;
			this.m_referenceCreators[1] = appReferenceCreator;
		}

		// Token: 0x06007704 RID: 30468 RVA: 0x001EC490 File Offset: 0x001EA690
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable CreateRIFObject(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType, ref Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable persistable = null;
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			while (num < this.m_objectCreators.Length && !flag)
			{
				flag = this.m_objectCreators[num].TryCreateObject(objectType, out persistable);
				num++;
			}
			if (!flag)
			{
				flag2 = true;
				BaseReference baseReference = null;
				int num2 = 0;
				while (num2 < this.m_referenceCreators.Length && !flag)
				{
					flag = this.m_referenceCreators[num2].TryCreateReference(objectType, out baseReference);
					num2++;
				}
				persistable = baseReference;
			}
			if (flag)
			{
				persistable.Deserialize(context);
				if (flag2)
				{
					BaseReference baseReference2 = (BaseReference)persistable;
					persistable = baseReference2.ScalabilityCache.PoolReference(baseReference2);
				}
			}
			else
			{
				Global.Tracer.Assert(false, "Cannot create object of type: {0}", new object[] { objectType });
			}
			return persistable;
		}

		// Token: 0x06007705 RID: 30469 RVA: 0x001EC54C File Offset: 0x001EA74C
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration> GetDeclarations()
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration> list = new List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration>();
			for (int i = 0; i < this.m_objectCreators.Length; i++)
			{
				list.AddRange(this.m_objectCreators[i].GetDeclarations());
			}
			return list;
		}

		// Token: 0x170027CD RID: 10189
		// (get) Token: 0x06007706 RID: 30470 RVA: 0x001EC586 File Offset: 0x001EA786
		// (set) Token: 0x06007707 RID: 30471 RVA: 0x001EC58E File Offset: 0x001EA78E
		internal IScalabilityCache ScalabilityCache
		{
			get
			{
				return this.m_scalabilityCache;
			}
			set
			{
				this.m_scalabilityCache = value;
			}
		}

		// Token: 0x04003C3C RID: 15420
		private IScalabilityObjectCreator[] m_objectCreators;

		// Token: 0x04003C3D RID: 15421
		private IReferenceCreator[] m_referenceCreators;

		// Token: 0x04003C3E RID: 15422
		private IScalabilityCache m_scalabilityCache;
	}
}
