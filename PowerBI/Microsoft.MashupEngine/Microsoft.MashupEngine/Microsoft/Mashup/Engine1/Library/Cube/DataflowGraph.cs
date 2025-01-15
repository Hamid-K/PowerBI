using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D1B RID: 3355
	internal class DataflowGraph : Graph<ScopePath, Dimensionality, Constraint[]>
	{
		// Token: 0x06005A74 RID: 23156 RVA: 0x0013C024 File Offset: 0x0013A224
		public static DataflowGraph From(SetContextProvider provider, ICube cube, Set set)
		{
			DataflowGraph dataflowGraph = new DataflowGraph(provider, cube);
			dataflowGraph.Add(ScopePath.Default, Dimensionality.Empty);
			foreach (ICubeHierarchy cubeHierarchy in set.Dimensionality.Hierarchies)
			{
				ScopePath scopePath;
				cubeHierarchy.GetUnscoped(out scopePath);
				dataflowGraph.Add(scopePath, new Dimensionality(new CubeLevelRange[] { set.Dimensionality.GetLevelRange(cubeHierarchy) }));
			}
			foreach (Set set2 in set.GetSubsets())
			{
				foreach (ICubeHierarchy cubeHierarchy2 in set2.Dimensionality.Hierarchies)
				{
					ScopePath scopePath2;
					cubeHierarchy2.GetUnscoped(out scopePath2);
					dataflowGraph.Add(scopePath2, new Dimensionality(new CubeLevelRange[] { set2.Dimensionality.GetLevelRange(cubeHierarchy2) }));
				}
			}
			foreach (ScopePath scopePath3 in dataflowGraph.Keys)
			{
				for (int i = scopePath3.Path.Length - 1; i >= 0; i--)
				{
					ScopePath scopePath4 = new ScopePath(DataflowGraph.GetArrayPrefix(scopePath3.Path, i));
					if (dataflowGraph.ContainsNode(scopePath4))
					{
						dataflowGraph.Add(scopePath3, scopePath4, EmptyArray<Constraint>.Instance);
						break;
					}
				}
			}
			return dataflowGraph;
		}

		// Token: 0x06005A75 RID: 23157 RVA: 0x0013C1DC File Offset: 0x0013A3DC
		public DataflowGraph(SetContextProvider provider, ICube cube)
		{
			this.provider = provider;
			this.cube = cube;
		}

		// Token: 0x06005A76 RID: 23158 RVA: 0x0013C1F4 File Offset: 0x0013A3F4
		public void AddConstraints(Set set, out Set remaining)
		{
			Dictionary<ScopePath, HashSet<ICubeMeasure>> dictionary = null;
			foreach (ICubeObject cubeObject in set.GetResultObjects())
			{
				ScopePath scopePath;
				ICubeMeasure cubeMeasure = cubeObject.GetUnscoped(out scopePath) as ICubeMeasure;
				if (cubeMeasure != null)
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<ScopePath, HashSet<ICubeMeasure>>();
					}
					HashSet<ICubeMeasure> hashSet;
					if (!dictionary.TryGetValue(scopePath, out hashSet))
					{
						hashSet = new HashSet<ICubeMeasure>();
						dictionary.Add(scopePath, hashSet);
					}
					hashSet.Add(cubeMeasure);
				}
			}
			remaining = EverythingSet.Instance;
			foreach (Set set2 in set.GetSubsets())
			{
				FilterSet filterSet = set2 as FilterSet;
				if (filterSet != null)
				{
					using (IEnumerator<CubeExpression> enumerator3 = filterSet.Predicate.GetConjunctiveNF().GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							CubeExpression cubeExpression = enumerator3.Current;
							Set set3;
							if (!this.provider.TryCompileScalarExpression(this.cube, filterSet.Dimensionality, cubeExpression, out set3))
							{
								throw new NotSupportedException();
							}
							BinaryCubeExpression binaryCubeExpression = cubeExpression as BinaryCubeExpression;
							IdentifierCubeExpression identifierCubeExpression;
							IdentifierCubeExpression identifierCubeExpression2;
							ICubeObject cubeObject2;
							ICubeObject cubeObject3;
							if (binaryCubeExpression != null && binaryCubeExpression.Operator == BinaryOperator2.Equals && binaryCubeExpression.Left.TryGetIdentifier(out identifierCubeExpression) && binaryCubeExpression.Right.TryGetIdentifier(out identifierCubeExpression2) && this.cube.TryGetObject(identifierCubeExpression, out cubeObject2) && this.cube.TryGetObject(identifierCubeExpression2, out cubeObject3))
							{
								ScopePath scopePath2;
								bool flag = cubeObject2.GetUnscoped(out scopePath2) is ICubeLevel;
								ScopePath scopePath3;
								ICubeLevel cubeLevel = cubeObject3.GetUnscoped(out scopePath3) as ICubeLevel;
								if (flag && cubeLevel != null)
								{
									Constraint[] array = new Constraint[]
									{
										new DataflowGraph.EqualityConstraint((ICubeLevel)cubeObject2, (ICubeLevel)cubeObject3, set3)
									};
									base.Add(scopePath2, scopePath3, array);
									Constraint[] array2 = new Constraint[]
									{
										new DataflowGraph.EqualityConstraint((ICubeLevel)cubeObject3, (ICubeLevel)cubeObject2, set3)
									};
									base.Add(scopePath3, scopePath2, array2);
									continue;
								}
							}
							bool flag2 = true;
							foreach (CubeExpression cubeExpression2 in cubeExpression.GetDisjunctiveNF())
							{
								BinaryCubeExpression binaryCubeExpression2 = cubeExpression2 as BinaryCubeExpression;
								IdentifierCubeExpression identifierCubeExpression3;
								ConstantCubeExpression constantCubeExpression;
								ICubeObject cubeObject4;
								if (binaryCubeExpression2 == null || binaryCubeExpression2.Operator != BinaryOperator2.NotEquals || ((!binaryCubeExpression2.Left.TryGetIdentifier(out identifierCubeExpression3) || !binaryCubeExpression2.Right.TryGetConstant(out constantCubeExpression)) && (!binaryCubeExpression2.Right.TryGetIdentifier(out identifierCubeExpression3) || !binaryCubeExpression2.Left.TryGetConstant(out constantCubeExpression))) || !this.cube.TryGetObject(identifierCubeExpression3, out cubeObject4) || !(cubeObject4 is ICubeMeasure) || !constantCubeExpression.Value.IsNull)
								{
									flag2 = false;
									break;
								}
								ScopePath scopePath4;
								ICubeMeasure cubeMeasure2 = (ICubeMeasure)cubeObject4.GetUnscoped(out scopePath4);
								HashSet<ICubeMeasure> hashSet2;
								if (dictionary != null && dictionary.TryGetValue(scopePath4, out hashSet2))
								{
									hashSet2.Remove(cubeMeasure2);
								}
							}
							if (flag2)
							{
								if (!dictionary.Values.Any((HashSet<ICubeMeasure> m) => m.Any<ICubeMeasure>()))
								{
									using (Dictionary<ScopePath, HashSet<ICubeMeasure>>.KeyCollection.Enumerator enumerator5 = dictionary.Keys.GetEnumerator())
									{
										while (enumerator5.MoveNext())
										{
											ScopePath scopePath5 = enumerator5.Current;
											this.AddMeasureConstraints(scopePath5);
										}
										continue;
									}
								}
							}
							IList<ScopePath> list;
							cubeExpression.GetUnscoped(out list);
							for (int i = 0; i < list.Count; i++)
							{
								ScopePath scopePath6 = list[i];
								for (int j = i + 1; j < list.Count; j++)
								{
									ScopePath scopePath7 = list[j];
									if (!scopePath6.Equals(scopePath7))
									{
										Constraint[] array3 = new Constraint[]
										{
											new DataflowGraph.UnknownConstraint(set3)
										};
										base.Add(scopePath6, scopePath7, array3);
										base.Add(scopePath7, scopePath6, array3);
									}
								}
							}
							remaining = remaining.Intersect(set3);
						}
						continue;
					}
				}
				remaining = remaining.Intersect(set2);
			}
		}

		// Token: 0x06005A77 RID: 23159 RVA: 0x0013C66C File Offset: 0x0013A86C
		public Dictionary<ScopePath, ScopePath> GetReplacements(out Set remaining)
		{
			HashSet<DataflowGraph.ScopePathPair> mergeCandidates = this.GetMergeCandidates(out remaining);
			return this.GetReplacements(mergeCandidates);
		}

		// Token: 0x06005A78 RID: 23160 RVA: 0x0013C688 File Offset: 0x0013A888
		protected override Dimensionality Merge(Dimensionality dimensionality1, Dimensionality dimensionality2)
		{
			return dimensionality1.Union(dimensionality2);
		}

		// Token: 0x06005A79 RID: 23161 RVA: 0x0013C691 File Offset: 0x0013A891
		protected override Constraint[] Merge(Constraint[] constraints1, Constraint[] constraints2)
		{
			return constraints1.Add(constraints2);
		}

		// Token: 0x06005A7A RID: 23162 RVA: 0x0013C69C File Offset: 0x0013A89C
		private static string[] GetArrayPrefix(string[] array, int length)
		{
			string[] array2 = new string[length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = array[i];
			}
			return array2;
		}

		// Token: 0x06005A7B RID: 23163 RVA: 0x0013C6C8 File Offset: 0x0013A8C8
		private static bool IsIndependent(ICube cube, ICubeLevel level)
		{
			ICube2 icube = DataflowGraph.GetICube2(cube);
			if (icube != null)
			{
				ScopePath scopePath;
				ICubeLevel unscoped = level.GetUnscoped(out scopePath);
				return icube.IsIndependent(unscoped);
			}
			return true;
		}

		// Token: 0x06005A7C RID: 23164 RVA: 0x0013C6F4 File Offset: 0x0013A8F4
		private static bool AreRelated(ICube cube, ICubeLevel level1, ICubeLevel level2)
		{
			ICube2 icube = DataflowGraph.GetICube2(cube);
			if (icube != null)
			{
				ScopePath scopePath;
				ICubeLevel unscoped = level1.GetUnscoped(out scopePath);
				ScopePath scopePath2;
				ICubeLevel unscoped2 = level2.GetUnscoped(out scopePath2);
				return icube.AreRelated(unscoped, unscoped2);
			}
			return false;
		}

		// Token: 0x06005A7D RID: 23165 RVA: 0x0013C72C File Offset: 0x0013A92C
		private static ICube2 GetICube2(ICube cube)
		{
			ScopeCube scopeCube = cube as ScopeCube;
			if (scopeCube != null)
			{
				cube = scopeCube[ScopeCube.unscopedScope];
			}
			return cube as ICube2;
		}

		// Token: 0x06005A7E RID: 23166 RVA: 0x0013C758 File Offset: 0x0013A958
		private void AddMeasureConstraints(ScopePath parent)
		{
			ScopePath[] array = (from child in base.KeysTo(parent)
				where !this.GetEdge(child, parent).Any<Constraint>()
				select child).ToArray<ScopePath>();
			foreach (ScopePath scopePath in array)
			{
				this.AddMeasureConstraints(scopePath);
				foreach (ScopePath scopePath2 in array)
				{
					Constraint[] array4;
					Constraint[] array5;
					if (!scopePath.Equals(scopePath2) && (!base.TryGetEdge(scopePath, scopePath2, out array4) || !array4.Any<Constraint>()) && (!base.TryGetEdge(scopePath2, scopePath, out array5) || !array5.Any<Constraint>()))
					{
						base.Add(scopePath, scopePath2, new Constraint[] { DataflowGraph.MeasureConstraint.Instance });
						base.Add(scopePath2, scopePath, new Constraint[] { DataflowGraph.MeasureConstraint.Instance });
					}
				}
			}
		}

		// Token: 0x06005A7F RID: 23167 RVA: 0x0013C845 File Offset: 0x0013AA45
		private IEnumerable<ICubeLevel> GetIndependentLevels(Dimensionality dimensionality)
		{
			foreach (ICubeHierarchy cubeHierarchy in dimensionality.Hierarchies)
			{
				ICubeLevel fine = dimensionality.GetLevelRange(cubeHierarchy).Fine;
				if (DataflowGraph.IsIndependent(this.cube, fine))
				{
					yield return fine;
				}
			}
			IEnumerator<ICubeHierarchy> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06005A80 RID: 23168 RVA: 0x0013C85C File Offset: 0x0013AA5C
		private HashSet<DataflowGraph.ScopePathPair> GetMergeCandidates(out Set remaining)
		{
			remaining = EverythingSet.Instance;
			HashSet<DataflowGraph.ScopePathPair> hashSet = new HashSet<DataflowGraph.ScopePathPair>();
			HashSet<DataflowGraph.ScopePathPair> hashSet2 = new HashSet<DataflowGraph.ScopePathPair>();
			foreach (ScopePath scopePath in base.Keys)
			{
				Dimensionality node = base.GetNode(scopePath);
				IEnumerable<ICubeLevel> independentLevels = this.GetIndependentLevels(node);
				foreach (ScopePath scopePath2 in base.KeysFrom(scopePath))
				{
					DataflowGraph.ScopePathPair scopePathPair = new DataflowGraph.ScopePathPair(scopePath, scopePath2);
					if (hashSet.Add(scopePathPair))
					{
						Dimensionality node2 = base.GetNode(scopePath2);
						IEnumerable<ICubeLevel> independentLevels2 = this.GetIndependentLevels(node2);
						bool flag = false;
						bool flag2 = false;
						Set set = EverythingSet.Instance;
						HashSet<ICubeLevel> fromConstrainedLevels = new HashSet<ICubeLevel>();
						HashSet<ICubeLevel> toConstrainedLevels = new HashSet<ICubeLevel>();
						foreach (Constraint constraint in base.GetEdge(scopePath, scopePath2))
						{
							if (constraint is DataflowGraph.UnknownConstraint)
							{
								flag = true;
							}
							else if (constraint is DataflowGraph.MeasureConstraint)
							{
								flag2 = true;
							}
							else
							{
								DataflowGraph.EqualityConstraint equalityConstraint = constraint as DataflowGraph.EqualityConstraint;
								if (equalityConstraint != null)
								{
									if (DataflowGraph.AreRelated(this.cube, equalityConstraint.From, equalityConstraint.To))
									{
										fromConstrainedLevels.Add(equalityConstraint.From);
										toConstrainedLevels.Add(equalityConstraint.To);
										set = set.Intersect(equalityConstraint.Filter);
									}
									else
									{
										remaining = remaining.Intersect(equalityConstraint.Filter);
									}
								}
							}
						}
						if (!flag && (flag2 || (independentLevels.Any<ICubeLevel>() && independentLevels.All((ICubeLevel l) => fromConstrainedLevels.Contains(l))) || (independentLevels2.Any<ICubeLevel>() && independentLevels2.All((ICubeLevel l) => toConstrainedLevels.Contains(l)))))
						{
							hashSet2.Add(scopePathPair);
						}
						else
						{
							remaining = remaining.Intersect(set);
						}
					}
				}
				if (base.KeysFrom(scopePath).HasExactlyOneElement<ScopePath>())
				{
					ScopePath scopePath3 = base.KeysFrom(scopePath).Single<ScopePath>();
					if (base.KeysTo(scopePath3).HasExactlyOneElement<ScopePath>() && !base.GetEdge(scopePath, scopePath3).Any<Constraint>())
					{
						DataflowGraph.ScopePathPair scopePathPair2 = new DataflowGraph.ScopePathPair(scopePath, scopePath3);
						hashSet2.Add(scopePathPair2);
					}
				}
			}
			return hashSet2;
		}

		// Token: 0x06005A81 RID: 23169 RVA: 0x0013CAE8 File Offset: 0x0013ACE8
		private Dictionary<ScopePath, ScopePath> GetReplacements(HashSet<DataflowGraph.ScopePathPair> candidates)
		{
			HashSet<HashSet<ScopePath>> hashSet = new HashSet<HashSet<ScopePath>>();
			foreach (DataflowGraph.ScopePathPair scopePathPair in candidates)
			{
				HashSet<ScopePath> hashSet2 = null;
				HashSet<ScopePath> group2 = null;
				foreach (HashSet<ScopePath> hashSet3 in hashSet)
				{
					if (hashSet3.Contains(scopePathPair.Path1))
					{
						if (hashSet2 != null)
						{
							throw new InvalidOperationException();
						}
						hashSet2 = hashSet3;
					}
					if (hashSet3.Contains(scopePathPair.Path2))
					{
						if (group2 != null)
						{
							throw new InvalidOperationException();
						}
						group2 = hashSet3;
					}
				}
				if (hashSet2 == null)
				{
					hashSet2 = new HashSet<ScopePath>();
					hashSet2.Add(scopePathPair.Path1);
				}
				if (group2 == null)
				{
					group2 = new HashSet<ScopePath>();
					group2.Add(scopePathPair.Path2);
				}
				if (hashSet2.All((ScopePath s1) => group2.All(delegate(ScopePath s2)
				{
					if (this.GetAllConstraints(s1, s2).All((Constraint c) => c is DataflowGraph.EqualityConstraint || c is DataflowGraph.MeasureConstraint))
					{
						return this.GetAllConstraints(s2, s1).All((Constraint c) => c is DataflowGraph.EqualityConstraint || c is DataflowGraph.MeasureConstraint);
					}
					return false;
				})))
				{
					hashSet2.UnionWith(group2);
					hashSet.Remove(group2);
					hashSet.Add(hashSet2);
				}
				else
				{
					hashSet.Add(hashSet2);
					hashSet.Add(group2);
				}
			}
			Dictionary<ScopePath, ScopePath> dictionary = new Dictionary<ScopePath, ScopePath>();
			foreach (HashSet<ScopePath> hashSet4 in hashSet)
			{
				ScopePath scopePath = hashSet4.OrderBy((ScopePath s) => s.ToString()).First<ScopePath>();
				foreach (ScopePath scopePath2 in hashSet4)
				{
					if (!scopePath2.Equals(scopePath))
					{
						dictionary.Add(scopePath2, scopePath);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06005A82 RID: 23170 RVA: 0x0013CD4C File Offset: 0x0013AF4C
		private IEnumerable<Constraint> GetAllConstraints(ScopePath fromPath, ScopePath toPath)
		{
			HashSet<ScopePath> hashSet = new HashSet<ScopePath>();
			HashSet<Constraint> hashSet2 = new HashSet<Constraint>();
			this.AddConstraints(hashSet, hashSet2, fromPath, toPath);
			return hashSet2;
		}

		// Token: 0x06005A83 RID: 23171 RVA: 0x0013CD74 File Offset: 0x0013AF74
		private bool AddConstraints(HashSet<ScopePath> visited, HashSet<Constraint> constraints, ScopePath current, ScopePath endPath)
		{
			if (current.Equals(endPath))
			{
				return true;
			}
			bool flag = false;
			foreach (ScopePath scopePath in base.KeysFrom(current))
			{
				if (visited.Add(scopePath) && this.AddConstraints(visited, constraints, scopePath, endPath))
				{
					flag = true;
					Constraint[] array;
					if (base.TryGetEdge(current, scopePath, out array))
					{
						constraints.UnionWith(array);
					}
				}
			}
			return flag;
		}

		// Token: 0x040032A4 RID: 12964
		private readonly SetContextProvider provider;

		// Token: 0x040032A5 RID: 12965
		private readonly ICube cube;

		// Token: 0x02000D1C RID: 3356
		private class EqualityConstraint : Constraint
		{
			// Token: 0x06005A84 RID: 23172 RVA: 0x0013CDF8 File Offset: 0x0013AFF8
			public EqualityConstraint(ICubeLevel from, ICubeLevel to, Set filter)
			{
				this.From = from;
				this.To = to;
				this.Filter = filter;
			}

			// Token: 0x17001ADC RID: 6876
			// (get) Token: 0x06005A85 RID: 23173 RVA: 0x0013CE15 File Offset: 0x0013B015
			// (set) Token: 0x06005A86 RID: 23174 RVA: 0x0013CE1D File Offset: 0x0013B01D
			public ICubeLevel From { get; private set; }

			// Token: 0x17001ADD RID: 6877
			// (get) Token: 0x06005A87 RID: 23175 RVA: 0x0013CE26 File Offset: 0x0013B026
			// (set) Token: 0x06005A88 RID: 23176 RVA: 0x0013CE2E File Offset: 0x0013B02E
			public ICubeLevel To { get; private set; }

			// Token: 0x17001ADE RID: 6878
			// (get) Token: 0x06005A89 RID: 23177 RVA: 0x0013CE37 File Offset: 0x0013B037
			// (set) Token: 0x06005A8A RID: 23178 RVA: 0x0013CE3F File Offset: 0x0013B03F
			public Set Filter { get; private set; }
		}

		// Token: 0x02000D1D RID: 3357
		private class UnknownConstraint : Constraint
		{
			// Token: 0x06005A8B RID: 23179 RVA: 0x0013CE48 File Offset: 0x0013B048
			public UnknownConstraint(Set filter)
			{
				this.Filter = filter;
			}

			// Token: 0x17001ADF RID: 6879
			// (get) Token: 0x06005A8C RID: 23180 RVA: 0x0013CE57 File Offset: 0x0013B057
			// (set) Token: 0x06005A8D RID: 23181 RVA: 0x0013CE5F File Offset: 0x0013B05F
			public Set Filter { get; private set; }
		}

		// Token: 0x02000D1E RID: 3358
		private class MeasureConstraint : Constraint
		{
			// Token: 0x06005A8E RID: 23182 RVA: 0x0013CE68 File Offset: 0x0013B068
			private MeasureConstraint()
			{
			}

			// Token: 0x040032AA RID: 12970
			public static readonly Constraint Instance = new DataflowGraph.MeasureConstraint();
		}

		// Token: 0x02000D1F RID: 3359
		private class ScopePathPair
		{
			// Token: 0x06005A90 RID: 23184 RVA: 0x0013CE7C File Offset: 0x0013B07C
			public ScopePathPair(ScopePath path1, ScopePath path2)
			{
				this.Path1 = path1;
				this.Path2 = path2;
			}

			// Token: 0x17001AE0 RID: 6880
			// (get) Token: 0x06005A91 RID: 23185 RVA: 0x0013CE92 File Offset: 0x0013B092
			// (set) Token: 0x06005A92 RID: 23186 RVA: 0x0013CE9A File Offset: 0x0013B09A
			public ScopePath Path1 { get; private set; }

			// Token: 0x17001AE1 RID: 6881
			// (get) Token: 0x06005A93 RID: 23187 RVA: 0x0013CEA3 File Offset: 0x0013B0A3
			// (set) Token: 0x06005A94 RID: 23188 RVA: 0x0013CEAB File Offset: 0x0013B0AB
			public ScopePath Path2 { get; private set; }

			// Token: 0x06005A95 RID: 23189 RVA: 0x0013CEB4 File Offset: 0x0013B0B4
			public override bool Equals(object other)
			{
				return this.Equals(other as DataflowGraph.ScopePathPair);
			}

			// Token: 0x06005A96 RID: 23190 RVA: 0x0013CEC4 File Offset: 0x0013B0C4
			public bool Equals(DataflowGraph.ScopePathPair other)
			{
				return other != null && ((this.Path1.Equals(other.Path1) && this.Path2.Equals(other.Path2)) || (this.Path1.Equals(other.Path2) && this.Path2.Equals(other.Path1)));
			}

			// Token: 0x06005A97 RID: 23191 RVA: 0x0013CF24 File Offset: 0x0013B124
			public override int GetHashCode()
			{
				return this.Path1.GetHashCode() + this.Path2.GetHashCode();
			}
		}
	}
}
