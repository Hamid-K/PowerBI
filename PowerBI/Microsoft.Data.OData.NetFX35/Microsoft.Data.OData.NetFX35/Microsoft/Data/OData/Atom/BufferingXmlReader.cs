using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000223 RID: 547
	internal sealed class BufferingXmlReader : XmlReader
	{
		// Token: 0x06001023 RID: 4131 RVA: 0x0003CF20 File Offset: 0x0003B120
		internal BufferingXmlReader(XmlReader reader, Uri parentXmlBaseUri, Uri documentBaseUri, bool disableXmlBase, int maxInnerErrorDepth, string odataNamespace)
		{
			this.reader = reader;
			this.documentBaseUri = documentBaseUri;
			this.disableXmlBase = disableXmlBase;
			this.maxInnerErrorDepth = maxInnerErrorDepth;
			XmlNameTable nameTable = this.reader.NameTable;
			this.XmlNamespace = nameTable.Add("http://www.w3.org/XML/1998/namespace");
			this.XmlBaseAttributeName = nameTable.Add("base");
			this.XmlLangAttributeName = nameTable.Add("lang");
			this.ODataMetadataNamespace = nameTable.Add("http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			this.ODataNamespace = nameTable.Add(odataNamespace);
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

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0003D00E File Offset: 0x0003B20E
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

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0003D034 File Offset: 0x0003B234
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

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0003D05A File Offset: 0x0003B25A
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

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x0003D080 File Offset: 0x0003B280
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

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0003D0A6 File Offset: 0x0003B2A6
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

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x0003D0CC File Offset: 0x0003B2CC
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

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0003D0F2 File Offset: 0x0003B2F2
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

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0003D118 File Offset: 0x0003B318
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

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0003D13F File Offset: 0x0003B33F
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

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0003D17F File Offset: 0x0003B37F
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0003D18C File Offset: 0x0003B38C
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

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x0003D1CB File Offset: 0x0003B3CB
		public override string BaseURI
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0003D1D0 File Offset: 0x0003B3D0
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

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0003D245 File Offset: 0x0003B445
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

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0003D268 File Offset: 0x0003B468
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

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x0003D2C6 File Offset: 0x0003B4C6
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x0003D2CE File Offset: 0x0003B4CE
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

		// Token: 0x06001035 RID: 4149 RVA: 0x0003D2D8 File Offset: 0x0003B4D8
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

		// Token: 0x06001036 RID: 4150 RVA: 0x0003D3FC File Offset: 0x0003B5FC
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

		// Token: 0x06001037 RID: 4151 RVA: 0x0003D47C File Offset: 0x0003B67C
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

		// Token: 0x06001038 RID: 4152 RVA: 0x0003D4DC File Offset: 0x0003B6DC
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

		// Token: 0x06001039 RID: 4153 RVA: 0x0003D590 File Offset: 0x0003B790
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

		// Token: 0x0600103A RID: 4154 RVA: 0x0003D61D File Offset: 0x0003B81D
		public override void Close()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0003D624 File Offset: 0x0003B824
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

		// Token: 0x0600103C RID: 4156 RVA: 0x0003D67C File Offset: 0x0003B87C
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

		// Token: 0x0600103D RID: 4157 RVA: 0x0003D6C0 File Offset: 0x0003B8C0
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

		// Token: 0x0600103E RID: 4158 RVA: 0x0003D700 File Offset: 0x0003B900
		public override string LookupNamespace(string prefix)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0003D708 File Offset: 0x0003B908
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

		// Token: 0x06001040 RID: 4160 RVA: 0x0003D750 File Offset: 0x0003B950
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

		// Token: 0x06001041 RID: 4161 RVA: 0x0003D794 File Offset: 0x0003B994
		public override void ResolveEntity()
		{
			throw new InvalidOperationException(Strings.ODataException_GeneralError);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0003D7A0 File Offset: 0x0003B9A0
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

		// Token: 0x06001043 RID: 4163 RVA: 0x0003D87C File Offset: 0x0003BA7C
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

		// Token: 0x06001044 RID: 4164 RVA: 0x0003D8D0 File Offset: 0x0003BAD0
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

		// Token: 0x06001045 RID: 4165 RVA: 0x0003D9EC File Offset: 0x0003BBEC
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

		// Token: 0x06001046 RID: 4166 RVA: 0x0003DA43 File Offset: 0x0003BC43
		private bool IsEndOfInputNode(BufferingXmlReader.BufferedNode node)
		{
			return object.ReferenceEquals(node, this.endOfInputBufferedNode);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0003DA54 File Offset: 0x0003BC54
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

		// Token: 0x06001048 RID: 4168 RVA: 0x0003DAC2 File Offset: 0x0003BCC2
		private BufferingXmlReader.BufferedNode GetCurrentElementNode()
		{
			if (this.isBuffering)
			{
				return this.currentBufferedNode.Value;
			}
			return this.bufferedNodes.First.Value;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0003DAE8 File Offset: 0x0003BCE8
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

		// Token: 0x0600104A RID: 4170 RVA: 0x0003DB38 File Offset: 0x0003BD38
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

		// Token: 0x0600104B RID: 4171 RVA: 0x0003DBA4 File Offset: 0x0003BDA4
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

		// Token: 0x0600104C RID: 4172 RVA: 0x0003DC32 File Offset: 0x0003BE32
		private void MoveFromAttributeValueNode()
		{
			if (this.currentAttributeNode != null)
			{
				this.currentBufferedNodeToReport = this.currentAttributeNode;
				this.currentAttributeNode = null;
			}
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0003DC50 File Offset: 0x0003BE50
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

		// Token: 0x0600104E RID: 4174 RVA: 0x0003DD0B File Offset: 0x0003BF0B
		[Conditional("DEBUG")]
		private void ValidateInternalState()
		{
		}

		// Token: 0x0400063B RID: 1595
		internal readonly string XmlNamespace;

		// Token: 0x0400063C RID: 1596
		internal readonly string XmlBaseAttributeName;

		// Token: 0x0400063D RID: 1597
		internal readonly string XmlLangAttributeName;

		// Token: 0x0400063E RID: 1598
		internal readonly string ODataMetadataNamespace;

		// Token: 0x0400063F RID: 1599
		internal readonly string ODataNamespace;

		// Token: 0x04000640 RID: 1600
		internal readonly string ODataErrorElementName;

		// Token: 0x04000641 RID: 1601
		private readonly XmlReader reader;

		// Token: 0x04000642 RID: 1602
		private readonly LinkedList<BufferingXmlReader.BufferedNode> bufferedNodes;

		// Token: 0x04000643 RID: 1603
		private readonly BufferingXmlReader.BufferedNode endOfInputBufferedNode;

		// Token: 0x04000644 RID: 1604
		private readonly bool disableXmlBase;

		// Token: 0x04000645 RID: 1605
		private readonly int maxInnerErrorDepth;

		// Token: 0x04000646 RID: 1606
		private readonly Uri documentBaseUri;

		// Token: 0x04000647 RID: 1607
		private LinkedListNode<BufferingXmlReader.BufferedNode> currentBufferedNode;

		// Token: 0x04000648 RID: 1608
		private LinkedListNode<BufferingXmlReader.BufferedNode> currentAttributeNode;

		// Token: 0x04000649 RID: 1609
		private LinkedListNode<BufferingXmlReader.BufferedNode> currentBufferedNodeToReport;

		// Token: 0x0400064A RID: 1610
		private bool isBuffering;

		// Token: 0x0400064B RID: 1611
		private bool removeOnNextRead;

		// Token: 0x0400064C RID: 1612
		private bool disableInStreamErrorDetection;

		// Token: 0x0400064D RID: 1613
		private Stack<BufferingXmlReader.XmlBaseDefinition> xmlBaseStack;

		// Token: 0x0400064E RID: 1614
		private Stack<BufferingXmlReader.XmlBaseDefinition> bufferStartXmlBaseStack;

		// Token: 0x02000224 RID: 548
		private sealed class BufferedNode
		{
			// Token: 0x0600104F RID: 4175 RVA: 0x0003DD10 File Offset: 0x0003BF10
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

			// Token: 0x06001050 RID: 4176 RVA: 0x0003DD78 File Offset: 0x0003BF78
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

			// Token: 0x06001051 RID: 4177 RVA: 0x0003DDCA File Offset: 0x0003BFCA
			private BufferedNode(string emptyString)
			{
				this.NodeType = 0;
				this.NamespaceUri = emptyString;
				this.LocalName = emptyString;
				this.Prefix = emptyString;
				this.Value = emptyString;
			}

			// Token: 0x170003AC RID: 940
			// (get) Token: 0x06001052 RID: 4178 RVA: 0x0003DDF5 File Offset: 0x0003BFF5
			// (set) Token: 0x06001053 RID: 4179 RVA: 0x0003DDFD File Offset: 0x0003BFFD
			internal XmlNodeType NodeType { get; private set; }

			// Token: 0x170003AD RID: 941
			// (get) Token: 0x06001054 RID: 4180 RVA: 0x0003DE06 File Offset: 0x0003C006
			// (set) Token: 0x06001055 RID: 4181 RVA: 0x0003DE0E File Offset: 0x0003C00E
			internal string NamespaceUri { get; private set; }

			// Token: 0x170003AE RID: 942
			// (get) Token: 0x06001056 RID: 4182 RVA: 0x0003DE17 File Offset: 0x0003C017
			// (set) Token: 0x06001057 RID: 4183 RVA: 0x0003DE1F File Offset: 0x0003C01F
			internal string LocalName { get; private set; }

			// Token: 0x170003AF RID: 943
			// (get) Token: 0x06001058 RID: 4184 RVA: 0x0003DE28 File Offset: 0x0003C028
			// (set) Token: 0x06001059 RID: 4185 RVA: 0x0003DE30 File Offset: 0x0003C030
			internal string Prefix { get; private set; }

			// Token: 0x170003B0 RID: 944
			// (get) Token: 0x0600105A RID: 4186 RVA: 0x0003DE39 File Offset: 0x0003C039
			// (set) Token: 0x0600105B RID: 4187 RVA: 0x0003DE41 File Offset: 0x0003C041
			internal string Value { get; private set; }

			// Token: 0x170003B1 RID: 945
			// (get) Token: 0x0600105C RID: 4188 RVA: 0x0003DE4A File Offset: 0x0003C04A
			// (set) Token: 0x0600105D RID: 4189 RVA: 0x0003DE52 File Offset: 0x0003C052
			internal int Depth { get; private set; }

			// Token: 0x170003B2 RID: 946
			// (get) Token: 0x0600105E RID: 4190 RVA: 0x0003DE5B File Offset: 0x0003C05B
			// (set) Token: 0x0600105F RID: 4191 RVA: 0x0003DE63 File Offset: 0x0003C063
			internal bool IsEmptyElement { get; private set; }

			// Token: 0x170003B3 RID: 947
			// (get) Token: 0x06001060 RID: 4192 RVA: 0x0003DE6C File Offset: 0x0003C06C
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

			// Token: 0x06001061 RID: 4193 RVA: 0x0003DE90 File Offset: 0x0003C090
			internal static BufferingXmlReader.BufferedNode CreateEndOfInput(XmlNameTable nametable)
			{
				string text = nametable.Add(string.Empty);
				return new BufferingXmlReader.BufferedNode(text);
			}

			// Token: 0x0400064F RID: 1615
			private LinkedList<BufferingXmlReader.BufferedNode> attributeNodes;
		}

		// Token: 0x02000225 RID: 549
		private sealed class XmlBaseDefinition
		{
			// Token: 0x06001062 RID: 4194 RVA: 0x0003DEAF File Offset: 0x0003C0AF
			internal XmlBaseDefinition(Uri baseUri, int depth)
			{
				this.BaseUri = baseUri;
				this.Depth = depth;
			}

			// Token: 0x170003B4 RID: 948
			// (get) Token: 0x06001063 RID: 4195 RVA: 0x0003DEC5 File Offset: 0x0003C0C5
			// (set) Token: 0x06001064 RID: 4196 RVA: 0x0003DECD File Offset: 0x0003C0CD
			internal Uri BaseUri { get; private set; }

			// Token: 0x170003B5 RID: 949
			// (get) Token: 0x06001065 RID: 4197 RVA: 0x0003DED6 File Offset: 0x0003C0D6
			// (set) Token: 0x06001066 RID: 4198 RVA: 0x0003DEDE File Offset: 0x0003C0DE
			internal int Depth { get; private set; }
		}
	}
}
