using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000462 RID: 1122
	internal sealed class Last : DataAggregate
	{
		// Token: 0x060033A2 RID: 13218 RVA: 0x000E4F44 File Offset: 0x000E3144
		internal override void Init()
		{
			this.m_value = null;
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000E4F4D File Offset: 0x000E314D
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			this.m_value = expressions[0];
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000E4F58 File Offset: 0x000E3158
		internal override object Result()
		{
			return this.m_value;
		}

		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x060033A5 RID: 13221 RVA: 0x000E4F60 File Offset: 0x000E3160
		internal override DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return DataAggregateInfo.AggregateTypes.Last;
			}
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x000E4F63 File Offset: 0x000E3163
		internal override DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			return new Last();
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x000E4F6C File Offset: 0x000E316C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Last.m_declaration);
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

		// Token: 0x060033A8 RID: 13224 RVA: 0x000E4FBC File Offset: 0x000E31BC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Last.m_declaration);
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

		// Token: 0x060033A9 RID: 13225 RVA: 0x000E500A File Offset: 0x000E320A
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "Last should not resolve references");
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000E501C File Offset: 0x000E321C
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Last;
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000E5020 File Offset: 0x000E3220
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (Last.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Last, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregate, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Value, Token.Object)
				});
			}
			return Last.m_declaration;
		}

		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x060033AC RID: 13228 RVA: 0x000E505B File Offset: 0x000E325B
		public override int Size
		{
			get
			{
				return ItemSizes.SizeOf(this.m_value);
			}
		}

		// Token: 0x040019D0 RID: 6608
		private object m_value;

		// Token: 0x040019D1 RID: 6609
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = Last.GetDeclaration();
	}
}
