using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002145 RID: 8517
	internal class OpenXmlPartWriter : OpenXmlWriter
	{
		// Token: 0x1700331D RID: 13085
		// (get) Token: 0x0600D3B4 RID: 54196 RVA: 0x0029FBD1 File Offset: 0x0029DDD1
		private static Type OpenXmlLeafTextElementClass
		{
			get
			{
				if (OpenXmlPartWriter._openXmlLeafTextElementClass == null)
				{
					OpenXmlPartWriter._openXmlLeafTextElementClass = typeof(OpenXmlLeafTextElement);
				}
				return OpenXmlPartWriter._openXmlLeafTextElementClass;
			}
		}

		// Token: 0x0600D3B5 RID: 54197 RVA: 0x0029FBEE File Offset: 0x0029DDEE
		public OpenXmlPartWriter(OpenXmlPart openXmlPart)
			: this(openXmlPart, Encoding.UTF8)
		{
		}

		// Token: 0x0600D3B6 RID: 54198 RVA: 0x0029FBFC File Offset: 0x0029DDFC
		public OpenXmlPartWriter(OpenXmlPart openXmlPart, Encoding encoding)
		{
			if (openXmlPart == null)
			{
				throw new ArgumentNullException("openXmlPart");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			Stream stream = openXmlPart.GetStream(FileMode.Create);
			this.Init(stream, true, encoding);
		}

		// Token: 0x0600D3B7 RID: 54199 RVA: 0x0029FC3C File Offset: 0x0029DE3C
		public OpenXmlPartWriter(Stream partStream)
			: this(partStream, Encoding.UTF8)
		{
		}

		// Token: 0x0600D3B8 RID: 54200 RVA: 0x0029FC4A File Offset: 0x0029DE4A
		public OpenXmlPartWriter(Stream partStream, Encoding encoding)
		{
			if (partStream == null)
			{
				throw new ArgumentNullException("partStream");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			this.Init(partStream, false, encoding);
		}

		// Token: 0x0600D3B9 RID: 54201 RVA: 0x0029FC77 File Offset: 0x0029DE77
		public override void WriteStartDocument()
		{
			this.ThrowIfObjectDisposed();
			this._xmlWriter.WriteStartDocument();
		}

		// Token: 0x0600D3BA RID: 54202 RVA: 0x0029FC8A File Offset: 0x0029DE8A
		public override void WriteStartDocument(bool standalone)
		{
			this.ThrowIfObjectDisposed();
			this._xmlWriter.WriteStartDocument(standalone);
		}

		// Token: 0x0600D3BB RID: 54203 RVA: 0x0029FC9E File Offset: 0x0029DE9E
		public override void WriteStartElement(OpenXmlReader elementReader)
		{
			this.WriteStartElement(elementReader, elementReader.Attributes, elementReader.NamespaceDeclarations);
		}

		// Token: 0x0600D3BC RID: 54204 RVA: 0x0029FCB3 File Offset: 0x0029DEB3
		public override void WriteStartElement(OpenXmlReader elementReader, IEnumerable<OpenXmlAttribute> attributes)
		{
			this.WriteStartElement(elementReader, attributes, elementReader.NamespaceDeclarations);
		}

		// Token: 0x0600D3BD RID: 54205 RVA: 0x0029FCC4 File Offset: 0x0029DEC4
		public override void WriteStartElement(OpenXmlReader elementReader, IEnumerable<OpenXmlAttribute> attributes, IEnumerable<KeyValuePair<string, string>> namespaceDeclarations)
		{
			if (elementReader == null)
			{
				throw new ArgumentNullException("elementReader");
			}
			if (elementReader.IsEndElement)
			{
				throw new ArgumentOutOfRangeException("elementReader");
			}
			if (elementReader.IsMiscNode)
			{
				throw new ArgumentOutOfRangeException("elementReader");
			}
			this.ThrowIfObjectDisposed();
			this._xmlWriter.WriteStartElement(elementReader.Prefix, elementReader.LocalName, elementReader.NamespaceUri);
			if (namespaceDeclarations != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in namespaceDeclarations)
				{
					this._xmlWriter.WriteAttributeString("xmlns", keyValuePair.Key, "http://www.w3.org/2000/xmlns/", keyValuePair.Value);
				}
			}
			if (attributes != null)
			{
				foreach (OpenXmlAttribute openXmlAttribute in attributes)
				{
					this._xmlWriter.WriteAttributeString(openXmlAttribute.Prefix, openXmlAttribute.LocalName, openXmlAttribute.NamespaceUri, openXmlAttribute.Value);
				}
			}
			if (OpenXmlPartWriter.IsOpenXmlLeafTextElement(elementReader.ElementType))
			{
				this._isLeafTextElementStart = true;
				return;
			}
			this._isLeafTextElementStart = false;
		}

		// Token: 0x0600D3BE RID: 54206 RVA: 0x0029FDF8 File Offset: 0x0029DFF8
		public override void WriteStartElement(OpenXmlElement elementObject)
		{
			if (elementObject == null)
			{
				throw new ArgumentNullException("elementObject");
			}
			if (elementObject is OpenXmlMiscNode)
			{
				throw new ArgumentOutOfRangeException("elementObject");
			}
			this.ThrowIfObjectDisposed();
			this._xmlWriter.WriteStartElement(elementObject.Prefix, elementObject.LocalName, elementObject.NamespaceUri);
			if (elementObject.HasAttributes)
			{
				foreach (OpenXmlAttribute openXmlAttribute in elementObject.GetAttributes())
				{
					this._xmlWriter.WriteAttributeString(openXmlAttribute.Prefix, openXmlAttribute.LocalName, openXmlAttribute.NamespaceUri, openXmlAttribute.Value);
				}
			}
			if (OpenXmlPartWriter.IsOpenXmlLeafTextElement(elementObject))
			{
				this._isLeafTextElementStart = true;
				return;
			}
			this._isLeafTextElementStart = false;
		}

		// Token: 0x0600D3BF RID: 54207 RVA: 0x0029FEC8 File Offset: 0x0029E0C8
		public override void WriteStartElement(OpenXmlElement elementObject, IEnumerable<OpenXmlAttribute> attributes)
		{
			this.WriteStartElement(elementObject, attributes, elementObject.NamespaceDeclarations);
		}

		// Token: 0x0600D3C0 RID: 54208 RVA: 0x0029FED8 File Offset: 0x0029E0D8
		public override void WriteStartElement(OpenXmlElement elementObject, IEnumerable<OpenXmlAttribute> attributes, IEnumerable<KeyValuePair<string, string>> namespaceDeclarations)
		{
			if (elementObject == null)
			{
				throw new ArgumentNullException("elementObject");
			}
			if (elementObject is OpenXmlMiscNode)
			{
				throw new ArgumentOutOfRangeException("elementObject");
			}
			this.ThrowIfObjectDisposed();
			this._xmlWriter.WriteStartElement(elementObject.Prefix, elementObject.LocalName, elementObject.NamespaceUri);
			if (namespaceDeclarations != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in namespaceDeclarations)
				{
					this._xmlWriter.WriteAttributeString("xmlns", keyValuePair.Key, "http://www.w3.org/2000/xmlns/", keyValuePair.Value);
				}
			}
			if (attributes != null)
			{
				foreach (OpenXmlAttribute openXmlAttribute in attributes)
				{
					this._xmlWriter.WriteAttributeString(openXmlAttribute.Prefix, openXmlAttribute.LocalName, openXmlAttribute.NamespaceUri, openXmlAttribute.Value);
				}
			}
			if (OpenXmlPartWriter.IsOpenXmlLeafTextElement(elementObject))
			{
				this._isLeafTextElementStart = true;
				return;
			}
			this._isLeafTextElementStart = false;
		}

		// Token: 0x0600D3C1 RID: 54209 RVA: 0x0029FFF4 File Offset: 0x0029E1F4
		public override void WriteEndElement()
		{
			this.ThrowIfObjectDisposed();
			this._xmlWriter.WriteEndElement();
			this._isLeafTextElementStart = false;
		}

		// Token: 0x0600D3C2 RID: 54210 RVA: 0x002A000E File Offset: 0x0029E20E
		public override void WriteString(string text)
		{
			this.ThrowIfObjectDisposed();
			if (this._isLeafTextElementStart)
			{
				this._xmlWriter.WriteString(text);
				return;
			}
			throw new InvalidOperationException(ExceptionMessages.InvalidWriteStringCall);
		}

		// Token: 0x0600D3C3 RID: 54211 RVA: 0x002A0035 File Offset: 0x0029E235
		public override void WriteElement(OpenXmlElement elementObject)
		{
			if (elementObject == null)
			{
				throw new ArgumentNullException("elementObject");
			}
			this.ThrowIfObjectDisposed();
			elementObject.WriteTo(this._xmlWriter);
			this._isLeafTextElementStart = false;
		}

		// Token: 0x0600D3C4 RID: 54212 RVA: 0x002A005E File Offset: 0x0029E25E
		public override void Close()
		{
			if (this._xmlWriter != null)
			{
				this._xmlWriter.Close();
			}
			this._isLeafTextElementStart = false;
		}

		// Token: 0x0600D3C5 RID: 54213 RVA: 0x002A007C File Offset: 0x0029E27C
		private void Init(Stream partStream, bool closeOutput, Encoding encoding)
		{
			this._xmlWriter = XmlWriter.Create(partStream, new XmlWriterSettings
			{
				CloseOutput = closeOutput,
				Encoding = encoding
			});
		}

		// Token: 0x0600D3C6 RID: 54214 RVA: 0x002A00AA File Offset: 0x0029E2AA
		private static bool IsOpenXmlLeafTextElement(Type elementType)
		{
			return elementType.IsSubclassOf(OpenXmlPartWriter.OpenXmlLeafTextElementClass);
		}

		// Token: 0x0600D3C7 RID: 54215 RVA: 0x002A00B7 File Offset: 0x0029E2B7
		private static bool IsOpenXmlLeafTextElement(OpenXmlElement element)
		{
			return element is OpenXmlLeafTextElement;
		}

		// Token: 0x040069A0 RID: 27040
		private static Type _openXmlLeafTextElementClass;

		// Token: 0x040069A1 RID: 27041
		private XmlWriter _xmlWriter;

		// Token: 0x040069A2 RID: 27042
		private bool _isLeafTextElementStart;
	}
}
