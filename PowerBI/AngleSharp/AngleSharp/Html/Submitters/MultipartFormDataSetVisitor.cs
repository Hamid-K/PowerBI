using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AngleSharp.Dom.Io;
using AngleSharp.Extensions;

namespace AngleSharp.Html.Submitters
{
	// Token: 0x020000C4 RID: 196
	internal sealed class MultipartFormDataSetVisitor : IFormSubmitter, IFormDataSetVisitor
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x0002E60B File Offset: 0x0002C80B
		public MultipartFormDataSetVisitor(Encoding encoding, string boundary)
		{
			this._encoding = encoding;
			this._writers = new List<Action<StreamWriter>>();
			this._boundary = boundary;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0002E62C File Offset: 0x0002C82C
		public void Text(FormDataSetEntry entry, string value)
		{
			if (entry.HasName && value != null)
			{
				this._writers.Add(delegate(StreamWriter stream)
				{
					stream.WriteLine("Content-Disposition: form-data; name=\"{0}\"", entry.Name.HtmlEncode(this._encoding));
					stream.WriteLine();
					stream.WriteLine(value.HtmlEncode(this._encoding));
				});
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0002E680 File Offset: 0x0002C880
		public void File(FormDataSetEntry entry, string fileName, string contentType, IFile content)
		{
			if (entry.HasName)
			{
				this._writers.Add(delegate(StreamWriter stream)
				{
					bool flag;
					if (content != null)
					{
						IFile content2 = content;
						if (((content2 != null) ? content2.Name : null) != null && content.Type != null)
						{
							flag = content.Body != null;
							goto IL_003A;
						}
					}
					flag = false;
					IL_003A:
					stream.WriteLine("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"", entry.Name.HtmlEncode(this._encoding), fileName.HtmlEncode(this._encoding));
					stream.WriteLine("Content-Type: {0}", contentType);
					stream.WriteLine();
					if (flag)
					{
						stream.Flush();
						content.Body.CopyTo(stream.BaseStream);
					}
					stream.WriteLine();
				});
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0002E6DC File Offset: 0x0002C8DC
		public void Serialize(StreamWriter stream)
		{
			foreach (Action<StreamWriter> action in this._writers)
			{
				stream.Write(MultipartFormDataSetVisitor.DashDash);
				stream.WriteLine(this._boundary);
				action(stream);
			}
			stream.Write(MultipartFormDataSetVisitor.DashDash);
			stream.Write(this._boundary);
			stream.Write(MultipartFormDataSetVisitor.DashDash);
		}

		// Token: 0x040005E8 RID: 1512
		private static readonly string DashDash = "--";

		// Token: 0x040005E9 RID: 1513
		private readonly Encoding _encoding;

		// Token: 0x040005EA RID: 1514
		private readonly List<Action<StreamWriter>> _writers;

		// Token: 0x040005EB RID: 1515
		private readonly string _boundary;
	}
}
