using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AngleSharp.Dom.Io;
using AngleSharp.Extensions;
using AngleSharp.Html.Submitters;

namespace AngleSharp.Html
{
	// Token: 0x020000B5 RID: 181
	public sealed class FormDataSet : IEnumerable<string>, IEnumerable
	{
		// Token: 0x06000538 RID: 1336 RVA: 0x00020640 File Offset: 0x0001E840
		public FormDataSet()
		{
			this._boundary = Guid.NewGuid().ToString();
			this._entries = new List<FormDataSetEntry>();
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00020677 File Offset: 0x0001E877
		public string Boundary
		{
			get
			{
				return this._boundary;
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0002067F File Offset: 0x0001E87F
		public Stream AsMultipart(Encoding encoding = null)
		{
			return this.BuildRequestContent(encoding, delegate(StreamWriter stream)
			{
				this.Connect(new MultipartFormDataSetVisitor(stream.Encoding, this._boundary), stream);
			});
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00020694 File Offset: 0x0001E894
		public Stream AsUrlEncoded(Encoding encoding = null)
		{
			return this.BuildRequestContent(encoding, delegate(StreamWriter stream)
			{
				this.Connect(new UrlEncodedFormDataSetVisitor(stream.Encoding), stream);
			});
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000206A9 File Offset: 0x0001E8A9
		public Stream AsPlaintext(Encoding encoding = null)
		{
			return this.BuildRequestContent(encoding, delegate(StreamWriter stream)
			{
				this.Connect(new PlaintextFormDataSetVisitor(), stream);
			});
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000206BE File Offset: 0x0001E8BE
		public Stream AsJson()
		{
			return this.BuildRequestContent(TextEncoding.Utf8, delegate(StreamWriter stream)
			{
				this.Connect(new JsonFormDataSetVisitor(), stream);
			});
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000206D8 File Offset: 0x0001E8D8
		public Stream As(IFormSubmitter submitter, Encoding encoding = null)
		{
			return this.BuildRequestContent(encoding, delegate(StreamWriter stream)
			{
				this.Connect(submitter, stream);
			});
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0002070C File Offset: 0x0001E90C
		public void Append(string name, string value, string type)
		{
			if (type.Isi(TagNames.Textarea))
			{
				name = name.NormalizeLineEndings();
				value = value.NormalizeLineEndings();
			}
			this._entries.Add(new TextDataSetEntry(name, value, type));
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0002073E File Offset: 0x0001E93E
		public void Append(string name, IFile value, string type)
		{
			if (type.Isi(InputTypeNames.File))
			{
				name = name.NormalizeLineEndings();
			}
			this._entries.Add(new FileDataSetEntry(name, value, type));
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00020768 File Offset: 0x0001E968
		private Stream BuildRequestContent(Encoding encoding, Action<StreamWriter> process)
		{
			encoding = encoding ?? TextEncoding.Utf8;
			MemoryStream memoryStream = new MemoryStream();
			this.FixPotentialBoundaryCollisions(encoding);
			this.ReplaceCharset(encoding);
			StreamWriter streamWriter = new StreamWriter(memoryStream, encoding);
			process(streamWriter);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000207B4 File Offset: 0x0001E9B4
		private void Connect(IFormSubmitter submitter, StreamWriter stream)
		{
			foreach (FormDataSetEntry formDataSetEntry in this._entries)
			{
				formDataSetEntry.Accept(submitter);
			}
			submitter.Serialize(stream);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0002080C File Offset: 0x0001EA0C
		private void ReplaceCharset(Encoding encoding)
		{
			for (int i = 0; i < this._entries.Count; i++)
			{
				FormDataSetEntry formDataSetEntry = this._entries[i];
				if (!string.IsNullOrEmpty(formDataSetEntry.Name) && formDataSetEntry.Name.Is("_charset_") && formDataSetEntry.Type.Isi(InputTypeNames.Hidden))
				{
					this._entries[i] = new TextDataSetEntry(formDataSetEntry.Name, encoding.WebName, formDataSetEntry.Type);
				}
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00020890 File Offset: 0x0001EA90
		private void FixPotentialBoundaryCollisions(Encoding encoding)
		{
			bool flag = false;
			do
			{
				for (int i = 0; i < this._entries.Count; i++)
				{
					if (flag = this._entries[i].Contains(this._boundary, encoding))
					{
						this._boundary = Guid.NewGuid().ToString();
						break;
					}
				}
			}
			while (flag);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000208EF File Offset: 0x0001EAEF
		public IEnumerator<string> GetEnumerator()
		{
			return this._entries.Select((FormDataSetEntry m) => m.Name).GetEnumerator();
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00020920 File Offset: 0x0001EB20
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040004E0 RID: 1248
		private readonly List<FormDataSetEntry> _entries;

		// Token: 0x040004E1 RID: 1249
		private string _boundary;
	}
}
