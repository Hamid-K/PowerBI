using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004AC RID: 1196
	[Serializable]
	public class DataValueList : ArrayList, IList<DataValue>, ICollection<DataValue>, IEnumerable<DataValue>, IEnumerable
	{
		// Token: 0x06003AC1 RID: 15041 RVA: 0x000FEA35 File Offset: 0x000FCC35
		public DataValueList()
		{
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x000FEA3D File Offset: 0x000FCC3D
		internal DataValueList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001953 RID: 6483
		internal DataValue this[int index]
		{
			get
			{
				return (DataValue)base[index];
			}
		}

		// Token: 0x06003AC4 RID: 15044 RVA: 0x000FEA54 File Offset: 0x000FCC54
		internal static string CreatePropertyNameString(string prefix, int rowIndex, int cellIndex, int valueIndex)
		{
			if (rowIndex > 0)
			{
				return string.Concat(new string[]
				{
					prefix,
					"DataValue(Row:",
					rowIndex.ToString(),
					")(Cell:",
					cellIndex.ToString(),
					")(Index:",
					valueIndex.ToString(),
					")"
				});
			}
			return prefix + "CustomProperty(Index:" + valueIndex.ToString() + ")";
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x000FEACA File Offset: 0x000FCCCA
		internal void Initialize(string prefix, InitializationContext context)
		{
			this.Initialize(prefix, -1, -1, true, context);
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x000FEAD8 File Offset: 0x000FCCD8
		internal void Initialize(string prefix, int rowIndex, int cellIndex, bool isCustomProperty, InitializationContext context)
		{
			int count = this.Count;
			Microsoft.ReportingServices.ReportPublishing.CustomPropertyUniqueNameValidator customPropertyUniqueNameValidator = new Microsoft.ReportingServices.ReportPublishing.CustomPropertyUniqueNameValidator();
			for (int i = 0; i < count; i++)
			{
				Global.Tracer.Assert(this[i] != null);
				this[i].Initialize(DataValueList.CreatePropertyNameString(prefix, rowIndex + 1, cellIndex + 1, i + 1), isCustomProperty, customPropertyUniqueNameValidator, context);
			}
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x000FEB34 File Offset: 0x000FCD34
		internal void SetExprHost(IList<DataValueExprHost> dataValueHosts, ObjectModelImpl reportObjectModel)
		{
			if (dataValueHosts != null)
			{
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					Global.Tracer.Assert(this[i] != null);
					this[i].SetExprHost(dataValueHosts, reportObjectModel);
				}
			}
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x000FEB79 File Offset: 0x000FCD79
		int IList<DataValue>.IndexOf(DataValue item)
		{
			return this.IndexOf(item);
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x000FEB82 File Offset: 0x000FCD82
		void IList<DataValue>.Insert(int index, DataValue item)
		{
			this.Insert(index, item);
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x000FEB8C File Offset: 0x000FCD8C
		void IList<DataValue>.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x17001954 RID: 6484
		DataValue IList<DataValue>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				base[index] = value;
			}
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x000FEBA8 File Offset: 0x000FCDA8
		void ICollection<DataValue>.Add(DataValue item)
		{
			this.Add(item);
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x000FEBB2 File Offset: 0x000FCDB2
		void ICollection<DataValue>.Clear()
		{
			this.Clear();
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x000FEBBA File Offset: 0x000FCDBA
		bool ICollection<DataValue>.Contains(DataValue item)
		{
			return this.Contains(item);
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x000FEBC3 File Offset: 0x000FCDC3
		void ICollection<DataValue>.CopyTo(DataValue[] array, int arrayIndex)
		{
			this.CopyTo(array, arrayIndex);
		}

		// Token: 0x17001955 RID: 6485
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x000FEBCD File Offset: 0x000FCDCD
		int ICollection<DataValue>.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17001956 RID: 6486
		// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x000FEBD5 File Offset: 0x000FCDD5
		bool ICollection<DataValue>.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x000FEBDD File Offset: 0x000FCDDD
		bool ICollection<DataValue>.Remove(DataValue item)
		{
			if (!this.Contains(item))
			{
				return false;
			}
			this.Remove(item);
			return true;
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x000FEBF2 File Offset: 0x000FCDF2
		IEnumerator<DataValue> IEnumerable<DataValue>.GetEnumerator()
		{
			foreach (object obj in this)
			{
				DataValue dataValue = (DataValue)obj;
				yield return dataValue;
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x000FEC01 File Offset: 0x000FCE01
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
