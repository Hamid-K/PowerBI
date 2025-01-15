using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.ProgramSynthesis.Translation.Python
{
	// Token: 0x02000312 RID: 786
	public static class PythonNameUtils
	{
		// Token: 0x06001163 RID: 4451 RVA: 0x000324B4 File Offset: 0x000306B4
		public static string ConvertStringToSnakeCase(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}
			string text = char.ToLowerInvariant(s[0]).ToString() + s.Substring(1);
			return PythonNameUtils.SnakeCaseConverterRegex.Replace(text, delegate(Match m)
			{
				if (m.Groups[1].Length <= 0)
				{
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}", new object[] { m.Groups[2].Value.ToLowerInvariant() }));
				}
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}_{1}", new object[]
				{
					m.Groups[1].Value,
					m.Groups[2].Value.ToLowerInvariant()
				}));
			});
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00032516 File Offset: 0x00030716
		public static bool IsValidIdentifier(string id, out string normalized)
		{
			normalized = id.Normalize(NormalizationForm.FormKC);
			return PythonNameUtils.ValidPythonIdentifier.IsMatch(normalized) && !PythonNameUtils.ReservedPythonIdentifiers.Contains(normalized);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00032540 File Offset: 0x00030740
		public static string NearestValidIdentifier(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				id = "var";
			}
			string text = id.Normalize(NormalizationForm.FormKC);
			if (!PythonNameUtils.ValidPythonIdentifier.IsMatch(text))
			{
				text = PythonNameUtils.ValidPythonIdentifier_NotContinueCharacter.Replace(text, "_");
				if (!PythonNameUtils.ValidPythonIdentifier_InitialCharacter.IsMatch(text.Substring(0, 1)))
				{
					text = "_" + text;
				}
			}
			while (PythonNameUtils.ReservedPythonIdentifiers.Contains(text) || (text.StartsWith("__") && text.EndsWith("__")))
			{
				if (text.Length > 2 && text.StartsWith("__"))
				{
					text = text.Substring(1);
				}
				else if (text == "__")
				{
					text = "var__";
				}
				else
				{
					text += "_";
				}
			}
			return text;
		}

		// Token: 0x04000853 RID: 2131
		private const string IdentifierInitialCharacterClass = "_\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\u1885\\u1886\\u2118\\u212e\\u309b\\u309c";

		// Token: 0x04000854 RID: 2132
		private const string IdentifierContinueCharacterClass = "_\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\u1885\\u1886\\u2118\\u212e\\u309b\\u309c\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\u00b7\\u0387\\u1369-\\u1371\\u19da";

		// Token: 0x04000855 RID: 2133
		private static readonly Regex SnakeCaseConverterRegex = new Regex("(\\p{Ll}?)(\\p{Lu})", RegexOptions.Compiled);

		// Token: 0x04000856 RID: 2134
		private static readonly Regex ValidPythonIdentifier_InitialCharacter = new Regex("[_\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\u1885\\u1886\\u2118\\u212e\\u309b\\u309c]", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04000857 RID: 2135
		private static readonly Regex ValidPythonIdentifier_NotContinueCharacter = new Regex("[^_\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\u1885\\u1886\\u2118\\u212e\\u309b\\u309c\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\u00b7\\u0387\\u1369-\\u1371\\u19da]", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04000858 RID: 2136
		private static readonly Regex ValidPythonIdentifier = new Regex("^[_\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\u1885\\u1886\\u2118\\u212e\\u309b\\u309c][_\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\u1885\\u1886\\u2118\\u212e\\u309b\\u309c\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\u00b7\\u0387\\u1369-\\u1371\\u19da]*$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04000859 RID: 2137
		private static readonly HashSet<string> ReservedPythonIdentifiers = new HashSet<string>
		{
			"and", "as", "assert", "break", "class", "continue", "def", "del", "elif", "else",
			"except", "exec", "finally", "for", "from", "global", "if", "import", "in", "is",
			"lambda", "not", "or", "pass", "print", "raise", "return", "try", "while", "with",
			"yield", "ArithmeticError", "AssertionError", "AttributeError", "BaseException", "BlockingIOError", "BrokenPipeError", "BufferError", "BytesWarning", "ChildProcessError",
			"ConnectionAbortedError", "ConnectionError", "ConnectionRefusedError", "ConnectionResetError", "DeprecationWarning", "EOFError", "Ellipsis", "EnvironmentError", "Exception", "False",
			"FileExistsError", "FileNotFoundError", "FloatingPointError", "FutureWarning", "GeneratorExit", "IOError", "ImportError", "ImportWarning", "IndentationError", "IndexError",
			"InterruptedError", "IsADirectoryError", "KeyError", "KeyboardInterrupt", "LookupError", "MemoryError", "NameError", "None", "NotADirectoryError", "NotImplemented",
			"NotImplementedError", "OSError", "OverflowError", "PendingDeprecationWarning", "PermissionError", "ProcessLookupError", "ReferenceError", "ResourceWarning", "RuntimeError", "RuntimeWarning",
			"StopIteration", "SyntaxError", "SyntaxWarning", "SystemError", "SystemExit", "TabError", "TimeoutError", "True", "TypeError", "UnboundLocalError",
			"UnicodeDecodeError", "UnicodeEncodeError", "UnicodeError", "UnicodeTranslateError", "UnicodeWarning", "UserWarning", "ValueError", "Warning", "ZeroDivisionError", "_",
			"__build_class__", "__debug__", "__doc__", "__import__", "__name__", "__package__", "abs", "all", "any", "ascii",
			"bin", "bool", "bytearray", "bytes", "callable", "chr", "classmethod", "compile", "complex", "copyright",
			"credits", "delattr", "dict", "dir", "divmod", "enumerate", "eval", "exec", "exit", "filter",
			"float", "format", "frozenset", "getattr", "globals", "hasattr", "hash", "help", "hex", "id",
			"input", "int", "isinstance", "issubclass", "iter", "len", "license", "list", "locals", "map",
			"max", "memoryview", "min", "next", "object", "oct", "open", "ord", "pow", "print",
			"property", "quit", "range", "repr", "reversed", "round", "set", "setattr", "slice", "sorted",
			"staticmethod", "str", "sum", "super", "tuple", "type", "vars", "zip"
		};

		// Token: 0x02000313 RID: 787
		public static class Operators
		{
			// Token: 0x06001167 RID: 4455 RVA: 0x00032EC4 File Offset: 0x000310C4
			// Note: this type is marked as 'beforefieldinit'.
			static Operators()
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				dictionary["operators.__assign__"] = 5;
				dictionary["operators.__ite__"] = 10;
				dictionary["operators.__or__"] = 20;
				dictionary["operators.__and__"] = 30;
				dictionary["operators.__equals__"] = 50;
				dictionary["operators.__not_equals__"] = 50;
				dictionary["operators.__lte__"] = 50;
				dictionary["operators.__notin__"] = 50;
				dictionary["operators.__for_in_if__"] = 50;
				dictionary["operators.__add__"] = 100;
				dictionary["operators.__minus__"] = 100;
				dictionary["operators.__times__"] = 110;
				dictionary["operators.__divideby__"] = 110;
				dictionary["operators.__intdivideby__"] = 110;
				dictionary["operators.__mod__"] = 110;
				dictionary["operators.__dot__"] = 140;
				dictionary["operators.__getitem_slice2__"] = 150;
				dictionary["operators.__getitem_slice_start_only__"] = 150;
				dictionary["operators.__getitem_slice_end_only__"] = 150;
				dictionary["operators.__getitem__"] = 150;
				dictionary["operators.__make_list__"] = 160;
				dictionary["operators.__tuple__"] = 160;
				PythonNameUtils.Operators.OpPrec = dictionary;
				PythonNameUtils.Operators.AcLikeOps = new HashSet<string> { "operators.__or__", "operators.__and__", "operators.__add__", "operators.__dot__", "operators.__getitem_slice2__", "operators.__getitem_slice_start_only__", "operators.__getitem_slice_end_only__", "operators.__getitem__", "operators.__make_list__", "operators.__tuple__" };
			}

			// Token: 0x0400085A RID: 2138
			public const string Prefix = "operators.";

			// Token: 0x0400085B RID: 2139
			public const string LessThanOrEqualTo = "operators.__lte__";

			// Token: 0x0400085C RID: 2140
			public const string Add = "operators.__add__";

			// Token: 0x0400085D RID: 2141
			public const string And = "operators.__and__";

			// Token: 0x0400085E RID: 2142
			public const string Or = "operators.__or__";

			// Token: 0x0400085F RID: 2143
			public const string IfThenElse = "operators.__ite__";

			// Token: 0x04000860 RID: 2144
			public const string GetItem = "operators.__getitem__";

			// Token: 0x04000861 RID: 2145
			public const string Slice = "operators.__getitem_slice2__";

			// Token: 0x04000862 RID: 2146
			public const string SliceStartOnly = "operators.__getitem_slice_start_only__";

			// Token: 0x04000863 RID: 2147
			public const string SliceEndOnly = "operators.__getitem_slice_end_only__";

			// Token: 0x04000864 RID: 2148
			public const string MakeList = "operators.__make_list__";

			// Token: 0x04000865 RID: 2149
			public const string Dot = "operators.__dot__";

			// Token: 0x04000866 RID: 2150
			public const string ForInIf = "operators.__for_in_if__";

			// Token: 0x04000867 RID: 2151
			public const string Tuple = "operators.__tuple__";

			// Token: 0x04000868 RID: 2152
			public const string EqualTo = "operators.__equals__";

			// Token: 0x04000869 RID: 2153
			public const string NotEquals = "operators.__not_equals__";

			// Token: 0x0400086A RID: 2154
			public const string Assign = "operators.__assign__";

			// Token: 0x0400086B RID: 2155
			public const string Minus = "operators.__minus__";

			// Token: 0x0400086C RID: 2156
			public const string Times = "operators.__times__";

			// Token: 0x0400086D RID: 2157
			public const string DivideBy = "operators.__divideby__";

			// Token: 0x0400086E RID: 2158
			public const string IntDivideBy = "operators.__intdivideby__";

			// Token: 0x0400086F RID: 2159
			public const string NotIn = "operators.__notin__";

			// Token: 0x04000870 RID: 2160
			public const string Mod = "operators.__mod__";

			// Token: 0x04000871 RID: 2161
			public const int MaxPrecValue = 200;

			// Token: 0x04000872 RID: 2162
			public static Dictionary<string, int> OpPrec;

			// Token: 0x04000873 RID: 2163
			public static HashSet<string> AcLikeOps;
		}
	}
}
