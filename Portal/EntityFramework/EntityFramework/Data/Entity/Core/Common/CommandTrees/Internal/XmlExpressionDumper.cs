using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x020006F5 RID: 1781
	internal class XmlExpressionDumper : ExpressionDumper
	{
		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x060052CA RID: 21194 RVA: 0x00129F77 File Offset: 0x00128177
		internal static Encoding DefaultEncoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x060052CB RID: 21195 RVA: 0x00129F7E File Offset: 0x0012817E
		internal XmlExpressionDumper(Stream stream)
			: this(stream, XmlExpressionDumper.DefaultEncoding)
		{
		}

		// Token: 0x060052CC RID: 21196 RVA: 0x00129F8C File Offset: 0x0012818C
		internal XmlExpressionDumper(Stream stream, Encoding encoding)
		{
			this._writer = XmlWriter.Create(stream, new XmlWriterSettings
			{
				CheckCharacters = false,
				Indent = true,
				Encoding = encoding
			});
			this._writer.WriteStartDocument(true);
		}

		// Token: 0x060052CD RID: 21197 RVA: 0x00129FD3 File Offset: 0x001281D3
		internal void Close()
		{
			this._writer.WriteEndDocument();
			this._writer.Flush();
			this._writer.Close();
		}

		// Token: 0x060052CE RID: 21198 RVA: 0x00129FF8 File Offset: 0x001281F8
		internal override void Begin(string name, Dictionary<string, object> attrs)
		{
			this._writer.WriteStartElement(name);
			if (attrs != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in attrs)
				{
					this._writer.WriteAttributeString(keyValuePair.Key, (keyValuePair.Value == null) ? "" : keyValuePair.Value.ToString());
				}
			}
		}

		// Token: 0x060052CF RID: 21199 RVA: 0x0012A07C File Offset: 0x0012827C
		internal override void End(string name)
		{
			this._writer.WriteEndElement();
		}

		// Token: 0x04001DED RID: 7661
		private readonly XmlWriter _writer;
	}
}
