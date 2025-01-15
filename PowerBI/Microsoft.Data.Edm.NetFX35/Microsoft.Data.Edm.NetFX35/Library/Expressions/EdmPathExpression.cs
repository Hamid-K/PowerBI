using System;
using System.Collections.Generic;
using Microsoft.Data.Edm.Expressions;

namespace Microsoft.Data.Edm.Library.Expressions
{
	// Token: 0x020000BA RID: 186
	public class EdmPathExpression : EdmElement, IEdmPathExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000355 RID: 853 RVA: 0x00008AE0 File Offset: 0x00006CE0
		public EdmPathExpression(string path)
			: this(EdmUtil.CheckArgumentNull<string>(path, "path").Split(new char[] { '/' }))
		{
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00008B10 File Offset: 0x00006D10
		public EdmPathExpression(params string[] path)
			: this((IEnumerable<string>)path)
		{
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00008B20 File Offset: 0x00006D20
		public EdmPathExpression(IEnumerable<string> path)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<string>>(path, "path");
			foreach (string text in path)
			{
				if (text.Contains("/"))
				{
					throw new ArgumentException(Strings.PathSegmentMustNotContainSlash);
				}
			}
			this.path = path;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00008B94 File Offset: 0x00006D94
		public IEnumerable<string> Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00008B9C File Offset: 0x00006D9C
		public EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Path;
			}
		}

		// Token: 0x04000178 RID: 376
		private readonly IEnumerable<string> path;
	}
}
