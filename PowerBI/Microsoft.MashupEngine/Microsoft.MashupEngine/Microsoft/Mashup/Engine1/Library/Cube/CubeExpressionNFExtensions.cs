using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CE9 RID: 3305
	internal static class CubeExpressionNFExtensions
	{
		// Token: 0x060059B4 RID: 22964 RVA: 0x00139F67 File Offset: 0x00138167
		public static IList<CubeExpression> GetConjunctiveNF(this CubeExpression cubeExpression)
		{
			return (from n in CubeExpressionNFExtensions.ConvertToNode(cubeExpression).GetConjunctiveNF().GetConjunctions()
				select n.GetCubeExpression()).ToArray<CubeExpression>();
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x00139FA2 File Offset: 0x001381A2
		public static IList<CubeExpression> GetDisjunctiveNF(this CubeExpression cubeExpression)
		{
			return (from n in CubeExpressionNFExtensions.ConvertToNode(cubeExpression).GetDisjunctiveNF().GetDisjunctions()
				select n.GetCubeExpression()).ToArray<CubeExpression>();
		}

		// Token: 0x060059B6 RID: 22966 RVA: 0x00139FE0 File Offset: 0x001381E0
		private static CubeExpressionNFExtensions.Node ConvertToNode(CubeExpression expression)
		{
			if (expression.Kind == CubeExpressionKind.Binary)
			{
				BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)expression;
				BinaryOperator2 @operator = binaryCubeExpression.Operator;
				if (@operator == BinaryOperator2.And)
				{
					List<CubeExpression> list = new List<CubeExpression>();
					CubeExpressionNFExtensions.FlattenConjunctions(binaryCubeExpression, list);
					return new CubeExpressionNFExtensions.AndNode(list.ConvertAll<CubeExpressionNFExtensions.Node>(new Converter<CubeExpression, CubeExpressionNFExtensions.Node>(CubeExpressionNFExtensions.ConvertToNode)));
				}
				if (@operator == BinaryOperator2.Or)
				{
					List<CubeExpression> list = new List<CubeExpression>();
					CubeExpressionNFExtensions.FlattenDisjunctions(binaryCubeExpression, list);
					return new CubeExpressionNFExtensions.OrNode(list.ConvertAll<CubeExpressionNFExtensions.Node>(new Converter<CubeExpression, CubeExpressionNFExtensions.Node>(CubeExpressionNFExtensions.ConvertToNode)));
				}
			}
			return new CubeExpressionNFExtensions.LeafNode(expression);
		}

		// Token: 0x060059B7 RID: 22967 RVA: 0x0013A060 File Offset: 0x00138260
		private static void FlattenConjunctions(CubeExpression expression, List<CubeExpression> children)
		{
			if (expression.Kind == CubeExpressionKind.Binary)
			{
				BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)expression;
				if (binaryCubeExpression.Operator == BinaryOperator2.And)
				{
					CubeExpressionNFExtensions.FlattenConjunctions(binaryCubeExpression.Left, children);
					CubeExpressionNFExtensions.FlattenConjunctions(binaryCubeExpression.Right, children);
					return;
				}
			}
			children.Add(expression);
		}

		// Token: 0x060059B8 RID: 22968 RVA: 0x0013A0A8 File Offset: 0x001382A8
		private static void FlattenDisjunctions(CubeExpression expression, List<CubeExpression> children)
		{
			if (expression.Kind == CubeExpressionKind.Binary)
			{
				BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)expression;
				if (binaryCubeExpression.Operator == BinaryOperator2.Or)
				{
					CubeExpressionNFExtensions.FlattenDisjunctions(binaryCubeExpression.Left, children);
					CubeExpressionNFExtensions.FlattenDisjunctions(binaryCubeExpression.Right, children);
					return;
				}
			}
			children.Add(expression);
		}

		// Token: 0x02000CEA RID: 3306
		private abstract class Node
		{
			// Token: 0x060059B9 RID: 22969
			public abstract CubeExpression GetCubeExpression();

			// Token: 0x060059BA RID: 22970
			public abstract IList<CubeExpressionNFExtensions.Node> GetConjunctions();

			// Token: 0x060059BB RID: 22971
			public abstract IList<CubeExpressionNFExtensions.Node> GetDisjunctions();

			// Token: 0x060059BC RID: 22972
			public abstract CubeExpressionNFExtensions.Node GetConjunctiveNF();

			// Token: 0x060059BD RID: 22973
			public abstract CubeExpressionNFExtensions.Node GetDisjunctiveNF();

			// Token: 0x060059BE RID: 22974 RVA: 0x0013A0F0 File Offset: 0x001382F0
			protected static bool Increment(List<IList<CubeExpressionNFExtensions.Node>> childParts, int[] childIndices)
			{
				for (int i = 0; i < childIndices.Length; i++)
				{
					childIndices[i]++;
					if (childIndices[i] < childParts[i].Count)
					{
						return true;
					}
					childIndices[i] = 0;
				}
				return false;
			}
		}

		// Token: 0x02000CEB RID: 3307
		private sealed class AndNode : CubeExpressionNFExtensions.Node
		{
			// Token: 0x060059C0 RID: 22976 RVA: 0x0013A12F File Offset: 0x0013832F
			public AndNode(IList<CubeExpressionNFExtensions.Node> children)
			{
				this.children = children;
			}

			// Token: 0x060059C1 RID: 22977 RVA: 0x0013A140 File Offset: 0x00138340
			public override CubeExpression GetCubeExpression()
			{
				CubeExpression cubeExpression = null;
				foreach (CubeExpressionNFExtensions.Node node in this.children)
				{
					CubeExpression cubeExpression2 = node.GetCubeExpression();
					if (cubeExpression == null)
					{
						cubeExpression = cubeExpression2;
					}
					else
					{
						cubeExpression = new BinaryCubeExpression(BinaryOperator2.And, cubeExpression, cubeExpression2);
					}
				}
				return cubeExpression;
			}

			// Token: 0x060059C2 RID: 22978 RVA: 0x0013A1A0 File Offset: 0x001383A0
			public override CubeExpressionNFExtensions.Node GetConjunctiveNF()
			{
				List<CubeExpressionNFExtensions.Node> list = new List<CubeExpressionNFExtensions.Node>();
				foreach (CubeExpressionNFExtensions.Node node in this.children)
				{
					CubeExpressionNFExtensions.Node conjunctiveNF = node.GetConjunctiveNF();
					list.AddRange(conjunctiveNF.GetConjunctions());
				}
				return new CubeExpressionNFExtensions.AndNode(list);
			}

			// Token: 0x060059C3 RID: 22979 RVA: 0x0013A204 File Offset: 0x00138404
			public override CubeExpressionNFExtensions.Node GetDisjunctiveNF()
			{
				List<IList<CubeExpressionNFExtensions.Node>> list = new List<IList<CubeExpressionNFExtensions.Node>>();
				foreach (CubeExpressionNFExtensions.Node node in this.children)
				{
					list.Add(node.GetDisjunctiveNF().GetDisjunctions());
				}
				List<CubeExpressionNFExtensions.Node> list2 = new List<CubeExpressionNFExtensions.Node>();
				int[] array = new int[list.Count];
				do
				{
					CubeExpressionNFExtensions.Node[] array2 = new CubeExpressionNFExtensions.Node[array.Length];
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i] = list[i][array[i]];
					}
					list2.Add(new CubeExpressionNFExtensions.AndNode(array2));
				}
				while (CubeExpressionNFExtensions.Node.Increment(list, array));
				return new CubeExpressionNFExtensions.OrNode(list2);
			}

			// Token: 0x060059C4 RID: 22980 RVA: 0x0013A2C4 File Offset: 0x001384C4
			public override IList<CubeExpressionNFExtensions.Node> GetConjunctions()
			{
				return this.children;
			}

			// Token: 0x060059C5 RID: 22981 RVA: 0x0013A2CC File Offset: 0x001384CC
			public override IList<CubeExpressionNFExtensions.Node> GetDisjunctions()
			{
				return new CubeExpressionNFExtensions.Node[] { this };
			}

			// Token: 0x0400322D RID: 12845
			private readonly IList<CubeExpressionNFExtensions.Node> children;
		}

		// Token: 0x02000CEC RID: 3308
		private sealed class OrNode : CubeExpressionNFExtensions.Node
		{
			// Token: 0x060059C6 RID: 22982 RVA: 0x0013A2D8 File Offset: 0x001384D8
			public OrNode(IList<CubeExpressionNFExtensions.Node> children)
			{
				this.children = children;
			}

			// Token: 0x060059C7 RID: 22983 RVA: 0x0013A2E8 File Offset: 0x001384E8
			public override CubeExpression GetCubeExpression()
			{
				CubeExpression cubeExpression = null;
				foreach (CubeExpressionNFExtensions.Node node in this.children)
				{
					CubeExpression cubeExpression2 = node.GetCubeExpression();
					if (cubeExpression == null)
					{
						cubeExpression = cubeExpression2;
					}
					else
					{
						cubeExpression = new BinaryCubeExpression(BinaryOperator2.Or, cubeExpression, cubeExpression2);
					}
				}
				return cubeExpression;
			}

			// Token: 0x060059C8 RID: 22984 RVA: 0x0013A348 File Offset: 0x00138548
			public override CubeExpressionNFExtensions.Node GetConjunctiveNF()
			{
				List<IList<CubeExpressionNFExtensions.Node>> list = new List<IList<CubeExpressionNFExtensions.Node>>();
				foreach (CubeExpressionNFExtensions.Node node in this.children)
				{
					list.Add(node.GetConjunctiveNF().GetConjunctions());
				}
				List<CubeExpressionNFExtensions.Node> list2 = new List<CubeExpressionNFExtensions.Node>();
				int[] array = new int[list.Count];
				do
				{
					CubeExpressionNFExtensions.Node[] array2 = new CubeExpressionNFExtensions.Node[array.Length];
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i] = list[i][array[i]];
					}
					list2.Add(new CubeExpressionNFExtensions.OrNode(array2));
				}
				while (CubeExpressionNFExtensions.Node.Increment(list, array));
				return new CubeExpressionNFExtensions.AndNode(list2);
			}

			// Token: 0x060059C9 RID: 22985 RVA: 0x0013A408 File Offset: 0x00138608
			public override CubeExpressionNFExtensions.Node GetDisjunctiveNF()
			{
				List<CubeExpressionNFExtensions.Node> list = new List<CubeExpressionNFExtensions.Node>();
				foreach (CubeExpressionNFExtensions.Node node in this.children)
				{
					CubeExpressionNFExtensions.Node disjunctiveNF = node.GetDisjunctiveNF();
					list.AddRange(disjunctiveNF.GetDisjunctions());
				}
				return new CubeExpressionNFExtensions.OrNode(list);
			}

			// Token: 0x060059CA RID: 22986 RVA: 0x0013A2CC File Offset: 0x001384CC
			public override IList<CubeExpressionNFExtensions.Node> GetConjunctions()
			{
				return new CubeExpressionNFExtensions.Node[] { this };
			}

			// Token: 0x060059CB RID: 22987 RVA: 0x0013A46C File Offset: 0x0013866C
			public override IList<CubeExpressionNFExtensions.Node> GetDisjunctions()
			{
				return this.children;
			}

			// Token: 0x0400322E RID: 12846
			private readonly IList<CubeExpressionNFExtensions.Node> children;
		}

		// Token: 0x02000CED RID: 3309
		private sealed class LeafNode : CubeExpressionNFExtensions.Node
		{
			// Token: 0x060059CC RID: 22988 RVA: 0x0013A474 File Offset: 0x00138674
			public LeafNode(CubeExpression expression)
			{
				this.expression = expression;
			}

			// Token: 0x060059CD RID: 22989 RVA: 0x0013A483 File Offset: 0x00138683
			public override CubeExpression GetCubeExpression()
			{
				return this.expression;
			}

			// Token: 0x060059CE RID: 22990 RVA: 0x0013A2CC File Offset: 0x001384CC
			public override IList<CubeExpressionNFExtensions.Node> GetConjunctions()
			{
				return new CubeExpressionNFExtensions.Node[] { this };
			}

			// Token: 0x060059CF RID: 22991 RVA: 0x0013A2CC File Offset: 0x001384CC
			public override IList<CubeExpressionNFExtensions.Node> GetDisjunctions()
			{
				return new CubeExpressionNFExtensions.Node[] { this };
			}

			// Token: 0x060059D0 RID: 22992 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override CubeExpressionNFExtensions.Node GetConjunctiveNF()
			{
				return this;
			}

			// Token: 0x060059D1 RID: 22993 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override CubeExpressionNFExtensions.Node GetDisjunctiveNF()
			{
				return this;
			}

			// Token: 0x0400322F RID: 12847
			private readonly CubeExpression expression;
		}
	}
}
