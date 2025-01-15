using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020000F5 RID: 245
	internal sealed class BufferingXmlReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x06000E60 RID: 3680 RVA: 0x00021FFC File Offset: 0x000201FC
		internal BufferingXmlReader(XmlReader reader, Uri parentXmlBaseUri, Uri documentBaseUri, bool disableXmlBase, int maxInnerErrorDepth)
		{
			this.reader = reader;
			this.lineInfo = reader as IXmlLineInfo;
			this.documentBaseUri = documentBaseUri;
			this.disableXmlBase = disableXmlBase;
			this.maxInnerErrorDepth = maxInnerErrorDepth;
			XmlNameTable nameTable = this.reader.NameTable;
			this.XmlNamespace = nameTable.Add("http://www.w3.org/XML/1998/namespace");
			this.XmlBaseAttributeName = nameTable.Add("base");
			this.ODataMetadataNamespace = nameTable.Add("http://docs.oasis-open.org/odata/ns/metadata");
			this.ODataNamespace = nameTable.Add("http://docs.oasis-open.org/odata/ns/data");
			this.ODataErrorElementName = nameTable.Add("error");
			this.bufferedNodes = new LinkedList<BufferingXmlReader.BufferedNode>();
			this.currentBufferedNode = null;
			this.endOfInputBufferedNode = BufferingXmlReader.BufferedNode.CreateEndOfInput(this.reader.NameTable);
			this.xmlBaseStack = new Stack<BufferingXmlReader.XmlBaseDefinition>();
			if (parentXmlBaseUri != null)
			{
				this.xmlBaseStack.Push(new BufferingXmlReader.XmlBaseDefinition(parentXmlBaseUri, 0));
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x000220E8 File Offset: 0x000202E8
		public override XmlNodeType NodeType
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.NodeType;
				}
				return this.currentBufferedNodeToReport.Value.NodeType;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0002210E File Offset: 0x0002030E
		public override bool IsEmptyElement
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.IsEmptyElement;
				}
				return this.currentBufferedNodeToReport.Value.IsEmptyElement;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x00022134 File Offset: 0x00020334
		public override string LocalName
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.LocalName;
				}
				return this.currentBufferedNodeToReport.Value.LocalName;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0002215A File Offset: 0x0002035A
		public override string Prefix
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.Prefix;
				}
				return this.currentBufferedNodeToReport.Value.Prefix;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x00022180 File Offset: 0x00020380
		public override string NamespaceURI
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.NamespaceURI;
				}
				return this.currentBufferedNodeToReport.Value.NamespaceUri;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x000221A6 File Offset: 0x000203A6
		public override string Value
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.Value;
				}
				return this.currentBufferedNodeToReport.Value.Value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x000221CC File Offset: 0x000203CC
		public override int Depth
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.Depth;
				}
				return this.currentBufferedNodeToReport.Value.Depth;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x000221F2 File Offset: 0x000203F2
		public override bool EOF
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.EOF;
				}
				return this.IsEndOfInputNode(this.currentBufferedNodeToReport.Value);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x00022219 File Offset: 0x00020419
		public override ReadState ReadState
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.ReadState;
				}
				if (this.IsEndOfInputNode(this.currentBufferedNodeToReport.Value))
				{
					return ReadState.EndOfFile;
				}
				if (this.currentBufferedNodeToReport.Value.NodeType != XmlNodeType.None)
				{
					return ReadState.Interactive;
				}
				return ReadState.Initial;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00022259 File Offset: 0x00020459
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x00022266 File Offset: 0x00020466
		public override int AttributeCount
		{
			get
			{
				if (this.currentBufferedNodeToReport == null)
				{
					return this.reader.AttributeCount;
				}
				if (this.currentBufferedNodeToReport.Value.AttributeNodes == null)
				{
					return 0;
				}
				return this.currentBufferedNodeToReport.Value.AttributeNodes.Count;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0000360D File Offset: 0x0000180D
		public override string BaseURI
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x000222A8 File Offset: 0x000204A8
		public override bool HasValue
		{
			get
			{
				if (this.currentBufferedNodeToReport != null)
				{
					switch (this.NodeType)
					{
					case XmlNodeType.Attribute:
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.Comment:
					case XmlNodeType.DocumentType:
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
					case XmlNodeType.XmlDeclaration:
						return true;
					}
					return false;
				}
				return this.reader.HasValue;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0002231D File Offset: 0x0002051D
		int IXmlLineInfo.LineNumber
		{
			get
			{
				if (!this.HasLineInfo())
				{
					return 0;
				}
				return this.lineInfo.LineNumber;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x00022334 File Offset: 0x00020534
		int IXmlLineInfo.LinePosition
		{
			get
			{
				if (!this.HasLineInfo())
				{
					return 0;
				}
				return this.lineInfo.LinePosition;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0002234B File Offset: 0x0002054B
		internal Uri XmlBaseUri
		{
			get
			{
				if (this.xmlBaseStack.Count <= 0)
				{
					return null;
				}
				return this.xmlBaseStack.Peek().BaseUri;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x00022370 File Offset: 0x00020570
		internal Uri ParentXmlBaseUri
		{
			get
			{
				if (this.xmlBaseStack.Count == 0)
				{
					return null;
				}
				BufferingXmlReader.XmlBaseDefinition xmlBaseDefinition = this.xmlBaseStack.Peek();
				if (xmlBaseDefinition.Depth == this.Depth)
				{
					if (this.xmlBaseStack.Count == 1)
					{
						return null;
					}
					xmlBaseDefinition = this.xmlBaseStack.Skip(1).First<BufferingXmlReader.XmlBaseDefinition>();
				}
				return xmlBaseDefinition.BaseUri;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x000223CE File Offset: 0x000205CE
		// (set) Token: 0x06000E73 RID: 3699 RVA: 0x000223D6 File Offset: 0x000205D6
		internal bool DisableInStreamErrorDetection
		{
			get
			{
				return this.disableInStreamErrorDetection;
			}
			set
			{
				this.disableInStreamErrorDetection = value;
			}
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x000223E0 File Offset: 0x000205E0
		public override bool Read()
		{
			if (!this.disableXmlBase && this.xmlBaseStack.Count > 0)
			{
				XmlNodeType xmlNodeType = this.NodeType;
				if (xmlNodeType == XmlNodeType.Attribute)
				{
					this.MoveToElement();
					xmlNodeType = XmlNodeType.Element;
				}
				if (this.xmlBaseStack.Peek().Depth == this.Depth && (xmlNodeType == XmlNodeType.EndElement || (xmlNodeType == XmlNodeType.Element && this.IsEmptyElement)))
				{
					this.xmlBaseStack.Pop();
				}
			}
			bool flag = this.ReadInternal(this.disableInStreamErrorDetection);
			if (flag && !this.disableXmlBase && this.NodeType == XmlNodeType.Element)
			{
				string attributeWithAtomizedName = this.GetAttributeWithAtomizedName(this.XmlBaseAttributeName, this.XmlNamespace);
				if (attributeWithAtomizedName != null)
				{
					Uri uri = new Uri(attributeWithAtomizedName, UriKind.RelativeOrAbsolute);
					if (!uri.IsAbsoluteUri)
					{
						if (this.xmlBaseStack.Count == 0)
						{
							if (this.documentBaseUri == null)
							{
								throw new ODataException(Strings.ODataAtomDeserializer_RelativeUriUsedWithoutBaseUriSpecified(attributeWithAtomizedName));
							}
							uri = UriUtils.UriToAbsoluteUri(this.documentBaseUri, uri);
						}
						else
						{
							uri = UriUtils.UriToAbsoluteUri(this.xmlBaseStack.Peek().BaseUri, uri);
						}
					}
					this.xmlBaseStack.Push(new BufferingXmlReader.XmlBaseDefinition(uri, this.Depth));
				}
			}
			return flag;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00022504 File Offset: 0x00020704
		public override bool MoveToElement()
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.MoveToElement();
			}
			this.MoveFromAttributeValueNode();
			if (this.isBuffering)
			{
				if (this.currentBufferedNodeToReport.Value.NodeType == XmlNodeType.Attribute)
				{
					this.currentBufferedNodeToReport = this.currentBufferedNode;
					return true;
				}
				return false;
			}
			else
			{
				if (this.currentBufferedNodeToReport.Value.NodeType == XmlNodeType.Attribute)
				{
					this.currentBufferedNodeToReport = this.bufferedNodes.First;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00022584 File Offset: 0x00020784
		public override bool MoveToFirstAttribute()
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.MoveToFirstAttribute();
			}
			BufferingXmlReader.BufferedNode currentElementNode = this.GetCurrentElementNode();
			if (currentElementNode.NodeType == XmlNodeType.Element && currentElementNode.AttributeNodes.Count > 0)
			{
				this.currentAttributeNode = null;
				this.currentBufferedNodeToReport = currentElementNode.AttributeNodes.First;
				return true;
			}
			return false;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x000225E4 File Offset: 0x000207E4
		public override bool MoveToNextAttribute()
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.MoveToNextAttribute();
			}
			LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = this.currentAttributeNode;
			if (linkedListNode == null)
			{
				linkedListNode = this.currentBufferedNodeToReport;
			}
			if (linkedListNode.Value.NodeType == XmlNodeType.Attribute)
			{
				if (linkedListNode.Next != null)
				{
					this.currentAttributeNode = null;
					this.currentBufferedNodeToReport = linkedListNode.Next;
					return true;
				}
				return false;
			}
			else
			{
				if (this.currentBufferedNodeToReport.Value.NodeType != XmlNodeType.Element)
				{
					return false;
				}
				if (this.currentBufferedNodeToReport.Value.AttributeNodes.Count > 0)
				{
					this.currentBufferedNodeToReport = this.currentBufferedNodeToReport.Value.AttributeNodes.First;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00022698 File Offset: 0x00020898
		public override bool ReadAttributeValue()
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.ReadAttributeValue();
			}
			if (this.currentBufferedNodeToReport.Value.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			if (this.currentAttributeNode != null)
			{
				return false;
			}
			BufferingXmlReader.BufferedNode bufferedNode = new BufferingXmlReader.BufferedNode(this.currentBufferedNodeToReport.Value.Value, this.currentBufferedNodeToReport.Value.Depth, this.NameTable);
			LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = new LinkedListNode<BufferingXmlReader.BufferedNode>(bufferedNode);
			this.currentAttributeNode = this.currentBufferedNodeToReport;
			this.currentBufferedNodeToReport = linkedListNode;
			return true;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00022728 File Offset: 0x00020928
		public override string GetAttribute(int i)
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.GetAttribute(i);
			}
			if (i < 0 || i >= this.AttributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = this.FindAttributeBufferedNode(i);
			if (linkedListNode != null)
			{
				return linkedListNode.Value.Value;
			}
			return null;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00022780 File Offset: 0x00020980
		public override string GetAttribute(string name, string namespaceURI)
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.GetAttribute(name, namespaceURI);
			}
			LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = this.FindAttributeBufferedNode(name, namespaceURI);
			if (linkedListNode != null)
			{
				return linkedListNode.Value.Value;
			}
			return null;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x000227C4 File Offset: 0x000209C4
		public override string GetAttribute(string name)
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.GetAttribute(name);
			}
			LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = this.FindAttributeBufferedNode(name);
			if (linkedListNode != null)
			{
				return linkedListNode.Value.Value;
			}
			return null;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00002396 File Offset: 0x00000596
		public override string LookupNamespace(string prefix)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00022804 File Offset: 0x00020A04
		public override bool MoveToAttribute(string name, string ns)
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.MoveToAttribute(name, ns);
			}
			LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = this.FindAttributeBufferedNode(name, ns);
			if (linkedListNode != null)
			{
				this.currentAttributeNode = null;
				this.currentBufferedNodeToReport = linkedListNode;
				return true;
			}
			return false;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0002284C File Offset: 0x00020A4C
		public override bool MoveToAttribute(string name)
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.MoveToAttribute(name);
			}
			LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = this.FindAttributeBufferedNode(name);
			if (linkedListNode != null)
			{
				this.currentAttributeNode = null;
				this.currentBufferedNodeToReport = linkedListNode;
				return true;
			}
			return false;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00022890 File Offset: 0x00020A90
		public override void ResolveEntity()
		{
			throw new InvalidOperationException(Strings.ODataException_GeneralError);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0002289C File Offset: 0x00020A9C
		bool IXmlLineInfo.HasLineInfo()
		{
			return this.HasLineInfo();
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x000228A4 File Offset: 0x00020AA4
		internal void StartBuffering()
		{
			if (this.bufferedNodes.Count == 0)
			{
				this.bufferedNodes.AddFirst(this.BufferCurrentReaderNode());
			}
			else
			{
				this.removeOnNextRead = false;
			}
			this.currentBufferedNode = this.bufferedNodes.First;
			this.currentBufferedNodeToReport = this.currentBufferedNode;
			int count = this.xmlBaseStack.Count;
			if (count != 0)
			{
				if (count != 1)
				{
					BufferingXmlReader.XmlBaseDefinition[] array = this.xmlBaseStack.ToArray();
					this.bufferStartXmlBaseStack = new Stack<BufferingXmlReader.XmlBaseDefinition>(count);
					for (int i = count - 1; i >= 0; i--)
					{
						this.bufferStartXmlBaseStack.Push(array[i]);
					}
				}
				else
				{
					this.bufferStartXmlBaseStack = new Stack<BufferingXmlReader.XmlBaseDefinition>();
					this.bufferStartXmlBaseStack.Push(this.xmlBaseStack.Peek());
				}
			}
			else
			{
				this.bufferStartXmlBaseStack = new Stack<BufferingXmlReader.XmlBaseDefinition>();
			}
			this.isBuffering = true;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00022978 File Offset: 0x00020B78
		internal void StopBuffering()
		{
			this.isBuffering = false;
			this.removeOnNextRead = true;
			this.currentBufferedNode = null;
			if (this.bufferedNodes.Count > 0)
			{
				this.currentBufferedNodeToReport = this.bufferedNodes.First;
			}
			this.xmlBaseStack = this.bufferStartXmlBaseStack;
			this.bufferStartXmlBaseStack = null;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x000229CC File Offset: 0x00020BCC
		private bool ReadInternal(bool ignoreInStreamErrors)
		{
			if (this.removeOnNextRead)
			{
				this.currentBufferedNodeToReport = this.currentBufferedNodeToReport.Next;
				this.bufferedNodes.RemoveFirst();
				this.removeOnNextRead = false;
			}
			bool flag;
			if (this.isBuffering)
			{
				this.MoveFromAttributeValueNode();
				if (this.currentBufferedNode.Next != null)
				{
					this.currentBufferedNode = this.currentBufferedNode.Next;
					this.currentBufferedNodeToReport = this.currentBufferedNode;
					flag = true;
				}
				else if (ignoreInStreamErrors)
				{
					flag = this.reader.Read();
					this.bufferedNodes.AddLast(this.BufferCurrentReaderNode());
					this.currentBufferedNode = this.bufferedNodes.Last;
					this.currentBufferedNodeToReport = this.currentBufferedNode;
				}
				else
				{
					flag = this.ReadNextAndCheckForInStreamError();
				}
			}
			else if (this.bufferedNodes.Count == 0)
			{
				flag = (ignoreInStreamErrors ? this.reader.Read() : this.ReadNextAndCheckForInStreamError());
			}
			else
			{
				this.currentBufferedNodeToReport = this.bufferedNodes.First;
				BufferingXmlReader.BufferedNode value = this.currentBufferedNodeToReport.Value;
				flag = !this.IsEndOfInputNode(value);
				this.removeOnNextRead = true;
			}
			return flag;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00022AE8 File Offset: 0x00020CE8
		private bool ReadNextAndCheckForInStreamError()
		{
			bool flag = this.ReadInternal(true);
			if (!this.disableInStreamErrorDetection && this.NodeType == XmlNodeType.Element && this.LocalNameEquals(this.ODataErrorElementName) && this.NamespaceEquals(this.ODataMetadataNamespace))
			{
				ODataError odataError = ODataAtomErrorDeserializer.ReadErrorElement(this, this.maxInnerErrorDepth);
				throw new ODataErrorException(odataError);
			}
			return flag;
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00022B3F File Offset: 0x00020D3F
		private bool IsEndOfInputNode(BufferingXmlReader.BufferedNode node)
		{
			return node == this.endOfInputBufferedNode;
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00022B4C File Offset: 0x00020D4C
		private BufferingXmlReader.BufferedNode BufferCurrentReaderNode()
		{
			if (this.reader.EOF)
			{
				return this.endOfInputBufferedNode;
			}
			BufferingXmlReader.BufferedNode bufferedNode = new BufferingXmlReader.BufferedNode(this.reader);
			if (this.reader.NodeType == XmlNodeType.Element)
			{
				while (this.reader.MoveToNextAttribute())
				{
					bufferedNode.AttributeNodes.AddLast(new BufferingXmlReader.BufferedNode(this.reader));
				}
				this.reader.MoveToElement();
			}
			return bufferedNode;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00022BBA File Offset: 0x00020DBA
		private BufferingXmlReader.BufferedNode GetCurrentElementNode()
		{
			if (this.isBuffering)
			{
				return this.currentBufferedNode.Value;
			}
			return this.bufferedNodes.First.Value;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00022BE0 File Offset: 0x00020DE0
		private LinkedListNode<BufferingXmlReader.BufferedNode> FindAttributeBufferedNode(int index)
		{
			BufferingXmlReader.BufferedNode currentElementNode = this.GetCurrentElementNode();
			if (currentElementNode.NodeType == XmlNodeType.Element && currentElementNode.AttributeNodes.Count > 0)
			{
				LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = currentElementNode.AttributeNodes.First;
				int num = 0;
				while (linkedListNode != null)
				{
					if (num == index)
					{
						return linkedListNode;
					}
					num++;
					linkedListNode = linkedListNode.Next;
				}
			}
			return null;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00022C30 File Offset: 0x00020E30
		private LinkedListNode<BufferingXmlReader.BufferedNode> FindAttributeBufferedNode(string localName, string namespaceUri)
		{
			BufferingXmlReader.BufferedNode currentElementNode = this.GetCurrentElementNode();
			if (currentElementNode.NodeType == XmlNodeType.Element && currentElementNode.AttributeNodes.Count > 0)
			{
				for (LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = currentElementNode.AttributeNodes.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					BufferingXmlReader.BufferedNode value = linkedListNode.Value;
					if (string.CompareOrdinal(value.NamespaceUri, namespaceUri) == 0 && string.CompareOrdinal(value.LocalName, localName) == 0)
					{
						return linkedListNode;
					}
				}
			}
			return null;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00022C9C File Offset: 0x00020E9C
		private LinkedListNode<BufferingXmlReader.BufferedNode> FindAttributeBufferedNode(string qualifiedName)
		{
			BufferingXmlReader.BufferedNode currentElementNode = this.GetCurrentElementNode();
			if (currentElementNode.NodeType == XmlNodeType.Element && currentElementNode.AttributeNodes.Count > 0)
			{
				for (LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = currentElementNode.AttributeNodes.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					BufferingXmlReader.BufferedNode value = linkedListNode.Value;
					bool flag = !string.IsNullOrEmpty(value.Prefix);
					if ((!flag && string.CompareOrdinal(value.LocalName, qualifiedName) == 0) || (flag && string.CompareOrdinal(value.Prefix + ":" + value.LocalName, qualifiedName) == 0))
					{
						return linkedListNode;
					}
				}
			}
			return null;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00022D2A File Offset: 0x00020F2A
		private void MoveFromAttributeValueNode()
		{
			if (this.currentAttributeNode != null)
			{
				this.currentBufferedNodeToReport = this.currentAttributeNode;
				this.currentAttributeNode = null;
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00022D48 File Offset: 0x00020F48
		private string GetAttributeWithAtomizedName(string name, string namespaceURI)
		{
			if (this.bufferedNodes.Count > 0)
			{
				for (LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = this.GetCurrentElementNode().AttributeNodes.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					BufferingXmlReader.BufferedNode value = linkedListNode.Value;
					if (namespaceURI == value.NamespaceUri && name == value.LocalName)
					{
						return linkedListNode.Value.Value;
					}
				}
				return null;
			}
			string text = null;
			while (this.reader.MoveToNextAttribute())
			{
				if (name == this.reader.LocalName && namespaceURI == this.reader.NamespaceURI)
				{
					text = this.reader.Value;
					break;
				}
			}
			this.reader.MoveToElement();
			return text;
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0000239D File Offset: 0x0000059D
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This is a DEBUG only method.")]
		[Conditional("DEBUG")]
		private void ValidateInternalState()
		{
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00022DEF File Offset: 0x00020FEF
		private bool HasLineInfo()
		{
			return this.lineInfo != null && this.lineInfo.HasLineInfo();
		}

		// Token: 0x0400071A RID: 1818
		internal readonly string XmlNamespace;

		// Token: 0x0400071B RID: 1819
		internal readonly string XmlBaseAttributeName;

		// Token: 0x0400071C RID: 1820
		internal readonly string ODataMetadataNamespace;

		// Token: 0x0400071D RID: 1821
		internal readonly string ODataNamespace;

		// Token: 0x0400071E RID: 1822
		internal readonly string ODataErrorElementName;

		// Token: 0x0400071F RID: 1823
		private readonly IXmlLineInfo lineInfo;

		// Token: 0x04000720 RID: 1824
		private readonly XmlReader reader;

		// Token: 0x04000721 RID: 1825
		private readonly LinkedList<BufferingXmlReader.BufferedNode> bufferedNodes;

		// Token: 0x04000722 RID: 1826
		private readonly BufferingXmlReader.BufferedNode endOfInputBufferedNode;

		// Token: 0x04000723 RID: 1827
		private readonly bool disableXmlBase;

		// Token: 0x04000724 RID: 1828
		private readonly int maxInnerErrorDepth;

		// Token: 0x04000725 RID: 1829
		private readonly Uri documentBaseUri;

		// Token: 0x04000726 RID: 1830
		private LinkedListNode<BufferingXmlReader.BufferedNode> currentBufferedNode;

		// Token: 0x04000727 RID: 1831
		private LinkedListNode<BufferingXmlReader.BufferedNode> currentAttributeNode;

		// Token: 0x04000728 RID: 1832
		private LinkedListNode<BufferingXmlReader.BufferedNode> currentBufferedNodeToReport;

		// Token: 0x04000729 RID: 1833
		private bool isBuffering;

		// Token: 0x0400072A RID: 1834
		private bool removeOnNextRead;

		// Token: 0x0400072B RID: 1835
		private bool disableInStreamErrorDetection;

		// Token: 0x0400072C RID: 1836
		private Stack<BufferingXmlReader.XmlBaseDefinition> xmlBaseStack;

		// Token: 0x0400072D RID: 1837
		private Stack<BufferingXmlReader.XmlBaseDefinition> bufferStartXmlBaseStack;

		// Token: 0x02000363 RID: 867
		private sealed class BufferedNode
		{
			// Token: 0x06001EC6 RID: 7878 RVA: 0x0005986C File Offset: 0x00057A6C
			internal BufferedNode(XmlReader reader)
			{
				this.NodeType = reader.NodeType;
				this.NamespaceUri = reader.NamespaceURI;
				this.LocalName = reader.LocalName;
				this.Prefix = reader.Prefix;
				this.Value = reader.Value;
				this.Depth = reader.Depth;
				this.IsEmptyElement = reader.IsEmptyElement;
			}

			// Token: 0x06001EC7 RID: 7879 RVA: 0x000598D4 File Offset: 0x00057AD4
			internal BufferedNode(string value, int depth, XmlNameTable nametable)
			{
				string text = nametable.Add(string.Empty);
				this.NodeType = XmlNodeType.Text;
				this.NamespaceUri = text;
				this.LocalName = text;
				this.Prefix = text;
				this.Value = value;
				this.Depth = depth + 1;
				this.IsEmptyElement = false;
			}

			// Token: 0x06001EC8 RID: 7880 RVA: 0x00059926 File Offset: 0x00057B26
			private BufferedNode(string emptyString)
			{
				this.NodeType = XmlNodeType.None;
				this.NamespaceUri = emptyString;
				this.LocalName = emptyString;
				this.Prefix = emptyString;
				this.Value = emptyString;
			}

			// Token: 0x1700061F RID: 1567
			// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x00059951 File Offset: 0x00057B51
			// (set) Token: 0x06001ECA RID: 7882 RVA: 0x00059959 File Offset: 0x00057B59
			internal XmlNodeType NodeType { get; private set; }

			// Token: 0x17000620 RID: 1568
			// (get) Token: 0x06001ECB RID: 7883 RVA: 0x00059962 File Offset: 0x00057B62
			// (set) Token: 0x06001ECC RID: 7884 RVA: 0x0005996A File Offset: 0x00057B6A
			internal string NamespaceUri { get; private set; }

			// Token: 0x17000621 RID: 1569
			// (get) Token: 0x06001ECD RID: 7885 RVA: 0x00059973 File Offset: 0x00057B73
			// (set) Token: 0x06001ECE RID: 7886 RVA: 0x0005997B File Offset: 0x00057B7B
			internal string LocalName { get; private set; }

			// Token: 0x17000622 RID: 1570
			// (get) Token: 0x06001ECF RID: 7887 RVA: 0x00059984 File Offset: 0x00057B84
			// (set) Token: 0x06001ED0 RID: 7888 RVA: 0x0005998C File Offset: 0x00057B8C
			internal string Prefix { get; private set; }

			// Token: 0x17000623 RID: 1571
			// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x00059995 File Offset: 0x00057B95
			// (set) Token: 0x06001ED2 RID: 7890 RVA: 0x0005999D File Offset: 0x00057B9D
			internal string Value { get; private set; }

			// Token: 0x17000624 RID: 1572
			// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x000599A6 File Offset: 0x00057BA6
			// (set) Token: 0x06001ED4 RID: 7892 RVA: 0x000599AE File Offset: 0x00057BAE
			internal int Depth { get; private set; }

			// Token: 0x17000625 RID: 1573
			// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x000599B7 File Offset: 0x00057BB7
			// (set) Token: 0x06001ED6 RID: 7894 RVA: 0x000599BF File Offset: 0x00057BBF
			internal bool IsEmptyElement { get; private set; }

			// Token: 0x17000626 RID: 1574
			// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x000599C8 File Offset: 0x00057BC8
			internal LinkedList<BufferingXmlReader.BufferedNode> AttributeNodes
			{
				get
				{
					if (this.NodeType == XmlNodeType.Element && this.attributeNodes == null)
					{
						this.attributeNodes = new LinkedList<BufferingXmlReader.BufferedNode>();
					}
					return this.attributeNodes;
				}
			}

			// Token: 0x06001ED8 RID: 7896 RVA: 0x000599EC File Offset: 0x00057BEC
			internal static BufferingXmlReader.BufferedNode CreateEndOfInput(XmlNameTable nametable)
			{
				string text = nametable.Add(string.Empty);
				return new BufferingXmlReader.BufferedNode(text);
			}

			// Token: 0x04000E23 RID: 3619
			private LinkedList<BufferingXmlReader.BufferedNode> attributeNodes;
		}

		// Token: 0x02000364 RID: 868
		private sealed class XmlBaseDefinition
		{
			// Token: 0x06001ED9 RID: 7897 RVA: 0x00059A0B File Offset: 0x00057C0B
			internal XmlBaseDefinition(Uri baseUri, int depth)
			{
				this.BaseUri = baseUri;
				this.Depth = depth;
			}

			// Token: 0x17000627 RID: 1575
			// (get) Token: 0x06001EDA RID: 7898 RVA: 0x00059A21 File Offset: 0x00057C21
			// (set) Token: 0x06001EDB RID: 7899 RVA: 0x00059A29 File Offset: 0x00057C29
			internal Uri BaseUri { get; private set; }

			// Token: 0x17000628 RID: 1576
			// (get) Token: 0x06001EDC RID: 7900 RVA: 0x00059A32 File Offset: 0x00057C32
			// (set) Token: 0x06001EDD RID: 7901 RVA: 0x00059A3A File Offset: 0x00057C3A
			internal int Depth { get; private set; }
		}
	}
}
