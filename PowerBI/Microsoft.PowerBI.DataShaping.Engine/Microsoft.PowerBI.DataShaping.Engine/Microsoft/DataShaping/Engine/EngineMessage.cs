using System;
using System.Globalization;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x0200000E RID: 14
	internal class EngineMessage : EngineMessageBase
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002AE8 File Offset: 0x00000CE8
		internal EngineMessage(string message, string traceMessage, EngineErrorCode errorCode, EngineMessageSeverity severity, ErrorSource source)
			: base(message, traceMessage, severity, source, null)
		{
			this._errorCode = errorCode;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002AFE File Offset: 0x00000CFE
		private static CultureInfo FormatCulture
		{
			get
			{
				return CultureInfo.CurrentCulture;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002B05 File Offset: 0x00000D05
		public EngineErrorCode ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002B10 File Offset: 0x00000D10
		public static EngineMessage Create(EngineErrorCode errorCode, EngineMessageSeverity severity, ErrorSource source, params string[] args)
		{
			string template = EngineMessages.GetTemplate(errorCode);
			return EngineMessage.Create(errorCode, template, severity, source, args);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B30 File Offset: 0x00000D30
		private static EngineMessage Create(EngineErrorCode errorCode, string messageTemplate, EngineMessageSeverity severity, ErrorSource source, params string[] args)
		{
			string text = string.Format(EngineMessage.FormatCulture, messageTemplate, args);
			return new EngineMessage(text, text, errorCode, severity, source);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B58 File Offset: 0x00000D58
		public override bool Equals(EngineMessageBase other)
		{
			bool flag;
			EngineMessage engineMessage;
			if (EngineMessageBase.CheckReferenceAndBaseEquality<EngineMessage>(this, other, out flag, out engineMessage))
			{
				return flag;
			}
			return this._errorCode == engineMessage._errorCode;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B84 File Offset: 0x00000D84
		public override string GetErrorCodeString()
		{
			return this.ErrorCode.ToString();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002BA5 File Offset: 0x00000DA5
		protected override int GetDerivedTypeHashCodeContent()
		{
			return Hashing.GetHashCode<EngineErrorCode>(this._errorCode, null);
		}

		// Token: 0x04000039 RID: 57
		private readonly EngineErrorCode _errorCode;
	}
}
