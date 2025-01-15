using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200021B RID: 539
	internal sealed class DMMultiLevelHashTable : IMultiLevelHashTable, IObjectCreator
	{
		// Token: 0x060011E0 RID: 4576 RVA: 0x00038F79 File Offset: 0x00037179
		public DMMultiLevelHashTable(int level, IDirectoryNodeFactory directoryNodeFactory)
		{
			this._level = level;
			this._directoryNodeFactory = directoryNodeFactory;
			this._rootHashTable = this.GetRootLevelHashTable();
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00038F9B File Offset: 0x0003719B
		public DMMultiLevelHashTable(IIndexStoreSchema iSchema, IDirectoryNodeFactory directoryNodeFactory)
		{
			this._level = iSchema.Level;
			this._directoryNodeFactory = directoryNodeFactory;
			this._rootHashTable = this.GetRootLevelHashTable();
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x00038FC2 File Offset: 0x000371C2
		public int Level
		{
			get
			{
				return this._level;
			}
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00038FCA File Offset: 0x000371CA
		private BaseHashTable GetRootLevelHashTable()
		{
			return new BaseHashTable(this._directoryNodeFactory);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00038FCA File Offset: 0x000371CA
		private BaseHashTable GetSubLevelHashTable()
		{
			return new BaseHashTable(this._directoryNodeFactory);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00038FD8 File Offset: 0x000371D8
		public void Add(object[] LevelwiseKeys, object obj)
		{
			BaseHashTable leafHashTable = this.GetLeafHashTable(LevelwiseKeys);
			leafHashTable.Upsert(LevelwiseKeys[this._level - 1], obj);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00039000 File Offset: 0x00037200
		private BaseHashTable GetLeafHashTable(object[] LevelwiseKeys)
		{
			BaseHashTable baseHashTable = this._rootHashTable;
			for (int i = 0; i < this._level - 1; i++)
			{
				baseHashTable = (BaseHashTable)baseHashTable.AddOrGet(LevelwiseKeys[i], this);
			}
			return baseHashTable;
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00039038 File Offset: 0x00037238
		public object Upsert(object[] LevelwiseKeys, object obj)
		{
			BaseHashTable leafHashTable = this.GetLeafHashTable(LevelwiseKeys);
			return leafHashTable.Upsert(LevelwiseKeys[this._level - 1], obj);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00039060 File Offset: 0x00037260
		public object Delete(object[] LevelwiseKeys)
		{
			BaseHashTable baseHashTable = this._rootHashTable;
			for (int i = 0; i < this._level - 1; i++)
			{
				baseHashTable = (BaseHashTable)baseHashTable.Get(LevelwiseKeys[i]);
				if (baseHashTable == null)
				{
					return null;
				}
			}
			return baseHashTable.Delete(LevelwiseKeys[this._level - 1]);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x000390AC File Offset: 0x000372AC
		public IEnumerator Find(object[] LevelwiseKeys)
		{
			if (LevelwiseKeys.Length == this._level)
			{
				return new DMSingleObjectEnumerator(this.Get(LevelwiseKeys));
			}
			BaseHashTable baseHashTable = this._rootHashTable;
			for (int i = 0; i < LevelwiseKeys.Length; i++)
			{
				baseHashTable = (BaseHashTable)baseHashTable.Get(LevelwiseKeys[i]);
				if (baseHashTable == null)
				{
					return new DMSingleObjectEnumerator(null);
				}
			}
			return new DMMultiLevelHashTableEnumerator(baseHashTable);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00039108 File Offset: 0x00037308
		public IEnumerator FindUnion(List<object[]> listLevelwiseKeys)
		{
			List<IEnumerator> list = this.PrepareList(listLevelwiseKeys);
			return new DMMultiLevelUnionEnumerator(list, this._directoryNodeFactory);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x0003912C File Offset: 0x0003732C
		public IEnumerator FindIntersection(List<object[]> listLevelwiseKeys)
		{
			List<IEnumerator> list = this.PrepareList(listLevelwiseKeys);
			return new DMMultiLevelIntersectionEnumerator(list, this._directoryNodeFactory);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00039150 File Offset: 0x00037350
		private List<IEnumerator> PrepareList(List<object[]> listLevelwiseKeys)
		{
			List<IEnumerator> list = new List<IEnumerator>(16);
			foreach (object[] array in listLevelwiseKeys)
			{
				IEnumerator enumerator2 = this.Find(array);
				list.Add(enumerator2);
			}
			return list;
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x000391B0 File Offset: 0x000373B0
		public object GetObject()
		{
			return this.GetSubLevelHashTable();
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x000391B8 File Offset: 0x000373B8
		public object Get(object[] LevelwiseKeys)
		{
			BaseHashTable baseHashTable = this._rootHashTable;
			for (int i = 0; i < this._level - 1; i++)
			{
				baseHashTable = (BaseHashTable)baseHashTable.Get(LevelwiseKeys[i]);
				if (baseHashTable == null)
				{
					return null;
				}
			}
			return baseHashTable.Get(LevelwiseKeys[this._level - 1]);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00039203 File Offset: 0x00037403
		public IIndexStoreSchema GetSchema()
		{
			return new DMMultiLevelHashTableSchema(this._level);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00039203 File Offset: 0x00037403
		IIndexStoreSchema IMultiLevelHashTable.GetSchema()
		{
			return new DMMultiLevelHashTableSchema(this._level);
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00039210 File Offset: 0x00037410
		public EnumeratorState FindWithStatelessEnumerator(object[] levelwiseKeys)
		{
			BaseHashTable baseHashTable = this._rootHashTable;
			for (int i = 0; i < levelwiseKeys.Length; i++)
			{
				baseHashTable = (BaseHashTable)baseHashTable.Get(levelwiseKeys[i]);
				if (baseHashTable == null)
				{
					return new FindEnumeratorState();
				}
			}
			List<FixedDepthEnumeratorState> list = null;
			int num = this._level - (levelwiseKeys.Length + 1);
			if (num > 0)
			{
				list = new List<FixedDepthEnumeratorState>(this._level - (levelwiseKeys.Length + 1));
				for (int j = 0; j < num; j++)
				{
					FixedDepthEnumeratorState fixedDepthEnumerator = baseHashTable.GetFixedDepthEnumerator();
					list.Add(fixedDepthEnumerator);
					BaseHashTable baseHashTable2 = (BaseHashTable)baseHashTable.GetData(fixedDepthEnumerator);
					if (baseHashTable2 == null)
					{
						baseHashTable2 = (BaseHashTable)baseHashTable.MoveNextAndGetData(fixedDepthEnumerator);
					}
					if (baseHashTable2 == null)
					{
						return new FindEnumeratorState();
					}
					baseHashTable = baseHashTable2;
				}
			}
			EnumeratorState statelessEnumeratorState = baseHashTable.GetStatelessEnumeratorState();
			return new FindEnumeratorState(this, levelwiseKeys, list, (BaseEnumeratorState)statelessEnumeratorState);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000392D5 File Offset: 0x000374D5
		public EnumeratorState UnionAllWithStatelessEnumerator(List<object[]> listlevelwiseKeys)
		{
			return new UnionAllEnumeratorState(listlevelwiseKeys, this);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x000392E0 File Offset: 0x000374E0
		private static Stack<StackElement> LoadStack(List<FixedDepthEnumeratorState> list, BaseHashTable hashTable)
		{
			if (list == null)
			{
				return null;
			}
			Stack<StackElement> stack = new Stack<StackElement>(list.Count);
			BaseHashTable baseHashTable = hashTable;
			for (int i = 0; i < list.Count; i++)
			{
				FixedDepthEnumeratorState fixedDepthEnumeratorState = list[i];
				if (!fixedDepthEnumeratorState.IsValidState(baseHashTable))
				{
					fixedDepthEnumeratorState = baseHashTable.GetFixedDepthEnumerator();
				}
				stack.Push(new StackElement(fixedDepthEnumeratorState, baseHashTable));
				BaseHashTable baseHashTable2 = (BaseHashTable)baseHashTable.GetData(fixedDepthEnumeratorState);
				if (baseHashTable2 == null)
				{
					baseHashTable2 = (BaseHashTable)baseHashTable.MoveNextAndGetData(fixedDepthEnumeratorState);
				}
				if (baseHashTable2 == null)
				{
					return null;
				}
				baseHashTable = baseHashTable2;
			}
			return stack;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00039364 File Offset: 0x00037564
		private static void UnLoadStack(List<FixedDepthEnumeratorState> list, Stack<StackElement> stack)
		{
			if (list == null)
			{
				return;
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				list[i] = stack.Pop().State;
			}
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0003939C File Offset: 0x0003759C
		public bool GetBatch(IScanner scanner, EnumeratorState enumeratorState)
		{
			FindEnumeratorState findEnumeratorState = enumeratorState as FindEnumeratorState;
			if (findEnumeratorState != null)
			{
				return this.GetBatchWithFind(scanner, findEnumeratorState);
			}
			UnionAllEnumeratorState unionAllEnumeratorState = enumeratorState as UnionAllEnumeratorState;
			if (unionAllEnumeratorState != null)
			{
				return this.GetBatchWithUnionALL(scanner, unionAllEnumeratorState);
			}
			ReleaseAssert.Fail("Unexpected enumerator state.", new object[0]);
			return false;
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x000393E0 File Offset: 0x000375E0
		private bool GetBatchWithUnionALL(IScanner scanner, UnionAllEnumeratorState state)
		{
			if (state.Exhausted)
			{
				return true;
			}
			for (;;)
			{
				this.GetBatchWithFind(scanner, state.CurrentState);
				if (scanner.BatchCompleted)
				{
					break;
				}
				state.ListKeys[state.CurrentIndex] = null;
				state.CurrentIndex++;
				if (state.CurrentIndex == state.ListKeys.Count)
				{
					goto IL_007E;
				}
				state.CurrentState = (FindEnumeratorState)this.FindWithStatelessEnumerator(state.ListKeys[state.CurrentIndex]);
			}
			return state.Exhausted;
			IL_007E:
			state.Exhausted = true;
			return state.Exhausted;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00039478 File Offset: 0x00037678
		private bool GetBatchWithFind(IScanner scanner, FindEnumeratorState state)
		{
			if (state.Exhausted)
			{
				return true;
			}
			BaseHashTable baseHashTable = this.FindWithKey(state);
			Stack<StackElement> stack = null;
			for (;;)
			{
				baseHashTable = this.GetLeafHashTableFromList(state, baseHashTable, ref stack);
				if (baseHashTable != null)
				{
					BaseEnumeratorState baseEnumeratorState = state.CurrentState;
					if (!baseEnumeratorState.IsValidState(baseHashTable))
					{
						baseEnumeratorState = (state.CurrentState = (BaseEnumeratorState)baseHashTable.GetStatelessEnumeratorState());
					}
					baseHashTable.GetBatch(scanner, baseEnumeratorState);
					if (scanner.BatchCompleted)
					{
						break;
					}
				}
				if (!this.MoveNextInList(state, stack))
				{
					goto Block_5;
				}
			}
			DMMultiLevelHashTable.UnLoadStack(state.List, stack);
			return state.Exhausted;
			Block_5:
			state.Exhausted = true;
			return state.Exhausted;
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0003950C File Offset: 0x0003770C
		private bool MoveNextInList(FindEnumeratorState state, Stack<StackElement> stack)
		{
			if (stack == null || state.List == null)
			{
				return false;
			}
			while (stack.Count > 0)
			{
				StackElement stackElement = stack.Peek();
				if (stackElement.MoveNextAndGetData() != null)
				{
					return true;
				}
				stack.Pop();
				if (!this.ConstructStack(stack))
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00039554 File Offset: 0x00037754
		private BaseHashTable GetLeafHashTableFromList(FindEnumeratorState state, BaseHashTable table, ref Stack<StackElement> stack)
		{
			if (state.List == null)
			{
				return table;
			}
			if (stack == null)
			{
				stack = DMMultiLevelHashTable.LoadStack(state.List, table);
			}
			if (stack == null)
			{
				return null;
			}
			while (stack.Count > 0)
			{
				StackElement stackElement = stack.Peek();
				BaseHashTable baseHashTable = (BaseHashTable)stackElement.GetData();
				if (baseHashTable != null)
				{
					return baseHashTable;
				}
				baseHashTable = (BaseHashTable)stackElement.MoveNextAndGetData();
				if (baseHashTable != null)
				{
					return baseHashTable;
				}
				stack.Pop();
				if (!this.ConstructStack(stack))
				{
					return null;
				}
			}
			return null;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x000395CC File Offset: 0x000377CC
		private bool ConstructStack(Stack<StackElement> stack)
		{
			while (stack.Count > 0)
			{
				StackElement stackElement = stack.Peek();
				object obj = stackElement.MoveNextAndGetData();
				BaseHashTable baseHashTable = obj as BaseHashTable;
				if (baseHashTable != null)
				{
					FixedDepthEnumeratorState fixedDepthEnumerator = baseHashTable.GetFixedDepthEnumerator();
					stack.Push(new StackElement(fixedDepthEnumerator, baseHashTable));
					return true;
				}
				stack.Pop();
				if (!this.ConstructStack(stack))
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00039625 File Offset: 0x00037825
		private BaseHashTable FindWithKey(FindEnumeratorState findEnumeratorState)
		{
			return this.FindObject(findEnumeratorState.Keys);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00039634 File Offset: 0x00037834
		private BaseHashTable FindObject(object[] keys)
		{
			BaseHashTable baseHashTable = this._rootHashTable;
			for (int i = 0; i < keys.Length; i++)
			{
				baseHashTable = (BaseHashTable)baseHashTable.Get(keys[i]);
				if (baseHashTable == null)
				{
					return null;
				}
			}
			return baseHashTable;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0003966C File Offset: 0x0003786C
		public int DoCompaction()
		{
			int num = this._rootHashTable.DoCompaction();
			IEnumerator enumerator = this._rootHashTable.GetEnumerator();
			Stack<IEnumerator> stack = new Stack<IEnumerator>(this._level - 1);
			stack.Push(enumerator);
			while (stack.Count != 0)
			{
				int count = stack.Count;
				IEnumerator enumerator2 = stack.Pop();
				while (enumerator2.MoveNext())
				{
					object obj = enumerator2.Current;
					BaseHashTable baseHashTable = (BaseHashTable)obj;
					num += baseHashTable.DoCompaction();
					if (count < this._level - 1)
					{
						stack.Push(enumerator2);
						stack.Push(baseHashTable.GetEnumerator());
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x04000B06 RID: 2822
		private readonly int _level;

		// Token: 0x04000B07 RID: 2823
		private readonly BaseHashTable _rootHashTable;

		// Token: 0x04000B08 RID: 2824
		private IDirectoryNodeFactory _directoryNodeFactory;
	}
}
