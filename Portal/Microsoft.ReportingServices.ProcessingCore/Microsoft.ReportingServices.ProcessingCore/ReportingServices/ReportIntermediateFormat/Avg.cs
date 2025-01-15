using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000464 RID: 1124
	internal sealed class Avg : Sum
	{
		// Token: 0x060033BC RID: 13244 RVA: 0x000E52E6 File Offset: 0x000E34E6
		internal override void Init()
		{
			base.Init();
			this.m_currentCount = 0U;
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x000E52F5 File Offset: 0x000E34F5
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			if (DataAggregate.IsNull(DataAggregate.GetTypeCode(expressions[0])))
			{
				return;
			}
			base.Update(expressions, iErrorContext);
			this.m_currentCount += 1U;
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000E5320 File Offset: 0x000E3520
		internal override object Result()
		{
			DataAggregate.DataTypeCode currentTotalType = this.m_currentTotalType;
			if (currentTotalType == DataAggregate.DataTypeCode.Null)
			{
				return null;
			}
			if (currentTotalType == DataAggregate.DataTypeCode.Double)
			{
				return (double)this.m_currentTotal / this.m_currentCount;
			}
			if (currentTotalType != DataAggregate.DataTypeCode.Decimal)
			{
				Global.Tracer.Assert(false);
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			return (decimal)this.m_currentTotal / this.m_currentCount;
		}

		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x060033BF RID: 13247 RVA: 0x000E5395 File Offset: 0x000E3595
		internal override DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return DataAggregateInfo.AggregateTypes.Avg;
			}
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000E5398 File Offset: 0x000E3598
		internal override DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			return new Avg();
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x000E53A0 File Offset: 0x000E35A0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Avg.m_declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.CurrentCount)
				{
					writer.Write(this.m_currentCount);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000E53F8 File Offset: 0x000E35F8
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Avg.m_declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.CurrentCount)
				{
					this.m_currentCount = reader.ReadUInt32();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x000E544D File Offset: 0x000E364D
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "Avg should not resolve references");
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000E545F File Offset: 0x000E365F
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Avg;
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x000E5464 File Offset: 0x000E3664
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (Avg.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Avg, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Sum, new List<MemberInfo>
				{
					new MemberInfo(MemberName.CurrentCount, Token.UInt32)
				});
			}
			return Avg.m_declaration;
		}

		// Token: 0x17001753 RID: 5971
		// (get) Token: 0x060033C6 RID: 13254 RVA: 0x000E54A0 File Offset: 0x000E36A0
		public override int Size
		{
			get
			{
				return base.Size + 4;
			}
		}

		// Token: 0x040019D6 RID: 6614
		private uint m_currentCount;

		// Token: 0x040019D7 RID: 6615
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = Avg.GetDeclaration();
	}
}
