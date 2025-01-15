using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.InfoNav.Analytics.TimeSeriesPreProcessing;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x02000026 RID: 38
	[ImmutableObject(true)]
	internal sealed class DataPreprocessResult
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00004390 File Offset: 0x00002590
		internal DataPreprocessResult(IReadOnlyList<IDataRow> dataRows, int lastTrainDataRowIndex, int forecastLength, ForecastStepData dataStep, double minValue, double maxValue, double[] correctedXValues, double[] correctedYValues, int sizeAfterCorrection)
		{
			this._dataRows = dataRows;
			this._lastTrainDataRowIndex = lastTrainDataRowIndex;
			this._forecastLength = forecastLength;
			this._dataStep = dataStep;
			this._minValue = minValue;
			this._maxValue = maxValue;
			this._range = this._maxValue - this._minValue;
			this._correctedXValues = correctedXValues;
			this._correctedYValues = correctedYValues;
			this._sizeAfterCorrection = sizeAfterCorrection;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000043FB File Offset: 0x000025FB
		internal int SizeAfterCorrection
		{
			get
			{
				return this._sizeAfterCorrection;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004403 File Offset: 0x00002603
		internal IReadOnlyList<double> CorrectedXValues
		{
			get
			{
				return this._correctedXValues;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000440B File Offset: 0x0000260B
		internal IReadOnlyList<double> CorrectedYValues
		{
			get
			{
				return this._correctedYValues;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00004413 File Offset: 0x00002613
		internal IReadOnlyList<IDataRow> DataRows
		{
			get
			{
				return this._dataRows;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000441B File Offset: 0x0000261B
		internal int LastTrainRowIndex
		{
			get
			{
				return this._lastTrainDataRowIndex;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004423 File Offset: 0x00002623
		internal int ForecastLength
		{
			get
			{
				return this._forecastLength;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000442B File Offset: 0x0000262B
		internal ForecastStepData DataStep
		{
			get
			{
				return this._dataStep;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004433 File Offset: 0x00002633
		internal bool NeedNormalize
		{
			get
			{
				return this._maxValue > 3.4028234663852886E+38 || this._minValue < -3.4028234663852886E+38;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000445A File Offset: 0x0000265A
		internal float GetNormalizedValue(double value)
		{
			if (this._range == 0.0)
			{
				return 1f;
			}
			return (float)((value - this._minValue) / this._range);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004483 File Offset: 0x00002683
		internal double GetDenormalizedValue(float value)
		{
			if (this._range == 0.0)
			{
				return (double)value * this._maxValue;
			}
			return this._range * (double)value + this._minValue;
		}

		// Token: 0x0400009F RID: 159
		private readonly IReadOnlyList<IDataRow> _dataRows;

		// Token: 0x040000A0 RID: 160
		private readonly int _lastTrainDataRowIndex;

		// Token: 0x040000A1 RID: 161
		private readonly int _forecastLength;

		// Token: 0x040000A2 RID: 162
		private readonly ForecastStepData _dataStep;

		// Token: 0x040000A3 RID: 163
		private readonly double _minValue;

		// Token: 0x040000A4 RID: 164
		private readonly double _maxValue;

		// Token: 0x040000A5 RID: 165
		private readonly double _range;

		// Token: 0x040000A6 RID: 166
		private readonly IReadOnlyList<double> _correctedXValues;

		// Token: 0x040000A7 RID: 167
		private readonly IReadOnlyList<double> _correctedYValues;

		// Token: 0x040000A8 RID: 168
		private readonly int _sizeAfterCorrection;
	}
}
