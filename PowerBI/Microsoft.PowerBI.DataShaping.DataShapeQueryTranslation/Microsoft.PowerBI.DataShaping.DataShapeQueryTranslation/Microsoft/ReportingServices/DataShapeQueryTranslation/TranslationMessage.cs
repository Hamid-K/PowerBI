using System;
using System.Globalization;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	internal sealed class TranslationMessage : EngineMessageBase
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0000D728 File Offset: 0x0000B928
		internal TranslationMessage(string message, string traceMessage, TranslationErrorCode errorCode, EngineMessageSeverity severity, ErrorSource source, ObjectType objectType, string objectId, string propertyName, int? line, int? position, string[] affectedItems)
			: base(message, traceMessage, severity, source, affectedItems)
		{
			this.m_errorCode = errorCode;
			this.m_objectType = objectType;
			this.m_objectId = objectId;
			this.m_propertyName = propertyName;
			this.m_line = line;
			this.m_position = position;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000D767 File Offset: 0x0000B967
		public TranslationErrorCode ErrorCode
		{
			get
			{
				return this.m_errorCode;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000D76F File Offset: 0x0000B96F
		public ObjectType ObjectType
		{
			get
			{
				return this.m_objectType;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000D777 File Offset: 0x0000B977
		public string ObjectId
		{
			get
			{
				return this.m_objectId;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000D77F File Offset: 0x0000B97F
		public string PropertyName
		{
			get
			{
				return this.m_propertyName;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000D787 File Offset: 0x0000B987
		public int? Line
		{
			get
			{
				return this.m_line;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000D78F File Offset: 0x0000B98F
		public int? Position
		{
			get
			{
				return this.m_position;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000D798 File Offset: 0x0000B998
		public string FormatMessage()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} ({1}.{2}) : {3} [{4}]", new object[] { base.Severity, this.m_objectId, this.m_propertyName, base.Message, this.m_errorCode });
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000D7F4 File Offset: 0x0000B9F4
		public override string GetErrorCodeString()
		{
			return this.ErrorCode.ToString();
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000D818 File Offset: 0x0000BA18
		public override bool Equals(EngineMessageBase other)
		{
			bool flag;
			TranslationMessage translationMessage;
			if (EngineMessageBase.CheckReferenceAndBaseEquality<TranslationMessage>(this, other, out flag, out translationMessage))
			{
				return flag;
			}
			if (this.m_errorCode == translationMessage.m_errorCode && this.m_objectType == translationMessage.m_objectType && this.m_objectId == translationMessage.m_objectId && this.m_propertyName == translationMessage.m_propertyName)
			{
				int? num = this.m_line;
				int? num2 = translationMessage.m_line;
				if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
				{
					num2 = this.m_position;
					num = translationMessage.m_position;
					return (num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null));
				}
			}
			return false;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000D8E0 File Offset: 0x0000BAE0
		protected override int GetDerivedTypeHashCodeContent()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<TranslationErrorCode>(this.m_errorCode, null), Hashing.GetHashCode<ObjectType>(this.m_objectType, null), Hashing.GetHashCode<string>(this.m_objectId, null), Hashing.GetHashCode<string>(this.m_propertyName, null), Hashing.GetHashCode<int?>(this.m_line, null), Hashing.GetHashCode<int?>(this.m_position, null));
		}

		// Token: 0x04000256 RID: 598
		private readonly TranslationErrorCode m_errorCode;

		// Token: 0x04000257 RID: 599
		private readonly ObjectType m_objectType;

		// Token: 0x04000258 RID: 600
		private readonly string m_objectId;

		// Token: 0x04000259 RID: 601
		private readonly string m_propertyName;

		// Token: 0x0400025A RID: 602
		private readonly int? m_line;

		// Token: 0x0400025B RID: 603
		private readonly int? m_position;
	}
}
