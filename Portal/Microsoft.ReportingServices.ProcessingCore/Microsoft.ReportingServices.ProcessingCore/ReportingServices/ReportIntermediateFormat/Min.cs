using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000466 RID: 1126
	internal sealed class Min : DataAggregate
	{
		// Token: 0x060033D7 RID: 13271 RVA: 0x000E5787 File Offset: 0x000E3987
		internal Min()
		{
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x000E578F File Offset: 0x000E398F
		internal Min(IDataComparer comparer)
		{
			this.m_currentMin = null;
			this.m_comparer = comparer;
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x000E57A5 File Offset: 0x000E39A5
		internal override void Init()
		{
			this.m_currentMin = null;
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x000E57B0 File Offset: 0x000E39B0
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
				iErrorContext.Register(ProcessingErrorCode.rsMinMaxOfNonSortableData, Severity.Warning, Array.Empty<string>());
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			if (this.m_currentMin == null)
			{
				this.m_currentMin = obj;
				this.m_expressionType = typeCode;
				return;
			}
			bool flag;
			int num = this.m_comparer.Compare(this.m_currentMin, obj, false, false, out flag);
			if (flag)
			{
				if (num > 0)
				{
					this.m_currentMin = obj;
					this.m_expressionType = typeCode;
				}
				return;
			}
			if (typeCode != this.m_expressionType)
			{
				iErrorContext.Register(ProcessingErrorCode.rsAggregateOfMixedDataTypes, Severity.Warning, Array.Empty<string>());
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			iErrorContext.Register(ProcessingErrorCode.rsMinMaxOfNonSortableData, Severity.Warning, Array.Empty<string>());
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000E5877 File Offset: 0x000E3A77
		internal override object Result()
		{
			return this.m_currentMin;
		}

		// Token: 0x17001756 RID: 5974
		// (get) Token: 0x060033DC RID: 13276 RVA: 0x000E587F File Offset: 0x000E3A7F
		internal override DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return DataAggregateInfo.AggregateTypes.Min;
			}
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x000E5882 File Offset: 0x000E3A82
		internal override DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			return new Min(odpContext.ProcessingComparer);
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x000E5890 File Offset: 0x000E3A90
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Min.m_declaration);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.ExpressionType)
				{
					if (memberName != MemberName.CurrentMin)
					{
						if (memberName != MemberName.Comparer)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							int num = scalabilityCache.StoreStaticReference(this.m_comparer);
							writer.Write(num);
						}
					}
					else
					{
						writer.Write(this.m_currentMin);
					}
				}
				else
				{
					writer.WriteEnum((int)this.m_expressionType);
				}
			}
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x000E5928 File Offset: 0x000E3B28
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Min.m_declaration);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.ExpressionType)
				{
					if (memberName != MemberName.CurrentMin)
					{
						if (memberName != MemberName.Comparer)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							int num = reader.ReadInt32();
							this.m_comparer = (ReportProcessing.ProcessingComparer)scalabilityCache.FetchStaticReference(num);
						}
					}
					else
					{
						this.m_currentMin = reader.ReadVariant();
					}
				}
				else
				{
					this.m_expressionType = (DataAggregate.DataTypeCode)reader.ReadEnum();
				}
			}
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x000E59C2 File Offset: 0x000E3BC2
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "Min should not resolve references");
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x000E59D4 File Offset: 0x000E3BD4
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Min;
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x000E59D8 File Offset: 0x000E3BD8
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (Min.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Min, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.ExpressionType, Token.Enum),
					new MemberInfo(MemberName.CurrentMin, Token.Object),
					new MemberInfo(MemberName.Comparer, Token.Int32)
				});
			}
			return Min.m_declaration;
		}

		// Token: 0x17001757 RID: 5975
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x000E5A35 File Offset: 0x000E3C35
		public override int Size
		{
			get
			{
				return 4 + ItemSizes.SizeOf(this.m_currentMin) + ItemSizes.ReferenceSize;
			}
		}

		// Token: 0x040019DC RID: 6620
		private DataAggregate.DataTypeCode m_expressionType;

		// Token: 0x040019DD RID: 6621
		private object m_currentMin;

		// Token: 0x040019DE RID: 6622
		private IDataComparer m_comparer;

		// Token: 0x040019DF RID: 6623
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = Min.GetDeclaration();
	}
}
