using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000011 RID: 17
	internal abstract class SpatialReader<TSource>
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00002DEB File Offset: 0x00000FEB
		protected SpatialReader(SpatialPipeline destination)
		{
			Util.CheckArgumentNull(destination, "destination");
			this.Destination = destination;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002E05 File Offset: 0x00001005
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00002E0D File Offset: 0x0000100D
		protected SpatialPipeline Destination { get; set; }

		// Token: 0x060000BD RID: 189 RVA: 0x00002E18 File Offset: 0x00001018
		[SuppressMessage("DataWeb.Usage", "AC0014:DoNotHandleProhibitedExceptionsRule", Justification = "We're calling this correctly")]
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

		// Token: 0x060000BE RID: 190 RVA: 0x00002E68 File Offset: 0x00001068
		[SuppressMessage("DataWeb.Usage", "AC0014:DoNotHandleProhibitedExceptionsRule", Justification = "We're calling this correctly")]
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

		// Token: 0x060000BF RID: 191 RVA: 0x00002EB8 File Offset: 0x000010B8
		public virtual void Reset()
		{
			this.Destination.Reset();
			this.Destination.Reset();
		}

		// Token: 0x060000C0 RID: 192
		protected abstract void ReadGeometryImplementation(TSource input);

		// Token: 0x060000C1 RID: 193
		protected abstract void ReadGeographyImplementation(TSource input);
	}
}
