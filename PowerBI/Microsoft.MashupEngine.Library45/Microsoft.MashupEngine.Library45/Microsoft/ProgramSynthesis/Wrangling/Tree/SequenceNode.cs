using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Tree
{
	// Token: 0x020000F0 RID: 240
	[DataContract(Namespace = "Microsoft.ProgramSynthesis.Wrangling.Tree")]
	[KnownType(typeof(SequenceNode))]
	[KnownType(typeof(StructNode))]
	[KnownType(typeof(HoleNode))]
	[KnownType(typeof(Node[]))]
	public class SequenceNode : Node, IEquatable<SequenceNode>, IEnumerable<Node>, IEnumerable
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x000125B8 File Offset: 0x000107B8
		public SequenceNode(string label, Attributes attributes, Node parent = null, Node[] children = null, Node separator = null)
			: base(label, attributes, children, parent)
		{
			this.Separator = separator;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000125CD File Offset: 0x000107CD
		public static SequenceNode CreateUnsafe(string label, Attributes attributes, Node separator = null)
		{
			return new SequenceNode(label, attributes, null, Node.EmptyNodeCollection, separator);
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x000125DD File Offset: 0x000107DD
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x000125E5 File Offset: 0x000107E5
		[DataMember]
		public Node Separator { get; private set; }

		// Token: 0x06000582 RID: 1410 RVA: 0x000125EE File Offset: 0x000107EE
		public IEnumerator<Node> GetEnumerator()
		{
			return ((IEnumerable<Node>)base.Children).GetEnumerator();
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000125FB File Offset: 0x000107FB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00012604 File Offset: 0x00010804
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(SequenceNode other)
		{
			return other != null && (this == other || (string.Equals(base.Label, other.Label) && this.GetHashCode() == other.GetHashCode() && base.Attributes.Equals(other.Attributes) && EqualityComparer<Node>.Default.Equals(this.Separator, other.Separator) && base.Children.SequenceEqual(other.Children)));
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001267B File Offset: 0x0001087B
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((SequenceNode)obj)));
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000126AC File Offset: 0x000108AC
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			int num = base.Label.GetHashCode() * 479 + base.Attributes.GetHashCode();
			if (this.Separator != null)
			{
				num += 107 * this.Separator.GetHashCode();
			}
			Node[] children = base.Children;
			int num2 = children.Length;
			for (int i = 0; i < num2; i++)
			{
				num = num * 107 + children[i].GetHashCode();
			}
			this._hashCode = new int?(num);
			return num;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001273C File Offset: 0x0001093C
		public override Node DeepClone()
		{
			if (base.IsIrrelevantNode())
			{
				return this;
			}
			Node node = ((this.Separator != null && !this.Separator.IsIrrelevantNode()) ? this.Separator.DeepClone() : this.Separator);
			string label = base.Label;
			Attributes attributes = base.Attributes;
			Node node2 = null;
			Node node3 = node;
			return new SequenceNode(label, attributes, node2, base.Children.Select((Node child) => child.DeepClone()).ToArray<Node>(), node3)
			{
				StartPosition = base.StartPosition,
				EndPosition = base.EndPosition,
				_hashCode = this._hashCode,
				_count = this._count,
				_labelCounts = this._labelCounts
			};
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000127FC File Offset: 0x000109FC
		public override T AcceptVisitor<T>(NodeVisitor<T> visitor)
		{
			return visitor.VisitSequence(this);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00012805 File Offset: 0x00010A05
		public static SequenceNode DeserializeFromXml(XElement node)
		{
			if (node == null)
			{
				throw new ArgumentException("Failed to deserialize SequenceNode. node cannot be null.", "node");
			}
			SequenceNode sequenceNode = XmlUtils.DeserializeFromXml<SequenceNode>(node);
			Node.AddNonSerializableInfoToTree(sequenceNode);
			return sequenceNode;
		}
	}
}
