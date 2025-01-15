using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000055 RID: 85
	internal class GeographyCollectionImplementation : GeographyCollection
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0000605A File Offset: 0x0000425A
		internal GeographyCollectionImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params Geography[] geography)
			: base(coordinateSystem, creator)
		{
			this.geographyArray = geography ?? new Geography[0];
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00006075 File Offset: 0x00004275
		internal GeographyCollectionImplementation(SpatialImplementation creator, params Geography[] geography)
			: this(CoordinateSystem.DefaultGeography, creator, geography)
		{
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00006084 File Offset: 0x00004284
		public override bool IsEmpty
		{
			get
			{
				return this.geographyArray.Length == 0;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00006090 File Offset: 0x00004290
		public override ReadOnlyCollection<Geography> Geographies
		{
			get
			{
				return new ReadOnlyCollection<Geography>(this.geographyArray);
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000060A0 File Offset: 0x000042A0
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

		// Token: 0x04000067 RID: 103
		private Geography[] geographyArray;
	}
}
