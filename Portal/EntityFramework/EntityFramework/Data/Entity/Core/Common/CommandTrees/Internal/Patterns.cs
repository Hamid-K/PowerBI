using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x020006F2 RID: 1778
	internal static class Patterns
	{
		// Token: 0x0600529C RID: 21148 RVA: 0x001289EB File Offset: 0x00126BEB
		internal static Func<DbExpression, bool> And(Func<DbExpression, bool> pattern1, Func<DbExpression, bool> pattern2)
		{
			return (DbExpression e) => pattern1(e) && pattern2(e);
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x00128A0B File Offset: 0x00126C0B
		internal static Func<DbExpression, bool> And(Func<DbExpression, bool> pattern1, Func<DbExpression, bool> pattern2, Func<DbExpression, bool> pattern3)
		{
			return (DbExpression e) => pattern1(e) && pattern2(e) && pattern3(e);
		}

		// Token: 0x0600529E RID: 21150 RVA: 0x00128A32 File Offset: 0x00126C32
		internal static Func<DbExpression, bool> Or(Func<DbExpression, bool> pattern1, Func<DbExpression, bool> pattern2)
		{
			return (DbExpression e) => pattern1(e) || pattern2(e);
		}

		// Token: 0x0600529F RID: 21151 RVA: 0x00128A52 File Offset: 0x00126C52
		internal static Func<DbExpression, bool> Or(Func<DbExpression, bool> pattern1, Func<DbExpression, bool> pattern2, Func<DbExpression, bool> pattern3)
		{
			return (DbExpression e) => pattern1(e) || pattern2(e) || pattern3(e);
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x060052A0 RID: 21152 RVA: 0x00128A79 File Offset: 0x00126C79
		internal static Func<DbExpression, bool> AnyExpression
		{
			get
			{
				return (DbExpression e) => true;
			}
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x060052A1 RID: 21153 RVA: 0x00128A9A File Offset: 0x00126C9A
		internal static Func<IEnumerable<DbExpression>, bool> AnyExpressions
		{
			get
			{
				return (IEnumerable<DbExpression> elems) => true;
			}
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x060052A2 RID: 21154 RVA: 0x00128ABB File Offset: 0x00126CBB
		internal static Func<DbExpression, bool> MatchComplexType
		{
			get
			{
				return (DbExpression e) => TypeSemantics.IsComplexType(e.ResultType);
			}
		}

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x060052A3 RID: 21155 RVA: 0x00128ADC File Offset: 0x00126CDC
		internal static Func<DbExpression, bool> MatchEntityType
		{
			get
			{
				return (DbExpression e) => TypeSemantics.IsEntityType(e.ResultType);
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x060052A4 RID: 21156 RVA: 0x00128AFD File Offset: 0x00126CFD
		internal static Func<DbExpression, bool> MatchRowType
		{
			get
			{
				return (DbExpression e) => TypeSemantics.IsRowType(e.ResultType);
			}
		}

		// Token: 0x060052A5 RID: 21157 RVA: 0x00128B1E File Offset: 0x00126D1E
		internal static Func<DbExpression, bool> MatchKind(DbExpressionKind kindToMatch)
		{
			return (DbExpression e) => e.ExpressionKind == kindToMatch;
		}

		// Token: 0x060052A6 RID: 21158 RVA: 0x00128B37 File Offset: 0x00126D37
		internal static Func<IEnumerable<DbExpression>, bool> MatchForAll(Func<DbExpression, bool> elementPattern)
		{
			Func<DbExpression, bool> <>9__1;
			return delegate(IEnumerable<DbExpression> elems)
			{
				Func<DbExpression, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (DbExpression e) => !elementPattern(e));
				}
				return elems.FirstOrDefault(func) == null;
			};
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x00128B50 File Offset: 0x00126D50
		internal static Func<DbExpression, bool> MatchBinary()
		{
			return (DbExpression e) => e is DbBinaryExpression;
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x00128B71 File Offset: 0x00126D71
		internal static Func<DbExpression, bool> MatchFilter(Func<DbExpression, bool> inputPattern, Func<DbExpression, bool> predicatePattern)
		{
			return delegate(DbExpression e)
			{
				if (e.ExpressionKind != DbExpressionKind.Filter)
				{
					return false;
				}
				DbFilterExpression dbFilterExpression = (DbFilterExpression)e;
				return inputPattern(dbFilterExpression.Input.Expression) && predicatePattern(dbFilterExpression.Predicate);
			};
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x00128B91 File Offset: 0x00126D91
		internal static Func<DbExpression, bool> MatchProject(Func<DbExpression, bool> inputPattern, Func<DbExpression, bool> projectionPattern)
		{
			return delegate(DbExpression e)
			{
				if (e.ExpressionKind != DbExpressionKind.Project)
				{
					return false;
				}
				DbProjectExpression dbProjectExpression = (DbProjectExpression)e;
				return inputPattern(dbProjectExpression.Input.Expression) && projectionPattern(dbProjectExpression.Projection);
			};
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x00128BB1 File Offset: 0x00126DB1
		internal static Func<DbExpression, bool> MatchCase(Func<IEnumerable<DbExpression>, bool> whenPattern, Func<IEnumerable<DbExpression>, bool> thenPattern, Func<DbExpression, bool> elsePattern)
		{
			return delegate(DbExpression e)
			{
				if (e.ExpressionKind != DbExpressionKind.Case)
				{
					return false;
				}
				DbCaseExpression dbCaseExpression = (DbCaseExpression)e;
				return whenPattern(dbCaseExpression.When) && thenPattern(dbCaseExpression.Then) && elsePattern(dbCaseExpression.Else);
			};
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x00128BD8 File Offset: 0x00126DD8
		internal static Func<DbExpression, bool> MatchNewInstance()
		{
			return (DbExpression e) => e.ExpressionKind == DbExpressionKind.NewInstance;
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x00128BF9 File Offset: 0x00126DF9
		internal static Func<DbExpression, bool> MatchNewInstance(Func<IEnumerable<DbExpression>, bool> argumentsPattern)
		{
			return delegate(DbExpression e)
			{
				if (e.ExpressionKind != DbExpressionKind.NewInstance)
				{
					return false;
				}
				DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)e;
				return argumentsPattern(dbNewInstanceExpression.Arguments);
			};
		}
	}
}
