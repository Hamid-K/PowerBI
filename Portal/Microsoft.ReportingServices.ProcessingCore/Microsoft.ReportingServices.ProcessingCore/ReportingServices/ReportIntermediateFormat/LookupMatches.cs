using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004E7 RID: 1255
	internal class LookupMatches : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, ITransferable
	{
		// Token: 0x06003F6B RID: 16235 RVA: 0x0010CEDB File Offset: 0x0010B0DB
		internal LookupMatches()
		{
		}

		// Token: 0x17001AD5 RID: 6869
		// (get) Token: 0x06003F6C RID: 16236 RVA: 0x0010CEEE File Offset: 0x0010B0EE
		internal bool HasRow
		{
			get
			{
				return this.m_firstRowOffset != DataFieldRow.UnInitializedStreamOffset;
			}
		}

		// Token: 0x17001AD6 RID: 6870
		// (get) Token: 0x06003F6D RID: 16237 RVA: 0x0010CF00 File Offset: 0x0010B100
		internal int MatchCount
		{
			get
			{
				int num = 0;
				if (this.m_rowOffsets != null)
				{
					num = this.m_rowOffsets.Count;
				}
				if (this.HasRow)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x0010CF30 File Offset: 0x0010B130
		internal virtual void AddRow(long rowOffset, int rowIndex, IScalabilityCache scaleCache)
		{
			if (this.HasRow)
			{
				if (this.m_rowOffsets == null)
				{
					this.m_rowOffsets = new ScalableList<long>(0, scaleCache, 500, 10);
				}
				this.m_rowOffsets.Add(rowOffset);
				return;
			}
			this.m_firstRowOffset = rowOffset;
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x0010CF6C File Offset: 0x0010B16C
		internal virtual void SetupRow(int matchIndex, OnDemandProcessingContext odpContext)
		{
			long num = DataFieldRow.UnInitializedStreamOffset;
			if (matchIndex == 0)
			{
				num = this.m_firstRowOffset;
			}
			else
			{
				num = this.m_rowOffsets[matchIndex - 1];
			}
			odpContext.ReportObjectModel.UpdateFieldValues(num);
		}

		// Token: 0x17001AD7 RID: 6871
		// (get) Token: 0x06003F70 RID: 16240 RVA: 0x0010CFA6 File Offset: 0x0010B1A6
		public virtual int Size
		{
			get
			{
				return 8 + ItemSizes.SizeOf<long>(this.m_rowOffsets);
			}
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x0010CFB8 File Offset: 0x0010B1B8
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(LookupMatches.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.FirstRowOffset)
				{
					if (memberName != MemberName.RowOffsets)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_rowOffsets);
					}
				}
				else
				{
					writer.Write(this.m_firstRowOffset);
				}
			}
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x0010D024 File Offset: 0x0010B224
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(LookupMatches.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.FirstRowOffset)
				{
					if (memberName != MemberName.RowOffsets)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_rowOffsets = reader.ReadRIFObject<ScalableList<long>>();
					}
				}
				else
				{
					this.m_firstRowOffset = reader.ReadInt64();
				}
			}
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x0010D090 File Offset: 0x0010B290
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x0010D092 File Offset: 0x0010B292
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupMatches;
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x0010D09C File Offset: 0x0010B29C
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (LookupMatches.m_Declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupMatches, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.FirstRowOffset, Token.Int64),
					new MemberInfo(MemberName.RowOffsets, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList, Token.Int64)
				});
			}
			return LookupMatches.m_Declaration;
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x0010D0F4 File Offset: 0x0010B2F4
		public virtual void TransferTo(IScalabilityCache scaleCache)
		{
			if (this.m_rowOffsets != null)
			{
				this.m_rowOffsets.TransferTo(scaleCache);
			}
		}

		// Token: 0x04001D40 RID: 7488
		private long m_firstRowOffset = DataFieldRow.UnInitializedStreamOffset;

		// Token: 0x04001D41 RID: 7489
		private ScalableList<long> m_rowOffsets;

		// Token: 0x04001D42 RID: 7490
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = LookupMatches.GetDeclaration();
	}
}
