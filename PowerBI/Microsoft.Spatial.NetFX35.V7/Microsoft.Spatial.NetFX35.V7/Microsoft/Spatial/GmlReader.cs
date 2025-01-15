using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Spatial
{
	// Token: 0x0200005C RID: 92
	internal class GmlReader : SpatialReader<XmlReader>
	{
		// Token: 0x0600022F RID: 559 RVA: 0x000059F4 File Offset: 0x00003BF4
		public GmlReader(SpatialPipeline destination)
			: base(destination)
		{
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000059FD File Offset: 0x00003BFD
		protected override void ReadGeographyImplementation(XmlReader input)
		{
			new GmlReader.Parser(input, new TypeWashedToGeographyLatLongPipeline(base.Destination)).Read();
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00005A15 File Offset: 0x00003C15
		protected override void ReadGeometryImplementation(XmlReader input)
		{
			new GmlReader.Parser(input, new TypeWashedToGeometryPipeline(base.Destination)).Read();
		}

		// Token: 0x02000082 RID: 130
		private class Parser
		{
			// Token: 0x0600030F RID: 783 RVA: 0x000078CC File Offset: 0x00005ACC
			internal Parser(XmlReader reader, TypeWashedPipeline pipeline)
			{
				this.reader = reader;
				this.pipeline = pipeline;
				XmlNameTable nameTable = this.reader.NameTable;
				this.gmlNamespace = nameTable.Add("http://www.opengis.net/gml");
				this.fullGlobeNamespace = nameTable.Add("http://schemas.microsoft.com/sqlserver/2011/geography");
			}

			// Token: 0x06000310 RID: 784 RVA: 0x0000791B File Offset: 0x00005B1B
			public void Read()
			{
				this.ParseGmlGeometry(true);
			}

			// Token: 0x06000311 RID: 785 RVA: 0x00007924 File Offset: 0x00005B24
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

			// Token: 0x06000312 RID: 786 RVA: 0x00007ACC File Offset: 0x00005CCC
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
								if (!value.StartsWith("http://www.opengis.net/def/crs/EPSG/0/", 4))
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
					this.pipeline.SetCoordinateSystem(default(int?));
				}
			}

			// Token: 0x06000313 RID: 787 RVA: 0x00007C40 File Offset: 0x00005E40
			private void ParseGmlPointShape()
			{
				this.pipeline.BeginGeo(SpatialType.Point);
				this.PrepareFigure();
				this.ParseGmlPointElement(true);
				this.EndFigure();
				this.pipeline.EndGeo();
			}

			// Token: 0x06000314 RID: 788 RVA: 0x00007C6C File Offset: 0x00005E6C
			private void ParseGmlLineStringShape()
			{
				this.pipeline.BeginGeo(SpatialType.LineString);
				this.PrepareFigure();
				this.ParseGmlLineString();
				this.EndFigure();
				this.pipeline.EndGeo();
			}

			// Token: 0x06000315 RID: 789 RVA: 0x00007C98 File Offset: 0x00005E98
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

			// Token: 0x06000316 RID: 790 RVA: 0x00007D31 File Offset: 0x00005F31
			private void ParseGmlMultiPointShape()
			{
				this.pipeline.BeginGeo(SpatialType.MultiPoint);
				this.ParseMultiItemElement("MultiPoint", "pointMember", "pointMembers", new Action(this.ParseGmlPointShape));
				this.pipeline.EndGeo();
			}

			// Token: 0x06000317 RID: 791 RVA: 0x00007D6B File Offset: 0x00005F6B
			private void ParseGmlMultiCurveShape()
			{
				this.pipeline.BeginGeo(SpatialType.MultiLineString);
				this.ParseMultiItemElement("MultiCurve", "curveMember", "curveMembers", new Action(this.ParseGmlLineStringShape));
				this.pipeline.EndGeo();
			}

			// Token: 0x06000318 RID: 792 RVA: 0x00007DA5 File Offset: 0x00005FA5
			private void ParseGmlMultiSurfaceShape()
			{
				this.pipeline.BeginGeo(SpatialType.MultiPolygon);
				this.ParseMultiItemElement("MultiSurface", "surfaceMember", "surfaceMembers", new Action(this.ParseGmlPolygonShape));
				this.pipeline.EndGeo();
			}

			// Token: 0x06000319 RID: 793 RVA: 0x00007DDF File Offset: 0x00005FDF
			private void ParseGmlMultiGeometryShape()
			{
				this.pipeline.BeginGeo(SpatialType.Collection);
				this.ParseMultiItemElement("MultiGeometry", "geometryMember", "geometryMembers", delegate
				{
					this.ParseGmlGeometry(false);
				});
				this.pipeline.EndGeo();
			}

			// Token: 0x0600031A RID: 794 RVA: 0x00007E19 File Offset: 0x00006019
			private void ParseGmlFullGlobeElement()
			{
				this.pipeline.BeginGeo(SpatialType.FullGlobe);
				if (this.ReadStartOrEmptyElement("FullGlobe") && this.IsEndElement("FullGlobe"))
				{
					this.ReadEndElement();
				}
				this.pipeline.EndGeo();
			}

			// Token: 0x0600031B RID: 795 RVA: 0x00007E53 File Offset: 0x00006053
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

			// Token: 0x0600031C RID: 796 RVA: 0x00007E7B File Offset: 0x0000607B
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

			// Token: 0x0600031D RID: 797 RVA: 0x00007EB4 File Offset: 0x000060B4
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

			// Token: 0x0600031E RID: 798 RVA: 0x00007ED4 File Offset: 0x000060D4
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

			// Token: 0x0600031F RID: 799 RVA: 0x00007F24 File Offset: 0x00006124
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

			// Token: 0x06000320 RID: 800 RVA: 0x00007FB4 File Offset: 0x000061B4
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

			// Token: 0x06000321 RID: 801 RVA: 0x00008054 File Offset: 0x00006254
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

			// Token: 0x06000322 RID: 802 RVA: 0x0000807B File Offset: 0x0000627B
			private void ParseGmlPointPropertyElement(bool allowEmpty)
			{
				if (this.ReadStartOrEmptyElement("pointProperty"))
				{
					this.ParseGmlPointElement(allowEmpty);
					this.ReadEndElement();
				}
			}

			// Token: 0x06000323 RID: 803 RVA: 0x00008098 File Offset: 0x00006298
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

			// Token: 0x06000324 RID: 804 RVA: 0x0000813C File Offset: 0x0000633C
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

			// Token: 0x06000325 RID: 805 RVA: 0x00008184 File Offset: 0x00006384
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

			// Token: 0x06000326 RID: 806 RVA: 0x000081D3 File Offset: 0x000063D3
			private bool IsStartElement(string element)
			{
				return this.reader.IsStartElement(element, this.gmlNamespace);
			}

			// Token: 0x06000327 RID: 807 RVA: 0x000081E7 File Offset: 0x000063E7
			private bool IsEndElement(string element)
			{
				this.reader.MoveToContent();
				return this.reader.NodeType == 15 && this.reader.LocalName.Equals(element, 4);
			}

			// Token: 0x06000328 RID: 808 RVA: 0x00008218 File Offset: 0x00006418
			private void ReadEndElement()
			{
				this.reader.MoveToContent();
				if (this.reader.NodeType != 15)
				{
					throw new FormatException(Strings.GmlReader_UnexpectedElement(this.reader.Name));
				}
				this.reader.ReadEndElement();
			}

			// Token: 0x06000329 RID: 809 RVA: 0x00008258 File Offset: 0x00006458
			private void ReadSkippableElements()
			{
				bool flag = true;
				while (flag)
				{
					this.reader.MoveToContent();
					if (this.reader.NodeType == 1 && this.reader.NamespaceURI == this.gmlNamespace)
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

			// Token: 0x0600032A RID: 810 RVA: 0x000082C3 File Offset: 0x000064C3
			private bool IsPosListStart()
			{
				return this.IsStartElement("pos") || this.IsStartElement("pointProperty");
			}

			// Token: 0x0600032B RID: 811 RVA: 0x000082DF File Offset: 0x000064DF
			private void PrepareFigure()
			{
				this.points = 0;
			}

			// Token: 0x0600032C RID: 812 RVA: 0x000082E8 File Offset: 0x000064E8
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

			// Token: 0x0600032D RID: 813 RVA: 0x0000836B File Offset: 0x0000656B
			private void EndFigure()
			{
				if (this.points > 0)
				{
					this.pipeline.EndFigure();
				}
			}

			// Token: 0x0600032E RID: 814 RVA: 0x00008384 File Offset: 0x00006584
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

			// Token: 0x04000119 RID: 281
			private static readonly char[] coordinateDelimiter = new char[] { ' ', '\t', '\r', '\n' };

			// Token: 0x0400011A RID: 282
			private static readonly Dictionary<string, string> skippableElements;

			// Token: 0x0400011B RID: 283
			private readonly string gmlNamespace;

			// Token: 0x0400011C RID: 284
			private readonly string fullGlobeNamespace;

			// Token: 0x0400011D RID: 285
			private readonly TypeWashedPipeline pipeline;

			// Token: 0x0400011E RID: 286
			private readonly XmlReader reader;

			// Token: 0x0400011F RID: 287
			private int points;
		}
	}
}
