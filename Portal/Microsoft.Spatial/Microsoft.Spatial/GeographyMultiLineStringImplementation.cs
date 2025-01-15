using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000050 RID: 80
	internal class GeographyMultiLineStringImplementation : GeographyMultiLineString
	{
		// Token: 0x06000251 RID: 593 RVA: 0x00005CD0 File Offset: 0x00003ED0
		internal GeographyMultiLineStringImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeographyLineString[] lineStrings)
			: base(coordinateSystem, creator)
		{
			this.lineStrings = lineStrings ?? new GeographyLineString[0];
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00005CEB File Offset: 0x00003EEB
		internal GeographyMultiLineStringImplementation(SpatialImplementation creator, params GeographyLineString[] lineStrings)
			: this(CoordinateSystem.DefaultGeography, creator, lineStrings)
		{
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00005CFA File Offset: 0x00003EFA
		public override bool IsEmpty
		{
			get
			{
				return this.lineStrings.Length == 0;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00005D06 File Offset: 0x00003F06
		public override ReadOnlyCollection<Geography> Geographies
		{
			get
			{
				return new ReadOnlyCollection<Geography>(this.lineStrings);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00005D13 File Offset: 0x00003F13
		public override ReadOnlyCollection<GeographyLineString> LineStrings
		{
			get
			{
				return new ReadOnlyCollection<GeographyLineString>(this.lineStrings);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00005D20 File Offset: 0x00003F20
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

		// Token: 0x0400005F RID: 95
		private GeographyLineString[] lineStrings;
	}
}
