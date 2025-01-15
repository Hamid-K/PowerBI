using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.Tree
{
	// Token: 0x020000E2 RID: 226
	[DataContract(Namespace = "Microsoft.ProgramSynthesis.Wrangling.Tree")]
	[KnownType(typeof(SequenceNode))]
	[KnownType(typeof(StructNode))]
	[KnownType(typeof(HoleNode))]
	[KnownType(typeof(Node[]))]
	public class HoleNode : StructNode, IEquatable<HoleNode>
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x0001116A File Offset: 0x0000F36A
		public static int GetNewTemplateId()
		{
			HoleNode._templateId++;
			return HoleNode._templateId - 1;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001117F File Offset: 0x0000F37F
		public static void NotifyUsedTemplateId(int id)
		{
			HoleNode._templateId = ((HoleNode._templateId > id) ? HoleNode._templateId : (id + 1));
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00011198 File Offset: 0x0000F398
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x000111A0 File Offset: 0x0000F3A0
		[DataMember]
		public HoleNodeType Type { get; private set; }

		// Token: 0x0600050B RID: 1291 RVA: 0x000111A9 File Offset: 0x0000F3A9
		internal HoleNode(HoleNodeType type, string hole_ID, Attributes attributes, UnaryPredicates predicates = null, Node parent = null)
			: base(hole_ID, attributes, null, parent)
		{
			this.Type = type;
			this.Predicates = predicates ?? UnaryPredicates.Empty;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000111CE File Offset: 0x0000F3CE
		public new static StructNode Create(string label, Attributes attributes, Node[] children = null, Node parent = null)
		{
			throw new Exception("Use HoleNode.CreateTrueHole or HoleNode.CreateSameNode");
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000111DA File Offset: 0x0000F3DA
		public static HoleNode CreateTrueHole(string hole_ID, Attributes attributes, UnaryPredicates predicates = null, Node parent = null)
		{
			return new HoleNode(HoleNodeType.TrueHole, hole_ID, attributes, predicates, parent);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000111E6 File Offset: 0x0000F3E6
		public static HoleNode CreateSameNode(string hole_ID, Attributes attributes, UnaryPredicates predicates = null, Node parent = null)
		{
			return new HoleNode(HoleNodeType.SameNode, hole_ID, attributes, predicates, parent);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000111F4 File Offset: 0x0000F3F4
		public bool Equals(HoleNode other)
		{
			return other != null && (this == other || (this.Type == other.Type && base.Attributes.Equals(other.Attributes) && this.Predicates.Equals(other.Predicates)));
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00011240 File Offset: 0x0000F440
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((HoleNode)obj)));
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00011270 File Offset: 0x0000F470
		public override int GetHashCode()
		{
			return ((base.Label.GetHashCode() * 107 + this.Type.GetHashCode()) * 107 + this.Predicates.GetHashCode()) * 107 + base.Attributes.GetHashCode();
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000112BE File Offset: 0x0000F4BE
		public override Node DeepClone()
		{
			if (base.IsIrrelevantNode())
			{
				return this;
			}
			return new HoleNode(this.Type, base.Label, base.Attributes, this.Predicates, null);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000112E8 File Offset: 0x0000F4E8
		[DebuggerStepThrough]
		public override T AcceptVisitor<T>(NodeVisitor<T> visitor)
		{
			return visitor.VisitStruct(this);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000112F1 File Offset: 0x0000F4F1
		public new static HoleNode DeserializeFromXml(XElement node)
		{
			if (node == null)
			{
				throw new ArgumentException("Failed to deserialize HoleNode. node cannot be null.", "node");
			}
			HoleNode holeNode = XmlUtils.DeserializeFromXml<HoleNode>(node);
			Node.AddNonSerializableInfoToTree(holeNode);
			return holeNode;
		}

		// Token: 0x04000226 RID: 550
		private static int _templateId;

		// Token: 0x04000227 RID: 551
		[DataMember]
		public UnaryPredicates Predicates;
	}
}
