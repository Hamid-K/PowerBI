using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000237 RID: 567
	internal sealed class MDHConflictingHashNode : MDHLeafNode
	{
		// Token: 0x060012C5 RID: 4805 RVA: 0x0003A2C4 File Offset: 0x000384C4
		private AMDHObjectNode GetObjectNode(object key, int hashKey)
		{
			if (hashKey == base.HashCode)
			{
				return this.GetObjectNode(key);
			}
			return null;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0003A2D8 File Offset: 0x000384D8
		private AMDHObjectNode GetObjectNode(object key)
		{
			AMDHObjectNode amdhobjectNode = null;
			lock (this._list)
			{
				amdhobjectNode = this._list.Find((AMDHObjectNode objNode) => objNode != null && objNode.CompareKey(key));
			}
			return amdhobjectNode;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0003A344 File Offset: 0x00038544
		internal MDHConflictingHashNode(AMDHObjectNode oldON, AMDHObjectNode newON)
			: base(newON.HashCode)
		{
			this._list.Add(oldON);
			this._list.Add(newON);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0003A37C File Offset: 0x0003857C
		internal AMDHObjectNode GetNextNode(ref int index)
		{
			AMDHObjectNode amdhobjectNode2;
			lock (this._list)
			{
				AMDHObjectNode amdhobjectNode = null;
				if (index < this.Count)
				{
					amdhobjectNode = this._list[index++];
					while (index < this.Count && (amdhobjectNode == null || amdhobjectNode.GetCacheItem() == null))
					{
						amdhobjectNode = this._list[index++];
					}
				}
				amdhobjectNode2 = amdhobjectNode;
			}
			return amdhobjectNode2;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0003A40C File Offset: 0x0003860C
		internal void Put(ref MDHOperationInfo operationInfo)
		{
			AMDHObjectNode amdhobjectNode = null;
			object searchKey = operationInfo.SearchKey;
			Monitor.Enter(this._list);
			bool flag = true;
			try
			{
				operationInfo.TryReleaseLatch();
				int num = this.LocateIndex(searchKey);
				if (num != -1)
				{
					amdhobjectNode = this._list[num];
				}
				AMDHObjectNode amdhobjectNode2 = null;
				if (operationInfo.ParentHashTable.PreProcess(amdhobjectNode, ref amdhobjectNode2, ref operationInfo))
				{
					operationInfo.PreOperationCallBack();
					this.Replace(num, amdhobjectNode2);
					Monitor.Exit(this._list);
					flag = false;
					operationInfo.OperationDone();
					operationInfo.PostOperationCallBack();
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this._list);
				}
			}
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0003A4AC File Offset: 0x000386AC
		internal void Put(object key, AMDHObjectNode node)
		{
			lock (this._list)
			{
				int num = this.LocateIndex(key);
				ReleaseAssert.IsTrue(num != -1);
				this.Replace(num, node);
			}
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0003A504 File Offset: 0x00038704
		private int LocateIndex(object key)
		{
			return this._list.FindIndex((AMDHObjectNode N) => N != null && N.CompareKey(key));
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0003A537 File Offset: 0x00038737
		private void Replace(int index, AMDHObjectNode newObjectNode)
		{
			if (index == -1)
			{
				ReleaseAssert.IsTrue(newObjectNode != null);
				this._list.Add(newObjectNode);
				return;
			}
			this._list[index] = newObjectNode;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0003A564 File Offset: 0x00038764
		internal override object Get(object key, int hashKey)
		{
			AMDHObjectNode objectNode = this.GetObjectNode(key, hashKey);
			if (objectNode != null)
			{
				return objectNode.GetCacheItem();
			}
			return null;
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0003A585 File Offset: 0x00038785
		internal override int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0003A592 File Offset: 0x00038792
		internal override bool CompareKey(object key)
		{
			return null != this.GetObjectNode(key);
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0003A5A4 File Offset: 0x000387A4
		internal override bool GetBatch(IScanner info, BaseEnumeratorState state)
		{
			int i = state.Index;
			lock (this._list)
			{
				while (i < this._list.Count)
				{
					if (this._list[i] != null)
					{
						ADMCacheItem dmcacheItem = this._list[i].DMCacheItem;
						if (dmcacheItem != null)
						{
							bool flag2 = info.Scan(dmcacheItem);
							if (info.BatchCompleted)
							{
								state.Index = (flag2 ? (i + 1) : i);
								return false;
							}
						}
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x0003A22E File Offset: 0x0003842E
		internal override MDHNodeType NodeType
		{
			get
			{
				return MDHNodeType.MDHConflictingHashNode;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x0003A648 File Offset: 0x00038848
		// (set) Token: 0x060012D3 RID: 4819 RVA: 0x0003A650 File Offset: 0x00038850
		internal MDHDirectoryNode Parent
		{
			get
			{
				return this._parent;
			}
			set
			{
				this._parent = value;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0003A659 File Offset: 0x00038859
		// (set) Token: 0x060012D5 RID: 4821 RVA: 0x0003A661 File Offset: 0x00038861
		internal int ParentIndex
		{
			get
			{
				return this._parentIndex;
			}
			set
			{
				this._parentIndex = value;
			}
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0003A66A File Offset: 0x0003886A
		private static bool IsNotNull(AMDHObjectNode node)
		{
			return node != null;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0003A674 File Offset: 0x00038874
		internal bool DeleteConflictingNode()
		{
			bool flag2;
			lock (this._list)
			{
				int num = -1;
				Predicate<AMDHObjectNode> predicate = new Predicate<AMDHObjectNode>(MDHConflictingHashNode.IsNotNull);
				int num2 = this._list.Count - this.GetNullCount();
				if (num2 > 1)
				{
					flag2 = false;
				}
				else
				{
					if (num2 == 1)
					{
						num = this._list.FindIndex(predicate);
					}
					if (num2 == 0)
					{
						this._parent.PutNodeInSlot(this._parentIndex, null);
						flag2 = true;
					}
					else
					{
						MDHNode mdhnode = this._list[num];
						this._parent.PutNodeInSlot(this._parentIndex, mdhnode);
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0003A72C File Offset: 0x0003892C
		private int GetNullCount()
		{
			int num = 0;
			for (int i = 0; i < this._list.Count; i++)
			{
				if (this._list[i] == null)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0003A764 File Offset: 0x00038964
		internal override bool CanNodeBeMoved()
		{
			bool flag2;
			lock (this._list)
			{
				int num = this._list.FindIndex((AMDHObjectNode node) => node != null && !node.CanNodeBeMoved());
				flag2 = num != -1;
			}
			return flag2;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0003A7D0 File Offset: 0x000389D0
		internal override void VerifyState()
		{
			if (Monitor.TryEnter(this._list))
			{
				try
				{
					for (int i = 0; i < this._list.Count; i++)
					{
						if (this._list[i] != null)
						{
							this._list[i].VerifyState();
						}
					}
					return;
				}
				finally
				{
					Monitor.Exit(this._list);
				}
			}
			throw new InvalidOperationException("List is locked");
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00003CAB File Offset: 0x00001EAB
		internal override object GetData(FixedDepthEnumeratorState state, int level)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000B5E RID: 2910
		private List<AMDHObjectNode> _list = new List<AMDHObjectNode>();

		// Token: 0x04000B5F RID: 2911
		private MDHDirectoryNode _parent;

		// Token: 0x04000B60 RID: 2912
		private int _parentIndex = -1;
	}
}
