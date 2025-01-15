using System;

namespace Microsoft.Spatial
{
	// Token: 0x0200006A RID: 106
	internal class ForwardingSegment : SpatialPipeline
	{
		// Token: 0x06000281 RID: 641 RVA: 0x00006677 File Offset: 0x00004877
		public ForwardingSegment(SpatialPipeline current)
		{
			this.current = current;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00006691 File Offset: 0x00004891
		public ForwardingSegment(GeographyPipeline currentGeography, GeometryPipeline currentGeometry)
			: this(new SpatialPipeline(currentGeography, currentGeometry))
		{
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000283 RID: 643 RVA: 0x000066A0 File Offset: 0x000048A0
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

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000284 RID: 644 RVA: 0x000066C8 File Offset: 0x000048C8
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

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000285 RID: 645 RVA: 0x000066EE File Offset: 0x000048EE
		public GeographyPipeline NextDrawGeography
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000286 RID: 646 RVA: 0x000066FB File Offset: 0x000048FB
		public GeometryPipeline NextDrawGeometry
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00006708 File Offset: 0x00004908
		public override SpatialPipeline ChainTo(SpatialPipeline destination)
		{
			Util.CheckArgumentNull(destination, "destination");
			this.next = destination;
			destination.StartingLink = base.StartingLink;
			return destination;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000672C File Offset: 0x0000492C
		private static void DoAction(Action handler, Action handlerReset, Action delegation, Action delegationReset)
		{
			try
			{
				handler.Invoke();
			}
			catch (Exception ex)
			{
				if (Util.IsCatchableExceptionType(ex))
				{
					handlerReset.Invoke();
					delegationReset.Invoke();
				}
				throw;
			}
			try
			{
				delegation.Invoke();
			}
			catch (Exception ex2)
			{
				if (Util.IsCatchableExceptionType(ex2))
				{
					handlerReset.Invoke();
				}
				throw;
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00006790 File Offset: 0x00004990
		private static void DoAction<T>(Action<T> handler, Action handlerReset, Action<T> delegation, Action delegationReset, T argument)
		{
			try
			{
				handler.Invoke(argument);
			}
			catch (Exception ex)
			{
				if (Util.IsCatchableExceptionType(ex))
				{
					handlerReset.Invoke();
					delegationReset.Invoke();
				}
				throw;
			}
			try
			{
				delegation.Invoke(argument);
			}
			catch (Exception ex2)
			{
				if (Util.IsCatchableExceptionType(ex2))
				{
					handlerReset.Invoke();
				}
				throw;
			}
		}

		// Token: 0x040000C6 RID: 198
		internal static readonly SpatialPipeline SpatialPipelineNoOp = new SpatialPipeline(new ForwardingSegment.NoOpGeographyPipeline(), new ForwardingSegment.NoOpGeometryPipeline());

		// Token: 0x040000C7 RID: 199
		private readonly SpatialPipeline current;

		// Token: 0x040000C8 RID: 200
		private SpatialPipeline next = ForwardingSegment.SpatialPipelineNoOp;

		// Token: 0x040000C9 RID: 201
		private ForwardingSegment.GeographyForwarder geographyForwarder;

		// Token: 0x040000CA RID: 202
		private ForwardingSegment.GeometryForwarder geometryForwarder;

		// Token: 0x02000087 RID: 135
		internal class GeographyForwarder : GeographyPipeline
		{
			// Token: 0x0600036F RID: 879 RVA: 0x000091EC File Offset: 0x000073EC
			public GeographyForwarder(ForwardingSegment segment)
			{
				this.segment = segment;
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x06000370 RID: 880 RVA: 0x000091FB File Offset: 0x000073FB
			private GeographyPipeline Current
			{
				get
				{
					return this.segment.current;
				}
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x06000371 RID: 881 RVA: 0x0000920D File Offset: 0x0000740D
			private GeographyPipeline Next
			{
				get
				{
					return this.segment.next;
				}
			}

			// Token: 0x06000372 RID: 882 RVA: 0x0000921F File Offset: 0x0000741F
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

			// Token: 0x06000373 RID: 883 RVA: 0x00009240 File Offset: 0x00007440
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

			// Token: 0x06000374 RID: 884 RVA: 0x00009261 File Offset: 0x00007461
			public override void EndGeography()
			{
				this.DoAction(new Action(this.Current.EndGeography), new Action(this.Next.EndGeography));
			}

			// Token: 0x06000375 RID: 885 RVA: 0x0000928D File Offset: 0x0000748D
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

			// Token: 0x06000376 RID: 886 RVA: 0x000092B9 File Offset: 0x000074B9
			public override void EndFigure()
			{
				this.DoAction(new Action(this.Current.EndFigure), new Action(this.Next.EndFigure));
			}

			// Token: 0x06000377 RID: 887 RVA: 0x000092E5 File Offset: 0x000074E5
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

			// Token: 0x06000378 RID: 888 RVA: 0x00009311 File Offset: 0x00007511
			public override void Reset()
			{
				this.DoAction(new Action(this.Current.Reset), new Action(this.Next.Reset));
			}

			// Token: 0x06000379 RID: 889 RVA: 0x0000933D File Offset: 0x0000753D
			private void DoAction<T>(Action<T> handler, Action<T> delegation, T argument)
			{
				ForwardingSegment.DoAction<T>(handler, new Action(this.Current.Reset), delegation, new Action(this.Next.Reset), argument);
			}

			// Token: 0x0600037A RID: 890 RVA: 0x0000936B File Offset: 0x0000756B
			private void DoAction(Action handler, Action delegation)
			{
				ForwardingSegment.DoAction(handler, new Action(this.Current.Reset), delegation, new Action(this.Next.Reset));
			}

			// Token: 0x04000143 RID: 323
			private readonly ForwardingSegment segment;
		}

		// Token: 0x02000088 RID: 136
		internal class GeometryForwarder : GeometryPipeline
		{
			// Token: 0x06000383 RID: 899 RVA: 0x00009408 File Offset: 0x00007608
			public GeometryForwarder(ForwardingSegment segment)
			{
				this.segment = segment;
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x06000384 RID: 900 RVA: 0x00009417 File Offset: 0x00007617
			private GeometryPipeline Current
			{
				get
				{
					return this.segment.current;
				}
			}

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x06000385 RID: 901 RVA: 0x00009429 File Offset: 0x00007629
			private GeometryPipeline Next
			{
				get
				{
					return this.segment.next;
				}
			}

			// Token: 0x06000386 RID: 902 RVA: 0x0000943B File Offset: 0x0000763B
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

			// Token: 0x06000387 RID: 903 RVA: 0x0000945C File Offset: 0x0000765C
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

			// Token: 0x06000388 RID: 904 RVA: 0x0000947D File Offset: 0x0000767D
			public override void EndGeometry()
			{
				this.DoAction(new Action(this.Current.EndGeometry), new Action(this.Next.EndGeometry));
			}

			// Token: 0x06000389 RID: 905 RVA: 0x000094A9 File Offset: 0x000076A9
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

			// Token: 0x0600038A RID: 906 RVA: 0x000094D5 File Offset: 0x000076D5
			public override void EndFigure()
			{
				this.DoAction(new Action(this.Current.EndFigure), new Action(this.Next.EndFigure));
			}

			// Token: 0x0600038B RID: 907 RVA: 0x00009501 File Offset: 0x00007701
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

			// Token: 0x0600038C RID: 908 RVA: 0x0000952D File Offset: 0x0000772D
			public override void Reset()
			{
				this.DoAction(new Action(this.Current.Reset), new Action(this.Next.Reset));
			}

			// Token: 0x0600038D RID: 909 RVA: 0x00009559 File Offset: 0x00007759
			private void DoAction<T>(Action<T> handler, Action<T> delegation, T argument)
			{
				ForwardingSegment.DoAction<T>(handler, new Action(this.Current.Reset), delegation, new Action(this.Next.Reset), argument);
			}

			// Token: 0x0600038E RID: 910 RVA: 0x00009587 File Offset: 0x00007787
			private void DoAction(Action handler, Action delegation)
			{
				ForwardingSegment.DoAction(handler, new Action(this.Current.Reset), delegation, new Action(this.Next.Reset));
			}

			// Token: 0x04000144 RID: 324
			private readonly ForwardingSegment segment;
		}

		// Token: 0x02000089 RID: 137
		private class NoOpGeographyPipeline : GeographyPipeline
		{
			// Token: 0x06000397 RID: 919 RVA: 0x000046A6 File Offset: 0x000028A6
			public override void LineTo(GeographyPosition position)
			{
			}

			// Token: 0x06000398 RID: 920 RVA: 0x000046A6 File Offset: 0x000028A6
			public override void BeginFigure(GeographyPosition position)
			{
			}

			// Token: 0x06000399 RID: 921 RVA: 0x000046A6 File Offset: 0x000028A6
			public override void BeginGeography(SpatialType type)
			{
			}

			// Token: 0x0600039A RID: 922 RVA: 0x000046A6 File Offset: 0x000028A6
			public override void EndFigure()
			{
			}

			// Token: 0x0600039B RID: 923 RVA: 0x000046A6 File Offset: 0x000028A6
			public override void EndGeography()
			{
			}

			// Token: 0x0600039C RID: 924 RVA: 0x000046A6 File Offset: 0x000028A6
			public override void Reset()
			{
			}

			// Token: 0x0600039D RID: 925 RVA: 0x000046A6 File Offset: 0x000028A6
			public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
			}
		}

		// Token: 0x0200008A RID: 138
		private class NoOpGeometryPipeline : GeometryPipeline
		{
			// Token: 0x0600039F RID: 927 RVA: 0x000046A6 File Offset: 0x000028A6
			public override void LineTo(GeometryPosition position)
			{
			}

			// Token: 0x060003A0 RID: 928 RVA: 0x000046A6 File Offset: 0x000028A6
			public override void BeginFigure(GeometryPosition position)
			{
			}

			// Token: 0x060003A1 RID: 929 RVA: 0x000046A6 File Offset: 0x000028A6
			public override void BeginGeometry(SpatialType type)
			{
			}

			// Token: 0x060003A2 RID: 930 RVA: 0x000046A6 File Offset: 0x000028A6
			public override void EndFigure()
			{
			}

			// Token: 0x060003A3 RID: 931 RVA: 0x000046A6 File Offset: 0x000028A6
			public override void EndGeometry()
			{
			}

			// Token: 0x060003A4 RID: 932 RVA: 0x000046A6 File Offset: 0x000028A6
			public override void Reset()
			{
			}

			// Token: 0x060003A5 RID: 933 RVA: 0x000046A6 File Offset: 0x000028A6
			public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
			}
		}
	}
}
