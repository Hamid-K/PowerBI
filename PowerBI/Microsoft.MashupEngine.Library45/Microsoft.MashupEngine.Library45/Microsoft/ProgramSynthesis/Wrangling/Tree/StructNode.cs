using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Tree
{
	// Token: 0x020000F2 RID: 242
	[DataContract(Namespace = "Microsoft.ProgramSynthesis.Wrangling.Tree")]
	[KnownType(typeof(SequenceNode))]
	[KnownType(typeof(StructNode))]
	[KnownType(typeof(HoleNode))]
	[KnownType(typeof(Node[]))]
	public class StructNode : Node, IEquatable<StructNode>
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x0001283A File Offset: 0x00010A3A
		protected StructNode(string label, Attributes attributes, Node[] children = null, Node parent = null)
			: base(label, attributes, children, parent)
		{
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00012847 File Offset: 0x00010A47
		public static StructNode Create(string label, Attributes attributes, Node[] children = null, Node parent = null)
		{
			return new StructNode(label, attributes, children, parent);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00012852 File Offset: 0x00010A52
		public static StructNode CreateUnsafe(string label, Attributes attributes, Node parent = null)
		{
			return new StructNode(label, attributes, Node.EmptyNodeCollection, parent);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00012864 File Offset: 0x00010A64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(StructNode other)
		{
			return other != null && (this == other || (string.Equals(base.Label, other.Label) && this.GetHashCode() == other.GetHashCode() && base.Attributes.Equals(other.Attributes) && base.Children.SequenceEqual(other.Children)));
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000128C3 File Offset: 0x00010AC3
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((StructNode)obj)));
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000128F4 File Offset: 0x00010AF4
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			int num = base.Label.GetHashCode() * 79 + base.Attributes.GetHashCode();
			Node[] children = base.Children;
			int num2 = children.Length;
			for (int i = 0; i < num2; i++)
			{
				num = num * 107 + children[i].GetHashCode();
			}
			this._hashCode = new int?(num);
			return num;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00012968 File Offset: 0x00010B68
		public override Node DeepClone()
		{
			if (base.IsIrrelevantNode())
			{
				return this;
			}
			return new StructNode(base.Label, base.Attributes, base.Children.Select((Node child) => child.DeepClone()).ToArray<Node>(), null)
			{
				StartPosition = base.StartPosition,
				EndPosition = base.EndPosition,
				_hashCode = this._hashCode,
				_count = this._count,
				_labelCounts = this._labelCounts
			};
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000112E8 File Offset: 0x0000F4E8
		[DebuggerStepThrough]
		public override T AcceptVisitor<T>(NodeVisitor<T> visitor)
		{
			return visitor.VisitStruct(this);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x000129FC File Offset: 0x00010BFC
		public static StructNode DeserializeFromXml(XElement node)
		{
			if (node == null)
			{
				throw new ArgumentException("Failed to deserialize Structnode. node cannot be null.", "node");
			}
			StructNode structNode = XmlUtils.DeserializeFromXml<StructNode>(node);
			Node.AddNonSerializableInfoToTree(structNode);
			return structNode;
		}
	}
}
