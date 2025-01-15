using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Azure.Core
{
	// Token: 0x02000075 RID: 117
	internal class MemoryResponse : Response
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000BAB3 File Offset: 0x00009CB3
		public override int Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000BABB File Offset: 0x00009CBB
		public override string ReasonPhrase
		{
			get
			{
				return this._reasonPhrase;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000BAC3 File Offset: 0x00009CC3
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x0000BACB File Offset: 0x00009CCB
		public override Stream ContentStream { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x0000BAF3 File Offset: 0x00009CF3
		public override string ClientRequestId
		{
			get
			{
				string text;
				if (!this.TryGetHeader("x-ms-client-request-id", out text))
				{
					return null;
				}
				return text;
			}
			set
			{
				this.SetHeader("x-ms-client-request-id", value);
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000BB01 File Offset: 0x00009D01
		public void SetStatus(int status)
		{
			this._status = status;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000BB0A File Offset: 0x00009D0A
		public void SetReasonPhrase(string reasonPhrase)
		{
			this._reasonPhrase = reasonPhrase;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000BB13 File Offset: 0x00009D13
		public void SetContent(byte[] content)
		{
			this.ContentStream = new MemoryStream(content);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000BB21 File Offset: 0x00009D21
		public void SetContent(string content)
		{
			this.SetContent(Encoding.UTF8.GetBytes(content));
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000BB34 File Offset: 0x00009D34
		public override void Dispose()
		{
			Stream contentStream = this.ContentStream;
			if (contentStream == null)
			{
				return;
			}
			contentStream.Dispose();
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000BB46 File Offset: 0x00009D46
		public void SetHeader(string name, string value)
		{
			this.SetHeader(name, new List<string> { value });
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000BB5B File Offset: 0x00009D5B
		public void SetHeader(string name, IEnumerable<string> values)
		{
			this._headers[name] = values.ToList<string>();
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000BB70 File Offset: 0x00009D70
		public void AddHeader(string name, string value)
		{
			List<string> list;
			if (!this._headers.TryGetValue(name, out list))
			{
				list = (this._headers[name] = new List<string>());
			}
			list.Add(value);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000BBA7 File Offset: 0x00009DA7
		protected internal override bool ContainsHeader(string name)
		{
			return this._headers.ContainsKey(name);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000BBB5 File Offset: 0x00009DB5
		protected internal override IEnumerable<HttpHeader> EnumerateHeaders()
		{
			return this._headers.Select((KeyValuePair<string, List<string>> header) => new HttpHeader(header.Key, MemoryResponse.JoinHeaderValues(header.Value)));
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		protected internal override bool TryGetHeader(string name, out string value)
		{
			List<string> list;
			if (this._headers.TryGetValue(name, out list))
			{
				value = MemoryResponse.JoinHeaderValues(list);
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000BC10 File Offset: 0x00009E10
		protected internal override bool TryGetHeaderValues(string name, out IEnumerable<string> values)
		{
			List<string> list;
			bool flag = this._headers.TryGetValue(name, out list);
			values = list;
			return flag;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000BC2E File Offset: 0x00009E2E
		private static string JoinHeaderValues(IEnumerable<string> values)
		{
			return string.Join(",", values);
		}

		// Token: 0x0400019C RID: 412
		private const int NoStatusCode = 0;

		// Token: 0x0400019D RID: 413
		private const string XmsClientRequestIdName = "x-ms-client-request-id";

		// Token: 0x0400019E RID: 414
		private int _status;

		// Token: 0x0400019F RID: 415
		private string _reasonPhrase;

		// Token: 0x040001A0 RID: 416
		private readonly IDictionary<string, List<string>> _headers = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
	}
}
