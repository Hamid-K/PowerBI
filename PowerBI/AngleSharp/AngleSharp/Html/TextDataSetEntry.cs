using System;
using System.Text;

namespace AngleSharp.Html
{
	// Token: 0x020000C2 RID: 194
	internal sealed class TextDataSetEntry : FormDataSetEntry
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x0002E403 File Offset: 0x0002C603
		public TextDataSetEntry(string name, string value, string type)
			: base(name, type)
		{
			this._value = value;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0002E414 File Offset: 0x0002C614
		public override bool Contains(string boundary, Encoding encoding)
		{
			return this._value != null && this._value.Contains(boundary);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0002E42C File Offset: 0x0002C62C
		public override void Accept(IFormDataSetVisitor visitor)
		{
			visitor.Text(this, this._value);
		}

		// Token: 0x040005E6 RID: 1510
		private readonly string _value;
	}
}
