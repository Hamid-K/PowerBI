using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200004E RID: 78
	internal abstract class SpatialTreeBuilder<T> : TypeWashedPipeline where T : class, ISpatial
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000203 RID: 515 RVA: 0x00005B8C File Offset: 0x00003D8C
		// (remove) Token: 0x06000204 RID: 516 RVA: 0x00005BC4 File Offset: 0x00003DC4
		public event Action<T> ProduceInstance;

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00005BF9 File Offset: 0x00003DF9
		public T ConstructedInstance
		{
			get
			{
				if (this.lastConstructedNode == null || this.lastConstructedNode.Instance == null || this.lastConstructedNode.Parent != null)
				{
					throw new InvalidOperationException(Strings.SpatialBuilder_CannotCreateBeforeDrawn);
				}
				return this.lastConstructedNode.Instance;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00005C38 File Offset: 0x00003E38
		public override bool IsGeography
		{
			get
			{
				return typeof(Geography).IsAssignableFrom(typeof(T));
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00005C53 File Offset: 0x00003E53
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		internal override void LineTo(double x, double y, double? z, double? m)
		{
			this.currentFigure.Add(this.CreatePoint(false, x, y, z, m));
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00005C6C File Offset: 0x00003E6C
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		internal override void BeginFigure(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			if (this.currentFigure == null)
			{
				this.currentFigure = new List<T>();
			}
			this.currentFigure.Add(this.CreatePoint(false, coordinate1, coordinate2, coordinate3, coordinate4));
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00005C98 File Offset: 0x00003E98
		internal override void BeginGeo(SpatialType type)
		{
			if (this.currentNode == null)
			{
				this.currentNode = new SpatialTreeBuilder<T>.SpatialBuilderNode
				{
					Type = type
				};
				this.lastConstructedNode = null;
				return;
			}
			this.currentNode = this.currentNode.CreateChildren(type);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00005CDC File Offset: 0x00003EDC
		internal override void EndFigure()
		{
			if (this.currentFigure.Count == 1)
			{
				SpatialTreeBuilder<T>.SpatialBuilderNode spatialBuilderNode = this.currentNode.CreateChildren(SpatialType.Point);
				spatialBuilderNode.Instance = this.currentFigure[0];
			}
			else
			{
				SpatialTreeBuilder<T>.SpatialBuilderNode spatialBuilderNode2 = this.currentNode.CreateChildren(SpatialType.LineString);
				spatialBuilderNode2.Instance = this.CreateShapeInstance(SpatialType.LineString, this.currentFigure);
			}
			this.currentFigure = null;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00005D48 File Offset: 0x00003F48
		internal override void EndGeo()
		{
			switch (this.currentNode.Type)
			{
			case SpatialType.Point:
				this.currentNode.Instance = ((this.currentNode.Children.Count > 0) ? this.currentNode.Children[0].Instance : this.CreatePoint(true, double.NaN, double.NaN, default(double?), default(double?)));
				break;
			case SpatialType.LineString:
				this.currentNode.Instance = ((this.currentNode.Children.Count > 0) ? this.currentNode.Children[0].Instance : this.CreateShapeInstance(SpatialType.LineString, new T[0]));
				break;
			case SpatialType.Polygon:
			case SpatialType.MultiPoint:
			case SpatialType.MultiLineString:
			case SpatialType.MultiPolygon:
			case SpatialType.Collection:
				this.currentNode.Instance = this.CreateShapeInstance(this.currentNode.Type, Enumerable.Select<SpatialTreeBuilder<T>.SpatialBuilderNode, T>(this.currentNode.Children, (SpatialTreeBuilder<T>.SpatialBuilderNode node) => node.Instance));
				break;
			case SpatialType.FullGlobe:
				this.currentNode.Instance = this.CreateShapeInstance(SpatialType.FullGlobe, new T[0]);
				break;
			}
			this.TraverseUpTheTree();
			this.NotifyIfWeJustFinishedBuildingSomething();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00005EB5 File Offset: 0x000040B5
		internal override void Reset()
		{
			this.currentNode = null;
			this.currentFigure = null;
		}

		// Token: 0x0600020D RID: 525
		protected abstract T CreatePoint(bool isEmpty, double x, double y, double? z, double? m);

		// Token: 0x0600020E RID: 526
		protected abstract T CreateShapeInstance(SpatialType type, IEnumerable<T> spatialData);

		// Token: 0x0600020F RID: 527 RVA: 0x00005EC5 File Offset: 0x000040C5
		private void NotifyIfWeJustFinishedBuildingSomething()
		{
			if (this.currentNode == null && this.ProduceInstance != null)
			{
				this.ProduceInstance.Invoke(this.lastConstructedNode.Instance);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00005EED File Offset: 0x000040ED
		private void TraverseUpTheTree()
		{
			this.lastConstructedNode = this.currentNode;
			this.currentNode = this.currentNode.Parent;
		}

		// Token: 0x04000059 RID: 89
		private List<T> currentFigure;

		// Token: 0x0400005A RID: 90
		private SpatialTreeBuilder<T>.SpatialBuilderNode currentNode;

		// Token: 0x0400005B RID: 91
		private SpatialTreeBuilder<T>.SpatialBuilderNode lastConstructedNode;

		// Token: 0x0200004F RID: 79
		private class SpatialBuilderNode
		{
			// Token: 0x06000213 RID: 531 RVA: 0x00005F14 File Offset: 0x00004114
			public SpatialBuilderNode()
			{
				this.Children = new List<SpatialTreeBuilder<T>.SpatialBuilderNode>();
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x06000214 RID: 532 RVA: 0x00005F27 File Offset: 0x00004127
			// (set) Token: 0x06000215 RID: 533 RVA: 0x00005F2F File Offset: 0x0000412F
			public List<SpatialTreeBuilder<T>.SpatialBuilderNode> Children { get; private set; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x06000216 RID: 534 RVA: 0x00005F38 File Offset: 0x00004138
			// (set) Token: 0x06000217 RID: 535 RVA: 0x00005F40 File Offset: 0x00004140
			public T Instance { get; set; }

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x06000218 RID: 536 RVA: 0x00005F49 File Offset: 0x00004149
			// (set) Token: 0x06000219 RID: 537 RVA: 0x00005F51 File Offset: 0x00004151
			public SpatialTreeBuilder<T>.SpatialBuilderNode Parent { get; private set; }

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x0600021A RID: 538 RVA: 0x00005F5A File Offset: 0x0000415A
			// (set) Token: 0x0600021B RID: 539 RVA: 0x00005F62 File Offset: 0x00004162
			public SpatialType Type { get; set; }

			// Token: 0x0600021C RID: 540 RVA: 0x00005F6C File Offset: 0x0000416C
			internal SpatialTreeBuilder<T>.SpatialBuilderNode CreateChildren(SpatialType type)
			{
				SpatialTreeBuilder<T>.SpatialBuilderNode spatialBuilderNode = new SpatialTreeBuilder<T>.SpatialBuilderNode
				{
					Parent = this,
					Type = type
				};
				this.Children.Add(spatialBuilderNode);
				return spatialBuilderNode;
			}
		}
	}
}
