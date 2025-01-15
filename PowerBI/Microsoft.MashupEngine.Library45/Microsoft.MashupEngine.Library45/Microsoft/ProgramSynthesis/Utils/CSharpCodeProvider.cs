using System;
using System.Linq;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000480 RID: 1152
	public class CSharpCodeProvider
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x0004E1C2 File Offset: 0x0004C3C2
		public static CSharpCodeProvider Instance { get; } = new CSharpCodeProvider();

		// Token: 0x06001A05 RID: 6661 RVA: 0x0004E1CC File Offset: 0x0004C3CC
		private string GetTypeArgumentsOutput(CodeTypeReferenceCollection typeArguments)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			this.GetTypeArgumentsOutput(typeArguments, 0, typeArguments.Count, stringBuilder, true);
			return stringBuilder.ToString();
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x0004E1FC File Offset: 0x0004C3FC
		private void GetTypeArgumentsOutput(CodeTypeReferenceCollection typeArguments, int start, int length, StringBuilder sb, bool includeNamespace = true)
		{
			sb.Append('<');
			bool flag = true;
			for (int i = start; i < start + length; i++)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					sb.Append(", ");
				}
				if (i < typeArguments.Count)
				{
					sb.Append(this.GetTypeOutput(typeArguments[i], includeNamespace));
				}
			}
			sb.Append('>');
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x0004E264 File Offset: 0x0004C464
		public string GetTypeOutput(CodeTypeReference typeRef, bool includeNamespace = true)
		{
			string text = string.Empty;
			CodeTypeReference codeTypeReference = typeRef;
			while (codeTypeReference.ArrayElementType != null)
			{
				codeTypeReference = codeTypeReference.ArrayElementType;
			}
			text += this.GetBaseTypeOutput(codeTypeReference, includeNamespace);
			while (typeRef != null && typeRef.ArrayRank > 0)
			{
				char[] array = new char[typeRef.ArrayRank + 1];
				array[0] = '[';
				array[typeRef.ArrayRank] = ']';
				for (int i = 1; i < typeRef.ArrayRank; i++)
				{
					array[i] = ',';
				}
				text += new string(array);
				typeRef = typeRef.ArrayElementType;
			}
			return text;
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x0004E2F0 File Offset: 0x0004C4F0
		private string GetBaseTypeOutput(CodeTypeReference typeRef, bool includeNamespace)
		{
			string text = typeRef.BaseType;
			if (text.Length == 0)
			{
				return "void";
			}
			string text2 = text.ToLowerInvariant().Trim();
			if (text2.StartsWith("system.nullable") && typeRef.TypeArguments.Count == 1)
			{
				return this.GetBaseTypeOutput(typeRef.TypeArguments[0], includeNamespace) + "?";
			}
			if (text2 != null)
			{
				switch (text2.Length)
				{
				case 11:
				{
					char c = text2[7];
					if (c != 'b')
					{
						if (c != 'c')
						{
							if (c != 'v')
							{
								goto IL_0352;
							}
							if (!(text2 == "system.void"))
							{
								goto IL_0352;
							}
							text = "void";
						}
						else
						{
							if (!(text2 == "system.char"))
							{
								goto IL_0352;
							}
							text = "char";
						}
					}
					else
					{
						if (!(text2 == "system.byte"))
						{
							goto IL_0352;
						}
						text = "byte";
					}
					break;
				}
				case 12:
				{
					char c = text2[10];
					if (c <= '3')
					{
						if (c != '1')
						{
							if (c != '3')
							{
								goto IL_0352;
							}
							if (!(text2 == "system.int32"))
							{
								goto IL_0352;
							}
							text = "int";
						}
						else
						{
							if (!(text2 == "system.int16"))
							{
								goto IL_0352;
							}
							text = "short";
						}
					}
					else if (c != '6')
					{
						if (c != 't')
						{
							goto IL_0352;
						}
						if (!(text2 == "system.sbyte"))
						{
							goto IL_0352;
						}
						text = "sbyte";
					}
					else
					{
						if (!(text2 == "system.int64"))
						{
							goto IL_0352;
						}
						text = "long";
					}
					break;
				}
				case 13:
				{
					char c = text2[11];
					if (c <= '6')
					{
						if (c != '1')
						{
							if (c != '3')
							{
								if (c != '6')
								{
									goto IL_0352;
								}
								if (!(text2 == "system.uint64"))
								{
									goto IL_0352;
								}
								text = "ulong";
							}
							else
							{
								if (!(text2 == "system.uint32"))
								{
									goto IL_0352;
								}
								text = "uint";
							}
						}
						else
						{
							if (!(text2 == "system.uint16"))
							{
								goto IL_0352;
							}
							text = "ushort";
						}
					}
					else if (c != 'c')
					{
						if (c != 'l')
						{
							if (c != 'n')
							{
								goto IL_0352;
							}
							if (!(text2 == "system.string"))
							{
								goto IL_0352;
							}
							text = "string";
						}
						else if (!(text2 == "system.single"))
						{
							if (!(text2 == "system.double"))
							{
								goto IL_0352;
							}
							text = "double";
						}
						else
						{
							text = "float";
						}
					}
					else
					{
						if (!(text2 == "system.object"))
						{
							goto IL_0352;
						}
						text = "object";
					}
					break;
				}
				case 14:
				{
					char c = text2[7];
					if (c != 'b')
					{
						if (c != 'd')
						{
							goto IL_0352;
						}
						if (!(text2 == "system.decimal"))
						{
							goto IL_0352;
						}
						text = "decimal";
					}
					else
					{
						if (!(text2 == "system.boolean"))
						{
							goto IL_0352;
						}
						text = "bool";
					}
					break;
				}
				default:
					goto IL_0352;
				}
				return text;
			}
			IL_0352:
			StringBuilder stringBuilder = new StringBuilder(text.Length + 10);
			if ((typeRef.Options & CodeTypeReferenceOptions.GlobalReference) != (CodeTypeReferenceOptions)0)
			{
				stringBuilder.Append("global::");
			}
			string text3 = (includeNamespace ? typeRef.BaseType : typeRef.BaseType.Split(new char[] { '.' }).Last<string>());
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < text3.Length; i++)
			{
				char c = text3[i];
				if (c != '+' && c != '.')
				{
					if (c == '`')
					{
						stringBuilder.Append(this.CreateEscapedIdentifier(text3.Substring(num, i - num)));
						i++;
						int num3 = 0;
						while (i < text3.Length && text3[i] >= '0' && text3[i] <= '9')
						{
							num3 = num3 * 10 + (int)(text3[i] - '0');
							i++;
						}
						this.GetTypeArgumentsOutput(typeRef.TypeArguments, num2, num3, stringBuilder, includeNamespace);
						num2 += num3;
						if (i < text3.Length && (text3[i] == '+' || text3[i] == '.'))
						{
							stringBuilder.Append('.');
							i++;
						}
						num = i;
					}
				}
				else
				{
					stringBuilder.Append(this.CreateEscapedIdentifier(text3.Substring(num, i - num)));
					stringBuilder.Append('.');
					i++;
					num = i;
				}
			}
			if (num < text3.Length)
			{
				stringBuilder.Append(this.CreateEscapedIdentifier(text3.Substring(num)));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0004E7E4 File Offset: 0x0004C9E4
		public string CreateEscapedIdentifier(string name)
		{
			if (CSharpCodeProvider.IsKeyword(name) || CSharpCodeProvider.IsPrefixTwoUnderscore(name))
			{
				return "@" + name;
			}
			return name;
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x0004E803 File Offset: 0x0004CA03
		private static bool IsKeyword(string value)
		{
			return FixedStringLookup.Contains(CSharpCodeProvider.keywords, value, false);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x0004E811 File Offset: 0x0004CA11
		private static bool IsPrefixTwoUnderscore(string value)
		{
			return value.Length >= 3 && (value[0] == '_' && value[1] == '_') && value[2] != '_';
		}

		// Token: 0x04000CDF RID: 3295
		private static readonly string[][] keywords = new string[][]
		{
			null,
			new string[] { "as", "by", "do", "if", "in", "is", "on", "or" },
			new string[]
			{
				"add", "and", "for", "get", "int", "let", "new", "not", "out", "ref",
				"set", "try", "var"
			},
			new string[]
			{
				"base", "bool", "byte", "case", "char", "else", "enum", "file", "from", "goto",
				"init", "into", "join", "lock", "long", "nint", "null", "this", "true", "uint",
				"void", "when", "with"
			},
			new string[]
			{
				"alias", "async", "await", "break", "catch", "class", "const", "event", "false", "fixed",
				"float", "group", "nuint", "sbyte", "short", "throw", "ulong", "using", "value", "where",
				"where", "while", "yield"
			},
			new string[]
			{
				"double", "equals", "extern", "global", "nameof", "object", "params", "public", "record", "remove",
				"return", "scoped", "sealed", "select", "sizeof", "static", "string", "struct", "switch", "typeof",
				"unsafe", "ushort"
			},
			new string[]
			{
				"checked", "decimal", "default", "dynamic", "finally", "foreach", "managed", "notnull", "orderby", "partial",
				"private", "virtual"
			},
			new string[]
			{
				"abstract", "continue", "delegate", "explicit", "implicit", "internal", "operator", "override", "readonly", "required",
				"volatile"
			},
			new string[] { "__arglist", "__makeref", "__reftype", "ascending", "interface", "namespace", "protected", "unchecked", "unmanaged" },
			new string[] { "__refvalue", "descending", "stackalloc" }
		};
	}
}
