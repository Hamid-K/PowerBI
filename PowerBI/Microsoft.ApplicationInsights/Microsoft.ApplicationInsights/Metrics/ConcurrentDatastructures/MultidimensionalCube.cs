using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ApplicationInsights.Metrics.ConcurrentDatastructures
{
	// Token: 0x02000036 RID: 54
	internal class MultidimensionalCube<TDimensionValue, TPoint>
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x0000A9FB File Offset: 0x00008BFB
		public MultidimensionalCube(Func<TDimensionValue[], TPoint> pointsFactory, IEnumerable<int> subdimensionsCountLimits)
			: this(int.MaxValue, pointsFactory, subdimensionsCountLimits)
		{
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000AA0A File Offset: 0x00008C0A
		public MultidimensionalCube(int totalPointsCountLimit, Func<TDimensionValue[], TPoint> pointsFactory, IEnumerable<int> subdimensionsCountLimits)
			: this(totalPointsCountLimit, pointsFactory, (subdimensionsCountLimits != null) ? subdimensionsCountLimits.ToArray<int>() : null)
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000AA20 File Offset: 0x00008C20
		public MultidimensionalCube(Func<TDimensionValue[], TPoint> pointsFactory, params int[] subdimensionsCountLimits)
			: this(int.MaxValue, pointsFactory, subdimensionsCountLimits)
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000AA30 File Offset: 0x00008C30
		public MultidimensionalCube(int totalPointsCountLimit, Func<TDimensionValue[], TPoint> pointsFactory, params int[] subdimensionsCountLimits)
		{
			if (totalPointsCountLimit < 1)
			{
				throw new ArgumentOutOfRangeException("totalPointsCountLimit", global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0} must be 1 or larger. Typically much larger.", new object[] { "totalPointsCountLimit" })));
			}
			Util.ValidateNotNull(pointsFactory, "pointsFactory");
			Util.ValidateNotNull(subdimensionsCountLimits, "subdimensionsCountLimits");
			if (subdimensionsCountLimits.Length == 0)
			{
				throw new ArgumentException("Cube must have 1 or more dimensions.", "subdimensionsCountLimits");
			}
			if (subdimensionsCountLimits.Length > 50)
			{
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Cube may not have more than ${0} dimensions,", new object[] { 50 })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" but {0} dimensions were specified.", new object[] { subdimensionsCountLimits.Length })));
			}
			for (int i = 0; i < subdimensionsCountLimits.Length; i++)
			{
				if (subdimensionsCountLimits[i] < 1)
				{
					throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("The limit of distinct dimension values must be 1 or larger, but the limit specified for dimension {0} is {1}.", new object[]
					{
						i,
						subdimensionsCountLimits[i]
					})));
				}
			}
			this.totalPointsCountLimit = totalPointsCountLimit;
			this.subdimensionsCountLimits = subdimensionsCountLimits;
			this.points = new MultidimensionalCubeDimension<TDimensionValue, TPoint>(this, subdimensionsCountLimits[0], subdimensionsCountLimits.Length == 1);
			this.pointsFactory = pointsFactory;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000AB57 File Offset: 0x00008D57
		public int DimensionsCount
		{
			get
			{
				return this.subdimensionsCountLimits.Length;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000AB61 File Offset: 0x00008D61
		public int TotalPointsCountLimit
		{
			get
			{
				return this.totalPointsCountLimit;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000AB69 File Offset: 0x00008D69
		public int TotalPointsCount
		{
			get
			{
				return Volatile.Read(ref this.totalPointsCount);
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000AB76 File Offset: 0x00008D76
		public int GetSubdimensionsCountLimit(int dimension)
		{
			return this.subdimensionsCountLimits[dimension];
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000AB80 File Offset: 0x00008D80
		public IReadOnlyCollection<KeyValuePair<TDimensionValue[], TPoint>> GetAllPoints()
		{
			List<KeyValuePair<TDimensionValue[], TPoint>> list = new List<KeyValuePair<TDimensionValue[], TPoint>>();
			this.GetAllPoints(list);
			return list;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000AB9C File Offset: 0x00008D9C
		public void GetAllPoints(ICollection<KeyValuePair<TDimensionValue[], TPoint>> pointContainer)
		{
			new List<KeyValuePair<TDimensionValue[], TPoint>>();
			foreach (KeyValuePair<IList<TDimensionValue>, TPoint> keyValuePair in this.points.GetAllPointsReversed())
			{
				KeyValuePair<TDimensionValue[], TPoint> keyValuePair2 = new KeyValuePair<TDimensionValue[], TPoint>(new TDimensionValue[keyValuePair.Key.Count], keyValuePair.Value);
				int num = keyValuePair.Key.Count - 1;
				for (int i = num; i >= 0; i--)
				{
					keyValuePair2.Key[num - i] = keyValuePair.Key[i];
				}
				pointContainer.Add(keyValuePair2);
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000AC50 File Offset: 0x00008E50
		public MultidimensionalPointResult<TPoint> TryGetOrCreatePoint(params TDimensionValue[] coordinates)
		{
			return this.points.TryGetOrAddVector(coordinates);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000AC5E File Offset: 0x00008E5E
		public MultidimensionalPointResult<TPoint> TryGetPoint(params TDimensionValue[] coordinates)
		{
			return this.points.TryGetVector(coordinates);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000AC6C File Offset: 0x00008E6C
		public Task<MultidimensionalPointResult<TPoint>> TryGetOrCreatePointAsync(params TDimensionValue[] coordinates)
		{
			return this.TryGetOrCreatePointAsync(TimeSpan.FromMilliseconds(2.0), TimeSpan.FromMilliseconds(11.0), CancellationToken.None, coordinates);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000AC98 File Offset: 0x00008E98
		public async Task<MultidimensionalPointResult<TPoint>> TryGetOrCreatePointAsync(TimeSpan sleepDuration, TimeSpan timeout, CancellationToken cancelToken, params TDimensionValue[] coordinates)
		{
			try
			{
				MultidimensionalPointResult<TPoint> multidimensionalPointResult = this.TryGetOrCreatePoint(coordinates);
				if (multidimensionalPointResult.IsSuccess)
				{
					return multidimensionalPointResult;
				}
				if (timeout == TimeSpan.Zero)
				{
					multidimensionalPointResult.SetAsyncTimeoutReachedFailure();
					return multidimensionalPointResult;
				}
			}
			catch (Exception ex)
			{
				if (!MultidimensionalCube<TDimensionValue, TPoint>.IsThrownByPointsFactoryKey(ex) || timeout == TimeSpan.Zero)
				{
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
			}
			bool infiniteTimeout = timeout == Timeout.InfiniteTimeSpan;
			if (!infiniteTimeout)
			{
				if (Math.Round(timeout.TotalMilliseconds) >= 2147483647.0)
				{
					throw new ArgumentOutOfRangeException("timeout", global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0} must be smaller than {1} msec, but it is {2}.", new object[] { "timeout", int.MaxValue, timeout })));
				}
				if (Math.Round(timeout.TotalMilliseconds) < 1.0)
				{
					throw new ArgumentOutOfRangeException("timeout", global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0} must be zero, positive or Infinite, but it is {1}.", new object[] { "timeout", timeout })));
				}
			}
			if (Math.Round(sleepDuration.TotalMilliseconds) > 2147483647.0)
			{
				throw new ArgumentOutOfRangeException("sleepDuration", global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0} must be smaller than {1} msec, but it is {2}.", new object[] { "sleepDuration", int.MaxValue, sleepDuration })));
			}
			if (Math.Round(sleepDuration.TotalMilliseconds) < 0.0)
			{
				throw new ArgumentOutOfRangeException("sleepDuration", global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0} must be non-negative, but it is {1}.", new object[] { "sleepDuration", sleepDuration })));
			}
			int num = (int)Math.Round(timeout.TotalMilliseconds);
			int sleepMillis = (int)Math.Round(sleepDuration.TotalMilliseconds);
			int tickCount = Environment.TickCount;
			int stopMillis = tickCount + num;
			MultidimensionalPointResult<TPoint> multidimensionalPointResult2;
			for (;;)
			{
				cancelToken.ThrowIfCancellationRequested();
				int num2;
				try
				{
					MultidimensionalPointResult<TPoint> multidimensionalPointResult = this.TryGetOrCreatePoint(coordinates);
					if (multidimensionalPointResult.IsSuccess)
					{
						multidimensionalPointResult2 = multidimensionalPointResult;
						break;
					}
					num2 = (infiniteTimeout ? sleepMillis : Math.Min(stopMillis - Environment.TickCount, sleepMillis));
					if (num2 < 0)
					{
						multidimensionalPointResult.SetAsyncTimeoutReachedFailure();
						multidimensionalPointResult2 = multidimensionalPointResult;
						break;
					}
				}
				catch (Exception ex2)
				{
					if (!MultidimensionalCube<TDimensionValue, TPoint>.IsThrownByPointsFactoryKey(ex2))
					{
						ExceptionDispatchInfo.Capture(ex2).Throw();
					}
					num2 = (infiniteTimeout ? sleepMillis : Math.Min(stopMillis - Environment.TickCount, sleepMillis));
					if (num2 < 0)
					{
						ExceptionDispatchInfo.Capture(ex2).Throw();
					}
				}
				await Task.Delay(num2, cancelToken).ConfigureAwait(true);
			}
			return multidimensionalPointResult2;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000AD00 File Offset: 0x00008F00
		internal TPoint InvokePointsFactory(TDimensionValue[] coordinates)
		{
			TPoint tpoint;
			try
			{
				tpoint = this.pointsFactory(coordinates);
			}
			catch (Exception ex)
			{
				ex.Data["Microsoft.ApplicationInsights.ConcurrentDatastructures.MultidimensionalCube.ExceptionThrownByPointsFactory"] = bool.TrueString;
				ExceptionDispatchInfo.Capture(ex).Throw();
				tpoint = default(TPoint);
			}
			return tpoint;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000AD58 File Offset: 0x00008F58
		internal bool TryIncTotalPointsCount()
		{
			if (Interlocked.Increment(ref this.totalPointsCount) <= this.totalPointsCountLimit)
			{
				return true;
			}
			Interlocked.Decrement(ref this.totalPointsCount);
			return false;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000AD7C File Offset: 0x00008F7C
		internal int DecTotalPointsCount()
		{
			return Interlocked.Decrement(ref this.totalPointsCount);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000AD8C File Offset: 0x00008F8C
		private static bool IsThrownByPointsFactoryKey(Exception exception)
		{
			IDictionary dictionary = ((exception != null) ? exception.Data : null);
			if (dictionary == null)
			{
				return false;
			}
			object obj = dictionary["Microsoft.ApplicationInsights.ConcurrentDatastructures.MultidimensionalCube.ExceptionThrownByPointsFactory"];
			return obj != null && bool.TrueString.Equals(obj);
		}

		// Token: 0x040000E4 RID: 228
		private const int DimensionsCountLimit = 50;

		// Token: 0x040000E5 RID: 229
		private const string ExceptionThrownByPointsFactoryKey = "Microsoft.ApplicationInsights.ConcurrentDatastructures.MultidimensionalCube.ExceptionThrownByPointsFactory";

		// Token: 0x040000E6 RID: 230
		private readonly int[] subdimensionsCountLimits;

		// Token: 0x040000E7 RID: 231
		private readonly MultidimensionalCubeDimension<TDimensionValue, TPoint> points;

		// Token: 0x040000E8 RID: 232
		private readonly Func<TDimensionValue[], TPoint> pointsFactory;

		// Token: 0x040000E9 RID: 233
		private readonly int totalPointsCountLimit;

		// Token: 0x040000EA RID: 234
		private int totalPointsCount;
	}
}
