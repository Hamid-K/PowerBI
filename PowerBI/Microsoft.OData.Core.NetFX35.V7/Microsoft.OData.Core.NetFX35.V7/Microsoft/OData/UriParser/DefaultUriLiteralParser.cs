using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A2 RID: 418
	internal sealed class DefaultUriLiteralParser : IUriLiteralParser
	{
		// Token: 0x060010ED RID: 4333 RVA: 0x0002F126 File Offset: 0x0002D326
		private DefaultUriLiteralParser()
		{
			List<IUriLiteralParser> list = new List<IUriLiteralParser>();
			list.Add(CustomUriLiteralParsers.Instance);
			list.Add(UriPrimitiveTypeParser.Instance);
			this.uriTypeParsers = list;
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x0002F14F File Offset: 0x0002D34F
		internal static DefaultUriLiteralParser Instance
		{
			get
			{
				return DefaultUriLiteralParser.singleInstance;
			}
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0002F158 File Offset: 0x0002D358
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

		// Token: 0x040008BB RID: 2235
		private List<IUriLiteralParser> uriTypeParsers;

		// Token: 0x040008BC RID: 2236
		private static DefaultUriLiteralParser singleInstance = new DefaultUriLiteralParser();
	}
}
