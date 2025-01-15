using System;
using System.Collections.Generic;
using System.IO;
using AngleSharp.Dom.Io;

namespace AngleSharp.Html.Submitters
{
	// Token: 0x020000C5 RID: 197
	internal sealed class PlaintextFormDataSetVisitor : IFormSubmitter, IFormDataSetVisitor
	{
		// Token: 0x060005C6 RID: 1478 RVA: 0x0002E774 File Offset: 0x0002C974
		public PlaintextFormDataSetVisitor()
		{
			this._lines = new List<string>();
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0002E787 File Offset: 0x0002C987
		public void Text(FormDataSetEntry entry, string value)
		{
			if (entry.HasName && value != null)
			{
				this.Add(entry.Name, value);
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0002E7A1 File Offset: 0x0002C9A1
		public void File(FormDataSetEntry entry, string fileName, string contentType, IFile content)
		{
			if (entry.HasName && content != null && content.Name != null)
			{
				this.Add(entry.Name, content.Name);
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0002E7CC File Offset: 0x0002C9CC
		public void Serialize(StreamWriter stream)
		{
			string text = string.Join(Symbols.NewLines[0], this._lines);
			stream.Write(text);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0002E7F3 File Offset: 0x0002C9F3
		private void Add(string name, string value)
		{
			this._lines.Add(name + "=" + value);
		}

		// Token: 0x040005EC RID: 1516
		private readonly List<string> _lines;
	}
}
