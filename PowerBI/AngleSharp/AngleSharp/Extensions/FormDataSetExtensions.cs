using System;
using System.IO;
using System.Text;
using AngleSharp.Html;
using AngleSharp.Network;

namespace AngleSharp.Extensions
{
	// Token: 0x020000ED RID: 237
	internal static class FormDataSetExtensions
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x00034A54 File Offset: 0x00032C54
		public static Stream CreateBody(this FormDataSet formDataSet, string enctype, string charset)
		{
			Encoding encoding = TextEncoding.Resolve(charset);
			return formDataSet.CreateBody(enctype, encoding);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00034A70 File Offset: 0x00032C70
		public static Stream CreateBody(this FormDataSet formDataSet, string enctype, Encoding encoding)
		{
			if (enctype.Isi(MimeTypeNames.UrlencodedForm))
			{
				return formDataSet.AsUrlEncoded(encoding);
			}
			if (enctype.Isi(MimeTypeNames.MultipartForm))
			{
				return formDataSet.AsMultipart(encoding);
			}
			if (enctype.Isi(MimeTypeNames.Plain))
			{
				return formDataSet.AsPlaintext(encoding);
			}
			if (enctype.Isi(MimeTypeNames.ApplicationJson))
			{
				return formDataSet.AsJson();
			}
			return Stream.Null;
		}
	}
}
