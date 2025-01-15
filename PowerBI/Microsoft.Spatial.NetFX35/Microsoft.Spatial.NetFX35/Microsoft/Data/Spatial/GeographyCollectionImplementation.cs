using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000059 RID: 89
	internal class GeographyCollectionImplementation : GeographyCollection
	{
		// Token: 0x06000253 RID: 595 RVA: 0x000066E6 File Offset: 0x000048E6
		internal GeographyCollectionImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params Geography[] geography)
			: base(coordinateSystem, creator)
		{
			this.geographyArray = geography ?? new Geography[0];
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00006701 File Offset: 0x00004901
		internal GeographyCollectionImplementation(SpatialImplementation creator, params Geography[] geography)
			: this(CoordinateSystem.DefaultGeography, creator, geography)
		{
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00006710 File Offset: 0x00004910
		public override bool IsEmpty
		{
			get
			{
				return this.geographyArray.Length == 0;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000671D File Offset: 0x0000491D
		public override ReadOnlyCollection<Geography> Geographies
		{
			get
			{
				return new ReadOnlyCollection<Geography>(this.geographyArray);
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000672C File Offset: 0x0000492C
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeographyPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeography(SpatialType.Collection);
			for (int i = 0; i < this.geographyArray.Length; i++)
			{
				this.geographyArray[i].SendTo(pipeline);
			}
			pipeline.EndGeography();
		}

		// Token: 0x04000070 RID: 112
		private Geography[] geographyArray;
	}
}
