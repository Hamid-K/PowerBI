using System;
using System.ComponentModel;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x0200002C RID: 44
	[ImmutableObject(true)]
	internal sealed class ForecastContext
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00004E64 File Offset: 0x00003064
		private ForecastContext(DataTransformExecutionContext transformContext, ForecastParameterUnit unit, int forecastLength, int ignoreLast, int xValueColumnIndex, int yValueColumnIndex, int forecastValueColumnIndex, int lowerBoundValueColumnIndex, int upperBoundValueColumnIndex, int outputSchemaColumnCount, DataType outputDataType, int? maxSeasonality, float confidenceLevel, DataType xColumnDataType, Algorithm? algorithm, int? consumeLength)
		{
			this._transformContext = transformContext;
			this._unit = unit;
			this._forecastLength = forecastLength;
			this._ignoreLast = ignoreLast;
			this._xValueColumnIndex = xValueColumnIndex;
			this._yValueColumnIndex = yValueColumnIndex;
			this._forecastValueColumnIndex = forecastValueColumnIndex;
			this._lowerBoundValueColumnIndex = lowerBoundValueColumnIndex;
			this._upperBoundValueColumnIndex = upperBoundValueColumnIndex;
			this._outputSchemaColumnCount = outputSchemaColumnCount;
			this._outputDataType = outputDataType;
			this._maxSeasonality = maxSeasonality;
			this._confidenceLevel = confidenceLevel;
			this._xColumnDataType = xColumnDataType;
			this._algorithm = algorithm;
			this._consumeLength = consumeLength;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004EF4 File Offset: 0x000030F4
		internal DataTransformExecutionContext TransformContext
		{
			get
			{
				return this._transformContext;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004EFC File Offset: 0x000030FC
		internal ForecastParameterUnit ParameterUnit
		{
			get
			{
				return this._unit;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004F04 File Offset: 0x00003104
		internal int ForecastLength
		{
			get
			{
				return this._forecastLength;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004F0C File Offset: 0x0000310C
		internal int IgnoreLast
		{
			get
			{
				return this._ignoreLast;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004F14 File Offset: 0x00003114
		internal int XValueColumnIndex
		{
			get
			{
				return this._xValueColumnIndex;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004F1C File Offset: 0x0000311C
		internal int YValueColumnIndex
		{
			get
			{
				return this._yValueColumnIndex;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004F24 File Offset: 0x00003124
		internal int ForecastValueColumnIndex
		{
			get
			{
				return this._forecastValueColumnIndex;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004F2C File Offset: 0x0000312C
		internal int LowerBoundForecastValue
		{
			get
			{
				return this._lowerBoundValueColumnIndex;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004F34 File Offset: 0x00003134
		internal int UpperBoundForecastValue
		{
			get
			{
				return this._upperBoundValueColumnIndex;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004F3C File Offset: 0x0000313C
		internal int OutputSchemaColumnCount
		{
			get
			{
				return this._outputSchemaColumnCount;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004F44 File Offset: 0x00003144
		internal DataType OutputDataType
		{
			get
			{
				return this._outputDataType;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004F4C File Offset: 0x0000314C
		internal int? MaxSeasonality
		{
			get
			{
				return this._maxSeasonality;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004F54 File Offset: 0x00003154
		internal float ConfidenceLevel
		{
			get
			{
				return this._confidenceLevel;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004F5C File Offset: 0x0000315C
		internal DataType XColumnDataType
		{
			get
			{
				return this._xColumnDataType;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004F64 File Offset: 0x00003164
		internal Algorithm? Algorithm
		{
			get
			{
				return this._algorithm;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004F6C File Offset: 0x0000316C
		internal int? ConsumeLength
		{
			get
			{
				return this._consumeLength;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004F74 File Offset: 0x00003174
		internal static ForecastContext Create(DataTransformExecutionContext context, int xValueIndex, int yValueIndex, float defaultConfidence)
		{
			ForecastParameterUnit? forecastParameterUnit = null;
			int? num = null;
			int? num2 = null;
			int? num3 = null;
			float? num4 = null;
			Algorithm? algorithm = null;
			int num5 = 0;
			int i = 0;
			while (i < context.Parameters.Count)
			{
				DataTransformParameter dataTransformParameter = context.Parameters[i];
				object value = dataTransformParameter.Value;
				string name = dataTransformParameter.Name;
				uint num6 = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num6 <= 2065166438U)
				{
					if (num6 <= 1656517241U)
					{
						if (num6 != 1642099001U)
						{
							if (num6 != 1656517241U)
							{
								goto IL_032F;
							}
							if (!(name == "ConfidenceLevel"))
							{
								goto IL_032F;
							}
							float num7;
							if (!value.TryConvertToFloat(out num7))
							{
								throw new TransformException("Invalid data type or value for parameter " + dataTransformParameter.Name);
							}
							num4 = new float?(num7);
						}
						else
						{
							if (!(name == "ConsumeLength"))
							{
								goto IL_032F;
							}
							int num8;
							if (!value.TryConvertToInt32(out num8))
							{
								throw new TransformException("Invalid data type or value for parameter " + dataTransformParameter.Name);
							}
							num = new int?(num8);
						}
					}
					else if (num6 != 1759602215U)
					{
						if (num6 != 2065166438U)
						{
							goto IL_032F;
						}
						if (!(name == "DataStepSize"))
						{
							goto IL_032F;
						}
					}
					else
					{
						if (!(name == "Unit"))
						{
							goto IL_032F;
						}
						int num8;
						if (!value.TryConvertToInt32(out num8) || !Enum.IsDefined(typeof(ForecastParameterUnit), num8))
						{
							throw new TransformException("Invalid data type or value for parameter " + dataTransformParameter.Name);
						}
						forecastParameterUnit = new ForecastParameterUnit?((ForecastParameterUnit)num8);
					}
				}
				else if (num6 <= 2377981135U)
				{
					if (num6 != 2083852035U)
					{
						if (num6 != 2377981135U)
						{
							goto IL_032F;
						}
						if (!(name == "MaxSeasonality"))
						{
							goto IL_032F;
						}
						int num8;
						if (!value.TryConvertToInt32(out num8))
						{
							throw new TransformException("Invalid data type or value for parameter " + dataTransformParameter.Name);
						}
						num3 = new int?(num8);
					}
					else
					{
						if (!(name == "IgnoreLast"))
						{
							goto IL_032F;
						}
						int num8;
						if (!value.TryConvertToInt32(out num8))
						{
							throw new TransformException("Invalid data type or value for parameter " + dataTransformParameter.Name);
						}
						num5 = num8;
					}
				}
				else if (num6 != 2580448333U)
				{
					if (num6 != 3297850349U)
					{
						if (num6 != 3613153356U)
						{
							goto IL_032F;
						}
						if (!(name == "ForecastLength"))
						{
							goto IL_032F;
						}
						int num8;
						if (!value.TryConvertToInt32(out num8))
						{
							throw new TransformException("Invalid data type or value for parameter " + dataTransformParameter.Name);
						}
						num2 = new int?(num8);
					}
					else
					{
						if (!(name == "ForecastAlgorithm"))
						{
							goto IL_032F;
						}
						int num8;
						if (!value.TryConvertToInt32(out num8) || !Enum.IsDefined(typeof(Algorithm), num8))
						{
							throw new TransformException("Invalid data type or value for parameter " + dataTransformParameter.Name);
						}
						algorithm = new Algorithm?((Algorithm)num8);
					}
				}
				else if (!(name == "DataStepUnit"))
				{
					goto IL_032F;
				}
				i++;
				continue;
				IL_032F:
				throw new TransformException("Unknown parameter " + dataTransformParameter.Name);
			}
			algorithm = new Algorithm?(Microsoft.InfoNav.Analytics.Forecast.Algorithm.ETS);
			if (forecastParameterUnit == null)
			{
				throw new TransformException("Missing Unit value");
			}
			if (num2 == null)
			{
				throw new TransformException("Missing ForecastLength value");
			}
			if (num2.Value <= 0)
			{
				throw new TransformException(StringUtil.FormatInvariant("Invalid ForecastLength {0} value", num2.Value));
			}
			if (num4 == null)
			{
				num4 = new float?(defaultConfidence);
			}
			if (num4.Value > 0f)
			{
				float? num9 = num4;
				float num10 = (float)1;
				if (!((num9.GetValueOrDefault() >= num10) & (num9 != null)))
				{
					int count = context.InputSchema.Count;
					int num11 = count + 1;
					int num12 = num11 + 1;
					int num13 = context.InputSchema.Count + 3;
					DataType dataType = context.InputSchema.GetColumn(yValueIndex).DataType;
					DataType dataType2 = context.InputSchema.GetColumn(xValueIndex).DataType;
					return new ForecastContext(context, forecastParameterUnit.Value, num2.Value, num5, xValueIndex, yValueIndex, count, num11, num12, num13, dataType, num3, num4.Value, dataType2, algorithm, num);
				}
			}
			throw new TransformException(StringUtil.FormatInvariant("Invalid confidence level {0}. Must be in range (0..1)", num4.Value));
		}

		// Token: 0x040000DA RID: 218
		private readonly DataTransformExecutionContext _transformContext;

		// Token: 0x040000DB RID: 219
		private readonly ForecastParameterUnit _unit;

		// Token: 0x040000DC RID: 220
		private readonly int _forecastLength;

		// Token: 0x040000DD RID: 221
		private readonly int _ignoreLast;

		// Token: 0x040000DE RID: 222
		private readonly int _xValueColumnIndex;

		// Token: 0x040000DF RID: 223
		private readonly int _yValueColumnIndex;

		// Token: 0x040000E0 RID: 224
		private readonly int _forecastValueColumnIndex;

		// Token: 0x040000E1 RID: 225
		private readonly int _lowerBoundValueColumnIndex;

		// Token: 0x040000E2 RID: 226
		private readonly int _upperBoundValueColumnIndex;

		// Token: 0x040000E3 RID: 227
		private readonly int _outputSchemaColumnCount;

		// Token: 0x040000E4 RID: 228
		private readonly DataType _outputDataType;

		// Token: 0x040000E5 RID: 229
		private readonly int? _maxSeasonality;

		// Token: 0x040000E6 RID: 230
		private readonly float _confidenceLevel;

		// Token: 0x040000E7 RID: 231
		private readonly DataType _xColumnDataType;

		// Token: 0x040000E8 RID: 232
		private readonly Algorithm? _algorithm;

		// Token: 0x040000E9 RID: 233
		private readonly int? _consumeLength;
	}
}
