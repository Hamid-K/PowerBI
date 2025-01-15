using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000036 RID: 54
	internal abstract class SpatialReader<TSource>
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00004720 File Offset: 0x00002920
		protected SpatialReader(SpatialPipeline destination)
		{
			Util.CheckArgumentNull(destination, "destination");
			this.Destination = destination;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000473A File Offset: 0x0000293A
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00004742 File Offset: 0x00002942
		protected SpatialPipeline Destination { get; set; }

		// Token: 0x060001A4 RID: 420 RVA: 0x0000474C File Offset: 0x0000294C
		public void ReadGeography(TSource input)
		{
			Util.CheckArgumentNull(input, "input");
			try
			{
				this.ReadGeographyImplementation(input);
			}
			catch (Exception ex)
			{
				if (Util.IsCatchableExceptionType(ex))
				{
					throw new ParseErrorException(ex.Message, ex);
				}
				throw;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000479C File Offset: 0x0000299C
		public void ReadGeometry(TSource input)
		{
			Util.CheckArgumentNull(input, "input");
			try
			{
				this.ReadGeometryImplementation(input);
			}
			catch (Exception ex)
			{
				if (Util.IsCatchableExceptionType(ex))
				{
					throw new ParseErrorException(ex.Message, ex);
				}
				throw;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000047EC File Offset: 0x000029EC
		public virtual void Reset()
		{
			this.Destination.Reset();
			this.Destination.Reset();
		}

		// Token: 0x060001A7 RID: 423
		protected abstract void ReadGeometryImplementation(TSource input);

		// Token: 0x060001A8 RID: 424
		protected abstract void ReadGeographyImplementation(TSource input);
	}
}
