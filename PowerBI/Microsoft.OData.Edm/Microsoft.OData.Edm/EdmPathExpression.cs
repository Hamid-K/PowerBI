using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200003F RID: 63
	public class EdmPathExpression : EdmElement, IEdmPathExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000141 RID: 321 RVA: 0x0000453B File Offset: 0x0000273B
		public EdmPathExpression(string path)
		{
			EdmUtil.CheckArgumentNull<string>(path, "path");
			this.path = path;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004556 File Offset: 0x00002756
		public EdmPathExpression(params string[] pathSegments)
			: this(pathSegments)
		{
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004560 File Offset: 0x00002760
		public EdmPathExpression(IEnumerable<string> pathSegments)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<string>>(pathSegments, "pathSegments");
			if (pathSegments.Any((string segment) => segment.Contains("/")))
			{
				throw new ArgumentException(Strings.PathSegmentMustNotContainSlash);
			}
			this.pathSegments = pathSegments.ToList<string>();
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000045C0 File Offset: 0x000027C0
		public IEnumerable<string> PathSegments
		{
			get
			{
				IEnumerable<string> enumerable;
				if ((enumerable = this.pathSegments) == null)
				{
					enumerable = (this.pathSegments = this.path.Split(new char[] { '/' }));
				}
				return enumerable;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000045F8 File Offset: 0x000027F8
		public string Path
		{
			get
			{
				string text;
				if ((text = this.path) == null)
				{
					text = (this.path = string.Join("/", this.pathSegments.ToArray<string>()));
				}
				return text;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000462D File Offset: 0x0000282D
		public virtual EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Path;
			}
		}

		// Token: 0x04000076 RID: 118
		private IEnumerable<string> pathSegments;

		// Token: 0x04000077 RID: 119
		private string path;
	}
}
