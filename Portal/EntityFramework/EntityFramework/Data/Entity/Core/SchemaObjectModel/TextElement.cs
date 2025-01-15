using System;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000320 RID: 800
	internal sealed class TextElement : SchemaElement
	{
		// Token: 0x06002611 RID: 9745 RVA: 0x0006CDC9 File Offset: 0x0006AFC9
		public TextElement(SchemaElement parentElement)
			: base(parentElement, null)
		{
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002612 RID: 9746 RVA: 0x0006CDD3 File Offset: 0x0006AFD3
		// (set) Token: 0x06002613 RID: 9747 RVA: 0x0006CDDB File Offset: 0x0006AFDB
		public string Value { get; private set; }

		// Token: 0x06002614 RID: 9748 RVA: 0x0006CDE4 File Offset: 0x0006AFE4
		protected override bool HandleText(XmlReader reader)
		{
			this.TextElementTextHandler(reader);
			return true;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x0006CDF0 File Offset: 0x0006AFF0
		private void TextElementTextHandler(XmlReader reader)
		{
			string value = reader.Value;
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			if (string.IsNullOrEmpty(this.Value))
			{
				this.Value = value;
				return;
			}
			this.Value += value;
		}
	}
}
