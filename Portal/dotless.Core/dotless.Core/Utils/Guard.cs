using System;
using System.Collections.Generic;
using System.Linq;
using dotless.Core.Exceptions;
using dotless.Core.Parser;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Utils
{
	// Token: 0x0200000B RID: 11
	public static class Guard
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002B01 File Offset: 0x00000D01
		public static void Expect(string expected, string actual, object @in, NodeLocation location)
		{
			if (actual == expected)
			{
				return;
			}
			throw new ParsingException(string.Format("Expected '{0}' in {1}, found '{2}'", expected, @in, actual), location);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002B20 File Offset: 0x00000D20
		[Obsolete("Use Expect(bool, string, NodeLocation) instead")]
		public static void Expect(Func<bool> condition, string message, NodeLocation location)
		{
			if (condition())
			{
				return;
			}
			throw new ParsingException(message, location);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002B32 File Offset: 0x00000D32
		public static void Expect(bool condition, string message, NodeLocation location)
		{
			if (condition)
			{
				return;
			}
			throw new ParsingException(message, location);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002B40 File Offset: 0x00000D40
		public static TExpected ExpectNode<TExpected>(Node actual, object @in, NodeLocation location) where TExpected : Node
		{
			if (actual is TExpected)
			{
				return (TExpected)((object)actual);
			}
			string text = typeof(TExpected).Name.ToLowerInvariant();
			throw new ParsingException(string.Format("Expected {0} in {1}, found {2}", text, @in, actual.ToCSS(new Env(null))), location);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002B90 File Offset: 0x00000D90
		public static void ExpectNodeToBeOneOf<TExpected1, TExpected2>(Node actual, object @in, NodeLocation location) where TExpected1 : Node where TExpected2 : Node
		{
			if (actual is TExpected1 || actual is TExpected2)
			{
				return;
			}
			string text = typeof(TExpected1).Name.ToLowerInvariant();
			string text2 = typeof(TExpected2).Name.ToLowerInvariant();
			throw new ParsingException(string.Format("Expected {0} or {1} in {2}, found {3}", new object[]
			{
				text,
				text2,
				@in,
				actual.ToCSS(new Env(null))
			}), location);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002C0C File Offset: 0x00000E0C
		public static List<TExpected> ExpectAllNodes<TExpected>(IEnumerable<Node> actual, object @in, NodeLocation location) where TExpected : Node
		{
			return actual.Select((Node node) => Guard.ExpectNode<TExpected>(node, @in, location)).ToList<TExpected>();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002C44 File Offset: 0x00000E44
		public static void ExpectNumArguments(int expected, int actual, object @in, NodeLocation location)
		{
			if (actual == expected)
			{
				return;
			}
			throw new ParsingException(string.Format("Expected {0} arguments in {1}, found {2}", expected, @in, actual), location);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002C68 File Offset: 0x00000E68
		public static void ExpectMinArguments(int expected, int actual, object @in, NodeLocation location)
		{
			if (actual >= expected)
			{
				return;
			}
			throw new ParsingException(string.Format("Expected at least {0} arguments in {1}, found {2}", expected, @in, actual), location);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002C8C File Offset: 0x00000E8C
		public static void ExpectMaxArguments(int expected, int actual, object @in, NodeLocation location)
		{
			if (actual <= expected)
			{
				return;
			}
			throw new ParsingException(string.Format("Expected at most {0} arguments in {1}, found {2}", expected, @in, actual), location);
		}
	}
}
