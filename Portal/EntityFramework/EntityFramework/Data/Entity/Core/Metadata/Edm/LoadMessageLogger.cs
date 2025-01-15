using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Text;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200050D RID: 1293
	internal class LoadMessageLogger
	{
		// Token: 0x06003FB5 RID: 16309 RVA: 0x000D4048 File Offset: 0x000D2248
		internal LoadMessageLogger(Action<string> logLoadMessage)
		{
			this._logLoadMessage = logLoadMessage;
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x000D4062 File Offset: 0x000D2262
		internal virtual void LogLoadMessage(string message, EdmType relatedType)
		{
			if (this._logLoadMessage != null)
			{
				this._logLoadMessage(message);
			}
			this.LogMessagesWithTypeInfo(message, relatedType);
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x000D4080 File Offset: 0x000D2280
		internal virtual string CreateErrorMessageWithTypeSpecificLoadLogs(string errorMessage, EdmType relatedType)
		{
			return new StringBuilder(errorMessage).AppendLine(this.GetTypeRelatedLogMessage(relatedType)).ToString();
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x000D409C File Offset: 0x000D229C
		private string GetTypeRelatedLogMessage(EdmType relatedType)
		{
			if (this._messages.ContainsKey(relatedType))
			{
				return new StringBuilder().AppendLine().AppendLine(Strings.ExtraInfo).AppendLine(this._messages[relatedType].ToString())
					.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x000D40EC File Offset: 0x000D22EC
		private void LogMessagesWithTypeInfo(string message, EdmType relatedType)
		{
			if (this._messages.ContainsKey(relatedType))
			{
				this._messages[relatedType].AppendLine(message);
				return;
			}
			this._messages.Add(relatedType, new StringBuilder(message));
		}

		// Token: 0x0400163E RID: 5694
		private readonly Action<string> _logLoadMessage;

		// Token: 0x0400163F RID: 5695
		private readonly Dictionary<EdmType, StringBuilder> _messages = new Dictionary<EdmType, StringBuilder>();
	}
}
