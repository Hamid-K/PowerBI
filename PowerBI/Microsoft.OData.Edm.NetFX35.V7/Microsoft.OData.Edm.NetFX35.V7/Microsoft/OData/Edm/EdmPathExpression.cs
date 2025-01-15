using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200006E RID: 110
	public class EdmPathExpression : EdmElement, IEdmPathExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x0000BEEE File Offset: 0x0000A0EE
		public EdmPathExpression(string path)
		{
			EdmUtil.CheckArgumentNull<string>(path, "path");
			this.path = path;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000BF09 File Offset: 0x0000A109
		public EdmPathExpression(params string[] pathSegments)
			: this(pathSegments)
		{
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000BF14 File Offset: 0x0000A114
		public EdmPathExpression(IEnumerable<string> pathSegments)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<string>>(pathSegments, "pathSegments");
			if (Enumerable.Any<string>(pathSegments, (string segment) => segment.Contains("/")))
			{
				throw new ArgumentException(Strings.PathSegmentMustNotContainSlash);
			}
			this.pathSegments = Enumerable.ToList<string>(pathSegments);
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000BF74 File Offset: 0x0000A174
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		public string Path
		{
			get
			{
				string text;
				if ((text = this.path) == null)
				{
					text = (this.path = string.Join("/", Enumerable.ToArray<string>(this.pathSegments)));
				}
				return text;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000BFE1 File Offset: 0x0000A1E1
		public virtual EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Path;
			}
		}

		// Token: 0x040000F5 RID: 245
		private IEnumerable<string> pathSegments;

		// Token: 0x040000F6 RID: 246
		private string path;
	}
}
