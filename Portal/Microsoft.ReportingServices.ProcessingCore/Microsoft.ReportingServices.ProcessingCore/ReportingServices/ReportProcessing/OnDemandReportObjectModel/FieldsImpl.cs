using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007AE RID: 1966
	internal sealed class FieldsImpl : Fields
	{
		// Token: 0x06006F78 RID: 28536 RVA: 0x001D1220 File Offset: 0x001CF420
		internal FieldsImpl(ObjectModelImpl reportOM, int size, bool addRowIndex, bool noRows)
		{
			this.m_reportOM = reportOM;
			if (addRowIndex)
			{
				this.m_collection = new FieldImpl[size + 1];
			}
			else
			{
				this.m_collection = new FieldImpl[size];
			}
			this.m_nameMap = new Hashtable(size);
			this.m_fieldMissing = null;
			this.m_count = 0;
			this.m_referenced = false;
			this.m_readerExtensionsSupported = false;
			this.m_isAggregateRow = false;
			this.m_aggregationFieldCount = size;
			this.m_aggregationFieldCountForDetailRow = size;
			this.m_noRows = noRows;
			this.m_validAggregateRow = true;
			this.m_addRowIndex = addRowIndex;
		}

		// Token: 0x06006F79 RID: 28537 RVA: 0x001D12B8 File Offset: 0x001CF4B8
		internal FieldsImpl(ObjectModelImpl reportOM)
		{
			this.m_reportOM = reportOM;
			this.m_collection = null;
			this.m_nameMap = null;
			this.m_fieldMissing = null;
			this.m_count = 0;
			this.m_referenced = false;
			this.m_readerExtensionsSupported = false;
			this.m_isAggregateRow = false;
			this.m_aggregationFieldCount = 0;
			this.m_aggregationFieldCountForDetailRow = 0;
			this.m_noRows = true;
			this.m_validAggregateRow = true;
			this.m_addRowIndex = false;
		}

		// Token: 0x170025F0 RID: 9712
		public override Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.Field this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ReportProcessingException_NonExistingFieldReference(key);
				}
				this.ValidateFieldCollection();
				Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.Field field;
				try
				{
					int num = (int)this.m_nameMap[key];
					field = this.CheckedGetFieldByIndex(num);
				}
				catch (Exception ex)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex))
					{
						throw;
					}
					throw new ReportProcessingException_NonExistingFieldReference(key);
				}
				return field;
			}
		}

		// Token: 0x170025F1 RID: 9713
		// (get) Token: 0x06006F7B RID: 28539 RVA: 0x001D1390 File Offset: 0x001CF590
		internal bool IsCollectionInitialized
		{
			get
			{
				return this.m_collection != null;
			}
		}

		// Token: 0x06006F7C RID: 28540 RVA: 0x001D139B File Offset: 0x001CF59B
		internal FieldImpl GetFieldByIndex(int index)
		{
			return this.m_collection[index];
		}

		// Token: 0x06006F7D RID: 28541 RVA: 0x001D13A8 File Offset: 0x001CF5A8
		private FieldImpl CheckedGetFieldByIndex(int index)
		{
			FieldImpl fieldImpl;
			try
			{
				if (this.m_collection[index] == null || this.m_collection[index].IsCalculatedField)
				{
					this.m_reportOM.PerformPendingFieldValueUpdate();
				}
				fieldImpl = this.m_collection[index];
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				throw new ReportProcessingException_NonExistingFieldReference();
			}
			return fieldImpl;
		}

		// Token: 0x170025F2 RID: 9714
		internal FieldImpl this[int index]
		{
			get
			{
				this.ValidateFieldCollection();
				return this.CheckedGetFieldByIndex(index);
			}
			set
			{
				Global.Tracer.Assert(this.m_collection != null, "(null != m_collection)");
				this.m_collection[index] = value;
			}
		}

		// Token: 0x170025F3 RID: 9715
		// (get) Token: 0x06006F80 RID: 28544 RVA: 0x001D1437 File Offset: 0x001CF637
		internal int Count
		{
			get
			{
				return this.m_count - ((this.m_addRowIndex > false) ? 1 : 0);
			}
		}

		// Token: 0x170025F4 RID: 9716
		// (get) Token: 0x06006F81 RID: 28545 RVA: 0x001D1449 File Offset: 0x001CF649
		internal int CountWithRowIndex
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x170025F5 RID: 9717
		// (get) Token: 0x06006F82 RID: 28546 RVA: 0x001D1451 File Offset: 0x001CF651
		// (set) Token: 0x06006F83 RID: 28547 RVA: 0x001D1459 File Offset: 0x001CF659
		internal bool ReaderExtensionsSupported
		{
			get
			{
				return this.m_readerExtensionsSupported;
			}
			set
			{
				this.m_readerExtensionsSupported = value;
			}
		}

		// Token: 0x170025F6 RID: 9718
		// (get) Token: 0x06006F84 RID: 28548 RVA: 0x001D1462 File Offset: 0x001CF662
		// (set) Token: 0x06006F85 RID: 28549 RVA: 0x001D146A File Offset: 0x001CF66A
		internal bool ReaderFieldProperties
		{
			get
			{
				return this.m_readerFieldProperties;
			}
			set
			{
				this.m_readerFieldProperties = value;
			}
		}

		// Token: 0x170025F7 RID: 9719
		// (get) Token: 0x06006F86 RID: 28550 RVA: 0x001D1473 File Offset: 0x001CF673
		// (set) Token: 0x06006F87 RID: 28551 RVA: 0x001D147B File Offset: 0x001CF67B
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

		// Token: 0x170025F8 RID: 9720
		// (get) Token: 0x06006F88 RID: 28552 RVA: 0x001D1484 File Offset: 0x001CF684
		// (set) Token: 0x06006F89 RID: 28553 RVA: 0x001D148C File Offset: 0x001CF68C
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

		// Token: 0x170025F9 RID: 9721
		// (set) Token: 0x06006F8A RID: 28554 RVA: 0x001D1495 File Offset: 0x001CF695
		internal int AggregationFieldCountForDetailRow
		{
			set
			{
				this.m_aggregationFieldCountForDetailRow = value;
			}
		}

		// Token: 0x170025FA RID: 9722
		// (get) Token: 0x06006F8B RID: 28555 RVA: 0x001D149E File Offset: 0x001CF69E
		// (set) Token: 0x06006F8C RID: 28556 RVA: 0x001D14A6 File Offset: 0x001CF6A6
		internal bool ValidAggregateRow
		{
			get
			{
				return this.m_validAggregateRow;
			}
			set
			{
				this.m_validAggregateRow = value;
			}
		}

		// Token: 0x170025FB RID: 9723
		// (get) Token: 0x06006F8D RID: 28557 RVA: 0x001D14AF File Offset: 0x001CF6AF
		internal bool AddRowIndex
		{
			get
			{
				return this.m_addRowIndex;
			}
		}

		// Token: 0x170025FC RID: 9724
		// (get) Token: 0x06006F8E RID: 28558 RVA: 0x001D14B7 File Offset: 0x001CF6B7
		// (set) Token: 0x06006F8F RID: 28559 RVA: 0x001D14BF File Offset: 0x001CF6BF
		internal bool NeedsInlineSetup
		{
			get
			{
				return this.m_needsInlineSetup;
			}
			set
			{
				this.m_needsInlineSetup = value;
			}
		}

		// Token: 0x170025FD RID: 9725
		// (get) Token: 0x06006F90 RID: 28560 RVA: 0x001D14C8 File Offset: 0x001CF6C8
		internal long StreamOffset
		{
			get
			{
				return this.m_streamOffset;
			}
		}

		// Token: 0x06006F91 RID: 28561 RVA: 0x001D14D0 File Offset: 0x001CF6D0
		internal void Add(string name, FieldImpl field)
		{
			Global.Tracer.Assert(this.m_collection != null, "(null != m_collection)");
			Global.Tracer.Assert(this.m_nameMap != null, "(null != m_nameMap)");
			Global.Tracer.Assert(this.m_count < this.m_collection.Length, "(m_count < m_collection.Length)");
			this.m_nameMap.Add(name, this.m_count);
			this.m_collection[this.m_count] = field;
			this.m_count++;
		}

		// Token: 0x06006F92 RID: 28562 RVA: 0x001D1560 File Offset: 0x001CF760
		internal void AddRowIndexField()
		{
			Global.Tracer.Assert(this.m_collection != null, "(null != m_collection)");
			Global.Tracer.Assert(this.m_count < this.m_collection.Length, "(m_count < m_collection.Length)");
			this.m_collection[this.m_count] = null;
			this.m_count++;
		}

		// Token: 0x06006F93 RID: 28563 RVA: 0x001D15C0 File Offset: 0x001CF7C0
		internal void SetFieldIsMissing(int index)
		{
			if (this.m_fieldMissing == null)
			{
				this.m_fieldMissing = new bool[this.m_collection.Length];
			}
			this.m_fieldMissing[index] = true;
		}

		// Token: 0x06006F94 RID: 28564 RVA: 0x001D15E6 File Offset: 0x001CF7E6
		internal bool IsFieldMissing(int index)
		{
			return this.m_fieldMissing != null && this.m_fieldMissing[index];
		}

		// Token: 0x06006F95 RID: 28565 RVA: 0x001D15FA File Offset: 0x001CF7FA
		internal void SetFieldErrorRegistered(int index)
		{
			if (this.m_fieldError == null)
			{
				this.m_fieldError = new bool[this.m_collection.Length];
			}
			this.m_fieldError[index] = true;
		}

		// Token: 0x06006F96 RID: 28566 RVA: 0x001D1620 File Offset: 0x001CF820
		internal bool IsFieldErrorRegistered(int index)
		{
			return this.m_fieldError != null && this.m_fieldError[index];
		}

		// Token: 0x06006F97 RID: 28567 RVA: 0x001D1634 File Offset: 0x001CF834
		internal void NewRow()
		{
			this.NewRow(DataFieldRow.UnInitializedStreamOffset);
		}

		// Token: 0x06006F98 RID: 28568 RVA: 0x001D1641 File Offset: 0x001CF841
		internal void NewRow(long streamOffset)
		{
			this.m_noRows = false;
			this.m_validAggregateRow = true;
			this.m_streamOffset = streamOffset;
			if (this.m_referenced)
			{
				this.m_collection = new FieldImpl[this.m_count];
				this.m_referenced = false;
			}
		}

		// Token: 0x06006F99 RID: 28569 RVA: 0x001D1678 File Offset: 0x001CF878
		internal void SetRowIndex(int rowIndex)
		{
			Global.Tracer.Assert(this.m_addRowIndex, "(m_addRowIndex)");
			Global.Tracer.Assert(this.m_count > 0, "(m_count > 0)");
			this.m_collection[this.m_count - 1] = new FieldImpl(this.m_reportOM, rowIndex, false, null);
		}

		// Token: 0x06006F9A RID: 28570 RVA: 0x001D16D4 File Offset: 0x001CF8D4
		internal void SetFields(FieldImpl[] fields, long streamOffset)
		{
			bool flag = this.m_referenced || streamOffset == DataFieldRow.UnInitializedStreamOffset || this.m_streamOffset != streamOffset;
			this.NewRow(streamOffset);
			if (this.m_collection == null)
			{
				Global.Tracer.Assert(false, "Invalid FieldsImpl.  m_collection should not be null.");
			}
			if (fields == null)
			{
				for (int i = 0; i < this.m_count; i++)
				{
					FieldImpl fieldImpl = this.m_collection[i];
					Microsoft.ReportingServices.ReportIntermediateFormat.Field field = ((fieldImpl == null) ? null : fieldImpl.FieldDef);
					this.m_collection[i] = new FieldImpl(this.m_reportOM, null, false, field);
				}
				return;
			}
			if (flag)
			{
				if (fields.Length != this.m_count)
				{
					Global.Tracer.Assert(false, "Wrong number of fields during ReportOM update.  Usually this means the data set is wrong.  Expected: {0}.  Actual: {1}", new object[] { this.m_count, fields.Length });
				}
				for (int j = 0; j < this.m_count; j++)
				{
					this.m_collection[j] = fields[j];
				}
				this.m_isAggregateRow = false;
				this.m_aggregationFieldCount = this.m_aggregationFieldCountForDetailRow;
			}
		}

		// Token: 0x06006F9B RID: 28571 RVA: 0x001D17D3 File Offset: 0x001CF9D3
		internal void SetFields(FieldImpl[] fields, long streamOffset, bool isAggregateRow, int aggregationFieldCount, bool validAggregateRow)
		{
			this.SetFields(fields, streamOffset);
			this.m_isAggregateRow = isAggregateRow;
			this.m_aggregationFieldCount = aggregationFieldCount;
			this.m_validAggregateRow = validAggregateRow;
		}

		// Token: 0x06006F9C RID: 28572 RVA: 0x001D17F4 File Offset: 0x001CF9F4
		internal FieldImpl[] GetAndSaveFields()
		{
			Global.Tracer.Assert(this.m_collection != null, "(null != m_collection)");
			this.m_referenced = true;
			return this.m_collection;
		}

		// Token: 0x06006F9D RID: 28573 RVA: 0x001D181B File Offset: 0x001CFA1B
		internal FieldImpl[] GetFields()
		{
			return this.m_collection;
		}

		// Token: 0x06006F9E RID: 28574 RVA: 0x001D1823 File Offset: 0x001CFA23
		internal int GetRowIndex()
		{
			return (int)this.m_collection[this.m_count - 1].Value;
		}

		// Token: 0x06006F9F RID: 28575 RVA: 0x001D1840 File Offset: 0x001CFA40
		internal void Clone(FieldsImpl fields)
		{
			if (fields != null)
			{
				this.m_collection = fields.m_collection;
				this.m_nameMap = fields.m_nameMap;
				this.m_count = fields.m_count;
				this.m_referenced = fields.m_referenced;
				this.m_noRows = fields.m_noRows;
				this.m_fieldMissing = fields.m_fieldMissing;
			}
		}

		// Token: 0x06006FA0 RID: 28576 RVA: 0x001D1898 File Offset: 0x001CFA98
		private bool ValidateFieldCollection()
		{
			if (this.m_needsInlineSetup)
			{
				this.m_needsInlineSetup = false;
				this.m_reportOM.OdpContext.PrepareFieldsCollectionForDirectFields();
			}
			if (this.m_nameMap == null || this.m_collection == null)
			{
				throw new ReportProcessingException_NonExistingFieldReference();
			}
			if (this.m_noRows)
			{
				throw new ReportProcessingException_NoRowsFieldAccess();
			}
			return true;
		}

		// Token: 0x06006FA1 RID: 28577 RVA: 0x001D18EC File Offset: 0x001CFAEC
		internal void ResetFieldsUsedInExpression()
		{
			if (this.m_collection != null)
			{
				for (int i = 0; i < this.m_collection.Length; i++)
				{
					FieldImpl fieldImpl = this.m_collection[i];
					if (fieldImpl != null)
					{
						fieldImpl.UsedInExpression = false;
					}
				}
			}
		}

		// Token: 0x06006FA2 RID: 28578 RVA: 0x001D1928 File Offset: 0x001CFB28
		internal void AddFieldsUsedInExpression(List<string> fieldsUsedInValueExpression)
		{
			if (this.m_collection != null)
			{
				for (int i = 0; i < this.m_collection.Length; i++)
				{
					FieldImpl fieldImpl = this.m_collection[i];
					if (fieldImpl != null && fieldImpl.UsedInExpression && fieldImpl.FieldDef != null && fieldImpl.FieldDef.DataField != null)
					{
						fieldsUsedInValueExpression.Add(fieldImpl.FieldDef.DataField);
					}
				}
			}
		}

		// Token: 0x06006FA3 RID: 28579 RVA: 0x001D198C File Offset: 0x001CFB8C
		internal void ConsumeAggregationField(int fieldIndex)
		{
			FieldImpl fieldImpl = this[fieldIndex];
			if (!fieldImpl.AggregationFieldChecked && fieldImpl.IsAggregationField)
			{
				fieldImpl.AggregationFieldChecked = true;
				this.m_aggregationFieldCount--;
			}
		}

		// Token: 0x040039A7 RID: 14759
		private ObjectModelImpl m_reportOM;

		// Token: 0x040039A8 RID: 14760
		private Hashtable m_nameMap;

		// Token: 0x040039A9 RID: 14761
		private bool[] m_fieldMissing;

		// Token: 0x040039AA RID: 14762
		private bool[] m_fieldError;

		// Token: 0x040039AB RID: 14763
		private FieldImpl[] m_collection;

		// Token: 0x040039AC RID: 14764
		private int m_count;

		// Token: 0x040039AD RID: 14765
		private bool m_referenced;

		// Token: 0x040039AE RID: 14766
		private bool m_readerExtensionsSupported;

		// Token: 0x040039AF RID: 14767
		private bool m_readerFieldProperties;

		// Token: 0x040039B0 RID: 14768
		private bool m_isAggregateRow;

		// Token: 0x040039B1 RID: 14769
		private int m_aggregationFieldCount;

		// Token: 0x040039B2 RID: 14770
		private int m_aggregationFieldCountForDetailRow;

		// Token: 0x040039B3 RID: 14771
		private bool m_noRows;

		// Token: 0x040039B4 RID: 14772
		private long m_streamOffset = DataFieldRow.UnInitializedStreamOffset;

		// Token: 0x040039B5 RID: 14773
		private bool m_validAggregateRow;

		// Token: 0x040039B6 RID: 14774
		private bool m_addRowIndex;

		// Token: 0x040039B7 RID: 14775
		private bool m_needsInlineSetup;
	}
}
