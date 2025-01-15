using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001369 RID: 4969
	public class Keyword
	{
		// Token: 0x060082CD RID: 33485 RVA: 0x001BB522 File Offset: 0x001B9722
		private Keyword(KeywordType8 type, string text)
		{
			this.type = type;
			this.segmentedText = SegmentedString.New(text);
		}

		// Token: 0x17002341 RID: 9025
		// (get) Token: 0x060082CE RID: 33486 RVA: 0x001BB53D File Offset: 0x001B973D
		public KeywordType8 Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17002342 RID: 9026
		// (get) Token: 0x060082CF RID: 33487 RVA: 0x001BB545 File Offset: 0x001B9745
		public SegmentedString SegmentedText
		{
			get
			{
				return this.segmentedText;
			}
		}

		// Token: 0x060082D0 RID: 33488 RVA: 0x001BB54D File Offset: 0x001B974D
		private static bool Compare(SegmentedString s, int offset, int length, SegmentedString text)
		{
			return length == text.Length && s.CompareOrdinal(offset, text, 0, length) == 0;
		}

		// Token: 0x060082D1 RID: 33489 RVA: 0x001BB568 File Offset: 0x001B9768
		public static ContextualKeyword? GetContextualKeyword(string text)
		{
			return Keyword.GetContextualKeyword(SegmentedString.New(text));
		}

		// Token: 0x060082D2 RID: 33490 RVA: 0x001BB578 File Offset: 0x001B9778
		public static ContextualKeyword? GetContextualKeyword(SegmentedString text)
		{
			int length = text.Length;
			if (length > 0)
			{
				char c = text[0];
				if (c != 'c')
				{
					if (c != 'n')
					{
						if (c == 'o')
						{
							if (Keyword.Compare(text, 0, length, Keyword.optionalKeyword))
							{
								return new ContextualKeyword?(ContextualKeyword.Optional);
							}
						}
					}
					else if (Keyword.Compare(text, 0, length, Keyword.nullableKeyword))
					{
						return new ContextualKeyword?(ContextualKeyword.Nullable);
					}
				}
				else if (Keyword.Compare(text, 0, length, Keyword.catchKeyword))
				{
					return new ContextualKeyword?(ContextualKeyword.Catch);
				}
			}
			return null;
		}

		// Token: 0x060082D3 RID: 33491 RVA: 0x001BB5F6 File Offset: 0x001B97F6
		public static Keyword GetKeyword8(string text)
		{
			return Keyword.GetKeyword8(text, 0, text.Length);
		}

		// Token: 0x060082D4 RID: 33492 RVA: 0x001BB605 File Offset: 0x001B9805
		public static Keyword GetKeyword8(string text, int offset, int length)
		{
			return Keyword.GetKeyword8(SegmentedString.New(text), offset, length);
		}

		// Token: 0x060082D5 RID: 33493 RVA: 0x001BB614 File Offset: 0x001B9814
		public static Keyword GetKeyword8(SegmentedString text, int offset, int length)
		{
			if (length > 0)
			{
				char c = text[offset];
				switch (c)
				{
				case 'a':
					if (Keyword.Compare(text, offset, length, Keyword.LogicalAnd.SegmentedText))
					{
						return Keyword.LogicalAnd;
					}
					if (Keyword.Compare(text, offset, length, Keyword.As.SegmentedText))
					{
						return Keyword.As;
					}
					break;
				case 'b':
				case 'c':
				case 'd':
				case 'g':
				case 'h':
				case 'j':
				case 'k':
					break;
				case 'e':
					if (Keyword.Compare(text, offset, length, Keyword.Each.SegmentedText))
					{
						return Keyword.Each;
					}
					if (Keyword.Compare(text, offset, length, Keyword.Else.SegmentedText))
					{
						return Keyword.Else;
					}
					if (Keyword.Compare(text, offset, length, Keyword.Error.SegmentedText))
					{
						return Keyword.Error;
					}
					break;
				case 'f':
					if (Keyword.Compare(text, offset, length, Keyword.False.SegmentedText))
					{
						return Keyword.False;
					}
					break;
				case 'i':
					if (Keyword.Compare(text, offset, length, Keyword.If.SegmentedText))
					{
						return Keyword.If;
					}
					if (Keyword.Compare(text, offset, length, Keyword.In.SegmentedText))
					{
						return Keyword.In;
					}
					if (Keyword.Compare(text, offset, length, Keyword.Is.SegmentedText))
					{
						return Keyword.Is;
					}
					break;
				case 'l':
					if (Keyword.Compare(text, offset, length, Keyword.Let.SegmentedText))
					{
						return Keyword.Let;
					}
					break;
				case 'm':
					if (Keyword.Compare(text, offset, length, Keyword.Meta.SegmentedText))
					{
						return Keyword.Meta;
					}
					break;
				case 'n':
					if (Keyword.Compare(text, offset, length, Keyword.Not.SegmentedText))
					{
						return Keyword.Not;
					}
					if (Keyword.Compare(text, offset, length, Keyword.Null.SegmentedText))
					{
						return Keyword.Null;
					}
					break;
				case 'o':
					if (Keyword.Compare(text, offset, length, Keyword.LogicalOr.SegmentedText))
					{
						return Keyword.LogicalOr;
					}
					if (Keyword.Compare(text, offset, length, Keyword.Otherwise.SegmentedText))
					{
						return Keyword.Otherwise;
					}
					break;
				default:
					if (c != 's')
					{
						if (c == 't')
						{
							if (Keyword.Compare(text, offset, length, Keyword.Then.SegmentedText))
							{
								return Keyword.Then;
							}
							if (Keyword.Compare(text, offset, length, Keyword.True.SegmentedText))
							{
								return Keyword.True;
							}
							if (Keyword.Compare(text, offset, length, Keyword.Try.SegmentedText))
							{
								return Keyword.Try;
							}
							if (Keyword.Compare(text, offset, length, Keyword._Type.SegmentedText))
							{
								return Keyword._Type;
							}
						}
					}
					else
					{
						if (Keyword.Compare(text, offset, length, Keyword.Section.SegmentedText))
						{
							return Keyword.Section;
						}
						if (Keyword.Compare(text, offset, length, Keyword.Shared.SegmentedText))
						{
							return Keyword.Shared;
						}
					}
					break;
				}
			}
			return null;
		}

		// Token: 0x04004701 RID: 18177
		public static readonly Keyword As = new Keyword(KeywordType8.As, "as");

		// Token: 0x04004702 RID: 18178
		public static readonly Keyword Each = new Keyword(KeywordType8.Each, "each");

		// Token: 0x04004703 RID: 18179
		public static readonly Keyword Else = new Keyword(KeywordType8.Else, "else");

		// Token: 0x04004704 RID: 18180
		public static readonly Keyword Error = new Keyword(KeywordType8.Error, "error");

		// Token: 0x04004705 RID: 18181
		public static readonly Keyword False = new Keyword(KeywordType8.False, "false");

		// Token: 0x04004706 RID: 18182
		public static readonly Keyword If = new Keyword(KeywordType8.If, "if");

		// Token: 0x04004707 RID: 18183
		public static readonly Keyword In = new Keyword(KeywordType8.In, "in");

		// Token: 0x04004708 RID: 18184
		public static readonly Keyword Is = new Keyword(KeywordType8.Is, "is");

		// Token: 0x04004709 RID: 18185
		public static readonly Keyword Let = new Keyword(KeywordType8.Let, "let");

		// Token: 0x0400470A RID: 18186
		public static readonly Keyword LogicalAnd = new Keyword(KeywordType8.LogicalAnd, "and");

		// Token: 0x0400470B RID: 18187
		public static readonly Keyword LogicalOr = new Keyword(KeywordType8.LogicalOr, "or");

		// Token: 0x0400470C RID: 18188
		public static readonly Keyword Meta = new Keyword(KeywordType8.Meta, "meta");

		// Token: 0x0400470D RID: 18189
		public static readonly Keyword Not = new Keyword(KeywordType8.Not, "not");

		// Token: 0x0400470E RID: 18190
		public static readonly Keyword Null = new Keyword(KeywordType8.Null, "null");

		// Token: 0x0400470F RID: 18191
		public static readonly Keyword Otherwise = new Keyword(KeywordType8.Otherwise, "otherwise");

		// Token: 0x04004710 RID: 18192
		public static readonly Keyword Section = new Keyword(KeywordType8.Section, "section");

		// Token: 0x04004711 RID: 18193
		public static readonly Keyword Sections = new Keyword(KeywordType8.Sections, "sections");

		// Token: 0x04004712 RID: 18194
		public static readonly Keyword Shared = new Keyword(KeywordType8.Shared, "shared");

		// Token: 0x04004713 RID: 18195
		public static readonly Keyword Then = new Keyword(KeywordType8.Then, "then");

		// Token: 0x04004714 RID: 18196
		public static readonly Keyword True = new Keyword(KeywordType8.True, "true");

		// Token: 0x04004715 RID: 18197
		public static readonly Keyword Try = new Keyword(KeywordType8.Try, "try");

		// Token: 0x04004716 RID: 18198
		public static readonly Keyword _Type = new Keyword(KeywordType8.Type, "type");

		// Token: 0x04004717 RID: 18199
		private static readonly SegmentedString catchKeyword = SegmentedString.New("catch");

		// Token: 0x04004718 RID: 18200
		private static readonly SegmentedString optionalKeyword = SegmentedString.New("optional");

		// Token: 0x04004719 RID: 18201
		private static readonly SegmentedString nullableKeyword = SegmentedString.New("nullable");

		// Token: 0x0400471A RID: 18202
		private KeywordType8 type;

		// Token: 0x0400471B RID: 18203
		private SegmentedString segmentedText;
	}
}
