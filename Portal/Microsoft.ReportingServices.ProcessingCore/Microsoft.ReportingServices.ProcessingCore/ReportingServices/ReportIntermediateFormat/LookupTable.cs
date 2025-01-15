using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004E6 RID: 1254
	internal class LookupTable : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003F5D RID: 16221 RVA: 0x0010CD62 File Offset: 0x0010AF62
		internal LookupTable()
		{
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x0010CD6A File Offset: 0x0010AF6A
		internal LookupTable(IScalabilityCache scalabilityCache, IEqualityComparer<object> comparer, bool mustStoreDataRows)
		{
			this.m_table = new ScalableDictionary<object, LookupMatches>(0, scalabilityCache, 100, 10, comparer);
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x0010CD84 File Offset: 0x0010AF84
		internal bool TryGetValue(object key, out LookupMatches matches)
		{
			return this.m_table.TryGetValue(key, out matches);
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x0010CD93 File Offset: 0x0010AF93
		internal bool TryGetAndPinValue(object key, out LookupMatches matches, out IDisposable cleanupRef)
		{
			return this.m_table.TryGetAndPin(key, out matches, out cleanupRef);
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x0010CDA3 File Offset: 0x0010AFA3
		internal IDisposable AddAndPin(object key, LookupMatches matches)
		{
			return this.m_table.AddAndPin(key, matches);
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x0010CDB2 File Offset: 0x0010AFB2
		internal void TransferTo(IScalabilityCache scaleCache)
		{
			this.m_table.TransferTo(scaleCache);
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x0010CDC0 File Offset: 0x0010AFC0
		internal void SetEqualityComparer(IEqualityComparer<object> comparer)
		{
			this.m_table.UpdateComparer(comparer);
		}

		// Token: 0x17001AD4 RID: 6868
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x0010CDCE File Offset: 0x0010AFCE
		public int Size
		{
			get
			{
				return this.m_table.Size;
			}
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x0010CDDC File Offset: 0x0010AFDC
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(LookupTable.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.LookupTable)
				{
					writer.Write(this.m_table);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x0010CE30 File Offset: 0x0010B030
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(LookupTable.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.LookupTable)
				{
					this.m_table = reader.ReadRIFObject<ScalableDictionary<object, LookupMatches>>();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x0010CE81 File Offset: 0x0010B081
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x0010CE83 File Offset: 0x0010B083
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupTable;
		}

		// Token: 0x06003F69 RID: 16233 RVA: 0x0010CE8C File Offset: 0x0010B08C
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (LookupTable.m_Declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupTable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.LookupTable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionary, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupMatches)
				});
			}
			return LookupTable.m_Declaration;
		}

		// Token: 0x04001D3E RID: 7486
		private ScalableDictionary<object, LookupMatches> m_table;

		// Token: 0x04001D3F RID: 7487
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = LookupTable.GetDeclaration();
	}
}
