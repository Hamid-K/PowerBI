using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200045E RID: 1118
	public class DataAggregateObjResult : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600336D RID: 13165 RVA: 0x000E4240 File Offset: 0x000E2440
		internal DataAggregateObjResult()
		{
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x000E4248 File Offset: 0x000E2448
		internal DataAggregateObjResult(DataAggregateObjResult original)
		{
			this.ErrorOccurred = original.ErrorOccurred;
			this.HasCode = original.HasCode;
			this.Code = original.Code;
			this.Severity = original.Severity;
			this.FieldStatus = original.FieldStatus;
			DataAggregateObjResult.CloneHelperStruct cloneHelperStruct = new DataAggregateObjResult.CloneHelperStruct(original.Value);
			this.Value = cloneHelperStruct.Value;
			this.Arguments = original.Arguments;
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x000E42BC File Offset: 0x000E24BC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjResult, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ErrorOccurred, Token.Boolean),
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Variant),
				new MemberInfo(MemberName.HasCode, Token.Boolean),
				new MemberInfo(MemberName.Code, Token.Enum),
				new MemberInfo(MemberName.Severity, Token.Enum),
				new MemberInfo(MemberName.FieldStatus, Token.Enum),
				new MemberInfo(MemberName.Arguments, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Token.String)
			});
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x000E435C File Offset: 0x000E255C
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(DataAggregateObjResult.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Code)
				{
					if (memberName == MemberName.FieldStatus)
					{
						writer.WriteEnum((int)this.FieldStatus);
						continue;
					}
					if (memberName == MemberName.Value)
					{
						writer.Write(this.Value);
						continue;
					}
					if (memberName == MemberName.Code)
					{
						writer.WriteEnum((int)this.Code);
						continue;
					}
				}
				else if (memberName <= MemberName.ErrorOccurred)
				{
					if (memberName == MemberName.Severity)
					{
						writer.WriteEnum((int)this.Severity);
						continue;
					}
					if (memberName == MemberName.ErrorOccurred)
					{
						writer.Write(this.ErrorOccurred);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.HasCode)
					{
						writer.Write(this.HasCode);
						continue;
					}
					if (memberName == MemberName.Arguments)
					{
						writer.Write(this.Arguments);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x000E4454 File Offset: 0x000E2654
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(DataAggregateObjResult.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Code)
				{
					if (memberName == MemberName.FieldStatus)
					{
						this.FieldStatus = (DataFieldStatus)reader.ReadEnum();
						continue;
					}
					if (memberName == MemberName.Value)
					{
						this.Value = reader.ReadVariant();
						continue;
					}
					if (memberName == MemberName.Code)
					{
						this.Code = (ProcessingErrorCode)reader.ReadEnum();
						continue;
					}
				}
				else if (memberName <= MemberName.ErrorOccurred)
				{
					if (memberName == MemberName.Severity)
					{
						this.Severity = (Severity)reader.ReadEnum();
						continue;
					}
					if (memberName == MemberName.ErrorOccurred)
					{
						this.ErrorOccurred = reader.ReadBoolean();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.HasCode)
					{
						this.HasCode = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.Arguments)
					{
						this.Arguments = reader.ReadStringArray();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x000E454C File Offset: 0x000E274C
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x000E4559 File Offset: 0x000E2759
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjResult;
		}

		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x06003374 RID: 13172 RVA: 0x000E4560 File Offset: 0x000E2760
		public int Size
		{
			get
			{
				return 1 + ItemSizes.SizeOf(this.Value) + 1 + 4 + 4 + ItemSizes.SizeOf(this.Arguments) + 4;
			}
		}

		// Token: 0x040019BC RID: 6588
		internal bool ErrorOccurred;

		// Token: 0x040019BD RID: 6589
		internal object Value;

		// Token: 0x040019BE RID: 6590
		internal bool HasCode;

		// Token: 0x040019BF RID: 6591
		internal ProcessingErrorCode Code;

		// Token: 0x040019C0 RID: 6592
		internal Severity Severity;

		// Token: 0x040019C1 RID: 6593
		internal string[] Arguments;

		// Token: 0x040019C2 RID: 6594
		internal DataFieldStatus FieldStatus;

		// Token: 0x040019C3 RID: 6595
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DataAggregateObjResult.GetDeclaration();

		// Token: 0x0200096C RID: 2412
		private struct CloneHelperStruct
		{
			// Token: 0x06008059 RID: 32857 RVA: 0x00210DF2 File Offset: 0x0020EFF2
			internal CloneHelperStruct(object value)
			{
				this.Value = value;
			}

			// Token: 0x040040BD RID: 16573
			internal object Value;
		}
	}
}
