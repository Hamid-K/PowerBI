using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000084 RID: 132
	internal class ForwardingSegment : SpatialPipeline
	{
		// Token: 0x06000333 RID: 819 RVA: 0x000092DB File Offset: 0x000074DB
		public ForwardingSegment(SpatialPipeline current)
		{
			this.current = current;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000092F5 File Offset: 0x000074F5
		public ForwardingSegment(GeographyPipeline currentGeography, GeometryPipeline currentGeometry)
			: this(new SpatialPipeline(currentGeography, currentGeometry))
		{
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00009304 File Offset: 0x00007504
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

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000932C File Offset: 0x0000752C
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00009352 File Offset: 0x00007552
		public GeographyPipeline NextDrawGeography
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000935F File Offset: 0x0000755F
		public GeometryPipeline NextDrawGeometry
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000936C File Offset: 0x0000756C
		public override SpatialPipeline ChainTo(SpatialPipeline destination)
		{
			Util.CheckArgumentNull(destination, "destination");
			this.next = destination;
			destination.StartingLink = base.StartingLink;
			return destination;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00009390 File Offset: 0x00007590
		[SuppressMessage("DataWeb.Usage", "AC0014:DoNotHandleProhibitedExceptionsRule", Justification = "We're calling this correctly")]
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

		// Token: 0x0600033B RID: 827 RVA: 0x000093F4 File Offset: 0x000075F4
		[SuppressMessage("DataWeb.Usage", "AC0014:DoNotHandleProhibitedExceptionsRule", Justification = "We're calling this correctly")]
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

		// Token: 0x04000108 RID: 264
		internal static readonly SpatialPipeline SpatialPipelineNoOp = new SpatialPipeline(new ForwardingSegment.NoOpGeographyPipeline(), new ForwardingSegment.NoOpGeometryPipeline());

		// Token: 0x04000109 RID: 265
		private readonly SpatialPipeline current;

		// Token: 0x0400010A RID: 266
		private SpatialPipeline next = ForwardingSegment.SpatialPipelineNoOp;

		// Token: 0x0400010B RID: 267
		private ForwardingSegment.GeographyForwarder geographyForwarder;

		// Token: 0x0400010C RID: 268
		private ForwardingSegment.GeometryForwarder geometryForwarder;

		// Token: 0x02000085 RID: 133
		internal class GeographyForwarder : GeographyPipeline
		{
			// Token: 0x0600033D RID: 829 RVA: 0x00009472 File Offset: 0x00007672
			public GeographyForwarder(ForwardingSegment segment)
			{
				this.segment = segment;
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x0600033E RID: 830 RVA: 0x00009481 File Offset: 0x00007681
			private GeographyPipeline Current
			{
				get
				{
					return this.segment.current;
				}
			}

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x0600033F RID: 831 RVA: 0x00009493 File Offset: 0x00007693
			private GeographyPipeline Next
			{
				get
				{
					return this.segment.next;
				}
			}

			// Token: 0x06000340 RID: 832 RVA: 0x000094C1 File Offset: 0x000076C1
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

			// Token: 0x06000341 RID: 833 RVA: 0x000094FE File Offset: 0x000076FE
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

			// Token: 0x06000342 RID: 834 RVA: 0x0000951F File Offset: 0x0000771F
			public override void EndGeography()
			{
				this.DoAction(new Action(this.Current.EndGeography), new Action(this.Next.EndGeography));
			}

			// Token: 0x06000343 RID: 835 RVA: 0x00009567 File Offset: 0x00007767
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

			// Token: 0x06000344 RID: 836 RVA: 0x00009593 File Offset: 0x00007793
			public override void EndFigure()
			{
				this.DoAction(new Action(this.Current.EndFigure), new Action(this.Next.EndFigure));
			}

			// Token: 0x06000345 RID: 837 RVA: 0x000095DB File Offset: 0x000077DB
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

			// Token: 0x06000346 RID: 838 RVA: 0x00009607 File Offset: 0x00007807
			public override void Reset()
			{
				this.DoAction(new Action(this.Current.Reset), new Action(this.Next.Reset));
			}

			// Token: 0x06000347 RID: 839 RVA: 0x00009633 File Offset: 0x00007833
			private void DoAction<T>(Action<T> handler, Action<T> delegation, T argument)
			{
				ForwardingSegment.DoAction<T>(handler, new Action(this.Current.Reset), delegation, new Action(this.Next.Reset), argument);
			}

			// Token: 0x06000348 RID: 840 RVA: 0x00009661 File Offset: 0x00007861
			private void DoAction(Action handler, Action delegation)
			{
				ForwardingSegment.DoAction(handler, new Action(this.Current.Reset), delegation, new Action(this.Next.Reset));
			}

			// Token: 0x0400010D RID: 269
			private readonly ForwardingSegment segment;
		}

		// Token: 0x02000086 RID: 134
		internal class GeometryForwarder : GeometryPipeline
		{
			// Token: 0x06000351 RID: 849 RVA: 0x0000968E File Offset: 0x0000788E
			public GeometryForwarder(ForwardingSegment segment)
			{
				this.segment = segment;
			}

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x06000352 RID: 850 RVA: 0x0000969D File Offset: 0x0000789D
			private GeometryPipeline Current
			{
				get
				{
					return this.segment.current;
				}
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x06000353 RID: 851 RVA: 0x000096AF File Offset: 0x000078AF
			private GeometryPipeline Next
			{
				get
				{
					return this.segment.next;
				}
			}

			// Token: 0x06000354 RID: 852 RVA: 0x000096DD File Offset: 0x000078DD
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

			// Token: 0x06000355 RID: 853 RVA: 0x0000971A File Offset: 0x0000791A
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

			// Token: 0x06000356 RID: 854 RVA: 0x0000973B File Offset: 0x0000793B
			public override void EndGeometry()
			{
				this.DoAction(new Action(this.Current.EndGeometry), new Action(this.Next.EndGeometry));
			}

			// Token: 0x06000357 RID: 855 RVA: 0x00009783 File Offset: 0x00007983
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

			// Token: 0x06000358 RID: 856 RVA: 0x000097AF File Offset: 0x000079AF
			public override void EndFigure()
			{
				this.DoAction(new Action(this.Current.EndFigure), new Action(this.Next.EndFigure));
			}

			// Token: 0x06000359 RID: 857 RVA: 0x000097F7 File Offset: 0x000079F7
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

			// Token: 0x0600035A RID: 858 RVA: 0x00009823 File Offset: 0x00007A23
			public override void Reset()
			{
				this.DoAction(new Action(this.Current.Reset), new Action(this.Next.Reset));
			}

			// Token: 0x0600035B RID: 859 RVA: 0x0000984F File Offset: 0x00007A4F
			private void DoAction<T>(Action<T> handler, Action<T> delegation, T argument)
			{
				ForwardingSegment.DoAction<T>(handler, new Action(this.Current.Reset), delegation, new Action(this.Next.Reset), argument);
			}

			// Token: 0x0600035C RID: 860 RVA: 0x0000987D File Offset: 0x00007A7D
			private void DoAction(Action handler, Action delegation)
			{
				ForwardingSegment.DoAction(handler, new Action(this.Current.Reset), delegation, new Action(this.Next.Reset));
			}

			// Token: 0x0400010E RID: 270
			private readonly ForwardingSegment segment;
		}

		// Token: 0x02000087 RID: 135
		private class NoOpGeographyPipeline : GeographyPipeline
		{
			// Token: 0x06000365 RID: 869 RVA: 0x000098AA File Offset: 0x00007AAA
			public override void LineTo(GeographyPosition position)
			{
			}

			// Token: 0x06000366 RID: 870 RVA: 0x000098AC File Offset: 0x00007AAC
			public override void BeginFigure(GeographyPosition position)
			{
			}

			// Token: 0x06000367 RID: 871 RVA: 0x000098AE File Offset: 0x00007AAE
			public override void BeginGeography(SpatialType type)
			{
			}

			// Token: 0x06000368 RID: 872 RVA: 0x000098B0 File Offset: 0x00007AB0
			public override void EndFigure()
			{
			}

			// Token: 0x06000369 RID: 873 RVA: 0x000098B2 File Offset: 0x00007AB2
			public override void EndGeography()
			{
			}

			// Token: 0x0600036A RID: 874 RVA: 0x000098B4 File Offset: 0x00007AB4
			public override void Reset()
			{
			}

			// Token: 0x0600036B RID: 875 RVA: 0x000098B6 File Offset: 0x00007AB6
			public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
			}
		}

		// Token: 0x02000088 RID: 136
		private class NoOpGeometryPipeline : GeometryPipeline
		{
			// Token: 0x0600036D RID: 877 RVA: 0x000098C0 File Offset: 0x00007AC0
			public override void LineTo(GeometryPosition position)
			{
			}

			// Token: 0x0600036E RID: 878 RVA: 0x000098C2 File Offset: 0x00007AC2
			public override void BeginFigure(GeometryPosition position)
			{
			}

			// Token: 0x0600036F RID: 879 RVA: 0x000098C4 File Offset: 0x00007AC4
			public override void BeginGeometry(SpatialType type)
			{
			}

			// Token: 0x06000370 RID: 880 RVA: 0x000098C6 File Offset: 0x00007AC6
			public override void EndFigure()
			{
			}

			// Token: 0x06000371 RID: 881 RVA: 0x000098C8 File Offset: 0x00007AC8
			public override void EndGeometry()
			{
			}

			// Token: 0x06000372 RID: 882 RVA: 0x000098CA File Offset: 0x00007ACA
			public override void Reset()
			{
			}

			// Token: 0x06000373 RID: 883 RVA: 0x000098CC File Offset: 0x00007ACC
			public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
			}
		}
	}
}
