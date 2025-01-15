using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001883 RID: 6275
	internal static class PythonExpressionHelper
	{
		// Token: 0x0600CC6F RID: 52335 RVA: 0x002B96F4 File Offset: 0x002B78F4
		public static FormulaExpression AggregateFunc(string name, IEnumerable<FormulaExpression> terms)
		{
			terms = terms.ToReadOnlyList<FormulaExpression>();
			if (terms.Any((FormulaExpression t) => !(t is IFormulaTyped)))
			{
				return PythonExpressionHelper.Func(name, new FormulaExpression[] { PythonExpressionHelper.Array(terms) });
			}
			List<Type> list = (from IFormulaTyped t in terms
				select t.Type).ToList<Type>();
			Type type = ((list.Distinct<Type>().Count<Type>() == 1) ? list.First<Type>() : typeof(object));
			return PythonExpressionHelper.Func(name, type, new FormulaExpression[] { PythonExpressionHelper.Array(terms) });
		}

		// Token: 0x0600CC70 RID: 52336 RVA: 0x002B97AD File Offset: 0x002B79AD
		public static FormulaExpression And(FormulaExpression left, FormulaExpression right)
		{
			return new PythonAnd(left, right);
		}

		// Token: 0x0600CC71 RID: 52337 RVA: 0x002B97B6 File Offset: 0x002B79B6
		public static FormulaExpression Array(IEnumerable<FormulaExpression> items)
		{
			return new PythonArray(items);
		}

		// Token: 0x0600CC72 RID: 52338 RVA: 0x002B97BE File Offset: 0x002B79BE
		public static FormulaExpression Array(IEnumerable<string> items)
		{
			Func<string, FormulaExpression> func;
			if ((func = PythonExpressionHelper.<>O.<0>__StringLiteral) == null)
			{
				func = (PythonExpressionHelper.<>O.<0>__StringLiteral = new Func<string, FormulaExpression>(PythonExpressionHelper.StringLiteral));
			}
			return PythonExpressionHelper.Array(items.Select(func));
		}

		// Token: 0x0600CC73 RID: 52339 RVA: 0x002B97E6 File Offset: 0x002B79E6
		public static FormulaExpression Assign(string left, double right)
		{
			return PythonExpressionHelper.Assign(PythonExpressionHelper.Variable(left), PythonExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600CC74 RID: 52340 RVA: 0x002B97F9 File Offset: 0x002B79F9
		public static FormulaExpression Assign<T>(string left, FormulaExpression right)
		{
			return PythonExpressionHelper.Assign(PythonExpressionHelper.Variable<T>(left), right);
		}

		// Token: 0x0600CC75 RID: 52341 RVA: 0x002B9807 File Offset: 0x002B7A07
		public static FormulaExpression Assign(FormulaExpression left, double right)
		{
			return PythonExpressionHelper.Assign(left, PythonExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600CC76 RID: 52342 RVA: 0x002B9815 File Offset: 0x002B7A15
		public static FormulaExpression Assign(FormulaExpression left, FormulaExpression right)
		{
			return new PythonAssign(left, right, false);
		}

		// Token: 0x0600CC77 RID: 52343 RVA: 0x002B981F File Offset: 0x002B7A1F
		public static FormulaExpression AssignArg(string left, double right)
		{
			return PythonExpressionHelper.AssignArg(PythonExpressionHelper.Variable(left), PythonExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600CC78 RID: 52344 RVA: 0x002B9832 File Offset: 0x002B7A32
		public static FormulaExpression AssignArg(string left, string right)
		{
			return PythonExpressionHelper.AssignArg(PythonExpressionHelper.Variable(left), PythonExpressionHelper.StringLiteral(right));
		}

		// Token: 0x0600CC79 RID: 52345 RVA: 0x002B9845 File Offset: 0x002B7A45
		public static FormulaExpression AssignArg(string left, FormulaExpression right)
		{
			return PythonExpressionHelper.AssignArg(PythonExpressionHelper.Variable<object>(left), right);
		}

		// Token: 0x0600CC7A RID: 52346 RVA: 0x002B9853 File Offset: 0x002B7A53
		public static FormulaExpression AssignArg<T>(string left, FormulaExpression right)
		{
			return PythonExpressionHelper.AssignArg(PythonExpressionHelper.Variable<T>(left), right);
		}

		// Token: 0x0600CC7B RID: 52347 RVA: 0x002B9861 File Offset: 0x002B7A61
		public static FormulaExpression AssignArg(FormulaExpression left, double right)
		{
			return PythonExpressionHelper.AssignArg(left, PythonExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600CC7C RID: 52348 RVA: 0x002B986F File Offset: 0x002B7A6F
		public static FormulaExpression AssignArg(FormulaExpression left, FormulaExpression right)
		{
			return new PythonAssign(left, right, true);
		}

		// Token: 0x0600CC7D RID: 52349 RVA: 0x002B9879 File Offset: 0x002B7A79
		public static PythonBlock Block(IEnumerable<FormulaExpression> statements)
		{
			return new PythonBlock(statements);
		}

		// Token: 0x0600CC7E RID: 52350 RVA: 0x002B9881 File Offset: 0x002B7A81
		public static FormulaExpression Ceil(FormulaExpression subject)
		{
			return PythonExpressionHelper.Func<double>("math.ceil", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CC7F RID: 52351 RVA: 0x002B9897 File Offset: 0x002B7A97
		public static PythonComment Comment(string comment, PythonCommentType type)
		{
			return new PythonComment(comment, type);
		}

		// Token: 0x0600CC80 RID: 52352 RVA: 0x002B98A0 File Offset: 0x002B7AA0
		public static FormulaExpression Concat(FormulaExpression left, FormulaExpression right)
		{
			PythonStringLiteral pythonStringLiteral = left as PythonStringLiteral;
			if (pythonStringLiteral != null)
			{
				PythonStringLiteral pythonStringLiteral2 = right as PythonStringLiteral;
				if (pythonStringLiteral2 != null)
				{
					return PythonExpressionHelper.StringLiteral(pythonStringLiteral.Value + pythonStringLiteral2.Value);
				}
			}
			return new PythonConcat(left, right);
		}

		// Token: 0x0600CC81 RID: 52353 RVA: 0x002B98DF File Offset: 0x002B7ADF
		public static FormulaExpression Count(FormulaExpression subject, FormulaExpression findText)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<int>("count", new FormulaExpression[] { findText }));
		}

		// Token: 0x0600CC82 RID: 52354 RVA: 0x002B98FB File Offset: 0x002B7AFB
		public static FormulaExpression DateTimeLiteral(DateTime value)
		{
			return new PythonDateTimeLiteral(value);
		}

		// Token: 0x0600CC83 RID: 52355 RVA: 0x002B9904 File Offset: 0x002B7B04
		public static FormulaExpression DelimiterIndexEnumeration(FormulaExpression subject, FormulaExpression delimiter)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable<object>("i");
			return new PythonEnumeration(formulaExpression, formulaExpression, PythonExpressionHelper.Range(PythonExpressionHelper.Len(subject)), PythonExpressionHelper.StartsWith(subject, delimiter, formulaExpression));
		}

		// Token: 0x0600CC84 RID: 52356 RVA: 0x002B9936 File Offset: 0x002B7B36
		public static FormulaExpression Dot(string subject, FormulaExpression accessor)
		{
			return new PythonDot(PythonExpressionHelper.Variable<object>(subject), accessor);
		}

		// Token: 0x0600CC85 RID: 52357 RVA: 0x002B9944 File Offset: 0x002B7B44
		public static FormulaExpression Dot(string subject, string accessor)
		{
			return new PythonDot(PythonExpressionHelper.Variable<object>(subject), PythonExpressionHelper.Variable<object>(accessor));
		}

		// Token: 0x0600CC86 RID: 52358 RVA: 0x002B9957 File Offset: 0x002B7B57
		public static FormulaExpression Dot(FormulaExpression subject, string accessor)
		{
			return new PythonDot(subject, PythonExpressionHelper.Variable<object>(accessor));
		}

		// Token: 0x0600CC87 RID: 52359 RVA: 0x002B9965 File Offset: 0x002B7B65
		public static FormulaExpression Dot<T>(FormulaExpression subject, string accessor)
		{
			if (subject is IFormulaBinaryOperator)
			{
				subject = PythonExpressionHelper.Parenthesis(subject);
			}
			return new PythonDot(subject, PythonExpressionHelper.Variable<T>(accessor));
		}

		// Token: 0x0600CC88 RID: 52360 RVA: 0x002B9984 File Offset: 0x002B7B84
		public static FormulaExpression Dot(FormulaExpression subject, FormulaExpression accessor)
		{
			PythonDot pythonDot = new PythonDot(subject, accessor);
			if (!(subject is FormulaBinaryOperator))
			{
				return pythonDot;
			}
			return PythonExpressionHelper.Parenthesis(pythonDot);
		}

		// Token: 0x0600CC89 RID: 52361 RVA: 0x002B99A9 File Offset: 0x002B7BA9
		public static FormulaExpression DotFunc<T>(string subject, string funcName)
		{
			return PythonExpressionHelper.DotFunc<T>(PythonExpressionHelper.Variable(subject), funcName);
		}

		// Token: 0x0600CC8A RID: 52362 RVA: 0x002B99B7 File Offset: 0x002B7BB7
		public static FormulaExpression DotFunc<T>(FormulaExpression subject, string funcName)
		{
			return new PythonDot(subject, PythonExpressionHelper.Func<T>(funcName));
		}

		// Token: 0x0600CC8B RID: 52363 RVA: 0x002B99C5 File Offset: 0x002B7BC5
		public static FormulaExpression DotFunc<T>(string subject, string funcName, params FormulaExpression[] arguments)
		{
			return PythonExpressionHelper.DotFunc<T>(PythonExpressionHelper.Variable(subject), funcName, arguments);
		}

		// Token: 0x0600CC8C RID: 52364 RVA: 0x002B99D4 File Offset: 0x002B7BD4
		public static FormulaExpression DotFunc<T>(FormulaExpression subject, string funcName, params FormulaExpression[] arguments)
		{
			if (subject is IFormulaBinaryOperator)
			{
				subject = PythonExpressionHelper.Parenthesis(subject);
			}
			return new PythonDot(subject, PythonExpressionHelper.Func<T>(funcName, arguments));
		}

		// Token: 0x0600CC8D RID: 52365 RVA: 0x002B99F3 File Offset: 0x002B7BF3
		public static FormulaExpression End(FormulaExpression subject)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<int>("end"));
		}

		// Token: 0x0600CC8E RID: 52366 RVA: 0x002B9A05 File Offset: 0x002B7C05
		public static FormulaExpression EndsWith(FormulaExpression subject, FormulaExpression find)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<bool>("endswith", new FormulaExpression[] { find }));
		}

		// Token: 0x0600CC8F RID: 52367 RVA: 0x002B9A21 File Offset: 0x002B7C21
		public static FormulaExpression Enumeration(FormulaExpression selector, FormulaExpression index, FormulaExpression iterator, FormulaExpression condition = null)
		{
			return new PythonEnumeration(selector, index, iterator, condition);
		}

		// Token: 0x0600CC90 RID: 52368 RVA: 0x002B9A2C File Offset: 0x002B7C2C
		public static FormulaExpression False()
		{
			return new PythonBooleanLiteral(false);
		}

		// Token: 0x0600CC91 RID: 52369 RVA: 0x002B9A34 File Offset: 0x002B7C34
		public static FormulaExpression Find(FormulaExpression subject, FormulaExpression find, FormulaExpression startAt)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<string>("find", new FormulaExpression[] { find, startAt }));
		}

		// Token: 0x0600CC92 RID: 52370 RVA: 0x002B9A54 File Offset: 0x002B7C54
		public static FormulaExpression Find(FormulaExpression subject, FormulaExpression find)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<string>("find", new FormulaExpression[] { find }));
		}

		// Token: 0x0600CC93 RID: 52371 RVA: 0x002B9A70 File Offset: 0x002B7C70
		public static FormulaExpression FindIter(FormulaExpression subject, FormulaExpression pattern, string libraryName)
		{
			return PythonExpressionHelper.Dot(PythonExpressionHelper.Variable<object>(libraryName), PythonExpressionHelper.Func<object>("finditer", new FormulaExpression[] { pattern, subject }));
		}

		// Token: 0x0600CC94 RID: 52372 RVA: 0x002B9A95 File Offset: 0x002B7C95
		public static FormulaExpression Func<TReturnType>(string name)
		{
			return PythonExpressionHelper.Func(name, typeof(TReturnType), Enumerable.Empty<FormulaExpression>().ToArray<FormulaExpression>());
		}

		// Token: 0x0600CC95 RID: 52373 RVA: 0x002B9AB1 File Offset: 0x002B7CB1
		public static FormulaExpression Func(string name)
		{
			return PythonExpressionHelper.Func(name, typeof(object), Enumerable.Empty<FormulaExpression>().ToArray<FormulaExpression>());
		}

		// Token: 0x0600CC96 RID: 52374 RVA: 0x002B9ACD File Offset: 0x002B7CCD
		public static FormulaExpression Func<TReturnType>(string name, params FormulaExpression[] arguments)
		{
			return PythonExpressionHelper.Func(name, typeof(TReturnType), arguments);
		}

		// Token: 0x0600CC97 RID: 52375 RVA: 0x002B9AE0 File Offset: 0x002B7CE0
		public static FormulaExpression Func(string name, params FormulaExpression[] arguments)
		{
			return new PythonFunc(name, typeof(object), arguments);
		}

		// Token: 0x0600CC98 RID: 52376 RVA: 0x002B9AF3 File Offset: 0x002B7CF3
		public static FormulaExpression Func(string name, Type returnType, params FormulaExpression[] arguments)
		{
			return new PythonFunc(name, returnType, arguments);
		}

		// Token: 0x0600CC99 RID: 52377 RVA: 0x002B9AFD File Offset: 0x002B7CFD
		public static FormulaExpression Group(FormulaExpression subject)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<object>("group"));
		}

		// Token: 0x0600CC9A RID: 52378 RVA: 0x002B9B0F File Offset: 0x002B7D0F
		public static FormulaExpression If(FormulaExpression condition, PythonBlock trueBlock)
		{
			return new PythonIf(condition, trueBlock, null);
		}

		// Token: 0x0600CC9B RID: 52379 RVA: 0x002B9B19 File Offset: 0x002B7D19
		public static FormulaExpression If(FormulaExpression condition, PythonBlock trueBlock, PythonBlock falseBlock)
		{
			return new PythonIf(condition, trueBlock, falseBlock);
		}

		// Token: 0x0600CC9C RID: 52380 RVA: 0x002B9B23 File Offset: 0x002B7D23
		public static PythonImport Import(string module)
		{
			return new PythonImport(module, null, null);
		}

		// Token: 0x0600CC9D RID: 52381 RVA: 0x002B9B2D File Offset: 0x002B7D2D
		public static PythonImport Import(string module, HashSet<string> objects)
		{
			return new PythonImport(module, objects, null);
		}

		// Token: 0x0600CC9E RID: 52382 RVA: 0x002B9B37 File Offset: 0x002B7D37
		public static PythonImport Import(string module, string alias)
		{
			return new PythonImport(module, null, alias);
		}

		// Token: 0x0600CC9F RID: 52383 RVA: 0x002B9B41 File Offset: 0x002B7D41
		public static PythonImport Import(string module, string submodule, string alias)
		{
			return new PythonImport(module, new HashSet<string>(new string[] { submodule }), alias);
		}

		// Token: 0x0600CCA0 RID: 52384 RVA: 0x002B9B59 File Offset: 0x002B7D59
		public static FormulaExpression Index<T>(FormulaExpression subject, int index)
		{
			return PythonExpressionHelper.Index<T>(subject, PythonExpressionHelper.NumberLiteral(index));
		}

		// Token: 0x0600CCA1 RID: 52385 RVA: 0x002B9B67 File Offset: 0x002B7D67
		public static FormulaExpression Index<T>(FormulaExpression subject, FormulaExpression index)
		{
			return new PythonIndex(subject, index, typeof(T));
		}

		// Token: 0x0600CCA2 RID: 52386 RVA: 0x002B9B7A File Offset: 0x002B7D7A
		public static FormulaExpression IndexRange(int? start, int? end)
		{
			return PythonExpressionHelper.IndexRange((start == null) ? PythonExpressionHelper.NumberLiteral(0) : PythonExpressionHelper.NumberLiteral(start.Value), (end == null) ? null : PythonExpressionHelper.NumberLiteral(end.Value));
		}

		// Token: 0x0600CCA3 RID: 52387 RVA: 0x002B9BB6 File Offset: 0x002B7DB6
		public static FormulaExpression IndexRange(FormulaExpression start, FormulaExpression end)
		{
			return new PythonIndexRange(start, end);
		}

		// Token: 0x0600CCA4 RID: 52388 RVA: 0x002B9BBF File Offset: 0x002B7DBF
		public static FormulaExpression InterpolatedFormat(FormulaExpression input, FormulaExpression mask, string currencySymbol = null, bool currencyPrefix = true)
		{
			return new PythonInterpolatedFormat(input, mask, currencySymbol, currencyPrefix);
		}

		// Token: 0x0600CCA5 RID: 52389 RVA: 0x002B9BCA File Offset: 0x002B7DCA
		public static FormulaExpression IsAlpha(FormulaExpression subject)
		{
			return PythonExpressionHelper.Dot(PythonExpressionHelper.Variable("str"), PythonExpressionHelper.Func("isalpha", new FormulaExpression[] { subject }));
		}

		// Token: 0x0600CCA6 RID: 52390 RVA: 0x002B9BEF File Offset: 0x002B7DEF
		public static FormulaExpression IsDigit(FormulaExpression subject)
		{
			return PythonExpressionHelper.Dot(PythonExpressionHelper.Variable("str"), PythonExpressionHelper.Func("isdigit", new FormulaExpression[] { subject }));
		}

		// Token: 0x0600CCA7 RID: 52391 RVA: 0x002B9C14 File Offset: 0x002B7E14
		public static FormulaExpression IsInstanceFloat(FormulaExpression subject)
		{
			return PythonExpressionHelper.Func("isinstance", new FormulaExpression[]
			{
				subject,
				PythonExpressionHelper.Variable("float")
			});
		}

		// Token: 0x0600CCA8 RID: 52392 RVA: 0x002B9C37 File Offset: 0x002B7E37
		public static FormulaExpression IsInstanceInt(FormulaExpression subject)
		{
			return PythonExpressionHelper.Func("isinstance", new FormulaExpression[]
			{
				subject,
				PythonExpressionHelper.Variable("int")
			});
		}

		// Token: 0x0600CCA9 RID: 52393 RVA: 0x002B9C5A File Offset: 0x002B7E5A
		public static FormulaExpression IsInstanceStr(FormulaExpression subject)
		{
			return PythonExpressionHelper.Func("isinstance", new FormulaExpression[]
			{
				subject,
				PythonExpressionHelper.Variable("str")
			});
		}

		// Token: 0x0600CCAA RID: 52394 RVA: 0x002B9C7D File Offset: 0x002B7E7D
		public static FormulaExpression IsNullOrWhiteSpace(FormulaExpression subject)
		{
			return PythonExpressionHelper.Not(PythonExpressionHelper.Strip(subject));
		}

		// Token: 0x0600CCAB RID: 52395 RVA: 0x002B9C8A File Offset: 0x002B7E8A
		public static FormulaExpression IsNumeric(FormulaExpression subject)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func("isnumeric"));
		}

		// Token: 0x0600CCAC RID: 52396 RVA: 0x002B9C9C File Offset: 0x002B7E9C
		public static FormulaExpression Lambda(IEnumerable<FormulaExpression> arguments, FormulaExpression body)
		{
			return new PythonLambda(arguments, body);
		}

		// Token: 0x0600CCAD RID: 52397 RVA: 0x002B9CA5 File Offset: 0x002B7EA5
		public static FormulaExpression Len(FormulaExpression str)
		{
			return PythonExpressionHelper.Func<int>("len", new FormulaExpression[] { str });
		}

		// Token: 0x0600CCAE RID: 52398 RVA: 0x002B9CBC File Offset: 0x002B7EBC
		public static FormulaExpression Lower(FormulaExpression subject)
		{
			IFormulaBinaryOperator formulaBinaryOperator = subject as IFormulaBinaryOperator;
			if (formulaBinaryOperator != null && formulaBinaryOperator.Precedence < 10)
			{
				subject = PythonExpressionHelper.Parenthesis(subject);
			}
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<string>("lower"));
		}

		// Token: 0x0600CCAF RID: 52399 RVA: 0x002B9CF5 File Offset: 0x002B7EF5
		public static FormulaExpression Match(FormulaExpression subject, FormulaExpression pattern, string libraryName)
		{
			return PythonExpressionHelper.Dot(PythonExpressionHelper.Variable<object>(libraryName), PythonExpressionHelper.Func<object>("match", new FormulaExpression[] { pattern, subject }));
		}

		// Token: 0x0600CCB0 RID: 52400 RVA: 0x002B9D1A File Offset: 0x002B7F1A
		public static FormulaExpression MatchFull(FormulaExpression subject, FormulaExpression pattern, string libraryName)
		{
			return PythonExpressionHelper.Dot(PythonExpressionHelper.Variable<object>(libraryName), PythonExpressionHelper.Func<string>("fullmatch", new FormulaExpression[] { pattern, subject }));
		}

		// Token: 0x0600CCB1 RID: 52401 RVA: 0x002B9D40 File Offset: 0x002B7F40
		public static FormulaExpression MatchIndexEnumeration(FormulaExpression subject, FormulaExpression pattern, string libraryName)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable<object>("i");
			FormulaExpression formulaExpression2 = PythonExpressionHelper.FindIter(subject, pattern, libraryName);
			return new PythonEnumeration(formulaExpression, formulaExpression, formulaExpression2, null);
		}

		// Token: 0x0600CCB2 RID: 52402 RVA: 0x002B9D68 File Offset: 0x002B7F68
		public static FormulaExpression MatchSearch(FormulaExpression subject, FormulaExpression pattern, string libraryName)
		{
			return PythonExpressionHelper.Dot(PythonExpressionHelper.Variable<object>(libraryName), PythonExpressionHelper.Func<string>("search", new FormulaExpression[] { pattern, subject }));
		}

		// Token: 0x0600CCB3 RID: 52403 RVA: 0x002B9D8D File Offset: 0x002B7F8D
		public static FormulaExpression MatchFindAll(FormulaExpression subject, FormulaExpression pattern, string libraryName)
		{
			return PythonExpressionHelper.Dot(PythonExpressionHelper.Variable<object>(libraryName), PythonExpressionHelper.Func<string>("findall", new FormulaExpression[] { pattern, subject }));
		}

		// Token: 0x0600CCB4 RID: 52404 RVA: 0x002B9DB2 File Offset: 0x002B7FB2
		public static FormulaExpression Midpoint(FormulaExpression start, FormulaExpression end)
		{
			return PythonExpressionHelper.Plus(start, PythonExpressionHelper.Divide(PythonExpressionHelper.Minus(end, start), 2.0));
		}

		// Token: 0x0600CCB5 RID: 52405 RVA: 0x002B9DCF File Offset: 0x002B7FCF
		public static FormulaExpression Negate(FormulaExpression subject)
		{
			return new PythonNegate((subject is IFormulaBinaryOperator) ? PythonExpressionHelper.Parenthesis(subject) : subject);
		}

		// Token: 0x0600CCB6 RID: 52406 RVA: 0x002B9DE7 File Offset: 0x002B7FE7
		public static FormulaExpression None()
		{
			return PythonExpressionHelper.Variable("None");
		}

		// Token: 0x0600CCB7 RID: 52407 RVA: 0x002B9DF3 File Offset: 0x002B7FF3
		public static FormulaExpression Not(FormulaExpression subject)
		{
			return new PythonNot((subject is IFormulaBinaryOperator) ? PythonExpressionHelper.Parenthesis(subject) : subject);
		}

		// Token: 0x0600CCB8 RID: 52408 RVA: 0x002B9E0B File Offset: 0x002B800B
		public static FormulaExpression Or(FormulaExpression left, FormulaExpression right)
		{
			return new PythonOr(left, right);
		}

		// Token: 0x0600CCB9 RID: 52409 RVA: 0x002B9E14 File Offset: 0x002B8014
		public static FormulaExpression Parenthesis(FormulaExpression body)
		{
			return new PythonParenthesis(body);
		}

		// Token: 0x0600CCBA RID: 52410 RVA: 0x002B9E1C File Offset: 0x002B801C
		public static FormulaExpression Print(FormulaExpression subject)
		{
			return PythonExpressionHelper.Func<object>("print", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CCBB RID: 52411 RVA: 0x002B9E32 File Offset: 0x002B8032
		public static FormulaExpression Range(FormulaExpression subject)
		{
			return PythonExpressionHelper.Func<object>("range", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CCBC RID: 52412 RVA: 0x002B9E48 File Offset: 0x002B8048
		public static FormulaExpression Raw(string code)
		{
			return new PythonRawSegment(code);
		}

		// Token: 0x0600CCBD RID: 52413 RVA: 0x002B9E50 File Offset: 0x002B8050
		public static FormulaExpression Raw(params FormulaExpression[] children)
		{
			return new PythonRaw(children);
		}

		// Token: 0x0600CCBE RID: 52414 RVA: 0x002B9E58 File Offset: 0x002B8058
		public static FormulaExpression RegexLiteral(string value)
		{
			return new PythonRegexLiteral(value);
		}

		// Token: 0x0600CCBF RID: 52415 RVA: 0x002B9E60 File Offset: 0x002B8060
		public static FormulaExpression Replace(FormulaExpression subject, FormulaExpression find, FormulaExpression replace)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<string>("replace", new FormulaExpression[] { find, replace }));
		}

		// Token: 0x0600CCC0 RID: 52416 RVA: 0x002B9E80 File Offset: 0x002B8080
		public static FormulaExpression Return(FormulaExpression subject)
		{
			return new PythonReturn(subject);
		}

		// Token: 0x0600CCC1 RID: 52417 RVA: 0x002B9E88 File Offset: 0x002B8088
		public static FormulaExpression RFind(FormulaExpression subject, FormulaExpression find)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<string>("rfind", new FormulaExpression[] { find }));
		}

		// Token: 0x0600CCC2 RID: 52418 RVA: 0x002B9EA4 File Offset: 0x002B80A4
		public static FormulaExpression Split(FormulaExpression subject, FormulaExpression delimiter)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<object>("split", new FormulaExpression[] { delimiter }));
		}

		// Token: 0x0600CCC3 RID: 52419 RVA: 0x002B9EC0 File Offset: 0x002B80C0
		public static FormulaExpression Start(FormulaExpression subject)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<string>("start"));
		}

		// Token: 0x0600CCC4 RID: 52420 RVA: 0x002B9ED2 File Offset: 0x002B80D2
		public static FormulaExpression StartsWith(FormulaExpression subject, FormulaExpression find)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<bool>("startswith", new FormulaExpression[] { find }));
		}

		// Token: 0x0600CCC5 RID: 52421 RVA: 0x002B9EEE File Offset: 0x002B80EE
		public static FormulaExpression StartsWith(FormulaExpression subject, FormulaExpression find, FormulaExpression startAt)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<bool>("startswith", new FormulaExpression[] { find, startAt }));
		}

		// Token: 0x0600CCC6 RID: 52422 RVA: 0x002B9F0E File Offset: 0x002B810E
		public static FormulaExpression Str(FormulaExpression subject)
		{
			return PythonExpressionHelper.Func<string>("str", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CCC7 RID: 52423 RVA: 0x002B9F24 File Offset: 0x002B8124
		public static FormulaExpression StrfTime(FormulaExpression subject, string format)
		{
			return PythonExpressionHelper.StrfTime(subject, PythonExpressionHelper.StringLiteral(format));
		}

		// Token: 0x0600CCC8 RID: 52424 RVA: 0x002B9F32 File Offset: 0x002B8132
		public static FormulaExpression StrfTime(FormulaExpression subject, FormulaExpression format)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<string>("strftime", new FormulaExpression[] { format }));
		}

		// Token: 0x0600CCC9 RID: 52425 RVA: 0x002B9F4E File Offset: 0x002B814E
		public static FormulaExpression StringLiteral(string name)
		{
			return new PythonStringLiteral(name);
		}

		// Token: 0x0600CCCA RID: 52426 RVA: 0x002B9F56 File Offset: 0x002B8156
		public static FormulaExpression Strip(FormulaExpression subject)
		{
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<string>("strip"));
		}

		// Token: 0x0600CCCB RID: 52427 RVA: 0x002B9F68 File Offset: 0x002B8168
		public static FormulaExpression StrpTime(FormulaExpression subject, FormulaExpression format)
		{
			return PythonExpressionHelper.Dot(PythonExpressionHelper.Variable<object>("datetime"), PythonExpressionHelper.Func<DateTime>("strptime", new FormulaExpression[] { subject, format }));
		}

		// Token: 0x0600CCCC RID: 52428 RVA: 0x002B9F91 File Offset: 0x002B8191
		public static FormulaExpression Ternary(FormulaExpression condition, FormulaExpression trueBranch, FormulaExpression falseBranch)
		{
			return new PythonTernary(condition, trueBranch, falseBranch);
		}

		// Token: 0x0600CCCD RID: 52429 RVA: 0x002B9F9B File Offset: 0x002B819B
		public static FormulaExpression ThisDot(this FormulaExpression subject, FormulaExpression accessor)
		{
			return new PythonDot(subject, accessor);
		}

		// Token: 0x0600CCCE RID: 52430 RVA: 0x002B9FA4 File Offset: 0x002B81A4
		public static FormulaExpression Title(FormulaExpression subject)
		{
			IFormulaBinaryOperator formulaBinaryOperator = subject as IFormulaBinaryOperator;
			if (formulaBinaryOperator != null && formulaBinaryOperator.Precedence < 10)
			{
				subject = PythonExpressionHelper.Parenthesis(subject);
			}
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<string>("title"));
		}

		// Token: 0x0600CCCF RID: 52431 RVA: 0x002B9FDD File Offset: 0x002B81DD
		public static FormulaExpression True()
		{
			return new PythonBooleanLiteral(true);
		}

		// Token: 0x0600CCD0 RID: 52432 RVA: 0x002B9FE5 File Offset: 0x002B81E5
		public static FormulaExpression Tuple(params FormulaExpression[] items)
		{
			return new PythonTuple(items);
		}

		// Token: 0x0600CCD1 RID: 52433 RVA: 0x002B9FF0 File Offset: 0x002B81F0
		public static FormulaExpression Upper(FormulaExpression subject)
		{
			IFormulaBinaryOperator formulaBinaryOperator = subject as IFormulaBinaryOperator;
			if (formulaBinaryOperator != null && formulaBinaryOperator.Precedence < 10)
			{
				subject = PythonExpressionHelper.Parenthesis(subject);
			}
			return PythonExpressionHelper.Dot(subject, PythonExpressionHelper.Func<string>("upper"));
		}

		// Token: 0x0600CCD2 RID: 52434 RVA: 0x002BA029 File Offset: 0x002B8229
		public static FormulaExpression Variable(string name)
		{
			return PythonExpressionHelper.Variable(name, typeof(object));
		}

		// Token: 0x0600CCD3 RID: 52435 RVA: 0x002BA03B File Offset: 0x002B823B
		public static FormulaExpression Variable<T>(string name)
		{
			return PythonExpressionHelper.Variable(name, typeof(T));
		}

		// Token: 0x0600CCD4 RID: 52436 RVA: 0x002BA04D File Offset: 0x002B824D
		public static FormulaExpression Variable(string name, Type type)
		{
			return new PythonVariable(name, type);
		}

		// Token: 0x0600CCD5 RID: 52437 RVA: 0x002BA056 File Offset: 0x002B8256
		public static FormulaExpression DecimalLiteral(decimal value)
		{
			return new PythonDecimalLiteral(value);
		}

		// Token: 0x0600CCD6 RID: 52438 RVA: 0x002BA05E File Offset: 0x002B825E
		public static FormulaExpression FloatLiteral(double value)
		{
			return new PythonFloatLiteral(value);
		}

		// Token: 0x0600CCD7 RID: 52439 RVA: 0x002BA066 File Offset: 0x002B8266
		public static FormulaExpression IntLiteral(int value)
		{
			return new PythonIntLiteral(value);
		}

		// Token: 0x0600CCD8 RID: 52440 RVA: 0x002BA06E File Offset: 0x002B826E
		public static FormulaExpression NumberLiteral(int value)
		{
			return PythonExpressionHelper.IntLiteral(value);
		}

		// Token: 0x0600CCD9 RID: 52441 RVA: 0x002BA076 File Offset: 0x002B8276
		public static FormulaExpression NumberLiteral(double value)
		{
			if (value.Scale() != 0)
			{
				return PythonExpressionHelper.FloatLiteral(value);
			}
			return PythonExpressionHelper.IntLiteral(Convert.ToInt32(value));
		}

		// Token: 0x0600CCDA RID: 52442 RVA: 0x002BA092 File Offset: 0x002B8292
		public static FormulaExpression NumberLiteral(decimal value)
		{
			if (value.Scale() != 0)
			{
				return PythonExpressionHelper.FloatLiteral(Convert.ToDouble(value));
			}
			return PythonExpressionHelper.IntLiteral(Convert.ToInt32(value));
		}

		// Token: 0x0600CCDB RID: 52443 RVA: 0x002BA0B3 File Offset: 0x002B82B3
		public static FormulaExpression Avg(IEnumerable<FormulaExpression> terms)
		{
			terms = terms.ToReadOnlyList<FormulaExpression>();
			return PythonExpressionHelper.Divide(PythonExpressionHelper.Sum(terms), PythonExpressionHelper.NumberLiteral(terms.Count<FormulaExpression>()));
		}

		// Token: 0x0600CCDC RID: 52444 RVA: 0x002BA0D4 File Offset: 0x002B82D4
		public static FormulaExpression Decimal(FormulaExpression subject, bool force = false)
		{
			if (!force)
			{
				IFormulaTyped formulaTyped = subject as IFormulaTyped;
				if (formulaTyped != null && formulaTyped.Type == typeof(decimal))
				{
					return subject;
				}
			}
			return PythonExpressionHelper.Func<decimal>("Decimal", new FormulaExpression[] { PythonExpressionHelper.Str(subject) });
		}

		// Token: 0x0600CCDD RID: 52445 RVA: 0x002BA120 File Offset: 0x002B8320
		public static FormulaExpression Divide(FormulaExpression left, FormulaExpression right)
		{
			FormulaNumberLiteral formulaNumberLiteral = right as FormulaNumberLiteral;
			if (formulaNumberLiteral == null || formulaNumberLiteral.Value != 1.0)
			{
				return new PythonDivide(left, right);
			}
			return left;
		}

		// Token: 0x0600CCDE RID: 52446 RVA: 0x002BA151 File Offset: 0x002B8351
		public static FormulaExpression Divide(FormulaExpression left, double right)
		{
			return PythonExpressionHelper.Divide(left, PythonExpressionHelper.FloatLiteral(right));
		}

		// Token: 0x0600CCDF RID: 52447 RVA: 0x002BA160 File Offset: 0x002B8360
		public static FormulaExpression DivideFloor(FormulaExpression left, FormulaExpression right)
		{
			FormulaNumberLiteral formulaNumberLiteral = right as FormulaNumberLiteral;
			if (formulaNumberLiteral == null || formulaNumberLiteral.Value != 1.0)
			{
				return new PythonDivideFloor(left, right);
			}
			return left;
		}

		// Token: 0x0600CCE0 RID: 52448 RVA: 0x002BA191 File Offset: 0x002B8391
		public static FormulaExpression DivideFloor(FormulaExpression left, int right)
		{
			return PythonExpressionHelper.DivideFloor(left, PythonExpressionHelper.IntLiteral(right));
		}

		// Token: 0x0600CCE1 RID: 52449 RVA: 0x002BA19F File Offset: 0x002B839F
		public static FormulaExpression DivideFloor(FormulaExpression left, double right)
		{
			return PythonExpressionHelper.DivideFloor(left, PythonExpressionHelper.FloatLiteral(right));
		}

		// Token: 0x0600CCE2 RID: 52450 RVA: 0x002BA1B0 File Offset: 0x002B83B0
		public static FormulaExpression Exponent(FormulaExpression left, FormulaExpression right)
		{
			FormulaNumberLiteral formulaNumberLiteral = left as FormulaNumberLiteral;
			if (formulaNumberLiteral != null)
			{
				FormulaNumberLiteral formulaNumberLiteral2 = right as FormulaNumberLiteral;
				if (formulaNumberLiteral2 != null)
				{
					return PythonExpressionHelper.NumberLiteral(Math.Pow(formulaNumberLiteral.Value, formulaNumberLiteral2.Value));
				}
			}
			return new PythonExponent(left, right);
		}

		// Token: 0x0600CCE3 RID: 52451 RVA: 0x002BA1EF File Offset: 0x002B83EF
		public static FormulaExpression Float(FormulaExpression subject)
		{
			return PythonExpressionHelper.Func<double>("float", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CCE4 RID: 52452 RVA: 0x002BA205 File Offset: 0x002B8405
		public static FormulaExpression Floor(FormulaExpression subject)
		{
			return PythonExpressionHelper.Func<double>("math.floor", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CCE5 RID: 52453 RVA: 0x002BA21B File Offset: 0x002B841B
		public static FormulaExpression Int(FormulaExpression subject)
		{
			return PythonExpressionHelper.Func<int>("int", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CCE6 RID: 52454 RVA: 0x002BA231 File Offset: 0x002B8431
		public static FormulaExpression Minus(int left, FormulaExpression right)
		{
			return PythonExpressionHelper.Minus(PythonExpressionHelper.IntLiteral(left), right);
		}

		// Token: 0x0600CCE7 RID: 52455 RVA: 0x002BA23F File Offset: 0x002B843F
		public static FormulaExpression Minus(double left, FormulaExpression right)
		{
			return PythonExpressionHelper.Minus(PythonExpressionHelper.FloatLiteral(left), right);
		}

		// Token: 0x0600CCE8 RID: 52456 RVA: 0x002BA24D File Offset: 0x002B844D
		public static FormulaExpression Minus(FormulaExpression left, double right)
		{
			return PythonExpressionHelper.Minus(left, PythonExpressionHelper.FloatLiteral(right));
		}

		// Token: 0x0600CCE9 RID: 52457 RVA: 0x002BA25B File Offset: 0x002B845B
		public static FormulaExpression Minus(FormulaExpression left, int right)
		{
			return PythonExpressionHelper.Minus(left, PythonExpressionHelper.IntLiteral(right));
		}

		// Token: 0x0600CCEA RID: 52458 RVA: 0x002BA26C File Offset: 0x002B846C
		public static FormulaExpression Minus(FormulaExpression left, FormulaExpression right)
		{
			FormulaNumberLiteral formulaNumberLiteral = right as FormulaNumberLiteral;
			if (formulaNumberLiteral != null)
			{
				if (formulaNumberLiteral.Value == 0.0)
				{
					return left;
				}
				FormulaNumberLiteral formulaNumberLiteral2 = left as FormulaNumberLiteral;
				if (formulaNumberLiteral2 != null)
				{
					return PythonExpressionHelper.NumberLiteral(formulaNumberLiteral2.Value - formulaNumberLiteral.Value);
				}
				if (formulaNumberLiteral.Value < 0.0)
				{
					return PythonExpressionHelper.Plus(left, PythonExpressionHelper.NumberLiteral(-formulaNumberLiteral.Value));
				}
				FormulaPlus formulaPlus = left as FormulaPlus;
				if (formulaPlus != null)
				{
					FormulaNumberLiteral formulaNumberLiteral3 = formulaPlus.Right as FormulaNumberLiteral;
					if (formulaNumberLiteral3 != null)
					{
						return PythonExpressionHelper.Plus(formulaPlus.Left, PythonExpressionHelper.Minus(formulaNumberLiteral3, formulaNumberLiteral));
					}
				}
				FormulaMinus formulaMinus = left as FormulaMinus;
				if (formulaMinus != null)
				{
					FormulaNumberLiteral formulaNumberLiteral4 = formulaMinus.Right as FormulaNumberLiteral;
					if (formulaNumberLiteral4 != null)
					{
						return PythonExpressionHelper.Minus(formulaMinus.Left, PythonExpressionHelper.Plus(formulaNumberLiteral4, formulaNumberLiteral));
					}
				}
			}
			return new PythonMinus(left, right);
		}

		// Token: 0x0600CCEB RID: 52459 RVA: 0x002BA340 File Offset: 0x002B8540
		public static FormulaExpression Minus1(FormulaExpression val)
		{
			return PythonExpressionHelper.Minus(val, PythonExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600CCEC RID: 52460 RVA: 0x002BA34E File Offset: 0x002B854E
		public static FormulaExpression Modulo(FormulaExpression left, double right)
		{
			return PythonExpressionHelper.Modulo(left, PythonExpressionHelper.FloatLiteral(right));
		}

		// Token: 0x0600CCED RID: 52461 RVA: 0x002BA35C File Offset: 0x002B855C
		public static FormulaExpression Modulo(FormulaExpression left, int right)
		{
			return PythonExpressionHelper.Modulo(left, PythonExpressionHelper.IntLiteral(right));
		}

		// Token: 0x0600CCEE RID: 52462 RVA: 0x002BA36A File Offset: 0x002B856A
		public static FormulaExpression Modulo(FormulaExpression left, FormulaExpression right)
		{
			return new PythonModulo(left, right);
		}

		// Token: 0x0600CCEF RID: 52463 RVA: 0x002BA373 File Offset: 0x002B8573
		public static FormulaExpression Multiply(int left, FormulaExpression right)
		{
			return PythonExpressionHelper.Multiply(PythonExpressionHelper.IntLiteral(left), right);
		}

		// Token: 0x0600CCF0 RID: 52464 RVA: 0x002BA381 File Offset: 0x002B8581
		public static FormulaExpression Multiply(double left, FormulaExpression right)
		{
			return PythonExpressionHelper.Multiply(PythonExpressionHelper.FloatLiteral(left), right);
		}

		// Token: 0x0600CCF1 RID: 52465 RVA: 0x002BA38F File Offset: 0x002B858F
		public static FormulaExpression Multiply(FormulaExpression left, double right)
		{
			return PythonExpressionHelper.Multiply(left, PythonExpressionHelper.FloatLiteral(right));
		}

		// Token: 0x0600CCF2 RID: 52466 RVA: 0x002BA39D File Offset: 0x002B859D
		public static FormulaExpression Multiply(FormulaExpression left, int right)
		{
			return PythonExpressionHelper.Multiply(left, PythonExpressionHelper.IntLiteral(right));
		}

		// Token: 0x0600CCF3 RID: 52467 RVA: 0x002BA3AC File Offset: 0x002B85AC
		public static FormulaExpression Multiply(FormulaExpression left, FormulaExpression right)
		{
			FormulaNumberLiteral formulaNumberLiteral = right as FormulaNumberLiteral;
			if (formulaNumberLiteral != null && formulaNumberLiteral.Value == 1.0)
			{
				return left;
			}
			formulaNumberLiteral = left as FormulaNumberLiteral;
			if (formulaNumberLiteral == null || formulaNumberLiteral.Value != 1.0)
			{
				return new PythonMultiply(left, right);
			}
			return right;
		}

		// Token: 0x0600CCF4 RID: 52468 RVA: 0x002BA3FA File Offset: 0x002B85FA
		public static FormulaExpression Plus(FormulaExpression left, double right)
		{
			return PythonExpressionHelper.Plus(left, PythonExpressionHelper.FloatLiteral(right));
		}

		// Token: 0x0600CCF5 RID: 52469 RVA: 0x002BA408 File Offset: 0x002B8608
		public static FormulaExpression Plus(FormulaExpression left, FormulaExpression right)
		{
			FormulaNumberLiteral formulaNumberLiteral = right as FormulaNumberLiteral;
			if (formulaNumberLiteral != null)
			{
				if (formulaNumberLiteral.Value == 0.0)
				{
					return left;
				}
				FormulaNumberLiteral formulaNumberLiteral2 = left as FormulaNumberLiteral;
				if (formulaNumberLiteral2 != null)
				{
					if (formulaNumberLiteral2.Value != 0.0)
					{
						return PythonExpressionHelper.NumberLiteral(formulaNumberLiteral2.Value + formulaNumberLiteral.Value);
					}
					return right;
				}
				else
				{
					if (formulaNumberLiteral.Value < 0.0)
					{
						return PythonExpressionHelper.Minus(left, PythonExpressionHelper.NumberLiteral(-formulaNumberLiteral.Value));
					}
					FormulaPlus formulaPlus = left as FormulaPlus;
					if (formulaPlus != null)
					{
						FormulaNumberLiteral formulaNumberLiteral3 = formulaPlus.Right as FormulaNumberLiteral;
						if (formulaNumberLiteral3 != null)
						{
							return PythonExpressionHelper.Plus(formulaPlus.Left, PythonExpressionHelper.Plus(formulaNumberLiteral3, formulaNumberLiteral));
						}
					}
					FormulaMinus formulaMinus = left as FormulaMinus;
					if (formulaMinus != null)
					{
						FormulaNumberLiteral formulaNumberLiteral4 = formulaMinus.Right as FormulaNumberLiteral;
						if (formulaNumberLiteral4 != null)
						{
							return PythonExpressionHelper.Minus(formulaMinus.Left, PythonExpressionHelper.Minus(formulaNumberLiteral4, formulaNumberLiteral));
						}
					}
				}
			}
			return new PythonPlus(left, right);
		}

		// Token: 0x0600CCF6 RID: 52470 RVA: 0x002BA4EF File Offset: 0x002B86EF
		public static FormulaExpression Plus1(FormulaExpression val)
		{
			return PythonExpressionHelper.Plus(val, PythonExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600CCF7 RID: 52471 RVA: 0x002BA4FD File Offset: 0x002B86FD
		public static FormulaExpression Prod(IEnumerable<FormulaExpression> terms)
		{
			return PythonExpressionHelper.AggregateFunc("prod", terms);
		}

		// Token: 0x0600CCF8 RID: 52472 RVA: 0x002BA50A File Offset: 0x002B870A
		public static FormulaExpression Sum(IEnumerable<FormulaExpression> terms)
		{
			return PythonExpressionHelper.AggregateFunc("sum", terms);
		}

		// Token: 0x0600CCF9 RID: 52473 RVA: 0x002BA517 File Offset: 0x002B8717
		public static FormulaExpression DateTime(int year, int month, int day)
		{
			return PythonExpressionHelper.DateTime(PythonExpressionHelper.NumberLiteral(year), PythonExpressionHelper.NumberLiteral(month), PythonExpressionHelper.NumberLiteral(day));
		}

		// Token: 0x0600CCFA RID: 52474 RVA: 0x002BA530 File Offset: 0x002B8730
		public static FormulaExpression DateTime(FormulaExpression year, int month, int day)
		{
			return PythonExpressionHelper.DateTime(year, PythonExpressionHelper.NumberLiteral(month), PythonExpressionHelper.NumberLiteral(day));
		}

		// Token: 0x0600CCFB RID: 52475 RVA: 0x002BA544 File Offset: 0x002B8744
		public static FormulaExpression DateTime(int year, FormulaExpression month, int day)
		{
			return PythonExpressionHelper.DateTime(PythonExpressionHelper.NumberLiteral(year), month, PythonExpressionHelper.NumberLiteral(day));
		}

		// Token: 0x0600CCFC RID: 52476 RVA: 0x002BA558 File Offset: 0x002B8758
		public static FormulaExpression DateTime(FormulaExpression year, FormulaExpression month, FormulaExpression day)
		{
			return PythonExpressionHelper.Func<DateTime>("datetime", new FormulaExpression[] { year, month, day });
		}

		// Token: 0x0600CCFD RID: 52477 RVA: 0x002BA576 File Offset: 0x002B8776
		public static FormulaExpression Day(FormulaExpression subject)
		{
			return PythonExpressionHelper.Dot<int>(subject, "day");
		}

		// Token: 0x0600CCFE RID: 52478 RVA: 0x002BA584 File Offset: 0x002B8784
		public static FormulaExpression DayEnd(FormulaExpression dayStart)
		{
			double? num = new double?((double)1);
			return PythonExpressionHelper.Plus(dayStart, PythonExpressionHelper.RelativeDelta(null, null, num, null, null, null, null));
		}

		// Token: 0x0600CCFF RID: 52479 RVA: 0x002BA5DC File Offset: 0x002B87DC
		public static FormulaExpression DayStart(FormulaExpression subject)
		{
			return PythonExpressionHelper.Func<DateTime>("datetime", new FormulaExpression[]
			{
				PythonExpressionHelper.Year(subject),
				PythonExpressionHelper.Month(subject),
				PythonExpressionHelper.Day(subject)
			});
		}

		// Token: 0x0600CD00 RID: 52480 RVA: 0x002BA609 File Offset: 0x002B8809
		public static FormulaExpression Hour(FormulaExpression subject)
		{
			return PythonExpressionHelper.Dot<int>(subject, "hour");
		}

		// Token: 0x0600CD01 RID: 52481 RVA: 0x002BA618 File Offset: 0x002B8818
		public static FormulaExpression HourEnd(FormulaExpression hourStart)
		{
			double? num = new double?((double)1);
			return PythonExpressionHelper.Plus(hourStart, PythonExpressionHelper.RelativeDelta(null, null, null, num, null, null, null));
		}

		// Token: 0x0600CD02 RID: 52482 RVA: 0x002BA670 File Offset: 0x002B8870
		public static FormulaExpression HourStart(FormulaExpression subject)
		{
			return PythonExpressionHelper.Func<DateTime>("datetime", new FormulaExpression[]
			{
				PythonExpressionHelper.Year(subject),
				PythonExpressionHelper.Month(subject),
				PythonExpressionHelper.Day(subject),
				PythonExpressionHelper.Hour(subject)
			});
		}

		// Token: 0x0600CD03 RID: 52483 RVA: 0x002BA6A6 File Offset: 0x002B88A6
		public static FormulaExpression Minute(FormulaExpression subject)
		{
			return PythonExpressionHelper.Dot<int>(subject, "minute");
		}

		// Token: 0x0600CD04 RID: 52484 RVA: 0x002BA6B4 File Offset: 0x002B88B4
		public static FormulaExpression MinuteEnd(FormulaExpression minuteStart)
		{
			double? num = new double?((double)1);
			return PythonExpressionHelper.Plus(minuteStart, PythonExpressionHelper.RelativeDelta(null, null, null, null, num, null, null));
		}

		// Token: 0x0600CD05 RID: 52485 RVA: 0x002BA70C File Offset: 0x002B890C
		public static FormulaExpression MinuteStart(FormulaExpression subject)
		{
			return PythonExpressionHelper.Func<DateTime>("datetime", new FormulaExpression[]
			{
				PythonExpressionHelper.Year(subject),
				PythonExpressionHelper.Month(subject),
				PythonExpressionHelper.Day(subject),
				PythonExpressionHelper.Hour(subject),
				PythonExpressionHelper.Minute(subject)
			});
		}

		// Token: 0x0600CD06 RID: 52486 RVA: 0x002BA74B File Offset: 0x002B894B
		public static FormulaExpression Month(FormulaExpression subject)
		{
			return PythonExpressionHelper.Dot<int>(subject, "month");
		}

		// Token: 0x0600CD07 RID: 52487 RVA: 0x002BA758 File Offset: 0x002B8958
		public static FormulaExpression MonthDays(FormulaExpression subject)
		{
			return new PythonMonthDays(subject);
		}

		// Token: 0x0600CD08 RID: 52488 RVA: 0x002BA760 File Offset: 0x002B8960
		public static FormulaExpression MonthEnd(FormulaExpression monthStart)
		{
			double? num = new double?((double)1);
			return PythonExpressionHelper.Plus(monthStart, PythonExpressionHelper.RelativeDelta(null, num, null, null, null, null, null));
		}

		// Token: 0x0600CD09 RID: 52489 RVA: 0x002BA7B8 File Offset: 0x002B89B8
		public static FormulaExpression MonthStart(FormulaExpression subject)
		{
			return PythonExpressionHelper.Func<DateTime>("datetime", new FormulaExpression[]
			{
				PythonExpressionHelper.Year(subject),
				PythonExpressionHelper.Month(subject),
				PythonExpressionHelper.NumberLiteral(1)
			});
		}

		// Token: 0x0600CD0A RID: 52490 RVA: 0x002BA7E8 File Offset: 0x002B89E8
		public static FormulaExpression RelativeDelta(double? years = null, double? months = null, double? days = null, double? hours = null, double? minutes = null, double? seconds = null, double? milliseconds = null)
		{
			List<FormulaExpression> list = new List<FormulaExpression>();
			if (years != null)
			{
				list.Add(PythonExpressionHelper.AssignArg<int>("years", PythonExpressionHelper.NumberLiteral(years.Value)));
			}
			if (months != null)
			{
				list.Add(PythonExpressionHelper.AssignArg<int>("months", PythonExpressionHelper.NumberLiteral(months.Value)));
			}
			if (days != null)
			{
				list.Add(PythonExpressionHelper.AssignArg<int>("days", PythonExpressionHelper.NumberLiteral(days.Value)));
			}
			if (hours != null)
			{
				list.Add(PythonExpressionHelper.AssignArg<int>("hours", PythonExpressionHelper.NumberLiteral(hours.Value)));
			}
			if (minutes != null)
			{
				list.Add(PythonExpressionHelper.AssignArg<int>("minutes", PythonExpressionHelper.NumberLiteral(minutes.Value)));
			}
			if (seconds != null)
			{
				list.Add(PythonExpressionHelper.AssignArg<int>("seconds", PythonExpressionHelper.NumberLiteral(seconds.Value)));
			}
			if (milliseconds != null)
			{
				list.Add(PythonExpressionHelper.AssignArg<int>("milliseconds", PythonExpressionHelper.NumberLiteral(milliseconds.Value)));
			}
			return PythonExpressionHelper.Func<DateTime>("relativedelta", list.ToArray());
		}

		// Token: 0x0600CD0B RID: 52491 RVA: 0x002BA910 File Offset: 0x002B8B10
		public static FormulaExpression RelativeDelta(FormulaExpression years = null, FormulaExpression months = null, FormulaExpression days = null, FormulaExpression hours = null, FormulaExpression minutes = null, FormulaExpression seconds = null, FormulaExpression milliseconds = null)
		{
			List<FormulaExpression> list = new List<FormulaExpression>();
			if (years != null)
			{
				list.Add(PythonExpressionHelper.AssignArg<int>("years", years));
			}
			if (months != null)
			{
				list.Add(PythonExpressionHelper.AssignArg<int>("months", months));
			}
			if (days != null)
			{
				list.Add(PythonExpressionHelper.AssignArg<int>("days", days));
			}
			if (hours != null)
			{
				list.Add(PythonExpressionHelper.AssignArg<int>("hours", hours));
			}
			if (minutes != null)
			{
				list.Add(PythonExpressionHelper.AssignArg<int>("minutes", minutes));
			}
			if (seconds != null)
			{
				list.Add(PythonExpressionHelper.AssignArg<int>("seconds", seconds));
			}
			if (milliseconds != null)
			{
				list.Add(PythonExpressionHelper.AssignArg<int>("milliseconds", milliseconds));
			}
			return PythonExpressionHelper.Func<DateTime>("relativedelta", list.ToArray());
		}

		// Token: 0x0600CD0C RID: 52492 RVA: 0x002BA9EF File Offset: 0x002B8BEF
		public static FormulaExpression Second(FormulaExpression subject)
		{
			return PythonExpressionHelper.Dot<int>(subject, "second");
		}

		// Token: 0x0600CD0D RID: 52493 RVA: 0x002BA9FC File Offset: 0x002B8BFC
		public static FormulaExpression SecondEnd(FormulaExpression secondStart)
		{
			double? num = new double?((double)1);
			return PythonExpressionHelper.Plus(secondStart, PythonExpressionHelper.RelativeDelta(null, null, null, null, null, num, null));
		}

		// Token: 0x0600CD0E RID: 52494 RVA: 0x002BAA54 File Offset: 0x002B8C54
		public static FormulaExpression SecondStart(FormulaExpression subject)
		{
			return PythonExpressionHelper.Func<DateTime>("datetime", new FormulaExpression[]
			{
				PythonExpressionHelper.Year(subject),
				PythonExpressionHelper.Month(subject),
				PythonExpressionHelper.Day(subject),
				PythonExpressionHelper.Hour(subject),
				PythonExpressionHelper.Minute(subject),
				PythonExpressionHelper.Second(subject)
			});
		}

		// Token: 0x0600CD0F RID: 52495 RVA: 0x002BAAA7 File Offset: 0x002B8CA7
		public static FormulaExpression WeekDay(FormulaExpression subject)
		{
			return new PythonWeekDay(subject);
		}

		// Token: 0x0600CD10 RID: 52496 RVA: 0x002BAAB0 File Offset: 0x002B8CB0
		public static FormulaExpression WeekEnd(FormulaExpression weekStart)
		{
			double? num = new double?((double)7);
			return PythonExpressionHelper.Plus(weekStart, PythonExpressionHelper.RelativeDelta(null, null, num, null, null, null, null));
		}

		// Token: 0x0600CD11 RID: 52497 RVA: 0x002BAB08 File Offset: 0x002B8D08
		public static FormulaExpression WeekStart(FormulaExpression subject)
		{
			return PythonExpressionHelper.Minus(PythonExpressionHelper.DayStart(subject), PythonExpressionHelper.RelativeDelta(null, null, PythonExpressionHelper.Modulo(PythonExpressionHelper.DotFunc<int>(subject, "isoweekday"), 7), null, null, null, null));
		}

		// Token: 0x0600CD12 RID: 52498 RVA: 0x002BAB31 File Offset: 0x002B8D31
		public static FormulaExpression Year(FormulaExpression subject)
		{
			return PythonExpressionHelper.Dot<int>(subject, "year");
		}

		// Token: 0x0600CD13 RID: 52499 RVA: 0x002BAB3E File Offset: 0x002B8D3E
		public static FormulaExpression YearDay(FormulaExpression subject)
		{
			return new PythonYearDay(subject);
		}

		// Token: 0x0600CD14 RID: 52500 RVA: 0x002BAB46 File Offset: 0x002B8D46
		public static FormulaExpression YearDays(FormulaExpression subject)
		{
			return PythonExpressionHelper.Ternary(PythonExpressionHelper.Func<bool>("calendar.isleap", new FormulaExpression[] { PythonExpressionHelper.Year(subject) }), PythonExpressionHelper.NumberLiteral(366), PythonExpressionHelper.NumberLiteral(365));
		}

		// Token: 0x0600CD15 RID: 52501 RVA: 0x002BAB7C File Offset: 0x002B8D7C
		public static FormulaExpression YearEnd(FormulaExpression yearStart)
		{
			return PythonExpressionHelper.Plus(yearStart, PythonExpressionHelper.RelativeDelta(new double?((double)1), null, null, null, null, null, null));
		}

		// Token: 0x0600CD16 RID: 52502 RVA: 0x002BABD1 File Offset: 0x002B8DD1
		public static FormulaExpression YearStart(FormulaExpression subject)
		{
			return PythonExpressionHelper.Func<DateTime>("datetime", new FormulaExpression[]
			{
				PythonExpressionHelper.Year(subject),
				PythonExpressionHelper.NumberLiteral(1),
				PythonExpressionHelper.NumberLiteral(1)
			});
		}

		// Token: 0x0600CD17 RID: 52503 RVA: 0x002BABFE File Offset: 0x002B8DFE
		public static FormulaExpression YearWeek(FormulaExpression subject)
		{
			return PythonExpressionHelper.Plus1(PythonExpressionHelper.Int(PythonExpressionHelper.StrfTime(subject, "%U")));
		}

		// Token: 0x0600CD18 RID: 52504 RVA: 0x002BAC15 File Offset: 0x002B8E15
		public static FormulaExpression Is(FormulaExpression left, FormulaExpression right)
		{
			return new PythonIs(left, right);
		}

		// Token: 0x0600CD19 RID: 52505 RVA: 0x002BAC1E File Offset: 0x002B8E1E
		public static FormulaExpression Equal(FormulaExpression left, FormulaExpression right)
		{
			return new PythonEqual(left, right);
		}

		// Token: 0x0600CD1A RID: 52506 RVA: 0x002BAC27 File Offset: 0x002B8E27
		public static FormulaExpression GreaterThan(FormulaExpression left, double right)
		{
			return PythonExpressionHelper.GreaterThan(left, PythonExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600CD1B RID: 52507 RVA: 0x002BAC35 File Offset: 0x002B8E35
		public static FormulaExpression GreaterThan(FormulaExpression left, FormulaExpression right)
		{
			return new PythonGreaterThan(left, right);
		}

		// Token: 0x0600CD1C RID: 52508 RVA: 0x002BAC3E File Offset: 0x002B8E3E
		public static FormulaExpression GreaterThanEqual(FormulaExpression left, double right)
		{
			return PythonExpressionHelper.GreaterThanEqual(left, PythonExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600CD1D RID: 52509 RVA: 0x002BAC4C File Offset: 0x002B8E4C
		public static FormulaExpression GreaterThanEqual(FormulaExpression left, FormulaExpression right)
		{
			return new PythonGreaterThanEqual(left, right);
		}

		// Token: 0x0600CD1E RID: 52510 RVA: 0x002BAC55 File Offset: 0x002B8E55
		public static FormulaExpression LessThan(FormulaExpression left, double right)
		{
			return PythonExpressionHelper.LessThan(left, PythonExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600CD1F RID: 52511 RVA: 0x002BAC63 File Offset: 0x002B8E63
		public static FormulaExpression LessThan(FormulaExpression left, FormulaExpression right)
		{
			return new PythonLessThan(left, right);
		}

		// Token: 0x0600CD20 RID: 52512 RVA: 0x002BAC6C File Offset: 0x002B8E6C
		public static FormulaExpression LessThanEqual(FormulaExpression left, double right)
		{
			return PythonExpressionHelper.LessThanEqual(left, PythonExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600CD21 RID: 52513 RVA: 0x002BAC7A File Offset: 0x002B8E7A
		public static FormulaExpression LessThanEqual(FormulaExpression left, FormulaExpression right)
		{
			return new PythonLessThanEqual(left, right);
		}

		// Token: 0x0600CD22 RID: 52514 RVA: 0x002BAC83 File Offset: 0x002B8E83
		public static FormulaExpression NotEqual(FormulaExpression left, FormulaExpression right)
		{
			return new PythonNotEqual(left, right);
		}

		// Token: 0x0600CD23 RID: 52515 RVA: 0x002BAC8C File Offset: 0x002B8E8C
		public static PythonDefinition Definition(string name, IEnumerable<PythonVariable> parameters, PythonBlock block, PythonComment comment = null)
		{
			return new PythonDefinition(name, parameters, block, comment);
		}

		// Token: 0x0600CD24 RID: 52516 RVA: 0x002BAC97 File Offset: 0x002B8E97
		public static PythonDefinition Definition(string name, IEnumerable<PythonVariable> parameters, IEnumerable<FormulaExpression> statements, PythonComment comment = null)
		{
			return new PythonDefinition(name, parameters, statements, comment);
		}

		// Token: 0x0600CD25 RID: 52517 RVA: 0x002BACA2 File Offset: 0x002B8EA2
		public static PythonProgram Program(IEnumerable<PythonImport> importStatements, PythonDefinition definition, IEnumerable<FormulaExpression> statements = null, PythonComment comment = null)
		{
			return PythonExpressionHelper.Program(importStatements, definition.Yield<PythonDefinition>(), statements, comment);
		}

		// Token: 0x0600CD26 RID: 52518 RVA: 0x002BACB2 File Offset: 0x002B8EB2
		public static PythonProgram Program(IEnumerable<PythonImport> importStatements, IEnumerable<PythonDefinition> definitions, IEnumerable<FormulaExpression> statements = null, PythonComment comment = null)
		{
			return new PythonProgram(importStatements, definitions, statements ?? Enumerable.Empty<FormulaExpression>(), comment);
		}

		// Token: 0x02001884 RID: 6276
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005029 RID: 20521
			public static Func<string, FormulaExpression> <0>__StringLiteral;
		}
	}
}
