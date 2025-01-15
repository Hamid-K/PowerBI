using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;

namespace Microsoft.OData.Metadata
{
	// Token: 0x020001D0 RID: 464
	internal sealed class BufferingXmlReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x060011FC RID: 4604 RVA: 0x00032158 File Offset: 0x00030358
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

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x00032244 File Offset: 0x00030444
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

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0003226A File Offset: 0x0003046A
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

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x00032290 File Offset: 0x00030490
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

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x000322B6 File Offset: 0x000304B6
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

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x000322DC File Offset: 0x000304DC
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

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x00032302 File Offset: 0x00030502
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

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x00032328 File Offset: 0x00030528
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

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0003234E File Offset: 0x0003054E
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

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x00032375 File Offset: 0x00030575
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
					return 3;
				}
				if (this.currentBufferedNodeToReport.Value.NodeType != null)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x000323B5 File Offset: 0x000305B5
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x000323C2 File Offset: 0x000305C2
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

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x0000B41B File Offset: 0x0000961B
		public override string BaseURI
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x00032404 File Offset: 0x00030604
		public override bool HasValue
		{
			get
			{
				if (this.currentBufferedNodeToReport != null)
				{
					switch (this.NodeType)
					{
					case 2:
					case 3:
					case 4:
					case 7:
					case 8:
					case 10:
					case 13:
					case 14:
					case 17:
						return true;
					}
					return false;
				}
				return this.reader.HasValue;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x00032479 File Offset: 0x00030679
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

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00032490 File Offset: 0x00030690
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

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x000324A7 File Offset: 0x000306A7
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

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x000324CC File Offset: 0x000306CC
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
					xmlBaseDefinition = Enumerable.First<BufferingXmlReader.XmlBaseDefinition>(Enumerable.Skip<BufferingXmlReader.XmlBaseDefinition>(this.xmlBaseStack, 1));
				}
				return xmlBaseDefinition.BaseUri;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0003252A File Offset: 0x0003072A
		// (set) Token: 0x0600120F RID: 4623 RVA: 0x00032532 File Offset: 0x00030732
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

		// Token: 0x06001210 RID: 4624 RVA: 0x0003253C File Offset: 0x0003073C
		public override bool Read()
		{
			if (!this.disableXmlBase && this.xmlBaseStack.Count > 0)
			{
				XmlNodeType xmlNodeType = this.NodeType;
				if (xmlNodeType == 2)
				{
					this.MoveToElement();
					xmlNodeType = 1;
				}
				if (this.xmlBaseStack.Peek().Depth == this.Depth && (xmlNodeType == 15 || (xmlNodeType == 1 && this.IsEmptyElement)))
				{
					this.xmlBaseStack.Pop();
				}
			}
			bool flag = this.ReadInternal(this.disableInStreamErrorDetection);
			if (flag && !this.disableXmlBase && this.NodeType == 1)
			{
				string attributeWithAtomizedName = this.GetAttributeWithAtomizedName(this.XmlBaseAttributeName, this.XmlNamespace);
				if (attributeWithAtomizedName != null)
				{
					Uri uri = new Uri(attributeWithAtomizedName, 0);
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

		// Token: 0x06001211 RID: 4625 RVA: 0x00032660 File Offset: 0x00030860
		public override bool MoveToElement()
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.MoveToElement();
			}
			this.MoveFromAttributeValueNode();
			if (this.isBuffering)
			{
				if (this.currentBufferedNodeToReport.Value.NodeType == 2)
				{
					this.currentBufferedNodeToReport = this.currentBufferedNode;
					return true;
				}
				return false;
			}
			else
			{
				if (this.currentBufferedNodeToReport.Value.NodeType == 2)
				{
					this.currentBufferedNodeToReport = this.bufferedNodes.First;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x000326E0 File Offset: 0x000308E0
		public override bool MoveToFirstAttribute()
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.MoveToFirstAttribute();
			}
			BufferingXmlReader.BufferedNode currentElementNode = this.GetCurrentElementNode();
			if (currentElementNode.NodeType == 1 && currentElementNode.AttributeNodes.Count > 0)
			{
				this.currentAttributeNode = null;
				this.currentBufferedNodeToReport = currentElementNode.AttributeNodes.First;
				return true;
			}
			return false;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00032740 File Offset: 0x00030940
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
			if (linkedListNode.Value.NodeType == 2)
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
				if (this.currentBufferedNodeToReport.Value.NodeType != 1)
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

		// Token: 0x06001214 RID: 4628 RVA: 0x000327F4 File Offset: 0x000309F4
		public override bool ReadAttributeValue()
		{
			if (this.bufferedNodes.Count <= 0)
			{
				return this.reader.ReadAttributeValue();
			}
			if (this.currentBufferedNodeToReport.Value.NodeType != 2)
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

		// Token: 0x06001215 RID: 4629 RVA: 0x00002506 File Offset: 0x00000706
		public override void Close()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00032884 File Offset: 0x00030A84
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

		// Token: 0x06001217 RID: 4631 RVA: 0x000328DC File Offset: 0x00030ADC
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

		// Token: 0x06001218 RID: 4632 RVA: 0x00032920 File Offset: 0x00030B20
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

		// Token: 0x06001219 RID: 4633 RVA: 0x00002506 File Offset: 0x00000706
		public override string LookupNamespace(string prefix)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00032960 File Offset: 0x00030B60
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

		// Token: 0x0600121B RID: 4635 RVA: 0x000329A8 File Offset: 0x00030BA8
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

		// Token: 0x0600121C RID: 4636 RVA: 0x000329EC File Offset: 0x00030BEC
		public override void ResolveEntity()
		{
			throw new InvalidOperationException(Strings.ODataException_GeneralError);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x000329F8 File Offset: 0x00030BF8
		bool IXmlLineInfo.HasLineInfo()
		{
			return this.HasLineInfo();
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00032A00 File Offset: 0x00030C00
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

		// Token: 0x0600121F RID: 4639 RVA: 0x00032AD4 File Offset: 0x00030CD4
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

		// Token: 0x06001220 RID: 4640 RVA: 0x00032B28 File Offset: 0x00030D28
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

		// Token: 0x06001221 RID: 4641 RVA: 0x00032C44 File Offset: 0x00030E44
		private bool ReadNextAndCheckForInStreamError()
		{
			bool flag = this.ReadInternal(true);
			if (!this.disableInStreamErrorDetection && this.NodeType == 1 && this.LocalNameEquals(this.ODataErrorElementName) && this.NamespaceEquals(this.ODataMetadataNamespace))
			{
				ODataError odataError = ODataAtomErrorDeserializer.ReadErrorElement(this, this.maxInnerErrorDepth);
				throw new ODataErrorException(odataError);
			}
			return flag;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00032C9B File Offset: 0x00030E9B
		private bool IsEndOfInputNode(BufferingXmlReader.BufferedNode node)
		{
			return node == this.endOfInputBufferedNode;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00032CA8 File Offset: 0x00030EA8
		private BufferingXmlReader.BufferedNode BufferCurrentReaderNode()
		{
			if (this.reader.EOF)
			{
				return this.endOfInputBufferedNode;
			}
			BufferingXmlReader.BufferedNode bufferedNode = new BufferingXmlReader.BufferedNode(this.reader);
			if (this.reader.NodeType == 1)
			{
				while (this.reader.MoveToNextAttribute())
				{
					bufferedNode.AttributeNodes.AddLast(new BufferingXmlReader.BufferedNode(this.reader));
				}
				this.reader.MoveToElement();
			}
			return bufferedNode;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00032D16 File Offset: 0x00030F16
		private BufferingXmlReader.BufferedNode GetCurrentElementNode()
		{
			if (this.isBuffering)
			{
				return this.currentBufferedNode.Value;
			}
			return this.bufferedNodes.First.Value;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00032D3C File Offset: 0x00030F3C
		private LinkedListNode<BufferingXmlReader.BufferedNode> FindAttributeBufferedNode(int index)
		{
			BufferingXmlReader.BufferedNode currentElementNode = this.GetCurrentElementNode();
			if (currentElementNode.NodeType == 1 && currentElementNode.AttributeNodes.Count > 0)
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

		// Token: 0x06001226 RID: 4646 RVA: 0x00032D8C File Offset: 0x00030F8C
		private LinkedListNode<BufferingXmlReader.BufferedNode> FindAttributeBufferedNode(string localName, string namespaceUri)
		{
			BufferingXmlReader.BufferedNode currentElementNode = this.GetCurrentElementNode();
			if (currentElementNode.NodeType == 1 && currentElementNode.AttributeNodes.Count > 0)
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

		// Token: 0x06001227 RID: 4647 RVA: 0x00032DF8 File Offset: 0x00030FF8
		private LinkedListNode<BufferingXmlReader.BufferedNode> FindAttributeBufferedNode(string qualifiedName)
		{
			BufferingXmlReader.BufferedNode currentElementNode = this.GetCurrentElementNode();
			if (currentElementNode.NodeType == 1 && currentElementNode.AttributeNodes.Count > 0)
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

		// Token: 0x06001228 RID: 4648 RVA: 0x00032E86 File Offset: 0x00031086
		private void MoveFromAttributeValueNode()
		{
			if (this.currentAttributeNode != null)
			{
				this.currentBufferedNodeToReport = this.currentAttributeNode;
				this.currentAttributeNode = null;
			}
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00032EA4 File Offset: 0x000310A4
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

		// Token: 0x0600122A RID: 4650 RVA: 0x0000250D File Offset: 0x0000070D
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This is a DEBUG only method.")]
		[Conditional("DEBUG")]
		private void ValidateInternalState()
		{
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00032F4B File Offset: 0x0003114B
		private bool HasLineInfo()
		{
			return this.lineInfo != null && this.lineInfo.HasLineInfo();
		}

		// Token: 0x04000926 RID: 2342
		internal readonly string XmlNamespace;

		// Token: 0x04000927 RID: 2343
		internal readonly string XmlBaseAttributeName;

		// Token: 0x04000928 RID: 2344
		internal readonly string ODataMetadataNamespace;

		// Token: 0x04000929 RID: 2345
		internal readonly string ODataNamespace;

		// Token: 0x0400092A RID: 2346
		internal readonly string ODataErrorElementName;

		// Token: 0x0400092B RID: 2347
		private readonly IXmlLineInfo lineInfo;

		// Token: 0x0400092C RID: 2348
		private readonly XmlReader reader;

		// Token: 0x0400092D RID: 2349
		private readonly LinkedList<BufferingXmlReader.BufferedNode> bufferedNodes;

		// Token: 0x0400092E RID: 2350
		private readonly BufferingXmlReader.BufferedNode endOfInputBufferedNode;

		// Token: 0x0400092F RID: 2351
		private readonly bool disableXmlBase;

		// Token: 0x04000930 RID: 2352
		private readonly int maxInnerErrorDepth;

		// Token: 0x04000931 RID: 2353
		private readonly Uri documentBaseUri;

		// Token: 0x04000932 RID: 2354
		private LinkedListNode<BufferingXmlReader.BufferedNode> currentBufferedNode;

		// Token: 0x04000933 RID: 2355
		private LinkedListNode<BufferingXmlReader.BufferedNode> currentAttributeNode;

		// Token: 0x04000934 RID: 2356
		private LinkedListNode<BufferingXmlReader.BufferedNode> currentBufferedNodeToReport;

		// Token: 0x04000935 RID: 2357
		private bool isBuffering;

		// Token: 0x04000936 RID: 2358
		private bool removeOnNextRead;

		// Token: 0x04000937 RID: 2359
		private bool disableInStreamErrorDetection;

		// Token: 0x04000938 RID: 2360
		private Stack<BufferingXmlReader.XmlBaseDefinition> xmlBaseStack;

		// Token: 0x04000939 RID: 2361
		private Stack<BufferingXmlReader.XmlBaseDefinition> bufferStartXmlBaseStack;

		// Token: 0x02000300 RID: 768
		private sealed class BufferedNode
		{
			// Token: 0x060019B8 RID: 6584 RVA: 0x0004A208 File Offset: 0x00048408
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

			// Token: 0x060019B9 RID: 6585 RVA: 0x0004A270 File Offset: 0x00048470
			internal BufferedNode(string value, int depth, XmlNameTable nametable)
			{
				string text = nametable.Add(string.Empty);
				this.NodeType = 3;
				this.NamespaceUri = text;
				this.LocalName = text;
				this.Prefix = text;
				this.Value = value;
				this.Depth = depth + 1;
				this.IsEmptyElement = false;
			}

			// Token: 0x060019BA RID: 6586 RVA: 0x0004A2C2 File Offset: 0x000484C2
			private BufferedNode(string emptyString)
			{
				this.NodeType = 0;
				this.NamespaceUri = emptyString;
				this.LocalName = emptyString;
				this.Prefix = emptyString;
				this.Value = emptyString;
			}

			// Token: 0x17000586 RID: 1414
			// (get) Token: 0x060019BB RID: 6587 RVA: 0x0004A2ED File Offset: 0x000484ED
			// (set) Token: 0x060019BC RID: 6588 RVA: 0x0004A2F5 File Offset: 0x000484F5
			internal XmlNodeType NodeType { get; private set; }

			// Token: 0x17000587 RID: 1415
			// (get) Token: 0x060019BD RID: 6589 RVA: 0x0004A2FE File Offset: 0x000484FE
			// (set) Token: 0x060019BE RID: 6590 RVA: 0x0004A306 File Offset: 0x00048506
			internal string NamespaceUri { get; private set; }

			// Token: 0x17000588 RID: 1416
			// (get) Token: 0x060019BF RID: 6591 RVA: 0x0004A30F File Offset: 0x0004850F
			// (set) Token: 0x060019C0 RID: 6592 RVA: 0x0004A317 File Offset: 0x00048517
			internal string LocalName { get; private set; }

			// Token: 0x17000589 RID: 1417
			// (get) Token: 0x060019C1 RID: 6593 RVA: 0x0004A320 File Offset: 0x00048520
			// (set) Token: 0x060019C2 RID: 6594 RVA: 0x0004A328 File Offset: 0x00048528
			internal string Prefix { get; private set; }

			// Token: 0x1700058A RID: 1418
			// (get) Token: 0x060019C3 RID: 6595 RVA: 0x0004A331 File Offset: 0x00048531
			// (set) Token: 0x060019C4 RID: 6596 RVA: 0x0004A339 File Offset: 0x00048539
			internal string Value { get; private set; }

			// Token: 0x1700058B RID: 1419
			// (get) Token: 0x060019C5 RID: 6597 RVA: 0x0004A342 File Offset: 0x00048542
			// (set) Token: 0x060019C6 RID: 6598 RVA: 0x0004A34A File Offset: 0x0004854A
			internal int Depth { get; private set; }

			// Token: 0x1700058C RID: 1420
			// (get) Token: 0x060019C7 RID: 6599 RVA: 0x0004A353 File Offset: 0x00048553
			// (set) Token: 0x060019C8 RID: 6600 RVA: 0x0004A35B File Offset: 0x0004855B
			internal bool IsEmptyElement { get; private set; }

			// Token: 0x1700058D RID: 1421
			// (get) Token: 0x060019C9 RID: 6601 RVA: 0x0004A364 File Offset: 0x00048564
			internal LinkedList<BufferingXmlReader.BufferedNode> AttributeNodes
			{
				get
				{
					if (this.NodeType == 1 && this.attributeNodes == null)
					{
						this.attributeNodes = new LinkedList<BufferingXmlReader.BufferedNode>();
					}
					return this.attributeNodes;
				}
			}

			// Token: 0x060019CA RID: 6602 RVA: 0x0004A388 File Offset: 0x00048588
			internal static BufferingXmlReader.BufferedNode CreateEndOfInput(XmlNameTable nametable)
			{
				string text = nametable.Add(string.Empty);
				return new BufferingXmlReader.BufferedNode(text);
			}

			// Token: 0x04000C80 RID: 3200
			private LinkedList<BufferingXmlReader.BufferedNode> attributeNodes;
		}

		// Token: 0x02000301 RID: 769
		private sealed class XmlBaseDefinition
		{
			// Token: 0x060019CB RID: 6603 RVA: 0x0004A3A7 File Offset: 0x000485A7
			internal XmlBaseDefinition(Uri baseUri, int depth)
			{
				this.BaseUri = baseUri;
				this.Depth = depth;
			}

			// Token: 0x1700058E RID: 1422
			// (get) Token: 0x060019CC RID: 6604 RVA: 0x0004A3BD File Offset: 0x000485BD
			// (set) Token: 0x060019CD RID: 6605 RVA: 0x0004A3C5 File Offset: 0x000485C5
			internal Uri BaseUri { get; private set; }

			// Token: 0x1700058F RID: 1423
			// (get) Token: 0x060019CE RID: 6606 RVA: 0x0004A3CE File Offset: 0x000485CE
			// (set) Token: 0x060019CF RID: 6607 RVA: 0x0004A3D6 File Offset: 0x000485D6
			internal int Depth { get; private set; }
		}
	}
}
