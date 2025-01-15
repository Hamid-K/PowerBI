using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000118 RID: 280
	internal sealed class DefaultUriLiteralParser : IUriLiteralParser
	{
		// Token: 0x06000F8C RID: 3980 RVA: 0x00026922 File Offset: 0x00024B22
		private DefaultUriLiteralParser()
		{
			this.uriTypeParsers = new List<IUriLiteralParser>
			{
				CustomUriLiteralParsers.Instance,
				UriPrimitiveTypeParser.Instance
			};
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0002694B File Offset: 0x00024B4B
		internal static DefaultUriLiteralParser Instance
		{
			get
			{
				return DefaultUriLiteralParser.singleInstance;
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00026954 File Offset: 0x00024B54
		public object ParseUriStringToType(string text, IEdmTypeReference targetType, out UriLiteralParsingException parsingException)
		{
			parsingException = null;
			foreach (IUriLiteralParser uriLiteralParser in this.uriTypeParsers)
			{
				object obj = uriLiteralParser.ParseUriStringToType(text, targetType, out parsingException);
				if (parsingException != null)
				{
					return null;
				}
				if (obj != null)
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x04000792 RID: 1938
		private List<IUriLiteralParser> uriTypeParsers;

		// Token: 0x04000793 RID: 1939
		private static DefaultUriLiteralParser singleInstance = new DefaultUriLiteralParser();
	}
}
