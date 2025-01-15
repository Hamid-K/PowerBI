using System;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001699 RID: 5785
	internal struct Message4 : IUserMessage
	{
		// Token: 0x06009261 RID: 37473 RVA: 0x001E56A1 File Offset: 0x001E38A1
		public Message4(string message, object[] args)
		{
			this.message = message;
			this.args = args;
		}

		// Token: 0x06009262 RID: 37474 RVA: 0x001E56B1 File Offset: 0x001E38B1
		public static implicit operator string(Message4 message)
		{
			return message.ToString();
		}

		// Token: 0x06009263 RID: 37475 RVA: 0x001E56C0 File Offset: 0x001E38C0
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, this.message, this.args);
		}

		// Token: 0x17002676 RID: 9846
		// (get) Token: 0x06009264 RID: 37476 RVA: 0x001E56D8 File Offset: 0x001E38D8
		public TextValue Message
		{
			get
			{
				return TextValue.New(ValueException2.NonCSharpFormatSpecifierRegex.Replace(ValueException2.CSharpFormatSpecifierRegex.Replace(this.message, "$1#$2"), "$1$3"));
			}
		}

		// Token: 0x17002677 RID: 9847
		// (get) Token: 0x06009265 RID: 37477 RVA: 0x001E5703 File Offset: 0x001E3903
		public ListValue Parameters
		{
			get
			{
				return ListValue.New(MessageUtils.MarshalValue(this.args));
			}
		}

		// Token: 0x04004E82 RID: 20098
		private readonly string message;

		// Token: 0x04004E83 RID: 20099
		private readonly object[] args;
	}
}
