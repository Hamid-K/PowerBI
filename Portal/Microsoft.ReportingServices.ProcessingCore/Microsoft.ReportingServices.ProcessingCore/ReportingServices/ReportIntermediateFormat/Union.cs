using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.SqlServer.Types;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000470 RID: 1136
	internal class Union : DataAggregate
	{
		// Token: 0x06003440 RID: 13376 RVA: 0x000E6A7A File Offset: 0x000E4C7A
		internal override void Init()
		{
			this.m_expressionType = DataAggregate.DataTypeCode.Null;
			this.m_currentUnion = null;
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000E6A8C File Offset: 0x000E4C8C
		internal override void Update(object[] expressions, IErrorContext iErrorContext)
		{
			object obj = expressions[0];
			DataAggregate.DataTypeCode typeCode = DataAggregate.GetTypeCode(obj);
			if (DataAggregate.IsNull(typeCode))
			{
				return;
			}
			if (!DataTypeUtility.IsSpatial(typeCode))
			{
				iErrorContext.Register(ProcessingErrorCode.rsUnionOfNonSpatialData, Severity.Warning, Array.Empty<string>());
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			if (this.m_expressionType == DataAggregate.DataTypeCode.Null)
			{
				this.m_expressionType = typeCode;
			}
			else if (typeCode != this.m_expressionType)
			{
				iErrorContext.Register(ProcessingErrorCode.rsUnionOfMixedSpatialTypes, Severity.Warning, Array.Empty<string>());
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			if (this.m_currentUnion == null)
			{
				this.m_expressionType = typeCode;
				this.m_currentUnion = obj;
				return;
			}
			if (this.m_expressionType == DataAggregate.DataTypeCode.SqlGeometry)
			{
				this.m_currentUnion = ((SqlGeometry)this.m_currentUnion).STUnion((SqlGeometry)obj);
				return;
			}
			this.m_currentUnion = ((SqlGeography)this.m_currentUnion).STUnion((SqlGeography)obj);
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x000E6B61 File Offset: 0x000E4D61
		internal override object Result()
		{
			return this.m_currentUnion;
		}

		// Token: 0x17001765 RID: 5989
		// (get) Token: 0x06003443 RID: 13379 RVA: 0x000E6B69 File Offset: 0x000E4D69
		internal override DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return DataAggregateInfo.AggregateTypes.Union;
			}
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x000E6B6D File Offset: 0x000E4D6D
		internal override DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			return new Union();
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x000E6B74 File Offset: 0x000E4D74
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Union.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.ExpressionType)
				{
					if (memberName != MemberName.CurrentUnion)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_currentUnion);
					}
				}
				else
				{
					writer.WriteEnum((int)this.m_expressionType);
				}
			}
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x000E6BE0 File Offset: 0x000E4DE0
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Union.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.ExpressionType)
				{
					if (memberName != MemberName.CurrentUnion)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_currentUnion = reader.ReadVariant();
					}
				}
				else
				{
					this.m_expressionType = (DataAggregate.DataTypeCode)reader.ReadEnum();
				}
			}
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x000E6C49 File Offset: 0x000E4E49
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "Union should not resolve references");
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x000E6C5B File Offset: 0x000E4E5B
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Union;
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x000E6C64 File Offset: 0x000E4E64
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (Union.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Union, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregate, new List<MemberInfo>
				{
					new MemberInfo(MemberName.ExpressionType, Token.Enum),
					new MemberInfo(MemberName.CurrentUnion, Token.Object)
				});
			}
			return Union.m_declaration;
		}

		// Token: 0x17001766 RID: 5990
		// (get) Token: 0x0600344A RID: 13386 RVA: 0x000E6CB3 File Offset: 0x000E4EB3
		public override int Size
		{
			get
			{
				return 4 + ItemSizes.SizeOf(this.m_currentUnion);
			}
		}

		// Token: 0x040019FA RID: 6650
		protected DataAggregate.DataTypeCode m_expressionType;

		// Token: 0x040019FB RID: 6651
		protected object m_currentUnion;

		// Token: 0x040019FC RID: 6652
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = Union.GetDeclaration();
	}
}
