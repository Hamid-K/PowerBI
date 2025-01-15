using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008BE RID: 2238
	[PersistedWithinRequestOnly]
	public class DataFieldRow : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007A93 RID: 31379 RVA: 0x001F92F1 File Offset: 0x001F74F1
		internal DataFieldRow()
		{
		}

		// Token: 0x06007A94 RID: 31380 RVA: 0x001F9304 File Offset: 0x001F7504
		internal DataFieldRow(FieldsImpl fields, bool getAndSave)
		{
			if (getAndSave)
			{
				this.m_fields = fields.GetAndSaveFields();
			}
			else
			{
				this.m_fields = fields.GetFields();
			}
			this.m_streamOffset = fields.StreamOffset;
		}

		// Token: 0x06007A95 RID: 31381 RVA: 0x001F9340 File Offset: 0x001F7540
		internal virtual void SetFields(FieldsImpl fields)
		{
			fields.SetFields(this.m_fields, this.m_streamOffset);
		}

		// Token: 0x06007A96 RID: 31382 RVA: 0x001F9354 File Offset: 0x001F7554
		internal virtual void RestoreDataSetAndSetFields(OnDemandProcessingContext odpContext, FieldsContext fieldsContext)
		{
			odpContext.ReportObjectModel.RestoreFields(fieldsContext);
			this.SetFields(odpContext.ReportObjectModel.FieldsImpl);
		}

		// Token: 0x06007A97 RID: 31383 RVA: 0x001F9373 File Offset: 0x001F7573
		internal virtual void SaveAggregateInfo(OnDemandProcessingContext odpContext)
		{
		}

		// Token: 0x17002859 RID: 10329
		internal FieldImpl this[int index]
		{
			get
			{
				return this.m_fields[index];
			}
		}

		// Token: 0x1700285A RID: 10330
		// (get) Token: 0x06007A99 RID: 31385 RVA: 0x001F937F File Offset: 0x001F757F
		internal long StreamOffset
		{
			get
			{
				return this.m_streamOffset;
			}
		}

		// Token: 0x06007A9A RID: 31386 RVA: 0x001F9388 File Offset: 0x001F7588
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(DataFieldRow.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Fields)
				{
					if (memberName != MemberName.Offset)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_streamOffset);
					}
				}
				else
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] fields = this.m_fields;
					writer.Write(fields);
				}
			}
		}

		// Token: 0x06007A9B RID: 31387 RVA: 0x001F93F4 File Offset: 0x001F75F4
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(DataFieldRow.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Fields)
				{
					if (memberName != MemberName.Offset)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_streamOffset = reader.ReadInt64();
					}
				}
				else
				{
					this.m_fields = reader.ReadArrayOfRIFObjects<FieldImpl>();
				}
			}
		}

		// Token: 0x06007A9C RID: 31388 RVA: 0x001F945D File Offset: 0x001F765D
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007A9D RID: 31389 RVA: 0x001F945F File Offset: 0x001F765F
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataFieldRow;
		}

		// Token: 0x06007A9E RID: 31390 RVA: 0x001F9464 File Offset: 0x001F7664
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (DataFieldRow.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataFieldRow, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Fields, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FieldImpl),
					new MemberInfo(MemberName.Offset, Token.Int64)
				});
			}
			return DataFieldRow.m_declaration;
		}

		// Token: 0x1700285B RID: 10331
		// (get) Token: 0x06007A9F RID: 31391 RVA: 0x001F94B2 File Offset: 0x001F76B2
		public virtual int Size
		{
			get
			{
				return ItemSizes.SizeOf<FieldImpl>(this.m_fields) + 8;
			}
		}

		// Token: 0x04003D44 RID: 15684
		protected FieldImpl[] m_fields;

		// Token: 0x04003D45 RID: 15685
		protected long m_streamOffset = DataFieldRow.UnInitializedStreamOffset;

		// Token: 0x04003D46 RID: 15686
		internal static readonly long UnInitializedStreamOffset = -1L;

		// Token: 0x04003D47 RID: 15687
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = DataFieldRow.GetDeclaration();
	}
}
