using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008BF RID: 2239
	[PersistedWithinRequestOnly]
	internal sealed class AggregateRow : DataFieldRow
	{
		// Token: 0x06007AA1 RID: 31393 RVA: 0x001F94D4 File Offset: 0x001F76D4
		internal AggregateRow()
		{
		}

		// Token: 0x06007AA2 RID: 31394 RVA: 0x001F94DC File Offset: 0x001F76DC
		internal AggregateRow(FieldsImpl fields, bool getAndSave)
			: base(fields, getAndSave)
		{
			this.m_isAggregateRow = fields.IsAggregateRow;
			this.m_aggregationFieldCount = fields.AggregationFieldCount;
			this.m_validAggregateRow = fields.ValidAggregateRow;
		}

		// Token: 0x06007AA3 RID: 31395 RVA: 0x001F950A File Offset: 0x001F770A
		internal override void SetFields(FieldsImpl fields)
		{
			fields.SetFields(this.m_fields, this.m_streamOffset, this.m_isAggregateRow, this.m_aggregationFieldCount, this.m_validAggregateRow);
		}

		// Token: 0x06007AA4 RID: 31396 RVA: 0x001F9530 File Offset: 0x001F7730
		internal override void RestoreDataSetAndSetFields(OnDemandProcessingContext odpContext, FieldsContext fieldsContext)
		{
			base.RestoreDataSetAndSetFields(odpContext, fieldsContext);
			if (this.m_aggregateInfo != null)
			{
				this.m_aggregateInfo.RestoreAggregateInfo(odpContext);
			}
		}

		// Token: 0x06007AA5 RID: 31397 RVA: 0x001F954E File Offset: 0x001F774E
		internal override void SaveAggregateInfo(OnDemandProcessingContext odpContext)
		{
			this.m_aggregateInfo = new AggregateRowInfo();
			this.m_aggregateInfo.SaveAggregateInfo(odpContext);
		}

		// Token: 0x06007AA6 RID: 31398 RVA: 0x001F9568 File Offset: 0x001F7768
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(AggregateRow.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.IsAggregateRow)
				{
					if (memberName != MemberName.AggregationFieldCount)
					{
						if (memberName != MemberName.ValidAggregateRow)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_validAggregateRow);
						}
					}
					else
					{
						writer.Write(this.m_aggregationFieldCount);
					}
				}
				else
				{
					writer.Write(this.m_isAggregateRow);
				}
			}
		}

		// Token: 0x06007AA7 RID: 31399 RVA: 0x001F95EC File Offset: 0x001F77EC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(AggregateRow.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.IsAggregateRow)
				{
					if (memberName != MemberName.AggregationFieldCount)
					{
						if (memberName != MemberName.ValidAggregateRow)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_validAggregateRow = reader.ReadBoolean();
						}
					}
					else
					{
						this.m_aggregationFieldCount = reader.ReadInt32();
					}
				}
				else
				{
					this.m_isAggregateRow = reader.ReadBoolean();
				}
			}
		}

		// Token: 0x06007AA8 RID: 31400 RVA: 0x001F966E File Offset: 0x001F786E
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007AA9 RID: 31401 RVA: 0x001F9670 File Offset: 0x001F7870
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.AggregateRow;
		}

		// Token: 0x06007AAA RID: 31402 RVA: 0x001F9674 File Offset: 0x001F7874
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (AggregateRow.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.AggregateRow, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataFieldRow, new List<MemberInfo>
				{
					new MemberInfo(MemberName.IsAggregateRow, Token.Boolean),
					new MemberInfo(MemberName.AggregationFieldCount, Token.Int32),
					new MemberInfo(MemberName.ValidAggregateRow, Token.Boolean)
				});
			}
			return AggregateRow.m_declaration;
		}

		// Token: 0x1700285C RID: 10332
		// (get) Token: 0x06007AAB RID: 31403 RVA: 0x001F96D5 File Offset: 0x001F78D5
		public override int Size
		{
			get
			{
				return base.Size + 1 + 4 + 1;
			}
		}

		// Token: 0x04003D48 RID: 15688
		[NonSerialized]
		private AggregateRowInfo m_aggregateInfo;

		// Token: 0x04003D49 RID: 15689
		private bool m_isAggregateRow;

		// Token: 0x04003D4A RID: 15690
		private int m_aggregationFieldCount;

		// Token: 0x04003D4B RID: 15691
		private bool m_validAggregateRow;

		// Token: 0x04003D4C RID: 15692
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = AggregateRow.GetDeclaration();
	}
}
