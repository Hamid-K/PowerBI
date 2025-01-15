using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Spatial;
using System.Text;
using System.Xml;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200007E RID: 126
	internal class WellKnownTextSqlReader : SpatialReader<TextReader>
	{
		// Token: 0x060002FD RID: 765 RVA: 0x00008759 File Offset: 0x00006959
		public WellKnownTextSqlReader(SpatialPipeline destination)
			: this(destination, false)
		{
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00008763 File Offset: 0x00006963
		public WellKnownTextSqlReader(SpatialPipeline destination, bool allowOnlyTwoDimensions)
			: base(destination)
		{
			this.allowOnlyTwoDimensions = allowOnlyTwoDimensions;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00008773 File Offset: 0x00006973
		protected override void ReadGeographyImplementation(TextReader input)
		{
			new WellKnownTextSqlReader.Parser(input, new TypeWashedToGeographyLongLatPipeline(base.Destination), this.allowOnlyTwoDimensions).Read();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00008791 File Offset: 0x00006991
		protected override void ReadGeometryImplementation(TextReader input)
		{
			new WellKnownTextSqlReader.Parser(input, new TypeWashedToGeometryPipeline(base.Destination), this.allowOnlyTwoDimensions).Read();
		}

		// Token: 0x040000E6 RID: 230
		private bool allowOnlyTwoDimensions;

		// Token: 0x0200007F RID: 127
		private class Parser
		{
			// Token: 0x06000301 RID: 769 RVA: 0x000087AF File Offset: 0x000069AF
			public Parser(TextReader reader, TypeWashedPipeline pipeline, bool allowOnlyTwoDimensions)
			{
				this.lexer = new WellKnownTextLexer(reader);
				this.pipeline = pipeline;
				this.allowOnlyTwoDimensions = allowOnlyTwoDimensions;
			}

			// Token: 0x06000302 RID: 770 RVA: 0x000087D1 File Offset: 0x000069D1
			public void Read()
			{
				this.ParseSRID();
				this.ParseTaggedText();
			}

			// Token: 0x06000303 RID: 771 RVA: 0x000087DF File Offset: 0x000069DF
			private bool IsTokenMatch(WellKnownTextTokenType type, string text)
			{
				return this.lexer.CurrentToken.MatchToken((int)type, text, 5);
			}

			// Token: 0x06000304 RID: 772 RVA: 0x000087F4 File Offset: 0x000069F4
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

			// Token: 0x06000305 RID: 773 RVA: 0x00008821 File Offset: 0x00006A21
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

			// Token: 0x06000306 RID: 774 RVA: 0x00008853 File Offset: 0x00006A53
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

			// Token: 0x06000307 RID: 775 RVA: 0x00008894 File Offset: 0x00006A94
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

			// Token: 0x06000308 RID: 776 RVA: 0x00008900 File Offset: 0x00006B00
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

			// Token: 0x06000309 RID: 777 RVA: 0x00008977 File Offset: 0x00006B77
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

			// Token: 0x0600030A RID: 778 RVA: 0x000089A3 File Offset: 0x00006BA3
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

			// Token: 0x0600030B RID: 779 RVA: 0x000089D8 File Offset: 0x00006BD8
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

			// Token: 0x0600030C RID: 780 RVA: 0x00008A30 File Offset: 0x00006C30
			private void ParseTaggedText()
			{
				if (!this.NextToken())
				{
					throw new FormatException(Strings.WellKnownText_UnknownTaggedText(string.Empty));
				}
				string text;
				if ((text = this.lexer.CurrentToken.Text.ToUpperInvariant()) != null)
				{
					if (<PrivateImplementationDetails>{01C72562-032F-41AC-AA49-E7E2BA5613D8}.$$method0x6000301-1 == null)
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
						<PrivateImplementationDetails>{01C72562-032F-41AC-AA49-E7E2BA5613D8}.$$method0x6000301-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{01C72562-032F-41AC-AA49-E7E2BA5613D8}.$$method0x6000301-1.TryGetValue(text, ref num))
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

			// Token: 0x0600030D RID: 781 RVA: 0x00008C54 File Offset: 0x00006E54
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

			// Token: 0x0600030E RID: 782 RVA: 0x00008CCB File Offset: 0x00006ECB
			private bool ReadEmptySet()
			{
				return this.ReadOptionalToken(WellKnownTextTokenType.Text, "EMPTY");
			}

			// Token: 0x0600030F RID: 783 RVA: 0x00008CD9 File Offset: 0x00006ED9
			private int ReadInteger()
			{
				this.ReadToken(WellKnownTextTokenType.Number, null);
				return XmlConvert.ToInt32(this.lexer.CurrentToken.Text);
			}

			// Token: 0x06000310 RID: 784 RVA: 0x00008CF8 File Offset: 0x00006EF8
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

			// Token: 0x06000311 RID: 785 RVA: 0x00008D94 File Offset: 0x00006F94
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

			// Token: 0x06000312 RID: 786 RVA: 0x00008DE7 File Offset: 0x00006FE7
			private void ReadToken(WellKnownTextTokenType type, string text)
			{
				if (!this.NextToken() || !this.IsTokenMatch(type, text))
				{
					throw new FormatException(Strings.WellKnownText_UnexpectedToken(type, text, this.lexer.CurrentToken));
				}
			}

			// Token: 0x040000E7 RID: 231
			private readonly bool allowOnlyTwoDimensions;

			// Token: 0x040000E8 RID: 232
			private readonly TextLexerBase lexer;

			// Token: 0x040000E9 RID: 233
			private readonly TypeWashedPipeline pipeline;
		}
	}
}
