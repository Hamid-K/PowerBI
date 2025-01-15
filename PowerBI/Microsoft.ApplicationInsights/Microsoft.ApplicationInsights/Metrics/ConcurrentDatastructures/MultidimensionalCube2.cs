using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ApplicationInsights.Metrics.ConcurrentDatastructures
{
	// Token: 0x02000037 RID: 55
	[SuppressMessage("Microsoft.Design", "CA1001: Types that own disposable fields should be disposable", Justification = "OK not to explicitly dispose a released SemaphoreSlim.")]
	internal class MultidimensionalCube2<TPoint>
	{
		// Token: 0x06000203 RID: 515 RVA: 0x0000ADC7 File Offset: 0x00008FC7
		public MultidimensionalCube2(Func<string[], TPoint> pointsFactory, params int[] dimensionValuesCountLimits)
			: this(int.MaxValue, pointsFactory, dimensionValuesCountLimits)
		{
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		public MultidimensionalCube2(int totalPointsCountLimit, Func<string[], TPoint> pointsFactory, params int[] dimensionValuesCountLimits)
		{
			if (totalPointsCountLimit < 1)
			{
				throw new ArgumentOutOfRangeException("totalPointsCountLimit", global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0} must be 1 or larger. Typically much larger.", new object[] { "totalPointsCountLimit" })));
			}
			Util.ValidateNotNull(pointsFactory, "pointsFactory");
			Util.ValidateNotNull(dimensionValuesCountLimits, "dimensionValuesCountLimits");
			if (dimensionValuesCountLimits.Length == 0)
			{
				throw new ArgumentException("Cube must have 1 or more dimensions.", "dimensionValuesCountLimits");
			}
			if (dimensionValuesCountLimits.Length > 50)
			{
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Cube may not have more than ${0} dimensions,", new object[] { 50 })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" but {0} dimensions were specified.", new object[] { dimensionValuesCountLimits.Length })));
			}
			for (int i = 0; i < dimensionValuesCountLimits.Length; i++)
			{
				if (dimensionValuesCountLimits[i] < 1)
				{
					throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("The limit of distinct dimension values must be 1 or larger, but the limit specified for dimension {0} is {1}.", new object[]
					{
						i,
						dimensionValuesCountLimits[i]
					})));
				}
			}
			this.totalPointsCountLimit = totalPointsCountLimit;
			this.dimensionValuesCountLimits = dimensionValuesCountLimits;
			this.dimensionValues = new HashSet<string>[dimensionValuesCountLimits.Length];
			this.points = new ConcurrentDictionary<string, TPoint>();
			this.pointsFactory = pointsFactory;
			this.totalPointsCount = 0;
			for (int j = 0; j < this.dimensionValues.Length; j++)
			{
				this.dimensionValues[j] = new HashSet<string>();
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000AF36 File Offset: 0x00009136
		public int DimensionsCount
		{
			get
			{
				return this.dimensionValuesCountLimits.Length;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000AF40 File Offset: 0x00009140
		public int TotalPointsCountLimit
		{
			get
			{
				return this.totalPointsCountLimit;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000AF48 File Offset: 0x00009148
		public int TotalPointsCount
		{
			get
			{
				return Volatile.Read(ref this.totalPointsCount);
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000AF55 File Offset: 0x00009155
		public int GetDimensionValuesCountLimit(int dimension)
		{
			this.ValidateDimensionIndex(dimension);
			return this.dimensionValuesCountLimits[dimension];
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000AF66 File Offset: 0x00009166
		public IReadOnlyCollection<string> GetDimensionValues(int dimension)
		{
			this.ValidateDimensionIndex(dimension);
			return (IReadOnlyCollection<string>)this.dimensionValues[dimension];
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000AF7C File Offset: 0x0000917C
		public IReadOnlyList<KeyValuePair<string[], TPoint>> GetAllPoints()
		{
			List<KeyValuePair<string[], TPoint>> list = new List<KeyValuePair<string[], TPoint>>(this.TotalPointsCount);
			this.GetAllPoints(list);
			return list;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000AFA0 File Offset: 0x000091A0
		public int GetAllPoints(ICollection<KeyValuePair<string[], TPoint>> pointContainer)
		{
			int num = 0;
			foreach (KeyValuePair<string, TPoint> keyValuePair in this.points)
			{
				string[] array = MultidimensionalCube2<TPoint>.ParsePointMoniker(keyValuePair.Key);
				KeyValuePair<string[], TPoint> keyValuePair2 = new KeyValuePair<string[], TPoint>(array, keyValuePair.Value);
				pointContainer.Add(keyValuePair2);
				num++;
			}
			return num;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B014 File Offset: 0x00009214
		public MultidimensionalPointResult<TPoint> TryGetOrCreatePoint(params string[] coordinates)
		{
			string pointMoniker = this.GetPointMoniker(coordinates);
			TPoint tpoint;
			if (this.points.TryGetValue(pointMoniker, out tpoint))
			{
				return new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Success_ExistingPointRetrieved, tpoint);
			}
			if (this.totalPointsCount >= this.totalPointsCountLimit)
			{
				return new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Failure_TotalPointsCountLimitReached, -1);
			}
			this.pointCreationLock.Wait();
			MultidimensionalPointResult<TPoint> multidimensionalPointResult;
			try
			{
				multidimensionalPointResult = this.TryCreatePoint(coordinates, pointMoniker);
			}
			finally
			{
				this.pointCreationLock.Release();
			}
			return multidimensionalPointResult;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000B08C File Offset: 0x0000928C
		public Task<MultidimensionalPointResult<TPoint>> TryGetOrCreatePointAsync(params string[] coordinates)
		{
			return this.TryGetOrCreatePointAsync(CancellationToken.None, coordinates);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000B09C File Offset: 0x0000929C
		public async Task<MultidimensionalPointResult<TPoint>> TryGetOrCreatePointAsync(CancellationToken cancelToken, params string[] coordinates)
		{
			string pointMoniker = this.GetPointMoniker(coordinates);
			TPoint tpoint;
			MultidimensionalPointResult<TPoint> multidimensionalPointResult;
			if (this.points.TryGetValue(pointMoniker, out tpoint))
			{
				multidimensionalPointResult = new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Success_ExistingPointRetrieved, tpoint);
			}
			else if (this.totalPointsCount >= this.totalPointsCountLimit)
			{
				multidimensionalPointResult = new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Failure_TotalPointsCountLimitReached, -1);
			}
			else
			{
				await this.pointCreationLock.WaitAsync(cancelToken).ConfigureAwait(false);
				try
				{
					multidimensionalPointResult = this.TryCreatePoint(coordinates, pointMoniker);
				}
				finally
				{
					this.pointCreationLock.Release();
				}
			}
			return multidimensionalPointResult;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000B0F4 File Offset: 0x000092F4
		public MultidimensionalPointResult<TPoint> TryGetPoint(params string[] coordinates)
		{
			string pointMoniker = this.GetPointMoniker(coordinates);
			TPoint tpoint;
			if (this.points.TryGetValue(pointMoniker, out tpoint))
			{
				return new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Success_ExistingPointRetrieved, tpoint);
			}
			return new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Failure_PointDoesNotExistCreationNotRequested, -1);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000B129 File Offset: 0x00009329
		private static string[] ParsePointMoniker(string pointMoniker)
		{
			return pointMoniker.Split(MultidimensionalCube2<TPoint>.PointMonikerSeparatorAsArray, StringSplitOptions.None);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000B138 File Offset: 0x00009338
		private static string BuildPointMoniker(string[] coordinates)
		{
			if (coordinates.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < coordinates.Length; i++)
			{
				if (coordinates[i] == null)
				{
					throw new ArgumentNullException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0}[{1}]", new object[] { "coordinates", i })), global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("The specified {0}-vector contains null at index {1}.", new object[] { "coordinates", i })));
				}
				if (coordinates[i].Contains("\0"))
				{
					throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("The value at index {0} of the specified {1}-vector contains", new object[] { i, "coordinates" })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" an invalid character sub-sequence. Complete coordinate value: \"{0}\".", new object[] { coordinates[i] })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" Invalid sub-sequence: \"{0}\".", new object[] { "\0" })));
				}
				if (i > 0)
				{
					stringBuilder.Append("\0");
				}
				stringBuilder.Append(coordinates[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000B258 File Offset: 0x00009458
		private MultidimensionalPointResult<TPoint> TryCreatePoint(string[] coordinates, string pointMoniker)
		{
			TPoint tpoint;
			if (this.points.TryGetValue(pointMoniker, out tpoint))
			{
				return new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Success_ExistingPointRetrieved, tpoint);
			}
			if (this.totalPointsCount >= this.totalPointsCountLimit)
			{
				return new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Failure_TotalPointsCountLimitReached, -1);
			}
			int num = -1;
			BitArray bitArray = new BitArray(coordinates.Length, false);
			for (int i = 0; i < coordinates.Length; i++)
			{
				HashSet<string> hashSet = this.dimensionValues[i];
				string text = coordinates[i];
				if (hashSet.Count >= this.dimensionValuesCountLimits[i] && !hashSet.Contains(text))
				{
					num = i;
					break;
				}
				bool flag = hashSet.Add(coordinates[i]);
				bitArray.Set(i, flag);
			}
			if (num != -1)
			{
				for (int j = 0; j <= num; j++)
				{
					if (bitArray.Get(j))
					{
						this.dimensionValues[j].Remove(coordinates[j]);
					}
				}
				return new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Failure_SubdimensionsCountLimitReached, num);
			}
			try
			{
				tpoint = this.pointsFactory(coordinates);
			}
			catch (Exception ex)
			{
				for (int k = 0; k <= num; k++)
				{
					if (bitArray.Get(k))
					{
						this.dimensionValues[k].Remove(coordinates[k]);
					}
				}
				ExceptionDispatchInfo.Capture(ex).Throw();
				throw;
			}
			if (!this.points.TryAdd(pointMoniker, tpoint))
			{
				throw new InvalidOperationException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Internal SDK bug. Please report this! (pointMoniker: {0})", new object[] { pointMoniker })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" Info: Failed to add a point to the {0}-collection in", new object[] { "points" })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" class {0} despite passing all the cerfification checks.", new object[] { "MultidimensionalCube2" })));
			}
			this.totalPointsCount++;
			return new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Success_NewPointCreated, tpoint);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000B40C File Offset: 0x0000960C
		private void ValidateDimensionIndex(int dimension)
		{
			if (dimension < 0)
			{
				throw new ArgumentOutOfRangeException("dimension", "Dimension index may not be negative.");
			}
			if (dimension >= this.DimensionsCount)
			{
				throw new ArgumentOutOfRangeException("dimension", global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Dimension index (zero-based) exceeds the number of dimensions of this cube ({0}).", new object[] { this.DimensionsCount })));
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000B464 File Offset: 0x00009664
		private string GetPointMoniker(string[] coordinates)
		{
			Util.ValidateNotNull(coordinates, "coordinates");
			if (coordinates.Length != this.DimensionsCount)
			{
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("The specified {0}-vector has {1} dimensions.", new object[] { "coordinates", coordinates.Length })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" However this has {0} dimensions.", new object[] { this.DimensionsCount })), "coordinates");
			}
			return MultidimensionalCube2<TPoint>.BuildPointMoniker(coordinates);
		}

		// Token: 0x040000EB RID: 235
		private const int DimensionsCountLimit = 50;

		// Token: 0x040000EC RID: 236
		private const string PointMonikerSeparator = "\0";

		// Token: 0x040000ED RID: 237
		private static readonly string[] PointMonikerSeparatorAsArray = new string[] { "\0" };

		// Token: 0x040000EE RID: 238
		private readonly SemaphoreSlim pointCreationLock = new SemaphoreSlim(1);

		// Token: 0x040000EF RID: 239
		private readonly int totalPointsCountLimit;

		// Token: 0x040000F0 RID: 240
		private readonly int[] dimensionValuesCountLimits;

		// Token: 0x040000F1 RID: 241
		private readonly HashSet<string>[] dimensionValues;

		// Token: 0x040000F2 RID: 242
		private readonly ConcurrentDictionary<string, TPoint> points;

		// Token: 0x040000F3 RID: 243
		private readonly Func<string[], TPoint> pointsFactory;

		// Token: 0x040000F4 RID: 244
		private int totalPointsCount;
	}
}
