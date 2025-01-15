using System;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000C5 RID: 197
	[Serializable]
	public sealed class ValidationMessage
	{
		// Token: 0x06000B42 RID: 2882 RVA: 0x00025178 File Offset: 0x00023378
		public ValidationMessage(ModelingErrorCode code, Severity severity, string objectType, string objectID, string message)
		{
			this.m_code = code;
			this.m_severity = severity;
			this.m_objectType = objectType ?? string.Empty;
			this.m_objectID = objectID ?? string.Empty;
			this.m_message = message ?? string.Empty;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000251CB File Offset: 0x000233CB
		internal ValidationMessage(ModelingErrorCode code, Severity severity, IValidationScope scope, string message)
			: this(code, severity, (scope != null) ? scope.ObjectType : null, (scope != null) ? scope.ObjectID : null, message)
		{
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x000251EF File Offset: 0x000233EF
		public ModelingErrorCode Code
		{
			get
			{
				return this.m_code;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x000251F7 File Offset: 0x000233F7
		public Severity Severity
		{
			get
			{
				return this.m_severity;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x000251FF File Offset: 0x000233FF
		public string ObjectType
		{
			get
			{
				return this.m_objectType;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x00025207 File Offset: 0x00023407
		public string ObjectID
		{
			get
			{
				return this.m_objectID;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0002520F File Offset: 0x0002340F
		public string Message
		{
			get
			{
				return this.m_message;
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00025218 File Offset: 0x00023418
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0} {1} {2} ({3} '{4}')", new object[]
			{
				char.ToLowerInvariant(this.m_severity.ToString()[0]),
				this.m_code,
				this.m_message,
				this.m_objectType,
				this.m_objectID
			});
		}

		// Token: 0x040004A0 RID: 1184
		private readonly ModelingErrorCode m_code;

		// Token: 0x040004A1 RID: 1185
		private readonly Severity m_severity;

		// Token: 0x040004A2 RID: 1186
		private readonly string m_objectType;

		// Token: 0x040004A3 RID: 1187
		private readonly string m_objectID;

		// Token: 0x040004A4 RID: 1188
		private readonly string m_message;
	}
}
