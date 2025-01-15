using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200001F RID: 31
	internal sealed class BufferingXmlReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00003CC4 File Offset: 0x00001EC4
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003DB0 File Offset: 0x00001FB0
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003DD6 File Offset: 0x00001FD6
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003DFC File Offset: 0x00001FFC
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003E22 File Offset: 0x00002022
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00003E48 File Offset: 0x00002048
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003E6E File Offset: 0x0000206E
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00003E94 File Offset: 0x00002094
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00003EBA File Offset: 0x000020BA
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

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00003EE1 File Offset: 0x000020E1
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00003F21 File Offset: 0x00002121
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00003F2E File Offset: 0x0000212E
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00003F6D File Offset: 0x0000216D
		public override string BaseURI
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003F70 File Offset: 0x00002170
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00003FE5 File Offset: 0x000021E5
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00003FFC File Offset: 0x000021FC
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

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004013 File Offset: 0x00002213
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004038 File Offset: 0x00002238
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004096 File Offset: 0x00002296
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000409E File Offset: 0x0000229E
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

		// Token: 0x060000FD RID: 253 RVA: 0x000040A8 File Offset: 0x000022A8
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

		// Token: 0x060000FE RID: 254 RVA: 0x000041CC File Offset: 0x000023CC
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

		// Token: 0x060000FF RID: 255 RVA: 0x0000424C File Offset: 0x0000244C
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

		// Token: 0x06000100 RID: 256 RVA: 0x000042AC File Offset: 0x000024AC
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

		// Token: 0x06000101 RID: 257 RVA: 0x00004360 File Offset: 0x00002560
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

		// Token: 0x06000102 RID: 258 RVA: 0x000043ED File Offset: 0x000025ED
		public override void Close()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000043F4 File Offset: 0x000025F4
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

		// Token: 0x06000104 RID: 260 RVA: 0x0000444C File Offset: 0x0000264C
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

		// Token: 0x06000105 RID: 261 RVA: 0x00004490 File Offset: 0x00002690
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

		// Token: 0x06000106 RID: 262 RVA: 0x000044D0 File Offset: 0x000026D0
		public override string LookupNamespace(string prefix)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000044D8 File Offset: 0x000026D8
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

		// Token: 0x06000108 RID: 264 RVA: 0x00004520 File Offset: 0x00002720
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

		// Token: 0x06000109 RID: 265 RVA: 0x00004564 File Offset: 0x00002764
		public override void ResolveEntity()
		{
			throw new InvalidOperationException(Strings.ODataException_GeneralError);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004570 File Offset: 0x00002770
		bool IXmlLineInfo.HasLineInfo()
		{
			return this.HasLineInfo();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004578 File Offset: 0x00002778
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
			switch (count)
			{
			case 0:
				this.bufferStartXmlBaseStack = new Stack<BufferingXmlReader.XmlBaseDefinition>();
				break;
			case 1:
				this.bufferStartXmlBaseStack = new Stack<BufferingXmlReader.XmlBaseDefinition>();
				this.bufferStartXmlBaseStack.Push(this.xmlBaseStack.Peek());
				break;
			default:
			{
				BufferingXmlReader.XmlBaseDefinition[] array = this.xmlBaseStack.ToArray();
				this.bufferStartXmlBaseStack = new Stack<BufferingXmlReader.XmlBaseDefinition>(count);
				for (int i = count - 1; i >= 0; i--)
				{
					this.bufferStartXmlBaseStack.Push(array[i]);
				}
				break;
			}
			}
			this.isBuffering = true;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004654 File Offset: 0x00002854
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

		// Token: 0x0600010D RID: 269 RVA: 0x000046A8 File Offset: 0x000028A8
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

		// Token: 0x0600010E RID: 270 RVA: 0x000047C4 File Offset: 0x000029C4
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

		// Token: 0x0600010F RID: 271 RVA: 0x0000481B File Offset: 0x00002A1B
		private bool IsEndOfInputNode(BufferingXmlReader.BufferedNode node)
		{
			return object.ReferenceEquals(node, this.endOfInputBufferedNode);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000482C File Offset: 0x00002A2C
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

		// Token: 0x06000111 RID: 273 RVA: 0x0000489A File Offset: 0x00002A9A
		private BufferingXmlReader.BufferedNode GetCurrentElementNode()
		{
			if (this.isBuffering)
			{
				return this.currentBufferedNode.Value;
			}
			return this.bufferedNodes.First.Value;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000048C0 File Offset: 0x00002AC0
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

		// Token: 0x06000113 RID: 275 RVA: 0x00004910 File Offset: 0x00002B10
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

		// Token: 0x06000114 RID: 276 RVA: 0x0000497C File Offset: 0x00002B7C
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

		// Token: 0x06000115 RID: 277 RVA: 0x00004A0A File Offset: 0x00002C0A
		private void MoveFromAttributeValueNode()
		{
			if (this.currentAttributeNode != null)
			{
				this.currentBufferedNodeToReport = this.currentAttributeNode;
				this.currentAttributeNode = null;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004A28 File Offset: 0x00002C28
		private string GetAttributeWithAtomizedName(string name, string namespaceURI)
		{
			if (this.bufferedNodes.Count > 0)
			{
				for (LinkedListNode<BufferingXmlReader.BufferedNode> linkedListNode = this.GetCurrentElementNode().AttributeNodes.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
				{
					BufferingXmlReader.BufferedNode value = linkedListNode.Value;
					if (object.ReferenceEquals(namespaceURI, value.NamespaceUri) && object.ReferenceEquals(name, value.LocalName))
					{
						return linkedListNode.Value.Value;
					}
				}
				return null;
			}
			string text = null;
			while (this.reader.MoveToNextAttribute())
			{
				if (object.ReferenceEquals(name, this.reader.LocalName) && object.ReferenceEquals(namespaceURI, this.reader.NamespaceURI))
				{
					text = this.reader.Value;
					break;
				}
			}
			this.reader.MoveToElement();
			return text;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004AE3 File Offset: 0x00002CE3
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This is a DEBUG only method.")]
		private void ValidateInternalState()
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004AE5 File Offset: 0x00002CE5
		private bool HasLineInfo()
		{
			return this.lineInfo != null && this.lineInfo.HasLineInfo();
		}

		// Token: 0x040000D6 RID: 214
		internal readonly string XmlNamespace;

		// Token: 0x040000D7 RID: 215
		internal readonly string XmlBaseAttributeName;

		// Token: 0x040000D8 RID: 216
		internal readonly string ODataMetadataNamespace;

		// Token: 0x040000D9 RID: 217
		internal readonly string ODataNamespace;

		// Token: 0x040000DA RID: 218
		internal readonly string ODataErrorElementName;

		// Token: 0x040000DB RID: 219
		private readonly IXmlLineInfo lineInfo;

		// Token: 0x040000DC RID: 220
		private readonly XmlReader reader;

		// Token: 0x040000DD RID: 221
		private readonly LinkedList<BufferingXmlReader.BufferedNode> bufferedNodes;

		// Token: 0x040000DE RID: 222
		private readonly BufferingXmlReader.BufferedNode endOfInputBufferedNode;

		// Token: 0x040000DF RID: 223
		private readonly bool disableXmlBase;

		// Token: 0x040000E0 RID: 224
		private readonly int maxInnerErrorDepth;

		// Token: 0x040000E1 RID: 225
		private readonly Uri documentBaseUri;

		// Token: 0x040000E2 RID: 226
		private LinkedListNode<BufferingXmlReader.BufferedNode> currentBufferedNode;

		// Token: 0x040000E3 RID: 227
		private LinkedListNode<BufferingXmlReader.BufferedNode> currentAttributeNode;

		// Token: 0x040000E4 RID: 228
		private LinkedListNode<BufferingXmlReader.BufferedNode> currentBufferedNodeToReport;

		// Token: 0x040000E5 RID: 229
		private bool isBuffering;

		// Token: 0x040000E6 RID: 230
		private bool removeOnNextRead;

		// Token: 0x040000E7 RID: 231
		private bool disableInStreamErrorDetection;

		// Token: 0x040000E8 RID: 232
		private Stack<BufferingXmlReader.XmlBaseDefinition> xmlBaseStack;

		// Token: 0x040000E9 RID: 233
		private Stack<BufferingXmlReader.XmlBaseDefinition> bufferStartXmlBaseStack;

		// Token: 0x02000020 RID: 32
		private sealed class BufferedNode
		{
			// Token: 0x06000119 RID: 281 RVA: 0x00004AFC File Offset: 0x00002CFC
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

			// Token: 0x0600011A RID: 282 RVA: 0x00004B64 File Offset: 0x00002D64
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

			// Token: 0x0600011B RID: 283 RVA: 0x00004BB6 File Offset: 0x00002DB6
			private BufferedNode(string emptyString)
			{
				this.NodeType = 0;
				this.NamespaceUri = emptyString;
				this.LocalName = emptyString;
				this.Prefix = emptyString;
				this.Value = emptyString;
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x0600011C RID: 284 RVA: 0x00004BE1 File Offset: 0x00002DE1
			// (set) Token: 0x0600011D RID: 285 RVA: 0x00004BE9 File Offset: 0x00002DE9
			internal XmlNodeType NodeType { get; private set; }

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x0600011E RID: 286 RVA: 0x00004BF2 File Offset: 0x00002DF2
			// (set) Token: 0x0600011F RID: 287 RVA: 0x00004BFA File Offset: 0x00002DFA
			internal string NamespaceUri { get; private set; }

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x06000120 RID: 288 RVA: 0x00004C03 File Offset: 0x00002E03
			// (set) Token: 0x06000121 RID: 289 RVA: 0x00004C0B File Offset: 0x00002E0B
			internal string LocalName { get; private set; }

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000122 RID: 290 RVA: 0x00004C14 File Offset: 0x00002E14
			// (set) Token: 0x06000123 RID: 291 RVA: 0x00004C1C File Offset: 0x00002E1C
			internal string Prefix { get; private set; }

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x06000124 RID: 292 RVA: 0x00004C25 File Offset: 0x00002E25
			// (set) Token: 0x06000125 RID: 293 RVA: 0x00004C2D File Offset: 0x00002E2D
			internal string Value { get; private set; }

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000126 RID: 294 RVA: 0x00004C36 File Offset: 0x00002E36
			// (set) Token: 0x06000127 RID: 295 RVA: 0x00004C3E File Offset: 0x00002E3E
			internal int Depth { get; private set; }

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000128 RID: 296 RVA: 0x00004C47 File Offset: 0x00002E47
			// (set) Token: 0x06000129 RID: 297 RVA: 0x00004C4F File Offset: 0x00002E4F
			internal bool IsEmptyElement { get; private set; }

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x0600012A RID: 298 RVA: 0x00004C58 File Offset: 0x00002E58
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

			// Token: 0x0600012B RID: 299 RVA: 0x00004C7C File Offset: 0x00002E7C
			internal static BufferingXmlReader.BufferedNode CreateEndOfInput(XmlNameTable nametable)
			{
				string text = nametable.Add(string.Empty);
				return new BufferingXmlReader.BufferedNode(text);
			}

			// Token: 0x040000EA RID: 234
			private LinkedList<BufferingXmlReader.BufferedNode> attributeNodes;
		}

		// Token: 0x02000021 RID: 33
		private sealed class XmlBaseDefinition
		{
			// Token: 0x0600012C RID: 300 RVA: 0x00004C9B File Offset: 0x00002E9B
			internal XmlBaseDefinition(Uri baseUri, int depth)
			{
				this.BaseUri = baseUri;
				this.Depth = depth;
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x0600012D RID: 301 RVA: 0x00004CB1 File Offset: 0x00002EB1
			// (set) Token: 0x0600012E RID: 302 RVA: 0x00004CB9 File Offset: 0x00002EB9
			internal Uri BaseUri { get; private set; }

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x0600012F RID: 303 RVA: 0x00004CC2 File Offset: 0x00002EC2
			// (set) Token: 0x06000130 RID: 304 RVA: 0x00004CCA File Offset: 0x00002ECA
			internal int Depth { get; private set; }
		}
	}
}
