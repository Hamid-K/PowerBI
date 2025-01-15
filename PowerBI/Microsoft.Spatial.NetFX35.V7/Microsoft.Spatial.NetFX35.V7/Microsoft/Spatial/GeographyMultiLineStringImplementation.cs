using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200004B RID: 75
	internal class GeographyMultiLineStringImplementation : GeographyMultiLineString
	{
		// Token: 0x060001DB RID: 475 RVA: 0x00005008 File Offset: 0x00003208
		internal GeographyMultiLineStringImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeographyLineString[] lineStrings)
			: base(coordinateSystem, creator)
		{
			this.lineStrings = lineStrings ?? new GeographyLineString[0];
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00005023 File Offset: 0x00003223
		internal GeographyMultiLineStringImplementation(SpatialImplementation creator, params GeographyLineString[] lineStrings)
			: this(CoordinateSystem.DefaultGeography, creator, lineStrings)
		{
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00005032 File Offset: 0x00003232
		public override bool IsEmpty
		{
			get
			{
				return this.lineStrings.Length == 0;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000503E File Offset: 0x0000323E
		public override ReadOnlyCollection<Geography> Geographies
		{
			get
			{
				return new ReadOnlyCollection<Geography>(this.lineStrings);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000504B File Offset: 0x0000324B
		public override ReadOnlyCollection<GeographyLineString> LineStrings
		{
			get
			{
				return new ReadOnlyCollection<GeographyLineString>(this.lineStrings);
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005058 File Offset: 0x00003258
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

		// Token: 0x04000052 RID: 82
		private GeographyLineString[] lineStrings;
	}
}
