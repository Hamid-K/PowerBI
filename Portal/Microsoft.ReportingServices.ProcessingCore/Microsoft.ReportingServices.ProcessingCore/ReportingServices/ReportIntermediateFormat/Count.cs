using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000467 RID: 1127
	internal sealed class Count : DataAggregate
	{
		// Token: 0x060033E5 RID: 13285 RVA: 0x000E5A56 File Offset: 0x000E3C56
		internal override void Init()
		{
			this.m_currentTotal = 0;
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x000E5A5F File Offset: 0x000E3C5F
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			if (DataAggregate.IsNull(DataAggregate.GetTypeCode(expressions[0])))
			{
				return;
			}
			this.m_currentTotal++;
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x000E5A7F File Offset: 0x000E3C7F
		internal override object Result()
		{
			return this.m_currentTotal;
		}

		// Token: 0x17001758 RID: 5976
		// (get) Token: 0x060033E8 RID: 13288 RVA: 0x000E5A8C File Offset: 0x000E3C8C
		internal override DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return DataAggregateInfo.AggregateTypes.Count;
			}
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x000E5A8F File Offset: 0x000E3C8F
		internal override DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			return new Count();
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000E5A98 File Offset: 0x000E3C98
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Count.m_declaration);
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

		// Token: 0x060033EB RID: 13291 RVA: 0x000E5AE8 File Offset: 0x000E3CE8
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Count.m_declaration);
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

		// Token: 0x060033EC RID: 13292 RVA: 0x000E5B36 File Offset: 0x000E3D36
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "Count should not resolve references");
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x000E5B48 File Offset: 0x000E3D48
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Count;
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x000E5B4C File Offset: 0x000E3D4C
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (Count.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Count, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.CurrentTotal, Token.Int32)
				});
			}
			return Count.m_declaration;
		}

		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x060033EF RID: 13295 RVA: 0x000E5B87 File Offset: 0x000E3D87
		public override int Size
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x040019E0 RID: 6624
		private int m_currentTotal;

		// Token: 0x040019E1 RID: 6625
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = Count.GetDeclaration();
	}
}
