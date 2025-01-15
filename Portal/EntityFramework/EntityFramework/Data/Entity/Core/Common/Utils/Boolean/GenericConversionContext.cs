using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000612 RID: 1554
	internal sealed class GenericConversionContext<T_Identifier> : ConversionContext<T_Identifier>
	{
		// Token: 0x06004B82 RID: 19330 RVA: 0x0010A984 File Offset: 0x00108B84
		internal override Vertex TranslateTermToVertex(TermExpr<T_Identifier> term)
		{
			int num;
			if (!this._variableMap.TryGetValue(term, out num))
			{
				num = this.Solver.CreateVariable();
				this._variableMap.Add(term, num);
			}
			return this.Solver.CreateLeafVertex(num, Solver.BooleanVariableChildren);
		}

		// Token: 0x06004B83 RID: 19331 RVA: 0x0010A9CC File Offset: 0x00108BCC
		internal override IEnumerable<LiteralVertexPair<T_Identifier>> GetSuccessors(Vertex vertex)
		{
			LiteralVertexPair<T_Identifier>[] array = new LiteralVertexPair<T_Identifier>[2];
			Vertex vertex2 = vertex.Children[0];
			Vertex vertex3 = vertex.Children[1];
			this.InitializeInverseVariableMap();
			Literal<T_Identifier> literal = new Literal<T_Identifier>(this._inverseVariableMap[vertex.Variable], true);
			array[0] = new LiteralVertexPair<T_Identifier>(vertex2, literal);
			literal = literal.MakeNegated();
			array[1] = new LiteralVertexPair<T_Identifier>(vertex3, literal);
			return array;
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x0010AA2C File Offset: 0x00108C2C
		private void InitializeInverseVariableMap()
		{
			if (this._inverseVariableMap == null)
			{
				this._inverseVariableMap = this._variableMap.ToDictionary((KeyValuePair<TermExpr<T_Identifier>, int> kvp) => kvp.Value, (KeyValuePair<TermExpr<T_Identifier>, int> kvp) => kvp.Key);
			}
		}

		// Token: 0x04001A6A RID: 6762
		private readonly Dictionary<TermExpr<T_Identifier>, int> _variableMap = new Dictionary<TermExpr<T_Identifier>, int>();

		// Token: 0x04001A6B RID: 6763
		private Dictionary<int, TermExpr<T_Identifier>> _inverseVariableMap;
	}
}
