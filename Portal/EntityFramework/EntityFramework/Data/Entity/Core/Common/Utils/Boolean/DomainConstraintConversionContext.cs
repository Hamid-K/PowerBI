using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200060E RID: 1550
	internal sealed class DomainConstraintConversionContext<T_Variable, T_Element> : ConversionContext<DomainConstraint<T_Variable, T_Element>>
	{
		// Token: 0x06004B70 RID: 19312 RVA: 0x0010A6D8 File Offset: 0x001088D8
		internal override Vertex TranslateTermToVertex(TermExpr<DomainConstraint<T_Variable, T_Element>> term)
		{
			Set<T_Element> range = term.Identifier.Range;
			DomainVariable<T_Variable, T_Element> variable = term.Identifier.Variable;
			Set<T_Element> domain = variable.Domain;
			if (range.All((T_Element element) => !domain.Contains(element)))
			{
				return Vertex.Zero;
			}
			if (domain.All((T_Element element) => range.Contains(element)))
			{
				return Vertex.One;
			}
			Vertex[] array = domain.Select(delegate(T_Element element)
			{
				if (!range.Contains(element))
				{
					return Vertex.Zero;
				}
				return Vertex.One;
			}).ToArray<Vertex>();
			int num;
			if (!this._domainVariableToRobddVariableMap.TryGetValue(variable, out num))
			{
				num = this.Solver.CreateVariable();
				this._domainVariableToRobddVariableMap[variable] = num;
			}
			return this.Solver.CreateLeafVertex(num, array);
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x0010A7A5 File Offset: 0x001089A5
		internal override IEnumerable<LiteralVertexPair<DomainConstraint<T_Variable, T_Element>>> GetSuccessors(Vertex vertex)
		{
			this.InitializeInverseMap();
			DomainVariable<T_Variable, T_Element> domainVariable = this._inverseMap[vertex.Variable];
			T_Element[] array = domainVariable.Domain.ToArray();
			Dictionary<Vertex, Set<T_Element>> dictionary = new Dictionary<Vertex, Set<T_Element>>();
			for (int i = 0; i < vertex.Children.Length; i++)
			{
				Vertex vertex2 = vertex.Children[i];
				Set<T_Element> set;
				if (!dictionary.TryGetValue(vertex2, out set))
				{
					set = new Set<T_Element>(domainVariable.Domain.Comparer);
					dictionary.Add(vertex2, set);
				}
				set.Add(array[i]);
			}
			foreach (KeyValuePair<Vertex, Set<T_Element>> keyValuePair in dictionary)
			{
				Vertex key = keyValuePair.Key;
				Set<T_Element> value = keyValuePair.Value;
				Literal<DomainConstraint<T_Variable, T_Element>> literal = new Literal<DomainConstraint<T_Variable, T_Element>>(new TermExpr<DomainConstraint<T_Variable, T_Element>>(new DomainConstraint<T_Variable, T_Element>(domainVariable, value.MakeReadOnly())), true);
				yield return new LiteralVertexPair<DomainConstraint<T_Variable, T_Element>>(key, literal);
			}
			Dictionary<Vertex, Set<T_Element>>.Enumerator enumerator = default(Dictionary<Vertex, Set<T_Element>>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x0010A7BC File Offset: 0x001089BC
		private void InitializeInverseMap()
		{
			if (this._inverseMap == null)
			{
				this._inverseMap = this._domainVariableToRobddVariableMap.ToDictionary((KeyValuePair<DomainVariable<T_Variable, T_Element>, int> kvp) => kvp.Value, (KeyValuePair<DomainVariable<T_Variable, T_Element>, int> kvp) => kvp.Key);
			}
		}

		// Token: 0x04001A5C RID: 6748
		private readonly Dictionary<DomainVariable<T_Variable, T_Element>, int> _domainVariableToRobddVariableMap = new Dictionary<DomainVariable<T_Variable, T_Element>, int>();

		// Token: 0x04001A5D RID: 6749
		private Dictionary<int, DomainVariable<T_Variable, T_Element>> _inverseMap;
	}
}
