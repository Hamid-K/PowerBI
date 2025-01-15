using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000035 RID: 53
	public abstract class SpatialFormatter<TReaderStream, TWriterStream>
	{
		// Token: 0x06000199 RID: 409 RVA: 0x0000462A File Offset: 0x0000282A
		protected SpatialFormatter(SpatialImplementation creator)
		{
			Util.CheckArgumentNull(creator, "creator");
			this.creator = creator;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00004644 File Offset: 0x00002844
		public TResult Read<TResult>(TReaderStream input) where TResult : class, ISpatial
		{
			KeyValuePair<SpatialPipeline, IShapeProvider> keyValuePair = this.MakeValidatingBuilder();
			IShapeProvider value = keyValuePair.Value;
			this.Read<TResult>(input, keyValuePair.Key);
			if (typeof(Geometry).IsAssignableFrom(typeof(TResult)))
			{
				return (TResult)((object)value.ConstructedGeometry);
			}
			return (TResult)((object)value.ConstructedGeography);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000046A0 File Offset: 0x000028A0
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "The type hierarchy is too deep to have a specificly typed Read for each of them.")]
		public void Read<TResult>(TReaderStream input, SpatialPipeline pipeline) where TResult : class, ISpatial
		{
			if (typeof(Geometry).IsAssignableFrom(typeof(TResult)))
			{
				this.ReadGeometry(input, pipeline);
				return;
			}
			this.ReadGeography(input, pipeline);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000046D0 File Offset: 0x000028D0
		public void Write(ISpatial spatial, TWriterStream writerStream)
		{
			SpatialPipeline spatialPipeline = this.CreateWriter(writerStream);
			spatial.SendTo(spatialPipeline);
		}

		// Token: 0x0600019D RID: 413
		public abstract SpatialPipeline CreateWriter(TWriterStream writerStream);

		// Token: 0x0600019E RID: 414
		protected abstract void ReadGeography(TReaderStream readerStream, SpatialPipeline pipeline);

		// Token: 0x0600019F RID: 415
		protected abstract void ReadGeometry(TReaderStream readerStream, SpatialPipeline pipeline);

		// Token: 0x060001A0 RID: 416 RVA: 0x000046EC File Offset: 0x000028EC
		protected KeyValuePair<SpatialPipeline, IShapeProvider> MakeValidatingBuilder()
		{
			SpatialBuilder spatialBuilder = this.creator.CreateBuilder();
			SpatialPipeline spatialPipeline = this.creator.CreateValidator();
			spatialPipeline.ChainTo(spatialBuilder);
			return new KeyValuePair<SpatialPipeline, IShapeProvider>(spatialPipeline, spatialBuilder);
		}

		// Token: 0x0400002A RID: 42
		private readonly SpatialImplementation creator;
	}
}
