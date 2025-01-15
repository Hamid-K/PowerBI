using System;
using System.Globalization;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Utils;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	internal sealed class SemanticQueryTranslationMessage : EngineMessageBase
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002586 File Offset: 0x00000786
		internal SemanticQueryTranslationMessage(string errorCode, string message, string traceMessage, EngineMessageSeverity severity, ErrorSource source, string[] affectedItems)
			: base(message, traceMessage, severity, source, affectedItems)
		{
			this.ErrorCode = errorCode;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000259D File Offset: 0x0000079D
		public string ErrorCode { get; }

		// Token: 0x06000021 RID: 33 RVA: 0x000025A5 File Offset: 0x000007A5
		public override string GetErrorCodeString()
		{
			return this.ErrorCode;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025B0 File Offset: 0x000007B0
		public override bool Equals(EngineMessageBase other)
		{
			bool flag;
			SemanticQueryTranslationMessage semanticQueryTranslationMessage;
			if (EngineMessageBase.CheckReferenceAndBaseEquality<SemanticQueryTranslationMessage>(this, other, out flag, out semanticQueryTranslationMessage))
			{
				return flag;
			}
			return this.ErrorCode == semanticQueryTranslationMessage.ErrorCode;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025DD File Offset: 0x000007DD
		protected override int GetDerivedTypeHashCodeContent()
		{
			return Hashing.GetHashCode<string>(this.ErrorCode, null);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025EB File Offset: 0x000007EB
		public static SemanticQueryTranslationMessage Create(string errorCode, string messageTemplate, EngineMessageSeverity severity, ErrorSource errorSource, params object[] args)
		{
			return SemanticQueryTranslationMessage.Create(errorCode, messageTemplate, null, severity, errorSource, args);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025FC File Offset: 0x000007FC
		public static SemanticQueryTranslationMessage Create(string errorCode, string messageTemplate, string[] affectedItems, EngineMessageSeverity severity, ErrorSource errorSource, params object[] args)
		{
			string text2;
			string text = SemanticQueryTranslationMessage.GenerateMessage(messageTemplate, args, out text2);
			return new SemanticQueryTranslationMessage(errorCode, text, text2, severity, errorSource, affectedItems);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002620 File Offset: 0x00000820
		private static string GenerateMessage(string messageTemplate, object[] args, out string traceMessage)
		{
			traceMessage = messageTemplate;
			if (args.IsNullOrEmpty<object>())
			{
				return messageTemplate;
			}
			object[] array = SemanticQueryTranslationMessage.StringifyArgs(args);
			return string.Format(CultureInfo.InvariantCulture, messageTemplate, array);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000264D File Offset: 0x0000084D
		private static object[] StringifyArgs(object[] args)
		{
			return args.Select(delegate(object a)
			{
				IContainsTelemetryMarkup containsTelemetryMarkup = a as IContainsTelemetryMarkup;
				if (containsTelemetryMarkup != null)
				{
					return containsTelemetryMarkup.ToCustomerContentString();
				}
				return a;
			}).ToArray<object>();
		}
	}
}
