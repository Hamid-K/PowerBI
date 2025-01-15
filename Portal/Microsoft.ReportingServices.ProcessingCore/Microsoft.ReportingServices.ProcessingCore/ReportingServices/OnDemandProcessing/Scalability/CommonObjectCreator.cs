using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000854 RID: 2132
	internal sealed class CommonObjectCreator : IScalabilityObjectCreator
	{
		// Token: 0x060076F9 RID: 30457 RVA: 0x001EC291 File Offset: 0x001EA491
		private CommonObjectCreator()
		{
		}

		// Token: 0x060076FA RID: 30458 RVA: 0x001EC29C File Offset: 0x001EA49C
		public bool TryCreateObject(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType, out Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable persistObj)
		{
			if (objectType != Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorageItem)
			{
				switch (objectType)
				{
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryValues:
					persistObj = new ScalableDictionaryValues();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNode:
					persistObj = new ScalableDictionaryNode();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionary:
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorableArray:
					persistObj = new StorableArray();
					return true;
				default:
					if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableHybridListEntry)
					{
						persistObj = new ScalableHybridListEntry();
						return true;
					}
					break;
				}
				persistObj = null;
				return false;
			}
			persistObj = new StorageItem();
			return true;
		}

		// Token: 0x060076FB RID: 30459 RVA: 0x001EC301 File Offset: 0x001EA501
		public List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration> GetDeclarations()
		{
			return CommonObjectCreator.m_declarations;
		}

		// Token: 0x170027CB RID: 10187
		// (get) Token: 0x060076FC RID: 30460 RVA: 0x001EC308 File Offset: 0x001EA508
		internal static CommonObjectCreator Instance
		{
			get
			{
				if (CommonObjectCreator.m_instance == null)
				{
					CommonObjectCreator.m_instance = new CommonObjectCreator();
				}
				return CommonObjectCreator.m_instance;
			}
		}

		// Token: 0x060076FD RID: 30461 RVA: 0x001EC320 File Offset: 0x001EA520
		private static List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration> BuildDeclarations()
		{
			return new List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration>(8)
			{
				BaseReference.GetDeclaration(),
				ScalableList<StorageItem>.GetDeclaration(),
				ScalableDictionary<int, StorageItem>.GetDeclaration(),
				ScalableDictionaryNode.GetDeclaration(),
				ScalableDictionaryValues.GetDeclaration(),
				StorageItem.GetDeclaration(),
				StorableArray.GetDeclaration(),
				ScalableHybridListEntry.GetDeclaration()
			};
		}

		// Token: 0x04003C39 RID: 15417
		private static List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration> m_declarations = CommonObjectCreator.BuildDeclarations();

		// Token: 0x04003C3A RID: 15418
		private static CommonObjectCreator m_instance = null;
	}
}
