using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000132 RID: 306
	internal abstract class DaxFunctionTransform
	{
		// Token: 0x06001122 RID: 4386 RVA: 0x0002EEDC File Offset: 0x0002D0DC
		internal static DaxFunctionTransform Create(QueryFunctionExpression expression, DaxTransform daxTransform)
		{
			DaxFunctionTransform daxFunctionTransform = DaxFunctionTransform.CreateCore(expression.Function.FullName, expression.Arguments.Count);
			daxFunctionTransform.Init(expression, daxTransform);
			return daxFunctionTransform;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0002EF04 File Offset: 0x0002D104
		private static DaxFunctionTransform CreateCore(string functionName, int argumentsCount)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(functionName);
			if (num <= 1563765191U)
			{
				if (num <= 1215433716U)
				{
					if (num <= 335576869U)
					{
						if (num <= 102308832U)
						{
							if (num != 47960491U)
							{
								if (num == 102308832U)
								{
									if (functionName == "Core.Multiply")
									{
										Func<DaxExpression, DaxExpression, DaxExpression> func;
										if ((func = DaxFunctionTransform.<>O.<4>__Multiply) == null)
										{
											func = (DaxFunctionTransform.<>O.<4>__Multiply = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxOperators.Multiply));
										}
										return new DaxFunctionTransforms.BinaryFunctionTransform(func);
									}
								}
							}
							else if (functionName == "Core.Sum")
							{
								Func<DaxColumnRef, DaxExpression> func2;
								if ((func2 = DaxFunctionTransform.<>O.<9>__Sum) == null)
								{
									func2 = (DaxFunctionTransform.<>O.<9>__Sum = new Func<DaxColumnRef, DaxExpression>(DaxFunctions.Sum));
								}
								Func<DaxExpression, DaxExpression, DaxExpression> func3;
								if ((func3 = DaxFunctionTransform.<>O.<10>__SumX) == null)
								{
									func3 = (DaxFunctionTransform.<>O.<10>__SumX = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.SumX));
								}
								return new DaxFunctionTransforms.AggregateFunctionTransform(func2, func3);
							}
						}
						else if (num != 251886180U)
						{
							if (num != 326939756U)
							{
								if (num == 335576869U)
								{
									if (functionName == "Core.MinValue")
									{
										Func<DaxExpression, DaxExpression, DaxExpression> func4;
										if ((func4 = DaxFunctionTransform.<>O.<47>__Min) == null)
										{
											func4 = (DaxFunctionTransform.<>O.<47>__Min = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.Min));
										}
										return new DaxFunctionTransforms.BinaryFunctionTransform(func4);
									}
								}
							}
							else if (functionName == "Core.Min")
							{
								Func<DaxColumnRef, DaxExpression> func5;
								if ((func5 = DaxFunctionTransform.<>O.<21>__Min) == null)
								{
									func5 = (DaxFunctionTransform.<>O.<21>__Min = new Func<DaxColumnRef, DaxExpression>(DaxFunctions.Min));
								}
								Func<DaxExpression, DaxExpression, DaxExpression> func6;
								if ((func6 = DaxFunctionTransform.<>O.<22>__MinX) == null)
								{
									func6 = (DaxFunctionTransform.<>O.<22>__MinX = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.MinX));
								}
								return new DaxFunctionTransforms.MinMaxFunctionTransform(func5, func6);
							}
						}
						else if (functionName == "Core.Date")
						{
							Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression> func7;
							if ((func7 = DaxFunctionTransform.<>O.<32>__Date) == null)
							{
								func7 = (DaxFunctionTransform.<>O.<32>__Date = new Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.Date));
							}
							return new DaxFunctionTransforms.TriaryFunctionTransform(func7);
						}
					}
					else if (num <= 507970719U)
					{
						if (num != 408408107U)
						{
							if (num != 456457951U)
							{
								if (num == 507970719U)
								{
									if (functionName == "Core.Ceiling")
									{
										Func<DaxExpression, DaxExpression, DaxExpression> func8;
										if ((func8 = DaxFunctionTransform.<>O.<44>__Ceiling) == null)
										{
											func8 = (DaxFunctionTransform.<>O.<44>__Ceiling = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.Ceiling));
										}
										return new DaxFunctionTransforms.BinaryFunctionTransform(func8);
									}
								}
							}
							else if (functionName == "Core.MaxValue")
							{
								Func<DaxExpression, DaxExpression, DaxExpression> func9;
								if ((func9 = DaxFunctionTransform.<>O.<48>__Max) == null)
								{
									func9 = (DaxFunctionTransform.<>O.<48>__Max = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.Max));
								}
								return new DaxFunctionTransforms.BinaryFunctionTransform(func9);
							}
						}
						else if (functionName == "Core.EDate")
						{
							Func<DaxExpression, DaxExpression, DaxExpression> func10;
							if ((func10 = DaxFunctionTransform.<>O.<39>__EDate) == null)
							{
								func10 = (DaxFunctionTransform.<>O.<39>__EDate = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.EDate));
							}
							return new DaxFunctionTransforms.BinaryFunctionTransform(func10);
						}
					}
					else if (num != 560443326U)
					{
						if (num != 1107063600U)
						{
							if (num == 1215433716U)
							{
								if (functionName == "Core.RoundDown")
								{
									Func<DaxExpression, DaxExpression, DaxExpression> func11;
									if ((func11 = DaxFunctionTransform.<>O.<50>__RoundDown) == null)
									{
										func11 = (DaxFunctionTransform.<>O.<50>__RoundDown = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.RoundDown));
									}
									return new DaxFunctionTransforms.BinaryFunctionTransform(func11);
								}
							}
						}
						else if (functionName == "Core.Between")
						{
							Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression> func12;
							if ((func12 = DaxFunctionTransform.<>O.<43>__InvokeDaxBetween) == null)
							{
								func12 = (DaxFunctionTransform.<>O.<43>__InvokeDaxBetween = new Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression>(DaxFunctionTransforms.ScalarFunctionTransformUtil.InvokeDaxBetween));
							}
							return new DaxFunctionTransforms.TriaryFunctionTransform(func12);
						}
					}
					else if (functionName == "Core.Max")
					{
						Func<DaxColumnRef, DaxExpression> func13;
						if ((func13 = DaxFunctionTransform.<>O.<23>__Max) == null)
						{
							func13 = (DaxFunctionTransform.<>O.<23>__Max = new Func<DaxColumnRef, DaxExpression>(DaxFunctions.Max));
						}
						Func<DaxExpression, DaxExpression, DaxExpression> func14;
						if ((func14 = DaxFunctionTransform.<>O.<24>__MaxX) == null)
						{
							func14 = (DaxFunctionTransform.<>O.<24>__MaxX = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.MaxX));
						}
						return new DaxFunctionTransforms.MinMaxFunctionTransform(func13, func14);
					}
				}
				else if (num <= 1382488607U)
				{
					if (num <= 1317937847U)
					{
						if (num != 1241663689U)
						{
							if (num == 1317937847U)
							{
								if (functionName == "Core.Average")
								{
									Func<DaxColumnRef, DaxExpression> func15;
									if ((func15 = DaxFunctionTransform.<>O.<19>__Average) == null)
									{
										func15 = (DaxFunctionTransform.<>O.<19>__Average = new Func<DaxColumnRef, DaxExpression>(DaxFunctions.Average));
									}
									Func<DaxExpression, DaxExpression, DaxExpression> func16;
									if ((func16 = DaxFunctionTransform.<>O.<20>__AverageX) == null)
									{
										func16 = (DaxFunctionTransform.<>O.<20>__AverageX = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.AverageX));
									}
									return new DaxFunctionTransforms.AggregateFunctionTransform(func15, func16);
								}
							}
						}
						else if (functionName == "Core.Power")
						{
							Func<DaxExpression, DaxExpression, DaxExpression> func17;
							if ((func17 = DaxFunctionTransform.<>O.<53>__Power) == null)
							{
								func17 = (DaxFunctionTransform.<>O.<53>__Power = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.Power));
							}
							return new DaxFunctionTransforms.BinaryFunctionTransform(func17);
						}
					}
					else if (num != 1320056494U)
					{
						if (num != 1363419501U)
						{
							if (num == 1382488607U)
							{
								if (functionName == "Core.Count")
								{
									Func<DaxColumnRef, DaxExpression> func18;
									if ((func18 = DaxFunctionTransform.<>O.<17>__CountA) == null)
									{
										func18 = (DaxFunctionTransform.<>O.<17>__CountA = new Func<DaxColumnRef, DaxExpression>(DaxFunctions.CountA));
									}
									Func<DaxExpression, DaxExpression, DaxExpression> func19;
									if ((func19 = DaxFunctionTransform.<>O.<18>__CountAX) == null)
									{
										func19 = (DaxFunctionTransform.<>O.<18>__CountAX = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.CountAX));
									}
									return new DaxFunctionTransforms.AggregateFunctionTransform(func18, func19);
								}
							}
						}
						else if (functionName == "Core.Or")
						{
							Func<DaxExpression, DaxExpression, DaxExpression> func20;
							if ((func20 = DaxFunctionTransform.<>O.<0>__Or) == null)
							{
								func20 = (DaxFunctionTransform.<>O.<0>__Or = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.Or));
							}
							return new DaxFunctionTransforms.BinaryFunctionTransform(func20);
						}
					}
					else if (functionName == "Core.Median")
					{
						Func<DaxColumnRef, DaxExpression> func21;
						if ((func21 = DaxFunctionTransform.<>O.<11>__Median) == null)
						{
							func21 = (DaxFunctionTransform.<>O.<11>__Median = new Func<DaxColumnRef, DaxExpression>(DaxFunctions.Median));
						}
						Func<DaxExpression, DaxExpression, DaxExpression> func22;
						if ((func22 = DaxFunctionTransform.<>O.<12>__MedianX) == null)
						{
							func22 = (DaxFunctionTransform.<>O.<12>__MedianX = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.MedianX));
						}
						return new DaxFunctionTransforms.AggregateFunctionTransform(func21, func22);
					}
				}
				else if (num <= 1439597094U)
				{
					if (num != 1408978916U)
					{
						if (num != 1427601766U)
						{
							if (num == 1439597094U)
							{
								if (functionName == "Core.Hour")
								{
									Func<DaxExpression, DaxExpression> func23;
									if ((func23 = DaxFunctionTransform.<>O.<36>__Hour) == null)
									{
										func23 = (DaxFunctionTransform.<>O.<36>__Hour = new Func<DaxExpression, DaxExpression>(DaxFunctions.Hour));
									}
									return new DaxFunctionTransforms.UnaryFunctionTransform(func23);
								}
							}
						}
						else if (functionName == "Core.Floor")
						{
							Func<DaxExpression, DaxExpression, DaxExpression> func24;
							if ((func24 = DaxFunctionTransform.<>O.<45>__Floor) == null)
							{
								func24 = (DaxFunctionTransform.<>O.<45>__Floor = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.Floor));
							}
							return new DaxFunctionTransforms.BinaryFunctionTransform(func24);
						}
					}
					else if (functionName == "Core.StandardDeviation")
					{
						Func<DaxColumnRef, DaxExpression> func25;
						if ((func25 = DaxFunctionTransform.<>O.<13>__StdevP) == null)
						{
							func25 = (DaxFunctionTransform.<>O.<13>__StdevP = new Func<DaxColumnRef, DaxExpression>(DaxFunctions.StdevP));
						}
						Func<DaxExpression, DaxExpression, DaxExpression> func26;
						if ((func26 = DaxFunctionTransform.<>O.<14>__StdevPX) == null)
						{
							func26 = (DaxFunctionTransform.<>O.<14>__StdevPX = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.StdevPX));
						}
						return new DaxFunctionTransforms.AggregateFunctionTransform(func25, func26);
					}
				}
				else if (num != 1462466552U)
				{
					if (num != 1468906554U)
					{
						if (num == 1563765191U)
						{
							if (functionName == "Core.If")
							{
								if (argumentsCount == 2)
								{
									Func<DaxExpression, DaxExpression, DaxExpression> func27;
									if ((func27 = DaxFunctionTransform.<>O.<40>__If) == null)
									{
										func27 = (DaxFunctionTransform.<>O.<40>__If = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.If));
									}
									return new DaxFunctionTransforms.BinaryFunctionTransform(func27);
								}
								Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression> func28;
								if ((func28 = DaxFunctionTransform.<>O.<41>__If) == null)
								{
									func28 = (DaxFunctionTransform.<>O.<41>__If = new Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.If));
								}
								return new DaxFunctionTransforms.TriaryFunctionTransform(func28);
							}
						}
					}
					else if (functionName == "Core.Day")
					{
						Func<DaxExpression, DaxExpression> func29;
						if ((func29 = DaxFunctionTransform.<>O.<35>__Day) == null)
						{
							func29 = (DaxFunctionTransform.<>O.<35>__Day = new Func<DaxExpression, DaxExpression>(DaxFunctions.Day));
						}
						return new DaxFunctionTransforms.UnaryFunctionTransform(func29);
					}
				}
				else if (functionName == "Core.Minus")
				{
					Func<DaxExpression, DaxExpression, DaxExpression> func30;
					if ((func30 = DaxFunctionTransform.<>O.<2>__Minus) == null)
					{
						func30 = (DaxFunctionTransform.<>O.<2>__Minus = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxOperators.Minus));
					}
					return new DaxFunctionTransforms.BinaryFunctionTransform(func30);
				}
			}
			else if (num <= 2621818657U)
			{
				if (num <= 1943976617U)
				{
					if (num <= 1618336190U)
					{
						if (num != 1613495025U)
						{
							if (num == 1618336190U)
							{
								if (functionName == "Core.EndsWith")
								{
									Func<DaxExpression, DaxExpression, DaxExpression> func31;
									if ((func31 = DaxFunctionTransform.<>O.<30>__InvokeEndsWith) == null)
									{
										func31 = (DaxFunctionTransform.<>O.<30>__InvokeEndsWith = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctionTransforms.TextFunctionTransformUtil.InvokeEndsWith));
									}
									return new DaxFunctionTransforms.BinaryFunctionTransform(func31);
								}
							}
						}
						else if (functionName == "Core.StartsWith")
						{
							Func<DaxExpression, DaxExpression, DaxExpression> func32;
							if ((func32 = DaxFunctionTransform.<>O.<31>__InvokeStartsWith) == null)
							{
								func32 = (DaxFunctionTransform.<>O.<31>__InvokeStartsWith = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctionTransforms.TextFunctionTransformUtil.InvokeStartsWith));
							}
							return new DaxFunctionTransforms.BinaryFunctionTransform(func32);
						}
					}
					else if (num != 1713354281U)
					{
						if (num != 1814483020U)
						{
							if (num == 1943976617U)
							{
								if (functionName == "Core.MinusSign")
								{
									Func<DaxExpression, DaxExpression> func33;
									if ((func33 = DaxFunctionTransform.<>O.<3>__MinusSign) == null)
									{
										func33 = (DaxFunctionTransform.<>O.<3>__MinusSign = new Func<DaxExpression, DaxExpression>(DaxOperators.MinusSign));
									}
									return new DaxFunctionTransforms.UnaryFunctionTransform(func33);
								}
							}
						}
						else if (functionName == "Core.Second")
						{
							Func<DaxExpression, DaxExpression> func34;
							if ((func34 = DaxFunctionTransform.<>O.<38>__Second) == null)
							{
								func34 = (DaxFunctionTransform.<>O.<38>__Second = new Func<DaxExpression, DaxExpression>(DaxFunctions.Second));
							}
							return new DaxFunctionTransforms.UnaryFunctionTransform(func34);
						}
					}
					else if (functionName == "Core.Contains")
					{
						Func<DaxExpression, DaxExpression, DaxExpression> func35;
						if ((func35 = DaxFunctionTransform.<>O.<29>__InvokeContains) == null)
						{
							func35 = (DaxFunctionTransform.<>O.<29>__InvokeContains = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctionTransforms.TextFunctionTransformUtil.InvokeContains));
						}
						return new DaxFunctionTransforms.BinaryFunctionTransform(func35);
					}
				}
				else if (num <= 2490518351U)
				{
					if (num != 1985017657U)
					{
						if (num != 2107455041U)
						{
							if (num == 2490518351U)
							{
								if (functionName == "Core.DistinctCount")
								{
									return new DaxFunctionTransforms.DistinctCountFunctionTransform();
								}
							}
						}
						else if (functionName == "Core.Int")
						{
							Func<DaxExpression, DaxExpression> func36;
							if ((func36 = DaxFunctionTransform.<>O.<49>__Int) == null)
							{
								func36 = (DaxFunctionTransform.<>O.<49>__Int = new Func<DaxExpression, DaxExpression>(DaxFunctions.Int));
							}
							return new DaxFunctionTransforms.UnaryFunctionTransform(func36);
						}
					}
					else if (functionName == "Core.Year")
					{
						Func<DaxExpression, DaxExpression> func37;
						if ((func37 = DaxFunctionTransform.<>O.<33>__Year) == null)
						{
							func37 = (DaxFunctionTransform.<>O.<33>__Year = new Func<DaxExpression, DaxExpression>(DaxFunctions.Year));
						}
						return new DaxFunctionTransforms.UnaryFunctionTransform(func37);
					}
				}
				else if (num != 2540548574U)
				{
					if (num != 2574418665U)
					{
						if (num == 2621818657U)
						{
							if (functionName == "Core.Not")
							{
								Func<DaxExpression, DaxExpression> func38;
								if ((func38 = DaxFunctionTransform.<>O.<8>__Not) == null)
								{
									func38 = (DaxFunctionTransform.<>O.<8>__Not = new Func<DaxExpression, DaxExpression>(DaxFunctions.Not));
								}
								return new DaxFunctionTransforms.UnaryFunctionTransform(func38);
							}
						}
					}
					else if (functionName == "Core.Divide")
					{
						if (argumentsCount == 2)
						{
							Func<DaxExpression, DaxExpression, DaxExpression> func39;
							if ((func39 = DaxFunctionTransform.<>O.<6>__Divide) == null)
							{
								func39 = (DaxFunctionTransform.<>O.<6>__Divide = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxOperators.Divide));
							}
							return new DaxFunctionTransforms.BinaryFunctionTransform(func39);
						}
						Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression> func40;
						if ((func40 = DaxFunctionTransform.<>O.<7>__Divide) == null)
						{
							func40 = (DaxFunctionTransform.<>O.<7>__Divide = new Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.Divide));
						}
						return new DaxFunctionTransforms.TriaryFunctionTransform(func40);
					}
				}
				else if (functionName == "Core.Month")
				{
					Func<DaxExpression, DaxExpression> func41;
					if ((func41 = DaxFunctionTransform.<>O.<34>__Month) == null)
					{
						func41 = (DaxFunctionTransform.<>O.<34>__Month = new Func<DaxExpression, DaxExpression>(DaxFunctions.Month));
					}
					return new DaxFunctionTransforms.UnaryFunctionTransform(func41);
				}
			}
			else if (num <= 3557254997U)
			{
				if (num <= 3037031955U)
				{
					if (num != 2870782494U)
					{
						if (num == 3037031955U)
						{
							if (functionName == "Core.Variance")
							{
								Func<DaxColumnRef, DaxExpression> func42;
								if ((func42 = DaxFunctionTransform.<>O.<15>__VarP) == null)
								{
									func42 = (DaxFunctionTransform.<>O.<15>__VarP = new Func<DaxColumnRef, DaxExpression>(DaxFunctions.VarP));
								}
								Func<DaxExpression, DaxExpression, DaxExpression> func43;
								if ((func43 = DaxFunctionTransform.<>O.<16>__VarPX) == null)
								{
									func43 = (DaxFunctionTransform.<>O.<16>__VarPX = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.VarPX));
								}
								return new DaxFunctionTransforms.AggregateFunctionTransform(func42, func43);
							}
						}
					}
					else if (functionName == "Core.Sqrt")
					{
						Func<DaxExpression, DaxExpression> func44;
						if ((func44 = DaxFunctionTransform.<>O.<46>__Sqrt) == null)
						{
							func44 = (DaxFunctionTransform.<>O.<46>__Sqrt = new Func<DaxExpression, DaxExpression>(DaxFunctions.Sqrt));
						}
						return new DaxFunctionTransforms.UnaryFunctionTransform(func44);
					}
				}
				else if (num != 3248488759U)
				{
					if (num != 3460334581U)
					{
						if (num == 3557254997U)
						{
							if (functionName == "Core.DivideRaw")
							{
								Func<DaxExpression, DaxExpression, DaxExpression> func45;
								if ((func45 = DaxFunctionTransform.<>O.<5>__DivideRaw) == null)
								{
									func45 = (DaxFunctionTransform.<>O.<5>__DivideRaw = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxOperators.DivideRaw));
								}
								return new DaxFunctionTransforms.BinaryFunctionTransform(func45);
							}
						}
					}
					else if (functionName == "Core.RoundUp")
					{
						Func<DaxExpression, DaxExpression, DaxExpression> func46;
						if ((func46 = DaxFunctionTransform.<>O.<51>__RoundUp) == null)
						{
							func46 = (DaxFunctionTransform.<>O.<51>__RoundUp = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.RoundUp));
						}
						return new DaxFunctionTransforms.BinaryFunctionTransform(func46);
					}
				}
				else if (functionName == "Core.IfError")
				{
					Func<DaxExpression, DaxExpression, DaxExpression> func47;
					if ((func47 = DaxFunctionTransform.<>O.<42>__IfError) == null)
					{
						func47 = (DaxFunctionTransform.<>O.<42>__IfError = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.IfError));
					}
					return new DaxFunctionTransforms.BinaryFunctionTransform(func47);
				}
			}
			else if (num <= 3886544790U)
			{
				if (num != 3598619976U)
				{
					if (num != 3797372689U)
					{
						if (num == 3886544790U)
						{
							if (functionName == "Core.DateTimeEqualToSecond")
							{
								return new DaxFunctionTransforms.DateTimeEqualToSecondFunctionTransform();
							}
						}
					}
					else if (functionName == "Core.PercentileInc")
					{
						Func<DaxColumnRef, DaxExpression, DaxExpression> func48;
						if ((func48 = DaxFunctionTransform.<>O.<27>__PercentileInc) == null)
						{
							func48 = (DaxFunctionTransform.<>O.<27>__PercentileInc = new Func<DaxColumnRef, DaxExpression, DaxExpression>(DaxFunctions.PercentileInc));
						}
						Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression> func49;
						if ((func49 = DaxFunctionTransform.<>O.<28>__PercentileXInc) == null)
						{
							func49 = (DaxFunctionTransform.<>O.<28>__PercentileXInc = new Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.PercentileXInc));
						}
						return new DaxFunctionTransforms.PercentileFunctionTransform(func48, func49);
					}
				}
				else if (functionName == "Core.Minute")
				{
					Func<DaxExpression, DaxExpression> func50;
					if ((func50 = DaxFunctionTransform.<>O.<37>__Minute) == null)
					{
						func50 = (DaxFunctionTransform.<>O.<37>__Minute = new Func<DaxExpression, DaxExpression>(DaxFunctions.Minute));
					}
					return new DaxFunctionTransforms.UnaryFunctionTransform(func50);
				}
			}
			else if (num != 4119138911U)
			{
				if (num != 4134362224U)
				{
					if (num == 4258294763U)
					{
						if (functionName == "Core.Log10")
						{
							Func<DaxExpression, DaxExpression> func51;
							if ((func51 = DaxFunctionTransform.<>O.<52>__Log10) == null)
							{
								func51 = (DaxFunctionTransform.<>O.<52>__Log10 = new Func<DaxExpression, DaxExpression>(DaxFunctions.Log10));
							}
							return new DaxFunctionTransforms.UnaryFunctionTransform(func51);
						}
					}
				}
				else if (functionName == "Core.Plus")
				{
					Func<DaxExpression, DaxExpression, DaxExpression> func52;
					if ((func52 = DaxFunctionTransform.<>O.<1>__Plus) == null)
					{
						func52 = (DaxFunctionTransform.<>O.<1>__Plus = new Func<DaxExpression, DaxExpression, DaxExpression>(DaxOperators.Plus));
					}
					return new DaxFunctionTransforms.BinaryFunctionTransform(func52);
				}
			}
			else if (functionName == "Core.PercentileExc")
			{
				Func<DaxColumnRef, DaxExpression, DaxExpression> func53;
				if ((func53 = DaxFunctionTransform.<>O.<25>__PercentileExc) == null)
				{
					func53 = (DaxFunctionTransform.<>O.<25>__PercentileExc = new Func<DaxColumnRef, DaxExpression, DaxExpression>(DaxFunctions.PercentileExc));
				}
				Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression> func54;
				if ((func54 = DaxFunctionTransform.<>O.<26>__PercentileXExc) == null)
				{
					func54 = (DaxFunctionTransform.<>O.<26>__PercentileXExc = new Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression>(DaxFunctions.PercentileXExc));
				}
				return new DaxFunctionTransforms.PercentileFunctionTransform(func53, func54);
			}
			throw new DaxTranslationException(DevErrors.DaxTranslation.UnexpectedFunction(functionName));
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x0002FC3F File Offset: 0x0002DE3F
		protected ReadOnlyCollection<QueryExpression> Arguments
		{
			get
			{
				return this._expr.Arguments;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x0002FC4C File Offset: 0x0002DE4C
		protected DaxTransform DaxTransform
		{
			get
			{
				return this._daxTransform;
			}
		}

		// Token: 0x06001126 RID: 4390
		internal abstract DaxExpression Translate();

		// Token: 0x06001127 RID: 4391 RVA: 0x0002FC54 File Offset: 0x0002DE54
		private void Init(QueryFunctionExpression expression, DaxTransform daxTransform)
		{
			this._expr = ArgumentValidation.CheckNotNull<QueryFunctionExpression>(expression, "expression");
			this._daxTransform = ArgumentValidation.CheckNotNull<DaxTransform>(daxTransform, "daxTransform");
		}

		// Token: 0x04000AA9 RID: 2729
		private QueryFunctionExpression _expr;

		// Token: 0x04000AAA RID: 2730
		private DaxTransform _daxTransform;

		// Token: 0x02000379 RID: 889
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001286 RID: 4742
			public static Func<DaxExpression, DaxExpression, DaxExpression> <0>__Or;

			// Token: 0x04001287 RID: 4743
			public static Func<DaxExpression, DaxExpression, DaxExpression> <1>__Plus;

			// Token: 0x04001288 RID: 4744
			public static Func<DaxExpression, DaxExpression, DaxExpression> <2>__Minus;

			// Token: 0x04001289 RID: 4745
			public static Func<DaxExpression, DaxExpression> <3>__MinusSign;

			// Token: 0x0400128A RID: 4746
			public static Func<DaxExpression, DaxExpression, DaxExpression> <4>__Multiply;

			// Token: 0x0400128B RID: 4747
			public static Func<DaxExpression, DaxExpression, DaxExpression> <5>__DivideRaw;

			// Token: 0x0400128C RID: 4748
			public static Func<DaxExpression, DaxExpression, DaxExpression> <6>__Divide;

			// Token: 0x0400128D RID: 4749
			public static Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression> <7>__Divide;

			// Token: 0x0400128E RID: 4750
			public static Func<DaxExpression, DaxExpression> <8>__Not;

			// Token: 0x0400128F RID: 4751
			public static Func<DaxColumnRef, DaxExpression> <9>__Sum;

			// Token: 0x04001290 RID: 4752
			public static Func<DaxExpression, DaxExpression, DaxExpression> <10>__SumX;

			// Token: 0x04001291 RID: 4753
			public static Func<DaxColumnRef, DaxExpression> <11>__Median;

			// Token: 0x04001292 RID: 4754
			public static Func<DaxExpression, DaxExpression, DaxExpression> <12>__MedianX;

			// Token: 0x04001293 RID: 4755
			public static Func<DaxColumnRef, DaxExpression> <13>__StdevP;

			// Token: 0x04001294 RID: 4756
			public static Func<DaxExpression, DaxExpression, DaxExpression> <14>__StdevPX;

			// Token: 0x04001295 RID: 4757
			public static Func<DaxColumnRef, DaxExpression> <15>__VarP;

			// Token: 0x04001296 RID: 4758
			public static Func<DaxExpression, DaxExpression, DaxExpression> <16>__VarPX;

			// Token: 0x04001297 RID: 4759
			public static Func<DaxColumnRef, DaxExpression> <17>__CountA;

			// Token: 0x04001298 RID: 4760
			public static Func<DaxExpression, DaxExpression, DaxExpression> <18>__CountAX;

			// Token: 0x04001299 RID: 4761
			public static Func<DaxColumnRef, DaxExpression> <19>__Average;

			// Token: 0x0400129A RID: 4762
			public static Func<DaxExpression, DaxExpression, DaxExpression> <20>__AverageX;

			// Token: 0x0400129B RID: 4763
			public static Func<DaxColumnRef, DaxExpression> <21>__Min;

			// Token: 0x0400129C RID: 4764
			public static Func<DaxExpression, DaxExpression, DaxExpression> <22>__MinX;

			// Token: 0x0400129D RID: 4765
			public static Func<DaxColumnRef, DaxExpression> <23>__Max;

			// Token: 0x0400129E RID: 4766
			public static Func<DaxExpression, DaxExpression, DaxExpression> <24>__MaxX;

			// Token: 0x0400129F RID: 4767
			public static Func<DaxColumnRef, DaxExpression, DaxExpression> <25>__PercentileExc;

			// Token: 0x040012A0 RID: 4768
			public static Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression> <26>__PercentileXExc;

			// Token: 0x040012A1 RID: 4769
			public static Func<DaxColumnRef, DaxExpression, DaxExpression> <27>__PercentileInc;

			// Token: 0x040012A2 RID: 4770
			public static Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression> <28>__PercentileXInc;

			// Token: 0x040012A3 RID: 4771
			public static Func<DaxExpression, DaxExpression, DaxExpression> <29>__InvokeContains;

			// Token: 0x040012A4 RID: 4772
			public static Func<DaxExpression, DaxExpression, DaxExpression> <30>__InvokeEndsWith;

			// Token: 0x040012A5 RID: 4773
			public static Func<DaxExpression, DaxExpression, DaxExpression> <31>__InvokeStartsWith;

			// Token: 0x040012A6 RID: 4774
			public static Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression> <32>__Date;

			// Token: 0x040012A7 RID: 4775
			public static Func<DaxExpression, DaxExpression> <33>__Year;

			// Token: 0x040012A8 RID: 4776
			public static Func<DaxExpression, DaxExpression> <34>__Month;

			// Token: 0x040012A9 RID: 4777
			public static Func<DaxExpression, DaxExpression> <35>__Day;

			// Token: 0x040012AA RID: 4778
			public static Func<DaxExpression, DaxExpression> <36>__Hour;

			// Token: 0x040012AB RID: 4779
			public static Func<DaxExpression, DaxExpression> <37>__Minute;

			// Token: 0x040012AC RID: 4780
			public static Func<DaxExpression, DaxExpression> <38>__Second;

			// Token: 0x040012AD RID: 4781
			public static Func<DaxExpression, DaxExpression, DaxExpression> <39>__EDate;

			// Token: 0x040012AE RID: 4782
			public static Func<DaxExpression, DaxExpression, DaxExpression> <40>__If;

			// Token: 0x040012AF RID: 4783
			public static Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression> <41>__If;

			// Token: 0x040012B0 RID: 4784
			public static Func<DaxExpression, DaxExpression, DaxExpression> <42>__IfError;

			// Token: 0x040012B1 RID: 4785
			public static Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression> <43>__InvokeDaxBetween;

			// Token: 0x040012B2 RID: 4786
			public static Func<DaxExpression, DaxExpression, DaxExpression> <44>__Ceiling;

			// Token: 0x040012B3 RID: 4787
			public static Func<DaxExpression, DaxExpression, DaxExpression> <45>__Floor;

			// Token: 0x040012B4 RID: 4788
			public static Func<DaxExpression, DaxExpression> <46>__Sqrt;

			// Token: 0x040012B5 RID: 4789
			public static Func<DaxExpression, DaxExpression, DaxExpression> <47>__Min;

			// Token: 0x040012B6 RID: 4790
			public static Func<DaxExpression, DaxExpression, DaxExpression> <48>__Max;

			// Token: 0x040012B7 RID: 4791
			public static Func<DaxExpression, DaxExpression> <49>__Int;

			// Token: 0x040012B8 RID: 4792
			public static Func<DaxExpression, DaxExpression, DaxExpression> <50>__RoundDown;

			// Token: 0x040012B9 RID: 4793
			public static Func<DaxExpression, DaxExpression, DaxExpression> <51>__RoundUp;

			// Token: 0x040012BA RID: 4794
			public static Func<DaxExpression, DaxExpression> <52>__Log10;

			// Token: 0x040012BB RID: 4795
			public static Func<DaxExpression, DaxExpression, DaxExpression> <53>__Power;
		}
	}
}
