using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200007F RID: 127
	internal class WellKnownTextSqlReader : SpatialReader<TextReader>
	{
		// Token: 0x06000307 RID: 775 RVA: 0x000086C9 File Offset: 0x000068C9
		public WellKnownTextSqlReader(SpatialPipeline destination)
			: this(destination, false)
		{
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000086D3 File Offset: 0x000068D3
		public WellKnownTextSqlReader(SpatialPipeline destination, bool allowOnlyTwoDimensions)
			: base(destination)
		{
			this.allowOnlyTwoDimensions = allowOnlyTwoDimensions;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000086E3 File Offset: 0x000068E3
		protected override void ReadGeographyImplementation(TextReader input)
		{
			new WellKnownTextSqlReader.Parser(input, new TypeWashedToGeographyLongLatPipeline(base.Destination), this.allowOnlyTwoDimensions).Read();
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00008701 File Offset: 0x00006901
		protected override void ReadGeometryImplementation(TextReader input)
		{
			new WellKnownTextSqlReader.Parser(input, new TypeWashedToGeometryPipeline(base.Destination), this.allowOnlyTwoDimensions).Read();
		}

		// Token: 0x040000E8 RID: 232
		private bool allowOnlyTwoDimensions;

		// Token: 0x02000080 RID: 128
		private class Parser
		{
			// Token: 0x0600030B RID: 779 RVA: 0x0000871F File Offset: 0x0000691F
			public Parser(TextReader reader, TypeWashedPipeline pipeline, bool allowOnlyTwoDimensions)
			{
				this.lexer = new WellKnownTextLexer(reader);
				this.pipeline = pipeline;
				this.allowOnlyTwoDimensions = allowOnlyTwoDimensions;
			}

			// Token: 0x0600030C RID: 780 RVA: 0x00008741 File Offset: 0x00006941
			[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Literals should not be localized")]
			public void Read()
			{
				this.ParseSRID();
				this.ParseTaggedText();
			}

			// Token: 0x0600030D RID: 781 RVA: 0x0000874F File Offset: 0x0000694F
			private bool IsTokenMatch(WellKnownTextTokenType type, string text)
			{
				return this.lexer.CurrentToken.MatchToken((int)type, text, 5);
			}

			// Token: 0x0600030E RID: 782 RVA: 0x00008764 File Offset: 0x00006964
			private bool NextToken()
			{
				while (this.lexer.Next())
				{
					if (!this.lexer.CurrentToken.MatchToken(8, string.Empty, 4))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600030F RID: 783 RVA: 0x00008791 File Offset: 0x00006991
			private void ParseCollectionText()
			{
				if (!this.ReadEmptySet())
				{
					this.ReadToken(WellKnownTextTokenType.LeftParen, null);
					this.ParseTaggedText();
					while (this.ReadOptionalToken(WellKnownTextTokenType.Comma, null))
					{
						this.ParseTaggedText();
					}
					this.ReadToken(WellKnownTextTokenType.RightParen, null);
				}
			}

			// Token: 0x06000310 RID: 784 RVA: 0x000087C3 File Offset: 0x000069C3
			private void ParseLineStringText()
			{
				if (!this.ReadEmptySet())
				{
					this.ReadToken(WellKnownTextTokenType.LeftParen, null);
					this.ParsePoint(true);
					while (this.ReadOptionalToken(WellKnownTextTokenType.Comma, null))
					{
						this.ParsePoint(false);
					}
					this.ReadToken(WellKnownTextTokenType.RightParen, null);
					this.pipeline.EndFigure();
				}
			}

			// Token: 0x06000311 RID: 785 RVA: 0x00008804 File Offset: 0x00006A04
			private void ParseMultiGeoText(SpatialType innerType, Action innerReader)
			{
				if (!this.ReadEmptySet())
				{
					this.ReadToken(WellKnownTextTokenType.LeftParen, null);
					this.pipeline.BeginGeo(innerType);
					innerReader.Invoke();
					this.pipeline.EndGeo();
					while (this.ReadOptionalToken(WellKnownTextTokenType.Comma, null))
					{
						this.pipeline.BeginGeo(innerType);
						innerReader.Invoke();
						this.pipeline.EndGeo();
					}
					this.ReadToken(WellKnownTextTokenType.RightParen, null);
				}
			}

			// Token: 0x06000312 RID: 786 RVA: 0x00008870 File Offset: 0x00006A70
			private void ParsePoint(bool firstFigure)
			{
				double num = this.ReadDouble();
				double num2 = this.ReadDouble();
				double? num3;
				if (this.TryReadOptionalNullableDouble(out num3) && this.allowOnlyTwoDimensions)
				{
					throw new FormatException(Strings.WellKnownText_TooManyDimensions);
				}
				double? num4;
				if (this.TryReadOptionalNullableDouble(out num4) && this.allowOnlyTwoDimensions)
				{
					throw new FormatException(Strings.WellKnownText_TooManyDimensions);
				}
				if (firstFigure)
				{
					this.pipeline.BeginFigure(num, num2, num3, num4);
					return;
				}
				this.pipeline.LineTo(num, num2, num3, num4);
			}

			// Token: 0x06000313 RID: 787 RVA: 0x000088E7 File Offset: 0x00006AE7
			private void ParsePointText()
			{
				if (!this.ReadEmptySet())
				{
					this.ReadToken(WellKnownTextTokenType.LeftParen, null);
					this.ParsePoint(true);
					this.ReadToken(WellKnownTextTokenType.RightParen, null);
					this.pipeline.EndFigure();
				}
			}

			// Token: 0x06000314 RID: 788 RVA: 0x00008913 File Offset: 0x00006B13
			private void ParsePolygonText()
			{
				if (!this.ReadEmptySet())
				{
					this.ReadToken(WellKnownTextTokenType.LeftParen, null);
					this.ParseLineStringText();
					while (this.ReadOptionalToken(WellKnownTextTokenType.Comma, null))
					{
						this.ParseLineStringText();
					}
					this.ReadToken(WellKnownTextTokenType.RightParen, null);
				}
			}

			// Token: 0x06000315 RID: 789 RVA: 0x00008948 File Offset: 0x00006B48
			private void ParseSRID()
			{
				if (this.ReadOptionalToken(WellKnownTextTokenType.Text, "SRID"))
				{
					this.ReadToken(WellKnownTextTokenType.Equals, null);
					this.pipeline.SetCoordinateSystem(new int?(this.ReadInteger()));
					this.ReadToken(WellKnownTextTokenType.Semicolon, null);
					return;
				}
				this.pipeline.SetCoordinateSystem(default(int?));
			}

			// Token: 0x06000316 RID: 790 RVA: 0x000089A0 File Offset: 0x00006BA0
			private void ParseTaggedText()
			{
				if (!this.NextToken())
				{
					throw new FormatException(Strings.WellKnownText_UnknownTaggedText(string.Empty));
				}
				string text;
				if ((text = this.lexer.CurrentToken.Text.ToUpperInvariant()) != null)
				{
					if (<PrivateImplementationDetails>{224B6CBA-CF75-43C3-B91B-1B39CE6F7CAF}.$$method0x600030c-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(8);
						dictionary.Add("POINT", 0);
						dictionary.Add("LINESTRING", 1);
						dictionary.Add("POLYGON", 2);
						dictionary.Add("MULTIPOINT", 3);
						dictionary.Add("MULTILINESTRING", 4);
						dictionary.Add("MULTIPOLYGON", 5);
						dictionary.Add("GEOMETRYCOLLECTION", 6);
						dictionary.Add("FULLGLOBE", 7);
						<PrivateImplementationDetails>{224B6CBA-CF75-43C3-B91B-1B39CE6F7CAF}.$$method0x600030c-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{224B6CBA-CF75-43C3-B91B-1B39CE6F7CAF}.$$method0x600030c-1.TryGetValue(text, ref num))
					{
						switch (num)
						{
						case 0:
							this.pipeline.BeginGeo(SpatialType.Point);
							this.ParsePointText();
							this.pipeline.EndGeo();
							return;
						case 1:
							this.pipeline.BeginGeo(SpatialType.LineString);
							this.ParseLineStringText();
							this.pipeline.EndGeo();
							return;
						case 2:
							this.pipeline.BeginGeo(SpatialType.Polygon);
							this.ParsePolygonText();
							this.pipeline.EndGeo();
							return;
						case 3:
							this.pipeline.BeginGeo(SpatialType.MultiPoint);
							this.ParseMultiGeoText(SpatialType.Point, new Action(this.ParsePointText));
							this.pipeline.EndGeo();
							return;
						case 4:
							this.pipeline.BeginGeo(SpatialType.MultiLineString);
							this.ParseMultiGeoText(SpatialType.LineString, new Action(this.ParseLineStringText));
							this.pipeline.EndGeo();
							return;
						case 5:
							this.pipeline.BeginGeo(SpatialType.MultiPolygon);
							this.ParseMultiGeoText(SpatialType.Polygon, new Action(this.ParsePolygonText));
							this.pipeline.EndGeo();
							return;
						case 6:
							this.pipeline.BeginGeo(SpatialType.Collection);
							this.ParseCollectionText();
							this.pipeline.EndGeo();
							return;
						case 7:
							this.pipeline.BeginGeo(SpatialType.FullGlobe);
							this.pipeline.EndGeo();
							return;
						}
					}
				}
				throw new FormatException(Strings.WellKnownText_UnknownTaggedText(this.lexer.CurrentToken.Text));
			}

			// Token: 0x06000317 RID: 791 RVA: 0x00008BC4 File Offset: 0x00006DC4
			private double ReadDouble()
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.ReadToken(WellKnownTextTokenType.Number, null);
				stringBuilder.Append(this.lexer.CurrentToken.Text);
				if (this.ReadOptionalToken(WellKnownTextTokenType.Period, null))
				{
					stringBuilder.Append(".");
					this.ReadToken(WellKnownTextTokenType.Number, null);
					stringBuilder.Append(this.lexer.CurrentToken.Text);
				}
				return double.Parse(stringBuilder.ToString(), CultureInfo.InvariantCulture);
			}

			// Token: 0x06000318 RID: 792 RVA: 0x00008C3B File Offset: 0x00006E3B
			private bool ReadEmptySet()
			{
				return this.ReadOptionalToken(WellKnownTextTokenType.Text, "EMPTY");
			}

			// Token: 0x06000319 RID: 793 RVA: 0x00008C49 File Offset: 0x00006E49
			private int ReadInteger()
			{
				this.ReadToken(WellKnownTextTokenType.Number, null);
				return XmlConvert.ToInt32(this.lexer.CurrentToken.Text);
			}

			// Token: 0x0600031A RID: 794 RVA: 0x00008C68 File Offset: 0x00006E68
			private bool TryReadOptionalNullableDouble(out double? value)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.ReadOptionalToken(WellKnownTextTokenType.Number, null))
				{
					stringBuilder.Append(this.lexer.CurrentToken.Text);
					if (this.ReadOptionalToken(WellKnownTextTokenType.Period, null))
					{
						stringBuilder.Append(".");
						this.ReadToken(WellKnownTextTokenType.Number, null);
						stringBuilder.Append(this.lexer.CurrentToken.Text);
					}
					value = new double?(double.Parse(stringBuilder.ToString(), CultureInfo.InvariantCulture));
					return true;
				}
				value = default(double?);
				return this.ReadOptionalToken(WellKnownTextTokenType.Text, "NULL");
			}

			// Token: 0x0600031B RID: 795 RVA: 0x00008D04 File Offset: 0x00006F04
			private bool ReadOptionalToken(WellKnownTextTokenType expectedTokenType, string expectedTokenText)
			{
				LexerToken lexerToken;
				while (this.lexer.Peek(out lexerToken))
				{
					if (lexerToken.MatchToken(8, null, 5))
					{
						this.lexer.Next();
					}
					else
					{
						if (lexerToken.MatchToken((int)expectedTokenType, expectedTokenText, 5))
						{
							this.lexer.Next();
							return true;
						}
						return false;
					}
				}
				return false;
			}

			// Token: 0x0600031C RID: 796 RVA: 0x00008D57 File Offset: 0x00006F57
			private void ReadToken(WellKnownTextTokenType type, string text)
			{
				if (!this.NextToken() || !this.IsTokenMatch(type, text))
				{
					throw new FormatException(Strings.WellKnownText_UnexpectedToken(type, text, this.lexer.CurrentToken));
				}
			}

			// Token: 0x040000E9 RID: 233
			private readonly bool allowOnlyTwoDimensions;

			// Token: 0x040000EA RID: 234
			private readonly TextLexerBase lexer;

			// Token: 0x040000EB RID: 235
			private readonly TypeWashedPipeline pipeline;
		}
	}
}
