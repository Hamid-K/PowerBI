using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Infrastructure.Nodes
{
	// Token: 0x02000060 RID: 96
	public class NodeList<TNode> : Node, IList<TNode>, ICollection<TNode>, IEnumerable<TNode>, IEnumerable where TNode : Node
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x000151D7 File Offset: 0x000133D7
		public NodeList()
		{
			this.Inner = new List<TNode>();
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000151EA File Offset: 0x000133EA
		public NodeList(params TNode[] nodes)
			: this(nodes)
		{
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000151F3 File Offset: 0x000133F3
		public NodeList(IEnumerable<TNode> nodes)
		{
			this.Inner = new List<TNode>(nodes);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00015207 File Offset: 0x00013407
		protected override Node CloneCore()
		{
			return new NodeList(this.Inner.Select((TNode i) => i.Clone()));
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00015238 File Offset: 0x00013438
		public override void AppendCSS(Env env)
		{
			env.Output.AppendMany<TNode>(this.Inner);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001524C File Offset: 0x0001344C
		public override void Accept(IVisitor visitor)
		{
			List<TNode> list = new List<TNode>(this.Inner.Count);
			foreach (TNode tnode in this.Inner)
			{
				TNode tnode2 = base.VisitAndReplace<TNode>(tnode, visitor, true);
				if (tnode2 != null)
				{
					list.Add(tnode2);
				}
			}
			this.Inner = list;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000152CC File Offset: 0x000134CC
		public void AddRange(IEnumerable<TNode> nodes)
		{
			this.Inner.AddRange(nodes);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000152DA File Offset: 0x000134DA
		public IEnumerator<TNode> GetEnumerator()
		{
			return this.Inner.GetEnumerator();
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000152EC File Offset: 0x000134EC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000152F4 File Offset: 0x000134F4
		public void InsertRange(int index, IEnumerable<TNode> collection)
		{
			this.Inner.InsertRange(index, collection);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00015303 File Offset: 0x00013503
		public void Add(TNode item)
		{
			this.Inner.Add(item);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00015311 File Offset: 0x00013511
		public void Clear()
		{
			this.Inner.Clear();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001531E File Offset: 0x0001351E
		public bool Contains(TNode item)
		{
			return this.Inner.Contains(item);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001532C File Offset: 0x0001352C
		public void CopyTo(TNode[] array, int arrayIndex)
		{
			this.Inner.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001533B File Offset: 0x0001353B
		public bool Remove(TNode item)
		{
			return this.Inner.Remove(item);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00015349 File Offset: 0x00013549
		public int Count
		{
			get
			{
				return this.Inner.Count;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00015356 File Offset: 0x00013556
		public bool IsReadOnly
		{
			get
			{
				return ((IList)this.Inner).IsReadOnly;
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00015363 File Offset: 0x00013563
		public int IndexOf(TNode item)
		{
			return this.Inner.IndexOf(item);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00015371 File Offset: 0x00013571
		public void Insert(int index, TNode item)
		{
			this.Inner.Insert(index, item);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00015380 File Offset: 0x00013580
		public void RemoveAt(int index)
		{
			this.Inner.RemoveAt(index);
		}

		// Token: 0x170000D3 RID: 211
		public TNode this[int index]
		{
			get
			{
				return this.Inner[index];
			}
			set
			{
				this.Inner[index] = value;
			}
		}

		// Token: 0x040000E9 RID: 233
		protected List<TNode> Inner;
	}
}
