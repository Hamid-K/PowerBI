using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002127 RID: 8487
	internal class OpenXmlDomReader : OpenXmlReader
	{
		// Token: 0x0600D239 RID: 53817 RVA: 0x0029D004 File Offset: 0x0029B204
		private OpenXmlDomReader()
		{
			this._elementStack = new Stack<OpenXmlElement>();
			this._elementState = ElementState.Null;
		}

		// Token: 0x0600D23A RID: 53818 RVA: 0x0029D01E File Offset: 0x0029B21E
		private OpenXmlDomReader(bool readMiscNodes)
			: base(readMiscNodes)
		{
			this._elementStack = new Stack<OpenXmlElement>();
			this._elementState = ElementState.Null;
		}

		// Token: 0x0600D23B RID: 53819 RVA: 0x0029D039 File Offset: 0x0029B239
		public OpenXmlDomReader(OpenXmlElement openXmlElement)
			: this()
		{
			if (openXmlElement == null)
			{
				throw new ArgumentNullException("openXmlElement");
			}
			this.Init(openXmlElement);
		}

		// Token: 0x0600D23C RID: 53820 RVA: 0x0029D056 File Offset: 0x0029B256
		public OpenXmlDomReader(OpenXmlElement openXmlElement, bool readMiscNodes)
			: this(readMiscNodes)
		{
			if (openXmlElement == null)
			{
				throw new ArgumentNullException("openXmlElement");
			}
			this.Init(openXmlElement);
		}

		// Token: 0x170032DD RID: 13021
		// (get) Token: 0x0600D23D RID: 53821 RVA: 0x0029D074 File Offset: 0x0029B274
		public override ReadOnlyCollection<OpenXmlAttribute> Attributes
		{
			get
			{
				this.ThrowIfObjectDisposed();
				this.ThrowIfNull();
				this.ThrowIfEof();
				if (this._elementState == ElementState.Start)
				{
					OpenXmlElement openXmlElement = this._elementStack.Peek();
					return new ReadOnlyCollection<OpenXmlAttribute>(openXmlElement.GetAttributes());
				}
				if (OpenXmlDomReader._emptyReadOnlyList == null)
				{
					OpenXmlDomReader._emptyReadOnlyList = new ReadOnlyCollection<OpenXmlAttribute>(new List<OpenXmlAttribute>());
				}
				return OpenXmlDomReader._emptyReadOnlyList;
			}
		}

		// Token: 0x170032DE RID: 13022
		// (get) Token: 0x0600D23E RID: 53822 RVA: 0x0029D0D0 File Offset: 0x0029B2D0
		public override IEnumerable<KeyValuePair<string, string>> NamespaceDeclarations
		{
			get
			{
				this.ThrowIfObjectDisposed();
				this.ThrowIfNull();
				this.ThrowIfEof();
				if (this._elementState == ElementState.Start)
				{
					OpenXmlElement openXmlElement = this._elementStack.Peek();
					return openXmlElement.NamespaceDeclarations;
				}
				return EmptyEnumerable<KeyValuePair<string, string>>.EmptyEnumerableSingleton;
			}
		}

		// Token: 0x170032DF RID: 13023
		// (get) Token: 0x0600D23F RID: 53823 RVA: 0x0029D110 File Offset: 0x0029B310
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

		// Token: 0x170032E0 RID: 13024
		// (get) Token: 0x0600D240 RID: 53824 RVA: 0x0029D134 File Offset: 0x0029B334
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

		// Token: 0x170032E1 RID: 13025
		// (get) Token: 0x0600D241 RID: 53825 RVA: 0x0029D154 File Offset: 0x0029B354
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

		// Token: 0x170032E2 RID: 13026
		// (get) Token: 0x0600D242 RID: 53826 RVA: 0x0029D185 File Offset: 0x0029B385
		public override bool IsEndElement
		{
			get
			{
				this.ThrowIfObjectDisposed();
				this.ThrowIfNull();
				this.ThrowIfEof();
				return !this.IsMiscNode && this._elementState == ElementState.End;
			}
		}

		// Token: 0x170032E3 RID: 13027
		// (get) Token: 0x0600D243 RID: 53827 RVA: 0x0029D1AD File Offset: 0x0029B3AD
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

		// Token: 0x170032E4 RID: 13028
		// (get) Token: 0x0600D244 RID: 53828 RVA: 0x0029D1CE File Offset: 0x0029B3CE
		public override bool EOF
		{
			get
			{
				this.ThrowIfObjectDisposed();
				return this._elementState == ElementState.EOF;
			}
		}

		// Token: 0x170032E5 RID: 13029
		// (get) Token: 0x0600D245 RID: 53829 RVA: 0x0029D1DF File Offset: 0x0029B3DF
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

		// Token: 0x170032E6 RID: 13030
		// (get) Token: 0x0600D246 RID: 53830 RVA: 0x0029D203 File Offset: 0x0029B403
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

		// Token: 0x170032E7 RID: 13031
		// (get) Token: 0x0600D247 RID: 53831 RVA: 0x0029D227 File Offset: 0x0029B427
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

		// Token: 0x0600D248 RID: 53832 RVA: 0x0029D24C File Offset: 0x0029B44C
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

		// Token: 0x0600D249 RID: 53833 RVA: 0x0029D288 File Offset: 0x0029B488
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

		// Token: 0x0600D24A RID: 53834 RVA: 0x0029D2C4 File Offset: 0x0029B4C4
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

		// Token: 0x0600D24B RID: 53835 RVA: 0x0029D2FE File Offset: 0x0029B4FE
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

		// Token: 0x0600D24C RID: 53836 RVA: 0x0029D334 File Offset: 0x0029B534
		private bool MoveToNextElement()
		{
			if (this._elementState == ElementState.Null)
			{
				return this.ReadRoot();
			}
			switch (this._elementState)
			{
			case ElementState.Start:
			{
				OpenXmlElement openXmlElement = this._elementStack.Peek();
				if (!openXmlElement.HasChildren)
				{
					this._elementState = ElementState.End;
					return true;
				}
				this._elementStack.Push(openXmlElement.FirstChild);
				if (openXmlElement.FirstChild is OpenXmlMiscNode)
				{
					this._elementState = ElementState.MiscNode;
					return true;
				}
				this._elementState = ElementState.Start;
				return true;
			}
			case ElementState.End:
			case ElementState.MiscNode:
			{
				OpenXmlElement openXmlElement = this._elementStack.Pop();
				if (this._elementStack.Count <= 0)
				{
					this._elementState = ElementState.EOF;
					return false;
				}
				openXmlElement = openXmlElement.NextSibling();
				if (openXmlElement == null)
				{
					this._elementState = ElementState.End;
					return true;
				}
				this._elementStack.Push(openXmlElement);
				if (openXmlElement is OpenXmlMiscNode)
				{
					this._elementState = ElementState.MiscNode;
					return true;
				}
				this._elementState = ElementState.Start;
				return true;
			}
			case ElementState.EOF:
				return false;
			}
			return false;
		}

		// Token: 0x0600D24D RID: 53837 RVA: 0x0029D430 File Offset: 0x0029B630
		private bool MoveToFirstChild()
		{
			this.ThrowIfNull();
			if (this._elementState != ElementState.Start)
			{
				return false;
			}
			OpenXmlElement openXmlElement = this._elementStack.Peek();
			if (openXmlElement.HasChildren)
			{
				this._elementStack.Push(openXmlElement.FirstChild);
				if (openXmlElement.FirstChild is OpenXmlMiscNode)
				{
					this._elementState = ElementState.MiscNode;
				}
				else
				{
					this._elementState = ElementState.Start;
				}
				return true;
			}
			this._elementState = ElementState.End;
			return false;
		}

		// Token: 0x0600D24E RID: 53838 RVA: 0x0029D49C File Offset: 0x0029B69C
		private bool MoveToNextSibling()
		{
			if (this._elementState == ElementState.EOF)
			{
				return false;
			}
			this.ThrowIfNull();
			if (this._elementStack.Count == 0)
			{
				this._elementState = ElementState.EOF;
				return false;
			}
			OpenXmlElement openXmlElement = this._elementStack.Pop();
			if (this._elementStack.Count == 0)
			{
				this._elementState = ElementState.EOF;
				return false;
			}
			openXmlElement = openXmlElement.NextSibling();
			if (openXmlElement != null)
			{
				this._elementStack.Push(openXmlElement);
				if (openXmlElement is OpenXmlMiscNode)
				{
					this._elementState = ElementState.MiscNode;
				}
				else
				{
					this._elementState = ElementState.Start;
				}
				return true;
			}
			if (this._elementStack.Count > 0)
			{
				this._elementState = ElementState.End;
			}
			else
			{
				this._elementState = ElementState.EOF;
			}
			return false;
		}

		// Token: 0x0600D24F RID: 53839 RVA: 0x0029D544 File Offset: 0x0029B744
		private void InnerSkip()
		{
			switch (this._elementState)
			{
			case ElementState.Null:
				this.ThrowIfNull();
				break;
			case ElementState.Start:
				this.MoveToNextSibling();
				return;
			case ElementState.End:
			case ElementState.MiscNode:
				this.MoveToNextElement();
				return;
			case ElementState.LeafStart:
			case ElementState.LeafEnd:
			case ElementState.LoadEnd:
				return;
			case ElementState.EOF:
				break;
			default:
				return;
			}
		}

		// Token: 0x0600D250 RID: 53840 RVA: 0x0029D598 File Offset: 0x0029B798
		public override OpenXmlElement LoadCurrentElement()
		{
			this.ThrowIfObjectDisposed();
			this.ThrowIfNull();
			this.ThrowIfEof();
			if (this._elementState == ElementState.Start)
			{
				OpenXmlElement openXmlElement = this._elementStack.Peek();
				this._elementState = ElementState.End;
				return openXmlElement;
			}
			if (this._elementState == ElementState.MiscNode)
			{
				OpenXmlElement openXmlElement2 = this._elementStack.Peek();
				this.Skip();
				return openXmlElement2;
			}
			throw new InvalidOperationException(ExceptionMessages.ReaderInEndState);
		}

		// Token: 0x0600D251 RID: 53841 RVA: 0x0029D5FC File Offset: 0x0029B7FC
		public override string GetText()
		{
			this.ThrowIfObjectDisposed();
			if (this._elementState == ElementState.Start)
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

		// Token: 0x0600D252 RID: 53842 RVA: 0x0029D63A File Offset: 0x0029B83A
		public override void Close()
		{
			this.ThrowIfObjectDisposed();
			this._elementState = ElementState.EOF;
			this._elementStack.Clear();
			this._rootElement = null;
		}

		// Token: 0x0600D253 RID: 53843 RVA: 0x0029D65B File Offset: 0x0029B85B
		private void Init(OpenXmlElement openXmlElement)
		{
			this._rootElement = openXmlElement;
			this._elementState = ElementState.Null;
		}

		// Token: 0x0600D254 RID: 53844 RVA: 0x0029D66B File Offset: 0x0029B86B
		private bool ReadRoot()
		{
			this._elementStack.Push(this._rootElement);
			if (this._rootElement is OpenXmlMiscNode)
			{
				this._elementState = ElementState.MiscNode;
			}
			else
			{
				this._elementState = ElementState.Start;
			}
			return true;
		}

		// Token: 0x0600D255 RID: 53845 RVA: 0x0029D69C File Offset: 0x0029B89C
		private void ThrowIfNull()
		{
			if (this._elementState == ElementState.Null)
			{
				throw new InvalidOperationException(ExceptionMessages.ReaderInNullState);
			}
		}

		// Token: 0x0600D256 RID: 53846 RVA: 0x0029D6B1 File Offset: 0x0029B8B1
		private void ThrowIfEof()
		{
			if (this._elementState == ElementState.EOF || this._elementStack.Count <= 0)
			{
				throw new InvalidOperationException(ExceptionMessages.ReaderInEofState);
			}
		}

		// Token: 0x0400697F RID: 27007
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static ReadOnlyCollection<OpenXmlAttribute> _emptyReadOnlyList;

		// Token: 0x04006980 RID: 27008
		private OpenXmlElement _rootElement;

		// Token: 0x04006981 RID: 27009
		private Stack<OpenXmlElement> _elementStack;

		// Token: 0x04006982 RID: 27010
		private ElementState _elementState;
	}
}
