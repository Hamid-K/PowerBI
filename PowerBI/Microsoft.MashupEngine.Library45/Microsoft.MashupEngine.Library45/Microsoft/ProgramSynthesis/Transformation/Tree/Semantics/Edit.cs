using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Tree;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Semantics
{
	// Token: 0x02001EC7 RID: 7879
	[DataContract(Namespace = "Microsoft.ProgramSynthesis.Transformation.Tree.Semantics")]
	public abstract class Edit : IEquatable<Edit>
	{
		// Token: 0x060109F7 RID: 68087 RVA: 0x00393B8B File Offset: 0x00391D8B
		protected Edit(Node target, Node newNode)
		{
			this.Target = target;
			this.NewNode = newNode;
			this.ComputeImpactedLabels();
		}

		// Token: 0x17002C06 RID: 11270
		// (get) Token: 0x060109F9 RID: 68089 RVA: 0x00393BB1 File Offset: 0x00391DB1
		// (set) Token: 0x060109F8 RID: 68088 RVA: 0x00393BA8 File Offset: 0x00391DA8
		[DataMember]
		public Node Target { get; set; }

		// Token: 0x17002C07 RID: 11271
		// (get) Token: 0x060109FB RID: 68091 RVA: 0x00393BC2 File Offset: 0x00391DC2
		// (set) Token: 0x060109FA RID: 68090 RVA: 0x00393BB9 File Offset: 0x00391DB9
		[DataMember]
		public Node NewNode { get; set; }

		// Token: 0x17002C08 RID: 11272
		// (get) Token: 0x060109FC RID: 68092 RVA: 0x00393BCC File Offset: 0x00391DCC
		[IgnoreDataMember]
		public Dictionary<string, int> ImpactedLabels
		{
			get
			{
				Dictionary<string, int> dictionary;
				if ((dictionary = this._impactedLabels) == null)
				{
					dictionary = (this._impactedLabels = this.ComputeImpactedLabels());
				}
				return dictionary;
			}
		}

		// Token: 0x060109FD RID: 68093 RVA: 0x00393BF2 File Offset: 0x00391DF2
		public bool Equals(Edit other)
		{
			return other != null && (this == other || (object.Equals(this.Target, other.Target) && object.Equals(this.NewNode, other.NewNode)));
		}

		// Token: 0x060109FE RID: 68094 RVA: 0x00393C25 File Offset: 0x00391E25
		public void Serialize(JsonTextWriter jsonWriter)
		{
			jsonWriter.WriteStartObject();
			jsonWriter.WritePropertyName("Target");
			this.Target.Serialize(jsonWriter);
			jsonWriter.WritePropertyName("NewNode");
			this.NewNode.Serialize(jsonWriter);
			jsonWriter.WriteEndObject();
		}

		// Token: 0x060109FF RID: 68095 RVA: 0x00393C64 File Offset: 0x00391E64
		public static Edit Deserialize(JObject jObject)
		{
			Node node = Node.Deserialize(jObject.Property("Target").Value as JObject);
			Node node2 = Node.Deserialize(jObject.Property("NewNode").Value as JObject);
			return Edit.CreateEdit(node, node2);
		}

		// Token: 0x06010A00 RID: 68096 RVA: 0x00393CAC File Offset: 0x00391EAC
		private Dictionary<string, int> ComputeImpactedLabels()
		{
			if (this.NewNode == null || this.Target == null)
			{
				return null;
			}
			return this.NewNode.LabelCounts.MultisetSignedDifference(this.Target.LabelCounts, null);
		}

		// Token: 0x06010A01 RID: 68097 RVA: 0x00393CDC File Offset: 0x00391EDC
		public static Edit CreateEdit(Node target, Node newNode)
		{
			DeleteChild deleteChild;
			if (Edit.TryCreateDeleteOperation(target, newNode, out deleteChild))
			{
				return deleteChild;
			}
			InsertAt insertAt;
			if (Edit.TryCreateInsertAtOperation(target, newNode, out insertAt))
			{
				return insertAt;
			}
			ReplaceChildren replaceChildren;
			if (Edit.TryCreateReplaceChildrenOperation(target, newNode, out replaceChildren))
			{
				return replaceChildren;
			}
			return new Update(newNode, target);
		}

		// Token: 0x06010A02 RID: 68098 RVA: 0x00393D18 File Offset: 0x00391F18
		public static bool TryCreateInsertAtOperation(Node location, Node newNode, out InsertAt insertAt)
		{
			insertAt = null;
			if (location.Label == newNode.Label && newNode.Children.Count<Node>() == location.Children.Count<Node>() + 1 && location.Children.IsSubsequenceOf(newNode.Children))
			{
				int num = location.Children.Count<Node>();
				foreach (Record<int, Node> record in location.Children.Enumerate<Node>())
				{
					if (!record.Item2.Equals(newNode.Children[record.Item1]))
					{
						num = record.Item1;
						break;
					}
				}
				insertAt = new InsertAt(location, newNode, num, newNode.Children[num]);
				return true;
			}
			return false;
		}

		// Token: 0x06010A03 RID: 68099 RVA: 0x00393DF0 File Offset: 0x00391FF0
		public static bool TryCreateReplaceChildrenOperation(Node target, Node newNode, out ReplaceChildren edit)
		{
			edit = null;
			if (target.Label != newNode.Label)
			{
				return false;
			}
			Tuple<Node, List<int>, List<Node>> tuple = Edit.ComputeReplacedNodes(target, newNode);
			if (tuple == null || tuple.Item2.Count == target.Children.Length || tuple.Item3.Count == newNode.Children.Length)
			{
				return false;
			}
			edit = new ReplaceChildren(target, newNode, tuple.Item2.ToArray(), tuple.Item3);
			return true;
		}

		// Token: 0x06010A04 RID: 68100 RVA: 0x00393E68 File Offset: 0x00392068
		public static bool TryCreateDeleteOperation(Node location, Node newNode, out DeleteChild deleteChild)
		{
			deleteChild = null;
			if (location.Label == newNode.Label && newNode.Children.Count<Node>() == location.Children.Count<Node>() - 1 && newNode.Children.IsSubsequenceOf(location.Children))
			{
				int num = location.Children.ZipWith(newNode.Children).TakeWhile((Record<Node, Node> x) => x.Item1.Equals(x.Item2)).Count<Record<Node, Node>>();
				deleteChild = new DeleteChild(location, newNode, num);
				return true;
			}
			return false;
		}

		// Token: 0x06010A05 RID: 68101 RVA: 0x00393F00 File Offset: 0x00392100
		internal static Tuple<Node, List<int>, List<Node>> ComputeReplacedNodes(Node originalNode, Node modifiedNode)
		{
			int num = originalNode.Children.ZipWith(modifiedNode.Children).TakeWhile((Record<Node, Node> n) => n.Item1.Equals(n.Item2)).Count<Record<Node, Node>>();
			int num2 = originalNode.Children.Reverse<Node>().ZipWith(modifiedNode.Children.Reverse<Node>()).TakeWhile((Record<Node, Node> n) => n.Item1.Equals(n.Item2))
				.Count<Record<Node, Node>>();
			int num3 = num + num2;
			if (num3 >= originalNode.Children.Length || num3 >= modifiedNode.Children.Length)
			{
				return null;
			}
			return Tuple.Create<Node, List<int>, List<Node>>(originalNode, Enumerable.Range(num, originalNode.Children.Length - num3).ToList<int>(), modifiedNode.Children.Skip(num).Take(modifiedNode.Children.Length - num3).ToList<Node>());
		}

		// Token: 0x06010A06 RID: 68102 RVA: 0x00393FE4 File Offset: 0x003921E4
		public double? ComputeDistance(Edit other)
		{
			if (this.ImpactedLabels == null || ((other != null) ? other.ImpactedLabels : null) == null)
			{
				return null;
			}
			double num = 0.0;
			foreach (KeyValuePair<string, int> keyValuePair in this.ImpactedLabels)
			{
				int num2;
				other.ImpactedLabels.TryGetValue(keyValuePair.Key, out num2);
				num += Math.Pow((double)(keyValuePair.Value - num2), 2.0);
			}
			foreach (KeyValuePair<string, int> keyValuePair2 in other.ImpactedLabels)
			{
				if (!this.ImpactedLabels.ContainsKey(keyValuePair2.Key))
				{
					num += Math.Pow((double)keyValuePair2.Value, 2.0);
				}
			}
			return new double?(Math.Sqrt(num));
		}

		// Token: 0x06010A07 RID: 68103 RVA: 0x00394100 File Offset: 0x00392300
		[OnSerializing]
		internal void OnSerializingMethod(StreamingContext context)
		{
			Node parent = this.Target.Parent;
			this._targetNodeIndex = ((parent != null) ? parent.Children.IndexOfByReference(this.Target) : null);
			if (this._targetNodeIndex != null)
			{
				this.Target = this.Target.Parent;
			}
			Node parent2 = this.NewNode.Parent;
			this._newNodeIndex = ((parent2 != null) ? parent2.Children.IndexOfByReference(this.NewNode) : null);
			if (this._newNodeIndex != null)
			{
				this.NewNode = this.NewNode.Parent;
			}
		}

		// Token: 0x06010A08 RID: 68104 RVA: 0x003941AC File Offset: 0x003923AC
		[OnSerialized]
		internal void OnSerializedMethod(StreamingContext context)
		{
			if (this._targetNodeIndex != null)
			{
				this.Target = this.Target.Children[this._targetNodeIndex.Value];
				this._targetNodeIndex = null;
			}
			if (this._newNodeIndex != null)
			{
				this.NewNode = this.NewNode.Children[this._newNodeIndex.Value];
				this._newNodeIndex = null;
			}
		}

		// Token: 0x06010A09 RID: 68105 RVA: 0x00394228 File Offset: 0x00392428
		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
			if (this._targetNodeIndex != null)
			{
				this.Target = this.ModifySerializedNode(this.Target, this._targetNodeIndex.Value);
				this._targetNodeIndex = null;
			}
			if (this._newNodeIndex != null)
			{
				this.NewNode = this.ModifySerializedNode(this.NewNode, this._newNodeIndex.Value);
				this._newNodeIndex = null;
			}
		}

		// Token: 0x06010A0A RID: 68106 RVA: 0x003942A4 File Offset: 0x003924A4
		private Node ModifySerializedNode(Node node, int index)
		{
			if (node.Children.Length < index)
			{
				throw new ArgumentException("Invalid index for the child node");
			}
			Node node2 = node;
			node = node2.Children[index];
			node.Parent = node2;
			Node.AddNonSerializableInfoToTree(node);
			return node;
		}

		// Token: 0x06010A0B RID: 68107
		public abstract XElement SerializeToXml();

		// Token: 0x06010A0C RID: 68108 RVA: 0x003942E1 File Offset: 0x003924E1
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((Edit)obj)));
		}

		// Token: 0x06010A0D RID: 68109 RVA: 0x00394310 File Offset: 0x00392510
		protected XElement SerializeHelper(DataContractSerializer ser)
		{
			XElement xelement;
			using (StringWriter stringWriter = new StringWriter())
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter)
				{
					Formatting = global::System.Xml.Formatting.None
				})
				{
					ser.WriteObject(xmlTextWriter, this);
					xelement = XElement.Parse(stringWriter.GetStringBuilder().ToString(), LoadOptions.PreserveWhitespace);
				}
			}
			return xelement;
		}

		// Token: 0x06010A0E RID: 68110 RVA: 0x00394380 File Offset: 0x00392580
		public override int GetHashCode()
		{
			return (((this.Target != null) ? this.Target.GetHashCode() : 0) * 433) ^ ((this.NewNode != null) ? this.NewNode.GetHashCode() : 0);
		}

		// Token: 0x04006359 RID: 25433
		[DataMember]
		private int? _newNodeIndex;

		// Token: 0x0400635A RID: 25434
		[DataMember]
		private int? _targetNodeIndex;

		// Token: 0x0400635B RID: 25435
		private Dictionary<string, int> _impactedLabels;
	}
}
