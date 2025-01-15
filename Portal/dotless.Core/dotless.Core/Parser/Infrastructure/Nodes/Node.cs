using System;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Infrastructure.Nodes
{
	// Token: 0x0200005E RID: 94
	public abstract class Node
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00014F8F File Offset: 0x0001318F
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x00014F97 File Offset: 0x00013197
		public bool IsReference
		{
			get
			{
				return this.isReference;
			}
			set
			{
				this.isReference = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00014FA0 File Offset: 0x000131A0
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x00014FA8 File Offset: 0x000131A8
		public NodeLocation Location { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00014FB1 File Offset: 0x000131B1
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x00014FB9 File Offset: 0x000131B9
		public NodeList PreComments { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00014FC2 File Offset: 0x000131C2
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x00014FCA File Offset: 0x000131CA
		public NodeList PostComments { get; set; }

		// Token: 0x06000413 RID: 1043 RVA: 0x00014FD3 File Offset: 0x000131D3
		public static implicit operator bool(Node node)
		{
			return node != null;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00014FD9 File Offset: 0x000131D9
		public static bool operator true(Node n)
		{
			return n != null;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00014FDF File Offset: 0x000131DF
		public static bool operator false(Node n)
		{
			return n == null;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00014FE5 File Offset: 0x000131E5
		public static bool operator !(Node n)
		{
			return n == null;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00014FEB File Offset: 0x000131EB
		public static Node operator &(Node n1, Node n2)
		{
			if (n1 == null)
			{
				return null;
			}
			return n2;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00014FF3 File Offset: 0x000131F3
		public static Node operator |(Node n1, Node n2)
		{
			return n1 ?? n2;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00014FFC File Offset: 0x000131FC
		public T ReducedFrom<T>(params Node[] nodes) where T : Node
		{
			foreach (Node node in nodes)
			{
				if (node != this)
				{
					this.Location = node.Location;
					if (node.PreComments)
					{
						if (this.PreComments)
						{
							this.PreComments.AddRange(node.PreComments);
						}
						else
						{
							this.PreComments = node.PreComments;
						}
					}
					if (node.PostComments)
					{
						if (this.PostComments)
						{
							this.PostComments.AddRange(node.PostComments);
						}
						else
						{
							this.PostComments = node.PostComments;
						}
					}
					this.IsReference = node.IsReference;
				}
			}
			return (T)((object)this);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000150BA File Offset: 0x000132BA
		public virtual Node Clone()
		{
			return this.CloneCore().ReducedFrom<Node>(new Node[] { this });
		}

		// Token: 0x0600041B RID: 1051
		protected abstract Node CloneCore();

		// Token: 0x0600041C RID: 1052 RVA: 0x000150D1 File Offset: 0x000132D1
		public virtual void AppendCSS(Env env)
		{
			this.Evaluate(env).AppendCSS(env);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000150E0 File Offset: 0x000132E0
		public virtual string ToCSS(Env env)
		{
			return env.Output.Push().Append(this).Pop()
				.ToString();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000150FD File Offset: 0x000132FD
		public virtual Node Evaluate(Env env)
		{
			return this;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00015100 File Offset: 0x00013300
		public virtual bool IgnoreOutput()
		{
			return this is RegexMatchResult || this is CharMatchResult;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00015115 File Offset: 0x00013315
		public virtual void Accept(IVisitor visitor)
		{
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00015117 File Offset: 0x00013317
		public T VisitAndReplace<T>(T nodeToVisit, IVisitor visitor) where T : Node
		{
			return this.VisitAndReplace<T>(nodeToVisit, visitor, false);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00015124 File Offset: 0x00013324
		public T VisitAndReplace<T>(T nodeToVisit, IVisitor visitor, bool allowNull) where T : Node
		{
			if (nodeToVisit == null)
			{
				return default(T);
			}
			Node node = visitor.Visit(nodeToVisit);
			T t = node as T;
			if (t != null || (allowNull && node == null))
			{
				return t;
			}
			throw new Exception("Not allowed null for node of type " + typeof(T).ToString());
		}

		// Token: 0x040000E5 RID: 229
		private bool isReference;
	}
}
