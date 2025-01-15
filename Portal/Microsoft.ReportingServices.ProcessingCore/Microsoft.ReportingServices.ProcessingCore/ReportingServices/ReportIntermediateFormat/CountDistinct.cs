using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000468 RID: 1128
	internal sealed class CountDistinct : DataAggregate
	{
		// Token: 0x060033F2 RID: 13298 RVA: 0x000E5B9E File Offset: 0x000E3D9E
		internal override void Init()
		{
			this.m_distinctValues.Clear();
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000E5BAC File Offset: 0x000E3DAC
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			object obj = expressions[0];
			DataAggregate.DataTypeCode typeCode = DataAggregate.GetTypeCode(obj);
			if (DataAggregate.IsNull(typeCode))
			{
				return;
			}
			if (!DataAggregate.IsVariant(typeCode) || DataTypeUtility.IsSpatial(typeCode))
			{
				iErrorContext.Register(ProcessingErrorCode.rsInvalidExpressionDataType, Severity.Warning, Array.Empty<string>());
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			if (!this.m_distinctValues.ContainsKey(obj))
			{
				this.m_distinctValues.Add(obj, null);
			}
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000E5C14 File Offset: 0x000E3E14
		internal override object Result()
		{
			return this.m_distinctValues.Count;
		}

		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x060033F5 RID: 13301 RVA: 0x000E5C26 File Offset: 0x000E3E26
		internal override DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return DataAggregateInfo.AggregateTypes.CountDistinct;
			}
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000E5C29 File Offset: 0x000E3E29
		internal override DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			return new CountDistinct();
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000E5C30 File Offset: 0x000E3E30
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(CountDistinct.m_declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.DistinctValues)
				{
					writer.WriteVariantVariantHashtable(this.m_distinctValues);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000E5C80 File Offset: 0x000E3E80
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(CountDistinct.m_declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.DistinctValues)
				{
					this.m_distinctValues = reader.ReadVariantVariantHashtable();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000E5CCE File Offset: 0x000E3ECE
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "CountDistinct should not resolve references");
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000E5CE0 File Offset: 0x000E3EE0
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CountDistinct;
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000E5CE4 File Offset: 0x000E3EE4
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (CountDistinct.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CountDistinct, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.DistinctValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VariantVariantHashtable)
				});
			}
			return CountDistinct.m_declaration;
		}

		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x060033FC RID: 13308 RVA: 0x000E5D1C File Offset: 0x000E3F1C
		public override int Size
		{
			get
			{
				return ItemSizes.SizeOf(this.m_distinctValues);
			}
		}

		// Token: 0x040019E2 RID: 6626
		private Hashtable m_distinctValues = new Hashtable();

		// Token: 0x040019E3 RID: 6627
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = CountDistinct.GetDeclaration();
	}
}
