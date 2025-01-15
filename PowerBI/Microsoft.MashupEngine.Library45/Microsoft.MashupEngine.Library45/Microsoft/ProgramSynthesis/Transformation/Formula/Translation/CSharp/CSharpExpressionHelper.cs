using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001974 RID: 6516
	internal static class CSharpExpressionHelper
	{
		// Token: 0x0600D439 RID: 54329 RVA: 0x002D2924 File Offset: 0x002D0B24
		public static FormulaExpression AllIndexesOf(FormulaExpression subject, FormulaExpression findText)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<int[]>("AllIndexesOf", new FormulaExpression[] { findText }));
		}

		// Token: 0x0600D43A RID: 54330 RVA: 0x002D2940 File Offset: 0x002D0B40
		public static FormulaExpression AllIndexesOf(FormulaExpression subject, FormulaExpression findText, bool conditionalAccess)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<int[]>("AllIndexesOf", new FormulaExpression[] { findText }), conditionalAccess);
		}

		// Token: 0x0600D43B RID: 54331 RVA: 0x002D295D File Offset: 0x002D0B5D
		public static FormulaExpression And(FormulaExpression left, FormulaExpression right)
		{
			return new CSharpAnd(left, right);
		}

		// Token: 0x0600D43C RID: 54332 RVA: 0x002D2966 File Offset: 0x002D0B66
		public static FormulaExpression Assign(FormulaExpression left, FormulaExpression right)
		{
			return new CSharpAssign(left, right, false);
		}

		// Token: 0x0600D43D RID: 54333 RVA: 0x002D2970 File Offset: 0x002D0B70
		public static CSharpBlock Block(IEnumerable<FormulaExpression> statements)
		{
			return new CSharpBlock(statements);
		}

		// Token: 0x0600D43E RID: 54334 RVA: 0x002D2978 File Offset: 0x002D0B78
		public static FormulaExpression Cast<T>(FormulaExpression subject)
		{
			return CSharpExpressionHelper.Cast(subject, typeof(T));
		}

		// Token: 0x0600D43F RID: 54335 RVA: 0x002D298A File Offset: 0x002D0B8A
		public static FormulaExpression Cast(FormulaExpression subject, Type type)
		{
			if (subject is IFormulaBinaryOperator)
			{
				subject = CSharpExpressionHelper.Parenthesis(subject);
			}
			return new CSharpCast(subject, type);
		}

		// Token: 0x0600D440 RID: 54336 RVA: 0x002D29A3 File Offset: 0x002D0BA3
		public static FormulaExpression Ceiling(FormulaExpression subject)
		{
			return CSharpExpressionHelper.Func<decimal>("Math.Ceiling", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D441 RID: 54337 RVA: 0x002D29B9 File Offset: 0x002D0BB9
		public static CSharpCharLiteral CharLiteral(char value)
		{
			return new CSharpCharLiteral(value);
		}

		// Token: 0x0600D442 RID: 54338 RVA: 0x002D29C4 File Offset: 0x002D0BC4
		public static FormulaExpression Concat(FormulaExpression left, FormulaExpression right)
		{
			CSharpStringLiteral csharpStringLiteral = left as CSharpStringLiteral;
			if (csharpStringLiteral != null)
			{
				CSharpStringLiteral csharpStringLiteral2 = right as CSharpStringLiteral;
				if (csharpStringLiteral2 != null)
				{
					return CSharpExpressionHelper.StringLiteral(csharpStringLiteral.Value + csharpStringLiteral2.Value);
				}
			}
			return new CSharpConcat(left, right);
		}

		// Token: 0x0600D443 RID: 54339 RVA: 0x002D2A03 File Offset: 0x002D0C03
		public static FormulaExpression ConditionalDot<TAccessor>(FormulaExpression subject, string accessor)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Variable(accessor, typeof(TAccessor)), true);
		}

		// Token: 0x0600D444 RID: 54340 RVA: 0x002D2A1C File Offset: 0x002D0C1C
		public static FormulaExpression Count(FormulaExpression subject)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<int>("Count"));
		}

		// Token: 0x0600D445 RID: 54341 RVA: 0x002D2A2E File Offset: 0x002D0C2E
		public static FormulaExpression CultureInfo(CultureInfo culture)
		{
			return CSharpExpressionHelper.CultureInfo(culture.Name);
		}

		// Token: 0x0600D446 RID: 54342 RVA: 0x002D2A3B File Offset: 0x002D0C3B
		public static FormulaExpression CultureInfo(string locale)
		{
			return CSharpExpressionHelper.CultureInfo(CSharpExpressionHelper.StringLiteral(locale));
		}

		// Token: 0x0600D447 RID: 54343 RVA: 0x002D2A48 File Offset: 0x002D0C48
		public static FormulaExpression CultureInfo(FormulaExpression locale)
		{
			return new CSharpCultureInfo(locale);
		}

		// Token: 0x0600D448 RID: 54344 RVA: 0x002D2A50 File Offset: 0x002D0C50
		public static FormulaExpression DateTimeLiteral(DateTime value)
		{
			return new CSharpDateTimeLiteral(value);
		}

		// Token: 0x0600D449 RID: 54345 RVA: 0x002D2A58 File Offset: 0x002D0C58
		public static FormulaExpression Dot<TAccessor>(string subject, string accessor)
		{
			return CSharpExpressionHelper.Dot(CSharpExpressionHelper.Variable(subject), CSharpExpressionHelper.Variable(accessor, typeof(TAccessor)));
		}

		// Token: 0x0600D44A RID: 54346 RVA: 0x002D2A75 File Offset: 0x002D0C75
		public static FormulaExpression Dot<TAccessor>(string subject, string accessor, bool conditionalAccess)
		{
			return CSharpExpressionHelper.Dot(CSharpExpressionHelper.Variable(subject), CSharpExpressionHelper.Variable(accessor, typeof(TAccessor)), conditionalAccess);
		}

		// Token: 0x0600D44B RID: 54347 RVA: 0x002D2A93 File Offset: 0x002D0C93
		public static FormulaExpression Dot<TAccessor>(FormulaExpression subject, string accessor)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Variable(accessor, typeof(TAccessor)));
		}

		// Token: 0x0600D44C RID: 54348 RVA: 0x002D2AAB File Offset: 0x002D0CAB
		public static FormulaExpression Dot(string subject, FormulaExpression accessor)
		{
			return CSharpExpressionHelper.Dot(CSharpExpressionHelper.Variable(subject), accessor);
		}

		// Token: 0x0600D44D RID: 54349 RVA: 0x002D2AB9 File Offset: 0x002D0CB9
		public static FormulaExpression Dot(string subject, FormulaExpression accessor, bool conditionalAccess)
		{
			return CSharpExpressionHelper.Dot(CSharpExpressionHelper.Variable(subject), accessor, conditionalAccess);
		}

		// Token: 0x0600D44E RID: 54350 RVA: 0x002D2AC8 File Offset: 0x002D0CC8
		public static FormulaExpression Dot(string subject, string accessor, Type accessorType)
		{
			return CSharpExpressionHelper.Dot(CSharpExpressionHelper.Variable(subject), CSharpExpressionHelper.Variable(accessor, accessorType));
		}

		// Token: 0x0600D44F RID: 54351 RVA: 0x002D2ADC File Offset: 0x002D0CDC
		public static FormulaExpression Dot(string subject, string accessor, Type accessorType, bool conditionalAccess)
		{
			return CSharpExpressionHelper.Dot(CSharpExpressionHelper.Variable(subject), CSharpExpressionHelper.Variable(accessor, accessorType), conditionalAccess);
		}

		// Token: 0x0600D450 RID: 54352 RVA: 0x002D2AF4 File Offset: 0x002D0CF4
		public static FormulaExpression Dot(FormulaExpression subject, FormulaExpression accessor)
		{
			IFormulaTyped formulaTyped = subject as IFormulaTyped;
			bool flag;
			if (formulaTyped != null)
			{
				Type type = formulaTyped.Type;
				flag = type != null && type.IsNullable();
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return CSharpExpressionHelper.Dot(subject, accessor, flag2);
		}

		// Token: 0x0600D451 RID: 54353 RVA: 0x002D2B29 File Offset: 0x002D0D29
		public static FormulaExpression Dot(FormulaExpression subject, FormulaExpression accessor, bool conditionalAccess)
		{
			if (subject is IFormulaBinaryOperator)
			{
				subject = CSharpExpressionHelper.Parenthesis(subject);
			}
			return new CSharpDot(subject, accessor, conditionalAccess);
		}

		// Token: 0x0600D452 RID: 54354 RVA: 0x002D2B43 File Offset: 0x002D0D43
		public static FormulaExpression DotFunc<T>(string subject, string funcName, bool conditionalAccess)
		{
			return CSharpExpressionHelper.DotFunc<T>(CSharpExpressionHelper.Variable(subject), funcName, conditionalAccess);
		}

		// Token: 0x0600D453 RID: 54355 RVA: 0x002D2B52 File Offset: 0x002D0D52
		public static FormulaExpression DotFunc<T>(FormulaExpression subject, string funcName, bool conditionalAccess)
		{
			return new CSharpDot(subject, CSharpExpressionHelper.Func<T>(funcName), conditionalAccess);
		}

		// Token: 0x0600D454 RID: 54356 RVA: 0x002D2B61 File Offset: 0x002D0D61
		public static FormulaExpression DotFunc<T>(string subject, string funcName, params FormulaExpression[] arguments)
		{
			return CSharpExpressionHelper.DotFunc<T>(CSharpExpressionHelper.Variable(subject), funcName, arguments);
		}

		// Token: 0x0600D455 RID: 54357 RVA: 0x002D2B70 File Offset: 0x002D0D70
		public static FormulaExpression DotFunc<T>(FormulaExpression subject, string funcName, params FormulaExpression[] arguments)
		{
			if (subject is IFormulaBinaryOperator)
			{
				subject = CSharpExpressionHelper.Parenthesis(subject);
			}
			return new CSharpDot(subject, CSharpExpressionHelper.Func<T>(funcName, arguments), false);
		}

		// Token: 0x0600D456 RID: 54358 RVA: 0x002D2B90 File Offset: 0x002D0D90
		public static FormulaExpression Equal(FormulaExpression left, FormulaExpression right)
		{
			return new CSharpEqual(left, right);
		}

		// Token: 0x0600D457 RID: 54359 RVA: 0x002D2B9C File Offset: 0x002D0D9C
		public static FormulaExpression Exponent(FormulaExpression left, FormulaExpression right)
		{
			CSharpIntLiteral csharpIntLiteral = left as CSharpIntLiteral;
			if (csharpIntLiteral != null)
			{
				CSharpIntLiteral csharpIntLiteral2 = right as CSharpIntLiteral;
				if (csharpIntLiteral2 != null)
				{
					return CSharpExpressionHelper.NumberLiteral(Math.Pow(csharpIntLiteral.Value, csharpIntLiteral2.Value));
				}
			}
			return new CSharpExponent(left, right);
		}

		// Token: 0x0600D458 RID: 54360 RVA: 0x002D2BDB File Offset: 0x002D0DDB
		public static FormulaExpression Floor(FormulaExpression subject)
		{
			return CSharpExpressionHelper.Func<decimal>("Math.Floor", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D459 RID: 54361 RVA: 0x002D2BF1 File Offset: 0x002D0DF1
		public static CSharpFunc Func<T>(string name)
		{
			return new CSharpFunc(name, typeof(T), Enumerable.Empty<FormulaExpression>());
		}

		// Token: 0x0600D45A RID: 54362 RVA: 0x002D2C08 File Offset: 0x002D0E08
		public static CSharpFunc Func(string name, Type returnType)
		{
			return new CSharpFunc(name, returnType, Enumerable.Empty<FormulaExpression>());
		}

		// Token: 0x0600D45B RID: 54363 RVA: 0x002D2C16 File Offset: 0x002D0E16
		public static CSharpFunc Func<T>(string name, params FormulaExpression[] arguments)
		{
			return new CSharpFunc(name, typeof(T), arguments);
		}

		// Token: 0x0600D45C RID: 54364 RVA: 0x002D2C29 File Offset: 0x002D0E29
		public static CSharpFunc Func(string name, Type returnType, params FormulaExpression[] arguments)
		{
			return new CSharpFunc(name, returnType, arguments);
		}

		// Token: 0x0600D45D RID: 54365 RVA: 0x002D2C33 File Offset: 0x002D0E33
		public static CSharpIf If(FormulaExpression condition, CSharpBlock trueBlock)
		{
			return new CSharpIf(condition, trueBlock, null);
		}

		// Token: 0x0600D45E RID: 54366 RVA: 0x002D2C3D File Offset: 0x002D0E3D
		public static CSharpIf If(FormulaExpression condition, CSharpBlock trueBlock, CSharpBlock falseBlock)
		{
			return new CSharpIf(condition, trueBlock, falseBlock);
		}

		// Token: 0x0600D45F RID: 54367 RVA: 0x002D2C47 File Offset: 0x002D0E47
		public static FormulaExpression IfNullReturnNull(FormulaExpression subject)
		{
			return CSharpExpressionHelper.If(CSharpExpressionHelper.Equal(subject, CSharpExpressionHelper.Null()), CSharpExpressionHelper.Block(CSharpExpressionHelper.Return(CSharpExpressionHelper.Null()).Yield<FormulaExpression>()));
		}

		// Token: 0x0600D460 RID: 54368 RVA: 0x002D2C6D File Offset: 0x002D0E6D
		public static CSharpIndex Index<T>(FormulaExpression subject, int index)
		{
			return CSharpExpressionHelper.Index(subject, CSharpExpressionHelper.NumberLiteral(index), typeof(T));
		}

		// Token: 0x0600D461 RID: 54369 RVA: 0x002D2C85 File Offset: 0x002D0E85
		public static CSharpIndex Index<T>(FormulaExpression subject, FormulaExpression index)
		{
			return CSharpExpressionHelper.Index(subject, index, typeof(T));
		}

		// Token: 0x0600D462 RID: 54370 RVA: 0x002D2C98 File Offset: 0x002D0E98
		public static CSharpIndex Index(FormulaExpression subject, int index)
		{
			return CSharpExpressionHelper.Index(subject, CSharpExpressionHelper.NumberLiteral(index), typeof(object));
		}

		// Token: 0x0600D463 RID: 54371 RVA: 0x002D2CB0 File Offset: 0x002D0EB0
		public static CSharpIndex Index(FormulaExpression subject, FormulaExpression index)
		{
			return CSharpExpressionHelper.Index(subject, index, typeof(object));
		}

		// Token: 0x0600D464 RID: 54372 RVA: 0x002D2CC3 File Offset: 0x002D0EC3
		public static CSharpIndex Index(FormulaExpression subject, FormulaExpression index, Type type)
		{
			return new CSharpIndex(subject, index, type);
		}

		// Token: 0x0600D465 RID: 54373 RVA: 0x002D2CCD File Offset: 0x002D0ECD
		public static FormulaExpression IndexOf(FormulaExpression subject, FormulaExpression find)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<int>("IndexOf", new FormulaExpression[] { find }), false);
		}

		// Token: 0x0600D466 RID: 54374 RVA: 0x002D2CEA File Offset: 0x002D0EEA
		public static FormulaExpression Is(FormulaExpression left, Type type, string variableName, out FormulaExpression variable)
		{
			variable = CSharpExpressionHelper.Variable(variableName);
			return new CSharpIs(left, CSharpExpressionHelper.Variable(type.CsName(true)), variable);
		}

		// Token: 0x0600D467 RID: 54375 RVA: 0x002D2D08 File Offset: 0x002D0F08
		public static FormulaExpression Is(FormulaExpression left, Type type)
		{
			return new CSharpIs(left, CSharpExpressionHelper.Variable(type.CsName(true)), null);
		}

		// Token: 0x0600D468 RID: 54376 RVA: 0x002D2D20 File Offset: 0x002D0F20
		public static FormulaExpression Is(FormulaExpression left, IEnumerable<Type> types)
		{
			types = types.ToReadOnlyList<Type>();
			if (types.None<Type>())
			{
				return null;
			}
			FormulaExpression formulaExpression = types.Skip(1).Aggregate(CSharpExpressionHelper.Variable(types.First<Type>().CsName(true)), (FormulaExpression current, Type next) => CSharpExpressionHelper.OrPattern(current, CSharpExpressionHelper.Variable(next.CsName(true))));
			return new CSharpIs(left, formulaExpression, null);
		}

		// Token: 0x0600D469 RID: 54377 RVA: 0x002D2D84 File Offset: 0x002D0F84
		public static FormulaExpression Is(FormulaExpression left, FormulaExpression right)
		{
			return new CSharpOr(left, right);
		}

		// Token: 0x0600D46A RID: 54378 RVA: 0x002D2D8D File Offset: 0x002D0F8D
		public static FormulaExpression IsDigit(FormulaExpression subject)
		{
			return CSharpExpressionHelper.Dot("Char", CSharpExpressionHelper.Func<bool>("IsDigit", new FormulaExpression[] { subject }));
		}

		// Token: 0x0600D46B RID: 54379 RVA: 0x002D2DAD File Offset: 0x002D0FAD
		public static FormulaExpression IsMatch(FormulaExpression subject, FormulaExpression pattern)
		{
			return CSharpExpressionHelper.Func<bool>("Regex.IsMatch", new FormulaExpression[] { subject, pattern });
		}

		// Token: 0x0600D46C RID: 54380 RVA: 0x002D2DC7 File Offset: 0x002D0FC7
		public static FormulaExpression IsNullOrEmpty(FormulaExpression subject)
		{
			return CSharpExpressionHelper.Dot(CSharpExpressionHelper.Variable("string"), CSharpExpressionHelper.Func<bool>("IsNullOrEmpty", new FormulaExpression[] { subject }));
		}

		// Token: 0x0600D46D RID: 54381 RVA: 0x002D2DEC File Offset: 0x002D0FEC
		public static FormulaExpression IsNullOrWhiteSpace(FormulaExpression subject)
		{
			return CSharpExpressionHelper.Dot(CSharpExpressionHelper.Variable("string"), CSharpExpressionHelper.Func<bool>("IsNullOrWhiteSpace", new FormulaExpression[] { subject }));
		}

		// Token: 0x0600D46E RID: 54382 RVA: 0x002D2E11 File Offset: 0x002D1011
		public static FormulaExpression Lambda(FormulaExpression argument, FormulaExpression body)
		{
			return new CSharpLambda(argument.Yield<FormulaExpression>(), body);
		}

		// Token: 0x0600D46F RID: 54383 RVA: 0x002D2E1F File Offset: 0x002D101F
		public static FormulaExpression Lambda(IEnumerable<FormulaExpression> arguments, FormulaExpression body)
		{
			return new CSharpLambda(arguments, body);
		}

		// Token: 0x0600D470 RID: 54384 RVA: 0x002D2E28 File Offset: 0x002D1028
		public static FormulaExpression LastIndexOf(FormulaExpression subject, FormulaExpression find)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<int>("LastIndexOf", new FormulaExpression[] { find }), false);
		}

		// Token: 0x0600D471 RID: 54385 RVA: 0x002D2E48 File Offset: 0x002D1048
		public static FormulaExpression Length(FormulaExpression subject)
		{
			CSharpStringLiteral csharpStringLiteral = subject as CSharpStringLiteral;
			if (csharpStringLiteral == null)
			{
				return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Variable<int>("Length"), false);
			}
			return CSharpExpressionHelper.NumberLiteral(csharpStringLiteral.Value.Length);
		}

		// Token: 0x0600D472 RID: 54386 RVA: 0x002D2E84 File Offset: 0x002D1084
		public static FormulaExpression Lower(FormulaExpression subject)
		{
			IFormulaBinaryOperator formulaBinaryOperator = subject as IFormulaBinaryOperator;
			if (formulaBinaryOperator != null && formulaBinaryOperator.Precedence < 10)
			{
				subject = CSharpExpressionHelper.Parenthesis(subject);
			}
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<string>("ToLower"));
		}

		// Token: 0x0600D473 RID: 54387 RVA: 0x002D2EBD File Offset: 0x002D10BD
		public static FormulaExpression Match(FormulaExpression subject, FormulaExpression pattern)
		{
			return CSharpExpressionHelper.Func<bool>("Regex.Match", new FormulaExpression[] { subject, pattern });
		}

		// Token: 0x0600D474 RID: 54388 RVA: 0x002D2ED7 File Offset: 0x002D10D7
		public static FormulaExpression Matches(FormulaExpression subject, FormulaExpression pattern)
		{
			return CSharpExpressionHelper.Func<object[]>("Regex.Matches", new FormulaExpression[] { subject, pattern });
		}

		// Token: 0x0600D475 RID: 54389 RVA: 0x002D2EF1 File Offset: 0x002D10F1
		public static FormulaExpression MatchFull(FormulaExpression subject, FormulaExpression pattern)
		{
			return CSharpExpressionHelper.ConditionalDot<Match>(CSharpExpressionHelper.Func<bool>("Regex.Match", new FormulaExpression[] { subject, pattern }), "Value");
		}

		// Token: 0x0600D476 RID: 54390 RVA: 0x002D2F15 File Offset: 0x002D1115
		public static CSharpNamespace Namespace(string name, IEnumerable<CSharpClass> classes)
		{
			return new CSharpNamespace(name, classes);
		}

		// Token: 0x0600D477 RID: 54391 RVA: 0x002D2F1E File Offset: 0x002D111E
		public static FormulaExpression Not(FormulaExpression subject)
		{
			return new CSharpNot((subject is IFormulaBinaryOperator) ? CSharpExpressionHelper.Parenthesis(subject) : subject);
		}

		// Token: 0x0600D478 RID: 54392 RVA: 0x002D2F36 File Offset: 0x002D1136
		public static CSharpNull Null()
		{
			return new CSharpNull();
		}

		// Token: 0x0600D479 RID: 54393 RVA: 0x002D2D84 File Offset: 0x002D0F84
		public static FormulaExpression Or(FormulaExpression left, FormulaExpression right)
		{
			return new CSharpOr(left, right);
		}

		// Token: 0x0600D47A RID: 54394 RVA: 0x002D2F3D File Offset: 0x002D113D
		public static FormulaExpression OrPattern(FormulaExpression left, FormulaExpression right)
		{
			return new CSharpOrPattern(left, right);
		}

		// Token: 0x0600D47B RID: 54395 RVA: 0x002D2F46 File Offset: 0x002D1146
		public static FormulaExpression Parenthesis(FormulaExpression body)
		{
			return new CSharpParenthesis(body);
		}

		// Token: 0x0600D47C RID: 54396 RVA: 0x002D2F4E File Offset: 0x002D114E
		public static FormulaExpression ParseDateTime(FormulaExpression subject, FormulaExpression format, FormulaExpression culture)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<DateTime?>("ParseDateTime", new FormulaExpression[] { format, culture }));
		}

		// Token: 0x0600D47D RID: 54397 RVA: 0x002D2F6E File Offset: 0x002D116E
		public static FormulaExpression ParseNumber(FormulaExpression subject, FormulaExpression culture)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<decimal?>("ParseNumber", new FormulaExpression[] { culture }));
		}

		// Token: 0x0600D47E RID: 54398 RVA: 0x002D2F8A File Offset: 0x002D118A
		public static CSharpRawSegment Raw(string code)
		{
			return new CSharpRawSegment(code);
		}

		// Token: 0x0600D47F RID: 54399 RVA: 0x002D2F92 File Offset: 0x002D1192
		public static CSharpRawLine RawLine(string code = "")
		{
			return new CSharpRawLine(code);
		}

		// Token: 0x0600D480 RID: 54400 RVA: 0x002D2F9A File Offset: 0x002D119A
		public static FormulaExpression RegexLiteral(string value)
		{
			return new CSharpRegexLiteral(value);
		}

		// Token: 0x0600D481 RID: 54401 RVA: 0x002D2FA2 File Offset: 0x002D11A2
		public static FormulaExpression Replace(FormulaExpression subject, FormulaExpression find, FormulaExpression replace)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<string>("Replace", new FormulaExpression[] { find, replace }));
		}

		// Token: 0x0600D482 RID: 54402 RVA: 0x002D2FC2 File Offset: 0x002D11C2
		public static FormulaExpression Return(FormulaExpression subject)
		{
			return new CSharpReturn(subject);
		}

		// Token: 0x0600D483 RID: 54403 RVA: 0x002D2FCA File Offset: 0x002D11CA
		public static FormulaExpression RoundDown(FormulaExpression subject, FormulaExpression delta)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<decimal>("RoundDown", new FormulaExpression[] { delta }));
		}

		// Token: 0x0600D484 RID: 54404 RVA: 0x002D2FE6 File Offset: 0x002D11E6
		public static FormulaExpression RoundNearest(FormulaExpression subject, FormulaExpression delta)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<decimal>("RoundNearest", new FormulaExpression[] { delta }));
		}

		// Token: 0x0600D485 RID: 54405 RVA: 0x002D3002 File Offset: 0x002D1202
		public static FormulaExpression RoundUp(FormulaExpression subject, FormulaExpression delta)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<decimal>("RoundUp", new FormulaExpression[] { delta }));
		}

		// Token: 0x0600D486 RID: 54406 RVA: 0x002D301E File Offset: 0x002D121E
		public static FormulaExpression Slice(FormulaExpression subject, FormulaExpression startIndex)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<string>("Slice", new FormulaExpression[] { startIndex }));
		}

		// Token: 0x0600D487 RID: 54407 RVA: 0x002D303A File Offset: 0x002D123A
		public static FormulaExpression Slice(FormulaExpression subject, FormulaExpression startIndex, FormulaExpression endIndex)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<string>("Slice", new FormulaExpression[] { startIndex, endIndex }));
		}

		// Token: 0x0600D488 RID: 54408 RVA: 0x002D305A File Offset: 0x002D125A
		public static FormulaExpression SliceBetween(FormulaExpression subject, FormulaExpression startText, FormulaExpression endText)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<string>("SliceBetween", new FormulaExpression[] { startText, endText }));
		}

		// Token: 0x0600D489 RID: 54409 RVA: 0x002D307A File Offset: 0x002D127A
		public static FormulaExpression Split(FormulaExpression subject, FormulaExpression delimiter)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<string[]>("Split", new FormulaExpression[] { delimiter }));
		}

		// Token: 0x0600D48A RID: 54410 RVA: 0x002D3096 File Offset: 0x002D1296
		public static FormulaExpression StartsWith(FormulaExpression subject, FormulaExpression find, FormulaExpression startAt)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<bool>("StartsWith", new FormulaExpression[] { find }));
		}

		// Token: 0x0600D48B RID: 54411 RVA: 0x002D30B2 File Offset: 0x002D12B2
		public static FormulaExpression StrictDot<TAccessor>(FormulaExpression subject, string accessor)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Variable(accessor, typeof(TAccessor)), false);
		}

		// Token: 0x0600D48C RID: 54412 RVA: 0x002D30CB File Offset: 0x002D12CB
		public static CSharpStringLiteral StringLiteral(string name)
		{
			return new CSharpStringLiteral(name);
		}

		// Token: 0x0600D48D RID: 54413 RVA: 0x002D30D3 File Offset: 0x002D12D3
		public static CSharpStringRange StringRange(int? start, int? end)
		{
			return CSharpExpressionHelper.StringRange((start == null) ? null : CSharpExpressionHelper.NumberLiteral(start.Value), (end == null) ? null : CSharpExpressionHelper.NumberLiteral(end.Value));
		}

		// Token: 0x0600D48E RID: 54414 RVA: 0x002D310A File Offset: 0x002D130A
		public static CSharpStringRange StringRange(FormulaExpression start, FormulaExpression end)
		{
			return new CSharpStringRange(start, end);
		}

		// Token: 0x0600D48F RID: 54415 RVA: 0x002D3113 File Offset: 0x002D1313
		public static FormulaExpression Ternary(FormulaExpression condition, FormulaExpression trueBranch, FormulaExpression falseBranch)
		{
			return new CSharpTernary(condition, trueBranch, falseBranch);
		}

		// Token: 0x0600D490 RID: 54416 RVA: 0x002D3120 File Offset: 0x002D1320
		public static FormulaExpression ToDateTime(FormulaExpression subject)
		{
			IFormulaTyped formulaTyped = subject as IFormulaTyped;
			if (formulaTyped == null || !(formulaTyped.Type == typeof(DateTime)))
			{
				return CSharpExpressionHelper.Func<DateTime>("Convert.ToDateTime", new FormulaExpression[] { subject });
			}
			return subject;
		}

		// Token: 0x0600D491 RID: 54417 RVA: 0x002D3164 File Offset: 0x002D1364
		public static FormulaExpression ToDecimal(FormulaExpression subject)
		{
			IFormulaTyped formulaTyped = subject as IFormulaTyped;
			if (formulaTyped == null || (!(formulaTyped.Type == typeof(decimal)) && !(formulaTyped.Type == typeof(decimal?))))
			{
				return CSharpExpressionHelper.Func<decimal>("Convert.ToDecimal", new FormulaExpression[] { subject });
			}
			return subject;
		}

		// Token: 0x0600D492 RID: 54418 RVA: 0x002D31C0 File Offset: 0x002D13C0
		public static FormulaExpression ToDouble(FormulaExpression subject)
		{
			IFormulaTyped formulaTyped = subject as IFormulaTyped;
			if (formulaTyped != null && formulaTyped.Type == typeof(double))
			{
				return subject;
			}
			CSharpIntLiteral csharpIntLiteral = subject as CSharpIntLiteral;
			if (csharpIntLiteral == null)
			{
				return CSharpExpressionHelper.Func<double>("Convert.ToDouble", new FormulaExpression[] { subject });
			}
			return CSharpExpressionHelper.DoubleLiteral(csharpIntLiteral.Value);
		}

		// Token: 0x0600D493 RID: 54419 RVA: 0x002D321C File Offset: 0x002D141C
		public static FormulaExpression ToInt(FormulaExpression subject)
		{
			IFormulaTyped formulaTyped = subject as IFormulaTyped;
			if (formulaTyped != null && formulaTyped.Type == typeof(int))
			{
				return subject;
			}
			CSharpDoubleLiteral csharpDoubleLiteral = subject as CSharpDoubleLiteral;
			if (csharpDoubleLiteral == null || csharpDoubleLiteral.Value % 1.0 != 0.0)
			{
				return CSharpExpressionHelper.Func<int>("Convert.ToInt32", new FormulaExpression[] { subject });
			}
			return CSharpExpressionHelper.IntLiteral(Convert.ToInt32(csharpDoubleLiteral.Value));
		}

		// Token: 0x0600D494 RID: 54420 RVA: 0x002D3298 File Offset: 0x002D1498
		public static FormulaExpression ToString(FormulaExpression subject)
		{
			IFormulaTyped formulaTyped = subject as IFormulaTyped;
			if (formulaTyped == null || !(formulaTyped.Type == typeof(string)))
			{
				return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<string>("ToString"));
			}
			return subject;
		}

		// Token: 0x0600D495 RID: 54421 RVA: 0x002D32D8 File Offset: 0x002D14D8
		public static FormulaExpression ToString(FormulaExpression subject, FormulaExpression format, FormulaExpression culture)
		{
			if (subject is IFormulaBinaryOperator)
			{
				subject = CSharpExpressionHelper.Parenthesis(subject);
			}
			if (!(culture == null))
			{
				return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<string>("ToString", new FormulaExpression[] { format, culture }));
			}
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<string>("ToString", new FormulaExpression[] { format }));
		}

		// Token: 0x0600D496 RID: 54422 RVA: 0x002D3337 File Offset: 0x002D1537
		public static FormulaExpression ToTitleCase(FormulaExpression subject, FormulaExpression culture)
		{
			if (subject is IFormulaBinaryOperator)
			{
				subject = CSharpExpressionHelper.Parenthesis(subject);
			}
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<string>("ToTitleCase", new FormulaExpression[] { culture }));
		}

		// Token: 0x0600D497 RID: 54423 RVA: 0x002D3363 File Offset: 0x002D1563
		public static FormulaExpression Trim(FormulaExpression subject)
		{
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<string>("Trim"));
		}

		// Token: 0x0600D498 RID: 54424 RVA: 0x002D3375 File Offset: 0x002D1575
		public static FormulaExpression True()
		{
			return CSharpExpressionHelper.Variable("true");
		}

		// Token: 0x0600D499 RID: 54425 RVA: 0x002D3384 File Offset: 0x002D1584
		public static FormulaExpression Upper(FormulaExpression subject)
		{
			IFormulaBinaryOperator formulaBinaryOperator = subject as IFormulaBinaryOperator;
			if (formulaBinaryOperator != null && formulaBinaryOperator.Precedence < 10)
			{
				subject = CSharpExpressionHelper.Parenthesis(subject);
			}
			return CSharpExpressionHelper.Dot(subject, CSharpExpressionHelper.Func<string>("ToUpper"));
		}

		// Token: 0x0600D49A RID: 54426 RVA: 0x002D33BD File Offset: 0x002D15BD
		public static CSharpUsing Using(string statement)
		{
			return new CSharpUsing(statement);
		}

		// Token: 0x0600D49B RID: 54427 RVA: 0x002D33C8 File Offset: 0x002D15C8
		public static CSharpVar Var(FormulaExpression subject)
		{
			CSharpVariable csharpVariable = subject as CSharpVariable;
			CSharpVar csharpVar;
			if (csharpVariable == null)
			{
				CSharpAssign csharpAssign = subject as CSharpAssign;
				if (csharpAssign != null)
				{
					CSharpVariable csharpVariable2 = csharpAssign.Left as CSharpVariable;
					if (csharpVariable2 != null)
					{
						return new CSharpVar(csharpAssign, csharpVariable2.Type);
					}
				}
				csharpVar = new CSharpVar(subject, null);
			}
			else
			{
				csharpVar = new CSharpVar(CSharpExpressionHelper.Variable(csharpVariable.Name), csharpVariable.Type);
			}
			return csharpVar;
		}

		// Token: 0x0600D49C RID: 54428 RVA: 0x002D342B File Offset: 0x002D162B
		public static FormulaExpression Variable<T>(string name)
		{
			return new CSharpVariable(name, typeof(T));
		}

		// Token: 0x0600D49D RID: 54429 RVA: 0x002D343D File Offset: 0x002D163D
		public static FormulaExpression Variable(string name)
		{
			return new CSharpVariable(name, null);
		}

		// Token: 0x0600D49E RID: 54430 RVA: 0x002D3446 File Offset: 0x002D1646
		public static FormulaExpression Variable(string name, Type type)
		{
			return new CSharpVariable(name, type);
		}

		// Token: 0x0600D49F RID: 54431 RVA: 0x002D344F File Offset: 0x002D164F
		public static FormulaExpression Average(IEnumerable<FormulaExpression> values)
		{
			return CSharpExpressionHelper.DotFunc<decimal>(CSharpExpressionHelper.NewArray(values), "Average", Array.Empty<FormulaExpression>());
		}

		// Token: 0x0600D4A0 RID: 54432 RVA: 0x002D3466 File Offset: 0x002D1666
		public static FormulaExpression Divide(double left, FormulaExpression right)
		{
			return CSharpExpressionHelper.Divide(CSharpExpressionHelper.DoubleLiteral(left), right);
		}

		// Token: 0x0600D4A1 RID: 54433 RVA: 0x002D3474 File Offset: 0x002D1674
		public static FormulaExpression Divide(int left, FormulaExpression right)
		{
			return CSharpExpressionHelper.Divide(CSharpExpressionHelper.IntLiteral(left), right);
		}

		// Token: 0x0600D4A2 RID: 54434 RVA: 0x002D3482 File Offset: 0x002D1682
		public static FormulaExpression Divide(FormulaExpression left, double right)
		{
			return CSharpExpressionHelper.Divide(left, CSharpExpressionHelper.DoubleLiteral(right));
		}

		// Token: 0x0600D4A3 RID: 54435 RVA: 0x002D3490 File Offset: 0x002D1690
		public static FormulaExpression Divide(FormulaExpression left, int right)
		{
			return CSharpExpressionHelper.Divide(left, CSharpExpressionHelper.IntLiteral(right));
		}

		// Token: 0x0600D4A4 RID: 54436 RVA: 0x002D34A0 File Offset: 0x002D16A0
		public static FormulaExpression Divide(FormulaExpression left, FormulaExpression right)
		{
			IFormulaTyped formulaTyped = left as IFormulaTyped;
			if (formulaTyped != null)
			{
				IFormulaTyped formulaTyped2 = right as IFormulaTyped;
				if (formulaTyped2 != null)
				{
					bool flag = formulaTyped.Type == typeof(decimal) || formulaTyped.Type == typeof(decimal?);
					bool flag2 = formulaTyped2.Type == typeof(decimal) || formulaTyped2.Type == typeof(decimal?);
					if (formulaTyped.Type == typeof(object) || formulaTyped2.Type == typeof(object) || formulaTyped.Type == null || formulaTyped2.Type == null)
					{
						left = CSharpExpressionHelper.ToDecimal(left);
						right = CSharpExpressionHelper.ToDecimal(right);
					}
					else if ((flag && !flag2) || (!flag && flag2))
					{
						left = CSharpExpressionHelper.ToDecimal(left);
						right = CSharpExpressionHelper.ToDecimal(right);
					}
				}
			}
			CSharpIntLiteral csharpIntLiteral = right as CSharpIntLiteral;
			if (csharpIntLiteral == null || csharpIntLiteral.Value != 1.0)
			{
				return new CSharpDivide(left, right);
			}
			return left;
		}

		// Token: 0x0600D4A5 RID: 54437 RVA: 0x002D35C8 File Offset: 0x002D17C8
		public static FormulaExpression Midpoint(FormulaExpression start, FormulaExpression end)
		{
			return CSharpExpressionHelper.Plus(start, CSharpExpressionHelper.Divide(CSharpExpressionHelper.Minus(end, start), 2));
		}

		// Token: 0x0600D4A6 RID: 54438 RVA: 0x002D35DD File Offset: 0x002D17DD
		public static FormulaExpression Minus(double left, FormulaExpression right)
		{
			return CSharpExpressionHelper.Minus(CSharpExpressionHelper.DoubleLiteral(left), right);
		}

		// Token: 0x0600D4A7 RID: 54439 RVA: 0x002D35EB File Offset: 0x002D17EB
		public static FormulaExpression Minus(FormulaExpression left, double right)
		{
			return CSharpExpressionHelper.Minus(left, CSharpExpressionHelper.DoubleLiteral(right));
		}

		// Token: 0x0600D4A8 RID: 54440 RVA: 0x002D35F9 File Offset: 0x002D17F9
		public static FormulaExpression Minus(FormulaExpression subject)
		{
			return new CSharpUnaryMinus((subject is IFormulaBinaryOperator) ? CSharpExpressionHelper.Parenthesis(subject) : subject);
		}

		// Token: 0x0600D4A9 RID: 54441 RVA: 0x002D3611 File Offset: 0x002D1811
		public static FormulaExpression Minus(int left, FormulaExpression right)
		{
			return CSharpExpressionHelper.Minus(CSharpExpressionHelper.IntLiteral(left), right);
		}

		// Token: 0x0600D4AA RID: 54442 RVA: 0x002D361F File Offset: 0x002D181F
		public static FormulaExpression Minus(FormulaExpression left, int right)
		{
			return CSharpExpressionHelper.Minus(left, CSharpExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D4AB RID: 54443 RVA: 0x002D3630 File Offset: 0x002D1830
		public static FormulaExpression Minus(FormulaExpression left, FormulaExpression right)
		{
			IFormulaTyped formulaTyped = left as IFormulaTyped;
			if (formulaTyped != null)
			{
				IFormulaTyped formulaTyped2 = right as IFormulaTyped;
				if (formulaTyped2 != null)
				{
					bool flag = formulaTyped.Type == typeof(decimal) || formulaTyped.Type == typeof(decimal?);
					bool flag2 = formulaTyped2.Type == typeof(decimal) || formulaTyped2.Type == typeof(decimal?);
					if (formulaTyped.Type == typeof(object) || formulaTyped2.Type == typeof(object) || formulaTyped.Type == null || formulaTyped2.Type == null)
					{
						left = CSharpExpressionHelper.ToDecimal(left);
						right = CSharpExpressionHelper.ToDecimal(right);
					}
					else if ((flag && !flag2) || (!flag && flag2))
					{
						left = CSharpExpressionHelper.ToDecimal(left);
						right = CSharpExpressionHelper.ToDecimal(right);
					}
				}
			}
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
					return CSharpExpressionHelper.NumberLiteral(formulaNumberLiteral2.Value - formulaNumberLiteral.Value);
				}
				if (formulaNumberLiteral.Value < 0.0)
				{
					return CSharpExpressionHelper.Plus(left, CSharpExpressionHelper.NumberLiteral(-formulaNumberLiteral.Value));
				}
				FormulaPlus formulaPlus = left as FormulaPlus;
				if (formulaPlus != null)
				{
					FormulaNumberLiteral formulaNumberLiteral3 = formulaPlus.Right as FormulaNumberLiteral;
					if (formulaNumberLiteral3 != null)
					{
						return CSharpExpressionHelper.Plus(formulaPlus.Left, CSharpExpressionHelper.Minus(formulaNumberLiteral3, formulaNumberLiteral));
					}
				}
				FormulaMinus formulaMinus = left as FormulaMinus;
				if (formulaMinus != null)
				{
					FormulaNumberLiteral formulaNumberLiteral4 = formulaMinus.Right as FormulaNumberLiteral;
					if (formulaNumberLiteral4 != null)
					{
						return CSharpExpressionHelper.Minus(formulaMinus.Left, CSharpExpressionHelper.Plus(formulaNumberLiteral4, formulaNumberLiteral));
					}
				}
			}
			return new CSharpMinus(left, right);
		}

		// Token: 0x0600D4AC RID: 54444 RVA: 0x002D3805 File Offset: 0x002D1A05
		public static FormulaExpression Minus1(FormulaExpression val)
		{
			return CSharpExpressionHelper.Minus(val, CSharpExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600D4AD RID: 54445 RVA: 0x002D3813 File Offset: 0x002D1A13
		public static FormulaExpression Modulo(FormulaExpression left, double right)
		{
			return CSharpExpressionHelper.Modulo(left, CSharpExpressionHelper.DoubleLiteral(right));
		}

		// Token: 0x0600D4AE RID: 54446 RVA: 0x002D3821 File Offset: 0x002D1A21
		public static FormulaExpression Modulo(FormulaExpression left, FormulaExpression right)
		{
			return new CSharpModulo(left, right);
		}

		// Token: 0x0600D4AF RID: 54447 RVA: 0x002D382A File Offset: 0x002D1A2A
		public static FormulaExpression Multiply(int left, FormulaExpression right)
		{
			return CSharpExpressionHelper.Multiply(CSharpExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600D4B0 RID: 54448 RVA: 0x002D3838 File Offset: 0x002D1A38
		public static FormulaExpression Multiply(double left, FormulaExpression right)
		{
			return CSharpExpressionHelper.Multiply(CSharpExpressionHelper.DoubleLiteral(left), right);
		}

		// Token: 0x0600D4B1 RID: 54449 RVA: 0x002D3846 File Offset: 0x002D1A46
		public static FormulaExpression Multiply(FormulaExpression left, double right)
		{
			return CSharpExpressionHelper.Multiply(left, CSharpExpressionHelper.DoubleLiteral(right));
		}

		// Token: 0x0600D4B2 RID: 54450 RVA: 0x002D3854 File Offset: 0x002D1A54
		public static FormulaExpression Multiply(FormulaExpression left, int right)
		{
			return CSharpExpressionHelper.Multiply(left, CSharpExpressionHelper.IntLiteral(right));
		}

		// Token: 0x0600D4B3 RID: 54451 RVA: 0x002D3864 File Offset: 0x002D1A64
		public static FormulaExpression Multiply(FormulaExpression left, FormulaExpression right)
		{
			IFormulaTyped formulaTyped = left as IFormulaTyped;
			if (formulaTyped != null)
			{
				IFormulaTyped formulaTyped2 = right as IFormulaTyped;
				if (formulaTyped2 != null)
				{
					bool flag = formulaTyped.Type == typeof(decimal) || formulaTyped.Type == typeof(decimal?);
					bool flag2 = formulaTyped2.Type == typeof(decimal) || formulaTyped2.Type == typeof(decimal?);
					if (formulaTyped.Type == typeof(object) || formulaTyped2.Type == typeof(object) || formulaTyped.Type == null || formulaTyped2.Type == null)
					{
						left = CSharpExpressionHelper.ToDecimal(left);
						right = CSharpExpressionHelper.ToDecimal(right);
					}
					else if ((flag && !flag2) || (!flag && flag2))
					{
						left = CSharpExpressionHelper.ToDecimal(left);
						right = CSharpExpressionHelper.ToDecimal(right);
					}
				}
			}
			CSharpIntLiteral csharpIntLiteral = right as CSharpIntLiteral;
			if (csharpIntLiteral != null && csharpIntLiteral.Value == 1.0)
			{
				return left;
			}
			csharpIntLiteral = left as CSharpIntLiteral;
			if (csharpIntLiteral == null || csharpIntLiteral.Value != 1.0)
			{
				return new CSharpMultiply(left, right);
			}
			return right;
		}

		// Token: 0x0600D4B4 RID: 54452 RVA: 0x002D39AC File Offset: 0x002D1BAC
		public static FormulaExpression NewArray(IEnumerable<FormulaExpression> values)
		{
			return new CSharpNewArray(values);
		}

		// Token: 0x0600D4B5 RID: 54453 RVA: 0x002D39B4 File Offset: 0x002D1BB4
		public static FormulaExpression Plus(double left, FormulaExpression right)
		{
			return CSharpExpressionHelper.Plus(CSharpExpressionHelper.DoubleLiteral(left), right);
		}

		// Token: 0x0600D4B6 RID: 54454 RVA: 0x002D39C2 File Offset: 0x002D1BC2
		public static FormulaExpression Plus(FormulaExpression left, double right)
		{
			return CSharpExpressionHelper.Plus(left, CSharpExpressionHelper.DoubleLiteral(right));
		}

		// Token: 0x0600D4B7 RID: 54455 RVA: 0x002D39D0 File Offset: 0x002D1BD0
		public static FormulaExpression Plus(int left, FormulaExpression right)
		{
			return CSharpExpressionHelper.Plus(CSharpExpressionHelper.IntLiteral(left), right);
		}

		// Token: 0x0600D4B8 RID: 54456 RVA: 0x002D39DE File Offset: 0x002D1BDE
		public static FormulaExpression Plus(FormulaExpression left, int right)
		{
			return CSharpExpressionHelper.Plus(left, CSharpExpressionHelper.IntLiteral(right));
		}

		// Token: 0x0600D4B9 RID: 54457 RVA: 0x002D39EC File Offset: 0x002D1BEC
		public static FormulaExpression Plus(FormulaExpression left, FormulaExpression right)
		{
			IFormulaTyped formulaTyped = left as IFormulaTyped;
			if (formulaTyped != null)
			{
				IFormulaTyped formulaTyped2 = right as IFormulaTyped;
				if (formulaTyped2 != null)
				{
					bool flag = formulaTyped.Type == typeof(decimal) || formulaTyped.Type == typeof(decimal?);
					bool flag2 = formulaTyped2.Type == typeof(decimal) || formulaTyped2.Type == typeof(decimal?);
					if (formulaTyped.Type == typeof(object) || formulaTyped2.Type == typeof(object) || formulaTyped.Type == null || formulaTyped2.Type == null)
					{
						left = CSharpExpressionHelper.ToDecimal(left);
						right = CSharpExpressionHelper.ToDecimal(right);
					}
					else if ((flag && !flag2) || (!flag && flag2))
					{
						left = CSharpExpressionHelper.ToDecimal(left);
						right = CSharpExpressionHelper.ToDecimal(right);
					}
				}
			}
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
						return CSharpExpressionHelper.NumberLiteral(formulaNumberLiteral2.Value + formulaNumberLiteral.Value);
					}
					return right;
				}
				else
				{
					if (formulaNumberLiteral.Value < 0.0)
					{
						return CSharpExpressionHelper.Minus(left, CSharpExpressionHelper.NumberLiteral(-formulaNumberLiteral.Value));
					}
					FormulaPlus formulaPlus = left as FormulaPlus;
					if (formulaPlus != null)
					{
						FormulaNumberLiteral formulaNumberLiteral3 = formulaPlus.Right as FormulaNumberLiteral;
						if (formulaNumberLiteral3 != null)
						{
							return CSharpExpressionHelper.Plus(formulaPlus.Left, CSharpExpressionHelper.Plus(formulaNumberLiteral3, formulaNumberLiteral));
						}
					}
					FormulaMinus formulaMinus = left as FormulaMinus;
					if (formulaMinus != null)
					{
						FormulaNumberLiteral formulaNumberLiteral4 = formulaMinus.Right as FormulaNumberLiteral;
						if (formulaNumberLiteral4 != null)
						{
							return CSharpExpressionHelper.Minus(formulaMinus.Left, CSharpExpressionHelper.Minus(formulaNumberLiteral4, formulaNumberLiteral));
						}
					}
				}
			}
			return new CSharpPlus(left, right);
		}

		// Token: 0x0600D4BA RID: 54458 RVA: 0x002D3BD5 File Offset: 0x002D1DD5
		public static FormulaExpression Plus1(FormulaExpression val)
		{
			return CSharpExpressionHelper.Plus(val, CSharpExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600D4BB RID: 54459 RVA: 0x002D3BE4 File Offset: 0x002D1DE4
		public static FormulaExpression Power(FormulaExpression left, FormulaExpression right)
		{
			CSharpIntLiteral csharpIntLiteral = left as CSharpIntLiteral;
			if (csharpIntLiteral != null)
			{
				CSharpIntLiteral csharpIntLiteral2 = right as CSharpIntLiteral;
				if (csharpIntLiteral2 != null)
				{
					return CSharpExpressionHelper.NumberLiteral(Math.Pow(csharpIntLiteral.Value, csharpIntLiteral2.Value));
				}
			}
			return new CSharpExponent(left, right);
		}

		// Token: 0x0600D4BC RID: 54460 RVA: 0x002D3C24 File Offset: 0x002D1E24
		public static FormulaExpression Product(IEnumerable<FormulaExpression> values)
		{
			FormulaExpression formulaExpression = CSharpExpressionHelper.Variable("total");
			FormulaExpression formulaExpression2 = CSharpExpressionHelper.Variable("next");
			return CSharpExpressionHelper.DotFunc<decimal>(CSharpExpressionHelper.NewArray(values), "Aggregate", new FormulaExpression[] { CSharpExpressionHelper.Lambda(new FormulaExpression[] { formulaExpression, formulaExpression2 }, CSharpExpressionHelper.Multiply(formulaExpression, formulaExpression2)) });
		}

		// Token: 0x0600D4BD RID: 54461 RVA: 0x002D3C7A File Offset: 0x002D1E7A
		public static FormulaExpression Sum(IEnumerable<FormulaExpression> values)
		{
			return CSharpExpressionHelper.DotFunc<decimal>(CSharpExpressionHelper.NewArray(values), "Sum", Array.Empty<FormulaExpression>());
		}

		// Token: 0x0600D4BE RID: 54462 RVA: 0x002D3C91 File Offset: 0x002D1E91
		public static FormulaExpression DecimalLiteral(decimal value)
		{
			return new CSharpDecimalLiteral(value);
		}

		// Token: 0x0600D4BF RID: 54463 RVA: 0x002D3C99 File Offset: 0x002D1E99
		public static FormulaExpression DoubleLiteral(double value)
		{
			return new CSharpDoubleLiteral(value);
		}

		// Token: 0x0600D4C0 RID: 54464 RVA: 0x002D3CA1 File Offset: 0x002D1EA1
		public static FormulaExpression IntLiteral(int value)
		{
			return new CSharpIntLiteral(value);
		}

		// Token: 0x0600D4C1 RID: 54465 RVA: 0x002D3CA9 File Offset: 0x002D1EA9
		public static FormulaExpression NumberLiteral(int value)
		{
			return CSharpExpressionHelper.IntLiteral(value);
		}

		// Token: 0x0600D4C2 RID: 54466 RVA: 0x002D3CB1 File Offset: 0x002D1EB1
		public static FormulaExpression NumberLiteral(double value)
		{
			if (value.Scale() != 0)
			{
				return CSharpExpressionHelper.DoubleLiteral(value);
			}
			return CSharpExpressionHelper.IntLiteral(Convert.ToInt32(value));
		}

		// Token: 0x0600D4C3 RID: 54467 RVA: 0x002D3CCD File Offset: 0x002D1ECD
		public static FormulaExpression NumberLiteral(decimal value)
		{
			if (value.Scale() != 0)
			{
				return CSharpExpressionHelper.DoubleLiteral(Convert.ToDouble(value));
			}
			return CSharpExpressionHelper.IntLiteral(Convert.ToInt32(value));
		}

		// Token: 0x0600D4C4 RID: 54468 RVA: 0x002D3CEE File Offset: 0x002D1EEE
		public static FormulaExpression GreaterThan(FormulaExpression left, decimal right)
		{
			return CSharpExpressionHelper.GreaterThan(left, CSharpExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D4C5 RID: 54469 RVA: 0x002D3CFC File Offset: 0x002D1EFC
		public static FormulaExpression GreaterThan(FormulaExpression left, int right)
		{
			return CSharpExpressionHelper.GreaterThan(left, CSharpExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D4C6 RID: 54470 RVA: 0x002D3D0A File Offset: 0x002D1F0A
		public static FormulaExpression GreaterThan(FormulaExpression left, double right)
		{
			return CSharpExpressionHelper.GreaterThan(left, CSharpExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D4C7 RID: 54471 RVA: 0x002D3D18 File Offset: 0x002D1F18
		public static FormulaExpression GreaterThan(FormulaExpression left, FormulaExpression right)
		{
			return new CSharpGreaterThan(left, right);
		}

		// Token: 0x0600D4C8 RID: 54472 RVA: 0x002D3D21 File Offset: 0x002D1F21
		public static FormulaExpression GreaterThanEqual(FormulaExpression left, int right)
		{
			return CSharpExpressionHelper.GreaterThanEqual(left, CSharpExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D4C9 RID: 54473 RVA: 0x002D3D2F File Offset: 0x002D1F2F
		public static FormulaExpression GreaterThanEqual(FormulaExpression left, decimal right)
		{
			return CSharpExpressionHelper.GreaterThanEqual(left, CSharpExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D4CA RID: 54474 RVA: 0x002D3D3D File Offset: 0x002D1F3D
		public static FormulaExpression GreaterThanEqual(FormulaExpression left, double right)
		{
			return CSharpExpressionHelper.GreaterThanEqual(left, CSharpExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D4CB RID: 54475 RVA: 0x002D3D4B File Offset: 0x002D1F4B
		public static FormulaExpression GreaterThanEqual(FormulaExpression left, FormulaExpression right)
		{
			return new CSharpGreaterThanEqual(left, right);
		}

		// Token: 0x0600D4CC RID: 54476 RVA: 0x002D3D54 File Offset: 0x002D1F54
		public static FormulaExpression LessThan(FormulaExpression left, int right)
		{
			return CSharpExpressionHelper.LessThan(left, CSharpExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D4CD RID: 54477 RVA: 0x002D3D62 File Offset: 0x002D1F62
		public static FormulaExpression LessThan(FormulaExpression left, decimal right)
		{
			return CSharpExpressionHelper.LessThan(left, CSharpExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D4CE RID: 54478 RVA: 0x002D3D70 File Offset: 0x002D1F70
		public static FormulaExpression LessThan(FormulaExpression left, double right)
		{
			return CSharpExpressionHelper.LessThan(left, CSharpExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D4CF RID: 54479 RVA: 0x002D3D7E File Offset: 0x002D1F7E
		public static FormulaExpression LessThan(FormulaExpression left, FormulaExpression right)
		{
			return new CSharpLessThan(left, right);
		}

		// Token: 0x0600D4D0 RID: 54480 RVA: 0x002D3D87 File Offset: 0x002D1F87
		public static FormulaExpression LessThanEqual(FormulaExpression left, int right)
		{
			return CSharpExpressionHelper.LessThanEqual(left, CSharpExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D4D1 RID: 54481 RVA: 0x002D3D95 File Offset: 0x002D1F95
		public static FormulaExpression LessThanEqual(FormulaExpression left, decimal right)
		{
			return CSharpExpressionHelper.LessThanEqual(left, CSharpExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D4D2 RID: 54482 RVA: 0x002D3DA3 File Offset: 0x002D1FA3
		public static FormulaExpression LessThanEqual(FormulaExpression left, double right)
		{
			return CSharpExpressionHelper.LessThanEqual(left, CSharpExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D4D3 RID: 54483 RVA: 0x002D3DB1 File Offset: 0x002D1FB1
		public static FormulaExpression LessThanEqual(FormulaExpression left, FormulaExpression right)
		{
			return new CSharpLessThanEqual(left, right);
		}

		// Token: 0x0600D4D4 RID: 54484 RVA: 0x002D3DBA File Offset: 0x002D1FBA
		public static FormulaExpression NotEqual(FormulaExpression left, FormulaExpression right)
		{
			return new CSharpNotEqual(left, right);
		}

		// Token: 0x0600D4D5 RID: 54485 RVA: 0x002D3DC3 File Offset: 0x002D1FC3
		public static FormulaExpression DateTime(int year, int month, int day)
		{
			return CSharpExpressionHelper.DateTime(CSharpExpressionHelper.NumberLiteral(year), CSharpExpressionHelper.NumberLiteral(month), CSharpExpressionHelper.NumberLiteral(day));
		}

		// Token: 0x0600D4D6 RID: 54486 RVA: 0x002D3DDC File Offset: 0x002D1FDC
		public static FormulaExpression DateTime(FormulaExpression year, int month, int day)
		{
			return CSharpExpressionHelper.DateTime(year, CSharpExpressionHelper.NumberLiteral(month), CSharpExpressionHelper.NumberLiteral(day));
		}

		// Token: 0x0600D4D7 RID: 54487 RVA: 0x002D3DF0 File Offset: 0x002D1FF0
		public static FormulaExpression DateTime(int year, FormulaExpression month, int day)
		{
			return CSharpExpressionHelper.DateTime(CSharpExpressionHelper.NumberLiteral(year), month, CSharpExpressionHelper.NumberLiteral(day));
		}

		// Token: 0x0600D4D8 RID: 54488 RVA: 0x002D3E04 File Offset: 0x002D2004
		public static FormulaExpression DateTime(FormulaExpression year, FormulaExpression month, FormulaExpression day)
		{
			return new CSharpDateTime(year, month, day, null, null, null, null);
		}

		// Token: 0x0600D4D9 RID: 54489 RVA: 0x002D3E12 File Offset: 0x002D2012
		public static FormulaExpression DateTime(FormulaExpression year, FormulaExpression month, FormulaExpression day, FormulaExpression hour, FormulaExpression minute, FormulaExpression second)
		{
			return new CSharpDateTime(year, month, day, hour, minute, second, CSharpExpressionHelper.NumberLiteral(0));
		}

		// Token: 0x0600D4DA RID: 54490 RVA: 0x002D3E27 File Offset: 0x002D2027
		public static FormulaExpression DateTime(FormulaExpression year, FormulaExpression month, FormulaExpression day, FormulaExpression hour, FormulaExpression minute, FormulaExpression second, FormulaExpression millisecond)
		{
			return new CSharpDateTime(year, month, day, hour, minute, second, millisecond);
		}

		// Token: 0x0600D4DB RID: 54491 RVA: 0x002D3E38 File Offset: 0x002D2038
		public static FormulaExpression Day(FormulaExpression subject)
		{
			return CSharpExpressionHelper.Dot<int>(subject, "Day");
		}

		// Token: 0x0600D4DC RID: 54492 RVA: 0x002D3E45 File Offset: 0x002D2045
		public static FormulaExpression DayEnd(FormulaExpression dayStart)
		{
			return CSharpExpressionHelper.AddDays(dayStart, 1);
		}

		// Token: 0x0600D4DD RID: 54493 RVA: 0x002D3E4E File Offset: 0x002D204E
		public static FormulaExpression DayStart(FormulaExpression subject)
		{
			return CSharpExpressionHelper.DateTime(CSharpExpressionHelper.Year(subject), CSharpExpressionHelper.Month(subject), CSharpExpressionHelper.Day(subject));
		}

		// Token: 0x0600D4DE RID: 54494 RVA: 0x002D3E67 File Offset: 0x002D2067
		public static FormulaExpression Hour(FormulaExpression subject)
		{
			return CSharpExpressionHelper.Dot<int>(subject, "Hour");
		}

		// Token: 0x0600D4DF RID: 54495 RVA: 0x002D3E74 File Offset: 0x002D2074
		public static FormulaExpression HourEnd(FormulaExpression hourStart)
		{
			return CSharpExpressionHelper.AddHours(hourStart, 1);
		}

		// Token: 0x0600D4E0 RID: 54496 RVA: 0x002D3E7D File Offset: 0x002D207D
		public static FormulaExpression HourStart(FormulaExpression subject)
		{
			return CSharpExpressionHelper.DateTime(CSharpExpressionHelper.Year(subject), CSharpExpressionHelper.Month(subject), CSharpExpressionHelper.Day(subject), CSharpExpressionHelper.Hour(subject), CSharpExpressionHelper.NumberLiteral(0), CSharpExpressionHelper.NumberLiteral(0));
		}

		// Token: 0x0600D4E1 RID: 54497 RVA: 0x002D3EA8 File Offset: 0x002D20A8
		public static FormulaExpression Minute(FormulaExpression subject)
		{
			return CSharpExpressionHelper.Dot<int>(subject, "Minute");
		}

		// Token: 0x0600D4E2 RID: 54498 RVA: 0x002D3EB5 File Offset: 0x002D20B5
		public static FormulaExpression MinuteEnd(FormulaExpression minuteStart)
		{
			return CSharpExpressionHelper.AddMinutes(minuteStart, 1);
		}

		// Token: 0x0600D4E3 RID: 54499 RVA: 0x002D3EBE File Offset: 0x002D20BE
		public static FormulaExpression MinuteStart(FormulaExpression subject)
		{
			return CSharpExpressionHelper.DateTime(CSharpExpressionHelper.Year(subject), CSharpExpressionHelper.Month(subject), CSharpExpressionHelper.Day(subject), CSharpExpressionHelper.Hour(subject), CSharpExpressionHelper.Minute(subject), CSharpExpressionHelper.NumberLiteral(0));
		}

		// Token: 0x0600D4E4 RID: 54500 RVA: 0x002D3EE9 File Offset: 0x002D20E9
		public static FormulaExpression Month(FormulaExpression subject)
		{
			return CSharpExpressionHelper.Dot<int>(subject, "Month");
		}

		// Token: 0x0600D4E5 RID: 54501 RVA: 0x002D3EF6 File Offset: 0x002D20F6
		public static FormulaExpression MonthDays(FormulaExpression calendar, FormulaExpression year, FormulaExpression month)
		{
			return CSharpExpressionHelper.DotFunc<int>(calendar, "GetDaysInMonth", new FormulaExpression[] { year, month });
		}

		// Token: 0x0600D4E6 RID: 54502 RVA: 0x002D3F11 File Offset: 0x002D2111
		public static FormulaExpression MonthEnd(FormulaExpression monthStart)
		{
			return CSharpExpressionHelper.AddMonths(monthStart, 1);
		}

		// Token: 0x0600D4E7 RID: 54503 RVA: 0x002D3F1A File Offset: 0x002D211A
		public static FormulaExpression MonthStart(FormulaExpression subject)
		{
			return CSharpExpressionHelper.DateTime(CSharpExpressionHelper.Year(subject), CSharpExpressionHelper.Month(subject), CSharpExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600D4E8 RID: 54504 RVA: 0x002D3F33 File Offset: 0x002D2133
		public static FormulaExpression Second(FormulaExpression subject)
		{
			return CSharpExpressionHelper.Dot<int>(subject, "Second");
		}

		// Token: 0x0600D4E9 RID: 54505 RVA: 0x002D3F40 File Offset: 0x002D2140
		public static FormulaExpression SecondEnd(FormulaExpression secondStart)
		{
			return CSharpExpressionHelper.DotFunc<DateTime>(secondStart, "AddSeconds", new FormulaExpression[] { CSharpExpressionHelper.NumberLiteral(1) });
		}

		// Token: 0x0600D4EA RID: 54506 RVA: 0x002D3F5C File Offset: 0x002D215C
		public static FormulaExpression SecondStart(FormulaExpression subject)
		{
			return CSharpExpressionHelper.DateTime(CSharpExpressionHelper.Year(subject), CSharpExpressionHelper.Month(subject), CSharpExpressionHelper.Day(subject), CSharpExpressionHelper.Hour(subject), CSharpExpressionHelper.Minute(subject), CSharpExpressionHelper.Second(subject));
		}

		// Token: 0x0600D4EB RID: 54507 RVA: 0x002D3F87 File Offset: 0x002D2187
		public static FormulaExpression WeekDay(FormulaExpression subject)
		{
			return CSharpExpressionHelper.Cast<int>(CSharpExpressionHelper.Dot<int>(subject, "DayOfWeek"));
		}

		// Token: 0x0600D4EC RID: 54508 RVA: 0x002D3F99 File Offset: 0x002D2199
		public static FormulaExpression WeekEnd(FormulaExpression weekStart)
		{
			return CSharpExpressionHelper.DotFunc<DateTime>(weekStart, "AddDays", new FormulaExpression[] { CSharpExpressionHelper.NumberLiteral(7) });
		}

		// Token: 0x0600D4ED RID: 54509 RVA: 0x002D3FB5 File Offset: 0x002D21B5
		public static FormulaExpression WeekStart(FormulaExpression subject)
		{
			return CSharpExpressionHelper.AddDays(CSharpExpressionHelper.DayStart(subject), CSharpExpressionHelper.Minus(CSharpExpressionHelper.WeekDay(subject)));
		}

		// Token: 0x0600D4EE RID: 54510 RVA: 0x002D3FCD File Offset: 0x002D21CD
		public static FormulaExpression Year(FormulaExpression subject)
		{
			return CSharpExpressionHelper.Dot<int>(subject, "Year");
		}

		// Token: 0x0600D4EF RID: 54511 RVA: 0x002D3FDA File Offset: 0x002D21DA
		public static FormulaExpression YearDay(FormulaExpression calendar, FormulaExpression subject)
		{
			return CSharpExpressionHelper.DotFunc<int>(calendar, "GetDayOfYear", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D4F0 RID: 54512 RVA: 0x002D3FF1 File Offset: 0x002D21F1
		public static FormulaExpression YearDays(FormulaExpression calendar, FormulaExpression subject)
		{
			return CSharpExpressionHelper.DotFunc<int>(calendar, "GetDaysInYear", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D4F1 RID: 54513 RVA: 0x002D4008 File Offset: 0x002D2208
		public static FormulaExpression YearEnd(FormulaExpression yearStart)
		{
			return CSharpExpressionHelper.DotFunc<DateTime>(yearStart, "AddYears", new FormulaExpression[] { CSharpExpressionHelper.NumberLiteral(1) });
		}

		// Token: 0x0600D4F2 RID: 54514 RVA: 0x002D4024 File Offset: 0x002D2224
		public static FormulaExpression YearStart(FormulaExpression subject)
		{
			return CSharpExpressionHelper.DateTime(CSharpExpressionHelper.Year(subject), CSharpExpressionHelper.NumberLiteral(1), CSharpExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600D4F3 RID: 54515 RVA: 0x002D403D File Offset: 0x002D223D
		public static FormulaExpression AddDays(FormulaExpression subject, int amount)
		{
			return CSharpExpressionHelper.AddDays(subject, CSharpExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D4F4 RID: 54516 RVA: 0x002D404B File Offset: 0x002D224B
		public static FormulaExpression AddDays(FormulaExpression subject, FormulaExpression amount)
		{
			return CSharpExpressionHelper.DotFunc<DateTime>(subject, "AddDays", new FormulaExpression[] { amount });
		}

		// Token: 0x0600D4F5 RID: 54517 RVA: 0x002D4062 File Offset: 0x002D2262
		public static FormulaExpression AddHours(FormulaExpression subject, int amount)
		{
			return CSharpExpressionHelper.AddHours(subject, CSharpExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D4F6 RID: 54518 RVA: 0x002D4070 File Offset: 0x002D2270
		public static FormulaExpression AddHours(FormulaExpression subject, FormulaExpression amount)
		{
			return CSharpExpressionHelper.DotFunc<DateTime>(subject, "AddHours", new FormulaExpression[] { amount });
		}

		// Token: 0x0600D4F7 RID: 54519 RVA: 0x002D4087 File Offset: 0x002D2287
		public static FormulaExpression AddMilliseconds(FormulaExpression subject, int amount)
		{
			return CSharpExpressionHelper.AddMilliseconds(subject, CSharpExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D4F8 RID: 54520 RVA: 0x002D4095 File Offset: 0x002D2295
		public static FormulaExpression AddMilliseconds(FormulaExpression subject, FormulaExpression amount)
		{
			return CSharpExpressionHelper.DotFunc<DateTime>(subject, "AddMilliseconds", new FormulaExpression[] { amount });
		}

		// Token: 0x0600D4F9 RID: 54521 RVA: 0x002D40AC File Offset: 0x002D22AC
		public static FormulaExpression AddMinutes(FormulaExpression subject, int amount)
		{
			return CSharpExpressionHelper.AddMinutes(subject, CSharpExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D4FA RID: 54522 RVA: 0x002D40BA File Offset: 0x002D22BA
		public static FormulaExpression AddMinutes(FormulaExpression subject, FormulaExpression amount)
		{
			return CSharpExpressionHelper.DotFunc<DateTime>(subject, "AddMinutes", new FormulaExpression[] { amount });
		}

		// Token: 0x0600D4FB RID: 54523 RVA: 0x002D40D1 File Offset: 0x002D22D1
		public static FormulaExpression AddMonths(FormulaExpression subject, int amount)
		{
			return CSharpExpressionHelper.AddMonths(subject, CSharpExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D4FC RID: 54524 RVA: 0x002D40DF File Offset: 0x002D22DF
		public static FormulaExpression AddMonths(FormulaExpression subject, FormulaExpression amount)
		{
			return CSharpExpressionHelper.DotFunc<DateTime>(subject, "AddMonths", new FormulaExpression[] { amount });
		}

		// Token: 0x0600D4FD RID: 54525 RVA: 0x002D40F6 File Offset: 0x002D22F6
		public static FormulaExpression AddSeconds(FormulaExpression subject, int amount)
		{
			return CSharpExpressionHelper.AddSeconds(subject, CSharpExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D4FE RID: 54526 RVA: 0x002D4104 File Offset: 0x002D2304
		public static FormulaExpression AddSeconds(FormulaExpression subject, FormulaExpression amount)
		{
			return CSharpExpressionHelper.DotFunc<DateTime>(subject, "AddSeconds", new FormulaExpression[] { amount });
		}

		// Token: 0x0600D4FF RID: 54527 RVA: 0x002D411B File Offset: 0x002D231B
		public static FormulaExpression AddYears(FormulaExpression subject, int amount)
		{
			return CSharpExpressionHelper.AddYears(subject, CSharpExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D500 RID: 54528 RVA: 0x002D4129 File Offset: 0x002D2329
		public static FormulaExpression AddYears(FormulaExpression subject, FormulaExpression amount)
		{
			return CSharpExpressionHelper.DotFunc<DateTime>(subject, "AddYears", new FormulaExpression[] { amount });
		}

		// Token: 0x0600D501 RID: 54529 RVA: 0x002D4140 File Offset: 0x002D2340
		public static FormulaExpression TimeSpanDays(FormulaExpression subject, double amount)
		{
			return CSharpExpressionHelper.TimeSpanDays(subject, CSharpExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D502 RID: 54530 RVA: 0x002D414E File Offset: 0x002D234E
		public static FormulaExpression TimeSpanDays(FormulaExpression subject, FormulaExpression amount)
		{
			return CSharpExpressionHelper.Func<DateTime>("TimeSpan.FromDays", new FormulaExpression[] { amount });
		}

		// Token: 0x0600D503 RID: 54531 RVA: 0x002D4164 File Offset: 0x002D2364
		public static FormulaExpression TimeSpanHours(FormulaExpression subject, double amount)
		{
			return CSharpExpressionHelper.TimeSpanHours(subject, CSharpExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D504 RID: 54532 RVA: 0x002D4172 File Offset: 0x002D2372
		public static FormulaExpression TimeSpanHours(FormulaExpression subject, FormulaExpression amount)
		{
			return CSharpExpressionHelper.Func<DateTime>("TimeSpan.FromHours", new FormulaExpression[] { amount });
		}

		// Token: 0x0600D505 RID: 54533 RVA: 0x002D4188 File Offset: 0x002D2388
		public static FormulaExpression TimeSpanMilliseconds(FormulaExpression subject, double amount)
		{
			return CSharpExpressionHelper.TimeSpanMilliseconds(subject, CSharpExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D506 RID: 54534 RVA: 0x002D4196 File Offset: 0x002D2396
		public static FormulaExpression TimeSpanMilliseconds(FormulaExpression subject, FormulaExpression amount)
		{
			return CSharpExpressionHelper.Func<DateTime>("TimeSpan.FromMilliseconds", new FormulaExpression[] { amount });
		}

		// Token: 0x0600D507 RID: 54535 RVA: 0x002D41AC File Offset: 0x002D23AC
		public static FormulaExpression TimeSpanMinutes(FormulaExpression subject, double amount)
		{
			return CSharpExpressionHelper.TimeSpanMinutes(subject, CSharpExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D508 RID: 54536 RVA: 0x002D41BA File Offset: 0x002D23BA
		public static FormulaExpression TimeSpanMinutes(FormulaExpression subject, FormulaExpression amount)
		{
			return CSharpExpressionHelper.Func<DateTime>("TimeSpan.FromMinutes", new FormulaExpression[] { amount });
		}

		// Token: 0x0600D509 RID: 54537 RVA: 0x002D41D0 File Offset: 0x002D23D0
		public static FormulaExpression TimeSpanMonths(FormulaExpression subject, double amount)
		{
			return CSharpExpressionHelper.TimeSpanMonths(subject, CSharpExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D50A RID: 54538 RVA: 0x002D41DE File Offset: 0x002D23DE
		public static FormulaExpression TimeSpanMonths(FormulaExpression subject, FormulaExpression amount)
		{
			return CSharpExpressionHelper.Func<DateTime>("TimeSpan.FromMonths", new FormulaExpression[] { amount });
		}

		// Token: 0x0600D50B RID: 54539 RVA: 0x002D41F4 File Offset: 0x002D23F4
		public static FormulaExpression TimeSpanSeconds(FormulaExpression subject, double amount)
		{
			return CSharpExpressionHelper.TimeSpanSeconds(subject, CSharpExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D50C RID: 54540 RVA: 0x002D4202 File Offset: 0x002D2402
		public static FormulaExpression TimeSpanSeconds(FormulaExpression subject, FormulaExpression amount)
		{
			return CSharpExpressionHelper.Func<DateTime>("TimeSpan.FromSeconds", new FormulaExpression[] { amount });
		}

		// Token: 0x0600D50D RID: 54541 RVA: 0x002D4218 File Offset: 0x002D2418
		public static FormulaExpression TimeSpanYears(FormulaExpression subject, double amount)
		{
			return CSharpExpressionHelper.TimeSpanYears(subject, CSharpExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D50E RID: 54542 RVA: 0x002D4226 File Offset: 0x002D2426
		public static FormulaExpression TimeSpanYears(FormulaExpression subject, FormulaExpression amount)
		{
			return CSharpExpressionHelper.Func<DateTime>("TimeSpan.FromYears", new FormulaExpression[] { amount });
		}

		// Token: 0x0600D50F RID: 54543 RVA: 0x002D423C File Offset: 0x002D243C
		public static CSharpClass Class(string name, IEnumerable<CSharpMethod> methods)
		{
			return CSharpExpressionHelper.Class(name, Enumerable.Empty<CSharpProperty>(), methods);
		}

		// Token: 0x0600D510 RID: 54544 RVA: 0x002D424A File Offset: 0x002D244A
		public static CSharpClass Class(string name, IEnumerable<CSharpProperty> properties, IEnumerable<CSharpMethod> methods)
		{
			return new CSharpClass(name, properties, methods);
		}

		// Token: 0x0600D511 RID: 54545 RVA: 0x002D4254 File Offset: 0x002D2454
		public static CSharpMethod Method(string name, Type returnType, IEnumerable<FormulaExpression> statements, string accessModifier = "public")
		{
			return new CSharpMethod(name, returnType, null, CSharpExpressionHelper.Block(statements), accessModifier);
		}

		// Token: 0x0600D512 RID: 54546 RVA: 0x002D4265 File Offset: 0x002D2465
		public static CSharpMethod Method(string name, Type returnType, IEnumerable<CSharpMethodParameter> parameters, IEnumerable<FormulaExpression> statements, string accessModifier = "public")
		{
			return new CSharpMethod(name, returnType, parameters, CSharpExpressionHelper.Block(statements), accessModifier);
		}

		// Token: 0x0600D513 RID: 54547 RVA: 0x002D4277 File Offset: 0x002D2477
		public static CSharpMethod Method(string name, Type returnType, IEnumerable<CSharpMethodParameter> parameters, CSharpBlock body, string accessModifier = "public")
		{
			return new CSharpMethod(name, returnType, parameters, body, accessModifier);
		}

		// Token: 0x0600D514 RID: 54548 RVA: 0x002D4284 File Offset: 0x002D2484
		public static CSharpMethodParameter MethodParameter(string name, Type type, bool nullable = false, FormulaExpression defaultValue = null, bool thisModifier = false)
		{
			return new CSharpMethodParameter(name, type, nullable, defaultValue, thisModifier);
		}

		// Token: 0x0600D515 RID: 54549 RVA: 0x002D4291 File Offset: 0x002D2491
		public static CSharpProgram Program(IEnumerable<CSharpUsing> usingStatements, CSharpNamespace namespaceBlock, IEnumerable<CSharpClass> classes, IEnumerable<CSharpMethod> methods, IEnumerable<FormulaExpression> statements)
		{
			return new CSharpProgram(usingStatements, namespaceBlock, classes, methods, statements);
		}
	}
}
