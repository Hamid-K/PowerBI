using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Spatial
{
	// Token: 0x02000061 RID: 97
	internal class GmlReader : SpatialReader<XmlReader>
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x000066BC File Offset: 0x000048BC
		public GmlReader(SpatialPipeline destination)
			: base(destination)
		{
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000066C5 File Offset: 0x000048C5
		protected override void ReadGeographyImplementation(XmlReader input)
		{
			new GmlReader.Parser(input, new TypeWashedToGeographyLatLongPipeline(base.Destination)).Read();
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000066DD File Offset: 0x000048DD
		protected override void ReadGeometryImplementation(XmlReader input)
		{
			new GmlReader.Parser(input, new TypeWashedToGeometryPipeline(base.Destination)).Read();
		}

		// Token: 0x0200008E RID: 142
		private class Parser
		{
			// Token: 0x06000397 RID: 919 RVA: 0x00008630 File Offset: 0x00006830
			internal Parser(XmlReader reader, TypeWashedPipeline pipeline)
			{
				this.reader = reader;
				this.pipeline = pipeline;
				XmlNameTable nameTable = this.reader.NameTable;
				this.gmlNamespace = nameTable.Add("http://www.opengis.net/gml");
				this.fullGlobeNamespace = nameTable.Add("http://schemas.microsoft.com/sqlserver/2011/geography");
			}

			// Token: 0x06000398 RID: 920 RVA: 0x0000867F File Offset: 0x0000687F
			public void Read()
			{
				this.ParseGmlGeometry(true);
			}

			// Token: 0x06000399 RID: 921 RVA: 0x00008688 File Offset: 0x00006888
			private void ParseGmlGeometry(bool readCoordinateSystem)
			{
				if (!this.reader.IsStartElement())
				{
					throw new FormatException(Strings.GmlReader_ExpectReaderAtElement);
				}
				if (this.reader.NamespaceURI == this.gmlNamespace)
				{
					this.ReadAttributes(readCoordinateSystem);
					string localName = this.reader.LocalName;
					uint num = <PrivateImplementationDetails>.ComputeStringHash(localName);
					if (num <= 3207645382U)
					{
						if (num != 2386032169U)
						{
							if (num != 2478244245U)
							{
								if (num == 3207645382U)
								{
									if (localName == "MultiGeometry")
									{
										this.ParseGmlMultiGeometryShape();
										return;
									}
								}
							}
							else if (localName == "MultiCurve")
							{
								this.ParseGmlMultiCurveShape();
								return;
							}
						}
						else if (localName == "Polygon")
						{
							this.ParseGmlPolygonShape();
							return;
						}
					}
					else if (num <= 3694217412U)
					{
						if (num != 3274687489U)
						{
							if (num == 3694217412U)
							{
								if (localName == "MultiPoint")
								{
									this.ParseGmlMultiPointShape();
									return;
								}
							}
						}
						else if (localName == "MultiSurface")
						{
							this.ParseGmlMultiSurfaceShape();
							return;
						}
					}
					else if (num != 3936939825U)
					{
						if (num == 4094167858U)
						{
							if (localName == "LineString")
							{
								this.ParseGmlLineStringShape();
								return;
							}
						}
					}
					else if (localName == "Point")
					{
						this.ParseGmlPointShape();
						return;
					}
					throw new FormatException(Strings.GmlReader_InvalidSpatialType(this.reader.LocalName));
				}
				if (this.reader.NamespaceURI == this.fullGlobeNamespace && this.reader.LocalName.Equals("FullGlobe"))
				{
					this.ReadAttributes(readCoordinateSystem);
					this.ParseGmlFullGlobeElement();
					return;
				}
				throw new FormatException(Strings.GmlReader_ExpectReaderAtElement);
			}

			// Token: 0x0600039A RID: 922 RVA: 0x00008830 File Offset: 0x00006A30
			private void ReadAttributes(bool expectSrsName)
			{
				bool flag = false;
				this.reader.MoveToContent();
				if (this.reader.MoveToFirstAttribute())
				{
					string localName;
					for (;;)
					{
						if (!this.reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/", StringComparison.Ordinal))
						{
							localName = this.reader.LocalName;
							if (!(localName == "axisLabels") && !(localName == "uomLabels") && !(localName == "count") && !(localName == "id"))
							{
								if (!(localName == "srsName"))
								{
									goto IL_010D;
								}
								if (!expectSrsName)
								{
									break;
								}
								string value = this.reader.Value;
								if (!value.StartsWith("http://www.opengis.net/def/crs/EPSG/0/", StringComparison.Ordinal))
								{
									goto IL_00FD;
								}
								int num = XmlConvert.ToInt32(value.Substring("http://www.opengis.net/def/crs/EPSG/0/".Length));
								this.pipeline.SetCoordinateSystem(new int?(num));
								flag = true;
							}
						}
						if (!this.reader.MoveToNextAttribute())
						{
							goto Block_9;
						}
					}
					this.reader.MoveToElement();
					throw new FormatException(Strings.GmlReader_InvalidAttribute(localName, this.reader.Name));
					IL_00FD:
					throw new FormatException(Strings.GmlReader_InvalidSrsName("http://www.opengis.net/def/crs/EPSG/0/"));
					IL_010D:
					this.reader.MoveToElement();
					throw new FormatException(Strings.GmlReader_InvalidAttribute(localName, this.reader.Name));
					Block_9:
					this.reader.MoveToElement();
				}
				if (expectSrsName && !flag)
				{
					this.pipeline.SetCoordinateSystem(null);
				}
			}

			// Token: 0x0600039B RID: 923 RVA: 0x000089A4 File Offset: 0x00006BA4
			private void ParseGmlPointShape()
			{
				this.pipeline.BeginGeo(SpatialType.Point);
				this.PrepareFigure();
				this.ParseGmlPointElement(true);
				this.EndFigure();
				this.pipeline.EndGeo();
			}

			// Token: 0x0600039C RID: 924 RVA: 0x000089D0 File Offset: 0x00006BD0
			private void ParseGmlLineStringShape()
			{
				this.pipeline.BeginGeo(SpatialType.LineString);
				this.PrepareFigure();
				this.ParseGmlLineString();
				this.EndFigure();
				this.pipeline.EndGeo();
			}

			// Token: 0x0600039D RID: 925 RVA: 0x000089FC File Offset: 0x00006BFC
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

			// Token: 0x0600039E RID: 926 RVA: 0x00008A95 File Offset: 0x00006C95
			private void ParseGmlMultiPointShape()
			{
				this.pipeline.BeginGeo(SpatialType.MultiPoint);
				this.ParseMultiItemElement("MultiPoint", "pointMember", "pointMembers", new Action(this.ParseGmlPointShape));
				this.pipeline.EndGeo();
			}

			// Token: 0x0600039F RID: 927 RVA: 0x00008ACF File Offset: 0x00006CCF
			private void ParseGmlMultiCurveShape()
			{
				this.pipeline.BeginGeo(SpatialType.MultiLineString);
				this.ParseMultiItemElement("MultiCurve", "curveMember", "curveMembers", new Action(this.ParseGmlLineStringShape));
				this.pipeline.EndGeo();
			}

			// Token: 0x060003A0 RID: 928 RVA: 0x00008B09 File Offset: 0x00006D09
			private void ParseGmlMultiSurfaceShape()
			{
				this.pipeline.BeginGeo(SpatialType.MultiPolygon);
				this.ParseMultiItemElement("MultiSurface", "surfaceMember", "surfaceMembers", new Action(this.ParseGmlPolygonShape));
				this.pipeline.EndGeo();
			}

			// Token: 0x060003A1 RID: 929 RVA: 0x00008B43 File Offset: 0x00006D43
			private void ParseGmlMultiGeometryShape()
			{
				this.pipeline.BeginGeo(SpatialType.Collection);
				this.ParseMultiItemElement("MultiGeometry", "geometryMember", "geometryMembers", delegate
				{
					this.ParseGmlGeometry(false);
				});
				this.pipeline.EndGeo();
			}

			// Token: 0x060003A2 RID: 930 RVA: 0x00008B7D File Offset: 0x00006D7D
			private void ParseGmlFullGlobeElement()
			{
				this.pipeline.BeginGeo(SpatialType.FullGlobe);
				if (this.ReadStartOrEmptyElement("FullGlobe") && this.IsEndElement("FullGlobe"))
				{
					this.ReadEndElement();
				}
				this.pipeline.EndGeo();
			}

			// Token: 0x060003A3 RID: 931 RVA: 0x00008BB7 File Offset: 0x00006DB7
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

			// Token: 0x060003A4 RID: 932 RVA: 0x00008BDF File Offset: 0x00006DDF
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

			// Token: 0x060003A5 RID: 933 RVA: 0x00008C18 File Offset: 0x00006E18
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

			// Token: 0x060003A6 RID: 934 RVA: 0x00008C38 File Offset: 0x00006E38
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

			// Token: 0x060003A7 RID: 935 RVA: 0x00008C88 File Offset: 0x00006E88
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
								parseItem();
								this.ReadEndElement();
								this.ReadSkippableElements();
							}
						}
						if (this.IsStartElement(members) && this.ReadStartOrEmptyElement(members))
						{
							while (this.reader.IsStartElement())
							{
								parseItem();
							}
							this.ReadEndElement();
						}
					}
					this.ReadSkippableElements();
					this.ReadEndElement();
				}
			}

			// Token: 0x060003A8 RID: 936 RVA: 0x00008D18 File Offset: 0x00006F18
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
						this.AddPoint(array[0], array[1], (array.Length > 2) ? new double?(array[2]) : null, (array.Length > 3) ? new double?(array[3]) : null);
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

			// Token: 0x060003A9 RID: 937 RVA: 0x00008DB8 File Offset: 0x00006FB8
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

			// Token: 0x060003AA RID: 938 RVA: 0x00008DDF File Offset: 0x00006FDF
			private void ParseGmlPointPropertyElement(bool allowEmpty)
			{
				if (this.ReadStartOrEmptyElement("pointProperty"))
				{
					this.ParseGmlPointElement(allowEmpty);
					this.ReadEndElement();
				}
			}

			// Token: 0x060003AB RID: 939 RVA: 0x00008DFC File Offset: 0x00006FFC
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
							this.AddPoint(array[i], array[i + 1], null, null);
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

			// Token: 0x060003AC RID: 940 RVA: 0x00008EA0 File Offset: 0x000070A0
			private double[] ReadContentAsDoubleArray()
			{
				string[] array = this.reader.ReadContentAsString().Split(GmlReader.Parser.coordinateDelimiter, StringSplitOptions.RemoveEmptyEntries);
				double[] array2 = new double[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = XmlConvert.ToDouble(array[i]);
				}
				return array2;
			}

			// Token: 0x060003AD RID: 941 RVA: 0x00008EE8 File Offset: 0x000070E8
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

			// Token: 0x060003AE RID: 942 RVA: 0x00008F37 File Offset: 0x00007137
			private bool IsStartElement(string element)
			{
				return this.reader.IsStartElement(element, this.gmlNamespace);
			}

			// Token: 0x060003AF RID: 943 RVA: 0x00008F4B File Offset: 0x0000714B
			private bool IsEndElement(string element)
			{
				this.reader.MoveToContent();
				return this.reader.NodeType == XmlNodeType.EndElement && this.reader.LocalName.Equals(element, StringComparison.Ordinal);
			}

			// Token: 0x060003B0 RID: 944 RVA: 0x00008F7C File Offset: 0x0000717C
			private void ReadEndElement()
			{
				this.reader.MoveToContent();
				if (this.reader.NodeType != XmlNodeType.EndElement)
				{
					throw new FormatException(Strings.GmlReader_UnexpectedElement(this.reader.Name));
				}
				this.reader.ReadEndElement();
			}

			// Token: 0x060003B1 RID: 945 RVA: 0x00008FBC File Offset: 0x000071BC
			private void ReadSkippableElements()
			{
				bool flag = true;
				while (flag)
				{
					this.reader.MoveToContent();
					if (this.reader.NodeType == XmlNodeType.Element && this.reader.NamespaceURI == this.gmlNamespace)
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

			// Token: 0x060003B2 RID: 946 RVA: 0x00009027 File Offset: 0x00007227
			private bool IsPosListStart()
			{
				return this.IsStartElement("pos") || this.IsStartElement("pointProperty");
			}

			// Token: 0x060003B3 RID: 947 RVA: 0x00009043 File Offset: 0x00007243
			private void PrepareFigure()
			{
				this.points = 0;
			}

			// Token: 0x060003B4 RID: 948 RVA: 0x0000904C File Offset: 0x0000724C
			private void AddPoint(double x, double y, double? z, double? m)
			{
				if (z != null && double.IsNaN(z.Value))
				{
					z = null;
				}
				if (m != null && double.IsNaN(m.Value))
				{
					m = null;
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

			// Token: 0x060003B5 RID: 949 RVA: 0x000090CF File Offset: 0x000072CF
			private void EndFigure()
			{
				if (this.points > 0)
				{
					this.pipeline.EndFigure();
				}
			}

			// Token: 0x04000135 RID: 309
			private static readonly char[] coordinateDelimiter = new char[] { ' ', '\t', '\r', '\n' };

			// Token: 0x04000136 RID: 310
			private static readonly Dictionary<string, string> skippableElements = new Dictionary<string, string>(StringComparer.Ordinal)
			{
				{ "name", "name" },
				{ "description", "description" },
				{ "metaDataProperty", "metaDataProperty" },
				{ "descriptionReference", "descriptionReference" },
				{ "identifier", "identifier" }
			};

			// Token: 0x04000137 RID: 311
			private readonly string gmlNamespace;

			// Token: 0x04000138 RID: 312
			private readonly string fullGlobeNamespace;

			// Token: 0x04000139 RID: 313
			private readonly TypeWashedPipeline pipeline;

			// Token: 0x0400013A RID: 314
			private readonly XmlReader reader;

			// Token: 0x0400013B RID: 315
			private int points;
		}
	}
}
