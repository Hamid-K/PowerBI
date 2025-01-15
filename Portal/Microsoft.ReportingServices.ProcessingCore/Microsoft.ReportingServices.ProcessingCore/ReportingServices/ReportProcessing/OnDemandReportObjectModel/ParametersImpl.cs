using System;
using System.Collections;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007B1 RID: 1969
	internal sealed class ParametersImpl : Parameters
	{
		// Token: 0x06006FDA RID: 28634 RVA: 0x001D25E3 File Offset: 0x001D07E3
		internal ParametersImpl()
		{
		}

		// Token: 0x06006FDB RID: 28635 RVA: 0x001D25EB File Offset: 0x001D07EB
		internal ParametersImpl(int size)
		{
			this.m_collection = new ParameterImpl[size];
			this.m_nameMap = new Hashtable(size);
			this.m_count = 0;
		}

		// Token: 0x06006FDC RID: 28636 RVA: 0x001D2614 File Offset: 0x001D0814
		internal ParametersImpl(ParametersImpl copy)
		{
			this.m_count = copy.m_count;
			if (copy.m_collection != null)
			{
				this.m_collection = new ParameterImpl[this.m_count];
				Array.Copy(copy.m_collection, this.m_collection, this.m_count);
			}
			if (copy.m_nameMap != null)
			{
				this.m_nameMap = (Hashtable)copy.m_nameMap.Clone();
			}
		}

		// Token: 0x1700261A RID: 9754
		public override Parameter this[string key]
		{
			get
			{
				if (key == null || this.m_nameMap == null || this.m_collection == null)
				{
					throw new ReportProcessingException_NonExistingParameterReference(key);
				}
				Parameter parameter;
				try
				{
					parameter = this.m_collection[(int)this.m_nameMap[key]];
				}
				catch (Exception ex)
				{
					if (AsynchronousExceptionDetection.IsStoppingException(ex))
					{
						throw;
					}
					throw new ReportProcessingException_NonExistingParameterReference(key);
				}
				return parameter;
			}
		}

		// Token: 0x1700261B RID: 9755
		// (get) Token: 0x06006FDE RID: 28638 RVA: 0x001D26E8 File Offset: 0x001D08E8
		// (set) Token: 0x06006FDF RID: 28639 RVA: 0x001D26F0 File Offset: 0x001D08F0
		internal ParameterImpl[] Collection
		{
			get
			{
				return this.m_collection;
			}
			set
			{
				this.m_collection = value;
			}
		}

		// Token: 0x1700261C RID: 9756
		// (get) Token: 0x06006FE0 RID: 28640 RVA: 0x001D26F9 File Offset: 0x001D08F9
		// (set) Token: 0x06006FE1 RID: 28641 RVA: 0x001D2701 File Offset: 0x001D0901
		internal Hashtable NameMap
		{
			get
			{
				return this.m_nameMap;
			}
			set
			{
				this.m_nameMap = value;
			}
		}

		// Token: 0x1700261D RID: 9757
		// (get) Token: 0x06006FE2 RID: 28642 RVA: 0x001D270A File Offset: 0x001D090A
		// (set) Token: 0x06006FE3 RID: 28643 RVA: 0x001D2712 File Offset: 0x001D0912
		internal int Count
		{
			get
			{
				return this.m_count;
			}
			set
			{
				this.m_count = value;
			}
		}

		// Token: 0x06006FE4 RID: 28644 RVA: 0x001D271C File Offset: 0x001D091C
		internal void Add(string name, ParameterImpl parameter)
		{
			Global.Tracer.Assert(this.m_collection != null, "(null != m_collection)");
			Global.Tracer.Assert(this.m_nameMap != null, "(null != m_nameMap)");
			Global.Tracer.Assert(this.m_count < this.m_collection.Length, "(m_count < m_collection.Length)");
			this.m_nameMap.Add(name, this.m_count);
			this.m_collection[this.m_count] = parameter;
			this.m_count++;
		}

		// Token: 0x06006FE5 RID: 28645 RVA: 0x001D27AB File Offset: 0x001D09AB
		internal void Clear()
		{
			if (this.m_nameMap != null)
			{
				this.m_nameMap.Clear();
			}
			if (this.m_collection != null)
			{
				this.m_collection = new ParameterImpl[this.m_collection.Length];
			}
			this.m_count = 0;
		}

		// Token: 0x040039CB RID: 14795
		private Hashtable m_nameMap;

		// Token: 0x040039CC RID: 14796
		private ParameterImpl[] m_collection;

		// Token: 0x040039CD RID: 14797
		private int m_count;
	}
}
