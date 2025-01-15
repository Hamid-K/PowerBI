using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000469 RID: 1129
	internal sealed class CountRows : DataAggregate
	{
		// Token: 0x060033FF RID: 13311 RVA: 0x000E5D48 File Offset: 0x000E3F48
		internal override void Init()
		{
			this.m_currentTotal = 0;
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000E5D51 File Offset: 0x000E3F51
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			this.m_currentTotal++;
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000E5D61 File Offset: 0x000E3F61
		internal override object Result()
		{
			return this.m_currentTotal;
		}

		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x06003402 RID: 13314 RVA: 0x000E5D6E File Offset: 0x000E3F6E
		internal override DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return DataAggregateInfo.AggregateTypes.CountRows;
			}
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000E5D71 File Offset: 0x000E3F71
		internal override DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			return new CountRows();
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000E5D78 File Offset: 0x000E3F78
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(CountRows.m_declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.CurrentTotal)
				{
					writer.Write(this.m_currentTotal);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000E5DC8 File Offset: 0x000E3FC8
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(CountRows.m_declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.CurrentTotal)
				{
					this.m_currentTotal = reader.ReadInt32();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x000E5E16 File Offset: 0x000E4016
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "CountRows should not resolve references");
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x000E5E28 File Offset: 0x000E4028
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CountRows;
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x000E5E2C File Offset: 0x000E402C
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (CountRows.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CountRows, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.CurrentTotal, Token.Int32)
				});
			}
			return CountRows.m_declaration;
		}

		// Token: 0x1700175D RID: 5981
		// (get) Token: 0x06003409 RID: 13321 RVA: 0x000E5E67 File Offset: 0x000E4067
		public override int Size
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x040019E4 RID: 6628
		private int m_currentTotal;

		// Token: 0x040019E5 RID: 6629
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = CountRows.GetDeclaration();
	}
}
