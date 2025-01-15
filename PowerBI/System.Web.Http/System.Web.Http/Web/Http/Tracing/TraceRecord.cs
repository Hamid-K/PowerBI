using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;

namespace System.Web.Http.Tracing
{
	// Token: 0x0200011F RID: 287
	[DebuggerDisplay("Category: {Category}, Operation: {Operation}, Level: {Level}, Kind: {Kind}")]
	public class TraceRecord
	{
		// Token: 0x06000791 RID: 1937 RVA: 0x000131A4 File Offset: 0x000113A4
		public TraceRecord(HttpRequestMessage request, string category, TraceLevel level)
		{
			this.Timestamp = DateTime.UtcNow;
			this.Request = request;
			this.RequestId = ((request != null) ? request.GetCorrelationId() : Guid.Empty);
			this.Category = category;
			this.Level = level;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x00013217 File Offset: 0x00011417
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x0001321F File Offset: 0x0001141F
		public string Category { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00013228 File Offset: 0x00011428
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x00013230 File Offset: 0x00011430
		public Exception Exception { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00013239 File Offset: 0x00011439
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x00013241 File Offset: 0x00011441
		public TraceKind Kind
		{
			get
			{
				return this._traceKind;
			}
			set
			{
				TraceKindHelper.Validate(value, "value");
				this._traceKind = value;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x00013255 File Offset: 0x00011455
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x0001325D File Offset: 0x0001145D
		public TraceLevel Level
		{
			get
			{
				return this._traceLevel;
			}
			set
			{
				TraceLevelHelper.Validate(value, "value");
				this._traceLevel = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00013271 File Offset: 0x00011471
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x00013279 File Offset: 0x00011479
		public string Message { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00013282 File Offset: 0x00011482
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x0001328A File Offset: 0x0001148A
		public string Operation { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00013293 File Offset: 0x00011493
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x0001329B File Offset: 0x0001149B
		public string Operator { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x000132A4 File Offset: 0x000114A4
		public Dictionary<object, object> Properties
		{
			get
			{
				return this._properties.Value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x000132B1 File Offset: 0x000114B1
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x000132B9 File Offset: 0x000114B9
		public HttpRequestMessage Request { get; private set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x000132C2 File Offset: 0x000114C2
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x000132CA File Offset: 0x000114CA
		public Guid RequestId { get; private set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x000132D3 File Offset: 0x000114D3
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x000132DB File Offset: 0x000114DB
		public HttpStatusCode Status { get; set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x000132E4 File Offset: 0x000114E4
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x000132EC File Offset: 0x000114EC
		public DateTime Timestamp { get; private set; }

		// Token: 0x040001F9 RID: 505
		private TraceKind _traceKind;

		// Token: 0x040001FA RID: 506
		private TraceLevel _traceLevel;

		// Token: 0x040001FB RID: 507
		private Lazy<Dictionary<object, object>> _properties = new Lazy<Dictionary<object, object>>(() => new Dictionary<object, object>());
	}
}
