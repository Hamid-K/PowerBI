using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200003C RID: 60
	internal static class RdlReportItemConverterFactory
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x000085C4 File Offset: 0x000067C4
		internal static IRdlReportItemConverter CreateRdlReportItemConverter(string visualType)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(visualType);
			if (num <= 2872871853U)
			{
				if (num <= 1227570485U)
				{
					if (num <= 638284327U)
					{
						if (num != 139802751U)
						{
							if (num != 638284327U)
							{
								goto IL_039F;
							}
							if (!(visualType == "CategoryChart"))
							{
								goto IL_039F;
							}
							return new CategoryChartRdlReportItemConverter();
						}
						else
						{
							if (!(visualType == "Card"))
							{
								goto IL_039F;
							}
							return new CardRdlReportItemConverter();
						}
					}
					else if (num != 699101059U)
					{
						if (num != 1151856721U)
						{
							if (num != 1227570485U)
							{
								goto IL_039F;
							}
							if (!(visualType == "Sunburst"))
							{
								goto IL_039F;
							}
							return new CategoryChartRdlReportItemConverter();
						}
						else
						{
							if (!(visualType == "Map"))
							{
								goto IL_039F;
							}
							return new MapRdlReportItemConverter();
						}
					}
					else
					{
						if (!(visualType == "Play"))
						{
							goto IL_039F;
						}
						return new PlayRdlReportItemConverter();
					}
				}
				else if (num <= 1494001562U)
				{
					if (num != 1342272825U)
					{
						if (num != 1356523456U)
						{
							if (num != 1494001562U)
							{
								goto IL_039F;
							}
							if (!(visualType == "Image"))
							{
								goto IL_039F;
							}
							return new ImageRdlReportItemConverter();
						}
						else
						{
							if (!(visualType == "SmallMultiple"))
							{
								goto IL_039F;
							}
							return new SmallMultipleRdlReportItemConverter();
						}
					}
					else
					{
						if (!(visualType == "Slicer"))
						{
							goto IL_039F;
						}
						return new SlicerRdlReportItemConverter();
					}
				}
				else if (num != 2541007974U)
				{
					if (num != 2550723911U)
					{
						if (num != 2872871853U)
						{
							goto IL_039F;
						}
						if (!(visualType == "CanvasVisual"))
						{
							goto IL_039F;
						}
						return new SlideRdlReportItemConverter();
					}
					else
					{
						if (!(visualType == "Line"))
						{
							goto IL_039F;
						}
						return new CategoryChartRdlReportItemConverter();
					}
				}
				else if (!(visualType == "Band"))
				{
					goto IL_039F;
				}
			}
			else if (num <= 3477574229U)
			{
				if (num <= 3211044737U)
				{
					if (num != 3096863483U)
					{
						if (num != 3211044737U)
						{
							goto IL_039F;
						}
						if (!(visualType == "TreeMap"))
						{
							goto IL_039F;
						}
						return new CategoryChartRdlReportItemConverter();
					}
					else
					{
						if (!(visualType == "Textbox"))
						{
							goto IL_039F;
						}
						return new TextBoxRdlReportItemConverter();
					}
				}
				else if (num != 3293395595U)
				{
					if (num != 3344896287U)
					{
						if (num != 3477574229U)
						{
							goto IL_039F;
						}
						if (!(visualType == "Pie"))
						{
							goto IL_039F;
						}
						return new CategoryChartRdlReportItemConverter();
					}
					else
					{
						if (!(visualType == "DecisionTree"))
						{
							goto IL_039F;
						}
						return new DecisionTreeRdlReportItemConverter();
					}
				}
				else
				{
					if (!(visualType == "ChoroplethMap"))
					{
						goto IL_039F;
					}
					return new MapRdlReportItemConverter();
				}
			}
			else if (num <= 3922821388U)
			{
				if (num != 3607948159U)
				{
					if (num != 3783502617U)
					{
						if (num != 3922821388U)
						{
							goto IL_039F;
						}
						if (!(visualType == "Matrix"))
						{
							goto IL_039F;
						}
						return new MatrixRdlReportItemConverter();
					}
					else
					{
						if (!(visualType == "Funnel"))
						{
							goto IL_039F;
						}
						return new CategoryChartRdlReportItemConverter();
					}
				}
				else
				{
					if (!(visualType == "Table"))
					{
						goto IL_039F;
					}
					return new TableRdlReportItemConverter();
				}
			}
			else if (num != 4094025000U)
			{
				if (num != 4217405153U)
				{
					if (num != 4291281030U)
					{
						goto IL_039F;
					}
					if (!(visualType == "Slide"))
					{
						goto IL_039F;
					}
					return new SlideRdlReportItemConverter();
				}
				else
				{
					if (!(visualType == "Scatter"))
					{
						goto IL_039F;
					}
					return new ScatterChartRdlReportItemConverter();
				}
			}
			else if (!(visualType == "Subview"))
			{
				goto IL_039F;
			}
			return new TileRdlReportItemConverter();
			IL_039F:
			throw new InvalidOperationException("Invalid visual type");
		}
	}
}
