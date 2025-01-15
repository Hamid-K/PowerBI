using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000048 RID: 72
	[DebuggerDisplay("Count={Count}")]
	[DebuggerTypeProxy(typeof(TargetRegistry<>.DebugView))]
	internal sealed class TargetRegistry<T>
	{
		// Token: 0x06000289 RID: 649 RVA: 0x0000B4D1 File Offset: 0x000096D1
		internal TargetRegistry(ISourceBlock<T> owningSource)
		{
			this._owningSource = owningSource;
			this._targetInformation = new Dictionary<ITargetBlock<T>, TargetRegistry<T>.LinkedTargetInfo>();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000B4EC File Offset: 0x000096EC
		internal void Add(ref ITargetBlock<T> target, DataflowLinkOptions linkOptions)
		{
			TargetRegistry<T>.LinkedTargetInfo linkedTargetInfo;
			if (this._targetInformation.TryGetValue(target, out linkedTargetInfo))
			{
				target = new TargetRegistry<T>.NopLinkPropagator(this._owningSource, target);
			}
			TargetRegistry<T>.LinkedTargetInfo linkedTargetInfo2 = new TargetRegistry<T>.LinkedTargetInfo(target, linkOptions);
			this.AddToList(linkedTargetInfo2, linkOptions.Append);
			this._targetInformation.Add(target, linkedTargetInfo2);
			if (linkedTargetInfo2.RemainingMessages > 0)
			{
				this._linksWithRemainingMessages++;
			}
			DataflowEtwProvider log = DataflowEtwProvider.Log;
			if (log.IsEnabled())
			{
				log.DataflowBlockLinking<T>(this._owningSource, target);
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000B570 File Offset: 0x00009770
		internal bool Contains(ITargetBlock<T> target)
		{
			return this._targetInformation.ContainsKey(target);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000B57E File Offset: 0x0000977E
		internal void Remove(ITargetBlock<T> target, bool onlyIfReachedMaxMessages = false)
		{
			if (onlyIfReachedMaxMessages && this._linksWithRemainingMessages == 0)
			{
				return;
			}
			this.Remove_Slow(target, onlyIfReachedMaxMessages);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000B594 File Offset: 0x00009794
		private void Remove_Slow(ITargetBlock<T> target, bool onlyIfReachedMaxMessages)
		{
			TargetRegistry<T>.LinkedTargetInfo linkedTargetInfo;
			if (this._targetInformation.TryGetValue(target, out linkedTargetInfo))
			{
				if (!onlyIfReachedMaxMessages || linkedTargetInfo.RemainingMessages == 1)
				{
					this.RemoveFromList(linkedTargetInfo);
					this._targetInformation.Remove(target);
					if (linkedTargetInfo.RemainingMessages == 0)
					{
						this._linksWithRemainingMessages--;
					}
					DataflowEtwProvider log = DataflowEtwProvider.Log;
					if (log.IsEnabled())
					{
						log.DataflowBlockUnlinking<T>(this._owningSource, target);
						return;
					}
				}
				else if (linkedTargetInfo.RemainingMessages > 0)
				{
					linkedTargetInfo.RemainingMessages--;
				}
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000B61C File Offset: 0x0000981C
		internal TargetRegistry<T>.LinkedTargetInfo ClearEntryPoints()
		{
			TargetRegistry<T>.LinkedTargetInfo firstTarget = this._firstTarget;
			this._firstTarget = (this._lastTarget = null);
			this._targetInformation.Clear();
			this._linksWithRemainingMessages = 0;
			return firstTarget;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000B654 File Offset: 0x00009854
		internal void PropagateCompletion(TargetRegistry<T>.LinkedTargetInfo firstTarget)
		{
			Task completion = this._owningSource.Completion;
			for (TargetRegistry<T>.LinkedTargetInfo linkedTargetInfo = firstTarget; linkedTargetInfo != null; linkedTargetInfo = linkedTargetInfo.Next)
			{
				if (linkedTargetInfo.PropagateCompletion)
				{
					Common.PropagateCompletion(completion, linkedTargetInfo.Target, Common.AsyncExceptionHandler);
				}
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000B694 File Offset: 0x00009894
		internal TargetRegistry<T>.LinkedTargetInfo FirstTargetNode
		{
			get
			{
				return this._firstTarget;
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000B69C File Offset: 0x0000989C
		internal void AddToList(TargetRegistry<T>.LinkedTargetInfo node, bool append)
		{
			if (this._firstTarget == null && this._lastTarget == null)
			{
				this._lastTarget = node;
				this._firstTarget = node;
				return;
			}
			if (append)
			{
				node.Previous = this._lastTarget;
				this._lastTarget.Next = node;
				this._lastTarget = node;
				return;
			}
			node.Next = this._firstTarget;
			this._firstTarget.Previous = node;
			this._firstTarget = node;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000B70C File Offset: 0x0000990C
		internal void RemoveFromList(TargetRegistry<T>.LinkedTargetInfo node)
		{
			TargetRegistry<T>.LinkedTargetInfo previous = node.Previous;
			TargetRegistry<T>.LinkedTargetInfo next = node.Next;
			if (node.Previous != null)
			{
				node.Previous.Next = next;
				node.Previous = null;
			}
			if (node.Next != null)
			{
				node.Next.Previous = previous;
				node.Next = null;
			}
			if (this._firstTarget == node)
			{
				this._firstTarget = next;
			}
			if (this._lastTarget == node)
			{
				this._lastTarget = previous;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000B77D File Offset: 0x0000997D
		private int Count
		{
			get
			{
				return this._targetInformation.Count;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000B78C File Offset: 0x0000998C
		private ITargetBlock<T>[] TargetsForDebugger
		{
			get
			{
				ITargetBlock<T>[] array = new ITargetBlock<T>[this.Count];
				int num = 0;
				for (TargetRegistry<T>.LinkedTargetInfo linkedTargetInfo = this._firstTarget; linkedTargetInfo != null; linkedTargetInfo = linkedTargetInfo.Next)
				{
					array[num++] = linkedTargetInfo.Target;
				}
				return array;
			}
		}

		// Token: 0x040000CE RID: 206
		private readonly ISourceBlock<T> _owningSource;

		// Token: 0x040000CF RID: 207
		private readonly Dictionary<ITargetBlock<T>, TargetRegistry<T>.LinkedTargetInfo> _targetInformation;

		// Token: 0x040000D0 RID: 208
		private TargetRegistry<T>.LinkedTargetInfo _firstTarget;

		// Token: 0x040000D1 RID: 209
		private TargetRegistry<T>.LinkedTargetInfo _lastTarget;

		// Token: 0x040000D2 RID: 210
		private int _linksWithRemainingMessages;

		// Token: 0x0200008E RID: 142
		internal sealed class LinkedTargetInfo
		{
			// Token: 0x06000463 RID: 1123 RVA: 0x0001017D File Offset: 0x0000E37D
			internal LinkedTargetInfo(ITargetBlock<T> target, DataflowLinkOptions linkOptions)
			{
				this.Target = target;
				this.PropagateCompletion = linkOptions.PropagateCompletion;
				this.RemainingMessages = linkOptions.MaxMessages;
			}

			// Token: 0x040001CB RID: 459
			internal readonly ITargetBlock<T> Target;

			// Token: 0x040001CC RID: 460
			internal readonly bool PropagateCompletion;

			// Token: 0x040001CD RID: 461
			internal int RemainingMessages;

			// Token: 0x040001CE RID: 462
			internal TargetRegistry<T>.LinkedTargetInfo Previous;

			// Token: 0x040001CF RID: 463
			internal TargetRegistry<T>.LinkedTargetInfo Next;
		}

		// Token: 0x0200008F RID: 143
		[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
		[DebuggerTypeProxy(typeof(TargetRegistry<>.NopLinkPropagator.DebugView))]
		private sealed class NopLinkPropagator : IPropagatorBlock<T, T>, ITargetBlock<T>, IDataflowBlock, ISourceBlock<T>, IDebuggerDisplay
		{
			// Token: 0x06000464 RID: 1124 RVA: 0x000101A4 File Offset: 0x0000E3A4
			internal NopLinkPropagator(ISourceBlock<T> owningSource, ITargetBlock<T> target)
			{
				this._owningSource = owningSource;
				this._target = target;
			}

			// Token: 0x06000465 RID: 1125 RVA: 0x000101BA File Offset: 0x0000E3BA
			DataflowMessageStatus ITargetBlock<T>.OfferMessage(DataflowMessageHeader messageHeader, T messageValue, ISourceBlock<T> source, bool consumeToAccept)
			{
				return this._target.OfferMessage(messageHeader, messageValue, this, consumeToAccept);
			}

			// Token: 0x06000466 RID: 1126 RVA: 0x000101CC File Offset: 0x0000E3CC
			T ISourceBlock<T>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<T> target, out bool messageConsumed)
			{
				return this._owningSource.ConsumeMessage(messageHeader, this, out messageConsumed);
			}

			// Token: 0x06000467 RID: 1127 RVA: 0x000101DC File Offset: 0x0000E3DC
			bool ISourceBlock<T>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<T> target)
			{
				return this._owningSource.ReserveMessage(messageHeader, this);
			}

			// Token: 0x06000468 RID: 1128 RVA: 0x000101EB File Offset: 0x0000E3EB
			void ISourceBlock<T>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<T> target)
			{
				this._owningSource.ReleaseReservation(messageHeader, this);
			}

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x06000469 RID: 1129 RVA: 0x000101FA File Offset: 0x0000E3FA
			Task IDataflowBlock.Completion
			{
				get
				{
					return this._owningSource.Completion;
				}
			}

			// Token: 0x0600046A RID: 1130 RVA: 0x00010207 File Offset: 0x0000E407
			void IDataflowBlock.Complete()
			{
				this._target.Complete();
			}

			// Token: 0x0600046B RID: 1131 RVA: 0x00010214 File Offset: 0x0000E414
			void IDataflowBlock.Fault(Exception exception)
			{
				this._target.Fault(exception);
			}

			// Token: 0x0600046C RID: 1132 RVA: 0x00010222 File Offset: 0x0000E422
			IDisposable ISourceBlock<T>.LinkTo(ITargetBlock<T> target, DataflowLinkOptions linkOptions)
			{
				throw new NotSupportedException(SR.NotSupported_MemberNotNeeded);
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x0600046D RID: 1133 RVA: 0x00010230 File Offset: 0x0000E430
			private object DebuggerDisplayContent
			{
				get
				{
					IDebuggerDisplay debuggerDisplay = this._owningSource as IDebuggerDisplay;
					IDebuggerDisplay debuggerDisplay2 = this._target as IDebuggerDisplay;
					return string.Format("{0} Source=\"{1}\", Target=\"{2}\"", Common.GetNameForDebugger(this, null), (debuggerDisplay != null) ? debuggerDisplay.Content : this._owningSource, (debuggerDisplay2 != null) ? debuggerDisplay2.Content : this._target);
				}
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x0600046E RID: 1134 RVA: 0x00010288 File Offset: 0x0000E488
			object IDebuggerDisplay.Content
			{
				get
				{
					return this.DebuggerDisplayContent;
				}
			}

			// Token: 0x040001D0 RID: 464
			private readonly ISourceBlock<T> _owningSource;

			// Token: 0x040001D1 RID: 465
			private readonly ITargetBlock<T> _target;

			// Token: 0x020000A3 RID: 163
			private sealed class DebugView
			{
				// Token: 0x060004BE RID: 1214 RVA: 0x00010CE1 File Offset: 0x0000EEE1
				public DebugView(TargetRegistry<T>.NopLinkPropagator passthrough)
				{
					this._passthrough = passthrough;
				}

				// Token: 0x17000195 RID: 405
				// (get) Token: 0x060004BF RID: 1215 RVA: 0x00010CF0 File Offset: 0x0000EEF0
				public ITargetBlock<T> LinkedTarget
				{
					get
					{
						return this._passthrough._target;
					}
				}

				// Token: 0x04000202 RID: 514
				private readonly TargetRegistry<T>.NopLinkPropagator _passthrough;
			}
		}

		// Token: 0x02000090 RID: 144
		private sealed class DebugView
		{
			// Token: 0x0600046F RID: 1135 RVA: 0x00010290 File Offset: 0x0000E490
			public DebugView(TargetRegistry<T> registry)
			{
				this._registry = registry;
			}

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x06000470 RID: 1136 RVA: 0x0001029F File Offset: 0x0000E49F
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public ITargetBlock<T>[] Targets
			{
				get
				{
					return this._registry.TargetsForDebugger;
				}
			}

			// Token: 0x040001D2 RID: 466
			private readonly TargetRegistry<T> _registry;
		}
	}
}
