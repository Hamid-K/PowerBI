using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000054 RID: 84
	internal class GeometryMultiLineStringImplementation : GeometryMultiLineString
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00005560 File Offset: 0x00003760
		internal GeometryMultiLineStringImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryLineString[] lineStrings)
			: base(coordinateSystem, creator)
		{
			this.lineStrings = lineStrings ?? new GeometryLineString[0];
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000557B File Offset: 0x0000377B
		internal GeometryMultiLineStringImplementation(SpatialImplementation creator, params GeometryLineString[] lineStrings)
			: this(CoordinateSystem.DefaultGeometry, creator, lineStrings)
		{
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000558A File Offset: 0x0000378A
		public override bool IsEmpty
		{
			get
			{
				return this.lineStrings.Length == 0;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00005596 File Offset: 0x00003796
		public override ReadOnlyCollection<Geometry> Geometries
		{
			get
			{
				return new ReadOnlyCollection<Geometry>(this.lineStrings);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000055A3 File Offset: 0x000037A3
		public override ReadOnlyCollection<GeometryLineString> LineStrings
		{
			get
			{
				return new ReadOnlyCollection<GeometryLineString>(this.lineStrings);
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000055B0 File Offset: 0x000037B0
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeometryPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeometry(SpatialType.MultiLineString);
			for (int i = 0; i < this.lineStrings.Length; i++)
			{
				this.lineStrings[i].SendTo(pipeline);
			}
			pipeline.EndGeometry();
		}

		// Token: 0x0400005C RID: 92
		private GeometryLineString[] lineStrings;
	}
}
