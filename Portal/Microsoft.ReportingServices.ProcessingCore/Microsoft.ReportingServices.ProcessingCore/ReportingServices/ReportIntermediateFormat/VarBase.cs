using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200046A RID: 1130
	internal abstract class VarBase : DataAggregate
	{
		// Token: 0x0600340C RID: 13324 RVA: 0x000E5E7E File Offset: 0x000E407E
		internal override void Init()
		{
			this.m_currentCount = 0U;
			this.m_sumOfXType = DataAggregate.DataTypeCode.Null;
			this.m_sumOfX = null;
			this.m_sumOfXSquared = null;
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x000E5E9C File Offset: 0x000E409C
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
			object obj2 = DataAggregate.Square(typeCode, obj);
			if (this.m_sumOfX == null)
			{
				this.m_sumOfXType = typeCode;
				this.m_sumOfX = obj;
				this.m_sumOfXSquared = obj2;
			}
			else
			{
				this.m_sumOfX = DataAggregate.Add(this.m_sumOfXType, this.m_sumOfX, typeCode, obj);
				this.m_sumOfXSquared = DataAggregate.Add(this.m_sumOfXType, this.m_sumOfXSquared, typeCode, obj2);
			}
			this.m_currentCount += 1U;
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x000E5F8C File Offset: 0x000E418C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(VarBase.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.ExpressionType)
				{
					switch (memberName)
					{
					case MemberName.CurrentCount:
						writer.Write(this.m_currentCount);
						continue;
					case MemberName.SumOfXType:
						writer.WriteEnum((int)this.m_sumOfXType);
						continue;
					case MemberName.SumOfX:
						writer.Write(this.m_sumOfX);
						continue;
					case MemberName.SumOfXSquared:
						writer.Write(this.m_sumOfXSquared);
						continue;
					}
					Global.Tracer.Assert(false);
				}
				else
				{
					writer.WriteEnum((int)this.m_expressionType);
				}
			}
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x000E6048 File Offset: 0x000E4248
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(VarBase.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.ExpressionType)
				{
					switch (memberName)
					{
					case MemberName.CurrentCount:
						this.m_currentCount = reader.ReadUInt32();
						continue;
					case MemberName.SumOfXType:
						this.m_sumOfXType = (DataAggregate.DataTypeCode)reader.ReadEnum();
						continue;
					case MemberName.SumOfX:
						this.m_sumOfX = reader.ReadVariant();
						continue;
					case MemberName.SumOfXSquared:
						this.m_sumOfXSquared = reader.ReadVariant();
						continue;
					}
					Global.Tracer.Assert(false);
				}
				else
				{
					this.m_expressionType = (DataAggregate.DataTypeCode)reader.ReadEnum();
				}
			}
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x000E6101 File Offset: 0x000E4301
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "VarBase should not resolve references");
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x000E6113 File Offset: 0x000E4313
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VarBase;
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x000E6118 File Offset: 0x000E4318
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (VarBase.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VarBase, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.ExpressionType, Token.Enum),
					new MemberInfo(MemberName.CurrentCount, Token.UInt32),
					new MemberInfo(MemberName.SumOfXType, Token.Enum),
					new MemberInfo(MemberName.SumOfX, Token.Object),
					new MemberInfo(MemberName.SumOfXSquared, Token.Object)
				});
			}
			return VarBase.m_declaration;
		}

		// Token: 0x1700175E RID: 5982
		// (get) Token: 0x06003413 RID: 13331 RVA: 0x000E618B File Offset: 0x000E438B
		public override int Size
		{
			get
			{
				return 12 + ItemSizes.SizeOf(this.m_sumOfX) + ItemSizes.SizeOf(this.m_sumOfXSquared);
			}
		}

		// Token: 0x040019E6 RID: 6630
		private DataAggregate.DataTypeCode m_expressionType;

		// Token: 0x040019E7 RID: 6631
		protected uint m_currentCount;

		// Token: 0x040019E8 RID: 6632
		protected DataAggregate.DataTypeCode m_sumOfXType;

		// Token: 0x040019E9 RID: 6633
		protected object m_sumOfX;

		// Token: 0x040019EA RID: 6634
		protected object m_sumOfXSquared;

		// Token: 0x040019EB RID: 6635
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = VarBase.GetDeclaration();
	}
}
