using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x0200007B RID: 123
	internal class LimitedDictionary<TKey, TValue>
	{
		// Token: 0x060003A1 RID: 929 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		public LimitedDictionary(int maxCount)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxCount, "maxCount");
			this.m_dictionary = new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>();
			this.m_linkedList = new LinkedList<KeyValuePair<TKey, TValue>>();
			this.m_maxCount = maxCount;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000E220 File Offset: 0x0000C420
		public bool TryGetValue(TKey key, out TValue value)
		{
			LinkedListNode<KeyValuePair<TKey, TValue>> linkedListNode;
			if (this.m_dictionary.TryGetValue(key, out linkedListNode))
			{
				value = linkedListNode.Value.Value;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x1700008E RID: 142
		public TValue this[TKey key]
		{
			get
			{
				return this.m_dictionary[key].Value.Value;
			}
			set
			{
				LinkedListNode<KeyValuePair<TKey, TValue>> linkedListNode;
				if (this.m_dictionary.TryGetValue(key, out linkedListNode))
				{
					this.m_linkedList.Remove(linkedListNode);
				}
				LinkedListNode<KeyValuePair<TKey, TValue>> linkedListNode2 = this.m_linkedList.AddLast(new KeyValuePair<TKey, TValue>(key, value));
				this.m_dictionary[key] = linkedListNode2;
				this.EnforceSizeLimit();
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000E2D4 File Offset: 0x0000C4D4
		private void EnforceSizeLimit()
		{
			this.AssertValid();
			while (this.m_dictionary.Count > this.m_maxCount)
			{
				LinkedListNode<KeyValuePair<TKey, TValue>> first = this.m_linkedList.First;
				this.m_linkedList.RemoveFirst();
				this.m_dictionary.Remove(first.Value.Key);
			}
			this.AssertValid();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00009B3B File Offset: 0x00007D3B
		private void AssertValid()
		{
		}

		// Token: 0x04000144 RID: 324
		private readonly int m_maxCount;

		// Token: 0x04000145 RID: 325
		private readonly Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>> m_dictionary;

		// Token: 0x04000146 RID: 326
		private readonly LinkedList<KeyValuePair<TKey, TValue>> m_linkedList;
	}
}
