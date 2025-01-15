using System;
using System.Xml;
using System.Xml.XPath;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000024 RID: 36
	internal sealed class DefaultNamespaceCompensatingXmlWriter : XmlWriter
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00004CD3 File Offset: 0x00002ED3
		internal DefaultNamespaceCompensatingXmlWriter(XmlWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00004CE2 File Offset: 0x00002EE2
		public override string XmlLang
		{
			get
			{
				return this.writer.XmlLang;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00004CEF File Offset: 0x00002EEF
		public override WriteState WriteState
		{
			get
			{
				return this.writer.WriteState;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004CFC File Offset: 0x00002EFC
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.writer.XmlSpace;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00004D09 File Offset: 0x00002F09
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.writer.Settings;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004D16 File Offset: 0x00002F16
		public override void WriteNode(XPathNavigator navigator, bool defattr)
		{
			this.writer.WriteNode(navigator, defattr);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004D25 File Offset: 0x00002F25
		public override void WriteNode(XmlReader reader, bool defattr)
		{
			this.writer.WriteNode(reader, defattr);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004D34 File Offset: 0x00002F34
		public override void WriteAttributes(XmlReader reader, bool defattr)
		{
			this.writer.WriteAttributes(reader, defattr);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004D43 File Offset: 0x00002F43
		public override string LookupPrefix(string ns)
		{
			return this.writer.LookupPrefix(ns);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004D51 File Offset: 0x00002F51
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004D5E File Offset: 0x00002F5E
		public override void WriteNmToken(string name)
		{
			this.writer.WriteNmToken(name);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004D6C File Offset: 0x00002F6C
		public override void Close()
		{
			this.writer.Close();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004D79 File Offset: 0x00002F79
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.writer.WriteBinHex(buffer, index, count);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004D89 File Offset: 0x00002F89
		public override void WriteRaw(string data)
		{
			this.writer.WriteRaw(data);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004D97 File Offset: 0x00002F97
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.writer.WriteBase64(buffer, index, count);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004DA7 File Offset: 0x00002FA7
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.writer.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004DB7 File Offset: 0x00002FB7
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.writer.WriteChars(buffer, index, count);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004DC7 File Offset: 0x00002FC7
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.writer.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004DD6 File Offset: 0x00002FD6
		public override void WriteString(string text)
		{
			this.writer.WriteString(text);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.writer.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004DF4 File Offset: 0x00002FF4
		public override void WriteEndAttribute()
		{
			this.writer.WriteEndAttribute();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004E01 File Offset: 0x00003001
		public override void WriteCData(string text)
		{
			this.writer.WriteCData(text);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004E0F File Offset: 0x0000300F
		public override void WriteComment(string text)
		{
			this.writer.WriteComment(text);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004E1D File Offset: 0x0000301D
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.writer.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004E2C File Offset: 0x0000302C
		public override void WriteEntityRef(string name)
		{
			this.writer.WriteEntityRef(name);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004E3A File Offset: 0x0000303A
		public override void WriteCharEntity(char ch)
		{
			this.writer.WriteCharEntity(ch);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004E48 File Offset: 0x00003048
		public override void WriteWhitespace(string ws)
		{
			this.writer.WriteWhitespace(ws);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004E56 File Offset: 0x00003056
		public override void WriteStartDocument()
		{
			this.writer.WriteStartDocument();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004E63 File Offset: 0x00003063
		public override void WriteStartDocument(bool standalone)
		{
			this.writer.WriteStartDocument(standalone);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004E71 File Offset: 0x00003071
		public override void WriteEndDocument()
		{
			this.writer.WriteEndDocument();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004E7E File Offset: 0x0000307E
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.writer.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004E90 File Offset: 0x00003090
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (this.rootPrefix == null)
			{
				this.rootPrefix = prefix;
				prefix = string.Empty;
			}
			else if (this.rootPrefix == prefix)
			{
				prefix = string.Empty;
			}
			this.writer.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004ECD File Offset: 0x000030CD
		public override void WriteEndElement()
		{
			this.writer.WriteEndElement();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004EDA File Offset: 0x000030DA
		public override void WriteFullEndElement()
		{
			this.writer.WriteFullEndElement();
		}

		// Token: 0x04000105 RID: 261
		private readonly XmlWriter writer;

		// Token: 0x04000106 RID: 262
		private string rootPrefix;
	}
}
