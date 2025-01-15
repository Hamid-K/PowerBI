using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.Spatial
{
	// Token: 0x02000064 RID: 100
	internal abstract class SpatialTreeBuilder<T> : TypeWashedPipeline where T : class, ISpatial
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060002AA RID: 682 RVA: 0x000066F8 File Offset: 0x000048F8
		// (remove) Token: 0x060002AB RID: 683 RVA: 0x00006730 File Offset: 0x00004930
		public event Action<T> ProduceInstance;

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00006765 File Offset: 0x00004965
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000067A4 File Offset: 0x000049A4
		public override bool IsGeography
		{
			get
			{
				return typeof(Geography).IsAssignableFrom(typeof(T));
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000067BF File Offset: 0x000049BF
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		internal override void LineTo(double x, double y, double? z, double? m)
		{
			this.currentFigure.Add(this.CreatePoint(false, x, y, z, m));
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000067D8 File Offset: 0x000049D8
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		internal override void BeginFigure(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			if (this.currentFigure == null)
			{
				this.currentFigure = new List<T>();
			}
			this.currentFigure.Add(this.CreatePoint(false, coordinate1, coordinate2, coordinate3, coordinate4));
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00006804 File Offset: 0x00004A04
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

		// Token: 0x060002B1 RID: 689 RVA: 0x0000683C File Offset: 0x00004A3C
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

		// Token: 0x060002B2 RID: 690 RVA: 0x000068A0 File Offset: 0x00004AA0
		internal override void EndGeo()
		{
			switch (this.currentNode.Type)
			{
			case SpatialType.Point:
				this.currentNode.Instance = ((this.currentNode.Children.Count > 0) ? this.currentNode.Children[0].Instance : this.CreatePoint(true, double.NaN, double.NaN, null, null));
				break;
			case SpatialType.LineString:
				this.currentNode.Instance = ((this.currentNode.Children.Count > 0) ? this.currentNode.Children[0].Instance : this.CreateShapeInstance(SpatialType.LineString, new T[0]));
				break;
			case SpatialType.Polygon:
			case SpatialType.MultiPoint:
			case SpatialType.MultiLineString:
			case SpatialType.MultiPolygon:
			case SpatialType.Collection:
				this.currentNode.Instance = this.CreateShapeInstance(this.currentNode.Type, this.currentNode.Children.Select((SpatialTreeBuilder<T>.SpatialBuilderNode node) => node.Instance));
				break;
			case SpatialType.FullGlobe:
				this.currentNode.Instance = this.CreateShapeInstance(SpatialType.FullGlobe, new T[0]);
				break;
			}
			this.TraverseUpTheTree();
			this.NotifyIfWeJustFinishedBuildingSomething();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00006A0F File Offset: 0x00004C0F
		internal override void Reset()
		{
			this.currentNode = null;
			this.currentFigure = null;
		}

		// Token: 0x060002B4 RID: 692
		protected abstract T CreatePoint(bool isEmpty, double x, double y, double? z, double? m);

		// Token: 0x060002B5 RID: 693
		protected abstract T CreateShapeInstance(SpatialType type, IEnumerable<T> spatialData);

		// Token: 0x060002B6 RID: 694 RVA: 0x00006A1F File Offset: 0x00004C1F
		private void NotifyIfWeJustFinishedBuildingSomething()
		{
			if (this.currentNode == null && this.ProduceInstance != null)
			{
				this.ProduceInstance(this.lastConstructedNode.Instance);
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00006A47 File Offset: 0x00004C47
		private void TraverseUpTheTree()
		{
			this.lastConstructedNode = this.currentNode;
			this.currentNode = this.currentNode.Parent;
		}

		// Token: 0x040000A8 RID: 168
		private List<T> currentFigure;

		// Token: 0x040000A9 RID: 169
		private SpatialTreeBuilder<T>.SpatialBuilderNode currentNode;

		// Token: 0x040000AA RID: 170
		private SpatialTreeBuilder<T>.SpatialBuilderNode lastConstructedNode;

		// Token: 0x0200008F RID: 143
		private class SpatialBuilderNode
		{
			// Token: 0x060003B8 RID: 952 RVA: 0x00009173 File Offset: 0x00007373
			public SpatialBuilderNode()
			{
				this.Children = new List<SpatialTreeBuilder<T>.SpatialBuilderNode>();
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x060003B9 RID: 953 RVA: 0x00009186 File Offset: 0x00007386
			// (set) Token: 0x060003BA RID: 954 RVA: 0x0000918E File Offset: 0x0000738E
			public List<SpatialTreeBuilder<T>.SpatialBuilderNode> Children { get; private set; }

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x060003BB RID: 955 RVA: 0x00009197 File Offset: 0x00007397
			// (set) Token: 0x060003BC RID: 956 RVA: 0x0000919F File Offset: 0x0000739F
			public T Instance { get; set; }

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x060003BD RID: 957 RVA: 0x000091A8 File Offset: 0x000073A8
			// (set) Token: 0x060003BE RID: 958 RVA: 0x000091B0 File Offset: 0x000073B0
			public SpatialTreeBuilder<T>.SpatialBuilderNode Parent { get; private set; }

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x060003BF RID: 959 RVA: 0x000091B9 File Offset: 0x000073B9
			// (set) Token: 0x060003C0 RID: 960 RVA: 0x000091C1 File Offset: 0x000073C1
			public SpatialType Type { get; set; }

			// Token: 0x060003C1 RID: 961 RVA: 0x000091CC File Offset: 0x000073CC
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
