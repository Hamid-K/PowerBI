using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200045D RID: 1117
	internal class Aggregate : DataAggregate
	{
		// Token: 0x06003360 RID: 13152 RVA: 0x000E4112 File Offset: 0x000E2312
		internal override void Init()
		{
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x000E4114 File Offset: 0x000E2314
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			this.m_value = expressions[0];
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x000E411F File Offset: 0x000E231F
		internal override object Result()
		{
			return this.m_value;
		}

		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x06003363 RID: 13155 RVA: 0x000E4127 File Offset: 0x000E2327
		internal override DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return DataAggregateInfo.AggregateTypes.Aggregate;
			}
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000E412B File Offset: 0x000E232B
		internal override DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			return new Aggregate();
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x000E4134 File Offset: 0x000E2334
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Aggregate.m_declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.Value)
				{
					writer.Write(this.m_value);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x000E4184 File Offset: 0x000E2384
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Aggregate.m_declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.Value)
				{
					this.m_value = reader.ReadVariant();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x000E41D2 File Offset: 0x000E23D2
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "Aggregate should not resolve references");
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x000E41E4 File Offset: 0x000E23E4
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Aggregate;
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x000E41E8 File Offset: 0x000E23E8
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (Aggregate.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Aggregate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Value, Token.Object)
				});
			}
			return Aggregate.m_declaration;
		}

		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x0600336A RID: 13162 RVA: 0x000E421F File Offset: 0x000E241F
		public override int Size
		{
			get
			{
				return ItemSizes.SizeOf(this.m_value);
			}
		}

		// Token: 0x040019BA RID: 6586
		private object m_value;

		// Token: 0x040019BB RID: 6587
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = Aggregate.GetDeclaration();
	}
}
