using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002126 RID: 8486
	internal class OpenXmlPartReader : OpenXmlReader
	{
		// Token: 0x0600D214 RID: 53780 RVA: 0x0029C4A2 File Offset: 0x0029A6A2
		private OpenXmlPartReader()
		{
			this._attributeList = new List<OpenXmlAttribute>();
			this._nsDecls = new List<KeyValuePair<string, string>>();
			this._elementStack = new Stack<OpenXmlElement>();
			this._elementContext = new OpenXmlElementContext();
			this._elementState = ElementState.Null;
		}

		// Token: 0x0600D215 RID: 53781 RVA: 0x0029C4DD File Offset: 0x0029A6DD
		private OpenXmlPartReader(bool readMiscNodes)
			: base(readMiscNodes)
		{
			this._attributeList = new List<OpenXmlAttribute>();
			this._nsDecls = new List<KeyValuePair<string, string>>();
			this._elementStack = new Stack<OpenXmlElement>();
			this._elementContext = new OpenXmlElementContext();
			this._elementState = ElementState.Null;
		}

		// Token: 0x0600D216 RID: 53782 RVA: 0x0029C51C File Offset: 0x0029A71C
		public OpenXmlPartReader(OpenXmlPart openXmlPart)
			: this()
		{
			if (openXmlPart == null)
			{
				throw new ArgumentNullException("openXmlPart");
			}
			Stream stream = openXmlPart.GetStream(FileMode.Open);
			this._elementContext.XmlReaderSettings.MaxCharactersInDocument = openXmlPart.MaxCharactersInPart;
			this.Init(stream, true);
		}

		// Token: 0x0600D217 RID: 53783 RVA: 0x0029C564 File Offset: 0x0029A764
		public OpenXmlPartReader(OpenXmlPart openXmlPart, bool readMiscNodes)
			: this(readMiscNodes)
		{
			if (openXmlPart == null)
			{
				throw new ArgumentNullException("openXmlPart");
			}
			this._elementContext.XmlReaderSettings.MaxCharactersInDocument = openXmlPart.MaxCharactersInPart;
			Stream stream = openXmlPart.GetStream(FileMode.Open);
			this.Init(stream, true);
		}

		// Token: 0x0600D218 RID: 53784 RVA: 0x0029C5AC File Offset: 0x0029A7AC
		public OpenXmlPartReader(Stream partStream)
			: this()
		{
			if (partStream == null)
			{
				throw new ArgumentNullException("partStream");
			}
			this.Init(partStream, false);
		}

		// Token: 0x0600D219 RID: 53785 RVA: 0x0029C5CA File Offset: 0x0029A7CA
		public OpenXmlPartReader(Stream partStream, bool readMiscNodes)
			: this(readMiscNodes)
		{
			if (partStream == null)
			{
				throw new ArgumentNullException("partStream");
			}
			this.Init(partStream, false);
		}

		// Token: 0x170032D0 RID: 13008
		// (get) Token: 0x0600D21A RID: 53786 RVA: 0x0029C5E9 File Offset: 0x0029A7E9
		public override string Encoding
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this._encoding;
			}
		}

		// Token: 0x170032D1 RID: 13009
		// (get) Token: 0x0600D21B RID: 53787 RVA: 0x0029C5F7 File Offset: 0x0029A7F7
		public override bool? StandaloneXml
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this._standalone;
			}
		}

		// Token: 0x170032D2 RID: 13010
		// (get) Token: 0x0600D21C RID: 53788 RVA: 0x0029C608 File Offset: 0x0029A808
		public override ReadOnlyCollection<OpenXmlAttribute> Attributes
		{
			get
			{
				this.ThrowIfObjectDisposed();
				this.ThrowIfNull();
				this.ThrowIfEof();
				if (this._elementState == ElementState.Start || this._elementState == ElementState.LeafStart)
				{
					return new ReadOnlyCollection<OpenXmlAttribute>(this._attributeList);
				}
				if (OpenXmlPartReader._emptyReadOnlyList == null)
				{
					OpenXmlPartReader._emptyReadOnlyList = new ReadOnlyCollection<OpenXmlAttribute>(new List<OpenXmlAttribute>());
				}
				return OpenXmlPartReader._emptyReadOnlyList;
			}
		}

		// Token: 0x170032D3 RID: 13011
		// (get) Token: 0x0600D21D RID: 53789 RVA: 0x0029C660 File Offset: 0x0029A860
		public override IEnumerable<KeyValuePair<string, string>> NamespaceDeclarations
		{
			get
			{
				this.ThrowIfObjectDisposed();
				this.ThrowIfNull();
				this.ThrowIfEof();
				if (this._elementState == ElementState.Start || this._elementState == ElementState.LeafStart)
				{
					return this._nsDecls;
				}
				return EmptyEnumerable<KeyValuePair<string, string>>.EmptyEnumerableSingleton;
			}
		}

		// Token: 0x170032D4 RID: 13012
		// (get) Token: 0x0600D21E RID: 53790 RVA: 0x0029C692 File Offset: 0x0029A892
		public override Type ElementType
		{
			get
			{
				this.ThrowIfObjectDisposed();
				this.ThrowIfNull();
				this.ThrowIfEof();
				return this._elementStack.Peek().GetType();
			}
		}

		// Token: 0x170032D5 RID: 13013
		// (get) Token: 0x0600D21F RID: 53791 RVA: 0x0029C6B6 File Offset: 0x0029A8B6
		public override bool IsMiscNode
		{
			get
			{
				this.ThrowIfObjectDisposed();
				this.ThrowIfNull();
				this.ThrowIfEof();
				return this._elementState == ElementState.MiscNode;
			}
		}

		// Token: 0x170032D6 RID: 13014
		// (get) Token: 0x0600D220 RID: 53792 RVA: 0x0029C6D6 File Offset: 0x0029A8D6
		public override bool IsStartElement
		{
			get
			{
				this.ThrowIfObjectDisposed();
				this.ThrowIfNull();
				this.ThrowIfEof();
				return !this.IsMiscNode && (this._elementState == ElementState.Start || this._elementState == ElementState.LeafStart);
			}
		}

		// Token: 0x170032D7 RID: 13015
		// (get) Token: 0x0600D221 RID: 53793 RVA: 0x0029C707 File Offset: 0x0029A907
		public override bool IsEndElement
		{
			get
			{
				this.ThrowIfObjectDisposed();
				this.ThrowIfNull();
				this.ThrowIfEof();
				return !this.IsMiscNode && (this._elementState == ElementState.End || this._elementState == ElementState.LeafEnd || this._elementState == ElementState.LoadEnd);
			}
		}

		// Token: 0x170032D8 RID: 13016
		// (get) Token: 0x0600D222 RID: 53794 RVA: 0x0029C741 File Offset: 0x0029A941
		public override int Depth
		{
			get
			{
				this.ThrowIfObjectDisposed();
				this.ThrowIfNull();
				this.ThrowIfEof();
				return this._elementStack.Count - 1;
			}
		}

		// Token: 0x170032D9 RID: 13017
		// (get) Token: 0x0600D223 RID: 53795 RVA: 0x0029C762 File Offset: 0x0029A962
		public override bool EOF
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this._elementState == ElementState.EOF;
			}
		}

		// Token: 0x170032DA RID: 13018
		// (get) Token: 0x0600D224 RID: 53796 RVA: 0x0029C773 File Offset: 0x0029A973
		public override string LocalName
		{
			get
			{
				this.ThrowIfObjectDisposed();
				this.ThrowIfNull();
				this.ThrowIfEof();
				return this._elementStack.Peek().LocalName;
			}
		}

		// Token: 0x170032DB RID: 13019
		// (get) Token: 0x0600D225 RID: 53797 RVA: 0x0029C797 File Offset: 0x0029A997
		public override string NamespaceUri
		{
			get
			{
				this.ThrowIfObjectDisposed();
				this.ThrowIfNull();
				this.ThrowIfEof();
				return this._elementStack.Peek().NamespaceUri;
			}
		}

		// Token: 0x170032DC RID: 13020
		// (get) Token: 0x0600D226 RID: 53798 RVA: 0x0029C7BB File Offset: 0x0029A9BB
		public override string Prefix
		{
			get
			{
				this.ThrowIfObjectDisposed();
				this.ThrowIfNull();
				this.ThrowIfEof();
				return this._elementStack.Peek().Prefix;
			}
		}

		// Token: 0x0600D227 RID: 53799 RVA: 0x0029C7E0 File Offset: 0x0029A9E0
		public override bool Read()
		{
			this.ThrowIfObjectDisposed();
			bool flag = this.MoveToNextElement();
			if (flag && !base.ReadMiscNodes)
			{
				while (flag && this.IsMiscNode)
				{
					flag = this.MoveToNextElement();
				}
			}
			return flag;
		}

		// Token: 0x0600D228 RID: 53800 RVA: 0x0029C81C File Offset: 0x0029AA1C
		public override bool ReadFirstChild()
		{
			this.ThrowIfObjectDisposed();
			bool flag = this.MoveToFirstChild();
			if (flag && !base.ReadMiscNodes)
			{
				while (flag && this.IsMiscNode)
				{
					flag = this.MoveToNextSibling();
				}
			}
			return flag;
		}

		// Token: 0x0600D229 RID: 53801 RVA: 0x0029C858 File Offset: 0x0029AA58
		public override bool ReadNextSibling()
		{
			this.ThrowIfObjectDisposed();
			bool flag = this.MoveToNextSibling();
			if (flag && !base.ReadMiscNodes)
			{
				while (flag && this.IsMiscNode)
				{
					flag = this.MoveToNextSibling();
				}
			}
			return flag;
		}

		// Token: 0x0600D22A RID: 53802 RVA: 0x0029C892 File Offset: 0x0029AA92
		public override void Skip()
		{
			this.ThrowIfObjectDisposed();
			this.InnerSkip();
			if (!this.EOF && !base.ReadMiscNodes)
			{
				while (!this.EOF && this.IsMiscNode)
				{
					this.InnerSkip();
				}
			}
		}

		// Token: 0x0600D22B RID: 53803 RVA: 0x0029C8C8 File Offset: 0x0029AAC8
		private bool MoveToNextElement()
		{
			switch (this._elementState)
			{
			case ElementState.Null:
				return this.ReadRoot();
			case ElementState.End:
			case ElementState.MiscNode:
				this._elementStack.Pop();
				break;
			case ElementState.LeafStart:
				this._elementState = ElementState.LeafEnd;
				return true;
			case ElementState.LeafEnd:
			case ElementState.LoadEnd:
				this._elementStack.Pop();
				if (this._elementStack.Count == 0)
				{
					this._elementState = ElementState.EOF;
					return false;
				}
				this.GetElementInformation();
				return true;
			case ElementState.EOF:
				return false;
			}
			this._elementState = ElementState.Null;
			if (this._xmlReader.EOF || !this._xmlReader.Read())
			{
				this._elementState = ElementState.EOF;
				return false;
			}
			this.GetElementInformation();
			return true;
		}

		// Token: 0x0600D22C RID: 53804 RVA: 0x0029C980 File Offset: 0x0029AB80
		private bool MoveToFirstChild()
		{
			switch (this._elementState)
			{
			case ElementState.Null:
				this.ThrowIfNull();
				break;
			case ElementState.Start:
				if (!this._xmlReader.Read())
				{
					return false;
				}
				this.GetElementInformation();
				return this._elementState != ElementState.End;
			case ElementState.End:
			case ElementState.LeafEnd:
			case ElementState.LoadEnd:
			case ElementState.MiscNode:
				return false;
			case ElementState.LeafStart:
				this._elementState = ElementState.LeafEnd;
				return false;
			case ElementState.EOF:
				return false;
			}
			return false;
		}

		// Token: 0x0600D22D RID: 53805 RVA: 0x0029C9F2 File Offset: 0x0029ABF2
		private bool MoveToNextSibling()
		{
			if (this._elementState == ElementState.EOF)
			{
				return false;
			}
			this.InnerSkip();
			return this._elementState != ElementState.EOF && this._elementState != ElementState.End;
		}

		// Token: 0x0600D22E RID: 53806 RVA: 0x0029CA1C File Offset: 0x0029AC1C
		private void InnerSkip()
		{
			switch (this._elementState)
			{
			case ElementState.Null:
				this.ThrowIfNull();
				break;
			case ElementState.Start:
				this._xmlReader.Skip();
				this._elementStack.Pop();
				this.GetElementInformation();
				return;
			case ElementState.End:
			case ElementState.MiscNode:
				this._xmlReader.Skip();
				this._elementStack.Pop();
				this.GetElementInformation();
				return;
			case ElementState.LeafStart:
				this._elementStack.Pop();
				this.GetElementInformation();
				return;
			case ElementState.LeafEnd:
			case ElementState.LoadEnd:
				this._elementStack.Pop();
				this.GetElementInformation();
				return;
			case ElementState.EOF:
				break;
			default:
				return;
			}
		}

		// Token: 0x0600D22F RID: 53807 RVA: 0x0029CAC0 File Offset: 0x0029ACC0
		public override OpenXmlElement LoadCurrentElement()
		{
			this.ThrowIfObjectDisposed();
			switch (this._elementState)
			{
			case ElementState.Null:
				this.ThrowIfNull();
				break;
			case ElementState.Start:
			{
				OpenXmlElement openXmlElement = this._elementStack.Peek();
				openXmlElement.Load(this._xmlReader, OpenXmlLoadMode.Full);
				this._elementState = ElementState.LoadEnd;
				return openXmlElement;
			}
			case ElementState.End:
			case ElementState.LeafEnd:
			case ElementState.LoadEnd:
				throw new InvalidOperationException(ExceptionMessages.ReaderInEndState);
			case ElementState.LeafStart:
			{
				OpenXmlElement openXmlElement = this._elementStack.Pop();
				this._elementStack.Push(openXmlElement.CloneNode(true));
				this._elementState = ElementState.LoadEnd;
				return openXmlElement;
			}
			case ElementState.MiscNode:
			{
				OpenXmlElement openXmlElement = this._elementStack.Pop();
				openXmlElement.Load(this._xmlReader, OpenXmlLoadMode.Full);
				this.GetElementInformation();
				return openXmlElement;
			}
			case ElementState.EOF:
				this.ThrowIfEof();
				break;
			}
			return null;
		}

		// Token: 0x0600D230 RID: 53808 RVA: 0x0029CB8C File Offset: 0x0029AD8C
		public override string GetText()
		{
			this.ThrowIfObjectDisposed();
			if (this._elementState == ElementState.LeafStart)
			{
				OpenXmlElement openXmlElement = this._elementStack.Peek();
				OpenXmlLeafTextElement openXmlLeafTextElement = openXmlElement as OpenXmlLeafTextElement;
				if (openXmlLeafTextElement != null)
				{
					return openXmlLeafTextElement.Text;
				}
			}
			return string.Empty;
		}

		// Token: 0x0600D231 RID: 53809 RVA: 0x0029CBCA File Offset: 0x0029ADCA
		public override void Close()
		{
			this.ThrowIfObjectDisposed();
			this._elementState = ElementState.EOF;
			this._elementStack.Clear();
			this._xmlReader.Close();
		}

		// Token: 0x0600D232 RID: 53810 RVA: 0x0029CBF0 File Offset: 0x0029ADF0
		private void Init(Stream partStream, bool closeInput)
		{
			this._elementContext.XmlReaderSettings.CloseInput = closeInput;
			this._xmlReader = XmlReader.Create(partStream, this._elementContext.XmlReaderSettings);
			this._xmlReader.Read();
			if (this._xmlReader.NodeType == XmlNodeType.XmlDeclaration)
			{
				this._encoding = this._xmlReader["encoding"];
				string text = this._xmlReader["standalone"];
				if (!string.IsNullOrEmpty(text))
				{
					if (text == "yes")
					{
						this._standalone = new bool?(true);
					}
					else
					{
						this._standalone = new bool?(false);
					}
				}
			}
			this._elementState = ElementState.Null;
		}

		// Token: 0x0600D233 RID: 53811 RVA: 0x0029CCA0 File Offset: 0x0029AEA0
		private bool ReadRoot()
		{
			this._xmlReader.MoveToContent();
			while (!this._xmlReader.EOF && this._xmlReader.NodeType != XmlNodeType.Element)
			{
				this._xmlReader.Skip();
			}
			if (this._xmlReader.EOF || !this._xmlReader.IsStartElement())
			{
				throw new InvalidDataException(ExceptionMessages.PartIsEmpty);
			}
			OpenXmlElement openXmlElement = RootElementFactory.CreateElement(this._xmlReader.NamespaceURI, this._xmlReader.LocalName);
			if (openXmlElement == null)
			{
				throw new InvalidDataException(ExceptionMessages.PartUnknown);
			}
			this._elementStack.Push(openXmlElement);
			this.LoadAttributes();
			if (this._xmlReader.IsEmptyElement)
			{
				this._elementState = ElementState.LeafStart;
				openXmlElement.Load(this._xmlReader, OpenXmlLoadMode.Full);
			}
			else
			{
				this._elementState = ElementState.Start;
			}
			return true;
		}

		// Token: 0x0600D234 RID: 53812 RVA: 0x0029CD70 File Offset: 0x0029AF70
		private void LoadAttributes()
		{
			this._attributeList.Clear();
			this._nsDecls.Clear();
			if (this._xmlReader.HasAttributes)
			{
				while (this._xmlReader.MoveToNextAttribute())
				{
					if (this._xmlReader.Prefix == "xmlns")
					{
						this._nsDecls.Add(new KeyValuePair<string, string>(this._xmlReader.LocalName, this._xmlReader.Value));
					}
					else
					{
						OpenXmlAttribute openXmlAttribute = new OpenXmlAttribute(this._xmlReader.Prefix, this._xmlReader.LocalName, this._xmlReader.NamespaceURI, this._xmlReader.Value);
						this._attributeList.Add(openXmlAttribute);
					}
				}
				this._xmlReader.MoveToElement();
			}
		}

		// Token: 0x0600D235 RID: 53813 RVA: 0x0029CE40 File Offset: 0x0029B040
		private void GetElementInformation()
		{
			if (this._xmlReader.EOF)
			{
				this._elementState = ElementState.EOF;
				return;
			}
			XmlNodeType nodeType = this._xmlReader.NodeType;
			OpenXmlElement openXmlElement;
			if (nodeType == XmlNodeType.Element)
			{
				openXmlElement = this.CreateChildElement();
				this.LoadAttributes();
				if (this._xmlReader.IsEmptyElement)
				{
					this._elementState = ElementState.LeafStart;
					openXmlElement.Load(this._xmlReader, OpenXmlLoadMode.Full);
				}
				else if (openXmlElement is OpenXmlLeafElement || openXmlElement is OpenXmlLeafTextElement)
				{
					this._elementState = ElementState.LeafStart;
					openXmlElement.Load(this._xmlReader, OpenXmlLoadMode.Full);
				}
				else if (openXmlElement is OpenXmlUnknownElement)
				{
					this._elementState = ElementState.Start;
				}
				else
				{
					this._elementState = ElementState.Start;
				}
				this._elementStack.Push(openXmlElement);
				return;
			}
			if (nodeType == XmlNodeType.EndElement)
			{
				this._elementState = ElementState.End;
				return;
			}
			openXmlElement = this.CreateChildElement();
			(openXmlElement as OpenXmlMiscNode).LoadOuterXml(this._xmlReader);
			this._elementStack.Push(openXmlElement);
			this._elementState = ElementState.MiscNode;
		}

		// Token: 0x0600D236 RID: 53814 RVA: 0x0029CF28 File Offset: 0x0029B128
		private OpenXmlElement CreateChildElement()
		{
			OpenXmlElement openXmlElement = this._elementStack.Peek();
			if ((openXmlElement is AlternateContentChoice || openXmlElement is AlternateContentFallback) && this._elementStack.Count > 2)
			{
				OpenXmlElement openXmlElement2 = this._elementStack.Pop();
				OpenXmlElement openXmlElement3 = this._elementStack.Pop();
				OpenXmlElement openXmlElement4 = this._elementStack.Peek().CloneNode(false);
				this._elementStack.Push(openXmlElement3);
				this._elementStack.Push(openXmlElement2);
				openXmlElement = openXmlElement2.CloneNode(false);
				openXmlElement3 = new AlternateContent();
				openXmlElement3.AppendChild<OpenXmlElement>(openXmlElement);
				openXmlElement4.AppendChild<OpenXmlElement>(openXmlElement3);
			}
			return openXmlElement.ElementFactory(this._xmlReader);
		}

		// Token: 0x0600D237 RID: 53815 RVA: 0x0029CFCB File Offset: 0x0029B1CB
		private void ThrowIfNull()
		{
			if (this._elementState == ElementState.Null)
			{
				throw new InvalidOperationException(ExceptionMessages.ReaderInNullState);
			}
		}

		// Token: 0x0600D238 RID: 53816 RVA: 0x0029CFE0 File Offset: 0x0029B1E0
		private void ThrowIfEof()
		{
			if (this._elementState == ElementState.EOF || this._elementStack.Count <= 0)
			{
				throw new InvalidOperationException(ExceptionMessages.ReaderInEofState);
			}
		}

		// Token: 0x04006976 RID: 26998
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static ReadOnlyCollection<OpenXmlAttribute> _emptyReadOnlyList;

		// Token: 0x04006977 RID: 26999
		private OpenXmlElementContext _elementContext;

		// Token: 0x04006978 RID: 27000
		private XmlReader _xmlReader;

		// Token: 0x04006979 RID: 27001
		private IList<OpenXmlAttribute> _attributeList;

		// Token: 0x0400697A RID: 27002
		private IList<KeyValuePair<string, string>> _nsDecls;

		// Token: 0x0400697B RID: 27003
		private Stack<OpenXmlElement> _elementStack;

		// Token: 0x0400697C RID: 27004
		private ElementState _elementState;

		// Token: 0x0400697D RID: 27005
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _encoding;

		// Token: 0x0400697E RID: 27006
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool? _standalone;
	}
}
