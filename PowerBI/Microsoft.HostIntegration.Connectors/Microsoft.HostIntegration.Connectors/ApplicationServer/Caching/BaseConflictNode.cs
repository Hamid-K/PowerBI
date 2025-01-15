using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001FB RID: 507
	internal sealed class BaseConflictNode : MDHLeafNode
	{
		// Token: 0x06001080 RID: 4224 RVA: 0x00037022 File Offset: 0x00035222
		internal BaseConflictNode(int hashCode)
			: base(hashCode)
		{
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00037038 File Offset: 0x00035238
		internal override object Get(object searchKey, int hashCode)
		{
			if (base.HashCode == hashCode)
			{
				lock (this._list)
				{
					for (int i = 0; i < this._list.Count; i++)
					{
						IBaseDataNode baseDataNode = this._list[i];
						if (baseDataNode.Key != null && searchKey.Equals(baseDataNode.Key))
						{
							return baseDataNode.Data;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x000370C4 File Offset: 0x000352C4
		internal override bool CompareKey(object searchKey)
		{
			lock (this._list)
			{
				for (int i = 0; i < this._list.Count; i++)
				{
					IBaseDataNode baseDataNode = this._list[i];
					if (baseDataNode.Key != null && searchKey.Equals(baseDataNode.Key))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00037140 File Offset: 0x00035340
		internal void Put(IBaseDataNode slotNode)
		{
			lock (this._list)
			{
				this._list.Add(slotNode);
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00037188 File Offset: 0x00035388
		internal bool Put(ref DMOperationInfo operationInfo)
		{
			bool flag = false;
			IBaseDataNode baseDataNode = null;
			lock (this._list)
			{
				for (int i = 0; i < this._list.Count; i++)
				{
					IBaseDataNode baseDataNode2 = this._list[i];
					if (baseDataNode2.Key != null && operationInfo.SearchKey.Equals(baseDataNode2.Key))
					{
						baseDataNode = baseDataNode2;
						break;
					}
				}
				IBaseDataNode baseDataNode3 = null;
				operationInfo.Operation.PreProcess(baseDataNode, ref baseDataNode3, ref operationInfo);
				if (baseDataNode == null)
				{
					if (baseDataNode3 != null)
					{
						this._list.Add(baseDataNode3);
					}
				}
				else
				{
					this._list.Remove(baseDataNode);
					if (baseDataNode3 != null)
					{
						this._list.Add(baseDataNode3);
					}
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00037258 File Offset: 0x00035458
		internal object NextData(ref int index)
		{
			IBaseDataNode baseDataNode = null;
			lock (this._list)
			{
				while (this._list.Count > index)
				{
					IBaseDataNode baseDataNode2 = this._list[index];
					if (baseDataNode2.Key != null)
					{
						baseDataNode = baseDataNode2;
						index++;
						break;
					}
					index++;
				}
			}
			return baseDataNode;
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x000372CC File Offset: 0x000354CC
		internal override bool GetBatch(IScanner info, BaseEnumeratorState state)
		{
			int i = state.Index;
			lock (this._list)
			{
				while (i < this._list.Count)
				{
					if (this._list[i].Key != null)
					{
						bool flag2 = info.Scan(this._list[i].Data);
						if (info.BatchCompleted)
						{
							state.Index = (flag2 ? (i + 1) : i);
							return false;
						}
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0003736C File Offset: 0x0003556C
		internal override object GetData(FixedDepthEnumeratorState state, int level)
		{
			object obj;
			lock (this._list)
			{
				obj = this._list[state.Index];
			}
			return obj;
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x000373BC File Offset: 0x000355BC
		internal override int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x000373C9 File Offset: 0x000355C9
		internal override MDHNodeType NodeType
		{
			get
			{
				return MDHNodeType.BaseConflictNode;
			}
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00002B16 File Offset: 0x00000D16
		internal override bool CanNodeBeMoved()
		{
			return true;
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x000036A9 File Offset: 0x000018A9
		internal override void VerifyState()
		{
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x000373CC File Offset: 0x000355CC
		internal BaseConflictNode Compact()
		{
			BaseConflictNode baseConflictNode2;
			lock (this._list)
			{
				if (this.HasNullElement())
				{
					BaseConflictNode baseConflictNode = new BaseConflictNode(base.HashCode);
					for (int i = 0; i < this._list.Count; i++)
					{
						if (this._list[i].Key != null)
						{
							baseConflictNode._list.Add(this._list[i]);
						}
					}
					if (baseConflictNode._list.Count == 0)
					{
						baseConflictNode2 = null;
					}
					else
					{
						baseConflictNode2 = baseConflictNode;
					}
				}
				else
				{
					baseConflictNode2 = this;
				}
			}
			return baseConflictNode2;
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00037474 File Offset: 0x00035674
		private bool HasNullElement()
		{
			for (int i = 0; i < this._list.Count; i++)
			{
				if (this._list[i].Key == null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x000374AD File Offset: 0x000356AD
		internal void DeleteConflictingNode()
		{
			this.Compact();
		}

		// Token: 0x04000ACC RID: 2764
		private List<IBaseDataNode> _list = new List<IBaseDataNode>();
	}
}
