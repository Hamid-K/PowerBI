using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000463 RID: 1123
	internal class Sum : DataAggregate
	{
		// Token: 0x060033AF RID: 13231 RVA: 0x000E507C File Offset: 0x000E327C
		internal override void Init()
		{
			this.m_currentTotalType = DataAggregate.DataTypeCode.Null;
			this.m_currentTotal = null;
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x000E508C File Offset: 0x000E328C
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			object obj = expressions[0];
			DataAggregate.DataTypeCode typeCode = DataAggregate.GetTypeCode(obj);
			if (DataAggregate.IsNull(typeCode))
			{
				return;
			}
			if (!DataTypeUtility.IsNumeric(typeCode))
			{
				iErrorContext.Register(ProcessingErrorCode.rsAggregateOfNonNumericData, Severity.Warning, Array.Empty<string>());
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			DataAggregate.ConvertToDoubleOrDecimal(typeCode, obj, out typeCode, out obj);
			if (this.m_expressionType == DataAggregate.DataTypeCode.Null)
			{
				this.m_expressionType = typeCode;
			}
			else if (typeCode != this.m_expressionType)
			{
				iErrorContext.Register(ProcessingErrorCode.rsAggregateOfMixedDataTypes, Severity.Warning, Array.Empty<string>());
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			if (this.m_currentTotal == null)
			{
				this.m_currentTotalType = typeCode;
				this.m_currentTotal = obj;
				return;
			}
			this.m_currentTotal = DataAggregate.Add(this.m_currentTotalType, this.m_currentTotal, typeCode, obj);
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x000E5142 File Offset: 0x000E3342
		internal override object Result()
		{
			return this.m_currentTotal;
		}

		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x060033B2 RID: 13234 RVA: 0x000E514A File Offset: 0x000E334A
		internal override DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return DataAggregateInfo.AggregateTypes.Sum;
			}
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x000E514D File Offset: 0x000E334D
		internal override DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			return new Sum();
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x000E5154 File Offset: 0x000E3354
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Sum.m_declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.ExpressionType:
					writer.WriteEnum((int)this.m_expressionType);
					break;
				case MemberName.CurrentTotalType:
					writer.WriteEnum((int)this.m_currentTotalType);
					break;
				case MemberName.CurrentTotal:
					writer.Write(this.m_currentTotal);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x000E51D4 File Offset: 0x000E33D4
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Sum.m_declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.ExpressionType:
					this.m_expressionType = (DataAggregate.DataTypeCode)reader.ReadEnum();
					break;
				case MemberName.CurrentTotalType:
					this.m_currentTotalType = (DataAggregate.DataTypeCode)reader.ReadEnum();
					break;
				case MemberName.CurrentTotal:
					this.m_currentTotal = reader.ReadVariant();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x000E5254 File Offset: 0x000E3454
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "Sum should not resolve references");
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x000E5266 File Offset: 0x000E3466
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Sum;
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x000E526C File Offset: 0x000E346C
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (Sum.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Sum, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregate, new List<MemberInfo>
				{
					new MemberInfo(MemberName.ExpressionType, Token.Enum),
					new MemberInfo(MemberName.CurrentTotalType, Token.Enum),
					new MemberInfo(MemberName.CurrentTotal, Token.Object)
				});
			}
			return Sum.m_declaration;
		}

		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x060033B9 RID: 13241 RVA: 0x000E52C3 File Offset: 0x000E34C3
		public override int Size
		{
			get
			{
				return 8 + ItemSizes.SizeOf(this.m_currentTotal);
			}
		}

		// Token: 0x040019D2 RID: 6610
		private DataAggregate.DataTypeCode m_expressionType;

		// Token: 0x040019D3 RID: 6611
		protected DataAggregate.DataTypeCode m_currentTotalType;

		// Token: 0x040019D4 RID: 6612
		protected object m_currentTotal;

		// Token: 0x040019D5 RID: 6613
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = Sum.GetDeclaration();
	}
}
