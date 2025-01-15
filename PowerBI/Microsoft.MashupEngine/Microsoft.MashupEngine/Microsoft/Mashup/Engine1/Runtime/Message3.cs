using System;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001698 RID: 5784
	internal struct Message3 : IUserMessage
	{
		// Token: 0x0600925C RID: 37468 RVA: 0x001E55ED File Offset: 0x001E37ED
		public Message3(string message, object arg0, object arg1, object arg2)
		{
			this.message = message;
			this.arg0 = arg0;
			this.arg1 = arg1;
			this.arg2 = arg2;
		}

		// Token: 0x0600925D RID: 37469 RVA: 0x001E560C File Offset: 0x001E380C
		public static implicit operator string(Message3 message)
		{
			return message.ToString();
		}

		// Token: 0x0600925E RID: 37470 RVA: 0x001E561B File Offset: 0x001E381B
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, this.message, this.arg0, this.arg1, this.arg2);
		}

		// Token: 0x17002674 RID: 9844
		// (get) Token: 0x0600925F RID: 37471 RVA: 0x001E563F File Offset: 0x001E383F
		public TextValue Message
		{
			get
			{
				return TextValue.New(ValueException2.NonCSharpFormatSpecifierRegex.Replace(ValueException2.CSharpFormatSpecifierRegex.Replace(this.message, "$1#$2"), "$1$3"));
			}
		}

		// Token: 0x17002675 RID: 9845
		// (get) Token: 0x06009260 RID: 37472 RVA: 0x001E566A File Offset: 0x001E386A
		public ListValue Parameters
		{
			get
			{
				return ListValue.New(new Value[]
				{
					MessageUtils.MarshalValue(this.arg0),
					MessageUtils.MarshalValue(this.arg1),
					MessageUtils.MarshalValue(this.arg2)
				});
			}
		}

		// Token: 0x04004E7E RID: 20094
		private readonly string message;

		// Token: 0x04004E7F RID: 20095
		private readonly object arg0;

		// Token: 0x04004E80 RID: 20096
		private readonly object arg1;

		// Token: 0x04004E81 RID: 20097
		private readonly object arg2;
	}
}
