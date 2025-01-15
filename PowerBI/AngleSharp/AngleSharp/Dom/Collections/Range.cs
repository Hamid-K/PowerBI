using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Collections
{
	// Token: 0x02000401 RID: 1025
	internal sealed class Range : IRange
	{
		// Token: 0x060020A0 RID: 8352 RVA: 0x00056CE8 File Offset: 0x00054EE8
		public Range(IDocument document)
		{
			this._start = new Range.Boundary
			{
				Offset = 0,
				Node = document
			};
			this._end = new Range.Boundary
			{
				Offset = 0,
				Node = document
			};
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x00056D39 File Offset: 0x00054F39
		private Range(Range.Boundary start, Range.Boundary end)
		{
			this._start = start;
			this._end = end;
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060020A2 RID: 8354 RVA: 0x00056D4F File Offset: 0x00054F4F
		public INode Root
		{
			get
			{
				return this._start.Node.GetRoot();
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x00056D61 File Offset: 0x00054F61
		public IEnumerable<INode> Nodes
		{
			get
			{
				return this.CommonAncestor.GetElements(true, new Predicate<INode>(this.Intersects));
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x00056D7C File Offset: 0x00054F7C
		public INode Head
		{
			get
			{
				return this._start.Node;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060020A5 RID: 8357 RVA: 0x00056D89 File Offset: 0x00054F89
		public int Start
		{
			get
			{
				return this._start.Offset;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x060020A6 RID: 8358 RVA: 0x00056D96 File Offset: 0x00054F96
		public INode Tail
		{
			get
			{
				return this._end.Node;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x00056DA3 File Offset: 0x00054FA3
		public int End
		{
			get
			{
				return this._end.Offset;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x00056DB0 File Offset: 0x00054FB0
		public bool IsCollapsed
		{
			get
			{
				return this._start.Node == this._end.Node;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x00056DCC File Offset: 0x00054FCC
		public INode CommonAncestor
		{
			get
			{
				INode node = this.Head;
				while (node != null && !this.Tail.Contains(node))
				{
					node = node.Parent;
				}
				return node;
			}
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x00056DFC File Offset: 0x00054FFC
		public void StartWith(INode refNode, int offset)
		{
			if (refNode == null)
			{
				throw new ArgumentNullException("refNode");
			}
			if (refNode.NodeType == NodeType.DocumentType)
			{
				throw new DomException(DomError.InvalidNodeType);
			}
			if (offset > refNode.ChildNodes.Length)
			{
				throw new DomException(DomError.IndexSizeError);
			}
			Range.Boundary boundary = new Range.Boundary
			{
				Node = refNode,
				Offset = offset
			};
			if (boundary > this._end || this.Root != refNode.GetRoot())
			{
				this._start = boundary;
			}
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x00056E7C File Offset: 0x0005507C
		public void EndWith(INode refNode, int offset)
		{
			if (refNode == null)
			{
				throw new ArgumentNullException("refNode");
			}
			if (refNode.NodeType == NodeType.DocumentType)
			{
				throw new DomException(DomError.InvalidNodeType);
			}
			if (offset > refNode.ChildNodes.Length)
			{
				throw new DomException(DomError.IndexSizeError);
			}
			Range.Boundary boundary = new Range.Boundary
			{
				Node = refNode,
				Offset = offset
			};
			if (boundary < this._start || this.Root != refNode.GetRoot())
			{
				this._end = boundary;
			}
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x00056EFC File Offset: 0x000550FC
		public void StartBefore(INode refNode)
		{
			if (refNode == null)
			{
				throw new ArgumentNullException("refNode");
			}
			INode parent = refNode.Parent;
			if (parent == null)
			{
				throw new DomException(DomError.InvalidNodeType);
			}
			this._start = new Range.Boundary
			{
				Node = parent,
				Offset = parent.ChildNodes.Index(refNode)
			};
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x00056F54 File Offset: 0x00055154
		public void EndBefore(INode refNode)
		{
			if (refNode == null)
			{
				throw new ArgumentNullException("refNode");
			}
			INode parent = refNode.Parent;
			if (parent == null)
			{
				throw new DomException(DomError.InvalidNodeType);
			}
			this._end = new Range.Boundary
			{
				Node = parent,
				Offset = parent.ChildNodes.Index(refNode)
			};
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x00056FAC File Offset: 0x000551AC
		public void StartAfter(INode refNode)
		{
			if (refNode == null)
			{
				throw new ArgumentNullException("refNode");
			}
			INode parent = refNode.Parent;
			if (parent == null)
			{
				throw new DomException(DomError.InvalidNodeType);
			}
			this._start = new Range.Boundary
			{
				Node = parent,
				Offset = parent.ChildNodes.Index(refNode) + 1
			};
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x00057008 File Offset: 0x00055208
		public void EndAfter(INode refNode)
		{
			if (refNode == null)
			{
				throw new ArgumentNullException("refNode");
			}
			INode parent = refNode.Parent;
			if (parent == null)
			{
				throw new DomException(DomError.InvalidNodeType);
			}
			this._end = new Range.Boundary
			{
				Node = parent,
				Offset = parent.ChildNodes.Index(refNode) + 1
			};
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x00057061 File Offset: 0x00055261
		public void Collapse(bool toStart)
		{
			if (toStart)
			{
				this._end = this._start;
				return;
			}
			this._start = this._end;
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x00057080 File Offset: 0x00055280
		public void Select(INode refNode)
		{
			if (refNode == null)
			{
				throw new ArgumentNullException("refNode");
			}
			INode parent = refNode.Parent;
			if (parent == null)
			{
				throw new DomException(DomError.InvalidNodeType);
			}
			int num = parent.ChildNodes.Index(refNode);
			this._start = new Range.Boundary
			{
				Node = parent,
				Offset = num
			};
			this._end = new Range.Boundary
			{
				Node = parent,
				Offset = num + 1
			};
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x000570FC File Offset: 0x000552FC
		public void SelectContent(INode refNode)
		{
			if (refNode == null)
			{
				throw new ArgumentNullException("refNode");
			}
			if (refNode.NodeType == NodeType.DocumentType)
			{
				throw new DomException(DomError.InvalidNodeType);
			}
			int length = refNode.ChildNodes.Length;
			this._start = new Range.Boundary
			{
				Node = refNode,
				Offset = 0
			};
			this._end = new Range.Boundary
			{
				Node = refNode,
				Offset = length
			};
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x00057174 File Offset: 0x00055374
		public void ClearContent()
		{
			if (this._start.Equals(this._end))
			{
				return;
			}
			Range.Boundary boundary = default(Range.Boundary);
			Range.Boundary start = this._start;
			Range.Boundary end = this._end;
			if (end.Node == start.Node && start.Node is ICharacterData)
			{
				int offset = start.Offset;
				ICharacterData characterData = (ICharacterData)start.Node;
				int num = end.Offset - start.Offset;
				characterData.Replace(offset, num, string.Empty);
				return;
			}
			INode[] array = this.Nodes.Where((INode m) => !this.Intersects(m.Parent)).ToArray<INode>();
			if (!start.Node.IsInclusiveAncestorOf(end.Node))
			{
				INode node = start.Node;
				while (node.Parent != null && node.Parent.IsInclusiveAncestorOf(end.Node))
				{
					node = node.Parent;
				}
				boundary = new Range.Boundary
				{
					Node = node.Parent,
					Offset = node.Parent.ChildNodes.Index(node) + 1
				};
			}
			else
			{
				boundary = start;
			}
			if (start.Node is ICharacterData)
			{
				int offset2 = start.Offset;
				ICharacterData characterData2 = (ICharacterData)start.Node;
				int num2 = end.Offset - start.Offset;
				characterData2.Replace(offset2, num2, string.Empty);
			}
			foreach (INode node2 in array)
			{
				node2.Parent.RemoveChild(node2);
			}
			if (end.Node is ICharacterData)
			{
				int num3 = 0;
				ICharacterData characterData3 = (ICharacterData)end.Node;
				int offset3 = end.Offset;
				characterData3.Replace(num3, offset3, string.Empty);
			}
			this._start = boundary;
			this._end = boundary;
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x00057338 File Offset: 0x00055538
		public IDocumentFragment ExtractContent()
		{
			IDocumentFragment documentFragment = this._start.Node.Owner.CreateDocumentFragment();
			if (this._start.Equals(this._end))
			{
				return documentFragment;
			}
			Range.Boundary boundary = this._start;
			Range.Boundary start = this._start;
			Range.Boundary end = this._end;
			if (start.Node == end.Node && this._start.Node is ICharacterData)
			{
				ICharacterData characterData = (ICharacterData)start.Node;
				int offset = start.Offset;
				int num = end.Offset - start.Offset;
				ICharacterData characterData2 = (ICharacterData)characterData.Clone(true);
				characterData2.Data = characterData.Substring(offset, num);
				documentFragment.AppendChild(characterData2);
				characterData.Replace(offset, num, string.Empty);
				return documentFragment;
			}
			INode node = start.Node;
			while (!node.IsInclusiveAncestorOf(end.Node))
			{
				node = node.Parent;
			}
			INode node2 = ((!start.Node.IsInclusiveAncestorOf(end.Node)) ? node.GetElements(true, new Predicate<INode>(this.IsPartiallyContained)).FirstOrDefault<INode>() : null);
			INode node3 = ((!end.Node.IsInclusiveAncestorOf(start.Node)) ? node.GetElements(true, new Predicate<INode>(this.IsPartiallyContained)).LastOrDefault<INode>() : null);
			List<INode> list = node.GetElements(true, new Predicate<INode>(this.Intersects)).ToList<INode>();
			if (list.OfType<IDocumentType>().Any<IDocumentType>())
			{
				throw new DomException(DomError.HierarchyRequest);
			}
			if (!start.Node.IsInclusiveAncestorOf(end.Node))
			{
				INode node4 = start.Node;
				while (node4.Parent != null && !node4.IsInclusiveAncestorOf(end.Node))
				{
					node4 = node4.Parent;
				}
				boundary = new Range.Boundary
				{
					Node = node4,
					Offset = node4.Parent.ChildNodes.Index(node4) + 1
				};
			}
			if (node2 is ICharacterData)
			{
				ICharacterData characterData3 = (ICharacterData)start.Node;
				int offset2 = start.Offset;
				int num2 = characterData3.Length - start.Offset;
				ICharacterData characterData4 = (ICharacterData)characterData3.Clone(true);
				characterData4.Data = characterData3.Substring(offset2, num2);
				documentFragment.AppendChild(characterData4);
				characterData3.Replace(offset2, num2, string.Empty);
			}
			else if (node2 != null)
			{
				INode node5 = node2.Clone(true);
				documentFragment.AppendChild(node5);
				IDocumentFragment documentFragment2 = new Range(start, new Range.Boundary
				{
					Node = node2,
					Offset = node2.ChildNodes.Length
				}).ExtractContent();
				documentFragment.AppendChild(documentFragment2);
			}
			foreach (INode node6 in list)
			{
				documentFragment.AppendChild(node6);
			}
			if (node3 is ICharacterData)
			{
				ICharacterData characterData5 = (ICharacterData)end.Node;
				ICharacterData characterData6 = (ICharacterData)characterData5.Clone(true);
				characterData6.Data = characterData5.Substring(0, end.Offset);
				documentFragment.AppendChild(characterData6);
				characterData5.Replace(0, end.Offset, string.Empty);
			}
			else if (node3 != null)
			{
				INode node7 = node3.Clone(true);
				documentFragment.AppendChild(node7);
				IDocumentFragment documentFragment3 = new Range(new Range.Boundary
				{
					Node = node3,
					Offset = 0
				}, end).ExtractContent();
				documentFragment.AppendChild(documentFragment3);
			}
			this._start = boundary;
			this._end = boundary;
			return documentFragment;
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000576DC File Offset: 0x000558DC
		public IDocumentFragment CopyContent()
		{
			IDocumentFragment documentFragment = this._start.Node.Owner.CreateDocumentFragment();
			if (this._start.Equals(this._end))
			{
				return documentFragment;
			}
			Range.Boundary start = this._start;
			Range.Boundary end = this._end;
			if (start.Node == end.Node && this._start.Node is ICharacterData)
			{
				ICharacterData characterData = (ICharacterData)start.Node;
				int offset = start.Offset;
				int num = end.Offset - start.Offset;
				ICharacterData characterData2 = (ICharacterData)characterData.Clone(true);
				characterData2.Data = characterData.Substring(offset, num);
				documentFragment.AppendChild(characterData2);
				return documentFragment;
			}
			INode node = start.Node;
			while (!node.IsInclusiveAncestorOf(end.Node))
			{
				node = node.Parent;
			}
			INode node2 = ((!start.Node.IsInclusiveAncestorOf(end.Node)) ? node.GetElements(true, new Predicate<INode>(this.IsPartiallyContained)).FirstOrDefault<INode>() : null);
			INode node3 = ((!end.Node.IsInclusiveAncestorOf(start.Node)) ? node.GetElements(true, new Predicate<INode>(this.IsPartiallyContained)).LastOrDefault<INode>() : null);
			List<INode> list = node.GetElements(true, new Predicate<INode>(this.Intersects)).ToList<INode>();
			if (list.OfType<IDocumentType>().Any<IDocumentType>())
			{
				throw new DomException(DomError.HierarchyRequest);
			}
			if (node2 is ICharacterData)
			{
				ICharacterData characterData3 = (ICharacterData)start.Node;
				int offset2 = start.Offset;
				int num2 = characterData3.Length - start.Offset;
				ICharacterData characterData4 = (ICharacterData)characterData3.Clone(true);
				characterData4.Data = characterData3.Substring(offset2, num2);
				documentFragment.AppendChild(characterData4);
			}
			else if (node2 != null)
			{
				INode node4 = node2.Clone(true);
				documentFragment.AppendChild(node4);
				IDocumentFragment documentFragment2 = new Range(start, new Range.Boundary
				{
					Node = node2,
					Offset = node2.ChildNodes.Length
				}).CopyContent();
				documentFragment.AppendChild(documentFragment2);
			}
			foreach (INode node5 in list)
			{
				documentFragment.AppendChild(node5.Clone(true));
			}
			if (node3 is ICharacterData)
			{
				ICharacterData characterData5 = (ICharacterData)end.Node;
				ICharacterData characterData6 = (ICharacterData)characterData5.Clone(true);
				characterData6.Data = characterData5.Substring(0, end.Offset);
				documentFragment.AppendChild(characterData6);
			}
			else if (node3 != null)
			{
				INode node6 = node3.Clone(true);
				documentFragment.AppendChild(node6);
				IDocumentFragment documentFragment3 = new Range(new Range.Boundary
				{
					Node = node3,
					Offset = 0
				}, end).CopyContent();
				documentFragment.AppendChild(documentFragment3);
			}
			return documentFragment;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x000579C4 File Offset: 0x00055BC4
		public void Insert(INode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			INode node2 = this._start.Node;
			NodeType nodeType = node2.NodeType;
			bool flag = nodeType == NodeType.Text;
			if (nodeType == NodeType.ProcessingInstruction || nodeType == NodeType.Comment || (flag && node2.Parent == null))
			{
				throw new DomException(DomError.HierarchyRequest);
			}
			INode node3 = (flag ? node2 : this._start.ChildAtOffset);
			INode node4 = ((node3 == null) ? node2 : node3.Parent);
			node4.EnsurePreInsertionValidity(node, node3);
			if (flag)
			{
				node3 = ((IText)node2).Split(this._start.Offset);
				node4 = node3.Parent;
			}
			if (node == node3)
			{
				node3 = node3.NextSibling;
			}
			INode parent = node.Parent;
			if (parent != null)
			{
				parent.RemoveChild(node);
			}
			int num = ((node3 == null) ? node4.ChildNodes.Length : node4.ChildNodes.Index(node3));
			num += ((node.NodeType == NodeType.DocumentFragment) ? node.ChildNodes.Length : 1);
			node4.PreInsert(node, node3);
			if (this._start.Equals(this._end))
			{
				this._end = new Range.Boundary
				{
					Node = node4,
					Offset = num
				};
			}
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x00057AF8 File Offset: 0x00055CF8
		public void Surround(INode newParent)
		{
			if (newParent == null)
			{
				throw new ArgumentNullException("newParent");
			}
			if (this.Nodes.Any((INode m) => m.NodeType != NodeType.Text && this.IsPartiallyContained(m)))
			{
				throw new DomException(DomError.InvalidState);
			}
			NodeType nodeType = newParent.NodeType;
			if (nodeType == NodeType.Document || nodeType == NodeType.DocumentType || nodeType == NodeType.DocumentFragment)
			{
				throw new DomException(DomError.InvalidNodeType);
			}
			IDocumentFragment documentFragment = this.ExtractContent();
			while (newParent.HasChildNodes)
			{
				newParent.RemoveChild(newParent.FirstChild);
			}
			this.Insert(newParent);
			newParent.PreInsert(documentFragment, null);
			this.Select(newParent);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x00057B87 File Offset: 0x00055D87
		public IRange Clone()
		{
			return new Range(this._start, this._end);
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x00003C25 File Offset: 0x00001E25
		public void Detach()
		{
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x00057B9C File Offset: 0x00055D9C
		public bool Contains(INode node, int offset)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.GetRoot() != this.Root)
			{
				return false;
			}
			if (node.NodeType == NodeType.DocumentType)
			{
				throw new DomException(DomError.InvalidNodeType);
			}
			if (offset > node.ChildNodes.Length)
			{
				throw new DomException(DomError.IndexSizeError);
			}
			return !this.IsStartAfter(node, offset) && !this.IsEndBefore(node, offset);
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x00057C08 File Offset: 0x00055E08
		public RangePosition CompareBoundaryTo(RangeType how, IRange sourceRange)
		{
			if (sourceRange == null)
			{
				throw new ArgumentNullException("sourceRange");
			}
			if (this.Root != sourceRange.Head.GetRoot())
			{
				throw new DomException(DomError.WrongDocument);
			}
			Range.Boundary boundary = default(Range.Boundary);
			Range.Boundary boundary2 = default(Range.Boundary);
			switch (how)
			{
			case RangeType.StartToStart:
				boundary = this._start;
				boundary2 = new Range.Boundary
				{
					Node = sourceRange.Head,
					Offset = sourceRange.Start
				};
				break;
			case RangeType.StartToEnd:
				boundary = this._end;
				boundary2 = new Range.Boundary
				{
					Node = sourceRange.Head,
					Offset = sourceRange.Start
				};
				break;
			case RangeType.EndToEnd:
				boundary = this._start;
				boundary2 = new Range.Boundary
				{
					Node = sourceRange.Tail,
					Offset = sourceRange.End
				};
				break;
			case RangeType.EndToStart:
				boundary = this._end;
				boundary2 = new Range.Boundary
				{
					Node = sourceRange.Tail,
					Offset = sourceRange.End
				};
				break;
			default:
				throw new DomException(DomError.NotSupported);
			}
			return boundary.CompareTo(boundary2);
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x00057D30 File Offset: 0x00055F30
		public RangePosition CompareTo(INode node, int offset)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (this.Root != this._start.Node.GetRoot())
			{
				throw new DomException(DomError.WrongDocument);
			}
			if (node.NodeType == NodeType.DocumentType)
			{
				throw new DomException(DomError.InvalidNodeType);
			}
			if (offset > node.ChildNodes.Length)
			{
				throw new DomException(DomError.IndexSizeError);
			}
			if (this.IsStartAfter(node, offset))
			{
				return RangePosition.Before;
			}
			if (this.IsEndBefore(node, offset))
			{
				return RangePosition.After;
			}
			return RangePosition.Equal;
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x00057DAC File Offset: 0x00055FAC
		public bool Intersects(INode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (this.Root != node.GetRoot())
			{
				return false;
			}
			INode parent = node.Parent;
			if (parent != null)
			{
				int num = parent.ChildNodes.Index(node);
				return this.IsEndAfter(parent, num) && this.IsStartBefore(parent, num + 1);
			}
			return true;
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x00057E08 File Offset: 0x00056008
		public override string ToString()
		{
			StringBuilder stringBuilder = Pool.NewStringBuilder();
			int start = this.Start;
			int end = this.End;
			IText text = this.Head as IText;
			IText text2 = this.Tail as IText;
			if (text != null && this.Head == this.Tail)
			{
				return text.Substring(start, end - start);
			}
			if (text != null)
			{
				stringBuilder.Append(text.Substring(start, text.Length - start));
			}
			foreach (IText text3 in this.CommonAncestor.Descendents<IText>())
			{
				if (this.IsStartBefore(text3, 0) && this.IsEndAfter(text3, text3.Length))
				{
					stringBuilder.Append(text3.Text);
				}
			}
			if (text2 != null)
			{
				stringBuilder.Append(text2.Substring(0, end));
			}
			return stringBuilder.ToPool();
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x00057F00 File Offset: 0x00056100
		private bool IsStartBefore(INode node, int offset)
		{
			return this._start < new Range.Boundary
			{
				Node = node,
				Offset = offset
			};
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x00057F34 File Offset: 0x00056134
		private bool IsStartAfter(INode node, int offset)
		{
			return this._start > new Range.Boundary
			{
				Node = node,
				Offset = offset
			};
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x00057F68 File Offset: 0x00056168
		private bool IsEndBefore(INode node, int offset)
		{
			return this._end < new Range.Boundary
			{
				Node = node,
				Offset = offset
			};
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x00057F9C File Offset: 0x0005619C
		private bool IsEndAfter(INode node, int offset)
		{
			return this._end > new Range.Boundary
			{
				Node = node,
				Offset = offset
			};
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x00057FD0 File Offset: 0x000561D0
		private bool IsPartiallyContained(INode node)
		{
			bool flag = node.IsInclusiveAncestorOf(this._start.Node);
			bool flag2 = node.IsInclusiveAncestorOf(this._end.Node);
			return (flag && !flag2) || (!flag && flag2);
		}

		// Token: 0x04000D1E RID: 3358
		private Range.Boundary _start;

		// Token: 0x04000D1F RID: 3359
		private Range.Boundary _end;

		// Token: 0x02000530 RID: 1328
		private struct Boundary : IEquatable<Range.Boundary>
		{
			// Token: 0x060026B8 RID: 9912 RVA: 0x0000EE9F File Offset: 0x0000D09F
			public static bool operator >(Range.Boundary a, Range.Boundary b)
			{
				return false;
			}

			// Token: 0x060026B9 RID: 9913 RVA: 0x0000EE9F File Offset: 0x0000D09F
			public static bool operator <(Range.Boundary a, Range.Boundary b)
			{
				return false;
			}

			// Token: 0x060026BA RID: 9914 RVA: 0x00064103 File Offset: 0x00062303
			public bool Equals(Range.Boundary other)
			{
				return this.Node == other.Node && this.Offset == other.Offset;
			}

			// Token: 0x060026BB RID: 9915 RVA: 0x00064123 File Offset: 0x00062323
			public RangePosition CompareTo(Range.Boundary other)
			{
				if (this < other)
				{
					return RangePosition.Before;
				}
				if (this > other)
				{
					return RangePosition.After;
				}
				return RangePosition.Equal;
			}

			// Token: 0x17000AD2 RID: 2770
			// (get) Token: 0x060026BC RID: 9916 RVA: 0x00064146 File Offset: 0x00062346
			public INode ChildAtOffset
			{
				get
				{
					if (this.Node.ChildNodes.Length <= this.Offset)
					{
						return null;
					}
					return this.Node.ChildNodes[this.Offset];
				}
			}

			// Token: 0x040012BB RID: 4795
			public INode Node;

			// Token: 0x040012BC RID: 4796
			public int Offset;
		}
	}
}
