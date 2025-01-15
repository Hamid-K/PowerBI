using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000050 RID: 80
	internal class GeographyCollectionImplementation : GeographyCollection
	{
		// Token: 0x060001FA RID: 506 RVA: 0x00005392 File Offset: 0x00003592
		internal GeographyCollectionImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params Geography[] geography)
			: base(coordinateSystem, creator)
		{
			this.geographyArray = geography ?? new Geography[0];
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000053AD File Offset: 0x000035AD
		internal GeographyCollectionImplementation(SpatialImplementation creator, params Geography[] geography)
			: this(CoordinateSystem.DefaultGeography, creator, geography)
		{
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000053BC File Offset: 0x000035BC
		public override bool IsEmpty
		{
			get
			{
				return this.geographyArray.Length == 0;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000053C8 File Offset: 0x000035C8
		public override ReadOnlyCollection<Geography> Geographies
		{
			get
			{
				return new ReadOnlyCollection<Geography>(this.geographyArray);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000053D8 File Offset: 0x000035D8
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

		// Token: 0x0400005A RID: 90
		private Geography[] geographyArray;
	}
}
