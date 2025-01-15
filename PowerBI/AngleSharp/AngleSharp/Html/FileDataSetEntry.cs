using System;
using System.IO;
using System.Text;
using AngleSharp.Dom.Io;
using AngleSharp.Network;

namespace AngleSharp.Html
{
	// Token: 0x020000B3 RID: 179
	internal sealed class FileDataSetEntry : FormDataSetEntry
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x00020523 File Offset: 0x0001E723
		public FileDataSetEntry(string name, IFile value, string type)
			: base(name, type)
		{
			this._value = value;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00020534 File Offset: 0x0001E734
		public string FileName
		{
			get
			{
				IFile value = this._value;
				return ((value != null) ? value.Name : null) ?? string.Empty;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00020551 File Offset: 0x0001E751
		public string ContentType
		{
			get
			{
				IFile value = this._value;
				return ((value != null) ? value.Type : null) ?? MimeTypeNames.Binary;
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00020570 File Offset: 0x0001E770
		public override bool Contains(string boundary, Encoding encoding)
		{
			bool flag = false;
			IFile value = this._value;
			Stream stream = ((value != null) ? value.Body : null);
			if (stream != null && stream.CanSeek)
			{
				using (StreamReader streamReader = new StreamReader(stream, encoding, false, 4096, true))
				{
					while (streamReader.Peek() != -1)
					{
						if (streamReader.ReadLine().Contains(boundary))
						{
							flag = true;
							break;
						}
					}
				}
				stream.Seek(0L, SeekOrigin.Begin);
			}
			return flag;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000205F0 File Offset: 0x0001E7F0
		public override void Accept(IFormDataSetVisitor visitor)
		{
			visitor.File(this, this.FileName, this.ContentType, this._value);
		}

		// Token: 0x040004DC RID: 1244
		private readonly IFile _value;
	}
}
