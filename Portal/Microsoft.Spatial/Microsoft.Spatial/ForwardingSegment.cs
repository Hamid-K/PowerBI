using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200006F RID: 111
	internal class ForwardingSegment : SpatialPipeline
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x0000733F File Offset: 0x0000553F
		public ForwardingSegment(SpatialPipeline current)
		{
			this.current = current;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00007359 File Offset: 0x00005559
		public ForwardingSegment(GeographyPipeline currentGeography, GeometryPipeline currentGeometry)
			: this(new SpatialPipeline(currentGeography, currentGeometry))
		{
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00007368 File Offset: 0x00005568
		public override GeographyPipeline GeographyPipeline
		{
			get
			{
				ForwardingSegment.GeographyForwarder geographyForwarder;
				if ((geographyForwarder = this.geographyForwarder) == null)
				{
					geographyForwarder = (this.geographyForwarder = new ForwardingSegment.GeographyForwarder(this));
				}
				return geographyForwarder;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00007390 File Offset: 0x00005590
		public override GeometryPipeline GeometryPipeline
		{
			get
			{
				ForwardingSegment.GeometryForwarder geometryForwarder;
				if ((geometryForwarder = this.geometryForwarder) == null)
				{
					geometryForwarder = (this.geometryForwarder = new ForwardingSegment.GeometryForwarder(this));
				}
				return geometryForwarder;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000073B6 File Offset: 0x000055B6
		public GeographyPipeline NextDrawGeography
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000073C3 File Offset: 0x000055C3
		public GeometryPipeline NextDrawGeometry
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000073D0 File Offset: 0x000055D0
		public override SpatialPipeline ChainTo(SpatialPipeline destination)
		{
			Util.CheckArgumentNull(destination, "destination");
			this.next = destination;
			destination.StartingLink = base.StartingLink;
			return destination;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000073F4 File Offset: 0x000055F4
		private static void DoAction(Action handler, Action handlerReset, Action delegation, Action delegationReset)
		{
			try
			{
				handler();
			}
			catch (Exception ex)
			{
				if (Util.IsCatchableExceptionType(ex))
				{
					handlerReset();
					delegationReset();
				}
				throw;
			}
			try
			{
				delegation();
			}
			catch (Exception ex2)
			{
				if (Util.IsCatchableExceptionType(ex2))
				{
					handlerReset();
				}
				throw;
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00007458 File Offset: 0x00005658
		private static void DoAction<T>(Action<T> handler, Action handlerReset, Action<T> delegation, Action delegationReset, T argument)
		{
			try
			{
				handler(argument);
			}
			catch (Exception ex)
			{
				if (Util.IsCatchableExceptionType(ex))
				{
					handlerReset();
					delegationReset();
				}
				throw;
			}
			try
			{
				delegation(argument);
			}
			catch (Exception ex2)
			{
				if (Util.IsCatchableExceptionType(ex2))
				{
					handlerReset();
				}
				throw;
			}
		}

		// Token: 0x040000D3 RID: 211
		internal static readonly SpatialPipeline SpatialPipelineNoOp = new SpatialPipeline(new ForwardingSegment.NoOpGeographyPipeline(), new ForwardingSegment.NoOpGeometryPipeline());

		// Token: 0x040000D4 RID: 212
		private readonly SpatialPipeline current;

		// Token: 0x040000D5 RID: 213
		private SpatialPipeline next = ForwardingSegment.SpatialPipelineNoOp;

		// Token: 0x040000D6 RID: 214
		private ForwardingSegment.GeographyForwarder geographyForwarder;

		// Token: 0x040000D7 RID: 215
		private ForwardingSegment.GeometryForwarder geometryForwarder;

		// Token: 0x02000093 RID: 147
		internal class GeographyForwarder : GeographyPipeline
		{
			// Token: 0x060003F7 RID: 1015 RVA: 0x00009F50 File Offset: 0x00008150
			public GeographyForwarder(ForwardingSegment segment)
			{
				this.segment = segment;
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00009F5F File Offset: 0x0000815F
			private GeographyPipeline Current
			{
				get
				{
					return this.segment.current;
				}
			}

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00009F71 File Offset: 0x00008171
			private GeographyPipeline Next
			{
				get
				{
					return this.segment.next;
				}
			}

			// Token: 0x060003FA RID: 1018 RVA: 0x00009F83 File Offset: 0x00008183
			public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
				this.DoAction<CoordinateSystem>(delegate(CoordinateSystem val)
				{
					this.Current.SetCoordinateSystem(val);
				}, delegate(CoordinateSystem val)
				{
					this.Next.SetCoordinateSystem(val);
				}, coordinateSystem);
			}

			// Token: 0x060003FB RID: 1019 RVA: 0x00009FA4 File Offset: 0x000081A4
			public override void BeginGeography(SpatialType type)
			{
				this.DoAction<SpatialType>(delegate(SpatialType val)
				{
					this.Current.BeginGeography(val);
				}, delegate(SpatialType val)
				{
					this.Next.BeginGeography(val);
				}, type);
			}

			// Token: 0x060003FC RID: 1020 RVA: 0x00009FC5 File Offset: 0x000081C5
			public override void EndGeography()
			{
				this.DoAction(new Action(this.Current.EndGeography), new Action(this.Next.EndGeography));
			}

			// Token: 0x060003FD RID: 1021 RVA: 0x00009FF1 File Offset: 0x000081F1
			public override void BeginFigure(GeographyPosition position)
			{
				Util.CheckArgumentNull(position, "position");
				this.DoAction<GeographyPosition>(delegate(GeographyPosition val)
				{
					this.Current.BeginFigure(val);
				}, delegate(GeographyPosition val)
				{
					this.Next.BeginFigure(val);
				}, position);
			}

			// Token: 0x060003FE RID: 1022 RVA: 0x0000A01D File Offset: 0x0000821D
			public override void EndFigure()
			{
				this.DoAction(new Action(this.Current.EndFigure), new Action(this.Next.EndFigure));
			}

			// Token: 0x060003FF RID: 1023 RVA: 0x0000A049 File Offset: 0x00008249
			public override void LineTo(GeographyPosition position)
			{
				Util.CheckArgumentNull(position, "position");
				this.DoAction<GeographyPosition>(delegate(GeographyPosition val)
				{
					this.Current.LineTo(val);
				}, delegate(GeographyPosition val)
				{
					this.Next.LineTo(val);
				}, position);
			}

			// Token: 0x06000400 RID: 1024 RVA: 0x0000A075 File Offset: 0x00008275
			public override void Reset()
			{
				this.DoAction(new Action(this.Current.Reset), new Action(this.Next.Reset));
			}

			// Token: 0x06000401 RID: 1025 RVA: 0x0000A0A1 File Offset: 0x000082A1
			private void DoAction<T>(Action<T> handler, Action<T> delegation, T argument)
			{
				ForwardingSegment.DoAction<T>(handler, new Action(this.Current.Reset), delegation, new Action(this.Next.Reset), argument);
			}

			// Token: 0x06000402 RID: 1026 RVA: 0x0000A0CF File Offset: 0x000082CF
			private void DoAction(Action handler, Action delegation)
			{
				ForwardingSegment.DoAction(handler, new Action(this.Current.Reset), delegation, new Action(this.Next.Reset));
			}

			// Token: 0x0400015F RID: 351
			private readonly ForwardingSegment segment;
		}

		// Token: 0x02000094 RID: 148
		internal class GeometryForwarder : GeometryPipeline
		{
			// Token: 0x0600040B RID: 1035 RVA: 0x0000A16C File Offset: 0x0000836C
			public GeometryForwarder(ForwardingSegment segment)
			{
				this.segment = segment;
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000A17B File Offset: 0x0000837B
			private GeometryPipeline Current
			{
				get
				{
					return this.segment.current;
				}
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000A18D File Offset: 0x0000838D
			private GeometryPipeline Next
			{
				get
				{
					return this.segment.next;
				}
			}

			// Token: 0x0600040E RID: 1038 RVA: 0x0000A19F File Offset: 0x0000839F
			public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
				this.DoAction<CoordinateSystem>(delegate(CoordinateSystem val)
				{
					this.Current.SetCoordinateSystem(val);
				}, delegate(CoordinateSystem val)
				{
					this.Next.SetCoordinateSystem(val);
				}, coordinateSystem);
			}

			// Token: 0x0600040F RID: 1039 RVA: 0x0000A1C0 File Offset: 0x000083C0
			public override void BeginGeometry(SpatialType type)
			{
				this.DoAction<SpatialType>(delegate(SpatialType val)
				{
					this.Current.BeginGeometry(val);
				}, delegate(SpatialType val)
				{
					this.Next.BeginGeometry(val);
				}, type);
			}

			// Token: 0x06000410 RID: 1040 RVA: 0x0000A1E1 File Offset: 0x000083E1
			public override void EndGeometry()
			{
				this.DoAction(new Action(this.Current.EndGeometry), new Action(this.Next.EndGeometry));
			}

			// Token: 0x06000411 RID: 1041 RVA: 0x0000A20D File Offset: 0x0000840D
			public override void BeginFigure(GeometryPosition position)
			{
				Util.CheckArgumentNull(position, "position");
				this.DoAction<GeometryPosition>(delegate(GeometryPosition val)
				{
					this.Current.BeginFigure(val);
				}, delegate(GeometryPosition val)
				{
					this.Next.BeginFigure(val);
				}, position);
			}

			// Token: 0x06000412 RID: 1042 RVA: 0x0000A239 File Offset: 0x00008439
			public override void EndFigure()
			{
				this.DoAction(new Action(this.Current.EndFigure), new Action(this.Next.EndFigure));
			}

			// Token: 0x06000413 RID: 1043 RVA: 0x0000A265 File Offset: 0x00008465
			public override void LineTo(GeometryPosition position)
			{
				Util.CheckArgumentNull(position, "position");
				this.DoAction<GeometryPosition>(delegate(GeometryPosition val)
				{
					this.Current.LineTo(val);
				}, delegate(GeometryPosition val)
				{
					this.Next.LineTo(val);
				}, position);
			}

			// Token: 0x06000414 RID: 1044 RVA: 0x0000A291 File Offset: 0x00008491
			public override void Reset()
			{
				this.DoAction(new Action(this.Current.Reset), new Action(this.Next.Reset));
			}

			// Token: 0x06000415 RID: 1045 RVA: 0x0000A2BD File Offset: 0x000084BD
			private void DoAction<T>(Action<T> handler, Action<T> delegation, T argument)
			{
				ForwardingSegment.DoAction<T>(handler, new Action(this.Current.Reset), delegation, new Action(this.Next.Reset), argument);
			}

			// Token: 0x06000416 RID: 1046 RVA: 0x0000A2EB File Offset: 0x000084EB
			private void DoAction(Action handler, Action delegation)
			{
				ForwardingSegment.DoAction(handler, new Action(this.Current.Reset), delegation, new Action(this.Next.Reset));
			}

			// Token: 0x04000160 RID: 352
			private readonly ForwardingSegment segment;
		}

		// Token: 0x02000095 RID: 149
		private class NoOpGeographyPipeline : GeographyPipeline
		{
			// Token: 0x0600041F RID: 1055 RVA: 0x0000537A File Offset: 0x0000357A
			public override void LineTo(GeographyPosition position)
			{
			}

			// Token: 0x06000420 RID: 1056 RVA: 0x0000537A File Offset: 0x0000357A
			public override void BeginFigure(GeographyPosition position)
			{
			}

			// Token: 0x06000421 RID: 1057 RVA: 0x0000537A File Offset: 0x0000357A
			public override void BeginGeography(SpatialType type)
			{
			}

			// Token: 0x06000422 RID: 1058 RVA: 0x0000537A File Offset: 0x0000357A
			public override void EndFigure()
			{
			}

			// Token: 0x06000423 RID: 1059 RVA: 0x0000537A File Offset: 0x0000357A
			public override void EndGeography()
			{
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x0000537A File Offset: 0x0000357A
			public override void Reset()
			{
			}

			// Token: 0x06000425 RID: 1061 RVA: 0x0000537A File Offset: 0x0000357A
			public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
			}
		}

		// Token: 0x02000096 RID: 150
		private class NoOpGeometryPipeline : GeometryPipeline
		{
			// Token: 0x06000427 RID: 1063 RVA: 0x0000537A File Offset: 0x0000357A
			public override void LineTo(GeometryPosition position)
			{
			}

			// Token: 0x06000428 RID: 1064 RVA: 0x0000537A File Offset: 0x0000357A
			public override void BeginFigure(GeometryPosition position)
			{
			}

			// Token: 0x06000429 RID: 1065 RVA: 0x0000537A File Offset: 0x0000357A
			public override void BeginGeometry(SpatialType type)
			{
			}

			// Token: 0x0600042A RID: 1066 RVA: 0x0000537A File Offset: 0x0000357A
			public override void EndFigure()
			{
			}

			// Token: 0x0600042B RID: 1067 RVA: 0x0000537A File Offset: 0x0000357A
			public override void EndGeometry()
			{
			}

			// Token: 0x0600042C RID: 1068 RVA: 0x0000537A File Offset: 0x0000357A
			public override void Reset()
			{
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x0000537A File Offset: 0x0000357A
			public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
			}
		}
	}
}
