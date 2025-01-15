using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	internal abstract class EngineErrorContextBase<TMessage> where TMessage : EngineMessageBase
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002B4D File Offset: 0x00000D4D
		public bool HasError
		{
			get
			{
				return this._hasError;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002B55 File Offset: 0x00000D55
		public bool HasMessage
		{
			get
			{
				return this._messages.Count > 0;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002B65 File Offset: 0x00000D65
		public IReadOnlyList<TMessage> Messages
		{
			get
			{
				return this._messages;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B6D File Offset: 0x00000D6D
		protected void Add(TMessage message)
		{
			this._hasError |= message.IsError;
			this._messages.Add(message);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B93 File Offset: 0x00000D93
		public string SummarizeMessages()
		{
			return string.Join(", ", this.Messages.Select(new Func<TMessage, string>(this.SummarizeMessage)).ToArray<string>());
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002BBB File Offset: 0x00000DBB
		public string SummarizeAllDetails()
		{
			return string.Join(",\n", this.Messages.Select(new Func<TMessage, string>(this.SummarizeMessageDetails)).ToArray<string>());
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002BE4 File Offset: 0x00000DE4
		private string SummarizeMessage(TMessage msg)
		{
			if (msg.AffectedItems == null || msg.AffectedItems.Length == 0)
			{
				return msg.GetErrorCodeString();
			}
			string text = string.Join(",", msg.AffectedItems);
			return string.Format(CultureInfo.InvariantCulture, "{0}:({1})", msg.GetErrorCodeString(), text);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C4C File Offset: 0x00000E4C
		private string SummarizeMessageDetails(TMessage msg)
		{
			if (msg.AffectedItems == null || msg.AffectedItems.Length == 0)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}: '{1}'", msg.GetErrorCodeString(), msg.Message);
			}
			string text = string.Join(",", msg.AffectedItems);
			return string.Format(CultureInfo.InvariantCulture, "{0}:({1}) '{2}'", msg.GetErrorCodeString(), text, msg.Message);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public TMessage GetHighestPriorityError()
		{
			TMessage tmessage = default(TMessage);
			for (int i = 0; i < this._messages.Count; i++)
			{
				TMessage tmessage2 = this._messages[i];
				if (tmessage2.Severity == EngineMessageSeverity.Error)
				{
					if (tmessage2.Source == ErrorSource.User)
					{
						return tmessage2;
					}
					if (tmessage == null)
					{
						tmessage = tmessage2;
					}
				}
			}
			return tmessage;
		}

		// Token: 0x0400003E RID: 62
		private readonly List<TMessage> _messages = new List<TMessage>();

		// Token: 0x0400003F RID: 63
		private bool _hasError;
	}
}
