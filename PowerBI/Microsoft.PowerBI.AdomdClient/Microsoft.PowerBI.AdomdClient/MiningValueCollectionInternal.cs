using System;
using System.Collections;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D9 RID: 217
	internal sealed class MiningValueCollectionInternal : ICollection, IEnumerable
	{
		// Token: 0x06000C15 RID: 3093 RVA: 0x0002DF24 File Offset: 0x0002C124
		internal MiningValueCollectionInternal()
		{
			this.internalObjectCollection = new ArrayList();
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002DF38 File Offset: 0x0002C138
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

		// Token: 0x06000C17 RID: 3095 RVA: 0x0002E052 File Offset: 0x0002C252
		internal void Add(MiningValue newValue)
		{
			this.internalObjectCollection.Add(newValue);
		}

		// Token: 0x1700048C RID: 1164
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

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002E08C File Offset: 0x0002C28C
		public IEnumerator GetEnumerator()
		{
			return new MiningValueCollection.Enumerator(this);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002E099 File Offset: 0x0002C299
		public void CopyTo(Array array, int index)
		{
			this.internalObjectCollection.CopyTo(array, index);
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0002E0A8 File Offset: 0x0002C2A8
		public int Count
		{
			get
			{
				return this.internalObjectCollection.Count;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0002E0B5 File Offset: 0x0002C2B5
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0002E0B8 File Offset: 0x0002C2B8
		public object SyncRoot
		{
			get
			{
				return this.internalObjectCollection.SyncRoot;
			}
		}

		// Token: 0x040007BE RID: 1982
		private ArrayList internalObjectCollection;
	}
}
