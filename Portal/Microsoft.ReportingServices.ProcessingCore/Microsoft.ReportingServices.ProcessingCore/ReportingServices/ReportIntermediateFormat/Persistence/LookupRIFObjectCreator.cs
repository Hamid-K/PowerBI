using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000538 RID: 1336
	internal struct LookupRIFObjectCreator : IRIFObjectCreator, IScalabilityObjectCreator
	{
		// Token: 0x06004968 RID: 18792 RVA: 0x001367D4 File Offset: 0x001349D4
		public IPersistable CreateRIFObject(ObjectType objectType, ref IntermediateFormatReader context)
		{
			IPersistable persistable = null;
			if (objectType == ObjectType.Null)
			{
				return null;
			}
			Global.Tracer.Assert(this.TryCreateObject(objectType, out persistable));
			persistable.Deserialize(context);
			return persistable;
		}

		// Token: 0x06004969 RID: 18793 RVA: 0x00136808 File Offset: 0x00134A08
		public bool TryCreateObject(ObjectType objectType, out IPersistable persistObj)
		{
			if (objectType != ObjectType.IntermediateFormatVersion)
			{
				switch (objectType)
				{
				case ObjectType.LookupTable:
					persistObj = new LookupTable();
					return true;
				case ObjectType.LookupMatches:
					persistObj = new LookupMatches();
					return true;
				case ObjectType.LookupMatchesWithRows:
					persistObj = new LookupMatchesWithRows();
					return true;
				}
				persistObj = null;
				return false;
			}
			persistObj = new IntermediateFormatVersion();
			return true;
		}

		// Token: 0x0600496A RID: 18794 RVA: 0x00136869 File Offset: 0x00134A69
		public List<Declaration> GetDeclarations()
		{
			return LookupRIFObjectCreator.m_declarations;
		}

		// Token: 0x0600496B RID: 18795 RVA: 0x00136870 File Offset: 0x00134A70
		private static List<Declaration> BuildDeclarations()
		{
			return new List<Declaration>(3)
			{
				LookupMatches.GetDeclaration(),
				LookupMatchesWithRows.GetDeclaration(),
				LookupTable.GetDeclaration()
			};
		}

		// Token: 0x0400206D RID: 8301
		private static List<Declaration> m_declarations = LookupRIFObjectCreator.BuildDeclarations();
	}
}
