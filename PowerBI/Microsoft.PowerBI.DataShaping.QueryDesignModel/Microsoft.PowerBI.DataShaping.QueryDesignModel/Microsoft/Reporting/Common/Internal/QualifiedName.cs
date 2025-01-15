using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.DataShaping.Common;

namespace Microsoft.Reporting.Common.Internal
{
	// Token: 0x0200028A RID: 650
	public struct QualifiedName : IEquatable<QualifiedName>
	{
		// Token: 0x06001BB8 RID: 7096 RVA: 0x0004D870 File Offset: 0x0004BA70
		private QualifiedName(QualifiedName.Node node)
		{
			this._node = node;
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0004D879 File Offset: 0x0004BA79
		private QualifiedName(QualifiedName par, Name name)
		{
			this._node = new QualifiedName.Node(par._node, name);
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0004D88D File Offset: 0x0004BA8D
		[Conditional("DEBUG")]
		private void AssertValid()
		{
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001BBB RID: 7099 RVA: 0x0004D88F File Offset: 0x0004BA8F
		public QualifiedName Parent
		{
			get
			{
				if (this._node != null)
				{
					return new QualifiedName(this._node.Parent);
				}
				return this;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001BBC RID: 7100 RVA: 0x0004D8B0 File Offset: 0x0004BAB0
		public Name Name
		{
			get
			{
				if (this._node != null)
				{
					return this._node.Name;
				}
				return default(Name);
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001BBD RID: 7101 RVA: 0x0004D8DA File Offset: 0x0004BADA
		public int Length
		{
			get
			{
				if (this._node != null)
				{
					return this._node.Length;
				}
				return 0;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001BBE RID: 7102 RVA: 0x0004D8F1 File Offset: 0x0004BAF1
		public bool IsRoot
		{
			get
			{
				return this._node == null;
			}
		}

		// Token: 0x170007BF RID: 1983
		public Name this[int index]
		{
			get
			{
				QualifiedName.Node node = this._node;
				while (node.Length > index + 1)
				{
					node = node.Parent;
				}
				return node.Name;
			}
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x0004D92C File Offset: 0x0004BB2C
		public bool IsPrefixOf(QualifiedName path)
		{
			int num = path.Length - this.Length;
			if (num < 0)
			{
				return false;
			}
			if (num > 0)
			{
				path = path.GoUp(num);
			}
			return path == this;
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x0004D968 File Offset: 0x0004BB68
		public QualifiedName Append(Name name)
		{
			return new QualifiedName(this, name);
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x0004D978 File Offset: 0x0004BB78
		public QualifiedName Append(QualifiedName path)
		{
			if (this.IsRoot)
			{
				return path;
			}
			if (path.Length <= 20)
			{
				return new QualifiedName(this._node.Append(path._node));
			}
			QualifiedName.Node[] array = new QualifiedName.Node[path.Length];
			int i = 0;
			QualifiedName.Node node;
			for (node = path._node; node != null; node = node.Parent)
			{
				array[i++] = node;
			}
			node = this._node;
			while (i > 0)
			{
				QualifiedName.Node node2 = array[--i];
				node = new QualifiedName.Node(node, node2.Name);
			}
			return new QualifiedName(node);
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0004DA03 File Offset: 0x0004BC03
		public QualifiedName GoUp(int count)
		{
			return new QualifiedName(this.GoUpCore(count));
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x0004DA14 File Offset: 0x0004BC14
		private QualifiedName.Node GoUpCore(int count)
		{
			QualifiedName.Node node = this._node;
			while (--count >= 0)
			{
				node = node.Parent;
			}
			return node;
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x0004DA3C File Offset: 0x0004BC3C
		public override string ToString()
		{
			if (this.IsRoot)
			{
				return "∂";
			}
			int num = 1;
			for (QualifiedName.Node node = this._node; node != null; node = node.Parent)
			{
				num += node.Name.Value.Length + 1;
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			stringBuilder.Length = num;
			for (QualifiedName.Node node2 = this._node; node2 != null; node2 = node2.Parent)
			{
				string text = node2.Name;
				int i = text.Length;
				while (i > 0)
				{
					stringBuilder[--num] = text[--i];
				}
				stringBuilder[--num] = ':';
			}
			stringBuilder[num - 1] = '∂';
			return stringBuilder.ToString();
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x0004DB04 File Offset: 0x0004BD04
		public static bool operator ==(QualifiedName path1, QualifiedName path2)
		{
			QualifiedName.Node node = path1._node;
			QualifiedName.Node node2 = path2._node;
			while (node != node2)
			{
				if (node == null || node2 == null)
				{
					return false;
				}
				if (node.GetHashCode() != node2.GetHashCode())
				{
					return false;
				}
				if (node.Name != node2.Name)
				{
					return false;
				}
				node = node.Parent;
				node2 = node2.Parent;
			}
			return true;
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x0004DB61 File Offset: 0x0004BD61
		public static bool operator !=(QualifiedName path1, QualifiedName path2)
		{
			return !(path1 == path2);
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x0004DB6D File Offset: 0x0004BD6D
		public override int GetHashCode()
		{
			if (this._node == null)
			{
				return 873244697;
			}
			return this._node.GetHashCode();
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x0004DB88 File Offset: 0x0004BD88
		public bool Equals(QualifiedName other)
		{
			return this == other;
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x0004DB96 File Offset: 0x0004BD96
		public override bool Equals(object obj)
		{
			return obj is QualifiedName && this == (QualifiedName)obj;
		}

		// Token: 0x04000F2E RID: 3886
		private readonly QualifiedName.Node _node;

		// Token: 0x04000F2F RID: 3887
		public static readonly QualifiedName Root;

		// Token: 0x02000419 RID: 1049
		private sealed class Node
		{
			// Token: 0x060021B4 RID: 8628 RVA: 0x0005AAC7 File Offset: 0x00058CC7
			internal Node(QualifiedName.Node par, Name name)
			{
				this.Parent = par;
				this.Name = name;
				this.Length = ((par == null) ? 1 : (1 + par.Length));
			}

			// Token: 0x060021B5 RID: 8629 RVA: 0x0005AAF1 File Offset: 0x00058CF1
			public QualifiedName.Node Append(QualifiedName.Node node)
			{
				if (node == null)
				{
					return this;
				}
				return new QualifiedName.Node(this.Append(node.Parent), node.Name);
			}

			// Token: 0x060021B6 RID: 8630 RVA: 0x0005AB0F File Offset: 0x00058D0F
			[Conditional("DEBUG")]
			internal void AssertValid()
			{
			}

			// Token: 0x060021B7 RID: 8631 RVA: 0x0005AB11 File Offset: 0x00058D11
			public override int GetHashCode()
			{
				if (this._hash == 0)
				{
					this.EnsureHash();
				}
				return this._hash;
			}

			// Token: 0x060021B8 RID: 8632 RVA: 0x0005AB2C File Offset: 0x00058D2C
			private void EnsureHash()
			{
				int num = Hashing.CombineHash((this.Parent == null) ? 873244697 : this.Parent.GetHashCode(), this.Name.GetHashCode());
				if (num == 0)
				{
					num = 1;
				}
				Interlocked.CompareExchange(ref this._hash, num, 0);
			}

			// Token: 0x04001485 RID: 5253
			internal const int HashNull = 873244697;

			// Token: 0x04001486 RID: 5254
			internal readonly QualifiedName.Node Parent;

			// Token: 0x04001487 RID: 5255
			internal readonly Name Name;

			// Token: 0x04001488 RID: 5256
			internal readonly int Length;

			// Token: 0x04001489 RID: 5257
			private volatile int _hash;
		}
	}
}
