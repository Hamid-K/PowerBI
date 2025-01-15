using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000030 RID: 48
	public abstract class SpatialFormatter<TReaderStream, TWriterStream>
	{
		// Token: 0x06000123 RID: 291 RVA: 0x0000395C File Offset: 0x00001B5C
		protected SpatialFormatter(SpatialImplementation creator)
		{
			Util.CheckArgumentNull(creator, "creator");
			this.creator = creator;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00003978 File Offset: 0x00001B78
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

		// Token: 0x06000125 RID: 293 RVA: 0x000039D4 File Offset: 0x00001BD4
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

		// Token: 0x06000126 RID: 294 RVA: 0x00003A04 File Offset: 0x00001C04
		public void Write(ISpatial spatial, TWriterStream writerStream)
		{
			SpatialPipeline spatialPipeline = this.CreateWriter(writerStream);
			spatial.SendTo(spatialPipeline);
		}

		// Token: 0x06000127 RID: 295
		public abstract SpatialPipeline CreateWriter(TWriterStream writerStream);

		// Token: 0x06000128 RID: 296
		protected abstract void ReadGeography(TReaderStream readerStream, SpatialPipeline pipeline);

		// Token: 0x06000129 RID: 297
		protected abstract void ReadGeometry(TReaderStream readerStream, SpatialPipeline pipeline);

		// Token: 0x0600012A RID: 298 RVA: 0x00003A20 File Offset: 0x00001C20
		protected KeyValuePair<SpatialPipeline, IShapeProvider> MakeValidatingBuilder()
		{
			SpatialBuilder spatialBuilder = this.creator.CreateBuilder();
			SpatialPipeline spatialPipeline = this.creator.CreateValidator();
			spatialPipeline.ChainTo(spatialBuilder);
			return new KeyValuePair<SpatialPipeline, IShapeProvider>(spatialPipeline, spatialBuilder);
		}

		// Token: 0x0400001D RID: 29
		private readonly SpatialImplementation creator;
	}
}
