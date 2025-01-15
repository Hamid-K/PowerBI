using System;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000048 RID: 72
	internal sealed class Message
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x00006087 File Offset: 0x00004287
		internal Message(string code, string severity, string messageText, string objectName, string objectType, string propertyName)
		{
			this._code = code;
			this._severity = severity;
			this._messageText = messageText;
			this._objectName = objectName;
			this._objectType = objectType;
			this._propertyName = propertyName;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000060BC File Offset: 0x000042BC
		internal string Code
		{
			get
			{
				return this._code;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000060C4 File Offset: 0x000042C4
		internal string Severity
		{
			get
			{
				return this._severity;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000060CC File Offset: 0x000042CC
		internal string MessageText
		{
			get
			{
				return this._messageText;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000060D4 File Offset: 0x000042D4
		internal string ObjectName
		{
			get
			{
				return this._objectName;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000060DC File Offset: 0x000042DC
		internal string ObjectType
		{
			get
			{
				return this._objectType;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000060E4 File Offset: 0x000042E4
		internal string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x0400012B RID: 299
		private readonly string _code;

		// Token: 0x0400012C RID: 300
		private readonly string _messageText;

		// Token: 0x0400012D RID: 301
		private readonly string _objectName;

		// Token: 0x0400012E RID: 302
		private readonly string _objectType;

		// Token: 0x0400012F RID: 303
		private readonly string _propertyName;

		// Token: 0x04000130 RID: 304
		private readonly string _severity;
	}
}
