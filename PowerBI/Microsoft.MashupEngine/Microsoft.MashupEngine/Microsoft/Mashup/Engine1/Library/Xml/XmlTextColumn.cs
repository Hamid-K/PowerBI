using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Xml
{
	// Token: 0x0200029C RID: 668
	internal class XmlTextColumn : XmlColumn
	{
		// Token: 0x06001AED RID: 6893 RVA: 0x000371BC File Offset: 0x000353BC
		public XmlTextColumn(string name, string text)
			: base(name)
		{
			this.text = text;
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x000371CC File Offset: 0x000353CC
		public override Value Reduce(XmlTableOptions options)
		{
			return TextValue.New(this.text).NewType(DataSource.SerializedTextType);
		}

		// Token: 0x04000820 RID: 2080
		private string text;
	}
}
