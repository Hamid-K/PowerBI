using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020020E3 RID: 8419
	public abstract class OpenXmlCompositeElement : OpenXmlElement
	{
		// Token: 0x0600CEE6 RID: 52966 RVA: 0x00293176 File Offset: 0x00291376
		protected OpenXmlCompositeElement()
		{
		}

		// Token: 0x0600CEE7 RID: 52967 RVA: 0x0029317E File Offset: 0x0029137E
		protected OpenXmlCompositeElement(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600CEE8 RID: 52968 RVA: 0x00293188 File Offset: 0x00291388
		protected OpenXmlCompositeElement(IEnumerable childrenElements)
			: this()
		{
			if (childrenElements == null)
			{
				throw new ArgumentNullException("childrenElements");
			}
			foreach (object obj in childrenElements)
			{
				OpenXmlElement openXmlElement = (OpenXmlElement)obj;
				this.AppendChild<OpenXmlElement>(openXmlElement);
			}
		}

		// Token: 0x0600CEE9 RID: 52969 RVA: 0x002931F4 File Offset: 0x002913F4
		protected OpenXmlCompositeElement(IEnumerable<OpenXmlElement> childrenElements)
			: this()
		{
			if (childrenElements == null)
			{
				throw new ArgumentNullException("childrenElements");
			}
			foreach (OpenXmlElement openXmlElement in childrenElements)
			{
				this.AppendChild<OpenXmlElement>(openXmlElement);
			}
		}

		// Token: 0x0600CEEA RID: 52970 RVA: 0x00293254 File Offset: 0x00291454
		protected OpenXmlCompositeElement(params OpenXmlElement[] childrenElements)
			: this()
		{
			if (childrenElements == null)
			{
				throw new ArgumentNullException("childrenElements");
			}
			foreach (OpenXmlElement openXmlElement in childrenElements)
			{
				this.AppendChild<OpenXmlElement>(openXmlElement);
			}
		}

		// Token: 0x170031B3 RID: 12723
		// (get) Token: 0x0600CEEB RID: 52971 RVA: 0x00293294 File Offset: 0x00291494
		public override OpenXmlElement FirstChild
		{
			get
			{
				base.MakeSureParsed();
				OpenXmlElement lastChild = this._lastChild;
				if (lastChild != null)
				{
					return lastChild.next;
				}
				return null;
			}
		}

		// Token: 0x170031B4 RID: 12724
		// (get) Token: 0x0600CEEC RID: 52972 RVA: 0x002932B9 File Offset: 0x002914B9
		public override OpenXmlElement LastChild
		{
			get
			{
				base.MakeSureParsed();
				return this._lastChild;
			}
		}

		// Token: 0x170031B5 RID: 12725
		// (get) Token: 0x0600CEED RID: 52973 RVA: 0x002932C7 File Offset: 0x002914C7
		public override bool HasChildren
		{
			get
			{
				return this.LastChild != null;
			}
		}

		// Token: 0x170031B6 RID: 12726
		// (get) Token: 0x0600CEEE RID: 52974 RVA: 0x002932D8 File Offset: 0x002914D8
		public override string InnerText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (OpenXmlElement openXmlElement in this.ChildElements)
				{
					stringBuilder.Append(openXmlElement.InnerText);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170031B7 RID: 12727
		// (set) Token: 0x0600CEEF RID: 52975 RVA: 0x00293338 File Offset: 0x00291538
		public override string InnerXml
		{
			set
			{
				this.RemoveAllChildren();
				if (!string.IsNullOrEmpty(value))
				{
					StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
					XmlTextWriter xmlTextWriter = new XmlDOMTextWriter(stringWriter);
					try
					{
						xmlTextWriter.WriteStartElement(this.Prefix, this.LocalName, this.NamespaceUri);
						xmlTextWriter.WriteRaw(value);
						xmlTextWriter.WriteEndElement();
					}
					finally
					{
						xmlTextWriter.Close();
					}
					OpenXmlElement openXmlElement = this.CloneNode(false);
					openXmlElement.OuterXml = stringWriter.ToString();
					OpenXmlElement openXmlElement3;
					for (OpenXmlElement openXmlElement2 = openXmlElement.FirstChild; openXmlElement2 != null; openXmlElement2 = openXmlElement3)
					{
						openXmlElement3 = openXmlElement2.NextSibling();
						openXmlElement2 = openXmlElement.RemoveChild<OpenXmlElement>(openXmlElement2);
						this.AppendChild<OpenXmlElement>(openXmlElement2);
					}
				}
			}
		}

		// Token: 0x0600CEF0 RID: 52976 RVA: 0x002933E4 File Offset: 0x002915E4
		public override T AppendChild<T>(T newChild)
		{
			if (newChild == null)
			{
				return default(T);
			}
			if (newChild.Parent != null)
			{
				throw new InvalidOperationException(ExceptionMessages.ElementIsPartOfTree);
			}
			this.ElementInsertingEvent(newChild);
			OpenXmlElement lastChild = this.LastChild;
			OpenXmlElement openXmlElement = newChild;
			if (lastChild == null)
			{
				openXmlElement.next = openXmlElement;
				this._lastChild = openXmlElement;
			}
			else
			{
				openXmlElement.next = lastChild.next;
				lastChild.next = openXmlElement;
				this._lastChild = openXmlElement;
			}
			newChild.Parent = this;
			this.ElementInsertedEvent(newChild);
			return newChild;
		}

		// Token: 0x0600CEF1 RID: 52977 RVA: 0x00293480 File Offset: 0x00291680
		public override T InsertAfter<T>(T newChild, OpenXmlElement refChild)
		{
			if (newChild == null)
			{
				return default(T);
			}
			if (newChild.Parent != null)
			{
				throw new InvalidOperationException(ExceptionMessages.ElementIsPartOfTree);
			}
			if (refChild == null)
			{
				return this.PrependChild<T>(newChild);
			}
			if (refChild.Parent != this)
			{
				throw new InvalidOperationException();
			}
			this.ElementInsertingEvent(newChild);
			OpenXmlElement openXmlElement = newChild;
			if (refChild == this._lastChild)
			{
				openXmlElement.next = refChild.next;
				refChild.next = openXmlElement;
				this._lastChild = openXmlElement;
			}
			else
			{
				OpenXmlElement next = refChild.next;
				openXmlElement.next = next;
				refChild.next = openXmlElement;
			}
			newChild.Parent = this;
			this.ElementInsertedEvent(newChild);
			return newChild;
		}

		// Token: 0x0600CEF2 RID: 52978 RVA: 0x00293540 File Offset: 0x00291740
		public override T InsertBefore<T>(T newChild, OpenXmlElement refChild)
		{
			if (newChild == null)
			{
				return default(T);
			}
			if (newChild.Parent != null)
			{
				throw new InvalidOperationException(ExceptionMessages.ElementIsPartOfTree);
			}
			if (refChild == null)
			{
				return this.AppendChild<T>(newChild);
			}
			if (refChild != null && refChild.Parent != this)
			{
				throw new InvalidOperationException();
			}
			this.ElementInsertingEvent(newChild);
			OpenXmlElement openXmlElement = newChild;
			if (refChild == this.FirstChild)
			{
				openXmlElement.next = refChild;
				this._lastChild.next = openXmlElement;
			}
			else
			{
				OpenXmlElement openXmlElement2 = refChild.PreviousSibling();
				openXmlElement.next = refChild;
				openXmlElement2.next = openXmlElement;
			}
			newChild.Parent = this;
			this.ElementInsertedEvent(newChild);
			return newChild;
		}

		// Token: 0x0600CEF3 RID: 52979 RVA: 0x002935FC File Offset: 0x002917FC
		public override T InsertAt<T>(T newChild, int index)
		{
			if (newChild == null)
			{
				return default(T);
			}
			if (newChild.Parent != null)
			{
				throw new InvalidOperationException(ExceptionMessages.ElementIsPartOfTree);
			}
			if (index < 0 || index > this.ChildElements.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (index == 0)
			{
				return this.PrependChild<T>(newChild);
			}
			if (index == this.ChildElements.Count)
			{
				return this.AppendChild<T>(newChild);
			}
			OpenXmlElement openXmlElement = this.ChildElements[index];
			return this.InsertBefore<T>(newChild, openXmlElement);
		}

		// Token: 0x0600CEF4 RID: 52980 RVA: 0x00293688 File Offset: 0x00291888
		public override T PrependChild<T>(T newChild)
		{
			if (newChild == null)
			{
				return default(T);
			}
			if (newChild.Parent != null)
			{
				throw new InvalidOperationException(ExceptionMessages.ElementIsPartOfTree);
			}
			return this.InsertBefore<T>(newChild, this.FirstChild);
		}

		// Token: 0x0600CEF5 RID: 52981 RVA: 0x002936D0 File Offset: 0x002918D0
		public override T RemoveChild<T>(T oldChild)
		{
			if (oldChild == null)
			{
				return default(T);
			}
			if (oldChild.Parent != this)
			{
				throw new InvalidOperationException(ExceptionMessages.ElementIsNotChild);
			}
			T t = oldChild;
			OpenXmlElement lastChild = this._lastChild;
			this.ElementRemovingEvent(t);
			if (t == this.FirstChild)
			{
				if (t == this._lastChild)
				{
					this._lastChild = null;
				}
				else
				{
					OpenXmlElement next = t.next;
					lastChild.next = next;
				}
			}
			else if (t == this._lastChild)
			{
				OpenXmlElement openXmlElement = t.PreviousSibling();
				OpenXmlElement next2 = t.next;
				openXmlElement.next = next2;
				this._lastChild = openXmlElement;
			}
			else
			{
				OpenXmlElement openXmlElement2 = t.PreviousSibling();
				OpenXmlElement next3 = t.next;
				openXmlElement2.next = next3;
			}
			t.next = null;
			t.Parent = null;
			this.ElementRemovedEvent(t);
			return t;
		}

		// Token: 0x0600CEF6 RID: 52982 RVA: 0x002937E8 File Offset: 0x002919E8
		public override void RemoveAllChildren()
		{
			OpenXmlElement openXmlElement2;
			for (OpenXmlElement openXmlElement = this.FirstChild; openXmlElement != null; openXmlElement = openXmlElement2)
			{
				openXmlElement2 = openXmlElement.NextSibling();
				this.RemoveChild<OpenXmlElement>(openXmlElement);
			}
		}

		// Token: 0x0600CEF7 RID: 52983 RVA: 0x00293814 File Offset: 0x00291A14
		public override T ReplaceChild<T>(OpenXmlElement newChild, T oldChild)
		{
			if (oldChild == null)
			{
				return default(T);
			}
			if (newChild == null)
			{
				throw new ArgumentNullException("newChild");
			}
			if (oldChild.Parent != this)
			{
				throw new InvalidOperationException(ExceptionMessages.ElementIsNotChild);
			}
			if (newChild.Parent != null)
			{
				throw new InvalidOperationException(ExceptionMessages.ElementIsPartOfTree);
			}
			OpenXmlElement openXmlElement = oldChild.NextSibling();
			this.RemoveChild<T>(oldChild);
			this.InsertBefore<OpenXmlElement>(newChild, openXmlElement);
			return oldChild;
		}

		// Token: 0x0600CEF8 RID: 52984 RVA: 0x00293890 File Offset: 0x00291A90
		internal override void WriteContentTo(XmlWriter w)
		{
			if (this.HasChildren)
			{
				foreach (OpenXmlElement openXmlElement in this.ChildElements)
				{
					openXmlElement.WriteTo(w);
				}
			}
		}

		// Token: 0x0600CEF9 RID: 52985 RVA: 0x002938E8 File Offset: 0x00291AE8
		internal void ElementInsertingEvent(OpenXmlElement element)
		{
			if (base.OpenXmlElementContext != null)
			{
				base.OpenXmlElementContext.ElementInsertingEvent(element, this);
			}
		}

		// Token: 0x0600CEFA RID: 52986 RVA: 0x002938FF File Offset: 0x00291AFF
		internal void ElementInsertedEvent(OpenXmlElement element)
		{
			if (base.OpenXmlElementContext != null)
			{
				base.OpenXmlElementContext.ElementInsertedEvent(element, this);
			}
		}

		// Token: 0x0600CEFB RID: 52987 RVA: 0x00293916 File Offset: 0x00291B16
		internal void ElementRemovingEvent(OpenXmlElement element)
		{
			if (base.OpenXmlElementContext != null)
			{
				base.OpenXmlElementContext.ElementRemovingEvent(element, this);
			}
		}

		// Token: 0x0600CEFC RID: 52988 RVA: 0x0029392D File Offset: 0x00291B2D
		internal void ElementRemovedEvent(OpenXmlElement element)
		{
			if (base.OpenXmlElementContext != null)
			{
				base.OpenXmlElementContext.ElementRemovedEvent(element, this);
			}
		}

		// Token: 0x0600CEFD RID: 52989 RVA: 0x00293944 File Offset: 0x00291B44
		internal override void Populate(XmlReader xmlReader, OpenXmlLoadMode loadMode)
		{
			this.LoadAttributes(xmlReader);
			if (!xmlReader.IsEmptyElement)
			{
				xmlReader.Read();
				while (!xmlReader.EOF)
				{
					if (xmlReader.NodeType == XmlNodeType.EndElement)
					{
						xmlReader.Skip();
						break;
					}
					OpenXmlElement openXmlElement = base.ElementFactory(xmlReader);
					openXmlElement.Parent = this;
					bool flag = openXmlElement is AlternateContent;
					if (flag && openXmlElement.OpenXmlElementContext != null)
					{
						openXmlElement.OpenXmlElementContext.ACBlockLevel += 1U;
					}
					bool flag2 = false;
					if (!(openXmlElement is OpenXmlMiscNode))
					{
						flag2 = base.PushMcContext(xmlReader);
					}
					ElementAction elementAction = ElementAction.Normal;
					if (base.OpenXmlElementContext != null && base.OpenXmlElementContext.MCSettings.ProcessMode != MarkupCompatibilityProcessMode.NoProcess)
					{
						elementAction = base.OpenXmlElementContext.MCContext.GetElementAction(openXmlElement, base.OpenXmlElementContext.MCSettings.TargetFileFormatVersions);
					}
					openXmlElement.Load(xmlReader, loadMode);
					if (flag2)
					{
						base.PopMcContext();
					}
					if (flag && openXmlElement.OpenXmlElementContext != null)
					{
						openXmlElement.OpenXmlElementContext.ACBlockLevel -= 1U;
					}
					switch (elementAction)
					{
					case ElementAction.Normal:
						this.AddANode(openXmlElement);
						break;
					case ElementAction.Ignore:
						openXmlElement.Parent = null;
						break;
					case ElementAction.ProcessContent:
						openXmlElement.Parent = null;
						while (openXmlElement.ChildElements.Count > 0)
						{
							OpenXmlElement firstChild = openXmlElement.FirstChild;
							firstChild.Remove();
							OpenXmlElement openXmlElement2 = null;
							if (firstChild is OpenXmlUnknownElement)
							{
								openXmlElement2 = this.ElementFactory(firstChild.Prefix, firstChild.LocalName, firstChild.NamespaceUri);
								if (!(openXmlElement2 is OpenXmlUnknownElement))
								{
									openXmlElement2.OuterXml = firstChild.OuterXml;
									OpenXmlCompositeElement.RemoveUnnecessaryExtAttr(firstChild, openXmlElement2);
								}
								else
								{
									openXmlElement2 = null;
								}
							}
							if (openXmlElement2 != null)
							{
								this.AddANode(openXmlElement2);
							}
							else
							{
								this.AddANode(firstChild);
							}
						}
						break;
					case ElementAction.ACBlock:
					{
						OpenXmlCompositeElement contentFromACBlock = base.OpenXmlElementContext.MCContext.GetContentFromACBlock(openXmlElement as AlternateContent, base.OpenXmlElementContext.MCSettings.TargetFileFormatVersions);
						if (contentFromACBlock != null)
						{
							openXmlElement.Parent = null;
							contentFromACBlock.Parent = null;
							while (contentFromACBlock.FirstChild != null)
							{
								OpenXmlElement firstChild2 = contentFromACBlock.FirstChild;
								firstChild2.Remove();
								this.AddANode(firstChild2);
								firstChild2.CheckMustUnderstandAttr();
							}
						}
						break;
					}
					}
				}
			}
			else
			{
				xmlReader.Skip();
			}
			base.RawOuterXml = string.Empty;
		}

		// Token: 0x0600CEFE RID: 52990 RVA: 0x00293B83 File Offset: 0x00291D83
		private static void RemoveUnnecessaryExtAttr(OpenXmlElement node, OpenXmlElement newnode)
		{
			node.MakeSureParsed();
			if (newnode.NamespaceDeclField != null && node.NamespaceDeclField != null)
			{
				newnode.NamespaceDeclField = new List<KeyValuePair<string, string>>(node.NamespaceDeclField);
			}
		}

		// Token: 0x170031B8 RID: 12728
		// (get) Token: 0x0600CEFF RID: 52991 RVA: 0x000020FA File Offset: 0x000002FA
		internal virtual string[] ElementTagNames
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170031B9 RID: 12729
		// (get) Token: 0x0600CF00 RID: 52992 RVA: 0x000020FA File Offset: 0x000002FA
		internal virtual byte[] ElementNamespaceIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600CF01 RID: 52993 RVA: 0x00293BAC File Offset: 0x00291DAC
		private int GetSequenceNumber(OpenXmlElement child)
		{
			for (int i = 0; i < this.ElementNamespaceIds.Length; i++)
			{
				if (this.ElementNamespaceIds[i] == child.NamespaceId && object.Equals(this.ElementTagNames[i], child.LocalName))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600CF02 RID: 52994 RVA: 0x00293BF4 File Offset: 0x00291DF4
		internal T GetElement<T>(int sequenceNumber) where T : OpenXmlElement
		{
			switch (this.OpenXmlCompositeType)
			{
			case OpenXmlCompositeType.Other:
				throw new InvalidOperationException();
			case OpenXmlCompositeType.OneSequence:
			{
				OpenXmlElement openXmlElement = this.FirstChild;
				while (openXmlElement != null)
				{
					if (OpenXmlElement.IsKnownElement(openXmlElement))
					{
						int sequenceNumber2 = this.GetSequenceNumber(openXmlElement);
						if (sequenceNumber2 == sequenceNumber)
						{
							T t = openXmlElement as T;
							if (t != null)
							{
								return t;
							}
							openXmlElement = openXmlElement.NextSibling();
						}
						else
						{
							if (sequenceNumber2 > sequenceNumber)
							{
								return default(T);
							}
							openXmlElement = openXmlElement.NextSibling();
						}
					}
					else
					{
						openXmlElement = openXmlElement.NextSibling();
					}
				}
				goto IL_013F;
			}
			case OpenXmlCompositeType.OneChoice:
				break;
			case OpenXmlCompositeType.OneAll:
			{
				using (IEnumerator<OpenXmlElement> enumerator = this.ChildElements.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						OpenXmlElement openXmlElement2 = enumerator.Current;
						if (OpenXmlElement.IsKnownElement(openXmlElement2))
						{
							int sequenceNumber3 = this.GetSequenceNumber(openXmlElement2);
							if (sequenceNumber3 == sequenceNumber)
							{
								T t = openXmlElement2 as T;
								if (t != null)
								{
									return t;
								}
							}
						}
					}
					goto IL_013F;
				}
				break;
			}
			default:
				goto IL_013F;
			}
			OpenXmlElement openXmlElement3 = this.FirstChild;
			while (openXmlElement3 != null && !OpenXmlElement.IsKnownElement(openXmlElement3))
			{
				openXmlElement3 = openXmlElement3.NextSibling();
			}
			if (openXmlElement3 != null)
			{
				int sequenceNumber4 = this.GetSequenceNumber(openXmlElement3);
				if (sequenceNumber4 == sequenceNumber)
				{
					T t = openXmlElement3 as T;
					if (t != null)
					{
						return t;
					}
				}
			}
			IL_013F:
			return default(T);
		}

		// Token: 0x0600CF03 RID: 52995 RVA: 0x00293D60 File Offset: 0x00291F60
		internal void SetElement<T>(int sequenceNumber, T newChild) where T : OpenXmlElement
		{
			switch (this.OpenXmlCompositeType)
			{
			case OpenXmlCompositeType.Other:
				throw new InvalidOperationException();
			case OpenXmlCompositeType.OneSequence:
			{
				OpenXmlElement openXmlElement = this.FirstChild;
				OpenXmlElement openXmlElement2 = null;
				while (openXmlElement != null)
				{
					if (OpenXmlElement.IsKnownElement(openXmlElement))
					{
						int sequenceNumber2 = this.GetSequenceNumber(openXmlElement);
						if (sequenceNumber2 == sequenceNumber)
						{
							if (openXmlElement is T)
							{
								openXmlElement2 = openXmlElement.PreviousSibling();
								this.RemoveChild<OpenXmlElement>(openXmlElement);
								break;
							}
							openXmlElement2 = openXmlElement;
						}
						else
						{
							if (sequenceNumber2 > sequenceNumber)
							{
								break;
							}
							openXmlElement2 = openXmlElement;
						}
					}
					openXmlElement = openXmlElement.NextSibling();
				}
				if (newChild != null)
				{
					this.InsertAfter<T>(newChild, openXmlElement2);
				}
				break;
			}
			case OpenXmlCompositeType.OneChoice:
			{
				OpenXmlElement openXmlElement3 = this.FirstChild;
				OpenXmlElement openXmlElement4 = null;
				while (openXmlElement3 != null && !OpenXmlElement.IsKnownElement(openXmlElement3))
				{
					openXmlElement4 = openXmlElement3;
					openXmlElement3 = openXmlElement3.NextSibling();
				}
				while (openXmlElement3 != null)
				{
					OpenXmlElement openXmlElement5 = openXmlElement3.NextSibling();
					if (OpenXmlElement.IsKnownElement(openXmlElement3))
					{
						this.RemoveChild<OpenXmlElement>(openXmlElement3);
					}
					openXmlElement3 = openXmlElement5;
				}
				if (newChild != null)
				{
					this.InsertAfter<T>(newChild, openXmlElement4);
					return;
				}
				break;
			}
			case OpenXmlCompositeType.OneAll:
			{
				T element = this.GetElement<T>(sequenceNumber);
				if (element != null)
				{
					this.RemoveChild<T>(element);
				}
				if (newChild != null)
				{
					this.AppendChild<T>(newChild);
					return;
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x170031BA RID: 12730
		// (get) Token: 0x0600CF04 RID: 52996 RVA: 0x00002105 File Offset: 0x00000305
		internal virtual OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.Other;
			}
		}

		// Token: 0x0600CF05 RID: 52997 RVA: 0x00293E80 File Offset: 0x00292080
		private void AddANode(OpenXmlElement node)
		{
			node.Parent = this;
			if (this._lastChild == null)
			{
				node.next = node;
				this._lastChild = node;
				return;
			}
			node.next = this._lastChild.next;
			this._lastChild.next = node;
			this._lastChild = node;
		}

		// Token: 0x04006868 RID: 26728
		private OpenXmlElement _lastChild;
	}
}
