using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200023A RID: 570
	internal class MDHDirectoryNode : MDHNode
	{
		// Token: 0x060012E1 RID: 4833 RVA: 0x0003A87E File Offset: 0x00038A7E
		private int GetIndex(uint path)
		{
			return this.GetSlotNumber((int)path);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0003A888 File Offset: 0x00038A88
		internal uint GetPathForIndex(int index, uint path)
		{
			if (this._slots.Length == index)
			{
				index = 0;
			}
			uint num = (uint)((uint)index << MDHGlobals.GetLSBOffset(this._maskOffset));
			uint num2 = path & (uint)(~(uint)MDHGlobals.IndexMasks[(int)this._maskOffset]);
			return num2 | num;
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0003A8C7 File Offset: 0x00038AC7
		internal MDHDirectoryNode(short maskOffset, MDHDirectoryNode parent, short parentIndex)
		{
			this._maskOffset = maskOffset;
			this._slots = new MDHNode[16];
			this._parent = parent;
			this._parentIndex = parentIndex;
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0003A8F1 File Offset: 0x00038AF1
		internal void Init(short maskOffset, MDHDirectoryNode parent, short parentIndex)
		{
			this._maskOffset = maskOffset;
			this._parent = parent;
			this._parentIndex = parentIndex;
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0003A908 File Offset: 0x00038B08
		internal MDHDirectoryNode()
		{
			this._slots = new MDHNode[16];
			this._parentIndex = -1;
			this._maskOffset = -1;
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x0003A92B File Offset: 0x00038B2B
		internal override int Count
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0003A930 File Offset: 0x00038B30
		internal void LatchSlot(int index)
		{
			int num = 0;
			while (!this.TryLatchSlot(index))
			{
				Thread.Sleep(1);
				num++;
				if (num > 1000 && num % 1000 == 0 && Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError("DistributedCache.DataManager", "Spining:Mutex not available for slot {0} retried for {1} iterations.", new object[] { index, num });
				}
			}
			if (num > 0 && Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<int, int>("DistributedCache.DataManager", "Got mutex for {0} after {1} retries.", index, num);
			}
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0003A9B4 File Offset: 0x00038BB4
		internal bool TryLatchSlot(int index)
		{
			int num = this._latches;
			int num2 = 1 << index;
			while ((num & num2) == 0)
			{
				if (Interlocked.CompareExchange(ref this._latches, num | num2, num) == num)
				{
					return true;
				}
				num = this._latches;
			}
			return false;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0003A9F4 File Offset: 0x00038BF4
		internal bool IsLatched(int index)
		{
			int latches = this._latches;
			int num = 1 << index;
			return (latches & num) != 0;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0003AA18 File Offset: 0x00038C18
		internal void UnLatchSlot(int index)
		{
			int num = ~(1 << index);
			int latches;
			do
			{
				latches = this._latches;
			}
			while (Interlocked.CompareExchange(ref this._latches, latches & num, latches) != latches);
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0003AA46 File Offset: 0x00038C46
		internal int GetSlotNumber(int hkey)
		{
			return (int)((uint)(hkey & MDHGlobals.IndexMasks[(int)this._maskOffset]) >> MDHGlobals.GetLSBOffset(this._maskOffset));
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0003AA65 File Offset: 0x00038C65
		internal MDHNode GetNodeInSlot(int slot)
		{
			return this._slots[slot];
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0003AA6F File Offset: 0x00038C6F
		internal void PutNodeInSlot(int slot, MDHNode node)
		{
			this._slots[slot] = node;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0003AA7A File Offset: 0x00038C7A
		internal short GetNextNode(short index, out MDHNode node)
		{
			node = null;
			if ((int)index < this.Count)
			{
				short num = index;
				index = num + 1;
				node = this.GetNodeInSlot((int)num);
				while ((int)index < this.Count && node == null)
				{
					short num2 = index;
					index = num2 + 1;
					node = this.GetNodeInSlot((int)num2);
				}
			}
			return index;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0003AAB8 File Offset: 0x00038CB8
		internal int PutNode(MDHLeafNode node)
		{
			int slotNumber = this.GetSlotNumber(node.HashCode);
			this._slots[slotNumber] = node;
			return slotNumber;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0003AADC File Offset: 0x00038CDC
		internal override bool GetBatch(IScanner info, BaseEnumeratorState state)
		{
			int num = this.GetIndex(state.Path);
			bool flag = true;
			while (num < this._slots.Length && !info.BatchCompleted)
			{
				MDHNode mdhnode = this._slots[num];
				if (mdhnode != null)
				{
					flag = mdhnode.GetBatch(info, state);
					if (!flag)
					{
						break;
					}
				}
				state.Index = 0;
				num++;
			}
			state.Path = this.GetPathForIndex(num, state.Path);
			return flag && num == this._slots.Length;
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00002B16 File Offset: 0x00000D16
		internal override MDHNodeType NodeType
		{
			get
			{
				return MDHNodeType.MDHDirectoryNode;
			}
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0003AB54 File Offset: 0x00038D54
		internal int GetCountOfNonEmptyNodes(ref int nonEmptySlotIndex)
		{
			int num = 0;
			for (int i = 0; i < this._slots.Length; i++)
			{
				if (this._slots[i] != null)
				{
					nonEmptySlotIndex = i;
					num++;
					if (num > 1)
					{
						nonEmptySlotIndex = -1;
						break;
					}
				}
			}
			if (num > 1)
			{
				nonEmptySlotIndex = -1;
			}
			return num;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0003AB98 File Offset: 0x00038D98
		internal int GetCountOfNonEmptyNodesAndLockOneNode(ref int nonEmptySlotIndex)
		{
			int num = 0;
			for (int i = 0; i < this._slots.Length; i++)
			{
				if (this._slots[i] != null)
				{
					nonEmptySlotIndex = i;
					num++;
					if (num > 1)
					{
						nonEmptySlotIndex = -1;
						break;
					}
				}
			}
			if (num == 1)
			{
				if (!this.TryLatchSlot(nonEmptySlotIndex))
				{
					num = 2;
					nonEmptySlotIndex = -1;
				}
				else if (this._slots[nonEmptySlotIndex] == null)
				{
					this.UnLatchSlot(nonEmptySlotIndex);
					num = 2;
					nonEmptySlotIndex = -1;
				}
			}
			else
			{
				nonEmptySlotIndex = -1;
			}
			return num;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0003AC08 File Offset: 0x00038E08
		internal bool DeleteDirectory(short parentIndex)
		{
			int num = -1;
			if (this._parent == null)
			{
				return false;
			}
			int countOfNonEmptyNodes = this.GetCountOfNonEmptyNodes(ref num);
			if (countOfNonEmptyNodes > 1)
			{
				return false;
			}
			this._parent.AtomicallyPutNodeInSlot((int)parentIndex, this._parent);
			int countOfNonEmptyNodesAndLockOneNode = this.GetCountOfNonEmptyNodesAndLockOneNode(ref num);
			if (countOfNonEmptyNodesAndLockOneNode > 1)
			{
				this._parent.AtomicallyPutNodeInSlot((int)parentIndex, this);
				return false;
			}
			if (countOfNonEmptyNodesAndLockOneNode == 0)
			{
				this._parent.AtomicallyPutNodeInSlot((int)parentIndex, null);
				return true;
			}
			MDHNode nodeInSlot = this.GetNodeInSlot(num);
			if (nodeInSlot.CanNodeBeMoved())
			{
				if (nodeInSlot.NodeType == MDHNodeType.MDHConflictingHashNode)
				{
					MDHConflictingHashNode mdhconflictingHashNode = (MDHConflictingHashNode)nodeInSlot;
					mdhconflictingHashNode.Parent = this._parent;
					mdhconflictingHashNode.ParentIndex = (int)this._parentIndex;
				}
				else if (nodeInSlot.NodeType == MDHNodeType.MDHDirectoryNode)
				{
					MDHDirectoryNode mdhdirectoryNode = (MDHDirectoryNode)nodeInSlot;
					mdhdirectoryNode.ParentDir = this._parent;
					mdhdirectoryNode.ParentIndex = parentIndex;
				}
				this._parent.AtomicallyPutNodeInSlot((int)parentIndex, nodeInSlot);
				this.UnLatchSlot(num);
				return true;
			}
			this._parent.AtomicallyPutNodeInSlot((int)parentIndex, this);
			this.UnLatchSlot(num);
			return false;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0003AD00 File Offset: 0x00038F00
		internal void DoCompactionOnSubDirectories(short dirIndex, ref int delCount, ref long lastCompactionEpoch)
		{
			short num = 0;
			while ((int)num < this.Count)
			{
				MDHNode mdhnode;
				num = this.GetNextNode(num, out mdhnode);
				if (mdhnode != null)
				{
					MDHDirectoryNode mdhdirectoryNode = null;
					if (mdhnode.NodeType == MDHNodeType.MDHDirectoryNode)
					{
						mdhdirectoryNode = (MDHDirectoryNode)mdhnode;
					}
					if (mdhdirectoryNode != null)
					{
						mdhdirectoryNode.DoCompactionOnSubDirectories(num - 1, ref delCount, ref lastCompactionEpoch);
					}
					else
					{
						MDHConflictingHashNode mdhconflictingHashNode = null;
						if (mdhnode.NodeType == MDHNodeType.MDHConflictingHashNode)
						{
							mdhconflictingHashNode = (MDHConflictingHashNode)mdhnode;
						}
						if (mdhconflictingHashNode != null && this.TryLatchSlot((int)(num - 1)))
						{
							try
							{
								if (mdhconflictingHashNode.DeleteConflictingNode())
								{
									Interlocked.Increment(ref lastCompactionEpoch);
								}
							}
							finally
							{
								this.UnLatchSlot((int)(num - 1));
							}
						}
					}
				}
			}
			if (dirIndex != -1 && this.DeleteDirectory(dirIndex))
			{
				Interlocked.Increment(ref lastCompactionEpoch);
				delCount++;
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("DistributedCache.DataManager", "Revoked a subdirectory through compaction", new object[0]);
				}
			}
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0003ADCC File Offset: 0x00038FCC
		internal bool AtomicallyCheckParentalLinkage()
		{
			if (this._parent != null)
			{
				MDHNode nodeInSlot = this._parent.GetNodeInSlot((int)this._parentIndex);
				return Interlocked.CompareExchange<MDHNode>(ref this._parent._slots[(int)this._parentIndex], nodeInSlot, nodeInSlot) == this;
			}
			return true;
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0003AE15 File Offset: 0x00039015
		internal bool AtomicallyCheckParentalLinkage(int slotIndex, MDHNode node)
		{
			return Interlocked.CompareExchange<MDHNode>(ref this._slots[slotIndex], node, node) == node;
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x0003AE2D File Offset: 0x0003902D
		// (set) Token: 0x060012F9 RID: 4857 RVA: 0x0003AE35 File Offset: 0x00039035
		internal MDHDirectoryNode ParentDir
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

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x0003AE3E File Offset: 0x0003903E
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x0003AE46 File Offset: 0x00039046
		internal short ParentIndex
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

		// Token: 0x060012FC RID: 4860 RVA: 0x0003AE50 File Offset: 0x00039050
		internal void AtomicallyPutNodeInSlot(int slot, MDHNode node)
		{
			MDHNode mdhnode;
			do
			{
				mdhnode = this._slots[slot];
			}
			while (Interlocked.CompareExchange<MDHNode>(ref this._slots[slot], node, mdhnode) != mdhnode);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00002B16 File Offset: 0x00000D16
		internal override bool CanNodeBeMoved()
		{
			return true;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0003AE80 File Offset: 0x00039080
		public void Clean()
		{
			if (this._slots != null)
			{
				for (int i = 0; i < this._slots.Length; i++)
				{
					this._slots[i] = null;
				}
			}
			this._maskOffset = -1;
			this._parentIndex = -1;
			this._parent = null;
			this._latches = 0;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0003AECD File Offset: 0x000390CD
		protected bool IsInUse()
		{
			return this._maskOffset != -1;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0003AEDC File Offset: 0x000390DC
		internal override void VerifyState()
		{
			for (int i = 0; i < this._slots.Length; i++)
			{
				if (this.IsLatched(i))
				{
					throw new InvalidOperationException("Slot is latched completely");
				}
				if (this._slots[i] != null)
				{
					this._slots[i].VerifyState();
				}
			}
		}

		// Token: 0x04000B64 RID: 2916
		private short _maskOffset;

		// Token: 0x04000B65 RID: 2917
		private readonly MDHNode[] _slots;

		// Token: 0x04000B66 RID: 2918
		private int _latches;

		// Token: 0x04000B67 RID: 2919
		private MDHDirectoryNode _parent;

		// Token: 0x04000B68 RID: 2920
		private short _parentIndex;

		// Token: 0x0200023B RID: 571
		private enum LatchState
		{
			// Token: 0x04000B6A RID: 2922
			LOCKED = 1,
			// Token: 0x04000B6B RID: 2923
			UNLOCKED = 0
		}
	}
}
