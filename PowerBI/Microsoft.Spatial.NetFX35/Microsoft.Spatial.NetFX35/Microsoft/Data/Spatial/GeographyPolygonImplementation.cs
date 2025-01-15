using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000058 RID: 88
	internal class GeographyPolygonImplementation : GeographyPolygon
	{
		// Token: 0x0600024E RID: 590 RVA: 0x0000665E File Offset: 0x0000485E
		internal GeographyPolygonImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeographyLineString[] rings)
			: base(coordinateSystem, creator)
		{
			this.rings = rings ?? new GeographyLineString[0];
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00006679 File Offset: 0x00004879
		internal GeographyPolygonImplementation(SpatialImplementation creator, params GeographyLineString[] rings)
			: this(CoordinateSystem.DefaultGeography, creator, rings)
		{
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00006688 File Offset: 0x00004888
		public override bool IsEmpty
		{
			get
			{
				return this.rings.Length == 0;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00006695 File Offset: 0x00004895
		public override ReadOnlyCollection<GeographyLineString> Rings
		{
			get
			{
				return new ReadOnlyCollection<GeographyLineString>(this.rings);
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000066A4 File Offset: 0x000048A4
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeographyPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeography(SpatialType.Polygon);
			for (int i = 0; i < this.rings.Length; i++)
			{
				this.rings[i].SendFigure(pipeline);
			}
			pipeline.EndGeography();
		}

		// Token: 0x0400006F RID: 111
		private GeographyLineString[] rings;
	}
}
