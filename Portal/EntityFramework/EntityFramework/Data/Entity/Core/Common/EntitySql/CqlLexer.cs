using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql.AST;
using System.Data.Entity.Resources;
using System.IO;
using System.Text.RegularExpressions;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000646 RID: 1606
	internal sealed class CqlLexer
	{
		// Token: 0x06004D29 RID: 19753 RVA: 0x00110D6A File Offset: 0x0010EF6A
		internal CqlLexer(TextReader reader)
			: this()
		{
			if (reader == null)
			{
				throw new EntitySqlException(EntityRes.GetString("ParserInputError"));
			}
			this.yy_reader = reader;
		}

		// Token: 0x06004D2A RID: 19754 RVA: 0x00110D8C File Offset: 0x0010EF8C
		internal CqlLexer(FileStream instream)
			: this()
		{
			if (instream == null)
			{
				throw new EntitySqlException(EntityRes.GetString("ParserInputError"));
			}
			this.yy_reader = new StreamReader(instream);
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x00110DB4 File Offset: 0x0010EFB4
		private CqlLexer()
		{
			this.yy_buffer = new char[512];
			this.yy_buffer_read = 0;
			this.yy_buffer_index = 0;
			this.yy_buffer_start = 0;
			this.yy_buffer_end = 0;
			this.yychar = 0;
			this.yyline = 0;
			this.yy_at_bol = true;
			this.yy_lexical_state = 0;
			this.accept_dispatch = new CqlLexer.AcceptMethod[]
			{
				null,
				null,
				new CqlLexer.AcceptMethod(this.Accept_2),
				new CqlLexer.AcceptMethod(this.Accept_3),
				new CqlLexer.AcceptMethod(this.Accept_4),
				new CqlLexer.AcceptMethod(this.Accept_5),
				new CqlLexer.AcceptMethod(this.Accept_6),
				new CqlLexer.AcceptMethod(this.Accept_7),
				new CqlLexer.AcceptMethod(this.Accept_8),
				new CqlLexer.AcceptMethod(this.Accept_9),
				new CqlLexer.AcceptMethod(this.Accept_10),
				new CqlLexer.AcceptMethod(this.Accept_11),
				new CqlLexer.AcceptMethod(this.Accept_12),
				new CqlLexer.AcceptMethod(this.Accept_13),
				new CqlLexer.AcceptMethod(this.Accept_14),
				new CqlLexer.AcceptMethod(this.Accept_15),
				new CqlLexer.AcceptMethod(this.Accept_16),
				new CqlLexer.AcceptMethod(this.Accept_17),
				new CqlLexer.AcceptMethod(this.Accept_18),
				null,
				new CqlLexer.AcceptMethod(this.Accept_20),
				new CqlLexer.AcceptMethod(this.Accept_21),
				new CqlLexer.AcceptMethod(this.Accept_22),
				new CqlLexer.AcceptMethod(this.Accept_23),
				null,
				new CqlLexer.AcceptMethod(this.Accept_25),
				new CqlLexer.AcceptMethod(this.Accept_26),
				new CqlLexer.AcceptMethod(this.Accept_27),
				new CqlLexer.AcceptMethod(this.Accept_28),
				null,
				new CqlLexer.AcceptMethod(this.Accept_30),
				new CqlLexer.AcceptMethod(this.Accept_31),
				new CqlLexer.AcceptMethod(this.Accept_32),
				null,
				new CqlLexer.AcceptMethod(this.Accept_34),
				new CqlLexer.AcceptMethod(this.Accept_35),
				null,
				new CqlLexer.AcceptMethod(this.Accept_37),
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				new CqlLexer.AcceptMethod(this.Accept_53),
				new CqlLexer.AcceptMethod(this.Accept_54),
				new CqlLexer.AcceptMethod(this.Accept_55),
				new CqlLexer.AcceptMethod(this.Accept_56),
				new CqlLexer.AcceptMethod(this.Accept_57),
				new CqlLexer.AcceptMethod(this.Accept_58),
				new CqlLexer.AcceptMethod(this.Accept_59),
				new CqlLexer.AcceptMethod(this.Accept_60),
				new CqlLexer.AcceptMethod(this.Accept_61),
				new CqlLexer.AcceptMethod(this.Accept_62),
				new CqlLexer.AcceptMethod(this.Accept_63),
				new CqlLexer.AcceptMethod(this.Accept_64),
				new CqlLexer.AcceptMethod(this.Accept_65),
				new CqlLexer.AcceptMethod(this.Accept_66),
				new CqlLexer.AcceptMethod(this.Accept_67),
				new CqlLexer.AcceptMethod(this.Accept_68),
				new CqlLexer.AcceptMethod(this.Accept_69),
				new CqlLexer.AcceptMethod(this.Accept_70),
				new CqlLexer.AcceptMethod(this.Accept_71),
				new CqlLexer.AcceptMethod(this.Accept_72),
				new CqlLexer.AcceptMethod(this.Accept_73),
				new CqlLexer.AcceptMethod(this.Accept_74),
				new CqlLexer.AcceptMethod(this.Accept_75),
				new CqlLexer.AcceptMethod(this.Accept_76),
				new CqlLexer.AcceptMethod(this.Accept_77),
				new CqlLexer.AcceptMethod(this.Accept_78),
				new CqlLexer.AcceptMethod(this.Accept_79),
				new CqlLexer.AcceptMethod(this.Accept_80),
				new CqlLexer.AcceptMethod(this.Accept_81),
				new CqlLexer.AcceptMethod(this.Accept_82),
				new CqlLexer.AcceptMethod(this.Accept_83),
				new CqlLexer.AcceptMethod(this.Accept_84)
			};
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x00111205 File Offset: 0x0010F405
		private CqlLexer.Token Accept_2()
		{
			return this.HandleEscapedIdentifiers();
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x0011120D File Offset: 0x0010F40D
		private CqlLexer.Token Accept_3()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x0011121B File Offset: 0x0010F41B
		private CqlLexer.Token Accept_4()
		{
			this.AdvanceIPos();
			this.ResetSymbolAsIdentifierState(false);
			return null;
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x0011122C File Offset: 0x0010F42C
		private CqlLexer.Token Accept_5()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x0011123B File Offset: 0x0010F43B
		private CqlLexer.Token Accept_6()
		{
			return this.MapPunctuator(this.YYText);
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x00111249 File Offset: 0x0010F449
		private CqlLexer.Token Accept_7()
		{
			return this.MapOperator(this.YYText);
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x00111257 File Offset: 0x0010F457
		private CqlLexer.Token Accept_8()
		{
			this._lineNumber++;
			this.AdvanceIPos();
			this.ResetSymbolAsIdentifierState(false);
			return null;
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x00111276 File Offset: 0x0010F476
		private CqlLexer.Token Accept_9()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.String);
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x00111285 File Offset: 0x0010F485
		private CqlLexer.Token Accept_10()
		{
			return this.MapDoubleQuotedString(this.YYText);
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x00111293 File Offset: 0x0010F493
		private CqlLexer.Token Accept_11()
		{
			return this.NewParameterToken(this.YYText);
		}

		// Token: 0x06004D36 RID: 19766 RVA: 0x001112A1 File Offset: 0x0010F4A1
		private CqlLexer.Token Accept_12()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Binary);
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x001112B0 File Offset: 0x0010F4B0
		private CqlLexer.Token Accept_13()
		{
			this._lineNumber++;
			this.AdvanceIPos();
			this.ResetSymbolAsIdentifierState(false);
			return null;
		}

		// Token: 0x06004D38 RID: 19768 RVA: 0x001112CF File Offset: 0x0010F4CF
		private CqlLexer.Token Accept_14()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Boolean);
		}

		// Token: 0x06004D39 RID: 19769 RVA: 0x001112DE File Offset: 0x0010F4DE
		private CqlLexer.Token Accept_15()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Time);
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x001112ED File Offset: 0x0010F4ED
		private CqlLexer.Token Accept_16()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Guid);
		}

		// Token: 0x06004D3B RID: 19771 RVA: 0x001112FC File Offset: 0x0010F4FC
		private CqlLexer.Token Accept_17()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.DateTime);
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x0011130B File Offset: 0x0010F50B
		private CqlLexer.Token Accept_18()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.DateTimeOffset);
		}

		// Token: 0x06004D3D RID: 19773 RVA: 0x0011131A File Offset: 0x0010F51A
		private CqlLexer.Token Accept_20()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D3E RID: 19774 RVA: 0x00111328 File Offset: 0x0010F528
		private CqlLexer.Token Accept_21()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x00111337 File Offset: 0x0010F537
		private CqlLexer.Token Accept_22()
		{
			return this.MapPunctuator(this.YYText);
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x00111345 File Offset: 0x0010F545
		private CqlLexer.Token Accept_23()
		{
			return this.MapOperator(this.YYText);
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x00111353 File Offset: 0x0010F553
		private CqlLexer.Token Accept_25()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x00111361 File Offset: 0x0010F561
		private CqlLexer.Token Accept_26()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x00111370 File Offset: 0x0010F570
		private CqlLexer.Token Accept_27()
		{
			return this.MapPunctuator(this.YYText);
		}

		// Token: 0x06004D44 RID: 19780 RVA: 0x0011137E File Offset: 0x0010F57E
		private CqlLexer.Token Accept_28()
		{
			return this.MapOperator(this.YYText);
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x0011138C File Offset: 0x0010F58C
		private CqlLexer.Token Accept_30()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x0011139A File Offset: 0x0010F59A
		private CqlLexer.Token Accept_31()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x001113A9 File Offset: 0x0010F5A9
		private CqlLexer.Token Accept_32()
		{
			return this.MapOperator(this.YYText);
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x001113B7 File Offset: 0x0010F5B7
		private CqlLexer.Token Accept_34()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x001113C5 File Offset: 0x0010F5C5
		private CqlLexer.Token Accept_35()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x06004D4A RID: 19786 RVA: 0x001113D4 File Offset: 0x0010F5D4
		private CqlLexer.Token Accept_37()
		{
			return this.NewLiteralToken(this.YYText, LiteralKind.Number);
		}

		// Token: 0x06004D4B RID: 19787 RVA: 0x001113E3 File Offset: 0x0010F5E3
		private CqlLexer.Token Accept_53()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D4C RID: 19788 RVA: 0x001113F1 File Offset: 0x0010F5F1
		private CqlLexer.Token Accept_54()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D4D RID: 19789 RVA: 0x001113FF File Offset: 0x0010F5FF
		private CqlLexer.Token Accept_55()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x0011140D File Offset: 0x0010F60D
		private CqlLexer.Token Accept_56()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x0011141B File Offset: 0x0010F61B
		private CqlLexer.Token Accept_57()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x00111429 File Offset: 0x0010F629
		private CqlLexer.Token Accept_58()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x00111437 File Offset: 0x0010F637
		private CqlLexer.Token Accept_59()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x00111445 File Offset: 0x0010F645
		private CqlLexer.Token Accept_60()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D53 RID: 19795 RVA: 0x00111453 File Offset: 0x0010F653
		private CqlLexer.Token Accept_61()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x00111461 File Offset: 0x0010F661
		private CqlLexer.Token Accept_62()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D55 RID: 19797 RVA: 0x0011146F File Offset: 0x0010F66F
		private CqlLexer.Token Accept_63()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x0011147D File Offset: 0x0010F67D
		private CqlLexer.Token Accept_64()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D57 RID: 19799 RVA: 0x0011148B File Offset: 0x0010F68B
		private CqlLexer.Token Accept_65()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x00111499 File Offset: 0x0010F699
		private CqlLexer.Token Accept_66()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x001114A7 File Offset: 0x0010F6A7
		private CqlLexer.Token Accept_67()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x001114B5 File Offset: 0x0010F6B5
		private CqlLexer.Token Accept_68()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x001114C3 File Offset: 0x0010F6C3
		private CqlLexer.Token Accept_69()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D5C RID: 19804 RVA: 0x001114D1 File Offset: 0x0010F6D1
		private CqlLexer.Token Accept_70()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x001114DF File Offset: 0x0010F6DF
		private CqlLexer.Token Accept_71()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x001114ED File Offset: 0x0010F6ED
		private CqlLexer.Token Accept_72()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D5F RID: 19807 RVA: 0x001114FB File Offset: 0x0010F6FB
		private CqlLexer.Token Accept_73()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D60 RID: 19808 RVA: 0x00111509 File Offset: 0x0010F709
		private CqlLexer.Token Accept_74()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D61 RID: 19809 RVA: 0x00111517 File Offset: 0x0010F717
		private CqlLexer.Token Accept_75()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D62 RID: 19810 RVA: 0x00111525 File Offset: 0x0010F725
		private CqlLexer.Token Accept_76()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D63 RID: 19811 RVA: 0x00111533 File Offset: 0x0010F733
		private CqlLexer.Token Accept_77()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D64 RID: 19812 RVA: 0x00111541 File Offset: 0x0010F741
		private CqlLexer.Token Accept_78()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D65 RID: 19813 RVA: 0x0011154F File Offset: 0x0010F74F
		private CqlLexer.Token Accept_79()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D66 RID: 19814 RVA: 0x0011155D File Offset: 0x0010F75D
		private CqlLexer.Token Accept_80()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D67 RID: 19815 RVA: 0x0011156B File Offset: 0x0010F76B
		private CqlLexer.Token Accept_81()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D68 RID: 19816 RVA: 0x00111579 File Offset: 0x0010F779
		private CqlLexer.Token Accept_82()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D69 RID: 19817 RVA: 0x00111587 File Offset: 0x0010F787
		private CqlLexer.Token Accept_83()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D6A RID: 19818 RVA: 0x00111595 File Offset: 0x0010F795
		private CqlLexer.Token Accept_84()
		{
			return this.MapIdentifierOrKeyword(this.YYText);
		}

		// Token: 0x06004D6B RID: 19819 RVA: 0x001115A3 File Offset: 0x0010F7A3
		private void yybegin(int state)
		{
			this.yy_lexical_state = state;
		}

		// Token: 0x06004D6C RID: 19820 RVA: 0x001115AC File Offset: 0x0010F7AC
		private char yy_advance()
		{
			int num;
			if (this.yy_buffer_index < this.yy_buffer_read)
			{
				char[] array = this.yy_buffer;
				num = this.yy_buffer_index;
				this.yy_buffer_index = num + 1;
				return CqlLexer.yy_translate.translate(array[num]);
			}
			if (this.yy_buffer_start != 0)
			{
				int i = this.yy_buffer_start;
				int num2 = 0;
				while (i < this.yy_buffer_read)
				{
					this.yy_buffer[num2] = this.yy_buffer[i];
					i++;
					num2++;
				}
				this.yy_buffer_end -= this.yy_buffer_start;
				this.yy_buffer_start = 0;
				this.yy_buffer_read = num2;
				this.yy_buffer_index = num2;
				int num3 = this.yy_reader.Read(this.yy_buffer, this.yy_buffer_read, this.yy_buffer.Length - this.yy_buffer_read);
				if (num3 <= 0)
				{
					return '\u0081';
				}
				this.yy_buffer_read += num3;
			}
			while (this.yy_buffer_index >= this.yy_buffer_read)
			{
				if (this.yy_buffer_index >= this.yy_buffer.Length)
				{
					this.yy_buffer = this.yy_double(this.yy_buffer);
				}
				int num3 = this.yy_reader.Read(this.yy_buffer, this.yy_buffer_read, this.yy_buffer.Length - this.yy_buffer_read);
				if (num3 <= 0)
				{
					return '\u0081';
				}
				this.yy_buffer_read += num3;
			}
			char[] array2 = this.yy_buffer;
			num = this.yy_buffer_index;
			this.yy_buffer_index = num + 1;
			return CqlLexer.yy_translate.translate(array2[num]);
		}

		// Token: 0x06004D6D RID: 19821 RVA: 0x00111714 File Offset: 0x0010F914
		private void yy_move_end()
		{
			if (this.yy_buffer_end > this.yy_buffer_start && '\n' == this.yy_buffer[this.yy_buffer_end - 1])
			{
				this.yy_buffer_end--;
			}
			if (this.yy_buffer_end > this.yy_buffer_start && '\r' == this.yy_buffer[this.yy_buffer_end - 1])
			{
				this.yy_buffer_end--;
			}
		}

		// Token: 0x06004D6E RID: 19822 RVA: 0x00111780 File Offset: 0x0010F980
		private void yy_mark_start()
		{
			for (int i = this.yy_buffer_start; i < this.yy_buffer_index; i++)
			{
				if (this.yy_buffer[i] == '\n' && !this.yy_last_was_cr)
				{
					this.yyline++;
				}
				if (this.yy_buffer[i] == '\r')
				{
					this.yyline++;
					this.yy_last_was_cr = true;
				}
				else
				{
					this.yy_last_was_cr = false;
				}
			}
			this.yychar = this.yychar + this.yy_buffer_index - this.yy_buffer_start;
			this.yy_buffer_start = this.yy_buffer_index;
		}

		// Token: 0x06004D6F RID: 19823 RVA: 0x00111815 File Offset: 0x0010FA15
		private void yy_mark_end()
		{
			this.yy_buffer_end = this.yy_buffer_index;
		}

		// Token: 0x06004D70 RID: 19824 RVA: 0x00111824 File Offset: 0x0010FA24
		private void yy_to_mark()
		{
			this.yy_buffer_index = this.yy_buffer_end;
			this.yy_at_bol = this.yy_buffer_end > this.yy_buffer_start && (this.yy_buffer[this.yy_buffer_end - 1] == '\r' || this.yy_buffer[this.yy_buffer_end - 1] == '\n');
		}

		// Token: 0x06004D71 RID: 19825 RVA: 0x0011187D File Offset: 0x0010FA7D
		internal string yytext()
		{
			return new string(this.yy_buffer, this.yy_buffer_start, this.yy_buffer_end - this.yy_buffer_start);
		}

		// Token: 0x06004D72 RID: 19826 RVA: 0x0011189D File Offset: 0x0010FA9D
		internal int yy_char()
		{
			return this.yychar;
		}

		// Token: 0x06004D73 RID: 19827 RVA: 0x001118A5 File Offset: 0x0010FAA5
		private int yylength()
		{
			return this.yy_buffer_end - this.yy_buffer_start;
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x001118B4 File Offset: 0x0010FAB4
		private char[] yy_double(char[] buf)
		{
			char[] array = new char[2 * buf.Length];
			for (int i = 0; i < buf.Length; i++)
			{
				array[i] = buf[i];
			}
			return array;
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x001118E1 File Offset: 0x0010FAE1
		private void yy_error(int code, bool fatal)
		{
			if (fatal)
			{
				throw new EntitySqlException(EntityRes.GetString("ParserFatalError"));
			}
		}

		// Token: 0x06004D76 RID: 19830 RVA: 0x001118F8 File Offset: 0x0010FAF8
		internal CqlLexer.Token yylex()
		{
			int num = CqlLexer.yy_state_dtrans[this.yy_lexical_state];
			int num2 = -1;
			bool flag = true;
			this.yy_mark_start();
			int num3 = CqlLexer.yy_acpt[num];
			if (num3 != 0)
			{
				num2 = num;
				this.yy_mark_end();
			}
			for (;;)
			{
				char c;
				if (flag && this.yy_at_bol)
				{
					c = '\u0080';
				}
				else
				{
					c = this.yy_advance();
				}
				int num4 = CqlLexer.yy_nxt[CqlLexer.yy_rmap[num], CqlLexer.yy_cmap[(int)c]];
				if ('\u0081' == c && flag)
				{
					break;
				}
				if (-1 != num4)
				{
					num = num4;
					flag = false;
					num3 = CqlLexer.yy_acpt[num];
					if (num3 != 0)
					{
						num2 = num;
						this.yy_mark_end();
					}
				}
				else
				{
					if (-1 == num2)
					{
						goto Block_7;
					}
					int num5 = CqlLexer.yy_acpt[num2];
					if ((2 & num5) != 0)
					{
						this.yy_move_end();
					}
					this.yy_to_mark();
					if (num2 < 0)
					{
						if (num2 < 85)
						{
							this.yy_error(0, false);
						}
					}
					else
					{
						CqlLexer.AcceptMethod acceptMethod = this.accept_dispatch[num2];
						if (acceptMethod != null)
						{
							CqlLexer.Token token = acceptMethod();
							if (token != null)
							{
								return token;
							}
						}
					}
					flag = true;
					num = CqlLexer.yy_state_dtrans[this.yy_lexical_state];
					num2 = -1;
					this.yy_mark_start();
					num3 = CqlLexer.yy_acpt[num];
					if (num3 != 0)
					{
						num2 = num;
						this.yy_mark_end();
					}
				}
			}
			return null;
			Block_7:
			throw new EntitySqlException(EntitySqlException.GetGenericErrorMessage(this._query, this.yychar));
		}

		// Token: 0x06004D77 RID: 19831 RVA: 0x00111A3E File Offset: 0x0010FC3E
		internal CqlLexer(string query, ParserOptions parserOptions)
			: this()
		{
			this._query = query;
			this._parserOptions = parserOptions;
			this.yy_reader = new StringReader(this._query);
		}

		// Token: 0x06004D78 RID: 19832 RVA: 0x00111A65 File Offset: 0x0010FC65
		internal static CqlLexer.Token NewToken(short tokenId, Node tokenvalue)
		{
			return new CqlLexer.Token(tokenId, tokenvalue);
		}

		// Token: 0x06004D79 RID: 19833 RVA: 0x00111A6E File Offset: 0x0010FC6E
		internal static CqlLexer.Token NewToken(short tokenId, CqlLexer.TerminalToken termToken)
		{
			return new CqlLexer.Token(tokenId, termToken);
		}

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06004D7A RID: 19834 RVA: 0x00111A77 File Offset: 0x0010FC77
		internal string YYText
		{
			get
			{
				return this.yytext();
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06004D7B RID: 19835 RVA: 0x00111A7F File Offset: 0x0010FC7F
		internal int IPos
		{
			get
			{
				return this._iPos;
			}
		}

		// Token: 0x06004D7C RID: 19836 RVA: 0x00111A87 File Offset: 0x0010FC87
		internal int AdvanceIPos()
		{
			this._iPos += this.YYText.Length;
			return this._iPos;
		}

		// Token: 0x06004D7D RID: 19837 RVA: 0x00111AA7 File Offset: 0x0010FCA7
		internal static bool IsReservedKeyword(string term)
		{
			return CqlLexer.InternalKeywordDictionary.ContainsKey(term);
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x00111AB4 File Offset: 0x0010FCB4
		internal CqlLexer.Token MapIdentifierOrKeyword(string symbol)
		{
			CqlLexer.Token token;
			if (this.IsEscapedIdentifier(symbol, out token))
			{
				return token;
			}
			if (this.IsKeyword(symbol, out token))
			{
				return token;
			}
			return this.MapUnescapedIdentifier(symbol);
		}

		// Token: 0x06004D7F RID: 19839 RVA: 0x00111AE4 File Offset: 0x0010FCE4
		private bool IsEscapedIdentifier(string symbol, out CqlLexer.Token identifierToken)
		{
			if (symbol.Length <= 1 || symbol[0] != '[')
			{
				identifierToken = null;
				return false;
			}
			if (symbol[symbol.Length - 1] == ']')
			{
				Identifier identifier = new Identifier(symbol.Substring(1, symbol.Length - 2), true, this._query, this._iPos);
				identifier.ErrCtx.ErrorContextInfo = "CtxEscapedIdentifier";
				identifierToken = CqlLexer.NewToken(CqlParser.ESCAPED_IDENTIFIER, identifier);
				return true;
			}
			string text = Strings.InvalidEscapedIdentifier(symbol);
			throw EntitySqlException.Create(this._query, text, this._iPos, null, false, null);
		}

		// Token: 0x06004D80 RID: 19840 RVA: 0x00111B7C File Offset: 0x0010FD7C
		private bool IsKeyword(string symbol, out CqlLexer.Token terminalToken)
		{
			char lookAheadChar = this.GetLookAheadChar();
			if (!this.IsInSymbolAsIdentifierState(lookAheadChar) && !this.IsCanonicalFunctionCall(symbol, lookAheadChar) && CqlLexer.InternalKeywordDictionary.ContainsKey(symbol))
			{
				this.ResetSymbolAsIdentifierState(true);
				short num = CqlLexer.InternalKeywordDictionary[symbol];
				if (num == CqlParser.AS)
				{
					this._symbolAsAliasIdentifierState = true;
				}
				else if (num == CqlParser.FUNCTION)
				{
					this._symbolAsInlineFunctionNameState = true;
				}
				terminalToken = CqlLexer.NewToken(num, new CqlLexer.TerminalToken(symbol, this._iPos));
				return true;
			}
			terminalToken = null;
			return false;
		}

		// Token: 0x06004D81 RID: 19841 RVA: 0x00111BFD File Offset: 0x0010FDFD
		private bool IsCanonicalFunctionCall(string symbol, char lookAheadChar)
		{
			return lookAheadChar == '(' && CqlLexer.InternalCanonicalFunctionNames.Contains(symbol);
		}

		// Token: 0x06004D82 RID: 19842 RVA: 0x00111C14 File Offset: 0x0010FE14
		private CqlLexer.Token MapUnescapedIdentifier(string symbol)
		{
			bool flag = CqlLexer.InternalInvalidAliasNames.Contains(symbol);
			if (this._symbolAsInlineFunctionNameState)
			{
				flag |= CqlLexer.InternalInvalidInlineFunctionNames.Contains(symbol);
			}
			this.ResetSymbolAsIdentifierState(true);
			if (flag)
			{
				string text = Strings.InvalidAliasName(symbol);
				throw EntitySqlException.Create(this._query, text, this._iPos, null, false, null);
			}
			Identifier identifier = new Identifier(symbol, false, this._query, this._iPos);
			identifier.ErrCtx.ErrorContextInfo = "CtxIdentifier";
			return CqlLexer.NewToken(CqlParser.IDENTIFIER, identifier);
		}

		// Token: 0x06004D83 RID: 19843 RVA: 0x00111C9C File Offset: 0x0010FE9C
		private char GetLookAheadChar()
		{
			this.yy_mark_end();
			char c = this.yy_advance();
			while (c != '\u0081' && (char.IsWhiteSpace(c) || CqlLexer.IsNewLine(c)))
			{
				c = this.yy_advance();
			}
			this.yy_to_mark();
			return c;
		}

		// Token: 0x06004D84 RID: 19844 RVA: 0x00111CDE File Offset: 0x0010FEDE
		private bool IsInSymbolAsIdentifierState(char lookAheadChar)
		{
			return this._symbolAsIdentifierState || this._symbolAsAliasIdentifierState || this._symbolAsInlineFunctionNameState || lookAheadChar == '.';
		}

		// Token: 0x06004D85 RID: 19845 RVA: 0x00111CFF File Offset: 0x0010FEFF
		private void ResetSymbolAsIdentifierState(bool significant)
		{
			this._symbolAsIdentifierState = false;
			if (significant)
			{
				this._symbolAsAliasIdentifierState = false;
				this._symbolAsInlineFunctionNameState = false;
			}
		}

		// Token: 0x06004D86 RID: 19846 RVA: 0x00111D1C File Offset: 0x0010FF1C
		internal CqlLexer.Token MapOperator(string oper)
		{
			if (CqlLexer.InternalOperatorDictionary.ContainsKey(oper))
			{
				return CqlLexer.NewToken(CqlLexer.InternalOperatorDictionary[oper], new CqlLexer.TerminalToken(oper, this._iPos));
			}
			string invalidOperatorSymbol = Strings.InvalidOperatorSymbol;
			throw EntitySqlException.Create(this._query, invalidOperatorSymbol, this._iPos, null, false, null);
		}

		// Token: 0x06004D87 RID: 19847 RVA: 0x00111D70 File Offset: 0x0010FF70
		internal CqlLexer.Token MapPunctuator(string punct)
		{
			if (CqlLexer.InternalPunctuatorDictionary.ContainsKey(punct))
			{
				this.ResetSymbolAsIdentifierState(true);
				if (punct.Equals(".", StringComparison.OrdinalIgnoreCase))
				{
					this._symbolAsIdentifierState = true;
				}
				return CqlLexer.NewToken(CqlLexer.InternalPunctuatorDictionary[punct], new CqlLexer.TerminalToken(punct, this._iPos));
			}
			string invalidPunctuatorSymbol = Strings.InvalidPunctuatorSymbol;
			throw EntitySqlException.Create(this._query, invalidPunctuatorSymbol, this._iPos, null, false, null);
		}

		// Token: 0x06004D88 RID: 19848 RVA: 0x00111DDE File Offset: 0x0010FFDE
		internal CqlLexer.Token MapDoubleQuotedString(string symbol)
		{
			return this.NewLiteralToken(symbol, LiteralKind.String);
		}

		// Token: 0x06004D89 RID: 19849 RVA: 0x00111DE8 File Offset: 0x0010FFE8
		internal CqlLexer.Token NewLiteralToken(string literal, LiteralKind literalKind)
		{
			string text = literal;
			switch (literalKind)
			{
			case LiteralKind.String:
				if ('N' == literal[0])
				{
					literalKind = LiteralKind.UnicodeString;
				}
				break;
			case LiteralKind.Binary:
				text = CqlLexer.GetLiteralSingleQuotePayload(literal);
				if (!CqlLexer.IsValidBinaryValue(text))
				{
					string text2 = Strings.InvalidLiteralFormat("binary", text);
					throw EntitySqlException.Create(this._query, text2, this._iPos, null, false, null);
				}
				break;
			case LiteralKind.DateTime:
				text = CqlLexer.GetLiteralSingleQuotePayload(literal);
				if (!CqlLexer.IsValidDateTimeValue(text))
				{
					string text3 = Strings.InvalidLiteralFormat("datetime", text);
					throw EntitySqlException.Create(this._query, text3, this._iPos, null, false, null);
				}
				break;
			case LiteralKind.Time:
				text = CqlLexer.GetLiteralSingleQuotePayload(literal);
				if (!CqlLexer.IsValidTimeValue(text))
				{
					string text4 = Strings.InvalidLiteralFormat("time", text);
					throw EntitySqlException.Create(this._query, text4, this._iPos, null, false, null);
				}
				break;
			case LiteralKind.DateTimeOffset:
				text = CqlLexer.GetLiteralSingleQuotePayload(literal);
				if (!CqlLexer.IsValidDateTimeOffsetValue(text))
				{
					string text5 = Strings.InvalidLiteralFormat("datetimeoffset", text);
					throw EntitySqlException.Create(this._query, text5, this._iPos, null, false, null);
				}
				break;
			case LiteralKind.Guid:
				text = CqlLexer.GetLiteralSingleQuotePayload(literal);
				if (!CqlLexer.IsValidGuidValue(text))
				{
					string text6 = Strings.InvalidLiteralFormat("guid", text);
					throw EntitySqlException.Create(this._query, text6, this._iPos, null, false, null);
				}
				break;
			}
			return CqlLexer.NewToken(CqlParser.LITERAL, new Literal(text, literalKind, this._query, this._iPos));
		}

		// Token: 0x06004D8A RID: 19850 RVA: 0x00111F59 File Offset: 0x00110159
		internal CqlLexer.Token NewParameterToken(string param)
		{
			return CqlLexer.NewToken(CqlParser.PARAMETER, new QueryParameter(param, this._query, this._iPos));
		}

		// Token: 0x06004D8B RID: 19851 RVA: 0x00111F78 File Offset: 0x00110178
		internal CqlLexer.Token HandleEscapedIdentifiers()
		{
			for (char c = this.YYText[0]; c != '\u0081'; c = this.yy_advance())
			{
				if (c == ']')
				{
					this.yy_mark_end();
					c = this.yy_advance();
					if (c != ']')
					{
						this.yy_to_mark();
						this.ResetSymbolAsIdentifierState(true);
						return this.MapIdentifierOrKeyword(this.YYText.Replace("]]", "]"));
					}
				}
			}
			string text = Strings.InvalidEscapedIdentifierUnbalanced(this.YYText);
			throw EntitySqlException.Create(this._query, text, this._iPos, null, false, null);
		}

		// Token: 0x06004D8C RID: 19852 RVA: 0x00112004 File Offset: 0x00110204
		internal static bool IsLetterOrDigitOrUnderscore(string symbol, out bool isIdentifierASCII)
		{
			isIdentifierASCII = true;
			for (int i = 0; i < symbol.Length; i++)
			{
				isIdentifierASCII = isIdentifierASCII && symbol[i] < '\u0080';
				if (!isIdentifierASCII && !CqlLexer.IsLetter(symbol[i]) && !CqlLexer.IsDigit(symbol[i]) && symbol[i] != '_')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x0011206A File Offset: 0x0011026A
		private static bool IsLetter(char c)
		{
			return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x00112087 File Offset: 0x00110287
		private static bool IsDigit(char c)
		{
			return c >= '0' && c <= '9';
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x00112098 File Offset: 0x00110298
		private static bool isHexDigit(char c)
		{
			return CqlLexer.IsDigit(c) || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x001120C0 File Offset: 0x001102C0
		internal static bool IsNewLine(char c)
		{
			for (int i = 0; i < CqlLexer._newLineCharacters.Length; i++)
			{
				if (c == CqlLexer._newLineCharacters[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x001120EC File Offset: 0x001102EC
		private static string GetLiteralSingleQuotePayload(string literal)
		{
			if (literal.Split(new char[] { '\'' }).Length != 3 || -1 == literal.IndexOf('\'') || -1 == literal.LastIndexOf('\''))
			{
				throw new EntitySqlException(Strings.MalformedSingleQuotePayload);
			}
			int num = literal.IndexOf('\'');
			string text = literal.Substring(num + 1, literal.Length - (num + 2));
			if (text.Split(new char[] { '\'' }).Length != 1)
			{
				throw new EntitySqlException(Strings.MalformedSingleQuotePayload);
			}
			return text;
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x00112170 File Offset: 0x00110370
		private static bool IsValidGuidValue(string guidValue)
		{
			int num = 0;
			if (guidValue.Length - 1 - num + 1 != 36)
			{
				return false;
			}
			int num2 = 0;
			bool flag = true;
			while (flag && num2 < 36)
			{
				if (num2 == 8 || num2 == 13 || num2 == 18 || num2 == 23)
				{
					flag = guidValue[num + num2] == '-';
				}
				else
				{
					flag = CqlLexer.isHexDigit(guidValue[num + num2]);
				}
				num2++;
			}
			return flag;
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x001121D8 File Offset: 0x001103D8
		private static bool IsValidBinaryValue(string binaryValue)
		{
			if (string.IsNullOrEmpty(binaryValue))
			{
				return true;
			}
			int num = 0;
			bool flag;
			for (flag = binaryValue.Length > 0; flag && num < binaryValue.Length; flag = CqlLexer.isHexDigit(binaryValue[num++]))
			{
			}
			return flag;
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x0011221B File Offset: 0x0011041B
		private static bool IsValidDateTimeValue(string datetimeValue)
		{
			if (CqlLexer._reDateTimeValue == null)
			{
				CqlLexer._reDateTimeValue = new Regex("^[0-9]{4}-[0-9]{1,2}-[0-9]{1,2}([ ])+[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?$", RegexOptions.Singleline | RegexOptions.CultureInvariant);
			}
			return CqlLexer._reDateTimeValue.IsMatch(datetimeValue);
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x00112243 File Offset: 0x00110443
		private static bool IsValidTimeValue(string timeValue)
		{
			if (CqlLexer._reTimeValue == null)
			{
				CqlLexer._reTimeValue = new Regex("^[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?$", RegexOptions.Singleline | RegexOptions.CultureInvariant);
			}
			return CqlLexer._reTimeValue.IsMatch(timeValue);
		}

		// Token: 0x06004D96 RID: 19862 RVA: 0x0011226B File Offset: 0x0011046B
		private static bool IsValidDateTimeOffsetValue(string datetimeOffsetValue)
		{
			if (CqlLexer._reDateTimeOffsetValue == null)
			{
				CqlLexer._reDateTimeOffsetValue = new Regex("^[0-9]{4}-[0-9]{1,2}-[0-9]{1,2}([ ])+[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?([ ])*[\\+-][0-9]{1,2}:[0-9]{1,2}$", RegexOptions.Singleline | RegexOptions.CultureInvariant);
			}
			return CqlLexer._reDateTimeOffsetValue.IsMatch(datetimeOffsetValue);
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06004D97 RID: 19863 RVA: 0x00112294 File Offset: 0x00110494
		private static Dictionary<string, short> InternalKeywordDictionary
		{
			get
			{
				if (CqlLexer._keywords == null)
				{
					CqlLexer._keywords = new Dictionary<string, short>(60, CqlLexer._stringComparer)
					{
						{
							"all",
							CqlParser.ALL
						},
						{
							"and",
							CqlParser.AND
						},
						{
							"anyelement",
							CqlParser.ANYELEMENT
						},
						{
							"apply",
							CqlParser.APPLY
						},
						{
							"as",
							CqlParser.AS
						},
						{
							"asc",
							CqlParser.ASC
						},
						{
							"between",
							CqlParser.BETWEEN
						},
						{
							"by",
							CqlParser.BY
						},
						{
							"case",
							CqlParser.CASE
						},
						{
							"cast",
							CqlParser.CAST
						},
						{
							"collate",
							CqlParser.COLLATE
						},
						{
							"collection",
							CqlParser.COLLECTION
						},
						{
							"createref",
							CqlParser.CREATEREF
						},
						{
							"cross",
							CqlParser.CROSS
						},
						{
							"deref",
							CqlParser.DEREF
						},
						{
							"desc",
							CqlParser.DESC
						},
						{
							"distinct",
							CqlParser.DISTINCT
						},
						{
							"element",
							CqlParser.ELEMENT
						},
						{
							"else",
							CqlParser.ELSE
						},
						{
							"end",
							CqlParser.END
						},
						{
							"escape",
							CqlParser.ESCAPE
						},
						{
							"except",
							CqlParser.EXCEPT
						},
						{
							"exists",
							CqlParser.EXISTS
						},
						{
							"false",
							CqlParser.LITERAL
						},
						{
							"flatten",
							CqlParser.FLATTEN
						},
						{
							"from",
							CqlParser.FROM
						},
						{
							"full",
							CqlParser.FULL
						},
						{
							"function",
							CqlParser.FUNCTION
						},
						{
							"group",
							CqlParser.GROUP
						},
						{
							"grouppartition",
							CqlParser.GROUPPARTITION
						},
						{
							"having",
							CqlParser.HAVING
						},
						{
							"in",
							CqlParser.IN
						},
						{
							"inner",
							CqlParser.INNER
						},
						{
							"intersect",
							CqlParser.INTERSECT
						},
						{
							"is",
							CqlParser.IS
						},
						{
							"join",
							CqlParser.JOIN
						},
						{
							"key",
							CqlParser.KEY
						},
						{
							"left",
							CqlParser.LEFT
						},
						{
							"like",
							CqlParser.LIKE
						},
						{
							"limit",
							CqlParser.LIMIT
						},
						{
							"multiset",
							CqlParser.MULTISET
						},
						{
							"navigate",
							CqlParser.NAVIGATE
						},
						{
							"not",
							CqlParser.NOT
						},
						{
							"null",
							CqlParser.NULL
						},
						{
							"of",
							CqlParser.OF
						},
						{
							"oftype",
							CqlParser.OFTYPE
						},
						{
							"on",
							CqlParser.ON
						},
						{
							"only",
							CqlParser.ONLY
						},
						{
							"or",
							CqlParser.OR
						},
						{
							"order",
							CqlParser.ORDER
						},
						{
							"outer",
							CqlParser.OUTER
						},
						{
							"overlaps",
							CqlParser.OVERLAPS
						},
						{
							"ref",
							CqlParser.REF
						},
						{
							"relationship",
							CqlParser.RELATIONSHIP
						},
						{
							"right",
							CqlParser.RIGHT
						},
						{
							"row",
							CqlParser.ROW
						},
						{
							"select",
							CqlParser.SELECT
						},
						{
							"set",
							CqlParser.SET
						},
						{
							"skip",
							CqlParser.SKIP
						},
						{
							"then",
							CqlParser.THEN
						},
						{
							"top",
							CqlParser.TOP
						},
						{
							"treat",
							CqlParser.TREAT
						},
						{
							"true",
							CqlParser.LITERAL
						},
						{
							"union",
							CqlParser.UNION
						},
						{
							"using",
							CqlParser.USING
						},
						{
							"value",
							CqlParser.VALUE
						},
						{
							"when",
							CqlParser.WHEN
						},
						{
							"where",
							CqlParser.WHERE
						},
						{
							"with",
							CqlParser.WITH
						}
					};
				}
				return CqlLexer._keywords;
			}
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06004D98 RID: 19864 RVA: 0x00112714 File Offset: 0x00110914
		private static HashSet<string> InternalInvalidAliasNames
		{
			get
			{
				if (CqlLexer._invalidAliasNames == null)
				{
					CqlLexer._invalidAliasNames = new HashSet<string>(CqlLexer._stringComparer)
					{
						"all", "and", "apply", "as", "asc", "between", "by", "case", "cast", "collate",
						"createref", "deref", "desc", "distinct", "element", "else", "end", "escape", "except", "exists",
						"flatten", "from", "group", "having", "in", "inner", "intersect", "is", "join", "like",
						"multiset", "navigate", "not", "null", "of", "oftype", "on", "only", "or", "overlaps",
						"ref", "relationship", "select", "set", "then", "treat", "union", "using", "when", "where",
						"with"
					};
				}
				return CqlLexer._invalidAliasNames;
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06004D99 RID: 19865 RVA: 0x001129A4 File Offset: 0x00110BA4
		private static HashSet<string> InternalInvalidInlineFunctionNames
		{
			get
			{
				if (CqlLexer._invalidInlineFunctionNames == null)
				{
					CqlLexer._invalidInlineFunctionNames = new HashSet<string>(CqlLexer._stringComparer) { "anyelement", "element", "function", "grouppartition", "key", "ref", "row", "skip", "top", "value" };
				}
				return CqlLexer._invalidInlineFunctionNames;
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06004D9A RID: 19866 RVA: 0x00112A48 File Offset: 0x00110C48
		private static Dictionary<string, short> InternalOperatorDictionary
		{
			get
			{
				if (CqlLexer._operators == null)
				{
					CqlLexer._operators = new Dictionary<string, short>(16, CqlLexer._stringComparer)
					{
						{
							"==",
							CqlParser.OP_EQ
						},
						{
							"!=",
							CqlParser.OP_NEQ
						},
						{
							"<>",
							CqlParser.OP_NEQ
						},
						{
							"<",
							CqlParser.OP_LT
						},
						{
							"<=",
							CqlParser.OP_LE
						},
						{
							">",
							CqlParser.OP_GT
						},
						{
							">=",
							CqlParser.OP_GE
						},
						{
							"&&",
							CqlParser.AND
						},
						{
							"||",
							CqlParser.OR
						},
						{
							"!",
							CqlParser.NOT
						},
						{
							"+",
							CqlParser.PLUS
						},
						{
							"-",
							CqlParser.MINUS
						},
						{
							"*",
							CqlParser.STAR
						},
						{
							"/",
							CqlParser.FSLASH
						},
						{
							"%",
							CqlParser.PERCENT
						}
					};
				}
				return CqlLexer._operators;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06004D9B RID: 19867 RVA: 0x00112B68 File Offset: 0x00110D68
		private static Dictionary<string, short> InternalPunctuatorDictionary
		{
			get
			{
				if (CqlLexer._punctuators == null)
				{
					CqlLexer._punctuators = new Dictionary<string, short>(16, CqlLexer._stringComparer)
					{
						{
							",",
							CqlParser.COMMA
						},
						{
							":",
							CqlParser.COLON
						},
						{
							".",
							CqlParser.DOT
						},
						{
							"?",
							CqlParser.QMARK
						},
						{
							"(",
							CqlParser.L_PAREN
						},
						{
							")",
							CqlParser.R_PAREN
						},
						{
							"[",
							CqlParser.L_BRACE
						},
						{
							"]",
							CqlParser.R_BRACE
						},
						{
							"{",
							CqlParser.L_CURLY
						},
						{
							"}",
							CqlParser.R_CURLY
						},
						{
							";",
							CqlParser.SCOLON
						},
						{
							"=",
							CqlParser.EQUAL
						}
					};
				}
				return CqlLexer._punctuators;
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06004D9C RID: 19868 RVA: 0x00112C55 File Offset: 0x00110E55
		private static HashSet<string> InternalCanonicalFunctionNames
		{
			get
			{
				if (CqlLexer._canonicalFunctionNames == null)
				{
					CqlLexer._canonicalFunctionNames = new HashSet<string>(CqlLexer._stringComparer) { "left", "right" };
				}
				return CqlLexer._canonicalFunctionNames;
			}
		}

		// Token: 0x04001B69 RID: 7017
		private const int YY_BUFFER_SIZE = 512;

		// Token: 0x04001B6A RID: 7018
		private const int YY_F = -1;

		// Token: 0x04001B6B RID: 7019
		private const int YY_NO_STATE = -1;

		// Token: 0x04001B6C RID: 7020
		private const int YY_NOT_ACCEPT = 0;

		// Token: 0x04001B6D RID: 7021
		private const int YY_START = 1;

		// Token: 0x04001B6E RID: 7022
		private const int YY_END = 2;

		// Token: 0x04001B6F RID: 7023
		private const int YY_NO_ANCHOR = 4;

		// Token: 0x04001B70 RID: 7024
		private readonly CqlLexer.AcceptMethod[] accept_dispatch;

		// Token: 0x04001B71 RID: 7025
		private const int YY_BOL = 128;

		// Token: 0x04001B72 RID: 7026
		private const int YY_EOF = 129;

		// Token: 0x04001B73 RID: 7027
		private readonly TextReader yy_reader;

		// Token: 0x04001B74 RID: 7028
		private int yy_buffer_index;

		// Token: 0x04001B75 RID: 7029
		private int yy_buffer_read;

		// Token: 0x04001B76 RID: 7030
		private int yy_buffer_start;

		// Token: 0x04001B77 RID: 7031
		private int yy_buffer_end;

		// Token: 0x04001B78 RID: 7032
		private char[] yy_buffer;

		// Token: 0x04001B79 RID: 7033
		private int yychar;

		// Token: 0x04001B7A RID: 7034
		private int yyline;

		// Token: 0x04001B7B RID: 7035
		private bool yy_at_bol;

		// Token: 0x04001B7C RID: 7036
		private int yy_lexical_state;

		// Token: 0x04001B7D RID: 7037
		private const int YYINITIAL = 0;

		// Token: 0x04001B7E RID: 7038
		private static readonly int[] yy_state_dtrans = new int[1];

		// Token: 0x04001B7F RID: 7039
		private bool yy_last_was_cr;

		// Token: 0x04001B80 RID: 7040
		private const int YY_E_INTERNAL = 0;

		// Token: 0x04001B81 RID: 7041
		private const int YY_E_MATCH = 1;

		// Token: 0x04001B82 RID: 7042
		private static string[] yy_error_string = new string[] { "Error: Internal error.\n", "Error: Unmatched input.\n" };

		// Token: 0x04001B83 RID: 7043
		private static readonly int[] yy_acpt = new int[]
		{
			0, 4, 4, 4, 4, 4, 4, 4, 4, 4,
			4, 4, 4, 2, 4, 4, 4, 4, 4, 0,
			4, 4, 4, 4, 0, 4, 4, 4, 4, 0,
			4, 4, 4, 0, 4, 4, 0, 4, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 4, 4, 4, 4, 4, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
			4, 4, 4, 4, 4
		};

		// Token: 0x04001B84 RID: 7044
		private static readonly int[] yy_cmap = new int[]
		{
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			27, 11, 11, 8, 11, 11, 11, 11, 11, 11,
			11, 11, 11, 11, 11, 11, 11, 11, 11, 11,
			11, 11, 12, 33, 28, 11, 11, 39, 36, 10,
			40, 40, 39, 38, 40, 25, 24, 39, 22, 22,
			22, 22, 22, 22, 22, 22, 22, 22, 40, 40,
			34, 32, 35, 40, 29, 5, 2, 30, 13, 15,
			18, 20, 30, 3, 30, 30, 23, 16, 26, 17,
			30, 30, 6, 19, 14, 21, 30, 30, 9, 7,
			30, 1, 11, 40, 11, 31, 11, 5, 2, 30,
			13, 15, 18, 20, 30, 3, 30, 30, 23, 16,
			4, 17, 30, 30, 6, 19, 14, 21, 30, 30,
			9, 7, 30, 40, 37, 40, 11, 11, 0, 41
		};

		// Token: 0x04001B85 RID: 7045
		private static readonly int[] yy_rmap = new int[]
		{
			0, 1, 1, 2, 3, 4, 5, 6, 7, 8,
			9, 10, 1, 1, 11, 1, 1, 1, 1, 12,
			13, 1, 14, 14, 15, 16, 17, 1, 18, 10,
			19, 20, 1, 21, 22, 23, 24, 25, 26, 27,
			5, 28, 29, 30, 31, 32, 33, 34, 35, 36,
			37, 38, 39, 40, 41, 42, 43, 44, 45, 46,
			47, 48, 49, 50, 51, 52, 53, 54, 55, 56,
			57, 58, 59, 60, 61, 62, 63, 11, 64, 65,
			66, 67, 68, 11, 69
		};

		// Token: 0x04001B86 RID: 7046
		private static readonly int[,] yy_nxt = new int[,]
		{
			{
				1, 2, 3, 83, 83, 83, 83, 83, 4, 20,
				19, -1, 4, 84, 64, 83, 83, 83, 71, 83,
				72, 83, 5, 83, 6, 7, 25, 8, 24, 29,
				83, 83, 22, 23, 28, 23, 33, 36, 32, 32,
				27, 1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 76, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, 4, -1,
				-1, -1, 4, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, 21, -1, 39, 21, -1, 21, -1,
				-1, 26, 5, 31, 40, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, 35, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, 41, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, 8, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				19, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, 24, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 11, 11, 11, 11, 11, 11, -1, 11,
				-1, -1, -1, 11, 11, 11, 11, 11, 11, 11,
				11, 11, 11, 11, -1, -1, 11, -1, -1, -1,
				11, 11, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, 19, 19, 19, 19, 19, 19, 19, 19, 19,
				9, 19, 19, 19, 19, 19, 19, 19, 19, 19,
				19, 19, 19, 19, 19, 19, 19, 19, 19, 19,
				19, 19, 19, 19, 19, 19, 19, 19, 19, 19,
				19, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				38, -1, -1, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, 32, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 10, 24,
				24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
				24, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				19, -1, -1, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, 24, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, 21, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, 32, -1, -1, 32, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 14, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, 21, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, 32, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, 44, 83,
				45, -1, 44, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, 21, -1, 39, 21, -1, 21, -1,
				-1, -1, 35, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, 32, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, 21, -1, -1, -1, -1, 21, -1,
				-1, -1, 37, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, 38, 38, 38, 38, 38, 38, 38, -1, 38,
				12, 38, 38, 38, 38, 38, 38, 38, 38, 38,
				38, 38, 38, 38, 38, 38, 38, -1, -1, 38,
				38, 38, 38, 38, 38, 38, 38, 38, 38, 38,
				38, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, 37, -1, -1, 42, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, 42, -1,
				-1, -1
			},
			{
				-1, 41, 41, 41, 41, 41, 41, 41, 43, 41,
				41, 41, 41, 41, 41, 41, 41, 41, 41, 41,
				41, 41, 41, 41, 41, 41, 41, 13, 41, 41,
				41, 41, 41, 41, 41, 41, 41, 41, 41, 41,
				41, 13
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, 37, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, 13, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, 44, -1,
				45, -1, 44, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, 45, 45, 45, 45, 45, 45, 45, -1, 45,
				15, 45, 45, 45, 45, 45, 45, 45, 45, 45,
				45, 45, 45, 45, 45, 45, 45, -1, -1, 45,
				45, 45, 45, 45, 45, 45, 45, 45, 45, 45,
				45, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, 46, -1,
				47, -1, 46, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, 47, 47, 47, 47, 47, 47, 47, -1, 47,
				16, 47, 47, 47, 47, 47, 47, 47, 47, 47,
				47, 47, 47, 47, 47, 47, 47, -1, -1, 47,
				47, 47, 47, 47, 47, 47, 47, 47, 47, 47,
				47, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, 48, -1,
				38, -1, 48, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, 49, -1,
				50, -1, 49, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, 50, 50, 50, 50, 50, 50, 50, -1, 50,
				17, 50, 50, 50, 50, 50, 50, 50, 50, 50,
				50, 50, 50, 50, 50, 50, 50, -1, -1, 50,
				50, 50, 50, 50, 50, 50, 50, 50, 50, 50,
				50, -1
			},
			{
				-1, -1, -1, -1, -1, -1, -1, -1, 51, -1,
				52, -1, 51, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, 52, 52, 52, 52, 52, 52, 52, -1, 52,
				18, 52, 52, 52, 52, 52, 52, 52, 52, 52,
				52, 52, 52, 52, 52, 52, 52, -1, -1, 52,
				52, 52, 52, 52, 52, 52, 52, 52, 52, 52,
				52, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 83, 83,
				83, 30, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, 46, 83,
				47, -1, 46, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 34, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, 48, 83,
				38, -1, 48, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 83, 30,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, 49, 83,
				50, -1, 49, 83, 83, 83, 83, 81, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 54, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, 51, 83,
				52, -1, 51, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 56, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 58, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 60, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 65, 83, 83, 53, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 55, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 57, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 59, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 61, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 62, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 63, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 66, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 83, 83,
				83, 67, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 68, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 69, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 83, 70,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 73, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 73, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 79, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 80, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 74, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 82, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 83, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 75, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			},
			{
				-1, -1, 83, 83, 83, 78, 83, 83, -1, 83,
				-1, -1, -1, 83, 83, 83, 83, 83, 83, 83,
				83, 83, 77, 83, -1, -1, 83, -1, -1, -1,
				83, 77, -1, -1, -1, -1, -1, -1, -1, -1,
				-1, -1
			}
		};

		// Token: 0x04001B87 RID: 7047
		private static readonly StringComparer _stringComparer = StringComparer.OrdinalIgnoreCase;

		// Token: 0x04001B88 RID: 7048
		private static Dictionary<string, short> _keywords;

		// Token: 0x04001B89 RID: 7049
		private static HashSet<string> _invalidAliasNames;

		// Token: 0x04001B8A RID: 7050
		private static HashSet<string> _invalidInlineFunctionNames;

		// Token: 0x04001B8B RID: 7051
		private static Dictionary<string, short> _operators;

		// Token: 0x04001B8C RID: 7052
		private static Dictionary<string, short> _punctuators;

		// Token: 0x04001B8D RID: 7053
		private static HashSet<string> _canonicalFunctionNames;

		// Token: 0x04001B8E RID: 7054
		private static Regex _reDateTimeValue;

		// Token: 0x04001B8F RID: 7055
		private static Regex _reTimeValue;

		// Token: 0x04001B90 RID: 7056
		private static Regex _reDateTimeOffsetValue;

		// Token: 0x04001B91 RID: 7057
		private const string _datetimeValueRegularExpression = "^[0-9]{4}-[0-9]{1,2}-[0-9]{1,2}([ ])+[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?$";

		// Token: 0x04001B92 RID: 7058
		private const string _timeValueRegularExpression = "^[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?$";

		// Token: 0x04001B93 RID: 7059
		private const string _datetimeOffsetValueRegularExpression = "^[0-9]{4}-[0-9]{1,2}-[0-9]{1,2}([ ])+[0-9]{1,2}:[0-9]{1,2}(:[0-9]{1,2}(\\.[0-9]{1,7})?)?([ ])*[\\+-][0-9]{1,2}:[0-9]{1,2}$";

		// Token: 0x04001B94 RID: 7060
		private int _iPos;

		// Token: 0x04001B95 RID: 7061
		private int _lineNumber;

		// Token: 0x04001B96 RID: 7062
		private ParserOptions _parserOptions;

		// Token: 0x04001B97 RID: 7063
		private readonly string _query;

		// Token: 0x04001B98 RID: 7064
		private bool _symbolAsIdentifierState;

		// Token: 0x04001B99 RID: 7065
		private bool _symbolAsAliasIdentifierState;

		// Token: 0x04001B9A RID: 7066
		private bool _symbolAsInlineFunctionNameState;

		// Token: 0x04001B9B RID: 7067
		private static readonly char[] _newLineCharacters = new char[] { '\n', '\u0085', '\v', '\u2028', '\u2029' };

		// Token: 0x02000C69 RID: 3177
		// (Invoke) Token: 0x06006B18 RID: 27416
		private delegate CqlLexer.Token AcceptMethod();

		// Token: 0x02000C6A RID: 3178
		internal class Token
		{
			// Token: 0x06006B1B RID: 27419 RVA: 0x0016EACE File Offset: 0x0016CCCE
			internal Token(short tokenId, Node tokenValue)
			{
				this._tokenId = tokenId;
				this._tokenValue = tokenValue;
			}

			// Token: 0x06006B1C RID: 27420 RVA: 0x0016EAE4 File Offset: 0x0016CCE4
			internal Token(short tokenId, CqlLexer.TerminalToken terminal)
			{
				this._tokenId = tokenId;
				this._tokenValue = terminal;
			}

			// Token: 0x17001188 RID: 4488
			// (get) Token: 0x06006B1D RID: 27421 RVA: 0x0016EAFA File Offset: 0x0016CCFA
			internal short TokenId
			{
				get
				{
					return this._tokenId;
				}
			}

			// Token: 0x17001189 RID: 4489
			// (get) Token: 0x06006B1E RID: 27422 RVA: 0x0016EB02 File Offset: 0x0016CD02
			internal object Value
			{
				get
				{
					return this._tokenValue;
				}
			}

			// Token: 0x0400310F RID: 12559
			private readonly short _tokenId;

			// Token: 0x04003110 RID: 12560
			private readonly object _tokenValue;
		}

		// Token: 0x02000C6B RID: 3179
		internal class TerminalToken
		{
			// Token: 0x06006B1F RID: 27423 RVA: 0x0016EB0A File Offset: 0x0016CD0A
			internal TerminalToken(string token, int iPos)
			{
				this._token = token;
				this._iPos = iPos;
			}

			// Token: 0x1700118A RID: 4490
			// (get) Token: 0x06006B20 RID: 27424 RVA: 0x0016EB20 File Offset: 0x0016CD20
			internal int IPos
			{
				get
				{
					return this._iPos;
				}
			}

			// Token: 0x1700118B RID: 4491
			// (get) Token: 0x06006B21 RID: 27425 RVA: 0x0016EB28 File Offset: 0x0016CD28
			internal string Token
			{
				get
				{
					return this._token;
				}
			}

			// Token: 0x04003111 RID: 12561
			private readonly string _token;

			// Token: 0x04003112 RID: 12562
			private readonly int _iPos;
		}

		// Token: 0x02000C6C RID: 3180
		internal static class yy_translate
		{
			// Token: 0x06006B22 RID: 27426 RVA: 0x0016EB30 File Offset: 0x0016CD30
			internal static char translate(char c)
			{
				if (char.IsWhiteSpace(c) || char.IsControl(c))
				{
					if (CqlLexer.IsNewLine(c))
					{
						return '\n';
					}
					return ' ';
				}
				else
				{
					if (c < '\u007f')
					{
						return c;
					}
					if (char.IsLetter(c) || char.IsSymbol(c) || char.IsNumber(c))
					{
						return 'a';
					}
					return '`';
				}
			}
		}
	}
}
