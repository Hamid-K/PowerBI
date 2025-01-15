using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.Spatial
{
	// Token: 0x0200006B RID: 107
	internal class WellKnownTextSqlReader : SpatialReader<TextReader>
	{
		// Token: 0x060002DD RID: 733 RVA: 0x00006DAC File Offset: 0x00004FAC
		public WellKnownTextSqlReader(SpatialPipeline destination)
			: this(destination, false)
		{
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00006DB6 File Offset: 0x00004FB6
		public WellKnownTextSqlReader(SpatialPipeline destination, bool allowOnlyTwoDimensions)
			: base(destination)
		{
			this.allowOnlyTwoDimensions = allowOnlyTwoDimensions;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00006DC6 File Offset: 0x00004FC6
		protected override void ReadGeographyImplementation(TextReader input)
		{
			new WellKnownTextSqlReader.Parser(input, new TypeWashedToGeographyLongLatPipeline(base.Destination), this.allowOnlyTwoDimensions).Read();
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00006DE4 File Offset: 0x00004FE4
		protected override void ReadGeometryImplementation(TextReader input)
		{
			new WellKnownTextSqlReader.Parser(input, new TypeWashedToGeometryPipeline(base.Destination), this.allowOnlyTwoDimensions).Read();
		}

		// Token: 0x040000B6 RID: 182
		private bool allowOnlyTwoDimensions;

		// Token: 0x02000092 RID: 146
		private class Parser
		{
			// Token: 0x060003E5 RID: 997 RVA: 0x0000987F File Offset: 0x00007A7F
			public Parser(TextReader reader, TypeWashedPipeline pipeline, bool allowOnlyTwoDimensions)
			{
				this.lexer = new WellKnownTextLexer(reader);
				this.pipeline = pipeline;
				this.allowOnlyTwoDimensions = allowOnlyTwoDimensions;
			}

			// Token: 0x060003E6 RID: 998 RVA: 0x000098A1 File Offset: 0x00007AA1
			public void Read()
			{
				this.ParseSRID();
				this.ParseTaggedText();
			}

			// Token: 0x060003E7 RID: 999 RVA: 0x000098AF File Offset: 0x00007AAF
			private bool IsTokenMatch(WellKnownTextTokenType type, string text)
			{
				return this.lexer.CurrentToken.MatchToken((int)type, text, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060003E8 RID: 1000 RVA: 0x000098C4 File Offset: 0x00007AC4
			private bool NextToken()
			{
				while (this.lexer.Next())
				{
					if (!this.lexer.CurrentToken.MatchToken(8, string.Empty, StringComparison.Ordinal))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060003E9 RID: 1001 RVA: 0x000098F1 File Offset: 0x00007AF1
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

			// Token: 0x060003EA RID: 1002 RVA: 0x00009923 File Offset: 0x00007B23
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

			// Token: 0x060003EB RID: 1003 RVA: 0x00009964 File Offset: 0x00007B64
			private void ParseMultiGeoText(SpatialType innerType, Action innerReader)
			{
				if (!this.ReadEmptySet())
				{
					this.ReadToken(WellKnownTextTokenType.LeftParen, null);
					this.pipeline.BeginGeo(innerType);
					innerReader();
					this.pipeline.EndGeo();
					while (this.ReadOptionalToken(WellKnownTextTokenType.Comma, null))
					{
						this.pipeline.BeginGeo(innerType);
						innerReader();
						this.pipeline.EndGeo();
					}
					this.ReadToken(WellKnownTextTokenType.RightParen, null);
				}
			}

			// Token: 0x060003EC RID: 1004 RVA: 0x000099D0 File Offset: 0x00007BD0
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

			// Token: 0x060003ED RID: 1005 RVA: 0x00009A47 File Offset: 0x00007C47
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

			// Token: 0x060003EE RID: 1006 RVA: 0x00009A73 File Offset: 0x00007C73
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

			// Token: 0x060003EF RID: 1007 RVA: 0x00009AA8 File Offset: 0x00007CA8
			private void ParseSRID()
			{
				if (this.ReadOptionalToken(WellKnownTextTokenType.Text, "SRID"))
				{
					this.ReadToken(WellKnownTextTokenType.Equals, null);
					this.pipeline.SetCoordinateSystem(new int?(this.ReadInteger()));
					this.ReadToken(WellKnownTextTokenType.Semicolon, null);
					return;
				}
				this.pipeline.SetCoordinateSystem(null);
			}

			// Token: 0x060003F0 RID: 1008 RVA: 0x00009B00 File Offset: 0x00007D00
			private void ParseTaggedText()
			{
				if (!this.NextToken())
				{
					throw new FormatException(Strings.WellKnownText_UnknownTaggedText(string.Empty));
				}
				string text = this.lexer.CurrentToken.Text.ToUpperInvariant();
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 583673641U)
				{
					if (num <= 257385033U)
					{
						if (num != 42671777U)
						{
							if (num == 257385033U)
							{
								if (text == "MULTILINESTRING")
								{
									this.pipeline.BeginGeo(SpatialType.MultiLineString);
									this.ParseMultiGeoText(SpatialType.LineString, new Action(this.ParseLineStringText));
									this.pipeline.EndGeo();
									return;
								}
							}
						}
						else if (text == "FULLGLOBE")
						{
							this.pipeline.BeginGeo(SpatialType.FullGlobe);
							this.pipeline.EndGeo();
							return;
						}
					}
					else if (num != 282801906U)
					{
						if (num == 583673641U)
						{
							if (text == "POLYGON")
							{
								this.pipeline.BeginGeo(SpatialType.Polygon);
								this.ParsePolygonText();
								this.pipeline.EndGeo();
								return;
							}
						}
					}
					else if (text == "LINESTRING")
					{
						this.pipeline.BeginGeo(SpatialType.LineString);
						this.ParseLineStringText();
						this.pipeline.EndGeo();
						return;
					}
				}
				else if (num <= 4017463941U)
				{
					if (num != 1680085828U)
					{
						if (num == 4017463941U)
						{
							if (text == "GEOMETRYCOLLECTION")
							{
								this.pipeline.BeginGeo(SpatialType.Collection);
								this.ParseCollectionText();
								this.pipeline.EndGeo();
								return;
							}
						}
					}
					else if (text == "MULTIPOINT")
					{
						this.pipeline.BeginGeo(SpatialType.MultiPoint);
						this.ParseMultiGeoText(SpatialType.Point, new Action(this.ParsePointText));
						this.pipeline.EndGeo();
						return;
					}
				}
				else if (num != 4029292593U)
				{
					if (num == 4148353876U)
					{
						if (text == "MULTIPOLYGON")
						{
							this.pipeline.BeginGeo(SpatialType.MultiPolygon);
							this.ParseMultiGeoText(SpatialType.Polygon, new Action(this.ParsePolygonText));
							this.pipeline.EndGeo();
							return;
						}
					}
				}
				else if (text == "POINT")
				{
					this.pipeline.BeginGeo(SpatialType.Point);
					this.ParsePointText();
					this.pipeline.EndGeo();
					return;
				}
				throw new FormatException(Strings.WellKnownText_UnknownTaggedText(this.lexer.CurrentToken.Text));
			}

			// Token: 0x060003F1 RID: 1009 RVA: 0x00009D8C File Offset: 0x00007F8C
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

			// Token: 0x060003F2 RID: 1010 RVA: 0x00009E03 File Offset: 0x00008003
			private bool ReadEmptySet()
			{
				return this.ReadOptionalToken(WellKnownTextTokenType.Text, "EMPTY");
			}

			// Token: 0x060003F3 RID: 1011 RVA: 0x00009E11 File Offset: 0x00008011
			private int ReadInteger()
			{
				this.ReadToken(WellKnownTextTokenType.Number, null);
				return XmlConvert.ToInt32(this.lexer.CurrentToken.Text);
			}

			// Token: 0x060003F4 RID: 1012 RVA: 0x00009E30 File Offset: 0x00008030
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
				value = null;
				return this.ReadOptionalToken(WellKnownTextTokenType.Text, "NULL");
			}

			// Token: 0x060003F5 RID: 1013 RVA: 0x00009ECC File Offset: 0x000080CC
			private bool ReadOptionalToken(WellKnownTextTokenType expectedTokenType, string expectedTokenText)
			{
				LexerToken lexerToken;
				while (this.lexer.Peek(out lexerToken))
				{
					if (lexerToken.MatchToken(8, null, StringComparison.OrdinalIgnoreCase))
					{
						this.lexer.Next();
					}
					else
					{
						if (lexerToken.MatchToken((int)expectedTokenType, expectedTokenText, StringComparison.OrdinalIgnoreCase))
						{
							this.lexer.Next();
							return true;
						}
						return false;
					}
				}
				return false;
			}

			// Token: 0x060003F6 RID: 1014 RVA: 0x00009F1F File Offset: 0x0000811F
			private void ReadToken(WellKnownTextTokenType type, string text)
			{
				if (!this.NextToken() || !this.IsTokenMatch(type, text))
				{
					throw new FormatException(Strings.WellKnownText_UnexpectedToken(type, text, this.lexer.CurrentToken));
				}
			}

			// Token: 0x0400015C RID: 348
			private readonly bool allowOnlyTwoDimensions;

			// Token: 0x0400015D RID: 349
			private readonly TextLexerBase lexer;

			// Token: 0x0400015E RID: 350
			private readonly TypeWashedPipeline pipeline;
		}
	}
}
