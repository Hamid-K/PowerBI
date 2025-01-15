using System;
using System.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000083 RID: 131
	internal class ForwardingSegment : SpatialPipeline
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0000936B File Offset: 0x0000756B
		public ForwardingSegment(SpatialPipeline current)
		{
			this.current = current;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00009385 File Offset: 0x00007585
		public ForwardingSegment(GeographyPipeline currentGeography, GeometryPipeline currentGeometry)
			: this(new SpatialPipeline(currentGeography, currentGeometry))
		{
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00009394 File Offset: 0x00007594
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600032C RID: 812 RVA: 0x000093BC File Offset: 0x000075BC
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

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600032D RID: 813 RVA: 0x000093E2 File Offset: 0x000075E2
		public GeographyPipeline NextDrawGeography
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600032E RID: 814 RVA: 0x000093EF File Offset: 0x000075EF
		public GeometryPipeline NextDrawGeometry
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000093FC File Offset: 0x000075FC
		public override SpatialPipeline ChainTo(SpatialPipeline destination)
		{
			Util.CheckArgumentNull(destination, "destination");
			this.next = destination;
			destination.StartingLink = base.StartingLink;
			return destination;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00009420 File Offset: 0x00007620
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

		// Token: 0x06000331 RID: 817 RVA: 0x00009484 File Offset: 0x00007684
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

		// Token: 0x04000106 RID: 262
		internal static readonly SpatialPipeline SpatialPipelineNoOp = new SpatialPipeline(new ForwardingSegment.NoOpGeographyPipeline(), new ForwardingSegment.NoOpGeometryPipeline());

		// Token: 0x04000107 RID: 263
		private readonly SpatialPipeline current;

		// Token: 0x04000108 RID: 264
		private SpatialPipeline next = ForwardingSegment.SpatialPipelineNoOp;

		// Token: 0x04000109 RID: 265
		private ForwardingSegment.GeographyForwarder geographyForwarder;

		// Token: 0x0400010A RID: 266
		private ForwardingSegment.GeometryForwarder geometryForwarder;

		// Token: 0x02000084 RID: 132
		internal class GeographyForwarder : GeographyPipeline
		{
			// Token: 0x06000333 RID: 819 RVA: 0x00009502 File Offset: 0x00007702
			public GeographyForwarder(ForwardingSegment segment)
			{
				this.segment = segment;
			}

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x06000334 RID: 820 RVA: 0x00009511 File Offset: 0x00007711
			private GeographyPipeline Current
			{
				get
				{
					return this.segment.current;
				}
			}

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x06000335 RID: 821 RVA: 0x00009523 File Offset: 0x00007723
			private GeographyPipeline Next
			{
				get
				{
					return this.segment.next;
				}
			}

			// Token: 0x06000336 RID: 822 RVA: 0x00009551 File Offset: 0x00007751
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

			// Token: 0x06000337 RID: 823 RVA: 0x0000958E File Offset: 0x0000778E
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

			// Token: 0x06000338 RID: 824 RVA: 0x000095AF File Offset: 0x000077AF
			public override void EndGeography()
			{
				this.DoAction(new Action(this.Current.EndGeography), new Action(this.Next.EndGeography));
			}

			// Token: 0x06000339 RID: 825 RVA: 0x000095F7 File Offset: 0x000077F7
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

			// Token: 0x0600033A RID: 826 RVA: 0x00009623 File Offset: 0x00007823
			public override void EndFigure()
			{
				this.DoAction(new Action(this.Current.EndFigure), new Action(this.Next.EndFigure));
			}

			// Token: 0x0600033B RID: 827 RVA: 0x0000966B File Offset: 0x0000786B
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

			// Token: 0x0600033C RID: 828 RVA: 0x00009697 File Offset: 0x00007897
			public override void Reset()
			{
				this.DoAction(new Action(this.Current.Reset), new Action(this.Next.Reset));
			}

			// Token: 0x0600033D RID: 829 RVA: 0x000096C3 File Offset: 0x000078C3
			private void DoAction<T>(Action<T> handler, Action<T> delegation, T argument)
			{
				ForwardingSegment.DoAction<T>(handler, new Action(this.Current.Reset), delegation, new Action(this.Next.Reset), argument);
			}

			// Token: 0x0600033E RID: 830 RVA: 0x000096F1 File Offset: 0x000078F1
			private void DoAction(Action handler, Action delegation)
			{
				ForwardingSegment.DoAction(handler, new Action(this.Current.Reset), delegation, new Action(this.Next.Reset));
			}

			// Token: 0x0400010B RID: 267
			private readonly ForwardingSegment segment;
		}

		// Token: 0x02000085 RID: 133
		internal class GeometryForwarder : GeometryPipeline
		{
			// Token: 0x06000347 RID: 839 RVA: 0x0000971E File Offset: 0x0000791E
			public GeometryForwarder(ForwardingSegment segment)
			{
				this.segment = segment;
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x06000348 RID: 840 RVA: 0x0000972D File Offset: 0x0000792D
			private GeometryPipeline Current
			{
				get
				{
					return this.segment.current;
				}
			}

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x06000349 RID: 841 RVA: 0x0000973F File Offset: 0x0000793F
			private GeometryPipeline Next
			{
				get
				{
					return this.segment.next;
				}
			}

			// Token: 0x0600034A RID: 842 RVA: 0x0000976D File Offset: 0x0000796D
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

			// Token: 0x0600034B RID: 843 RVA: 0x000097AA File Offset: 0x000079AA
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

			// Token: 0x0600034C RID: 844 RVA: 0x000097CB File Offset: 0x000079CB
			public override void EndGeometry()
			{
				this.DoAction(new Action(this.Current.EndGeometry), new Action(this.Next.EndGeometry));
			}

			// Token: 0x0600034D RID: 845 RVA: 0x00009813 File Offset: 0x00007A13
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

			// Token: 0x0600034E RID: 846 RVA: 0x0000983F File Offset: 0x00007A3F
			public override void EndFigure()
			{
				this.DoAction(new Action(this.Current.EndFigure), new Action(this.Next.EndFigure));
			}

			// Token: 0x0600034F RID: 847 RVA: 0x00009887 File Offset: 0x00007A87
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

			// Token: 0x06000350 RID: 848 RVA: 0x000098B3 File Offset: 0x00007AB3
			public override void Reset()
			{
				this.DoAction(new Action(this.Current.Reset), new Action(this.Next.Reset));
			}

			// Token: 0x06000351 RID: 849 RVA: 0x000098DF File Offset: 0x00007ADF
			private void DoAction<T>(Action<T> handler, Action<T> delegation, T argument)
			{
				ForwardingSegment.DoAction<T>(handler, new Action(this.Current.Reset), delegation, new Action(this.Next.Reset), argument);
			}

			// Token: 0x06000352 RID: 850 RVA: 0x0000990D File Offset: 0x00007B0D
			private void DoAction(Action handler, Action delegation)
			{
				ForwardingSegment.DoAction(handler, new Action(this.Current.Reset), delegation, new Action(this.Next.Reset));
			}

			// Token: 0x0400010C RID: 268
			private readonly ForwardingSegment segment;
		}

		// Token: 0x02000086 RID: 134
		private class NoOpGeographyPipeline : GeographyPipeline
		{
			// Token: 0x0600035B RID: 859 RVA: 0x0000993A File Offset: 0x00007B3A
			public override void LineTo(GeographyPosition position)
			{
			}

			// Token: 0x0600035C RID: 860 RVA: 0x0000993C File Offset: 0x00007B3C
			public override void BeginFigure(GeographyPosition position)
			{
			}

			// Token: 0x0600035D RID: 861 RVA: 0x0000993E File Offset: 0x00007B3E
			public override void BeginGeography(SpatialType type)
			{
			}

			// Token: 0x0600035E RID: 862 RVA: 0x00009940 File Offset: 0x00007B40
			public override void EndFigure()
			{
			}

			// Token: 0x0600035F RID: 863 RVA: 0x00009942 File Offset: 0x00007B42
			public override void EndGeography()
			{
			}

			// Token: 0x06000360 RID: 864 RVA: 0x00009944 File Offset: 0x00007B44
			public override void Reset()
			{
			}

			// Token: 0x06000361 RID: 865 RVA: 0x00009946 File Offset: 0x00007B46
			public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
			}
		}

		// Token: 0x02000087 RID: 135
		private class NoOpGeometryPipeline : GeometryPipeline
		{
			// Token: 0x06000363 RID: 867 RVA: 0x00009950 File Offset: 0x00007B50
			public override void LineTo(GeometryPosition position)
			{
			}

			// Token: 0x06000364 RID: 868 RVA: 0x00009952 File Offset: 0x00007B52
			public override void BeginFigure(GeometryPosition position)
			{
			}

			// Token: 0x06000365 RID: 869 RVA: 0x00009954 File Offset: 0x00007B54
			public override void BeginGeometry(SpatialType type)
			{
			}

			// Token: 0x06000366 RID: 870 RVA: 0x00009956 File Offset: 0x00007B56
			public override void EndFigure()
			{
			}

			// Token: 0x06000367 RID: 871 RVA: 0x00009958 File Offset: 0x00007B58
			public override void EndGeometry()
			{
			}

			// Token: 0x06000368 RID: 872 RVA: 0x0000995A File Offset: 0x00007B5A
			public override void Reset()
			{
			}

			// Token: 0x06000369 RID: 873 RVA: 0x0000995C File Offset: 0x00007B5C
			public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
			}
		}
	}
}
