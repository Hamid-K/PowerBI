using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Semantics
{
	// Token: 0x02001EC9 RID: 7881
	[DataContract(Namespace = "Microsoft.ProgramSynthesis.Transformation.Tree.Semantics")]
	public class DeleteChild : Edit, IEquatable<DeleteChild>
	{
		// Token: 0x06010A14 RID: 68116 RVA: 0x003943C1 File Offset: 0x003925C1
		public DeleteChild(Node target, Node newNode, int index)
			: base(target, newNode)
		{
			this.Index = index;
		}

		// Token: 0x17002C09 RID: 11273
		// (get) Token: 0x06010A15 RID: 68117 RVA: 0x003943D2 File Offset: 0x003925D2
		[DataMember]
		public int Index { get; }

		// Token: 0x06010A16 RID: 68118 RVA: 0x003943DC File Offset: 0x003925DC
		public override XElement SerializeToXml()
		{
			DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(DeleteChild));
			return base.SerializeHelper(dataContractSerializer);
		}

		// Token: 0x06010A17 RID: 68119 RVA: 0x00394400 File Offset: 0x00392600
		public static DeleteChild DeserializeFromXml(XElement deleteChild)
		{
			if (deleteChild == null)
			{
				throw new ArgumentException("deleteChild cannot be null.", "deleteChild");
			}
			return XmlUtils.DeserializeFromXml<DeleteChild>(deleteChild);
		}

		// Token: 0x06010A18 RID: 68120 RVA: 0x0039441B File Offset: 0x0039261B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DeleteChild);
		}

		// Token: 0x06010A19 RID: 68121 RVA: 0x0039442C File Offset: 0x0039262C
		public bool Equals(DeleteChild other)
		{
			return other != null && base.Equals(other) && EqualityComparer<Node>.Default.Equals(base.Target, other.Target) && EqualityComparer<Node>.Default.Equals(base.NewNode, other.NewNode) && this.Index == other.Index;
		}

		// Token: 0x06010A1A RID: 68122 RVA: 0x00394488 File Offset: 0x00392688
		public override int GetHashCode()
		{
			return (((268418378 * -1521134295 + base.GetHashCode()) * -1521134295 + EqualityComparer<Node>.Default.GetHashCode(base.Target)) * -1521134295 + EqualityComparer<Node>.Default.GetHashCode(base.NewNode)) * -1521134295 + this.Index.GetHashCode();
		}
	}
}
