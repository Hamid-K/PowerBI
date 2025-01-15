using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001695 RID: 5781
	internal struct Message0 : IUserMessage, IPiiFree
	{
		// Token: 0x0600924B RID: 37451 RVA: 0x001E54A4 File Offset: 0x001E36A4
		public Message0(string message)
		{
			this.message = message;
		}

		// Token: 0x0600924C RID: 37452 RVA: 0x001E54AD File Offset: 0x001E36AD
		public static implicit operator string(Message0 message)
		{
			return message.ToString();
		}

		// Token: 0x0600924D RID: 37453 RVA: 0x001E54BC File Offset: 0x001E36BC
		public override string ToString()
		{
			return this.message;
		}

		// Token: 0x1700266C RID: 9836
		// (get) Token: 0x0600924E RID: 37454 RVA: 0x001E54C4 File Offset: 0x001E36C4
		public TextValue Message
		{
			get
			{
				return TextValue.New(this.message);
			}
		}

		// Token: 0x1700266D RID: 9837
		// (get) Token: 0x0600924F RID: 37455 RVA: 0x001E54D1 File Offset: 0x001E36D1
		public ListValue Parameters
		{
			get
			{
				return ListValue.Empty;
			}
		}

		// Token: 0x1700266E RID: 9838
		// (get) Token: 0x06009250 RID: 37456 RVA: 0x001E54C4 File Offset: 0x001E36C4
		public Value Value
		{
			get
			{
				return TextValue.New(this.message);
			}
		}

		// Token: 0x1700266F RID: 9839
		// (get) Token: 0x06009251 RID: 37457 RVA: 0x001E54BC File Offset: 0x001E36BC
		public object ClrValue
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x04004E78 RID: 20088
		private readonly string message;
	}
}
