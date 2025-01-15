using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000054 RID: 84
	internal class GeographyMultiLineStringImplementation : GeographyMultiLineString
	{
		// Token: 0x06000234 RID: 564 RVA: 0x00006354 File Offset: 0x00004554
		internal GeographyMultiLineStringImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeographyLineString[] lineStrings)
			: base(coordinateSystem, creator)
		{
			this.lineStrings = lineStrings ?? new GeographyLineString[0];
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000636F File Offset: 0x0000456F
		internal GeographyMultiLineStringImplementation(SpatialImplementation creator, params GeographyLineString[] lineStrings)
			: this(CoordinateSystem.DefaultGeography, creator, lineStrings)
		{
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000637E File Offset: 0x0000457E
		public override bool IsEmpty
		{
			get
			{
				return this.lineStrings.Length == 0;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000638B File Offset: 0x0000458B
		public override ReadOnlyCollection<Geography> Geographies
		{
			get
			{
				return new ReadOnlyCollection<Geography>(this.lineStrings);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00006398 File Offset: 0x00004598
		public override ReadOnlyCollection<GeographyLineString> LineStrings
		{
			get
			{
				return new ReadOnlyCollection<GeographyLineString>(this.lineStrings);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000063A8 File Offset: 0x000045A8
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeographyPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeography(SpatialType.MultiLineString);
			for (int i = 0; i < this.lineStrings.Length; i++)
			{
				this.lineStrings[i].SendTo(pipeline);
			}
			pipeline.EndGeography();
		}

		// Token: 0x04000068 RID: 104
		private GeographyLineString[] lineStrings;
	}
}
