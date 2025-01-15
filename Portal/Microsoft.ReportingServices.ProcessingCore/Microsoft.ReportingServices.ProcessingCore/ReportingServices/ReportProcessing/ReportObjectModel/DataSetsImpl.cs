using System;
using System.Collections;
using System.Threading;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x02000798 RID: 1944
	internal sealed class DataSetsImpl : DataSets
	{
		// Token: 0x06006C55 RID: 27733 RVA: 0x001B7648 File Offset: 0x001B5848
		internal DataSetsImpl()
		{
		}

		// Token: 0x06006C56 RID: 27734 RVA: 0x001B7650 File Offset: 0x001B5850
		internal DataSetsImpl(bool lockAdd, int size)
		{
			this.m_lockAdd = lockAdd;
			this.m_collection = new Hashtable(size);
		}

		// Token: 0x06006C57 RID: 27735 RVA: 0x001B766C File Offset: 0x001B586C
		internal void Add(DataSet dataSetDef)
		{
			try
			{
				if (this.m_lockAdd)
				{
					Monitor.Enter(this.m_collection);
				}
				this.m_collection.Add(dataSetDef.Name, new DataSetImpl(dataSetDef));
			}
			finally
			{
				if (this.m_lockAdd)
				{
					Monitor.Exit(this.m_collection);
				}
			}
		}

		// Token: 0x170025B5 RID: 9653
		public override DataSet this[string key]
		{
			get
			{
				if (key == null || this.m_collection == null)
				{
					throw new ReportProcessingException_NonExistingDataSetReference(key);
				}
				DataSet dataSet2;
				try
				{
					DataSet dataSet = this.m_collection[key] as DataSet;
					if (dataSet == null)
					{
						throw new ReportProcessingException_NonExistingDataSetReference(key);
					}
					dataSet2 = dataSet;
				}
				catch
				{
					throw new ReportProcessingException_NonExistingDataSetReference(key);
				}
				return dataSet2;
			}
		}

		// Token: 0x04003672 RID: 13938
		private bool m_lockAdd;

		// Token: 0x04003673 RID: 13939
		private Hashtable m_collection;

		// Token: 0x04003674 RID: 13940
		internal const string Name = "DataSets";

		// Token: 0x04003675 RID: 13941
		internal const string FullName = "Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.DataSets";
	}
}
