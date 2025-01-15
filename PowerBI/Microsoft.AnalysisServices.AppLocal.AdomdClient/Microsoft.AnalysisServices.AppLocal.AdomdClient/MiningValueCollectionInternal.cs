using System;
using System.Collections;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D9 RID: 217
	internal sealed class MiningValueCollectionInternal : ICollection, IEnumerable
	{
		// Token: 0x06000C22 RID: 3106 RVA: 0x0002E254 File Offset: 0x0002C454
		internal MiningValueCollectionInternal()
		{
			this.internalObjectCollection = new ArrayList();
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002E268 File Offset: 0x0002C468
		internal MiningValueCollectionInternal(MiningModelColumn column)
		{
			this.internalObjectCollection = new ArrayList();
			if (column.IsTable)
			{
				return;
			}
			AdomdCommand adomdCommand = new AdomdCommand();
			adomdCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "SELECT DISTINCT {0} FROM [{1}]", column.FullyQualifiedName, column.ParentMiningModel.Name);
			adomdCommand.Connection = column.ParentMiningModel.ParentConnection;
			AdomdDataReader adomdDataReader = adomdCommand.ExecuteReader();
			int num = -1;
			while (adomdDataReader.Read())
			{
				num++;
				object obj = adomdDataReader[0];
				string content = column.Content;
				MiningValueType miningValueType = MiningValueType.Missing;
				if (num == 0 && content.IndexOf("key", StringComparison.OrdinalIgnoreCase) < 0)
				{
					miningValueType = MiningValueType.Missing;
				}
				else if (string.Compare(content, "discrete", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(content, "key", StringComparison.OrdinalIgnoreCase) == 0)
				{
					miningValueType = MiningValueType.Discrete;
				}
				else if (content.IndexOf("discretized", StringComparison.OrdinalIgnoreCase) == 0)
				{
					miningValueType = MiningValueType.Discretized;
				}
				else if (string.Compare(content, "continuous", StringComparison.OrdinalIgnoreCase) == 0)
				{
					miningValueType = MiningValueType.Continuous;
				}
				MiningValue miningValue = new MiningValue(miningValueType, num, obj);
				this.Add(miningValue);
			}
			adomdDataReader.Close();
			adomdDataReader.Dispose();
			adomdCommand.Dispose();
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002E382 File Offset: 0x0002C582
		internal void Add(MiningValue newValue)
		{
			this.internalObjectCollection.Add(newValue);
		}

		// Token: 0x17000492 RID: 1170
		public MiningValue this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return (MiningValue)this.internalObjectCollection[index];
			}
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0002E3BC File Offset: 0x0002C5BC
		public IEnumerator GetEnumerator()
		{
			return new MiningValueCollection.Enumerator(this);
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0002E3C9 File Offset: 0x0002C5C9
		public void CopyTo(Array array, int index)
		{
			this.internalObjectCollection.CopyTo(array, index);
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0002E3D8 File Offset: 0x0002C5D8
		public int Count
		{
			get
			{
				return this.internalObjectCollection.Count;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0002E3E5 File Offset: 0x0002C5E5
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0002E3E8 File Offset: 0x0002C5E8
		public object SyncRoot
		{
			get
			{
				return this.internalObjectCollection.SyncRoot;
			}
		}

		// Token: 0x040007CB RID: 1995
		private ArrayList internalObjectCollection;
	}
}
