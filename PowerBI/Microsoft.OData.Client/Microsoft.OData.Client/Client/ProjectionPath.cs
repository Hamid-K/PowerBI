using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

namespace Microsoft.OData.Client
{
	// Token: 0x0200005C RID: 92
	[DebuggerDisplay("{ToString()}")]
	internal class ProjectionPath : List<ProjectionPathSegment>
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0000B063 File Offset: 0x00009263
		internal ProjectionPath()
		{
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000B06B File Offset: 0x0000926B
		internal ProjectionPath(ParameterExpression root, Expression expectedRootType, Expression rootEntry)
		{
			this.Root = root;
			this.RootEntry = rootEntry;
			this.ExpectedRootType = expectedRootType;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000B088 File Offset: 0x00009288
		internal ProjectionPath(ParameterExpression root, Expression expectedRootType, Expression rootEntry, IEnumerable<Expression> members)
			: this(root, expectedRootType, rootEntry)
		{
			foreach (Expression expression in members)
			{
				base.Add(new ProjectionPathSegment(this, (MemberExpression)expression));
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000B0E8 File Offset: 0x000092E8
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000B0F0 File Offset: 0x000092F0
		internal ParameterExpression Root { get; private set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000B0F9 File Offset: 0x000092F9
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0000B101 File Offset: 0x00009301
		internal Expression RootEntry { get; private set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000B10A File Offset: 0x0000930A
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000B112 File Offset: 0x00009312
		internal Expression ExpectedRootType { get; private set; }

		// Token: 0x060002E9 RID: 745 RVA: 0x0000B11C File Offset: 0x0000931C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Root.ToString());
			stringBuilder.Append("->");
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i].SourceTypeAs != null)
				{
					stringBuilder.Insert(0, "(");
					stringBuilder.Append(" as " + base[i].SourceTypeAs.Name + ")");
				}
				if (i > 0)
				{
					stringBuilder.Append('.');
				}
				stringBuilder.Append((base[i].Member == null) ? "*" : base[i].Member);
			}
			return stringBuilder.ToString();
		}
	}
}
