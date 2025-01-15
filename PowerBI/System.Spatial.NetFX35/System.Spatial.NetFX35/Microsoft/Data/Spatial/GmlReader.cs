using System;
using System.Collections.Generic;
using System.Spatial;
using System.Xml;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000064 RID: 100
	internal class GmlReader : SpatialReader<XmlReader>
	{
		// Token: 0x0600027E RID: 638 RVA: 0x00006DE0 File Offset: 0x00004FE0
		public GmlReader(SpatialPipeline destination)
			: base(destination)
		{
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00006DE9 File Offset: 0x00004FE9
		protected override void ReadGeographyImplementation(XmlReader input)
		{
			new GmlReader.Parser(input, new TypeWashedToGeographyLatLongPipeline(base.Destination)).Read();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00006E01 File Offset: 0x00005001
		protected override void ReadGeometryImplementation(XmlReader input)
		{
			new GmlReader.Parser(input, new TypeWashedToGeometryPipeline(base.Destination)).Read();
		}

		// Token: 0x02000065 RID: 101
		private class Parser
		{
			// Token: 0x06000281 RID: 641 RVA: 0x00006E1C File Offset: 0x0000501C
			internal Parser(XmlReader reader, TypeWashedPipeline pipeline)
			{
				this.reader = reader;
				this.pipeline = pipeline;
				XmlNameTable nameTable = this.reader.NameTable;
				this.gmlNamespace = nameTable.Add("http://www.opengis.net/gml");
				this.fullGlobeNamespace = nameTable.Add("http://schemas.microsoft.com/sqlserver/2011/geography");
			}

			// Token: 0x06000282 RID: 642 RVA: 0x00006E6B File Offset: 0x0000506B
			public void Read()
			{
				this.ParseGmlGeometry(true);
			}

			// Token: 0x06000283 RID: 643 RVA: 0x00006E74 File Offset: 0x00005074
			private void ParseGmlGeometry(bool readCoordinateSystem)
			{
				if (!this.reader.IsStartElement())
				{
					throw new FormatException(Strings.GmlReader_ExpectReaderAtElement);
				}
				if (object.ReferenceEquals(this.reader.NamespaceURI, this.gmlNamespace))
				{
					this.ReadAttributes(readCoordinateSystem);
					string localName;
					if ((localName = this.reader.LocalName) != null)
					{
						if (<PrivateImplementationDetails>{01C72562-032F-41AC-AA49-E7E2BA5613D8}.$$method0x600027b-1 == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(7);
							dictionary.Add("Point", 0);
							dictionary.Add("LineString", 1);
							dictionary.Add("Polygon", 2);
							dictionary.Add("MultiPoint", 3);
							dictionary.Add("MultiCurve", 4);
							dictionary.Add("MultiSurface", 5);
							dictionary.Add("MultiGeometry", 6);
							<PrivateImplementationDetails>{01C72562-032F-41AC-AA49-E7E2BA5613D8}.$$method0x600027b-1 = dictionary;
						}
						int num;
						if (<PrivateImplementationDetails>{01C72562-032F-41AC-AA49-E7E2BA5613D8}.$$method0x600027b-1.TryGetValue(localName, ref num))
						{
							switch (num)
							{
							case 0:
								this.ParseGmlPointShape();
								return;
							case 1:
								this.ParseGmlLineStringShape();
								return;
							case 2:
								this.ParseGmlPolygonShape();
								return;
							case 3:
								this.ParseGmlMultiPointShape();
								return;
							case 4:
								this.ParseGmlMultiCurveShape();
								return;
							case 5:
								this.ParseGmlMultiSurfaceShape();
								return;
							case 6:
								this.ParseGmlMultiGeometryShape();
								return;
							}
						}
					}
					throw new FormatException(Strings.GmlReader_InvalidSpatialType(this.reader.LocalName));
				}
				if (object.ReferenceEquals(this.reader.NamespaceURI, this.fullGlobeNamespace) && this.reader.LocalName.Equals("FullGlobe"))
				{
					this.ReadAttributes(readCoordinateSystem);
					this.ParseGmlFullGlobeElement();
					return;
				}
				throw new FormatException(Strings.GmlReader_ExpectReaderAtElement);
			}

			// Token: 0x06000284 RID: 644 RVA: 0x00006FFC File Offset: 0x000051FC
			private void ReadAttributes(bool expectSrsName)
			{
				bool flag = false;
				this.reader.MoveToContent();
				if (this.reader.MoveToFirstAttribute())
				{
					string localName;
					for (;;)
					{
						if (!this.reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/", 4))
						{
							localName = this.reader.LocalName;
							string text;
							if ((text = localName) == null)
							{
								goto IL_011B;
							}
							if (!(text == "axisLabels") && !(text == "uomLabels") && !(text == "count") && !(text == "id"))
							{
								if (!(text == "srsName"))
								{
									goto IL_011B;
								}
								if (!expectSrsName)
								{
									break;
								}
								string value = this.reader.Value;
								if (!value.StartsWith("http://www.opengis.net/def/crs/EPSG/0/", 4))
								{
									goto IL_010B;
								}
								int num = XmlConvert.ToInt32(value.Substring("http://www.opengis.net/def/crs/EPSG/0/".Length));
								this.pipeline.SetCoordinateSystem(new int?(num));
								flag = true;
							}
						}
						if (!this.reader.MoveToNextAttribute())
						{
							goto Block_10;
						}
					}
					this.reader.MoveToElement();
					throw new FormatException(Strings.GmlReader_InvalidAttribute(localName, this.reader.Name));
					IL_010B:
					throw new FormatException(Strings.GmlReader_InvalidSrsName("http://www.opengis.net/def/crs/EPSG/0/"));
					IL_011B:
					this.reader.MoveToElement();
					throw new FormatException(Strings.GmlReader_InvalidAttribute(localName, this.reader.Name));
					Block_10:
					this.reader.MoveToElement();
				}
				if (expectSrsName && !flag)
				{
					this.pipeline.SetCoordinateSystem(default(int?));
				}
			}

			// Token: 0x06000285 RID: 645 RVA: 0x0000717E File Offset: 0x0000537E
			private void ParseGmlPointShape()
			{
				this.pipeline.BeginGeo(SpatialType.Point);
				this.PrepareFigure();
				this.ParseGmlPointElement(true);
				this.EndFigure();
				this.pipeline.EndGeo();
			}

			// Token: 0x06000286 RID: 646 RVA: 0x000071AA File Offset: 0x000053AA
			private void ParseGmlLineStringShape()
			{
				this.pipeline.BeginGeo(SpatialType.LineString);
				this.PrepareFigure();
				this.ParseGmlLineString();
				this.EndFigure();
				this.pipeline.EndGeo();
			}

			// Token: 0x06000287 RID: 647 RVA: 0x000071D8 File Offset: 0x000053D8
			private void ParseGmlPolygonShape()
			{
				this.pipeline.BeginGeo(SpatialType.Polygon);
				if (this.ReadStartOrEmptyElement("Polygon"))
				{
					this.ReadSkippableElements();
					if (!this.IsEndElement("Polygon"))
					{
						this.PrepareFigure();
						this.ParseGmlRingElement("exterior");
						this.EndFigure();
						this.ReadSkippableElements();
						while (this.IsStartElement("interior"))
						{
							this.PrepareFigure();
							this.ParseGmlRingElement("interior");
							this.EndFigure();
							this.ReadSkippableElements();
						}
					}
					this.ReadSkippableElements();
					this.ReadEndElement();
				}
				this.pipeline.EndGeo();
			}

			// Token: 0x06000288 RID: 648 RVA: 0x00007271 File Offset: 0x00005471
			private void ParseGmlMultiPointShape()
			{
				this.pipeline.BeginGeo(SpatialType.MultiPoint);
				this.ParseMultiItemElement("MultiPoint", "pointMember", "pointMembers", new Action(this.ParseGmlPointShape));
				this.pipeline.EndGeo();
			}

			// Token: 0x06000289 RID: 649 RVA: 0x000072AB File Offset: 0x000054AB
			private void ParseGmlMultiCurveShape()
			{
				this.pipeline.BeginGeo(SpatialType.MultiLineString);
				this.ParseMultiItemElement("MultiCurve", "curveMember", "curveMembers", new Action(this.ParseGmlLineStringShape));
				this.pipeline.EndGeo();
			}

			// Token: 0x0600028A RID: 650 RVA: 0x000072E5 File Offset: 0x000054E5
			private void ParseGmlMultiSurfaceShape()
			{
				this.pipeline.BeginGeo(SpatialType.MultiPolygon);
				this.ParseMultiItemElement("MultiSurface", "surfaceMember", "surfaceMembers", new Action(this.ParseGmlPolygonShape));
				this.pipeline.EndGeo();
			}

			// Token: 0x0600028B RID: 651 RVA: 0x00007328 File Offset: 0x00005528
			private void ParseGmlMultiGeometryShape()
			{
				this.pipeline.BeginGeo(SpatialType.Collection);
				this.ParseMultiItemElement("MultiGeometry", "geometryMember", "geometryMembers", delegate
				{
					this.ParseGmlGeometry(false);
				});
				this.pipeline.EndGeo();
			}

			// Token: 0x0600028C RID: 652 RVA: 0x00007362 File Offset: 0x00005562
			private void ParseGmlFullGlobeElement()
			{
				this.pipeline.BeginGeo(SpatialType.FullGlobe);
				if (this.ReadStartOrEmptyElement("FullGlobe") && this.IsEndElement("FullGlobe"))
				{
					this.ReadEndElement();
				}
				this.pipeline.EndGeo();
			}

			// Token: 0x0600028D RID: 653 RVA: 0x0000739C File Offset: 0x0000559C
			private void ParseGmlPointElement(bool allowEmpty)
			{
				if (this.ReadStartOrEmptyElement("Point"))
				{
					this.ReadSkippableElements();
					this.ParseGmlPosElement(allowEmpty);
					this.ReadSkippableElements();
					this.ReadEndElement();
				}
			}

			// Token: 0x0600028E RID: 654 RVA: 0x000073C4 File Offset: 0x000055C4
			private void ParseGmlLineString()
			{
				if (this.ReadStartOrEmptyElement("LineString"))
				{
					this.ReadSkippableElements();
					if (this.IsPosListStart())
					{
						this.ParsePosList(false);
					}
					else
					{
						this.ParseGmlPosListElement(true);
					}
					this.ReadSkippableElements();
					this.ReadEndElement();
				}
			}

			// Token: 0x0600028F RID: 655 RVA: 0x000073FD File Offset: 0x000055FD
			private void ParseGmlRingElement(string ringTag)
			{
				if (this.ReadStartOrEmptyElement(ringTag))
				{
					if (!this.IsEndElement(ringTag))
					{
						this.ParseGmlLinearRingElement();
					}
					this.ReadEndElement();
				}
			}

			// Token: 0x06000290 RID: 656 RVA: 0x00007420 File Offset: 0x00005620
			private void ParseGmlLinearRingElement()
			{
				if (this.ReadStartOrEmptyElement("LinearRing"))
				{
					if (this.IsEndElement("LinearRing"))
					{
						throw new FormatException(Strings.GmlReader_EmptyRingsNotAllowed);
					}
					if (this.IsPosListStart())
					{
						this.ParsePosList(false);
					}
					else
					{
						this.ParseGmlPosListElement(false);
					}
					this.ReadEndElement();
				}
			}

			// Token: 0x06000291 RID: 657 RVA: 0x00007470 File Offset: 0x00005670
			private void ParseMultiItemElement(string header, string member, string members, Action parseItem)
			{
				if (this.ReadStartOrEmptyElement(header))
				{
					this.ReadSkippableElements();
					if (!this.IsEndElement(header))
					{
						while (this.IsStartElement(member))
						{
							if (this.ReadStartOrEmptyElement(member) && !this.IsEndElement(member))
							{
								parseItem.Invoke();
								this.ReadEndElement();
								this.ReadSkippableElements();
							}
						}
						if (this.IsStartElement(members) && this.ReadStartOrEmptyElement(members))
						{
							while (this.reader.IsStartElement())
							{
								parseItem.Invoke();
							}
							this.ReadEndElement();
						}
					}
					this.ReadSkippableElements();
					this.ReadEndElement();
				}
			}

			// Token: 0x06000292 RID: 658 RVA: 0x00007500 File Offset: 0x00005700
			private void ParseGmlPosElement(bool allowEmpty)
			{
				this.ReadAttributes(false);
				if (this.ReadStartOrEmptyElement("pos"))
				{
					double[] array = this.ReadContentAsDoubleArray();
					if (array.Length != 0)
					{
						if (array.Length < 2)
						{
							throw new FormatException(Strings.GmlReader_PosNeedTwoNumbers);
						}
						this.AddPoint(array[0], array[1], (array.Length > 2) ? new double?(array[2]) : default(double?), (array.Length > 3) ? new double?(array[3]) : default(double?));
					}
					else if (!allowEmpty)
					{
						throw new FormatException(Strings.GmlReader_PosNeedTwoNumbers);
					}
					this.ReadEndElement();
					return;
				}
				if (!allowEmpty)
				{
					throw new FormatException(Strings.GmlReader_PosNeedTwoNumbers);
				}
			}

			// Token: 0x06000293 RID: 659 RVA: 0x000075A1 File Offset: 0x000057A1
			private void ParsePosList(bool allowEmpty)
			{
				do
				{
					if (this.IsStartElement("pos"))
					{
						this.ParseGmlPosElement(allowEmpty);
					}
					else
					{
						this.ParseGmlPointPropertyElement(allowEmpty);
					}
				}
				while (this.IsPosListStart());
			}

			// Token: 0x06000294 RID: 660 RVA: 0x000075C8 File Offset: 0x000057C8
			private void ParseGmlPointPropertyElement(bool allowEmpty)
			{
				if (this.ReadStartOrEmptyElement("pointProperty"))
				{
					this.ParseGmlPointElement(allowEmpty);
					this.ReadEndElement();
				}
			}

			// Token: 0x06000295 RID: 661 RVA: 0x000075E4 File Offset: 0x000057E4
			private void ParseGmlPosListElement(bool allowEmpty)
			{
				if (this.ReadStartOrEmptyElement("posList"))
				{
					if (!this.IsEndElement("posList"))
					{
						double[] array = this.ReadContentAsDoubleArray();
						if (array.Length == 0)
						{
							throw new FormatException(Strings.GmlReader_PosListNeedsEvenCount);
						}
						if (array.Length % 2 != 0)
						{
							throw new FormatException(Strings.GmlReader_PosListNeedsEvenCount);
						}
						for (int i = 0; i < array.Length; i += 2)
						{
							this.AddPoint(array[i], array[i + 1], default(double?), default(double?));
						}
					}
					else if (!allowEmpty)
					{
						throw new FormatException(Strings.GmlReader_PosListNeedsEvenCount);
					}
					this.ReadEndElement();
					return;
				}
				if (!allowEmpty)
				{
					throw new FormatException(Strings.GmlReader_PosListNeedsEvenCount);
				}
			}

			// Token: 0x06000296 RID: 662 RVA: 0x00007688 File Offset: 0x00005888
			private double[] ReadContentAsDoubleArray()
			{
				string[] array = this.reader.ReadContentAsString().Split(GmlReader.Parser.coordinateDelimiter, 1);
				double[] array2 = new double[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = XmlConvert.ToDouble(array[i]);
				}
				return array2;
			}

			// Token: 0x06000297 RID: 663 RVA: 0x000076D0 File Offset: 0x000058D0
			private bool ReadStartOrEmptyElement(string element)
			{
				bool isEmptyElement = this.reader.IsEmptyElement;
				if (element != "FullGlobe")
				{
					this.reader.ReadStartElement(element, this.gmlNamespace);
				}
				else
				{
					this.reader.ReadStartElement(element, "http://schemas.microsoft.com/sqlserver/2011/geography");
				}
				return !isEmptyElement;
			}

			// Token: 0x06000298 RID: 664 RVA: 0x0000771F File Offset: 0x0000591F
			private bool IsStartElement(string element)
			{
				return this.reader.IsStartElement(element, this.gmlNamespace);
			}

			// Token: 0x06000299 RID: 665 RVA: 0x00007733 File Offset: 0x00005933
			private bool IsEndElement(string element)
			{
				this.reader.MoveToContent();
				return this.reader.NodeType == 15 && this.reader.LocalName.Equals(element, 4);
			}

			// Token: 0x0600029A RID: 666 RVA: 0x00007764 File Offset: 0x00005964
			private void ReadEndElement()
			{
				this.reader.MoveToContent();
				if (this.reader.NodeType != 15)
				{
					throw new FormatException(Strings.GmlReader_UnexpectedElement(this.reader.Name));
				}
				this.reader.ReadEndElement();
			}

			// Token: 0x0600029B RID: 667 RVA: 0x000077A4 File Offset: 0x000059A4
			private void ReadSkippableElements()
			{
				bool flag = true;
				while (flag)
				{
					this.reader.MoveToContent();
					if (this.reader.NodeType == 1 && object.ReferenceEquals(this.reader.NamespaceURI, this.gmlNamespace))
					{
						string localName = this.reader.LocalName;
						flag = GmlReader.Parser.skippableElements.ContainsKey(localName);
					}
					else
					{
						flag = false;
					}
					if (flag)
					{
						this.reader.Skip();
					}
				}
			}

			// Token: 0x0600029C RID: 668 RVA: 0x00007814 File Offset: 0x00005A14
			private bool IsPosListStart()
			{
				return this.IsStartElement("pos") || this.IsStartElement("pointProperty");
			}

			// Token: 0x0600029D RID: 669 RVA: 0x00007830 File Offset: 0x00005A30
			private void PrepareFigure()
			{
				this.points = 0;
			}

			// Token: 0x0600029E RID: 670 RVA: 0x0000783C File Offset: 0x00005A3C
			private void AddPoint(double x, double y, double? z, double? m)
			{
				if (z != null && double.IsNaN(z.Value))
				{
					z = default(double?);
				}
				if (m != null && double.IsNaN(m.Value))
				{
					m = default(double?);
				}
				if (this.points == 0)
				{
					this.pipeline.BeginFigure(x, y, z, m);
				}
				else
				{
					this.pipeline.LineTo(x, y, z, m);
				}
				this.points++;
			}

			// Token: 0x0600029F RID: 671 RVA: 0x000078BF File Offset: 0x00005ABF
			private void EndFigure()
			{
				if (this.points > 0)
				{
					this.pipeline.EndFigure();
				}
			}

			// Token: 0x060002A1 RID: 673 RVA: 0x000078E0 File Offset: 0x00005AE0
			// Note: this type is marked as 'beforefieldinit'.
			static Parser()
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
				dictionary.Add("name", "name");
				dictionary.Add("description", "description");
				dictionary.Add("metaDataProperty", "metaDataProperty");
				dictionary.Add("descriptionReference", "descriptionReference");
				dictionary.Add("identifier", "identifier");
				GmlReader.Parser.skippableElements = dictionary;
			}

			// Token: 0x04000079 RID: 121
			private static readonly char[] coordinateDelimiter = new char[] { ' ', '\t', '\r', '\n' };

			// Token: 0x0400007A RID: 122
			private static readonly Dictionary<string, string> skippableElements;

			// Token: 0x0400007B RID: 123
			private readonly string gmlNamespace;

			// Token: 0x0400007C RID: 124
			private readonly string fullGlobeNamespace;

			// Token: 0x0400007D RID: 125
			private readonly TypeWashedPipeline pipeline;

			// Token: 0x0400007E RID: 126
			private readonly XmlReader reader;

			// Token: 0x0400007F RID: 127
			private int points;
		}
	}
}
