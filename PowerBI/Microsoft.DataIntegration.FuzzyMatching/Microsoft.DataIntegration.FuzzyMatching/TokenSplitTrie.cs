using System;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200010A RID: 266
	[Serializable]
	internal class TokenSplitTrie
	{
		// Token: 0x06000B08 RID: 2824 RVA: 0x000310EC File Offset: 0x0002F2EC
		public void BeginUpdate()
		{
			this.gotoFn = new FastInt64ToIntHash();
			this.prevWordEndUpdate = new List<int>();
			this.depthUpdate = new List<int>();
			this.wordIdUpdate = new List<int>();
			this.numStates = 1;
			this.prevWordEndUpdate.Add(-1);
			this.depthUpdate.Add(0);
			this.wordIdUpdate.Add(-1);
			this.curState = 0;
			this.curDepth = 0;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0003115E File Offset: 0x0002F35E
		public void Reset()
		{
			this.curState = 0;
			this.curDepth = 0;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00031170 File Offset: 0x0002F370
		public void Add(string word, int id)
		{
			int num = this.curState;
			for (int i = 0; i < word.Length; i++)
			{
				this.curState = this.UGoto(this.curState, word.get_Chars(i));
			}
			this.curDepth++;
			if (this.prevWordEndUpdate[this.curState] == -1 || this.depthUpdate[this.curState] < this.curDepth)
			{
				this.prevWordEndUpdate[this.curState] = num;
				this.depthUpdate[this.curState] = this.curDepth;
				this.wordIdUpdate[this.curState] = id;
			}
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00031224 File Offset: 0x0002F424
		public void Add(StringExtent word, int id)
		{
			int num = this.curState;
			for (int i = 0; i < word.Length; i++)
			{
				this.curState = this.UGoto(this.curState, word[i]);
			}
			this.curDepth++;
			if (this.prevWordEndUpdate[this.curState] == -1 || this.depthUpdate[this.curState] < this.curDepth)
			{
				this.prevWordEndUpdate[this.curState] = num;
				this.depthUpdate[this.curState] = this.curDepth;
				this.wordIdUpdate[this.curState] = id;
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000312DC File Offset: 0x0002F4DC
		public void EndUpdate()
		{
			this.prevWordEnd = this.prevWordEndUpdate.ToArray();
			this.wordId = this.wordIdUpdate.ToArray();
			this.prevWordEndUpdate = null;
			this.depthUpdate = null;
			this.wordIdUpdate = null;
			this.lookupStack = new Stack<int>();
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0003132C File Offset: 0x0002F52C
		public bool ResetLookup(string word)
		{
			int num = 0;
			int num2 = 0;
			while (num != -1 && num2 < word.Length)
			{
				num = this.Goto(num, word.get_Chars(num2));
				num2++;
			}
			this.lookupStack.Clear();
			if (num == -1 || this.prevWordEnd[num] == 0)
			{
				return false;
			}
			while (num != -1)
			{
				this.lookupStack.Push(this.wordId[num]);
				num = this.prevWordEnd[num];
			}
			return true;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0003139C File Offset: 0x0002F59C
		public bool ResetLookup(StringExtent word)
		{
			int num = 0;
			int num2 = 0;
			while (num != -1 && num2 < word.Length)
			{
				num = this.Goto(num, word[num2]);
				num2++;
			}
			this.lookupStack.Clear();
			if (num == -1 || this.prevWordEnd[num] <= 0)
			{
				return false;
			}
			while (num != -1)
			{
				this.lookupStack.Push(this.wordId[num]);
				num = this.prevWordEnd[num];
			}
			return true;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0003140E File Offset: 0x0002F60E
		public bool GetNext()
		{
			this.lookupStack.Pop();
			return this.lookupStack.Count > 0;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0003142A File Offset: 0x0002F62A
		public int GetCurrent()
		{
			return this.lookupStack.Peek();
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00031438 File Offset: 0x0002F638
		private int UGoto(int state, char token)
		{
			int num;
			if (!this.gotoFn.TryGetValue(state, (int)token, out num))
			{
				int num2 = this.numStates;
				this.numStates = num2 + 1;
				num = num2;
				this.gotoFn.Add(state, (int)token, num);
				this.prevWordEndUpdate.Add(-1);
				this.depthUpdate.Add(0);
				this.wordIdUpdate.Add(-1);
			}
			return num;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0003149C File Offset: 0x0002F69C
		private int Goto(int state, char token)
		{
			int num;
			if (!this.gotoFn.TryGetValue(state, (int)token, out num))
			{
				num = ((state == 0) ? 0 : (-1));
			}
			return num;
		}

		// Token: 0x0400042E RID: 1070
		private int numStates;

		// Token: 0x0400042F RID: 1071
		private FastInt64ToIntHash gotoFn;

		// Token: 0x04000430 RID: 1072
		private int curState;

		// Token: 0x04000431 RID: 1073
		private int curDepth;

		// Token: 0x04000432 RID: 1074
		private List<int> prevWordEndUpdate;

		// Token: 0x04000433 RID: 1075
		private List<int> depthUpdate;

		// Token: 0x04000434 RID: 1076
		private List<int> wordIdUpdate;

		// Token: 0x04000435 RID: 1077
		private int[] prevWordEnd;

		// Token: 0x04000436 RID: 1078
		private int[] wordId;

		// Token: 0x04000437 RID: 1079
		private Stack<int> lookupStack;
	}
}
