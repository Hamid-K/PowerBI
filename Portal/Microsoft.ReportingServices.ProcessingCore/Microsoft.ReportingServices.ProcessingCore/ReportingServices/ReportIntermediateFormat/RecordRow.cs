using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004F5 RID: 1269
	[Serializable]
	public sealed class RecordRow : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06004089 RID: 16521 RVA: 0x00110533 File Offset: 0x0010E733
		internal RecordRow()
		{
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x0011053C File Offset: 0x0010E73C
		internal RecordRow(FieldsImpl fields, int fieldCount, FieldInfo[] fieldInfos)
		{
			this.m_recordFields = new RecordField[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				if (!fields[i].IsMissing)
				{
					FieldInfo fieldInfo = null;
					if (fieldInfos != null && i < fieldInfos.Length)
					{
						fieldInfo = fieldInfos[i];
					}
					this.m_recordFields[i] = new RecordField(fields[i], fieldInfo);
				}
			}
			this.m_isAggregateRow = fields.IsAggregateRow;
			this.m_aggregationFieldCount = fields.AggregationFieldCount;
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x001105B1 File Offset: 0x0010E7B1
		internal RecordRow(RecordRow original, int[] mappingDataSetFieldIndexesToDataChunk)
		{
			this.m_streamPosition = original.m_streamPosition;
			this.m_isAggregateRow = original.m_isAggregateRow;
			this.m_recordFields = original.m_recordFields;
			this.ApplyFieldMapping(mappingDataSetFieldIndexesToDataChunk);
		}

		// Token: 0x17001B34 RID: 6964
		// (get) Token: 0x0600408C RID: 16524 RVA: 0x001105E4 File Offset: 0x0010E7E4
		// (set) Token: 0x0600408D RID: 16525 RVA: 0x001105EC File Offset: 0x0010E7EC
		internal RecordField[] RecordFields
		{
			get
			{
				return this.m_recordFields;
			}
			set
			{
				this.m_recordFields = value;
			}
		}

		// Token: 0x17001B35 RID: 6965
		// (get) Token: 0x0600408E RID: 16526 RVA: 0x001105F5 File Offset: 0x0010E7F5
		// (set) Token: 0x0600408F RID: 16527 RVA: 0x001105FD File Offset: 0x0010E7FD
		internal bool IsAggregateRow
		{
			get
			{
				return this.m_isAggregateRow;
			}
			set
			{
				this.m_isAggregateRow = value;
			}
		}

		// Token: 0x17001B36 RID: 6966
		// (get) Token: 0x06004090 RID: 16528 RVA: 0x00110606 File Offset: 0x0010E806
		// (set) Token: 0x06004091 RID: 16529 RVA: 0x0011060E File Offset: 0x0010E80E
		internal int AggregationFieldCount
		{
			get
			{
				return this.m_aggregationFieldCount;
			}
			set
			{
				this.m_aggregationFieldCount = value;
			}
		}

		// Token: 0x17001B37 RID: 6967
		// (get) Token: 0x06004092 RID: 16530 RVA: 0x00110617 File Offset: 0x0010E817
		// (set) Token: 0x06004093 RID: 16531 RVA: 0x0011061F File Offset: 0x0010E81F
		internal long StreamPosition
		{
			get
			{
				return this.m_streamPosition;
			}
			set
			{
				this.m_streamPosition = value;
			}
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x00110628 File Offset: 0x0010E828
		internal void ApplyFieldMapping(int[] mappingDataSetFieldIndexesToDataChunk)
		{
			if (mappingDataSetFieldIndexesToDataChunk == null)
			{
				return;
			}
			RecordField[] recordFields = this.m_recordFields;
			this.m_recordFields = new RecordField[mappingDataSetFieldIndexesToDataChunk.Length];
			this.m_aggregationFieldCount = 0;
			for (int i = 0; i < mappingDataSetFieldIndexesToDataChunk.Length; i++)
			{
				if (mappingDataSetFieldIndexesToDataChunk[i] >= 0)
				{
					this.m_recordFields[i] = recordFields[mappingDataSetFieldIndexesToDataChunk[i]];
					if (this.m_recordFields[i] != null && this.m_recordFields[i].IsAggregationField)
					{
						this.m_aggregationFieldCount++;
					}
				}
			}
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x001106A0 File Offset: 0x0010E8A0
		internal object GetFieldValue(int aliasIndex)
		{
			RecordField recordField = this.m_recordFields[aliasIndex];
			if (recordField == null)
			{
				throw new ReportProcessingException_FieldError(DataFieldStatus.IsMissing, null);
			}
			if (recordField.FieldStatus != DataFieldStatus.None)
			{
				throw new ReportProcessingException_FieldError(recordField.FieldStatus, Microsoft.ReportingServices.RdlExpressions.ReportRuntime.GetErrorName(recordField.FieldStatus, null));
			}
			return recordField.FieldValue;
		}

		// Token: 0x06004096 RID: 16534 RVA: 0x001106E7 File Offset: 0x0010E8E7
		internal bool IsAggregationField(int aliasIndex)
		{
			return this.m_recordFields[aliasIndex].IsAggregationField;
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x001106F8 File Offset: 0x0010E8F8
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RecordRow, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.RecordFields, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RecordField),
				new MemberInfo(MemberName.IsAggregateRow, Token.Boolean),
				new MemberInfo(MemberName.AggregationFieldCount, Token.Int32)
			});
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x00110748 File Offset: 0x0010E948
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RecordRow.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.RecordFields:
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] recordFields = this.m_recordFields;
					writer.Write(recordFields);
					break;
				}
				case MemberName.IsAggregateRow:
					writer.Write(this.m_isAggregateRow);
					break;
				case MemberName.AggregationFieldCount:
					writer.Write(this.m_aggregationFieldCount);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x001107CC File Offset: 0x0010E9CC
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			this.m_streamPosition = reader.ObjectStartPosition;
			reader.RegisterDeclaration(RecordRow.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.RecordFields:
					this.m_recordFields = reader.ReadArrayOfRIFObjects<RecordField>();
					break;
				case MemberName.IsAggregateRow:
					this.m_isAggregateRow = reader.ReadBoolean();
					break;
				case MemberName.AggregationFieldCount:
					this.m_aggregationFieldCount = reader.ReadInt32();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x00110858 File Offset: 0x0010EA58
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RecordRow;
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x0011085C File Offset: 0x0010EA5C
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x04001DA1 RID: 7585
		private RecordField[] m_recordFields;

		// Token: 0x04001DA2 RID: 7586
		private bool m_isAggregateRow;

		// Token: 0x04001DA3 RID: 7587
		private int m_aggregationFieldCount;

		// Token: 0x04001DA4 RID: 7588
		[NonSerialized]
		private long m_streamPosition;

		// Token: 0x04001DA5 RID: 7589
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = RecordRow.GetDeclaration();
	}
}
