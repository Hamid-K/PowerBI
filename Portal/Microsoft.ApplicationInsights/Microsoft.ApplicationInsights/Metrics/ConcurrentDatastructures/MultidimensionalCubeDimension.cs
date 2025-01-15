using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.ApplicationInsights.Metrics.ConcurrentDatastructures
{
	// Token: 0x02000038 RID: 56
	internal class MultidimensionalCubeDimension<TDimensionValue, TPoint>
	{
		// Token: 0x06000216 RID: 534 RVA: 0x0000B4FD File Offset: 0x000096FD
		public MultidimensionalCubeDimension(MultidimensionalCube<TDimensionValue, TPoint> ownerCube, int subdimensionsCountLimit, bool isLastDimensionLevel)
		{
			this.ownerCube = ownerCube;
			this.subdimensionsCountLimit = subdimensionsCountLimit;
			this.isLastDimensionLevel = isLastDimensionLevel;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000B528 File Offset: 0x00009728
		public MultidimensionalPointResult<TPoint> TryGetOrAddVector(TDimensionValue[] coordinates)
		{
			Util.ValidateNotNull(coordinates, "coordinates");
			if (coordinates.Length != this.ownerCube.DimensionsCount)
			{
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("The specified {0}-vector has {1} dimensions.", new object[] { "coordinates", coordinates.Length })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" However {0} has {1} dimensions.", new object[]
				{
					"ownerCube",
					this.ownerCube.DimensionsCount
				})), "coordinates");
			}
			return this.TryGetOrAddVectorInternal(coordinates, 0, true);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000B5C4 File Offset: 0x000097C4
		public MultidimensionalPointResult<TPoint> TryGetVector(TDimensionValue[] coordinates)
		{
			Util.ValidateNotNull(coordinates, "coordinates");
			if (coordinates.Length != this.ownerCube.DimensionsCount)
			{
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("The specified {0}-vector has {1} dimensions.", new object[] { "coordinates", coordinates.Length })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" However {0} has {1} dimensions.", new object[]
				{
					"ownerCube",
					this.ownerCube.DimensionsCount
				})), "coordinates");
			}
			return this.TryGetOrAddVectorInternal(coordinates, 0, false);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000B660 File Offset: 0x00009860
		public IReadOnlyCollection<KeyValuePair<IList<TDimensionValue>, TPoint>> GetAllPointsReversed()
		{
			List<KeyValuePair<IList<TDimensionValue>, TPoint>> list = new List<KeyValuePair<IList<TDimensionValue>, TPoint>>();
			if (this.isLastDimensionLevel)
			{
				using (IEnumerator<KeyValuePair<TDimensionValue, object>> enumerator = this.elements.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<TDimensionValue, object> keyValuePair = enumerator.Current;
						list.Add(new KeyValuePair<IList<TDimensionValue>, TPoint>(new List<TDimensionValue>(), (TPoint)((object)keyValuePair.Value))
						{
							Key = { keyValuePair.Key }
						});
					}
					return list;
				}
			}
			foreach (KeyValuePair<TDimensionValue, object> keyValuePair2 in this.elements)
			{
				foreach (KeyValuePair<IList<TDimensionValue>, TPoint> keyValuePair3 in ((MultidimensionalCubeDimension<TDimensionValue, TPoint>)keyValuePair2.Value).GetAllPointsReversed())
				{
					keyValuePair3.Key.Add(keyValuePair2.Key);
					list.Add(keyValuePair3);
				}
			}
			return list;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000B784 File Offset: 0x00009984
		private MultidimensionalPointResult<TPoint> TryGetOrAddVectorInternal(TDimensionValue[] coordinates, int currentDim, bool createIfNotExists)
		{
			TDimensionValue tdimensionValue = coordinates[currentDim];
			object obj;
			if (this.elements.TryGetValue(tdimensionValue, out obj))
			{
				if (this.isLastDimensionLevel)
				{
					return new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Success_ExistingPointRetrieved, (TPoint)((object)obj));
				}
				return ((MultidimensionalCubeDimension<TDimensionValue, TPoint>)obj).TryGetOrAddVectorInternal(coordinates, currentDim + 1, createIfNotExists);
			}
			else
			{
				if (!createIfNotExists)
				{
					return new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Failure_PointDoesNotExistCreationNotRequested, currentDim);
				}
				if (!this.isLastDimensionLevel)
				{
					return this.TryAddSubvector(coordinates, currentDim);
				}
				return this.TryAddPoint(coordinates, currentDim);
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000B7F4 File Offset: 0x000099F4
		private MultidimensionalPointResult<TPoint> TryAddPoint(TDimensionValue[] coordinates, int currentDim)
		{
			if (!this.TryIncSubdimensionsCount())
			{
				return new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Failure_SubdimensionsCountLimitReached, currentDim);
			}
			bool flag = true;
			MultidimensionalPointResult<TPoint> multidimensionalPointResult;
			try
			{
				if (!this.ownerCube.TryIncTotalPointsCount())
				{
					multidimensionalPointResult = new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Failure_TotalPointsCountLimitReached, -1);
				}
				else
				{
					bool flag2 = true;
					try
					{
						TPoint tpoint = this.ownerCube.InvokePointsFactory(coordinates);
						TDimensionValue tdimensionValue = coordinates[currentDim];
						if (this.elements.TryAdd(tdimensionValue, tpoint))
						{
							flag2 = false;
							flag = false;
							multidimensionalPointResult = new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Success_NewPointCreated, tpoint);
						}
						else
						{
							TPoint tpoint2 = (TPoint)((object)this.elements[tdimensionValue]);
							multidimensionalPointResult = new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Success_ExistingPointRetrieved, tpoint2);
						}
					}
					finally
					{
						if (flag2)
						{
							this.ownerCube.DecTotalPointsCount();
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					this.DecSubdimensionsCount();
				}
			}
			return multidimensionalPointResult;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000B8C0 File Offset: 0x00009AC0
		private MultidimensionalPointResult<TPoint> TryAddSubvector(TDimensionValue[] coordinates, int currentDim)
		{
			if (!this.TryIncSubdimensionsCount())
			{
				return new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Failure_SubdimensionsCountLimitReached, currentDim);
			}
			bool flag = true;
			try
			{
				TDimensionValue tdimensionValue = coordinates[currentDim];
				if (this.ownerCube.TotalPointsCount >= this.ownerCube.TotalPointsCountLimit)
				{
					return new MultidimensionalPointResult<TPoint>(MultidimensionalPointResultCodes.Failure_TotalPointsCountLimitReached, -1);
				}
				int num = currentDim + 1;
				bool flag2 = num == coordinates.Length - 1;
				MultidimensionalCubeDimension<TDimensionValue, TPoint> multidimensionalCubeDimension = new MultidimensionalCubeDimension<TDimensionValue, TPoint>(this.ownerCube, this.ownerCube.GetSubdimensionsCountLimit(num), flag2);
				MultidimensionalPointResult<TPoint> multidimensionalPointResult = multidimensionalCubeDimension.TryGetOrAddVectorInternal(coordinates, num, true);
				if (!multidimensionalPointResult.IsSuccess)
				{
					return multidimensionalPointResult;
				}
				if (this.elements.TryAdd(tdimensionValue, multidimensionalCubeDimension))
				{
					flag = false;
					return multidimensionalPointResult;
				}
				this.ownerCube.DecTotalPointsCount();
			}
			finally
			{
				if (flag)
				{
					this.DecSubdimensionsCount();
				}
			}
			return this.TryGetOrAddVectorInternal(coordinates, currentDim, true);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000B99C File Offset: 0x00009B9C
		private bool TryIncSubdimensionsCount()
		{
			if (Interlocked.Increment(ref this.subdimensionsCount) <= this.subdimensionsCountLimit)
			{
				return true;
			}
			Interlocked.Decrement(ref this.subdimensionsCount);
			return false;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		private int DecSubdimensionsCount()
		{
			return Interlocked.Decrement(ref this.subdimensionsCount);
		}

		// Token: 0x040000F5 RID: 245
		private readonly MultidimensionalCube<TDimensionValue, TPoint> ownerCube;

		// Token: 0x040000F6 RID: 246
		private readonly int subdimensionsCountLimit;

		// Token: 0x040000F7 RID: 247
		private readonly bool isLastDimensionLevel;

		// Token: 0x040000F8 RID: 248
		private ConcurrentDictionary<TDimensionValue, object> elements = new ConcurrentDictionary<TDimensionValue, object>();

		// Token: 0x040000F9 RID: 249
		private int subdimensionsCount;
	}
}
