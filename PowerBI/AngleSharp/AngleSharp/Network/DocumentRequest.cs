using System;
using System.Collections.Generic;
using System.IO;
using AngleSharp.Dom;
using AngleSharp.Html;

namespace AngleSharp.Network
{
	// Token: 0x0200008D RID: 141
	public class DocumentRequest
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x0001BF2B File Offset: 0x0001A12B
		public DocumentRequest(Url target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			this.Headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.Target = target;
			this.Method = HttpMethod.Get;
			this.Body = Stream.Null;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001BF6A File Offset: 0x0001A16A
		public static DocumentRequest Get(Url target, INode source = null, string referer = null)
		{
			return new DocumentRequest(target)
			{
				Method = HttpMethod.Get,
				Referer = referer,
				Source = source
			};
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001BF88 File Offset: 0x0001A188
		public static DocumentRequest Post(Url target, Stream body, string type, INode source = null, string referer = null)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return new DocumentRequest(target)
			{
				Method = HttpMethod.Post,
				Body = body,
				MimeType = type,
				Referer = referer,
				Source = source
			};
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public static DocumentRequest PostAsPlaintext(Url target, IDictionary<string, string> fields)
		{
			if (fields == null)
			{
				throw new ArgumentNullException("fields");
			}
			FormDataSet formDataSet = new FormDataSet();
			foreach (KeyValuePair<string, string> keyValuePair in fields)
			{
				formDataSet.Append(keyValuePair.Key, keyValuePair.Value, InputTypeNames.Text);
			}
			return DocumentRequest.Post(target, formDataSet.AsPlaintext(null), MimeTypeNames.Plain, null, null);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001C060 File Offset: 0x0001A260
		public static DocumentRequest PostAsUrlencoded(Url target, IDictionary<string, string> fields)
		{
			if (fields == null)
			{
				throw new ArgumentNullException("fields");
			}
			FormDataSet formDataSet = new FormDataSet();
			foreach (KeyValuePair<string, string> keyValuePair in fields)
			{
				formDataSet.Append(keyValuePair.Key, keyValuePair.Value, InputTypeNames.Text);
			}
			return DocumentRequest.Post(target, formDataSet.AsUrlEncoded(null), MimeTypeNames.UrlencodedForm, null, null);
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0001C0E4 File Offset: 0x0001A2E4
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0001C0EC File Offset: 0x0001A2EC
		public INode Source { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0001C0F5 File Offset: 0x0001A2F5
		public Url Target { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0001C0FD File Offset: 0x0001A2FD
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0001C10A File Offset: 0x0001A30A
		public string Referer
		{
			get
			{
				return this.GetHeader(HeaderNames.Referer);
			}
			set
			{
				this.SetHeader(HeaderNames.Referer, value);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0001C118 File Offset: 0x0001A318
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0001C120 File Offset: 0x0001A320
		public HttpMethod Method { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0001C129 File Offset: 0x0001A329
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x0001C131 File Offset: 0x0001A331
		public Stream Body { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0001C13A File Offset: 0x0001A33A
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x0001C147 File Offset: 0x0001A347
		public string MimeType
		{
			get
			{
				return this.GetHeader(HeaderNames.ContentType);
			}
			set
			{
				this.SetHeader(HeaderNames.ContentType, value);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0001C155 File Offset: 0x0001A355
		public Dictionary<string, string> Headers { get; }

		// Token: 0x06000468 RID: 1128 RVA: 0x0001C15D File Offset: 0x0001A35D
		private void SetHeader(string name, string value)
		{
			this.Headers[name] = value;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0001C16C File Offset: 0x0001A36C
		private string GetHeader(string name)
		{
			string text = null;
			this.Headers.TryGetValue(name, out text);
			return text;
		}
	}
}
