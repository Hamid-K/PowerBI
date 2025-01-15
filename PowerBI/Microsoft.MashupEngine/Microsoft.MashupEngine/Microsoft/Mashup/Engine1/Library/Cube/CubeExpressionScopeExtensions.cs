using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CEF RID: 3311
	internal static class CubeExpressionScopeExtensions
	{
		// Token: 0x060059D6 RID: 22998 RVA: 0x0013A4A0 File Offset: 0x001386A0
		public static IdentifierCubeExpression NewScope(this IdentifierCubeExpression identifier, string scope)
		{
			ScopePath scopePath;
			return identifier.GetUnscoped(out scopePath).SetScopePath(scopePath.NewScope(scope));
		}

		// Token: 0x060059D7 RID: 22999 RVA: 0x0013A4C4 File Offset: 0x001386C4
		public static IdentifierCubeExpression SetScopePath(this IdentifierCubeExpression identifier, ScopePath path)
		{
			string text = identifier.Identifier;
			int num = text.IndexOf('/');
			if (num != -1)
			{
				text = text.Substring(num + 1);
			}
			if (path.Equals(ScopePath.Default))
			{
				return identifier;
			}
			return new IdentifierCubeExpression(path.ToString() + "/" + text);
		}

		// Token: 0x060059D8 RID: 23000 RVA: 0x0013A514 File Offset: 0x00138714
		public static IdentifierCubeExpression GetUnscoped(this IdentifierCubeExpression identifier, out ScopePath path)
		{
			string identifier2 = identifier.Identifier;
			int num = identifier2.IndexOf('/');
			if (num != -1)
			{
				path = ScopePath.FromString(identifier2.Substring(0, num));
				return new IdentifierCubeExpression(identifier2.Substring(num + 1));
			}
			path = ScopePath.Default;
			return identifier;
		}

		// Token: 0x060059D9 RID: 23001 RVA: 0x0013A55B File Offset: 0x0013875B
		public static CubeExpression NewScope(this CubeExpression expression, string scope)
		{
			return CubeExpressionScopeExtensions.newScopeVisitor.NewScope(expression, scope);
		}

		// Token: 0x060059DA RID: 23002 RVA: 0x0013A569 File Offset: 0x00138769
		public static CubeExpression GetUnscoped(this CubeExpression expression, out IList<ScopePath> paths)
		{
			return CubeExpressionScopeExtensions.unscopeVisitor.GetUnscoped(expression, out paths);
		}

		// Token: 0x060059DB RID: 23003 RVA: 0x0013A577 File Offset: 0x00138777
		public static CubeExpression ReplaceScopePaths(this CubeExpression expression, IDictionary<ScopePath, ScopePath> replacements)
		{
			return CubeExpressionScopeExtensions.replaceScopePathsVisitor.ReplaceScopePaths(expression, replacements);
		}

		// Token: 0x060059DC RID: 23004 RVA: 0x0013A585 File Offset: 0x00138785
		public static CubeSortOrder ReplaceScopePaths(this CubeSortOrder order, IDictionary<ScopePath, ScopePath> replacements)
		{
			return new CubeSortOrder(order.Expression.ReplaceScopePaths(replacements), order.Ascending);
		}

		// Token: 0x04003233 RID: 12851
		private static readonly CubeExpressionScopeExtensions.NewScopeCubeExpressionVisitor newScopeVisitor = new CubeExpressionScopeExtensions.NewScopeCubeExpressionVisitor();

		// Token: 0x04003234 RID: 12852
		private static readonly CubeExpressionScopeExtensions.UnscopeCubeExpressionVisitor unscopeVisitor = new CubeExpressionScopeExtensions.UnscopeCubeExpressionVisitor();

		// Token: 0x04003235 RID: 12853
		private static readonly CubeExpressionScopeExtensions.ReplaceScopePathsCubeExpressionVisitor replaceScopePathsVisitor = new CubeExpressionScopeExtensions.ReplaceScopePathsCubeExpressionVisitor();

		// Token: 0x02000CF0 RID: 3312
		private class NewScopeCubeExpressionVisitor : CubeExpressionVisitor
		{
			// Token: 0x060059DE RID: 23006 RVA: 0x0013A5C0 File Offset: 0x001387C0
			public CubeExpression NewScope(CubeExpression expression, string scope)
			{
				string text = this.scope;
				CubeExpression cubeExpression;
				try
				{
					this.scope = scope;
					cubeExpression = this.Visit(expression);
				}
				finally
				{
					this.scope = text;
				}
				return cubeExpression;
			}

			// Token: 0x060059DF RID: 23007 RVA: 0x0013A600 File Offset: 0x00138800
			protected override CubeExpression VisitIdentifier(IdentifierCubeExpression identifier)
			{
				return identifier.NewScope(this.scope);
			}

			// Token: 0x04003236 RID: 12854
			private string scope;
		}

		// Token: 0x02000CF1 RID: 3313
		private class UnscopeCubeExpressionVisitor : CubeExpressionVisitor
		{
			// Token: 0x060059E1 RID: 23009 RVA: 0x0013A610 File Offset: 0x00138810
			public CubeExpression GetUnscoped(CubeExpression expression, out IList<ScopePath> paths)
			{
				HashSet<ScopePath> hashSet = this.paths;
				CubeExpression cubeExpression;
				try
				{
					this.paths = new HashSet<ScopePath>();
					expression = this.Visit(expression);
					paths = ((this.paths != null) ? this.paths.ToArray<ScopePath>() : EmptyArray<ScopePath>.Instance);
					cubeExpression = expression;
				}
				finally
				{
					this.paths = hashSet;
				}
				return cubeExpression;
			}

			// Token: 0x060059E2 RID: 23010 RVA: 0x0013A674 File Offset: 0x00138874
			protected override CubeExpression VisitIdentifier(IdentifierCubeExpression identifier)
			{
				ScopePath scopePath;
				identifier = identifier.GetUnscoped(out scopePath);
				this.paths.Add(scopePath);
				return identifier;
			}

			// Token: 0x04003237 RID: 12855
			private HashSet<ScopePath> paths;
		}

		// Token: 0x02000CF2 RID: 3314
		private class ReplaceScopePathsCubeExpressionVisitor : CubeExpressionVisitor
		{
			// Token: 0x060059E4 RID: 23012 RVA: 0x0013A69C File Offset: 0x0013889C
			public CubeExpression ReplaceScopePaths(CubeExpression expression, IDictionary<ScopePath, ScopePath> replacements)
			{
				IDictionary<ScopePath, ScopePath> dictionary = this.replacements;
				CubeExpression cubeExpression;
				try
				{
					this.replacements = replacements;
					cubeExpression = this.Visit(expression);
				}
				finally
				{
					this.replacements = dictionary;
				}
				return cubeExpression;
			}

			// Token: 0x060059E5 RID: 23013 RVA: 0x0013A6DC File Offset: 0x001388DC
			protected override CubeExpression VisitIdentifier(IdentifierCubeExpression identifier)
			{
				ScopePath scopePath;
				IdentifierCubeExpression unscoped = identifier.GetUnscoped(out scopePath);
				ScopePath scopePath2;
				if (this.replacements.TryGetValue(scopePath, out scopePath2))
				{
					identifier = unscoped.SetScopePath(scopePath2);
				}
				return identifier;
			}

			// Token: 0x04003238 RID: 12856
			private IDictionary<ScopePath, ScopePath> replacements;
		}
	}
}
