using System;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Ink;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02002150 RID: 8528
	internal static class O12UnionHelper
	{
		// Token: 0x0600D3EE RID: 54254 RVA: 0x002A1CC0 File Offset: 0x0029FEC0
		internal static OpenXmlSimpleType[] CreatePossibleMembers(UnionValueRestriction unionValueRestriction)
		{
			OpenXmlSimpleType[] array = new OpenXmlSimpleType[unionValueRestriction.UnionTypes.Length];
			int unionId = unionValueRestriction.UnionId;
			if (unionId <= 533)
			{
				if (unionId <= 368)
				{
					if (unionId <= 145)
					{
						switch (unionId)
						{
						case 25:
							array[0] = new EnumValue<AnimationBuildValues>();
							array[1] = new EnumValue<AnimationDiagramOnlyBuildValues>();
							break;
						case 26:
							break;
						case 27:
							array[0] = new EnumValue<AnimationBuildValues>();
							array[1] = new EnumValue<AnimationChartOnlyBuildValues>();
							break;
						default:
							switch (unionId)
							{
							case 45:
								array[0] = new Int64Value();
								array[1] = new StringValue();
								break;
							case 46:
								array[0] = new Int32Value();
								array[1] = new StringValue();
								break;
							default:
								if (unionId == 145)
								{
									array[0] = new EnumValue<ShapeTypeValues>();
									array[1] = new EnumValue<OutputShapeValues>();
								}
								break;
							}
							break;
						}
					}
					else
					{
						switch (unionId)
						{
						case 183:
							array[0] = new EnumValue<DocumentFormat.OpenXml.Drawing.Diagrams.HorizontalAlignmentValues>();
							array[1] = new EnumValue<DocumentFormat.OpenXml.Drawing.Diagrams.VerticalAlignmentValues>();
							array[2] = new EnumValue<ChildDirectionValues>();
							array[3] = new EnumValue<ChildAlignmentValues>();
							array[4] = new EnumValue<SecondaryChildAlignmentValues>();
							array[5] = new EnumValue<LinearDirectionValues>();
							array[6] = new EnumValue<SecondaryLinearDirectionValues>();
							array[7] = new EnumValue<StartingElementValues>();
							array[8] = new EnumValue<BendPointValues>();
							array[9] = new EnumValue<ConnectorRoutingValues>();
							array[10] = new EnumValue<ArrowheadStyleValues>();
							array[11] = new EnumValue<ConnectorDimensionValues>();
							array[12] = new EnumValue<RotationPathValues>();
							array[13] = new EnumValue<CenterShapeMappingValues>();
							array[14] = new EnumValue<NodeHorizontalAlignmentValues>();
							array[15] = new EnumValue<NodeVerticalAlignmentValues>();
							array[16] = new EnumValue<FallbackDimensionValues>();
							array[17] = new EnumValue<DocumentFormat.OpenXml.Drawing.Diagrams.TextDirectionValues>();
							array[18] = new EnumValue<PyramidAccentPositionValues>();
							array[19] = new EnumValue<PyramidAccentTextMarginValues>();
							array[20] = new EnumValue<TextBlockDirectionValues>();
							array[21] = new EnumValue<TextAnchorHorizontalValues>();
							array[22] = new EnumValue<TextAnchorVerticalValues>();
							array[23] = new EnumValue<TextAlignmentValues>();
							array[24] = new EnumValue<AutoTextRotationValues>();
							array[25] = new EnumValue<GrowDirectionValues>();
							array[26] = new EnumValue<FlowDirectionValues>();
							array[27] = new EnumValue<ContinueDirectionValues>();
							array[28] = new EnumValue<BreakpointValues>();
							array[29] = new EnumValue<OffsetValues>();
							array[30] = new EnumValue<HierarchyAlignmentValues>();
							array[31] = new Int32Value();
							array[32] = new DoubleValue();
							array[33] = new BooleanValue();
							array[34] = new StringValue();
							array[35] = new EnumValue<ConnectorPointValues>();
							break;
						case 184:
							array[0] = new Int32Value();
							array[1] = new StringValue();
							break;
						default:
							switch (unionId)
							{
							case 207:
								array[0] = new Int32Value();
								array[1] = new BooleanValue();
								array[2] = new EnumValue<DocumentFormat.OpenXml.Drawing.Diagrams.DirectionValues>();
								array[3] = new EnumValue<HierarchyBranchStyleValues>();
								array[4] = new EnumValue<AnimateOneByOneValues>();
								array[5] = new EnumValue<AnimationLevelStringValues>();
								array[6] = new EnumValue<ResizeHandlesStringValues>();
								break;
							case 208:
								break;
							case 209:
								array[0] = new EnumValue<VariableValues>();
								break;
							default:
								if (unionId == 368)
								{
									array[0] = new Int32Value();
									array[1] = new Int32Value();
								}
								break;
							}
							break;
						}
					}
				}
				else if (unionId <= 508)
				{
					if (unionId != 375)
					{
						if (unionId != 405)
						{
							if (unionId == 508)
							{
								array[0] = new StringValue();
								array[1] = new Int32Value();
							}
						}
						else
						{
							array[0] = new EnumValue<AutomaticColorValues>();
							array[1] = new HexBinaryValue();
						}
					}
					else
					{
						array[0] = new Int32Value();
						array[1] = new Int32Value();
					}
				}
				else if (unionId != 527)
				{
					if (unionId != 530)
					{
						if (unionId == 533)
						{
							array[0] = new IntegerValue();
							array[1] = new StringValue();
						}
					}
					else
					{
						array[0] = new UInt32Value();
						array[1] = new StringValue();
					}
				}
				else
				{
					array[0] = new IntegerValue();
					array[1] = new StringValue();
				}
			}
			else if (unionId <= 694)
			{
				if (unionId <= 558)
				{
					if (unionId != 536)
					{
						if (unionId != 541)
						{
							if (unionId == 558)
							{
								array[0] = new Int32Value();
								array[1] = new EnumValue<IndefiniteTimeDeclarationValues>();
							}
						}
						else
						{
							array[0] = new EnumValue<TransitionSlideDirectionValues>();
							array[1] = new EnumValue<TransitionCornerDirectionValues>();
						}
					}
					else
					{
						array[0] = new UInt32Value();
						array[1] = new StringValue();
					}
				}
				else
				{
					switch (unionId)
					{
					case 601:
						array[0] = new UInt32Value();
						array[1] = new EnumValue<IndefiniteTimeDeclarationValues>();
						break;
					case 602:
						array[0] = new StringValue();
						array[1] = new EnumValue<IndefiniteTimeDeclarationValues>();
						break;
					default:
						if (unionId != 686)
						{
							if (unionId == 694)
							{
								array[0] = new DecimalValue();
								array[1] = new BooleanValue();
							}
						}
						else
						{
							array[0] = new DateTimeValue();
							array[1] = new DateTimeValue();
							array[2] = new StringValue();
						}
						break;
					}
				}
			}
			else if (unionId <= 704)
			{
				if (unionId != 698)
				{
					if (unionId != 701)
					{
						if (unionId == 704)
						{
							array[0] = new EnumValue<StandardChannelNameValues>();
							array[1] = new StringValue();
						}
					}
					else
					{
						array[0] = new EnumValue<StandardBrushPropertyNameValues>();
						array[1] = new StringValue();
					}
				}
				else
				{
					array[0] = new EnumValue<StandardChannelPropertyNameValues>();
					array[1] = new StringValue();
				}
			}
			else if (unionId <= 729)
			{
				if (unionId != 716)
				{
					if (unionId == 729)
					{
						array[0] = new DecimalValue();
						array[1] = new BooleanValue();
						array[2] = new EnumValue<PenTipShapeValues>();
						array[3] = new EnumValue<RasterOperationValues>();
						array[4] = new StringValue();
					}
				}
				else
				{
					array[0] = new EnumValue<StandardLengthUnitsValues>();
					array[1] = new EnumValue<StandardPerLengthUnitsValues>();
					array[2] = new EnumValue<StandardTimeUnitsValues>();
					array[3] = new EnumValue<StandardPerTimeUnitsValues>();
					array[4] = new EnumValue<StandardMassForceUnitsValues>();
					array[5] = new EnumValue<StandardPerMassForceUnitsValues>();
					array[6] = new EnumValue<StandardAngleUnitsValues>();
					array[7] = new EnumValue<StandardPerAngleUnitsValues>();
					array[8] = new EnumValue<StandardOtherUnitsValues>();
					array[9] = new EnumValue<StandardPerOtherUnitsValues>();
					array[10] = new StringValue();
				}
			}
			else
			{
				switch (unionId)
				{
				case 743:
					array[0] = new StringValue();
					array[1] = new UInt32Value();
					break;
				case 744:
					array[0] = new EnumValue<KnownContextNodeTypeValues>();
					array[1] = new StringValue();
					break;
				case 745:
				case 746:
					break;
				case 747:
					array[0] = new EnumValue<KnownSemanticTypeValues>();
					array[1] = new UInt32Value();
					break;
				default:
					if (unionId == 750)
					{
						array[0] = new ListValue<StringValue>();
						array[1] = new Int32Value();
					}
					break;
				}
			}
			return array;
		}

		// Token: 0x0600D3EF RID: 54255 RVA: 0x002A22EC File Offset: 0x002A04EC
		internal static OpenXmlSimpleType CreateTargetValueObject(RedirectedRestriction redirectedRestriction)
		{
			OpenXmlSimpleType openXmlSimpleType = null;
			int attributeId = redirectedRestriction.AttributeId;
			if (attributeId <= 2709)
			{
				if (attributeId <= 2436)
				{
					if (attributeId <= 2380)
					{
						switch (attributeId)
						{
						case 2358:
							openXmlSimpleType = new UInt32Value();
							break;
						case 2359:
							break;
						case 2360:
							openXmlSimpleType = new UInt32Value();
							break;
						case 2361:
							openXmlSimpleType = new UInt32Value();
							break;
						default:
							switch (attributeId)
							{
							case 2365:
								openXmlSimpleType = new Int32Value();
								break;
							case 2366:
								break;
							case 2367:
								openXmlSimpleType = new Int32Value();
								break;
							default:
								if (attributeId == 2380)
								{
									openXmlSimpleType = new UInt32Value();
								}
								break;
							}
							break;
						}
					}
					else if (attributeId <= 2398)
					{
						switch (attributeId)
						{
						case 2383:
							openXmlSimpleType = new UInt32Value();
							break;
						case 2384:
						case 2385:
						case 2387:
						case 2390:
						case 2391:
							break;
						case 2386:
							openXmlSimpleType = new Int32Value();
							break;
						case 2388:
							openXmlSimpleType = new Int32Value();
							break;
						case 2389:
							openXmlSimpleType = new Int32Value();
							break;
						case 2392:
							openXmlSimpleType = new Int32Value();
							break;
						case 2393:
							openXmlSimpleType = new Int32Value();
							break;
						default:
							switch (attributeId)
							{
							case 2396:
								openXmlSimpleType = new UInt32Value();
								break;
							case 2398:
								openXmlSimpleType = new UInt32Value();
								break;
							}
							break;
						}
					}
					else if (attributeId != 2429)
					{
						if (attributeId == 2436)
						{
							openXmlSimpleType = new UInt32Value();
						}
					}
					else
					{
						openXmlSimpleType = new UInt32Value();
					}
				}
				else if (attributeId <= 2595)
				{
					switch (attributeId)
					{
					case 2449:
						openXmlSimpleType = new UInt32Value();
						break;
					case 2450:
						openXmlSimpleType = new UInt32Value();
						break;
					default:
						switch (attributeId)
						{
						case 2478:
							openXmlSimpleType = new Int32Value();
							break;
						case 2479:
							openXmlSimpleType = new UInt32Value();
							break;
						default:
							if (attributeId == 2595)
							{
								openXmlSimpleType = new Int32Value();
							}
							break;
						}
						break;
					}
				}
				else if (attributeId <= 2659)
				{
					if (attributeId != 2652)
					{
						switch (attributeId)
						{
						case 2658:
							openXmlSimpleType = new UInt32Value();
							break;
						case 2659:
							openXmlSimpleType = new UInt32Value();
							break;
						}
					}
					else
					{
						openXmlSimpleType = new StringValue();
					}
				}
				else if (attributeId != 2679)
				{
					if (attributeId == 2709)
					{
						openXmlSimpleType = new UInt32Value();
					}
				}
				else
				{
					openXmlSimpleType = new UInt32Value();
				}
			}
			else if (attributeId <= 2958)
			{
				if (attributeId <= 2807)
				{
					switch (attributeId)
					{
					case 2715:
						openXmlSimpleType = new UInt32Value();
						break;
					case 2716:
						openXmlSimpleType = new Int32Value();
						break;
					default:
						if (attributeId != 2760)
						{
							if (attributeId == 2807)
							{
								openXmlSimpleType = new Int32Value();
							}
						}
						else
						{
							openXmlSimpleType = new Int32Value();
						}
						break;
					}
				}
				else if (attributeId <= 2884)
				{
					if (attributeId != 2832)
					{
						if (attributeId == 2884)
						{
							openXmlSimpleType = new Int32Value();
						}
					}
					else
					{
						openXmlSimpleType = new HexBinaryValue();
					}
				}
				else if (attributeId != 2953)
				{
					if (attributeId == 2958)
					{
						openXmlSimpleType = new StringValue();
					}
				}
				else
				{
					openXmlSimpleType = new StringValue();
				}
			}
			else if (attributeId <= 3046)
			{
				switch (attributeId)
				{
				case 2992:
					openXmlSimpleType = new StringValue();
					break;
				case 2993:
					openXmlSimpleType = new StringValue();
					break;
				case 2994:
					openXmlSimpleType = new StringValue();
					break;
				default:
					if (attributeId != 3037)
					{
						switch (attributeId)
						{
						case 3045:
							openXmlSimpleType = new StringValue();
							break;
						case 3046:
							openXmlSimpleType = new StringValue();
							break;
						}
					}
					else
					{
						openXmlSimpleType = new StringValue();
					}
					break;
				}
			}
			else if (attributeId <= 3055)
			{
				if (attributeId != 3050)
				{
					if (attributeId == 3055)
					{
						openXmlSimpleType = new StringValue();
					}
				}
				else
				{
					openXmlSimpleType = new StringValue();
				}
			}
			else if (attributeId != 3091)
			{
				if (attributeId == 3185)
				{
					openXmlSimpleType = new StringValue();
				}
			}
			else
			{
				openXmlSimpleType = new UInt32Value();
			}
			return openXmlSimpleType;
		}
	}
}
