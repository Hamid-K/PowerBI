using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Semantics
{
	// Token: 0x02001ECC RID: 7884
	[DataContract(Namespace = "Microsoft.ProgramSynthesis.Transformation.Tree.Semantics")]
	public class ReplaceChildren : Edit, IEquatable<ReplaceChildren>
	{
		// Token: 0x17002C0C RID: 11276
		// (get) Token: 0x06010A26 RID: 68134 RVA: 0x00394697 File Offset: 0x00392897
		public int[] Indexes { get; }

		// Token: 0x17002C0D RID: 11277
		// (get) Token: 0x06010A27 RID: 68135 RVA: 0x0039469F File Offset: 0x0039289F
		public IEnumerable<Node> NewChildren { get; }

		// Token: 0x06010A28 RID: 68136 RVA: 0x003946A7 File Offset: 0x003928A7
		public ReplaceChildren(Node target, Node newNode, int[] indexes, IEnumerable<Node> newChildren)
			: base(target, newNode)
		{
			this.Indexes = indexes;
			this.NewChildren = newChildren;
		}

		// Token: 0x06010A29 RID: 68137 RVA: 0x003946C0 File Offset: 0x003928C0
		public override XElement SerializeToXml()
		{
			DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(ReplaceChildren));
			return base.SerializeHelper(dataContractSerializer);
		}

		// Token: 0x06010A2A RID: 68138 RVA: 0x003946E4 File Offset: 0x003928E4
		public static ReplaceChildren DeserializeFromXml(XElement replaceChildren)
		{
			if (replaceChildren == null)
			{
				throw new ArgumentException("replaceChildren cannot be null.", "replaceChildren");
			}
			return XmlUtils.DeserializeFromXml<ReplaceChildren>(replaceChildren);
		}

		// Token: 0x06010A2B RID: 68139 RVA: 0x003946FF File Offset: 0x003928FF
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ReplaceChildren);
		}

		// Token: 0x06010A2C RID: 68140 RVA: 0x00394710 File Offset: 0x00392910
		public bool Equals(ReplaceChildren other)
		{
			return other != null && base.Equals(other) && EqualityComparer<Node>.Default.Equals(base.Target, other.Target) && EqualityComparer<Node>.Default.Equals(base.NewNode, other.NewNode) && this.Indexes.SequenceEqual(other.Indexes) && this.NewChildren.SequenceEqual(other.NewChildren);
		}

		// Token: 0x06010A2D RID: 68141 RVA: 0x00394780 File Offset: 0x00392980
		public override int GetHashCode()
		{
			return ((((943929320 * -1521134295 + base.GetHashCode()) * -1521134295 + EqualityComparer<Node>.Default.GetHashCode(base.Target)) * -1521134295 + EqualityComparer<Node>.Default.GetHashCode(base.NewNode)) * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(this.Indexes)) * -1521134295 + EqualityComparer<IEnumerable<Node>>.Default.GetHashCode(this.NewChildren);
		}
	}
}
