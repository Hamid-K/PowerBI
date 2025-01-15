using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Semantics
{
	// Token: 0x02001ECA RID: 7882
	public class InsertAt : Edit, IEquatable<InsertAt>
	{
		// Token: 0x06010A1B RID: 68123 RVA: 0x003944EA File Offset: 0x003926EA
		public InsertAt(Node target, Node newNode, int index, Node newChild)
			: base(target, newNode)
		{
			this.Index = index;
			this.NewChild = newChild;
		}

		// Token: 0x17002C0A RID: 11274
		// (get) Token: 0x06010A1C RID: 68124 RVA: 0x00394503 File Offset: 0x00392703
		[DataMember]
		public int Index { get; }

		// Token: 0x17002C0B RID: 11275
		// (get) Token: 0x06010A1D RID: 68125 RVA: 0x0039450B File Offset: 0x0039270B
		public Node NewChild { get; }

		// Token: 0x06010A1E RID: 68126 RVA: 0x00394514 File Offset: 0x00392714
		public override XElement SerializeToXml()
		{
			DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(InsertAt));
			return base.SerializeHelper(dataContractSerializer);
		}

		// Token: 0x06010A1F RID: 68127 RVA: 0x00394538 File Offset: 0x00392738
		public static InsertAt DeserializeFromXml(XElement insertAt)
		{
			if (insertAt == null)
			{
				throw new ArgumentException("insertAt cannot be null.", "insertAt");
			}
			return XmlUtils.DeserializeFromXml<InsertAt>(insertAt);
		}

		// Token: 0x06010A20 RID: 68128 RVA: 0x00394553 File Offset: 0x00392753
		public override bool Equals(object obj)
		{
			return this.Equals(obj as InsertAt);
		}

		// Token: 0x06010A21 RID: 68129 RVA: 0x00394564 File Offset: 0x00392764
		public bool Equals(InsertAt other)
		{
			return other != null && base.Equals(other) && EqualityComparer<Node>.Default.Equals(base.Target, other.Target) && EqualityComparer<Node>.Default.Equals(base.NewNode, other.NewNode) && this.Index == other.Index && EqualityComparer<Node>.Default.Equals(this.NewChild, other.NewChild);
		}

		// Token: 0x06010A22 RID: 68130 RVA: 0x003945D4 File Offset: 0x003927D4
		public override int GetHashCode()
		{
			return ((((1689575296 * -1521134295 + base.GetHashCode()) * -1521134295 + EqualityComparer<Node>.Default.GetHashCode(base.Target)) * -1521134295 + EqualityComparer<Node>.Default.GetHashCode(base.NewNode)) * -1521134295 + this.Index.GetHashCode()) * -1521134295 + EqualityComparer<Node>.Default.GetHashCode(this.NewChild);
		}
	}
}
