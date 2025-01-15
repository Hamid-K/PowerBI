using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Parsers.Common;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020002BD RID: 701
	internal sealed class DefaultUriLiteralParser : IUriLiteralParser
	{
		// Token: 0x0600182C RID: 6188 RVA: 0x00052400 File Offset: 0x00050600
		private DefaultUriLiteralParser()
		{
			List<IUriLiteralParser> list = new List<IUriLiteralParser>();
			list.Add(CustomUriLiteralParsers.Instance);
			list.Add(UriPrimitiveTypeParser.Instance);
			this.uriTypeParsers = list;
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x00052436 File Offset: 0x00050636
		internal static DefaultUriLiteralParser Instance
		{
			get
			{
				return DefaultUriLiteralParser.singleInstance;
			}
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x00052440 File Offset: 0x00050640
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

		// Token: 0x04000A44 RID: 2628
		private List<IUriLiteralParser> uriTypeParsers;

		// Token: 0x04000A45 RID: 2629
		private static DefaultUriLiteralParser singleInstance = new DefaultUriLiteralParser();
	}
}
