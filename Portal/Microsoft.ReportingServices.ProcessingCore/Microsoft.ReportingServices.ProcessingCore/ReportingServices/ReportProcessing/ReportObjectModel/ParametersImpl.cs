using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x02000790 RID: 1936
	internal sealed class ParametersImpl : Parameters
	{
		// Token: 0x06006C1B RID: 27675 RVA: 0x001B6BA9 File Offset: 0x001B4DA9
		internal ParametersImpl(int size)
		{
			this.m_collection = new ParameterImpl[size];
			this.m_nameMap = new Hashtable(size);
			this.m_count = 0;
		}

		// Token: 0x17002598 RID: 9624
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
				catch
				{
					throw new ReportProcessingException_NonExistingParameterReference(key);
				}
				return parameter;
			}
		}

		// Token: 0x06006C1D RID: 27677 RVA: 0x001B6C2C File Offset: 0x001B4E2C
		internal void Add(string name, ParameterImpl parameter)
		{
			Global.Tracer.Assert(this.m_collection != null, "(null != m_collection)");
			Global.Tracer.Assert(this.m_nameMap != null, "(null != m_nameMap)");
			Global.Tracer.Assert(this.m_count < this.m_collection.Length, "(m_count < m_collection.Length)");
			this.m_nameMap.Add(name, this.m_count);
			this.m_collection[this.m_count] = parameter;
			this.m_count++;
		}

		// Token: 0x04003646 RID: 13894
		private Hashtable m_nameMap;

		// Token: 0x04003647 RID: 13895
		private ParameterImpl[] m_collection;

		// Token: 0x04003648 RID: 13896
		private int m_count;

		// Token: 0x04003649 RID: 13897
		internal const string Name = "Parameters";

		// Token: 0x0400364A RID: 13898
		internal const string FullName = "Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.Parameters";
	}
}
