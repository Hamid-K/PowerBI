using System;
using System.Collections;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000203 RID: 515
	internal class BaseHashTable : IOperation, IBaseHashTable, IEnumerable, ICloneable
	{
		// Token: 0x060010BE RID: 4286 RVA: 0x00037610 File Offset: 0x00035810
		internal BaseHashTable(IDirectoryNodeFactory directoryNodeFactory)
		{
			this._directoryNodeFactory = directoryNodeFactory;
			this._root = this._directoryNodeFactory.GetDirectory(0, null, -1);
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x00037651 File Offset: 0x00035851
		internal MDHDirectoryNode RootDirectory
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x00037659 File Offset: 0x00035859
		public ICollection Keys
		{
			get
			{
				return new BaseHashTableKeyCollection(this);
			}
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00037664 File Offset: 0x00035864
		public int DoCompaction()
		{
			int num = 0;
			Interlocked.Increment(ref this._lastCompactionEpoch);
			this._root.DoCompactionOnSubDirectories(-1, ref num, ref this._lastCompactionEpoch);
			if (num > 0)
			{
				Interlocked.Add(ref this.SplitCount, -1 * num);
			}
			return num;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x000376A8 File Offset: 0x000358A8
		public object Get(object searchKey)
		{
			int hashCode = searchKey.GetHashCode();
			return this.GetData(hashCode, searchKey);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x000376C4 File Offset: 0x000358C4
		public void Add(object searchKey, object data)
		{
			DMOperationInfo dmoperationInfo = new DMOperationInfo(DMOperationType.ADD, searchKey, data, this, null);
			this.Replace(ref dmoperationInfo);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x000376EC File Offset: 0x000358EC
		internal object AddOrGet(object searchKey, IObjectCreator objCreator)
		{
			DMOperationInfo dmoperationInfo = new DMOperationInfo(DMOperationType.ADD_OR_GET, searchKey, null, this, objCreator);
			this.Replace(ref dmoperationInfo);
			return dmoperationInfo.OldData;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0003771C File Offset: 0x0003591C
		public object Delete(object searchKey)
		{
			DMOperationInfo dmoperationInfo = new DMOperationInfo(DMOperationType.DELETE, searchKey, null, this, null);
			this.Replace(ref dmoperationInfo);
			return dmoperationInfo.OldData;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0003774C File Offset: 0x0003594C
		public object Upsert(object searchKey, object data)
		{
			DMOperationInfo dmoperationInfo = new DMOperationInfo(DMOperationType.UPSERT, searchKey, data, this, null);
			this.Replace(ref dmoperationInfo);
			return dmoperationInfo.OldData;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0003777C File Offset: 0x0003597C
		public bool ContainsKey(object searchKey)
		{
			int hashCode = searchKey.GetHashCode();
			return this.GetData(hashCode, searchKey) != null;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0003779E File Offset: 0x0003599E
		public IEnumerator GetEnumerator()
		{
			return new BaseHashTableDataEnumerator(this);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00008948 File Offset: 0x00006B48
		public BaseHashTable GetParentHashTable()
		{
			return this;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x000377A8 File Offset: 0x000359A8
		public void PreProcess(IBaseDataNode oldItem, ref IBaseDataNode newItem, ref DMOperationInfo operationInfo)
		{
			switch (operationInfo.OperationType)
			{
			case DMOperationType.ADD:
				if (oldItem == null)
				{
					newItem = BaseHashTable.GetBaseDataNode(operationInfo);
					return;
				}
				throw DMGlobal.GetException(2001);
			case DMOperationType.DELETE:
				newItem = null;
				operationInfo.OldData = ((oldItem == null) ? null : oldItem.Data);
				return;
			case DMOperationType.UPSERT:
				newItem = BaseHashTable.GetBaseDataNode(operationInfo);
				operationInfo.OldData = ((oldItem == null) ? null : oldItem.Data);
				return;
			case DMOperationType.ADD_OR_GET:
				if (oldItem == null)
				{
					object @object = operationInfo.ObjectCreator.GetObject();
					oldItem = new BaseDataNode(operationInfo.HashCode, operationInfo.SearchKey, @object);
				}
				newItem = oldItem;
				operationInfo.OldData = oldItem.Data;
				return;
			default:
				return;
			}
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00037858 File Offset: 0x00035A58
		private static IBaseDataNode GetBaseDataNode(DMOperationInfo operationInfo)
		{
			IBaseDataNode baseDataNode = operationInfo.NewData as IBaseDataNode;
			if (baseDataNode != null)
			{
				return baseDataNode;
			}
			return new BaseDataNode(operationInfo.HashCode, operationInfo.SearchKey, operationInfo.NewData);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00037891 File Offset: 0x00035A91
		internal IEnumerator GetKeyEnumerator()
		{
			return new BaseHashTableKeyEnumerator(this);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00037899 File Offset: 0x00035A99
		public IEnumerator GetKeyValueEnumerator()
		{
			return new BaseHashTableKeyValueEnumerator(this);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000378A4 File Offset: 0x00035AA4
		public bool GetBatch(IScanner scanner, EnumeratorState enumeratorState)
		{
			BaseEnumeratorState baseEnumeratorState = enumeratorState as BaseEnumeratorState;
			if (!baseEnumeratorState.IsValidState(this._lastCompactionEpoch) || !baseEnumeratorState.IsValidState(this))
			{
				throw DMGlobal.GetException(2008);
			}
			if (!baseEnumeratorState.Exhausted && this._root.GetBatch(scanner, baseEnumeratorState))
			{
				baseEnumeratorState.Exhausted = true;
			}
			if (!baseEnumeratorState.IsValidState(this._lastCompactionEpoch))
			{
				throw DMGlobal.GetException(2008);
			}
			return baseEnumeratorState.Exhausted;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00037917 File Offset: 0x00035B17
		public EnumeratorState GetStatelessEnumeratorState()
		{
			return new BaseEnumeratorState(this, this._lastCompactionEpoch);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00037928 File Offset: 0x00035B28
		internal FixedDepthEnumeratorState GetFixedDepthEnumerator()
		{
			return new FixedDepthEnumeratorState(this, this._lastCompactionEpoch);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00037943 File Offset: 0x00035B43
		internal object GetData(FixedDepthEnumeratorState state)
		{
			return this.GetData(this._root, state, -1);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00037953 File Offset: 0x00035B53
		internal object MoveNextAndGetData(FixedDepthEnumeratorState state)
		{
			return this.MoveNextAndGetData(this._root, state, -1);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00037964 File Offset: 0x00035B64
		internal object MoveNextAndGetData(MDHDirectoryNode node, FixedDepthEnumeratorState state, int level)
		{
			int num = level + 1;
			int index = BaseHashTable.GetIndex(node, state.Path);
			MDHNode nodeInSlot = node.GetNodeInSlot(index);
			if (num >= state.Level)
			{
				return this.SetLevelAndGetNext(node, state, num, true);
			}
			MDHDirectoryNode mdhdirectoryNode = nodeInSlot as MDHDirectoryNode;
			object obj = this.MoveNextAndGetData(mdhdirectoryNode, state, num);
			if (obj == null)
			{
				return this.SetLevelAndGetNext(mdhdirectoryNode, state, num, true);
			}
			return obj;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x000379C4 File Offset: 0x00035BC4
		private object SetLevelAndGetNext(MDHDirectoryNode node, FixedDepthEnumeratorState state, int level, bool sameLevelCall)
		{
			int i = BaseHashTable.GetIndex(node, state.Path);
			int num;
			if (sameLevelCall)
			{
				i++;
				num = level;
			}
			else
			{
				num = level + 1;
			}
			state.Index = 0;
			state.Level = num;
			while (i < node.Count)
			{
				MDHNode nodeInSlot = node.GetNodeInSlot(i);
				if (nodeInSlot != null)
				{
					MDHLeafNode mdhleafNode = nodeInSlot as MDHLeafNode;
					if (mdhleafNode != null)
					{
						object data = mdhleafNode.GetData(state, level);
						if (data != null)
						{
							state.Path = node.GetPathForIndex(i, state.Path);
							return data;
						}
					}
					else
					{
						MDHDirectoryNode mdhdirectoryNode = (MDHDirectoryNode)nodeInSlot;
						object obj = this.SetLevelAndGetNext(mdhdirectoryNode, state, num, false);
						if (obj != null)
						{
							state.Path = node.GetPathForIndex(i, state.Path);
							return obj;
						}
						state.Level = num;
					}
				}
				i++;
			}
			return null;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00037A7D File Offset: 0x00035C7D
		public void Clear()
		{
			this._root = this._directoryNodeFactory.GetDirectory(0, null, -1);
			this.SplitCount = 0;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x00037A9A File Offset: 0x00035C9A
		int IBaseHashTable.NumberOfSplits
		{
			get
			{
				return this.SplitCount;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00037AA2 File Offset: 0x00035CA2
		public long LastCompactionEpoch
		{
			get
			{
				return this._lastCompactionEpoch;
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00037AAC File Offset: 0x00035CAC
		public object Clone()
		{
			IBaseHashTable baseHashTable = DataStructureFactory.CreateBaseHashTable(this._directoryNodeFactory);
			IEnumerator keyValueEnumerator = this.GetKeyValueEnumerator();
			while (keyValueEnumerator.MoveNext())
			{
				object obj = keyValueEnumerator.Current;
				IBaseDataNode baseDataNode = obj as IBaseDataNode;
				baseHashTable.Add(baseDataNode.Key, baseDataNode.Data);
			}
			return baseHashTable;
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00037AF8 File Offset: 0x00035CF8
		internal void Replace(ref DMOperationInfo operationInfo)
		{
			int num = -1;
			bool flag = false;
			while (!flag)
			{
				MDHDirectoryNode directoryAndIndex = this.GetDirectoryAndIndex(operationInfo.HashCode, ref num);
				flag = this.ReplaceData(directoryAndIndex, num, ref operationInfo);
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00037B28 File Offset: 0x00035D28
		private bool ReplaceData(MDHDirectoryNode dir, int index, ref DMOperationInfo operationInfo)
		{
			bool flag;
			try
			{
				dir.LatchSlot(index);
				MDHNode nodeInSlot = dir.GetNodeInSlot(index);
				if (nodeInSlot == null)
				{
					flag = BaseHashTable.PutNodeInSlot(dir, index, ref operationInfo);
				}
				else
				{
					MDHLeafNode mdhleafNode = nodeInSlot as MDHLeafNode;
					if (mdhleafNode != null)
					{
						if (mdhleafNode.CompareHashCode(operationInfo.HashCode))
						{
							if (mdhleafNode.CompareKey(operationInfo.SearchKey))
							{
								flag = BaseHashTable.PutNodeInSlot(dir, index, ref operationInfo);
							}
							else
							{
								flag = BaseHashTable.AddConflictNode(index, dir, ref operationInfo);
							}
						}
						else
						{
							flag = this.SplitDirectory(index, dir, ref operationInfo);
						}
					}
					else
					{
						flag = false;
					}
				}
			}
			finally
			{
				dir.UnLatchSlot(index);
			}
			return flag;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00037BB8 File Offset: 0x00035DB8
		private static bool AddConflictNode(int slot, MDHDirectoryNode dir, ref DMOperationInfo operationInfo)
		{
			MDHLeafNode mdhleafNode = (MDHLeafNode)dir.GetNodeInSlot(slot);
			IBaseDataNode baseDataNode = null;
			operationInfo.Operation.PreProcess(null, ref baseDataNode, ref operationInfo);
			if (baseDataNode != null)
			{
				IBaseDataNode baseDataNode2 = mdhleafNode as IBaseDataNode;
				if (baseDataNode2 != null)
				{
					BaseConflictNode baseConflictNode = new BaseConflictNode(operationInfo.HashCode);
					baseConflictNode.Put(baseDataNode2);
					baseConflictNode.Put(baseDataNode);
					dir.PutNodeInSlot(slot, baseConflictNode);
				}
				else
				{
					BaseConflictNode baseConflictNode2 = mdhleafNode as BaseConflictNode;
					if (baseConflictNode2 != null)
					{
						baseConflictNode2.Put(baseDataNode);
					}
				}
			}
			return true;
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00037C2C File Offset: 0x00035E2C
		private static bool PutNodeInSlot(MDHDirectoryNode dir, int slot, ref DMOperationInfo operationInfo)
		{
			MDHNode nodeInSlot = dir.GetNodeInSlot(slot);
			IBaseDataNode baseDataNode = nodeInSlot as IBaseDataNode;
			if (nodeInSlot == null || baseDataNode != null)
			{
				IBaseDataNode baseDataNode2 = null;
				operationInfo.Operation.PreProcess(baseDataNode, ref baseDataNode2, ref operationInfo);
				if (baseDataNode2 != null)
				{
					MDHNode mdhnode = baseDataNode2 as MDHNode;
					dir.PutNodeInSlot(slot, mdhnode);
					if (!dir.AtomicallyCheckParentalLinkage())
					{
						dir.PutNodeInSlot(slot, nodeInSlot);
						return false;
					}
				}
				else
				{
					dir.PutNodeInSlot(slot, null);
				}
				return true;
			}
			BaseConflictNode baseConflictNode = dir.GetNodeInSlot(slot) as BaseConflictNode;
			return baseConflictNode != null && baseConflictNode.Put(ref operationInfo);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00037CAC File Offset: 0x00035EAC
		private bool SplitDirectory(int slot, MDHDirectoryNode dir, ref DMOperationInfo operationInfo)
		{
			MDHLeafNode mdhleafNode = (MDHLeafNode)dir.GetNodeInSlot(slot);
			IBaseDataNode baseDataNode = null;
			operationInfo.Operation.PreProcess(null, ref baseDataNode, ref operationInfo);
			if (baseDataNode != null)
			{
				int num = operationInfo.HashCode ^ mdhleafNode.HashCode;
				MDHDirectoryNode mdhdirectoryNode = this.CreateSubdirectoryToResolveSlotCollision(num, dir, slot);
				MDHLeafNode mdhleafNode2 = baseDataNode as MDHLeafNode;
				mdhdirectoryNode.PutNode(mdhleafNode2);
				mdhdirectoryNode.PutNode(mdhleafNode);
				dir.PutNodeInSlot(slot, mdhdirectoryNode);
				Interlocked.Increment(ref this.SplitCount);
			}
			return true;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00037D20 File Offset: 0x00035F20
		private MDHDirectoryNode CreateSubdirectoryToResolveSlotCollision(int diff, MDHDirectoryNode parent, int parentIndex)
		{
			int num = 0;
			int num2 = MDHGlobals.IndexMasks[0];
			while ((diff & num2) == 0)
			{
				num++;
				num2 = MDHGlobals.IndexMasks[num];
			}
			return this._directoryNodeFactory.GetDirectory((short)num, parent, (short)parentIndex);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00037D5C File Offset: 0x00035F5C
		private object GetData(int hashCode, object searchKey)
		{
			MDHLeafNode leafNode = this.GetLeafNode(hashCode);
			if (leafNode != null)
			{
				return leafNode.Get(searchKey, hashCode);
			}
			return null;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00037D80 File Offset: 0x00035F80
		private MDHLeafNode GetLeafNode(int hashCode)
		{
			int num = 0;
			MDHLeafNode mdhleafNode = null;
			this.GetDirectoryIndexAndNode(hashCode, ref num, ref mdhleafNode);
			return mdhleafNode;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00037DA0 File Offset: 0x00035FA0
		private MDHDirectoryNode GetDirectoryAndIndex(int hashcode, ref int index)
		{
			MDHLeafNode mdhleafNode = null;
			return this.GetDirectoryIndexAndNode(hashcode, ref index, ref mdhleafNode);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00037DBC File Offset: 0x00035FBC
		private MDHDirectoryNode GetDirectoryIndexAndNode(int hashCode, ref int index, ref MDHLeafNode leafNode)
		{
			MDHDirectoryNode mdhdirectoryNode = this._root;
			MDHNode nodeInSlot;
			MDHDirectoryNode mdhdirectoryNode2;
			for (;;)
			{
				index = mdhdirectoryNode.GetSlotNumber(hashCode);
				nodeInSlot = mdhdirectoryNode.GetNodeInSlot(index);
				mdhdirectoryNode2 = mdhdirectoryNode;
				mdhdirectoryNode = nodeInSlot as MDHDirectoryNode;
				if (mdhdirectoryNode == null)
				{
					break;
				}
				if (mdhdirectoryNode == mdhdirectoryNode2)
				{
					Thread.Sleep(1);
				}
			}
			leafNode = nodeInSlot as MDHLeafNode;
			return mdhdirectoryNode2;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00037E08 File Offset: 0x00036008
		internal object GetData(MDHDirectoryNode baseLeafNode, FixedDepthEnumeratorState state, int level)
		{
			int num = level + 1;
			int index = BaseHashTable.GetIndex(baseLeafNode, state.Path);
			MDHNode nodeInSlot = baseLeafNode.GetNodeInSlot(index);
			if (nodeInSlot == null)
			{
				return null;
			}
			if (num != state.Level)
			{
				MDHDirectoryNode mdhdirectoryNode = nodeInSlot as MDHDirectoryNode;
				return this.GetData(mdhdirectoryNode, state, num);
			}
			MDHLeafNode mdhleafNode = nodeInSlot as MDHLeafNode;
			if (mdhleafNode != null)
			{
				return mdhleafNode.GetData(state, level);
			}
			return null;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00037E63 File Offset: 0x00036063
		private static int GetIndex(MDHDirectoryNode directoryNode, uint path)
		{
			return directoryNode.GetSlotNumber((int)path);
		}

		// Token: 0x04000AD5 RID: 2773
		private MDHDirectoryNode _root;

		// Token: 0x04000AD6 RID: 2774
		private IDirectoryNodeFactory _directoryNodeFactory;

		// Token: 0x04000AD7 RID: 2775
		private long _lastCompactionEpoch = DateTime.UtcNow.Ticks;

		// Token: 0x04000AD8 RID: 2776
		internal int SplitCount;
	}
}
