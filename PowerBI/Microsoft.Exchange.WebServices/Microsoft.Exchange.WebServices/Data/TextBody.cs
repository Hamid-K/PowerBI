using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000A2 RID: 162
	public sealed class TextBody : MessageBody
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x0001913D File Offset: 0x0001813D
		public TextBody()
		{
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00019145 File Offset: 0x00018145
		public TextBody(string text)
			: base(BodyType.Text, text)
		{
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001914F File Offset: 0x0001814F
		public new static implicit operator TextBody(string textBody)
		{
			return new TextBody(textBody);
		}
	}
}
