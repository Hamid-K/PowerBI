using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Json;

namespace Microsoft.OData
{
	// Token: 0x02000081 RID: 129
	[DebuggerDisplay("{ErrorCode}: {Message}")]
	public sealed class ODataError : ODataAnnotatable
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000BE5A File Offset: 0x0000A05A
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000BE62 File Offset: 0x0000A062
		public string ErrorCode { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000BE6B File Offset: 0x0000A06B
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0000BE73 File Offset: 0x0000A073
		public string Message { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000BE7C File Offset: 0x0000A07C
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0000BE84 File Offset: 0x0000A084
		public string Target { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000BE8D File Offset: 0x0000A08D
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0000BE95 File Offset: 0x0000A095
		public ICollection<ODataErrorDetail> Details { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000BE9E File Offset: 0x0000A09E
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0000BEA6 File Offset: 0x0000A0A6
		public ODataInnerError InnerError { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x000035C0 File Offset: 0x000017C0
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x000035C8 File Offset: 0x000017C8
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "We want to allow the same instance annotation collection instance to be shared across ODataLib OM instances.")]
		public ICollection<ODataInstanceAnnotation> InstanceAnnotations
		{
			get
			{
				return base.GetInstanceAnnotations();
			}
			set
			{
				base.SetInstanceAnnotations(value);
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000BEB0 File Offset: 0x0000A0B0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{{\"error\":{{\"code\":\"{0}\",\"message\":\"{1}\",\"target\":\"{2}\",\"details\":{3},\"innererror\":{4} }}}}", new object[]
			{
				(this.ErrorCode == null) ? "" : JsonValueUtils.GetEscapedJsonString(this.ErrorCode),
				(this.Message == null) ? "" : JsonValueUtils.GetEscapedJsonString(this.Message),
				(this.Target == null) ? "" : JsonValueUtils.GetEscapedJsonString(this.Target),
				(this.Details == null) ? "{}" : this.GetJsonStringForDetails(),
				(this.InnerError == null) ? "{}" : this.InnerError.ToJson()
			});
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000BF60 File Offset: 0x0000A160
		private string GetJsonStringForDetails()
		{
			return "[" + string.Join(",", this.Details.Select((ODataErrorDetail i) => i.ToJson()).ToArray<string>()) + "]";
		}
	}
}
