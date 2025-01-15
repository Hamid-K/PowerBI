using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x0200005F RID: 95
	internal abstract class SpatialTreeBuilder<T> : TypeWashedPipeline where T : class, ISpatial
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000234 RID: 564 RVA: 0x00005A30 File Offset: 0x00003C30
		// (remove) Token: 0x06000235 RID: 565 RVA: 0x00005A68 File Offset: 0x00003C68
		public event Action<T> ProduceInstance;

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00005A9D File Offset: 0x00003C9D
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00005ADC File Offset: 0x00003CDC
		public override bool IsGeography
		{
			get
			{
				return typeof(Geography).IsAssignableFrom(typeof(T));
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00005AF7 File Offset: 0x00003CF7
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		internal override void LineTo(double x, double y, double? z, double? m)
		{
			this.currentFigure.Add(this.CreatePoint(false, x, y, z, m));
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00005B10 File Offset: 0x00003D10
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		internal override void BeginFigure(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			if (this.currentFigure == null)
			{
				this.currentFigure = new List<T>();
			}
			this.currentFigure.Add(this.CreatePoint(false, coordinate1, coordinate2, coordinate3, coordinate4));
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00005B3C File Offset: 0x00003D3C
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

		// Token: 0x0600023B RID: 571 RVA: 0x00005B74 File Offset: 0x00003D74
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

		// Token: 0x0600023C RID: 572 RVA: 0x00005BD8 File Offset: 0x00003DD8
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

		// Token: 0x0600023D RID: 573 RVA: 0x00005D47 File Offset: 0x00003F47
		internal override void Reset()
		{
			this.currentNode = null;
			this.currentFigure = null;
		}

		// Token: 0x0600023E RID: 574
		protected abstract T CreatePoint(bool isEmpty, double x, double y, double? z, double? m);

		// Token: 0x0600023F RID: 575
		protected abstract T CreateShapeInstance(SpatialType type, IEnumerable<T> spatialData);

		// Token: 0x06000240 RID: 576 RVA: 0x00005D57 File Offset: 0x00003F57
		private void NotifyIfWeJustFinishedBuildingSomething()
		{
			if (this.currentNode == null && this.ProduceInstance != null)
			{
				this.ProduceInstance.Invoke(this.lastConstructedNode.Instance);
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00005D7F File Offset: 0x00003F7F
		private void TraverseUpTheTree()
		{
			this.lastConstructedNode = this.currentNode;
			this.currentNode = this.currentNode.Parent;
		}

		// Token: 0x0400009B RID: 155
		private List<T> currentFigure;

		// Token: 0x0400009C RID: 156
		private SpatialTreeBuilder<T>.SpatialBuilderNode currentNode;

		// Token: 0x0400009D RID: 157
		private SpatialTreeBuilder<T>.SpatialBuilderNode lastConstructedNode;

		// Token: 0x02000083 RID: 131
		private class SpatialBuilderNode
		{
			// Token: 0x06000330 RID: 816 RVA: 0x0000840F File Offset: 0x0000660F
			public SpatialBuilderNode()
			{
				this.Children = new List<SpatialTreeBuilder<T>.SpatialBuilderNode>();
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x06000331 RID: 817 RVA: 0x00008422 File Offset: 0x00006622
			// (set) Token: 0x06000332 RID: 818 RVA: 0x0000842A File Offset: 0x0000662A
			public List<SpatialTreeBuilder<T>.SpatialBuilderNode> Children { get; private set; }

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x06000333 RID: 819 RVA: 0x00008433 File Offset: 0x00006633
			// (set) Token: 0x06000334 RID: 820 RVA: 0x0000843B File Offset: 0x0000663B
			public T Instance { get; set; }

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x06000335 RID: 821 RVA: 0x00008444 File Offset: 0x00006644
			// (set) Token: 0x06000336 RID: 822 RVA: 0x0000844C File Offset: 0x0000664C
			public SpatialTreeBuilder<T>.SpatialBuilderNode Parent { get; private set; }

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x06000337 RID: 823 RVA: 0x00008455 File Offset: 0x00006655
			// (set) Token: 0x06000338 RID: 824 RVA: 0x0000845D File Offset: 0x0000665D
			public SpatialType Type { get; set; }

			// Token: 0x06000339 RID: 825 RVA: 0x00008468 File Offset: 0x00006668
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
