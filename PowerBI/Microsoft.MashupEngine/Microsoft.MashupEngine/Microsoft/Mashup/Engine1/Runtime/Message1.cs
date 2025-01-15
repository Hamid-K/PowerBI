using System;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001696 RID: 5782
	internal struct Message1 : IUserMessage
	{
		// Token: 0x06009252 RID: 37458 RVA: 0x001E54D8 File Offset: 0x001E36D8
		public Message1(string message, object arg0)
		{
			this.message = message;
			this.arg0 = arg0;
		}

		// Token: 0x06009253 RID: 37459 RVA: 0x001E54E8 File Offset: 0x001E36E8
		public static implicit operator string(Message1 message)
		{
			return message.ToString();
		}

		// Token: 0x06009254 RID: 37460 RVA: 0x001E54F7 File Offset: 0x001E36F7
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, this.message, this.arg0);
		}

		// Token: 0x17002670 RID: 9840
		// (get) Token: 0x06009255 RID: 37461 RVA: 0x001E550F File Offset: 0x001E370F
		public TextValue Message
		{
			get
			{
				return TextValue.New(ValueException2.NonCSharpFormatSpecifierRegex.Replace(ValueException2.CSharpFormatSpecifierRegex.Replace(this.message, "$1#$2"), "$1$3"));
			}
		}

		// Token: 0x17002671 RID: 9841
		// (get) Token: 0x06009256 RID: 37462 RVA: 0x001E553A File Offset: 0x001E373A
		public ListValue Parameters
		{
			get
			{
				return ListValue.New(new Value[] { MessageUtils.MarshalValue(this.arg0) });
			}
		}

		// Token: 0x04004E79 RID: 20089
		private readonly string message;

		// Token: 0x04004E7A RID: 20090
		private readonly object arg0;
	}
}
