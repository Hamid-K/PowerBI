using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.Spatial
{
	// Token: 0x02000066 RID: 102
	internal class WellKnownTextSqlReader : SpatialReader<TextReader>
	{
		// Token: 0x06000267 RID: 615 RVA: 0x000060E4 File Offset: 0x000042E4
		public WellKnownTextSqlReader(SpatialPipeline destination)
			: this(destination, false)
		{
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000060EE File Offset: 0x000042EE
		public WellKnownTextSqlReader(SpatialPipeline destination, bool allowOnlyTwoDimensions)
			: base(destination)
		{
			this.allowOnlyTwoDimensions = allowOnlyTwoDimensions;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000060FE File Offset: 0x000042FE
		protected override void ReadGeographyImplementation(TextReader input)
		{
			new WellKnownTextSqlReader.Parser(input, new TypeWashedToGeographyLongLatPipeline(base.Destination), this.allowOnlyTwoDimensions).Read();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000611C File Offset: 0x0000431C
		protected override void ReadGeometryImplementation(TextReader input)
		{
			new WellKnownTextSqlReader.Parser(input, new TypeWashedToGeometryPipeline(base.Destination), this.allowOnlyTwoDimensions).Read();
		}

		// Token: 0x040000A9 RID: 169
		private bool allowOnlyTwoDimensions;

		// Token: 0x02000086 RID: 134
		private class Parser
		{
			// Token: 0x0600035D RID: 861 RVA: 0x00008B1B File Offset: 0x00006D1B
			public Parser(TextReader reader, TypeWashedPipeline pipeline, bool allowOnlyTwoDimensions)
			{
				this.lexer = new WellKnownTextLexer(reader);
				this.pipeline = pipeline;
				this.allowOnlyTwoDimensions = allowOnlyTwoDimensions;
			}

			// Token: 0x0600035E RID: 862 RVA: 0x00008B3D File Offset: 0x00006D3D
			public void Read()
			{
				this.ParseSRID();
				this.ParseTaggedText();
			}

			// Token: 0x0600035F RID: 863 RVA: 0x00008B4B File Offset: 0x00006D4B
			private bool IsTokenMatch(WellKnownTextTokenType type, string text)
			{
				return this.lexer.CurrentToken.MatchToken((int)type, text, 5);
			}

			// Token: 0x06000360 RID: 864 RVA: 0x00008B60 File Offset: 0x00006D60
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

			// Token: 0x06000361 RID: 865 RVA: 0x00008B8D File Offset: 0x00006D8D
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

			// Token: 0x06000362 RID: 866 RVA: 0x00008BBF File Offset: 0x00006DBF
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

			// Token: 0x06000363 RID: 867 RVA: 0x00008C00 File Offset: 0x00006E00
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

			// Token: 0x06000364 RID: 868 RVA: 0x00008C6C File Offset: 0x00006E6C
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

			// Token: 0x06000365 RID: 869 RVA: 0x00008CE3 File Offset: 0x00006EE3
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

			// Token: 0x06000366 RID: 870 RVA: 0x00008D0F File Offset: 0x00006F0F
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

			// Token: 0x06000367 RID: 871 RVA: 0x00008D44 File Offset: 0x00006F44
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

			// Token: 0x06000368 RID: 872 RVA: 0x00008D9C File Offset: 0x00006F9C
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

			// Token: 0x06000369 RID: 873 RVA: 0x00009028 File Offset: 0x00007228
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

			// Token: 0x0600036A RID: 874 RVA: 0x0000909F File Offset: 0x0000729F
			private bool ReadEmptySet()
			{
				return this.ReadOptionalToken(WellKnownTextTokenType.Text, "EMPTY");
			}

			// Token: 0x0600036B RID: 875 RVA: 0x000090AD File Offset: 0x000072AD
			private int ReadInteger()
			{
				this.ReadToken(WellKnownTextTokenType.Number, null);
				return XmlConvert.ToInt32(this.lexer.CurrentToken.Text);
			}

			// Token: 0x0600036C RID: 876 RVA: 0x000090CC File Offset: 0x000072CC
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

			// Token: 0x0600036D RID: 877 RVA: 0x00009168 File Offset: 0x00007368
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

			// Token: 0x0600036E RID: 878 RVA: 0x000091BB File Offset: 0x000073BB
			private void ReadToken(WellKnownTextTokenType type, string text)
			{
				if (!this.NextToken() || !this.IsTokenMatch(type, text))
				{
					throw new FormatException(Strings.WellKnownText_UnexpectedToken(type, text, this.lexer.CurrentToken));
				}
			}

			// Token: 0x04000140 RID: 320
			private readonly bool allowOnlyTwoDimensions;

			// Token: 0x04000141 RID: 321
			private readonly TextLexerBase lexer;

			// Token: 0x04000142 RID: 322
			private readonly TypeWashedPipeline pipeline;
		}
	}
}
