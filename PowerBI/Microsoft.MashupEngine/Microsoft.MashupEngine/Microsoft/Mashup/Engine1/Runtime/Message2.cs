using System;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001697 RID: 5783
	internal struct Message2 : IUserMessage
	{
		// Token: 0x06009257 RID: 37463 RVA: 0x001E5555 File Offset: 0x001E3755
		public Message2(string message, object arg0, object arg1)
		{
			this.message = message;
			this.arg0 = arg0;
			this.arg1 = arg1;
		}

		// Token: 0x06009258 RID: 37464 RVA: 0x001E556C File Offset: 0x001E376C
		public static implicit operator string(Message2 message)
		{
			return message.ToString();
		}

		// Token: 0x06009259 RID: 37465 RVA: 0x001E557B File Offset: 0x001E377B
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, this.message, this.arg0, this.arg1);
		}

		// Token: 0x17002672 RID: 9842
		// (get) Token: 0x0600925A RID: 37466 RVA: 0x001E5599 File Offset: 0x001E3799
		public TextValue Message
		{
			get
			{
				return TextValue.New(ValueException2.NonCSharpFormatSpecifierRegex.Replace(ValueException2.CSharpFormatSpecifierRegex.Replace(this.message, "$1#$2"), "$1$3"));
			}
		}

		// Token: 0x17002673 RID: 9843
		// (get) Token: 0x0600925B RID: 37467 RVA: 0x001E55C4 File Offset: 0x001E37C4
		public ListValue Parameters
		{
			get
			{
				return ListValue.New(new Value[]
				{
					MessageUtils.MarshalValue(this.arg0),
					MessageUtils.MarshalValue(this.arg1)
				});
			}
		}

		// Token: 0x04004E7B RID: 20091
		private readonly string message;

		// Token: 0x04004E7C RID: 20092
		private readonly object arg0;

		// Token: 0x04004E7D RID: 20093
		private readonly object arg1;
	}
}
