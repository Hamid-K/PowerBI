using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling.Tree.TreePathStep;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Tree
{
	// Token: 0x020000E3 RID: 227
	[DataContract(Namespace = "Microsoft.ProgramSynthesis.Wrangling.Tree")]
	[KnownType(typeof(StructNode))]
	[KnownType(typeof(SequenceNode))]
	[KnownType(typeof(HoleNode))]
	[KnownType(typeof(Node[]))]
	[DebuggerDisplay("{Label}")]
	public abstract class Node
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00011312 File Offset: 0x0000F512
		public static IReadOnlyDictionary<string, Node> KnownSynthesisIrrelevantNodes
		{
			get
			{
				return Node._knownSynthesisIrrelevantNodes;
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001131C File Offset: 0x0000F51C
		public static void AddSynthesisIrrelevantNodes(IReadOnlyDictionary<string, Node> labelToNode)
		{
			bool flag = false;
			while (!flag)
			{
				IReadOnlyDictionary<string, Node> knownSynthesisIrrelevantNodes = Node.KnownSynthesisIrrelevantNodes;
				Dictionary<string, Node> dictionary = knownSynthesisIrrelevantNodes.Concat(labelToNode).ToDictionary<string, Node>();
				flag = Interlocked.CompareExchange<IReadOnlyDictionary<string, Node>>(ref Node._knownSynthesisIrrelevantNodes, dictionary, knownSynthesisIrrelevantNodes) == knownSynthesisIrrelevantNodes;
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00011354 File Offset: 0x0000F554
		protected Node(string label, Attributes attributes, Node[] children = null, Node parent = null)
		{
			this.Label = label;
			this.Attributes = attributes;
			this._children = ((children == null || children.Length == 0) ? Node.EmptyNodeCollection : children);
			int count = this._children.Count;
			for (int i = 0; i < count; i++)
			{
				IReadOnlyDictionary<string, Node> knownSynthesisIrrelevantNodes = Node.KnownSynthesisIrrelevantNodes;
				if (knownSynthesisIrrelevantNodes == null || !knownSynthesisIrrelevantNodes.ContainsKey(this._children[i].Label))
				{
					this._children[i].Parent = this;
				}
			}
			this.Parent = parent;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x000113F8 File Offset: 0x0000F5F8
		public IReadOnlyDictionary<string, int> LabelCounts
		{
			get
			{
				IReadOnlyDictionary<string, int> readOnlyDictionary;
				if ((readOnlyDictionary = this._labelCounts) == null)
				{
					readOnlyDictionary = (this._labelCounts = this.EnumerateDescendants.Select((Node node) => node.Label).ToMultiset<string>());
				}
				return readOnlyDictionary;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00011448 File Offset: 0x0000F648
		public int Count
		{
			get
			{
				int? count = this._count;
				if (count == null)
				{
					int? num = (this._count = new int?(this._children.Aggregate(1, (int total, Node next) => total + ((next._children.Count == 0) ? 1 : next.Count))));
					return num.Value;
				}
				return count.GetValueOrDefault();
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x000114AE File Offset: 0x0000F6AE
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x000114B6 File Offset: 0x0000F6B6
		[DataMember]
		public string Label
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x000114BF File Offset: 0x0000F6BF
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x000114C7 File Offset: 0x0000F6C7
		[DataMember]
		public Attributes Attributes { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000114D0 File Offset: 0x0000F6D0
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x000114D8 File Offset: 0x0000F6D8
		[JsonIgnore]
		public Node Parent
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x000114E1 File Offset: 0x0000F6E1
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x000114E9 File Offset: 0x0000F6E9
		[DataMember]
		public Node.Position StartPosition { get; set; } = Node.Position.Missing;

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x000114F2 File Offset: 0x0000F6F2
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x000114FA File Offset: 0x0000F6FA
		[DataMember]
		public Node.Position EndPosition { get; set; } = Node.Position.Missing;

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00011503 File Offset: 0x0000F703
		public bool HasPosition
		{
			get
			{
				return this.StartPosition != Node.Position.Missing && this.EndPosition != Node.Position.Missing;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00011529 File Offset: 0x0000F729
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00011554 File Offset: 0x0000F754
		[DataMember]
		public Node[] Children
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (!(this._children is Node[]))
				{
					this._children = this._children.ToArray<Node>();
				}
				return this._children as Node[];
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			protected set
			{
				this._children = value;
				this.OnModified();
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00011563 File Offset: 0x0000F763
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ReplaceChildren(Node[] children)
		{
			this.Children = children;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001156C File Offset: 0x0000F76C
		public void AddChild(Node child, bool deserialization = false)
		{
			IReadOnlyDictionary<string, Node> knownSynthesisIrrelevantNodes = Node.KnownSynthesisIrrelevantNodes;
			if (knownSynthesisIrrelevantNodes == null || !knownSynthesisIrrelevantNodes.ContainsKey(child.Label))
			{
				child.Parent = this;
			}
			if (this._children.Count == 0)
			{
				this._children = new List<Node> { child };
			}
			else
			{
				if (this._children is Node[])
				{
					this._children = this._children.ToList<Node>();
				}
				this._children.Add(child);
			}
			if (!deserialization)
			{
				this.OnModified();
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000115F0 File Offset: 0x0000F7F0
		public void RemoveChild(Node child)
		{
			if (this._children is Node[])
			{
				this._children = this._children.Where((Node n) => n != child).ToArray<Node>();
			}
			else
			{
				this._children.Remove(child);
			}
			if (this._children.Count == 0)
			{
				this._children = Node.EmptyNodeCollection;
			}
			this.OnModified();
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001166B File Offset: 0x0000F86B
		public void ReplaceChildAt(int i, Node child, bool deserialization = false)
		{
			this._children[i] = child;
			if (!deserialization)
			{
				this.OnModified();
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00011684 File Offset: 0x0000F884
		public Node FindFirstDescendantByLabel(string label)
		{
			if (this.Label == label)
			{
				return this;
			}
			Node[] children = this.Children;
			int num = children.Length;
			for (int i = 0; i < num; i++)
			{
				Node node = children[i].FindFirstDescendantByLabel(label);
				if (node != null)
				{
					return node;
				}
			}
			return null;
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x000116C8 File Offset: 0x0000F8C8
		public IEnumerable<Node> Ancestors
		{
			get
			{
				Node current = this;
				while ((current = current.Parent) != null)
				{
					yield return current;
				}
				yield break;
			}
		}

		// Token: 0x0600052D RID: 1325
		public abstract Node DeepClone();

		// Token: 0x0600052E RID: 1326 RVA: 0x000116D8 File Offset: 0x0000F8D8
		public bool IsIrrelevantNode()
		{
			Node node;
			return Node.KnownSynthesisIrrelevantNodes != null && Node.KnownSynthesisIrrelevantNodes.TryGetValue(this.Label, out node) && node == this;
		}

		// Token: 0x0600052F RID: 1327
		public abstract T AcceptVisitor<T>(NodeVisitor<T> visitor);

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00011708 File Offset: 0x0000F908
		public IEnumerable<Node> EnumerateDescendantsPreOrder
		{
			get
			{
				Stack<Node> stack = new Stack<Node>();
				stack.Push(this);
				Node current;
				while (stack.TryPop(out current))
				{
					yield return current;
					using (IEnumerator<Node> enumerator = current.Children.Reverse<Node>().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Node node = enumerator.Current;
							stack.Push(node);
						}
						continue;
					}
					break;
				}
				yield break;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00011718 File Offset: 0x0000F918
		public IEnumerable<Node> EnumerateDescendantsPostOrder
		{
			get
			{
				Stack<Record<Node, bool>> stack = new Stack<Record<Node, bool>>();
				stack.Push(Record.Create<Node, bool>(this, false));
				Record<Node, bool> record;
				while (stack.TryPop(out record))
				{
					Node item = record.Item1;
					if (!record.Item2)
					{
						stack.Push(Record.Create<Node, bool>(item, true));
						using (IEnumerator<Node> enumerator = item.Children.Reverse<Node>().GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Node node = enumerator.Current;
								stack.Push(Record.Create<Node, bool>(node, false));
							}
							continue;
						}
						break;
					}
					yield return item;
				}
				yield break;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00011728 File Offset: 0x0000F928
		public IEnumerable<Node> EnumerateDescendants
		{
			get
			{
				return this.EnumerateDescendantsPreOrder;
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00011730 File Offset: 0x0000F930
		public Node WithChildren(Node[] newChildren)
		{
			HoleNode holeNode = this as HoleNode;
			Node node;
			if (holeNode == null)
			{
				if (!(this is StructNode))
				{
					SequenceNode sequenceNode = this as SequenceNode;
					if (sequenceNode == null)
					{
						throw new NotImplementedException("Unknown node type: " + base.GetType().Name);
					}
					node = new SequenceNode(this.Label, this.Attributes, null, newChildren, sequenceNode.Separator);
				}
				else
				{
					node = (this.IsIrrelevantNode() ? this : StructNode.Create(this.Label, this.Attributes, newChildren, null));
				}
			}
			else
			{
				node = new HoleNode(holeNode.Type, this.Label, this.Attributes, holeNode.Predicates, null);
			}
			Node node2 = node;
			node2.StartPosition = this.StartPosition;
			node2.EndPosition = this.EndPosition;
			return node2;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000117EF File Offset: 0x0000F9EF
		public IReadOnlyList<TreePath> TreePathsToDescendants(Predicate<Node> predicate)
		{
			if (predicate(this))
			{
				return new TreePath[]
				{
					new TreePath(new string[0])
				};
			}
			return Node.TreePathsToDescendants(this, null, predicate);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00011818 File Offset: 0x0000FA18
		private static IReadOnlyList<TreePath> TreePathsToDescendants(Node root, IReadOnlyList<IReadOnlyList<TreePathStep>> paths, Predicate<Node> predicate)
		{
			if (predicate(root))
			{
				return paths.Select((IReadOnlyList<TreePathStep> steps) => new TreePath(steps)).ToArray<TreePath>();
			}
			List<TreePath> list = new List<TreePath>();
			Node[] children = root.Children;
			int num = root.Children.Length;
			for (int i = 0; i < num; i++)
			{
				IReadOnlyList<TreePathStep> steps = Node.AllPossibleStepsTo(root, children[i]);
				int count = steps.Count;
				if (paths == null)
				{
					paths = Enumerable.Repeat<List<TreePathStep>>(new List<TreePathStep>(), count).ToArray<List<TreePathStep>>();
				}
				List<List<TreePathStep>> list2 = (from index in Enumerable.Range(0, count)
					select paths[index].AppendItem(steps[index]).ToList<TreePathStep>()).ToList<List<TreePathStep>>();
				IReadOnlyList<TreePath> readOnlyList = Node.TreePathsToDescendants(children[i], list2, predicate);
				list.AddRange(readOnlyList);
			}
			return list;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00011928 File Offset: 0x0000FB28
		private static IReadOnlyList<TreePathStep> AllPossibleStepsTo(Node source, Node dest)
		{
			int num = 0;
			int num2 = 0;
			string label = dest.Label;
			Node[] children = source.Children;
			int num3 = children.Length;
			for (int i = 0; i < num3; i++)
			{
				num++;
				if (children[i].Label == label)
				{
					num2++;
				}
				if (children[i] == dest)
				{
					break;
				}
			}
			return new TreePathStep[]
			{
				new KthLabelStep(label, num2),
				new KthStep(num)
			};
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00011998 File Offset: 0x0000FB98
		public XElement SerializeToXml()
		{
			DataContractSerializer dataContractSerializer = new DataContractSerializer(base.GetType());
			XElement xelement;
			using (StringWriter stringWriter = new StringWriter())
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter)
				{
					Formatting = global::System.Xml.Formatting.None
				})
				{
					dataContractSerializer.WriteObject(xmlTextWriter, this);
					xelement = XElement.Parse(stringWriter.GetStringBuilder().ToString(), LoadOptions.PreserveWhitespace);
				}
			}
			return xelement;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00011A14 File Offset: 0x0000FC14
		public static void AddNonSerializableInfoToTree(Node node)
		{
			if (node == null)
			{
				throw new ArgumentException("Failed to add parents to the tree. node cannot be null.", "node");
			}
			node.Children.ForEach(delegate(Node c)
			{
				c.Parent = node;
				Node.AddNonSerializableInfoToTree(c);
			});
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00011A64 File Offset: 0x0000FC64
		public string ToText()
		{
			string text = "{{type:\"{0}\", \r\n                \"label\":\"{1}\", \r\n                \"attributes\":\"[{2}]\", \r\n                \"children\": [{3}]}}";
			object[] array = new object[4];
			array[0] = base.GetType().Name;
			array[1] = this.Label;
			array[2] = string.Join(", ", new object[] { this.Attributes });
			array[3] = string.Join(", ", this.Children.Select((Node c) => c.ToText()));
			return FormattableString.Invariant(FormattableStringFactory.Create(text, array));
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00011AF0 File Offset: 0x0000FCF0
		public Node DCap(int d)
		{
			Node[] array = ((d == 1) ? null : this.Children.Select((Node c) => c.DCap(d - 1)).ToArray<Node>());
			if (this is StructNode)
			{
				return StructNode.Create(this.Label, this.Attributes, array, null);
			}
			SequenceNode sequenceNode = this as SequenceNode;
			if (sequenceNode != null)
			{
				return new SequenceNode(this.Label, this.Attributes, null, array, sequenceNode.Separator);
			}
			throw new NotImplementedException("Unknown node type: " + base.GetType().Name);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00011B8D File Offset: 0x0000FD8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetCount(int count)
		{
			this._count = new int?(count);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00011B9B File Offset: 0x0000FD9B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetHashCode(int hashCode)
		{
			this._hashCode = new int?(hashCode);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00011BAC File Offset: 0x0000FDAC
		public void Serialize(JsonWriter jsonWriter)
		{
			jsonWriter.WriteStartObject();
			jsonWriter.WritePropertyName("Kind");
			jsonWriter.WriteValue(base.GetType().Name);
			jsonWriter.WritePropertyName("Label");
			jsonWriter.WriteValue(this.Label);
			SequenceNode sequenceNode = this as SequenceNode;
			if (sequenceNode != null)
			{
				jsonWriter.WritePropertyName("Separator");
				sequenceNode.Separator.Serialize(jsonWriter);
			}
			HoleNode holeNode = this as HoleNode;
			if (holeNode != null)
			{
				jsonWriter.WritePropertyName("Predicates");
				holeNode.Predicates.Serialize(jsonWriter);
				jsonWriter.WritePropertyName("Type");
				jsonWriter.WriteValue(holeNode.Type.ToString());
			}
			jsonWriter.WritePropertyName("Attributes");
			jsonWriter.WriteStartArray();
			foreach (Attributes.Attribute attribute in this.Attributes.AllAttributes)
			{
				jsonWriter.WriteStartObject();
				jsonWriter.WritePropertyName("Name");
				jsonWriter.WriteValue(attribute.Name);
				jsonWriter.WritePropertyName("Value");
				jsonWriter.WriteValue(attribute.Value);
				jsonWriter.WriteEndObject();
			}
			jsonWriter.WriteEndArray();
			jsonWriter.WritePropertyName("Children");
			jsonWriter.WriteStartArray();
			Node[] children = this.Children;
			for (int i = 0; i < children.Length; i++)
			{
				children[i].Serialize(jsonWriter);
			}
			jsonWriter.WriteEndArray();
			jsonWriter.WriteEndObject();
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00011D18 File Offset: 0x0000FF18
		public static Node Deserialize(JObject nodeJson)
		{
			string text = nodeJson.Value<string>("Kind");
			string text2 = nodeJson.Value<string>("Label");
			List<Attributes.Attribute> list = new List<Attributes.Attribute>();
			foreach (JToken jtoken in (nodeJson.Property("Attributes").Value as JArray))
			{
				string text3 = jtoken.Value<string>("Name");
				string text4 = jtoken.Value<string>("Value");
				list.Add(Attributes.Attribute.Create(text3, text4));
			}
			Node[] array = (nodeJson.Property("Children").Value as JArray).Select((JToken cj) => Node.Deserialize(cj as JObject)).ToArray<Node>();
			if (text == "StructNode")
			{
				Node node;
				if (Node.KnownSynthesisIrrelevantNodes.TryGetValue(text2, out node))
				{
					return node;
				}
				return StructNode.Create(text2, new Attributes(list), array, null);
			}
			else
			{
				if (text == "SequenceNode")
				{
					Node node2 = Node.Deserialize(nodeJson.Property("Separator").Value as JObject);
					return new SequenceNode(text2, new Attributes(list), null, array, node2);
				}
				if (text == "HoleNode")
				{
					UnaryPredicates unaryPredicates = UnaryPredicates.Deserialize(nodeJson.Property("Predicates").Value as JObject);
					return new HoleNode((HoleNodeType)Enum.Parse(typeof(HoleNodeType), nodeJson.Value<string>("Type")), text2, new Attributes(list), unaryPredicates, null);
				}
				throw new NotImplementedException("Unknown node kind: " + text);
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00011EC8 File Offset: 0x000100C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void OnModified()
		{
			Node node = this;
			while ((node != null && node._hashCode != null) || (node != null && node._count != null) || ((node != null) ? node._labelCounts : null) != null)
			{
				node._count = null;
				node._hashCode = null;
				node._labelCounts = null;
				node = node.Parent;
			}
		}

		// Token: 0x04000229 RID: 553
		protected int? _count;

		// Token: 0x0400022A RID: 554
		protected int? _hashCode;

		// Token: 0x0400022B RID: 555
		public static readonly Node[] EmptyNodeCollection = new Node[0];

		// Token: 0x0400022C RID: 556
		private static IReadOnlyDictionary<string, Node> _knownSynthesisIrrelevantNodes = new Dictionary<string, Node>();

		// Token: 0x0400022D RID: 557
		protected IReadOnlyDictionary<string, int> _labelCounts;

		// Token: 0x04000233 RID: 563
		private IList<Node> _children;

		// Token: 0x04000234 RID: 564
		private const string Kind = "Kind";

		// Token: 0x020000E4 RID: 228
		[DataContract]
		[DebuggerDisplay("({Line}:{Column})")]
		public struct Position : IEquatable<Node.Position>
		{
			// Token: 0x06000541 RID: 1345 RVA: 0x00011F44 File Offset: 0x00010144
			private Position(int line, int column)
			{
				this.Line = line;
				this.Column = column;
			}

			// Token: 0x06000542 RID: 1346 RVA: 0x00011F54 File Offset: 0x00010154
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static Node.Position Of(int line, int column)
			{
				if (line < 0 || column < 0)
				{
					throw new ArgumentException(string.Format("Invalid line or column for position: ({0}, {1})", line, column));
				}
				return new Node.Position(line, column);
			}

			// Token: 0x06000543 RID: 1347 RVA: 0x00011F81 File Offset: 0x00010181
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static Node.Position Of_UnChecked(int line, int column)
			{
				return new Node.Position(line, column);
			}

			// Token: 0x06000544 RID: 1348 RVA: 0x00011F8A File Offset: 0x0001018A
			public bool Equals(Node.Position other)
			{
				return this.Line == other.Line && this.Column == other.Column;
			}

			// Token: 0x06000545 RID: 1349 RVA: 0x00011FAC File Offset: 0x000101AC
			public override bool Equals(object obj)
			{
				if (obj is Node.Position)
				{
					Node.Position position = (Node.Position)obj;
					return this.Equals(position);
				}
				return false;
			}

			// Token: 0x06000546 RID: 1350 RVA: 0x00011FD1 File Offset: 0x000101D1
			public override int GetHashCode()
			{
				return (this.Line * 397) ^ this.Column;
			}

			// Token: 0x06000547 RID: 1351 RVA: 0x00011FE6 File Offset: 0x000101E6
			public static bool operator !=(Node.Position left, Node.Position right)
			{
				return !object.Equals(left, right);
			}

			// Token: 0x06000548 RID: 1352 RVA: 0x00011FFC File Offset: 0x000101FC
			public static bool operator ==(Node.Position left, Node.Position right)
			{
				return object.Equals(left, right);
			}

			// Token: 0x06000549 RID: 1353 RVA: 0x0001200F File Offset: 0x0001020F
			public static bool operator <=(Node.Position left, Node.Position right)
			{
				return left.Line < right.Line || (left.Line == right.Line && left.Column <= right.Column);
			}

			// Token: 0x0600054A RID: 1354 RVA: 0x00012042 File Offset: 0x00010242
			public static bool operator <(Node.Position left, Node.Position right)
			{
				return left.Line < right.Line || (left.Line == right.Line && left.Column < right.Column);
			}

			// Token: 0x0600054B RID: 1355 RVA: 0x00012072 File Offset: 0x00010272
			public static bool operator >=(Node.Position left, Node.Position right)
			{
				return !(left < right);
			}

			// Token: 0x0600054C RID: 1356 RVA: 0x0001207E File Offset: 0x0001027E
			public static bool operator >(Node.Position left, Node.Position right)
			{
				return !(left <= right);
			}

			// Token: 0x04000235 RID: 565
			[DataMember]
			public readonly int Line;

			// Token: 0x04000236 RID: 566
			[DataMember]
			public readonly int Column;

			// Token: 0x04000237 RID: 567
			public static readonly Node.Position Missing = new Node.Position(-1, -1);
		}
	}
}
