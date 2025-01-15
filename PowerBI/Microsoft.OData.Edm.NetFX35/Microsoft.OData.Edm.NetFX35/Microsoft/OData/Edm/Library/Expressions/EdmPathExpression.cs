using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
	// Token: 0x0200005F RID: 95
	public class EdmPathExpression : EdmElement, IEdmPathExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000172 RID: 370 RVA: 0x0000404C File Offset: 0x0000224C
		public EdmPathExpression(string path)
			: this(EdmUtil.CheckArgumentNull<string>(path, "path").Split(new char[] { '/' }))
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000407C File Offset: 0x0000227C
		public EdmPathExpression(params string[] path)
			: this((IEnumerable<string>)path)
		{
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000408C File Offset: 0x0000228C
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

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00004100 File Offset: 0x00002300
		public IEnumerable<string> Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00004108 File Offset: 0x00002308
		public virtual EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Path;
			}
		}

		// Token: 0x0400007E RID: 126
		private readonly IEnumerable<string> path;
	}
}
