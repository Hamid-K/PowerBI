using System;
using System.IO;
using System.Text;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200214F RID: 8527
	internal class XmlDOMTextWriter : XmlTextWriter
	{
		// Token: 0x0600D3E9 RID: 54249 RVA: 0x002A1BFB File Offset: 0x0029FDFB
		public XmlDOMTextWriter(TextWriter w)
			: base(w)
		{
		}

		// Token: 0x0600D3EA RID: 54250 RVA: 0x002A1C04 File Offset: 0x0029FE04
		public XmlDOMTextWriter(Stream w, Encoding encoding)
			: base(w, encoding)
		{
		}

		// Token: 0x0600D3EB RID: 54251 RVA: 0x002A1C0E File Offset: 0x0029FE0E
		public XmlDOMTextWriter(string filename, Encoding encoding)
			: base(filename, encoding)
		{
		}

		// Token: 0x0600D3EC RID: 54252 RVA: 0x002A1C18 File Offset: 0x0029FE18
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (string.IsNullOrEmpty(localName))
			{
				throw new ArgumentNullException("localName");
			}
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			if (ns.Length == 0 && prefix.Length != 0)
			{
				prefix = string.Empty;
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x0600D3ED RID: 54253 RVA: 0x002A1C6C File Offset: 0x0029FE6C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (string.IsNullOrEmpty(localName))
			{
				throw new ArgumentNullException("localName");
			}
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			if (ns.Length == 0 && prefix.Length != 0)
			{
				prefix = string.Empty;
			}
			base.WriteStartElement(prefix, localName, ns);
		}
	}
}
