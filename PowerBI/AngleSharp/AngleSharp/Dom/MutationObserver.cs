using System;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200015A RID: 346
	[DomName("MutationObserver")]
	public sealed class MutationObserver
	{
		// Token: 0x06000BEB RID: 3051 RVA: 0x000441E2 File Offset: 0x000423E2
		[DomConstructor]
		public MutationObserver(MutationCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this._records = new Queue<IMutationRecord>();
			this._callback = callback;
			this._observing = new List<MutationObserver.MutationObserving>();
		}

		// Token: 0x1700020D RID: 525
		private MutationObserver.MutationObserving this[INode node]
		{
			get
			{
				foreach (MutationObserver.MutationObserving mutationObserving in this._observing)
				{
					if (mutationObserving.Target == node)
					{
						return mutationObserving;
					}
				}
				return null;
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00044274 File Offset: 0x00042474
		internal void Enqueue(MutationRecord record)
		{
			int count = this._records.Count;
			this._records.Enqueue(record);
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00044290 File Offset: 0x00042490
		internal void Trigger()
		{
			IMutationRecord[] array = this._records.ToArray();
			this._records.Clear();
			this.ClearTransients();
			if (array.Length != 0)
			{
				this.TriggerWith(array);
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x000442C5 File Offset: 0x000424C5
		internal void TriggerWith(IMutationRecord[] records)
		{
			this._callback(records, this);
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x000442D4 File Offset: 0x000424D4
		internal MutationObserver.MutationOptions ResolveOptions(INode node)
		{
			foreach (MutationObserver.MutationObserving mutationObserving in this._observing)
			{
				if (mutationObserving.Target == node || mutationObserving.TransientNodes.Contains(node))
				{
					return mutationObserving.Options;
				}
			}
			return default(MutationObserver.MutationOptions);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0004434C File Offset: 0x0004254C
		internal void AddTransient(INode ancestor, INode node)
		{
			MutationObserver.MutationObserving mutationObserving = this[ancestor];
			if (mutationObserving != null && mutationObserving.Options.IsObservingSubtree)
			{
				mutationObserving.TransientNodes.Add(node);
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00044380 File Offset: 0x00042580
		internal void ClearTransients()
		{
			foreach (MutationObserver.MutationObserving mutationObserving in this._observing)
			{
				mutationObserving.TransientNodes.Clear();
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x000443D8 File Offset: 0x000425D8
		[DomName("disconnect")]
		public void Disconnect()
		{
			foreach (MutationObserver.MutationObserving mutationObserving in this._observing)
			{
				((Node)mutationObserving.Target).Owner.Mutations.Unregister(this);
			}
			this._records.Clear();
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00044448 File Offset: 0x00042648
		[DomName("observe")]
		[DomInitDict(1, false)]
		public void Connect(INode target, bool childList = false, bool subtree = false, bool? attributes = null, bool? characterData = null, bool? attributeOldValue = null, bool? characterDataOldValue = null, IEnumerable<string> attributeFilter = null)
		{
			Node node = target as Node;
			if (node == null)
			{
				return;
			}
			bool flag = characterDataOldValue ?? false;
			bool flag2 = attributeOldValue ?? false;
			MutationObserver.MutationOptions mutationOptions = new MutationObserver.MutationOptions
			{
				IsObservingChildNodes = childList,
				IsObservingSubtree = subtree,
				IsExaminingOldCharacterData = flag,
				IsExaminingOldAttributeValue = flag2,
				IsObservingCharacterData = (characterData ?? flag),
				IsObservingAttributes = (attributes ?? (flag2 || attributeFilter != null)),
				AttributeFilters = attributeFilter
			};
			if (mutationOptions.IsExaminingOldAttributeValue && !mutationOptions.IsObservingAttributes)
			{
				throw new DomException(DomError.TypeMismatch);
			}
			if (mutationOptions.AttributeFilters != null && !mutationOptions.IsObservingAttributes)
			{
				throw new DomException(DomError.TypeMismatch);
			}
			if (mutationOptions.IsExaminingOldCharacterData && !mutationOptions.IsObservingCharacterData)
			{
				throw new DomException(DomError.TypeMismatch);
			}
			if (mutationOptions.IsInvalid)
			{
				throw new DomException(DomError.Syntax);
			}
			node.Owner.Mutations.Register(this);
			MutationObserver.MutationObserving mutationObserving = this[target];
			if (mutationObserving != null)
			{
				mutationObserving.TransientNodes.Clear();
				this._observing.Remove(mutationObserving);
			}
			this._observing.Add(new MutationObserver.MutationObserving(target, mutationOptions));
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x000445AD File Offset: 0x000427AD
		[DomName("takeRecords")]
		public IEnumerable<IMutationRecord> Flush()
		{
			while (this._records.Count > 0)
			{
				yield return this._records.Dequeue();
			}
			yield break;
		}

		// Token: 0x0400094E RID: 2382
		private readonly Queue<IMutationRecord> _records;

		// Token: 0x0400094F RID: 2383
		private readonly MutationCallback _callback;

		// Token: 0x04000950 RID: 2384
		private readonly List<MutationObserver.MutationObserving> _observing;

		// Token: 0x020004D6 RID: 1238
		internal struct MutationOptions
		{
			// Token: 0x17000AC4 RID: 2756
			// (get) Token: 0x06002596 RID: 9622 RVA: 0x00061E4C File Offset: 0x0006004C
			public bool IsInvalid
			{
				get
				{
					return !this.IsObservingAttributes && !this.IsObservingCharacterData && !this.IsObservingChildNodes;
				}
			}

			// Token: 0x0400119A RID: 4506
			public bool IsObservingChildNodes;

			// Token: 0x0400119B RID: 4507
			public bool IsObservingSubtree;

			// Token: 0x0400119C RID: 4508
			public bool IsObservingCharacterData;

			// Token: 0x0400119D RID: 4509
			public bool IsObservingAttributes;

			// Token: 0x0400119E RID: 4510
			public bool IsExaminingOldCharacterData;

			// Token: 0x0400119F RID: 4511
			public bool IsExaminingOldAttributeValue;

			// Token: 0x040011A0 RID: 4512
			public IEnumerable<string> AttributeFilters;
		}

		// Token: 0x020004D7 RID: 1239
		private sealed class MutationObserving
		{
			// Token: 0x06002597 RID: 9623 RVA: 0x00061E69 File Offset: 0x00060069
			public MutationObserving(INode target, MutationObserver.MutationOptions options)
			{
				this._target = target;
				this._options = options;
				this._transientNodes = new List<INode>();
			}

			// Token: 0x17000AC5 RID: 2757
			// (get) Token: 0x06002598 RID: 9624 RVA: 0x00061E8A File Offset: 0x0006008A
			public INode Target
			{
				get
				{
					return this._target;
				}
			}

			// Token: 0x17000AC6 RID: 2758
			// (get) Token: 0x06002599 RID: 9625 RVA: 0x00061E92 File Offset: 0x00060092
			public MutationObserver.MutationOptions Options
			{
				get
				{
					return this._options;
				}
			}

			// Token: 0x17000AC7 RID: 2759
			// (get) Token: 0x0600259A RID: 9626 RVA: 0x00061E9A File Offset: 0x0006009A
			public List<INode> TransientNodes
			{
				get
				{
					return this._transientNodes;
				}
			}

			// Token: 0x040011A1 RID: 4513
			private readonly INode _target;

			// Token: 0x040011A2 RID: 4514
			private readonly MutationObserver.MutationOptions _options;

			// Token: 0x040011A3 RID: 4515
			private readonly List<INode> _transientNodes;
		}
	}
}
