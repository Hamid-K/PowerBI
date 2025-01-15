using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x020005FA RID: 1530
	public class Cluster<TNode> : IEquatable<Cluster<TNode>> where TNode : IProgramNodeBuilder
	{
		// Token: 0x06002152 RID: 8530 RVA: 0x0005ECF3 File Offset: 0x0005CEF3
		public Cluster(ProgramSetBuilder<TNode> programSet, TNode bestNode, double bestScore, IReadOnlyList<State> data, IReadOnlyList<State> allMatchingData, uint dataCount, string description = null)
		{
			this.BestProgramNode = bestNode;
			this.BestProgramScore = bestScore;
			this.Data = data;
			this.ProgramSet = programSet;
			this.AllMatchingData = allMatchingData;
			this.DataCount = dataCount;
			this.Description = description;
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0005ED30 File Offset: 0x0005CF30
		public bool Equals(Cluster<TNode> other)
		{
			if (other == this)
			{
				return true;
			}
			if (other == null)
			{
				return false;
			}
			TNode bestProgramNode = this.BestProgramNode;
			return bestProgramNode.Equals(other.BestProgramNode);
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x0005ED68 File Offset: 0x0005CF68
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			Cluster<TNode> cluster = obj as Cluster<TNode>;
			return cluster != null && this.Equals(cluster);
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x0005ED9C File Offset: 0x0005CF9C
		public override int GetHashCode()
		{
			TNode bestProgramNode = this.BestProgramNode;
			return bestProgramNode.GetHashCode();
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x0005EDBD File Offset: 0x0005CFBD
		public static bool operator ==(Cluster<TNode> left, Cluster<TNode> right)
		{
			return right == left || (right == null == (left == null) && left.Equals(right));
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x0005EDD8 File Offset: 0x0005CFD8
		public static bool operator !=(Cluster<TNode> left, Cluster<TNode> right)
		{
			return !(left == right);
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
		public double BestProgramScore { get; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06002159 RID: 8537 RVA: 0x0005EDEC File Offset: 0x0005CFEC
		public TNode BestProgramNode { get; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x0005EDF4 File Offset: 0x0005CFF4
		public ProgramSetBuilder<TNode> ProgramSet { get; }

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x0600215B RID: 8539 RVA: 0x0005EDFC File Offset: 0x0005CFFC
		public IReadOnlyList<State> Data { get; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600215C RID: 8540 RVA: 0x0005EE04 File Offset: 0x0005D004
		public IReadOnlyList<State> AllMatchingData { get; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x0600215D RID: 8541 RVA: 0x0005EE0C File Offset: 0x0005D00C
		public uint DataCount { get; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x0005EE14 File Offset: 0x0005D014
		public string Description { get; }

		// Token: 0x0600215F RID: 8543 RVA: 0x0005EE1C File Offset: 0x0005D01C
		public override string ToString()
		{
			return this.Description ?? base.ToString();
		}
	}
}
