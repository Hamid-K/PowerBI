using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000031 RID: 49
	internal abstract class SpatialReader<TSource>
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00003A54 File Offset: 0x00001C54
		protected SpatialReader(SpatialPipeline destination)
		{
			Util.CheckArgumentNull(destination, "destination");
			this.Destination = destination;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00003A6E File Offset: 0x00001C6E
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00003A76 File Offset: 0x00001C76
		protected SpatialPipeline Destination { get; set; }

		// Token: 0x0600012E RID: 302 RVA: 0x00003A80 File Offset: 0x00001C80
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

		// Token: 0x0600012F RID: 303 RVA: 0x00003AD0 File Offset: 0x00001CD0
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

		// Token: 0x06000130 RID: 304 RVA: 0x00003B20 File Offset: 0x00001D20
		public virtual void Reset()
		{
			this.Destination.Reset();
			this.Destination.Reset();
		}

		// Token: 0x06000131 RID: 305
		protected abstract void ReadGeometryImplementation(TSource input);

		// Token: 0x06000132 RID: 306
		protected abstract void ReadGeographyImplementation(TSource input);
	}
}
