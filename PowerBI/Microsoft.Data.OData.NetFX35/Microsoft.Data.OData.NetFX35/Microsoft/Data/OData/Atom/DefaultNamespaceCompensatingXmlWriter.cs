using System;
using System.Xml;
using System.Xml.XPath;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020000E8 RID: 232
	internal sealed class DefaultNamespaceCompensatingXmlWriter : XmlWriter
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x000140BD File Offset: 0x000122BD
		internal DefaultNamespaceCompensatingXmlWriter(XmlWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x000140CC File Offset: 0x000122CC
		public override string XmlLang
		{
			get
			{
				return this.writer.XmlLang;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x000140D9 File Offset: 0x000122D9
		public override WriteState WriteState
		{
			get
			{
				return this.writer.WriteState;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x000140E6 File Offset: 0x000122E6
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.writer.XmlSpace;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x000140F3 File Offset: 0x000122F3
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.writer.Settings;
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00014100 File Offset: 0x00012300
		public override void WriteNode(XPathNavigator navigator, bool defattr)
		{
			this.writer.WriteNode(navigator, defattr);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001410F File Offset: 0x0001230F
		public override void WriteNode(XmlReader reader, bool defattr)
		{
			this.writer.WriteNode(reader, defattr);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001411E File Offset: 0x0001231E
		public override void WriteAttributes(XmlReader reader, bool defattr)
		{
			this.writer.WriteAttributes(reader, defattr);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001412D File Offset: 0x0001232D
		public override string LookupPrefix(string ns)
		{
			return this.writer.LookupPrefix(ns);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001413B File Offset: 0x0001233B
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00014148 File Offset: 0x00012348
		public override void WriteNmToken(string name)
		{
			this.writer.WriteNmToken(name);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00014156 File Offset: 0x00012356
		public override void Close()
		{
			this.writer.Close();
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00014163 File Offset: 0x00012363
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.writer.WriteBinHex(buffer, index, count);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00014173 File Offset: 0x00012373
		public override void WriteRaw(string data)
		{
			this.writer.WriteRaw(data);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00014181 File Offset: 0x00012381
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.writer.WriteBase64(buffer, index, count);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00014191 File Offset: 0x00012391
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.writer.WriteRaw(buffer, index, count);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000141A1 File Offset: 0x000123A1
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.writer.WriteChars(buffer, index, count);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000141B1 File Offset: 0x000123B1
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.writer.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000141C0 File Offset: 0x000123C0
		public override void WriteString(string text)
		{
			this.writer.WriteString(text);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000141CE File Offset: 0x000123CE
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.writer.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000141DE File Offset: 0x000123DE
		public override void WriteEndAttribute()
		{
			this.writer.WriteEndAttribute();
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000141EB File Offset: 0x000123EB
		public override void WriteCData(string text)
		{
			this.writer.WriteCData(text);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000141F9 File Offset: 0x000123F9
		public override void WriteComment(string text)
		{
			this.writer.WriteComment(text);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00014207 File Offset: 0x00012407
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.writer.WriteProcessingInstruction(name, text);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00014216 File Offset: 0x00012416
		public override void WriteEntityRef(string name)
		{
			this.writer.WriteEntityRef(name);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00014224 File Offset: 0x00012424
		public override void WriteCharEntity(char ch)
		{
			this.writer.WriteCharEntity(ch);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00014232 File Offset: 0x00012432
		public override void WriteWhitespace(string ws)
		{
			this.writer.WriteWhitespace(ws);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00014240 File Offset: 0x00012440
		public override void WriteStartDocument()
		{
			this.writer.WriteStartDocument();
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001424D File Offset: 0x0001244D
		public override void WriteStartDocument(bool standalone)
		{
			this.writer.WriteStartDocument(standalone);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001425B File Offset: 0x0001245B
		public override void WriteEndDocument()
		{
			this.writer.WriteEndDocument();
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00014268 File Offset: 0x00012468
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.writer.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001427A File Offset: 0x0001247A
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

		// Token: 0x060005CA RID: 1482 RVA: 0x000142B7 File Offset: 0x000124B7
		public override void WriteEndElement()
		{
			this.writer.WriteEndElement();
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000142C4 File Offset: 0x000124C4
		public override void WriteFullEndElement()
		{
			this.writer.WriteFullEndElement();
		}

		// Token: 0x04000262 RID: 610
		private readonly XmlWriter writer;

		// Token: 0x04000263 RID: 611
		private string rootPrefix;
	}
}
