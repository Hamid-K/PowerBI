using System;
using System.Collections.Generic;
using System.Linq;
using System.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200004D RID: 77
	internal abstract class SpatialTreeBuilder<T> : TypeWashedPipeline where T : class, ISpatial
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060001F9 RID: 505 RVA: 0x00005C1C File Offset: 0x00003E1C
		// (remove) Token: 0x060001FA RID: 506 RVA: 0x00005C54 File Offset: 0x00003E54
		public event Action<T> ProduceInstance;

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00005C89 File Offset: 0x00003E89
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

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00005CC8 File Offset: 0x00003EC8
		public override bool IsGeography
		{
			get
			{
				return typeof(Geography).IsAssignableFrom(typeof(T));
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00005CE3 File Offset: 0x00003EE3
		internal override void LineTo(double x, double y, double? z, double? m)
		{
			this.currentFigure.Add(this.CreatePoint(false, x, y, z, m));
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00005CFC File Offset: 0x00003EFC
		internal override void BeginFigure(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			if (this.currentFigure == null)
			{
				this.currentFigure = new List<T>();
			}
			this.currentFigure.Add(this.CreatePoint(false, coordinate1, coordinate2, coordinate3, coordinate4));
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00005D28 File Offset: 0x00003F28
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

		// Token: 0x06000200 RID: 512 RVA: 0x00005D6C File Offset: 0x00003F6C
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

		// Token: 0x06000201 RID: 513 RVA: 0x00005DD8 File Offset: 0x00003FD8
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

		// Token: 0x06000202 RID: 514 RVA: 0x00005F45 File Offset: 0x00004145
		internal override void Reset()
		{
			this.currentNode = null;
			this.currentFigure = null;
		}

		// Token: 0x06000203 RID: 515
		protected abstract T CreatePoint(bool isEmpty, double x, double y, double? z, double? m);

		// Token: 0x06000204 RID: 516
		protected abstract T CreateShapeInstance(SpatialType type, IEnumerable<T> spatialData);

		// Token: 0x06000205 RID: 517 RVA: 0x00005F55 File Offset: 0x00004155
		private void NotifyIfWeJustFinishedBuildingSomething()
		{
			if (this.currentNode == null && this.ProduceInstance != null)
			{
				this.ProduceInstance.Invoke(this.lastConstructedNode.Instance);
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00005F7D File Offset: 0x0000417D
		private void TraverseUpTheTree()
		{
			this.lastConstructedNode = this.currentNode;
			this.currentNode = this.currentNode.Parent;
		}

		// Token: 0x04000057 RID: 87
		private List<T> currentFigure;

		// Token: 0x04000058 RID: 88
		private SpatialTreeBuilder<T>.SpatialBuilderNode currentNode;

		// Token: 0x04000059 RID: 89
		private SpatialTreeBuilder<T>.SpatialBuilderNode lastConstructedNode;

		// Token: 0x0200004E RID: 78
		private class SpatialBuilderNode
		{
			// Token: 0x06000209 RID: 521 RVA: 0x00005FA4 File Offset: 0x000041A4
			public SpatialBuilderNode()
			{
				this.Children = new List<SpatialTreeBuilder<T>.SpatialBuilderNode>();
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x0600020A RID: 522 RVA: 0x00005FB7 File Offset: 0x000041B7
			// (set) Token: 0x0600020B RID: 523 RVA: 0x00005FBF File Offset: 0x000041BF
			public List<SpatialTreeBuilder<T>.SpatialBuilderNode> Children { get; private set; }

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x0600020C RID: 524 RVA: 0x00005FC8 File Offset: 0x000041C8
			// (set) Token: 0x0600020D RID: 525 RVA: 0x00005FD0 File Offset: 0x000041D0
			public T Instance { get; set; }

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x0600020E RID: 526 RVA: 0x00005FD9 File Offset: 0x000041D9
			// (set) Token: 0x0600020F RID: 527 RVA: 0x00005FE1 File Offset: 0x000041E1
			public SpatialTreeBuilder<T>.SpatialBuilderNode Parent { get; private set; }

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x06000210 RID: 528 RVA: 0x00005FEA File Offset: 0x000041EA
			// (set) Token: 0x06000211 RID: 529 RVA: 0x00005FF2 File Offset: 0x000041F2
			public SpatialType Type { get; set; }

			// Token: 0x06000212 RID: 530 RVA: 0x00005FFC File Offset: 0x000041FC
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
