using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Data
{
	// Token: 0x02000054 RID: 84
	public class ObjectToDataRecordAdapter<T> : IDataRecordProvider<T>
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x00014D5D File Offset: 0x00012F5D
		public ObjectToDataRecordAdapter()
			: this(20)
		{
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00014D68 File Offset: 0x00012F68
		public ObjectToDataRecordAdapter(BindingFlags fieldBindingFlags)
		{
			Type typeFromHandle = typeof(T);
			List<FieldInfo> list = new List<FieldInfo>();
			foreach (FieldInfo fieldInfo in typeFromHandle.GetFields(fieldBindingFlags))
			{
				if (!fieldInfo.IsStatic)
				{
					list.Add(fieldInfo);
				}
			}
			this.BuildSchema(list.ToArray());
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00014DC0 File Offset: 0x00012FC0
		public ObjectToDataRecordAdapter(params string[] fieldNames)
		{
			Type typeFromHandle = typeof(T);
			List<FieldInfo> list = new List<FieldInfo>();
			foreach (string text in fieldNames)
			{
				list.Add(typeFromHandle.GetField(text));
			}
			this.BuildSchema(list.ToArray());
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00014E13 File Offset: 0x00013013
		public DataTable Schema
		{
			get
			{
				return this.m_schema;
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00014E1B File Offset: 0x0001301B
		public DataTable GetSchemaTable()
		{
			return this.m_schema;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00014E24 File Offset: 0x00013024
		private void BuildSchema(FieldInfo[] fieldInfo)
		{
			this.m_fieldInfo = fieldInfo;
			DataTable dataTable = new DataTable();
			foreach (FieldInfo fieldInfo2 in fieldInfo)
			{
				dataTable.Columns.Add(fieldInfo2.Name, fieldInfo2.FieldType);
			}
			this.m_schema = dataTable.CreateDataReader().GetSchemaTable();
			this.m_record = new SimpleDataRecord(this.m_schema, new object[this.m_schema.Rows.Count]);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00014EA4 File Offset: 0x000130A4
		public IDataRecord GetRecord(T item)
		{
			for (int i = 0; i < this.m_fieldInfo.Length; i++)
			{
				this.m_record.Values[i] = this.m_fieldInfo[i].GetValue(item);
			}
			return this.m_record;
		}

		// Token: 0x04000072 RID: 114
		private DataTable m_schema;

		// Token: 0x04000073 RID: 115
		private FieldInfo[] m_fieldInfo;

		// Token: 0x04000074 RID: 116
		private SimpleDataRecord m_record;
	}
}
