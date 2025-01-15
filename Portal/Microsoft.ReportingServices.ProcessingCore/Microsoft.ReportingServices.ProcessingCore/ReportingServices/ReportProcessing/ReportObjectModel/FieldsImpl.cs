using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x0200078D RID: 1933
	internal sealed class FieldsImpl : Fields
	{
		// Token: 0x06006BD8 RID: 27608 RVA: 0x001B6194 File Offset: 0x001B4394
		internal FieldsImpl(int size, bool addRowIndex)
		{
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
			this.m_noRows = true;
			this.m_validAggregateRow = true;
			this.m_addRowIndex = addRowIndex;
		}

		// Token: 0x06006BD9 RID: 27609 RVA: 0x001B6218 File Offset: 0x001B4418
		internal FieldsImpl()
		{
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

		// Token: 0x17002576 RID: 9590
		public override Field this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ReportProcessingException_NonExistingFieldReference();
				}
				this.ValidateFieldCollection();
				Field field;
				try
				{
					FieldImpl fieldImpl = this.m_collection[(int)this.m_nameMap[key]];
					fieldImpl.UsedInExpression = true;
					field = fieldImpl;
				}
				catch (RSException)
				{
					throw;
				}
				catch (Exception ex)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex))
					{
						throw;
					}
					throw new ReportProcessingException_NonExistingFieldReference();
				}
				return field;
			}
		}

		// Token: 0x17002577 RID: 9591
		internal FieldImpl this[int index]
		{
			get
			{
				this.ValidateFieldCollection();
				FieldImpl fieldImpl2;
				try
				{
					FieldImpl fieldImpl = this.m_collection[index];
					fieldImpl.UsedInExpression = true;
					fieldImpl2 = fieldImpl;
				}
				catch (RSException)
				{
					throw;
				}
				catch (Exception ex)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex))
					{
						throw;
					}
					throw new ReportProcessingException_NonExistingFieldReference();
				}
				return fieldImpl2;
			}
			set
			{
				Global.Tracer.Assert(this.m_collection != null);
				this.m_collection[index] = value;
			}
		}

		// Token: 0x17002578 RID: 9592
		// (get) Token: 0x06006BDD RID: 27613 RVA: 0x001B6366 File Offset: 0x001B4566
		internal int Count
		{
			get
			{
				return this.m_count - ((this.m_addRowIndex > false) ? 1 : 0);
			}
		}

		// Token: 0x17002579 RID: 9593
		// (get) Token: 0x06006BDE RID: 27614 RVA: 0x001B6378 File Offset: 0x001B4578
		internal int CountWithRowIndex
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x1700257A RID: 9594
		// (get) Token: 0x06006BDF RID: 27615 RVA: 0x001B6380 File Offset: 0x001B4580
		// (set) Token: 0x06006BE0 RID: 27616 RVA: 0x001B6388 File Offset: 0x001B4588
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

		// Token: 0x1700257B RID: 9595
		// (get) Token: 0x06006BE1 RID: 27617 RVA: 0x001B6391 File Offset: 0x001B4591
		// (set) Token: 0x06006BE2 RID: 27618 RVA: 0x001B6399 File Offset: 0x001B4599
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

		// Token: 0x1700257C RID: 9596
		// (get) Token: 0x06006BE3 RID: 27619 RVA: 0x001B63A2 File Offset: 0x001B45A2
		// (set) Token: 0x06006BE4 RID: 27620 RVA: 0x001B63AA File Offset: 0x001B45AA
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

		// Token: 0x1700257D RID: 9597
		// (get) Token: 0x06006BE5 RID: 27621 RVA: 0x001B63B3 File Offset: 0x001B45B3
		// (set) Token: 0x06006BE6 RID: 27622 RVA: 0x001B63BB File Offset: 0x001B45BB
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

		// Token: 0x1700257E RID: 9598
		// (set) Token: 0x06006BE7 RID: 27623 RVA: 0x001B63C4 File Offset: 0x001B45C4
		internal int AggregationFieldCountForDetailRow
		{
			set
			{
				this.m_aggregationFieldCountForDetailRow = value;
			}
		}

		// Token: 0x1700257F RID: 9599
		// (get) Token: 0x06006BE8 RID: 27624 RVA: 0x001B63CD File Offset: 0x001B45CD
		// (set) Token: 0x06006BE9 RID: 27625 RVA: 0x001B63D5 File Offset: 0x001B45D5
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

		// Token: 0x17002580 RID: 9600
		// (get) Token: 0x06006BEA RID: 27626 RVA: 0x001B63DE File Offset: 0x001B45DE
		internal bool AddRowIndex
		{
			get
			{
				return this.m_addRowIndex;
			}
		}

		// Token: 0x06006BEB RID: 27627 RVA: 0x001B63E8 File Offset: 0x001B45E8
		internal void Add(string name, FieldImpl field)
		{
			Global.Tracer.Assert(this.m_collection != null, "(null != m_collection)");
			Global.Tracer.Assert(this.m_nameMap != null, "(null != m_nameMap)");
			Global.Tracer.Assert(this.m_count < this.m_collection.Length, "(m_count < m_collection.Length)");
			this.m_nameMap.Add(name, this.m_count);
			this.m_collection[this.m_count] = field;
			this.m_count++;
		}

		// Token: 0x06006BEC RID: 27628 RVA: 0x001B6478 File Offset: 0x001B4678
		internal void AddRowIndexField()
		{
			Global.Tracer.Assert(this.m_collection != null, "(null != m_collection)");
			Global.Tracer.Assert(this.m_count < this.m_collection.Length, "(m_count < m_collection.Length)");
			this.m_collection[this.m_count] = null;
			this.m_count++;
		}

		// Token: 0x06006BED RID: 27629 RVA: 0x001B64D8 File Offset: 0x001B46D8
		internal void SetFieldIsMissing(int index)
		{
			if (this.m_fieldMissing == null)
			{
				this.m_fieldMissing = new bool[this.m_collection.Length];
			}
			this.m_fieldMissing[index] = true;
		}

		// Token: 0x06006BEE RID: 27630 RVA: 0x001B64FE File Offset: 0x001B46FE
		internal bool IsFieldMissing(int index)
		{
			return this.m_fieldMissing != null && this.m_fieldMissing[index];
		}

		// Token: 0x06006BEF RID: 27631 RVA: 0x001B6512 File Offset: 0x001B4712
		internal void SetFieldErrorRegistered(int index)
		{
			if (this.m_fieldError == null)
			{
				this.m_fieldError = new bool[this.m_collection.Length];
			}
			this.m_fieldError[index] = true;
		}

		// Token: 0x06006BF0 RID: 27632 RVA: 0x001B6538 File Offset: 0x001B4738
		internal bool IsFieldErrorRegistered(int index)
		{
			return this.m_fieldError != null && this.m_fieldError[index];
		}

		// Token: 0x06006BF1 RID: 27633 RVA: 0x001B654C File Offset: 0x001B474C
		internal void NewRow()
		{
			this.m_noRows = false;
			if (this.m_referenced)
			{
				this.m_collection = new FieldImpl[this.m_count];
				this.m_referenced = false;
			}
		}

		// Token: 0x06006BF2 RID: 27634 RVA: 0x001B6578 File Offset: 0x001B4778
		internal void SetRowIndex(int rowIndex)
		{
			Global.Tracer.Assert(this.m_addRowIndex, "(m_addRowIndex)");
			Global.Tracer.Assert(this.m_count > 0, "(m_count > 0)");
			this.m_collection[this.m_count - 1] = new FieldImpl(rowIndex, false, null);
		}

		// Token: 0x06006BF3 RID: 27635 RVA: 0x001B65D0 File Offset: 0x001B47D0
		internal void SetFields(FieldImpl[] fields)
		{
			this.NewRow();
			Global.Tracer.Assert(this.m_collection != null, "(null != m_collection)");
			if (fields == null)
			{
				for (int i = 0; i < this.m_count; i++)
				{
					FieldImpl fieldImpl = this.m_collection[i];
					Field field = ((fieldImpl == null) ? null : fieldImpl.FieldDef);
					this.m_collection[i] = new FieldImpl(null, false, field);
				}
				return;
			}
			Global.Tracer.Assert(fields.Length == this.m_count, "(fields.Length == m_count)");
			for (int j = 0; j < this.m_count; j++)
			{
				this.m_collection[j] = fields[j];
			}
			this.m_isAggregateRow = false;
			this.m_aggregationFieldCount = this.m_aggregationFieldCountForDetailRow;
		}

		// Token: 0x06006BF4 RID: 27636 RVA: 0x001B667F File Offset: 0x001B487F
		internal void SetFields(FieldImpl[] fields, bool isAggregateRow, int aggregationFieldCount, bool validAggregateRow)
		{
			this.SetFields(fields);
			this.m_isAggregateRow = isAggregateRow;
			this.m_aggregationFieldCount = aggregationFieldCount;
			this.m_validAggregateRow = validAggregateRow;
		}

		// Token: 0x06006BF5 RID: 27637 RVA: 0x001B669E File Offset: 0x001B489E
		internal FieldImpl[] GetAndSaveFields()
		{
			Global.Tracer.Assert(this.m_collection != null, "(null != m_collection)");
			this.m_referenced = true;
			return this.m_collection;
		}

		// Token: 0x06006BF6 RID: 27638 RVA: 0x001B66C5 File Offset: 0x001B48C5
		internal FieldImpl[] GetFields()
		{
			Global.Tracer.Assert(this.m_collection != null, "(null != m_collection)");
			return this.m_collection;
		}

		// Token: 0x06006BF7 RID: 27639 RVA: 0x001B66E8 File Offset: 0x001B48E8
		internal int GetRowIndex()
		{
			Global.Tracer.Assert(this.m_addRowIndex, "(m_addRowIndex)");
			Global.Tracer.Assert(this.m_count > 0, "(m_count > 0)");
			return (int)this.m_collection[this.m_count - 1].Value;
		}

		// Token: 0x06006BF8 RID: 27640 RVA: 0x001B673C File Offset: 0x001B493C
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

		// Token: 0x06006BF9 RID: 27641 RVA: 0x001B6794 File Offset: 0x001B4994
		private bool ValidateFieldCollection()
		{
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

		// Token: 0x06006BFA RID: 27642 RVA: 0x001B67BC File Offset: 0x001B49BC
		internal void ResetUsedInExpression()
		{
			if (this.m_collection != null)
			{
				for (int i = 0; i < this.m_collection.Length; i++)
				{
					this.m_collection[i].UsedInExpression = false;
				}
			}
		}

		// Token: 0x06006BFB RID: 27643 RVA: 0x001B67F4 File Offset: 0x001B49F4
		internal void AddFieldsUsedInExpression(List<string> fieldsUsedInValueExpression)
		{
			if (this.m_collection != null)
			{
				for (int i = 0; i < this.m_collection.Length; i++)
				{
					FieldImpl fieldImpl = this.m_collection[i];
					if (fieldImpl.UsedInExpression && fieldImpl.FieldDef != null)
					{
						fieldsUsedInValueExpression.Add(fieldImpl.FieldDef.DataField);
					}
				}
			}
		}

		// Token: 0x04003629 RID: 13865
		private Hashtable m_nameMap;

		// Token: 0x0400362A RID: 13866
		private bool[] m_fieldMissing;

		// Token: 0x0400362B RID: 13867
		private bool[] m_fieldError;

		// Token: 0x0400362C RID: 13868
		private FieldImpl[] m_collection;

		// Token: 0x0400362D RID: 13869
		private int m_count;

		// Token: 0x0400362E RID: 13870
		private bool m_referenced;

		// Token: 0x0400362F RID: 13871
		private bool m_readerExtensionsSupported;

		// Token: 0x04003630 RID: 13872
		private bool m_readerFieldProperties;

		// Token: 0x04003631 RID: 13873
		private bool m_isAggregateRow;

		// Token: 0x04003632 RID: 13874
		private int m_aggregationFieldCount;

		// Token: 0x04003633 RID: 13875
		private int m_aggregationFieldCountForDetailRow;

		// Token: 0x04003634 RID: 13876
		private bool m_noRows;

		// Token: 0x04003635 RID: 13877
		private bool m_validAggregateRow;

		// Token: 0x04003636 RID: 13878
		private bool m_addRowIndex;

		// Token: 0x04003637 RID: 13879
		internal const string Name = "Fields";
	}
}
