using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200061E RID: 1566
	internal sealed class Solver
	{
		// Token: 0x06004BBD RID: 19389 RVA: 0x0010B254 File Offset: 0x00109454
		internal int CreateVariable()
		{
			int num = this._variableCount + 1;
			this._variableCount = num;
			return num;
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x0010B272 File Offset: 0x00109472
		internal Vertex Not(Vertex vertex)
		{
			return this.IfThenElse(vertex, Vertex.Zero, Vertex.One);
		}

		// Token: 0x06004BBF RID: 19391 RVA: 0x0010B285 File Offset: 0x00109485
		internal Vertex And(IEnumerable<Vertex> children)
		{
			return children.OrderByDescending((Vertex child) => child.Variable).Aggregate(Vertex.One, (Vertex left, Vertex right) => this.IfThenElse(left, right, Vertex.Zero));
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x0010B2C2 File Offset: 0x001094C2
		internal Vertex And(Vertex left, Vertex right)
		{
			return this.IfThenElse(left, right, Vertex.Zero);
		}

		// Token: 0x06004BC1 RID: 19393 RVA: 0x0010B2D1 File Offset: 0x001094D1
		internal Vertex Or(IEnumerable<Vertex> children)
		{
			return children.OrderByDescending((Vertex child) => child.Variable).Aggregate(Vertex.Zero, (Vertex left, Vertex right) => this.IfThenElse(left, Vertex.One, right));
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x0010B30E File Offset: 0x0010950E
		internal Vertex CreateLeafVertex(int variable, Vertex[] children)
		{
			return this.GetUniqueVertex(variable, children);
		}

		// Token: 0x06004BC3 RID: 19395 RVA: 0x0010B318 File Offset: 0x00109518
		private Vertex GetUniqueVertex(int variable, Vertex[] children)
		{
			Vertex vertex = new Vertex(variable, children);
			Vertex vertex2;
			if (this._knownVertices.TryGetValue(vertex, out vertex2))
			{
				return vertex2;
			}
			this._knownVertices.Add(vertex, vertex);
			return vertex;
		}

		// Token: 0x06004BC4 RID: 19396 RVA: 0x0010B350 File Offset: 0x00109550
		private Vertex IfThenElse(Vertex condition, Vertex then, Vertex @else)
		{
			if (condition.IsOne())
			{
				return then;
			}
			if (condition.IsZero())
			{
				return @else;
			}
			if (then.IsOne() && @else.IsZero())
			{
				return condition;
			}
			if (then.Equals(@else))
			{
				return then;
			}
			Triple<Vertex, Vertex, Vertex> triple = new Triple<Vertex, Vertex, Vertex>(condition, then, @else);
			Vertex uniqueVertex;
			if (this._computedIfThenElseValues.TryGetValue(triple, out uniqueVertex))
			{
				return uniqueVertex;
			}
			int num2;
			int num = Solver.DetermineTopVariable(condition, then, @else, out num2);
			Vertex[] array = new Vertex[num2];
			bool flag = true;
			for (int i = 0; i < num2; i++)
			{
				array[i] = this.IfThenElse(Solver.EvaluateFor(condition, num, i), Solver.EvaluateFor(then, num, i), Solver.EvaluateFor(@else, num, i));
				if (i > 0 && flag && !array[i].Equals(array[0]))
				{
					flag = false;
				}
			}
			if (flag)
			{
				return array[0];
			}
			uniqueVertex = this.GetUniqueVertex(num, array);
			this._computedIfThenElseValues.Add(triple, uniqueVertex);
			return uniqueVertex;
		}

		// Token: 0x06004BC5 RID: 19397 RVA: 0x0010B434 File Offset: 0x00109634
		private static int DetermineTopVariable(Vertex condition, Vertex then, Vertex @else, out int topVariableDomainCount)
		{
			int num;
			if (condition.Variable < then.Variable)
			{
				num = condition.Variable;
				topVariableDomainCount = condition.Children.Length;
			}
			else
			{
				num = then.Variable;
				topVariableDomainCount = then.Children.Length;
			}
			if (@else.Variable < num)
			{
				num = @else.Variable;
				topVariableDomainCount = @else.Children.Length;
			}
			return num;
		}

		// Token: 0x06004BC6 RID: 19398 RVA: 0x0010B48E File Offset: 0x0010968E
		private static Vertex EvaluateFor(Vertex vertex, int variable, int variableAssignment)
		{
			if (variable < vertex.Variable)
			{
				return vertex;
			}
			return vertex.Children[variableAssignment];
		}

		// Token: 0x06004BC7 RID: 19399 RVA: 0x0010B4A4 File Offset: 0x001096A4
		[Conditional("DEBUG")]
		private void AssertVerticesValid(IEnumerable<Vertex> vertices)
		{
			foreach (Vertex vertex in vertices)
			{
			}
		}

		// Token: 0x06004BC8 RID: 19400 RVA: 0x0010B4E8 File Offset: 0x001096E8
		[Conditional("DEBUG")]
		private void AssertVertexValid(Vertex vertex)
		{
			vertex.IsSink();
		}

		// Token: 0x04001A78 RID: 6776
		private readonly Dictionary<Triple<Vertex, Vertex, Vertex>, Vertex> _computedIfThenElseValues = new Dictionary<Triple<Vertex, Vertex, Vertex>, Vertex>();

		// Token: 0x04001A79 RID: 6777
		private readonly Dictionary<Vertex, Vertex> _knownVertices = new Dictionary<Vertex, Vertex>(Solver.VertexValueComparer.Instance);

		// Token: 0x04001A7A RID: 6778
		private int _variableCount;

		// Token: 0x04001A7B RID: 6779
		internal static readonly Vertex[] BooleanVariableChildren = new Vertex[]
		{
			Vertex.One,
			Vertex.Zero
		};

		// Token: 0x02000C57 RID: 3159
		private class VertexValueComparer : IEqualityComparer<Vertex>
		{
			// Token: 0x06006A99 RID: 27289 RVA: 0x0016C226 File Offset: 0x0016A426
			private VertexValueComparer()
			{
			}

			// Token: 0x06006A9A RID: 27290 RVA: 0x0016C230 File Offset: 0x0016A430
			public bool Equals(Vertex x, Vertex y)
			{
				if (x.IsSink())
				{
					return x.Equals(y);
				}
				if (x.Variable != y.Variable || x.Children.Length != y.Children.Length)
				{
					return false;
				}
				for (int i = 0; i < x.Children.Length; i++)
				{
					if (!x.Children[i].Equals(y.Children[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06006A9B RID: 27291 RVA: 0x0016C29C File Offset: 0x0016A49C
			public int GetHashCode(Vertex vertex)
			{
				if (vertex.IsSink())
				{
					return vertex.GetHashCode();
				}
				return (vertex.Children[0].GetHashCode() << 5) + 1 + vertex.Children[1].GetHashCode();
			}

			// Token: 0x040030D5 RID: 12501
			internal static readonly Solver.VertexValueComparer Instance = new Solver.VertexValueComparer();
		}
	}
}
