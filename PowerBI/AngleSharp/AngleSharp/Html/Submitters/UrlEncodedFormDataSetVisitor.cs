using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AngleSharp.Dom.Io;
using AngleSharp.Extensions;

namespace AngleSharp.Html.Submitters
{
	// Token: 0x020000C6 RID: 198
	internal sealed class UrlEncodedFormDataSetVisitor : IFormSubmitter, IFormDataSetVisitor
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x0002E80C File Offset: 0x0002CA0C
		public UrlEncodedFormDataSetVisitor(Encoding encoding)
		{
			this._encoding = encoding;
			this._lines = new List<string>();
			this._first = true;
			this._index = string.Empty;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0002E838 File Offset: 0x0002CA38
		public void Text(FormDataSetEntry entry, string value)
		{
			if (this._first && entry.HasName && entry.Name.Is(TagNames.IsIndex) && entry.Type.Isi(InputTypeNames.Text))
			{
				this._index = value ?? string.Empty;
			}
			else if (entry.HasName && value != null)
			{
				byte[] bytes = this._encoding.GetBytes(entry.Name);
				byte[] bytes2 = this._encoding.GetBytes(value);
				this.Add(bytes, bytes2);
			}
			this._first = false;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0002E8C4 File Offset: 0x0002CAC4
		public void File(FormDataSetEntry entry, string fileName, string contentType, IFile content)
		{
			if (entry.HasName && content != null && content.Name != null)
			{
				byte[] bytes = this._encoding.GetBytes(entry.Name);
				byte[] bytes2 = this._encoding.GetBytes(content.Name);
				this.Add(bytes, bytes2);
			}
			this._first = false;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0002E91C File Offset: 0x0002CB1C
		public void Serialize(StreamWriter stream)
		{
			string text = string.Join("&", this._lines);
			stream.Write(this._index);
			stream.Write(text);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0002E94D File Offset: 0x0002CB4D
		private void Add(byte[] name, byte[] value)
		{
			this._lines.Add(name.UrlEncode() + "=" + value.UrlEncode());
		}

		// Token: 0x040005ED RID: 1517
		private readonly Encoding _encoding;

		// Token: 0x040005EE RID: 1518
		private readonly List<string> _lines;

		// Token: 0x040005EF RID: 1519
		private bool _first;

		// Token: 0x040005F0 RID: 1520
		private string _index;
	}
}
