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
	// Token: 0x02000465 RID: 1125
	internal sealed class Max : DataAggregate
	{
		// Token: 0x060033C9 RID: 13257 RVA: 0x000E54BE File Offset: 0x000E36BE
		internal Max()
		{
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x000E54C6 File Offset: 0x000E36C6
		internal Max(IDataComparer comparer)
		{
			this.m_currentMax = null;
			this.m_comparer = comparer;
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x000E54DC File Offset: 0x000E36DC
		internal override void Init()
		{
			this.m_currentMax = null;
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x000E54E8 File Offset: 0x000E36E8
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
			if (this.m_currentMax == null)
			{
				this.m_currentMax = obj;
				this.m_expressionType = typeCode;
				return;
			}
			bool flag;
			int num = this.m_comparer.Compare(this.m_currentMax, obj, false, false, out flag);
			if (flag)
			{
				if (num < 0)
				{
					this.m_currentMax = obj;
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

		// Token: 0x060033CD RID: 13261 RVA: 0x000E55AF File Offset: 0x000E37AF
		internal override object Result()
		{
			return this.m_currentMax;
		}

		// Token: 0x17001754 RID: 5972
		// (get) Token: 0x060033CE RID: 13262 RVA: 0x000E55B7 File Offset: 0x000E37B7
		internal override DataAggregateInfo.AggregateTypes AggregateType
		{
			get
			{
				return DataAggregateInfo.AggregateTypes.Max;
			}
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x000E55BA File Offset: 0x000E37BA
		internal override DataAggregate ConstructAggregator(OnDemandProcessingContext odpContext, DataAggregateInfo aggregateDef)
		{
			return new Max(odpContext.ProcessingComparer);
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000E55C8 File Offset: 0x000E37C8
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Max.m_declaration);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.ExpressionType)
				{
					if (memberName != MemberName.CurrentMax)
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
						writer.Write(this.m_currentMax);
					}
				}
				else
				{
					writer.WriteEnum((int)this.m_expressionType);
				}
			}
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000E565C File Offset: 0x000E385C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Max.m_declaration);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.ExpressionType)
				{
					if (memberName != MemberName.CurrentMax)
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
						this.m_currentMax = reader.ReadVariant();
					}
				}
				else
				{
					this.m_expressionType = (DataAggregate.DataTypeCode)reader.ReadEnum();
				}
			}
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x000E56F3 File Offset: 0x000E38F3
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "Max should not resolve references");
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000E5705 File Offset: 0x000E3905
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Max;
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x000E570C File Offset: 0x000E390C
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (Max.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Max, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.ExpressionType, Token.Enum),
					new MemberInfo(MemberName.CurrentMax, Token.Object),
					new MemberInfo(MemberName.Comparer, Token.Int32)
				});
			}
			return Max.m_declaration;
		}

		// Token: 0x17001755 RID: 5973
		// (get) Token: 0x060033D5 RID: 13269 RVA: 0x000E5766 File Offset: 0x000E3966
		public override int Size
		{
			get
			{
				return 4 + ItemSizes.SizeOf(this.m_currentMax) + ItemSizes.ReferenceSize;
			}
		}

		// Token: 0x040019D8 RID: 6616
		private DataAggregate.DataTypeCode m_expressionType;

		// Token: 0x040019D9 RID: 6617
		private object m_currentMax;

		// Token: 0x040019DA RID: 6618
		private IDataComparer m_comparer;

		// Token: 0x040019DB RID: 6619
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = Max.GetDeclaration();
	}
}
