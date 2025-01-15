using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000036 RID: 54
	public abstract class SpatialFormatter<TReaderStream, TWriterStream>
	{
		// Token: 0x06000173 RID: 371 RVA: 0x000044AA File Offset: 0x000026AA
		protected SpatialFormatter(SpatialImplementation creator)
		{
			Util.CheckArgumentNull(creator, "creator");
			this.creator = creator;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000044C4 File Offset: 0x000026C4
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

		// Token: 0x06000175 RID: 373 RVA: 0x00004520 File Offset: 0x00002720
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

		// Token: 0x06000176 RID: 374 RVA: 0x00004550 File Offset: 0x00002750
		public void Write(ISpatial spatial, TWriterStream writerStream)
		{
			SpatialPipeline spatialPipeline = this.CreateWriter(writerStream);
			spatial.SendTo(spatialPipeline);
		}

		// Token: 0x06000177 RID: 375
		public abstract SpatialPipeline CreateWriter(TWriterStream writerStream);

		// Token: 0x06000178 RID: 376
		protected abstract void ReadGeography(TReaderStream readerStream, SpatialPipeline pipeline);

		// Token: 0x06000179 RID: 377
		protected abstract void ReadGeometry(TReaderStream readerStream, SpatialPipeline pipeline);

		// Token: 0x0600017A RID: 378 RVA: 0x0000456C File Offset: 0x0000276C
		protected KeyValuePair<SpatialPipeline, IShapeProvider> MakeValidatingBuilder()
		{
			SpatialBuilder spatialBuilder = this.creator.CreateBuilder();
			SpatialPipeline spatialPipeline = this.creator.CreateValidator();
			spatialPipeline.ChainTo(spatialBuilder);
			return new KeyValuePair<SpatialPipeline, IShapeProvider>(spatialPipeline, spatialBuilder);
		}

		// Token: 0x04000022 RID: 34
		private readonly SpatialImplementation creator;
	}
}
