using System;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000055 RID: 85
	internal sealed class TextElement : SchemaElement
	{
		// Token: 0x06000880 RID: 2176 RVA: 0x000120C1 File Offset: 0x000102C1
		public TextElement(SchemaElement parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x000120CA File Offset: 0x000102CA
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x000120D2 File Offset: 0x000102D2
		public string Value
		{
			get
			{
				return this._value;
			}
			private set
			{
				this._value = value;
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000120DB File Offset: 0x000102DB
		protected override bool HandleText(XmlReader reader)
		{
			this.TextElementTextHandler(reader);
			return true;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000120E8 File Offset: 0x000102E8
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

		// Token: 0x040006D0 RID: 1744
		private string _value;
	}
}
