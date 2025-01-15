using System;
using System.Xml;

namespace Microsoft.Reporting
{
	// Token: 0x020000CD RID: 205
	internal abstract class XmlWriterWrapper : XmlWriter
	{
		// Token: 0x06000D23 RID: 3363 RVA: 0x00021E2D File Offset: 0x0002002D
		protected XmlWriterWrapper(XmlWriter xw)
		{
			this._xw = ArgumentValidation.CheckNotNull<XmlWriter>(xw, "xw");
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x00021E46 File Offset: 0x00020046
		public override XmlWriterSettings Settings
		{
			get
			{
				return this._xw.Settings;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00021E53 File Offset: 0x00020053
		public override WriteState WriteState
		{
			get
			{
				return this._xw.WriteState;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00021E60 File Offset: 0x00020060
		public override string XmlLang
		{
			get
			{
				return this._xw.XmlLang;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x00021E6D File Offset: 0x0002006D
		public override XmlSpace XmlSpace
		{
			get
			{
				return this._xw.XmlSpace;
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00021E7A File Offset: 0x0002007A
		public override void Flush()
		{
			this._xw.Flush();
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00021E87 File Offset: 0x00020087
		public override string LookupPrefix(string ns)
		{
			return this._xw.LookupPrefix(ns);
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00021E95 File Offset: 0x00020095
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this._xw.WriteBase64(buffer, index, count);
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00021EA5 File Offset: 0x000200A5
		public override void WriteCData(string text)
		{
			this._xw.WriteCData(text);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00021EB3 File Offset: 0x000200B3
		public override void WriteCharEntity(char ch)
		{
			this._xw.WriteCharEntity(ch);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00021EC1 File Offset: 0x000200C1
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this._xw.WriteChars(buffer, index, count);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00021ED1 File Offset: 0x000200D1
		public override void WriteComment(string text)
		{
			this._xw.WriteComment(text);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00021EDF File Offset: 0x000200DF
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this._xw.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00021EF1 File Offset: 0x000200F1
		public override void WriteEndAttribute()
		{
			this._xw.WriteEndAttribute();
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00021EFE File Offset: 0x000200FE
		public override void WriteEndDocument()
		{
			this._xw.WriteEndDocument();
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00021F0B File Offset: 0x0002010B
		public override void WriteEndElement()
		{
			this._xw.WriteEndElement();
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00021F18 File Offset: 0x00020118
		public override void WriteEntityRef(string name)
		{
			this._xw.WriteEntityRef(name);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00021F26 File Offset: 0x00020126
		public override void WriteFullEndElement()
		{
			this._xw.WriteFullEndElement();
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00021F33 File Offset: 0x00020133
		public override void WriteProcessingInstruction(string name, string text)
		{
			this._xw.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00021F42 File Offset: 0x00020142
		public override void WriteQualifiedName(string localName, string ns)
		{
			this._xw.WriteQualifiedName(localName, ns);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00021F51 File Offset: 0x00020151
		public override void WriteRaw(string data)
		{
			this._xw.WriteRaw(data);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00021F5F File Offset: 0x0002015F
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this._xw.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00021F6F File Offset: 0x0002016F
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this._xw.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00021F7F File Offset: 0x0002017F
		public override void WriteStartDocument(bool standalone)
		{
			this._xw.WriteStartDocument(standalone);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00021F8D File Offset: 0x0002018D
		public override void WriteStartDocument()
		{
			this._xw.WriteStartDocument();
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00021F9A File Offset: 0x0002019A
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this._xw.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00021FAA File Offset: 0x000201AA
		public override void WriteString(string text)
		{
			this._xw.WriteString(text);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00021FB8 File Offset: 0x000201B8
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this._xw.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00021FC7 File Offset: 0x000201C7
		public override void WriteWhitespace(string ws)
		{
			this._xw.WriteWhitespace(ws);
		}

		// Token: 0x04000973 RID: 2419
		private readonly XmlWriter _xw;
	}
}
