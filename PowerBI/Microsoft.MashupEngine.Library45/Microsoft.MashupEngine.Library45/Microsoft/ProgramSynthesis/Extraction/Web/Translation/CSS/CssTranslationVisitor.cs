using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Translation.CSS
{
	// Token: 0x02001198 RID: 4504
	public class CssTranslationVisitor : ProgramNodeVisitor<CssSelector, CssSelector>
	{
		// Token: 0x06008600 RID: 34304 RVA: 0x001C220C File Offset: 0x001C040C
		public override CssSelector VisitNonterminal(NonterminalNode n, CssSelector css)
		{
			CssTranslationVisitor.<>c__DisplayClass1_0 CS$<>8__locals1 = new CssTranslationVisitor.<>c__DisplayClass1_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.css = css;
			CS$<>8__locals1.n = n;
			nodeCollection nodeCollection;
			if (CssTranslationVisitor.Build.Node.Is.nodeCollection(CS$<>8__locals1.n, out nodeCollection))
			{
				if (nodeCollection.Switch<bool>(CssTranslationVisitor.Build, (AsCollection asCollection) => true, delegate(DescendantsOf descendantsOf)
				{
					if (base.<VisitNonterminal>g__VisitChild|1<DescendantsOf, nodeCollection>(CS$<>8__locals1.n, new Func<ProgramNode, DescendantsOf?>(CssTranslationVisitor.Build.Node.AsRule.DescendantsOf), (DescendantsOf d) => d.nodeCollection))
					{
						CS$<>8__locals1.css.AddFrame(CssSelector.FrameType.Descendant);
						return true;
					}
					return false;
				}, delegate(RightSiblingOf rightSiblingOf)
				{
					if (base.<VisitNonterminal>g__VisitChild|1<RightSiblingOf, nodeCollection>(CS$<>8__locals1.n, new Func<ProgramNode, RightSiblingOf?>(CssTranslationVisitor.Build.Node.AsRule.RightSiblingOf), (RightSiblingOf r) => r.nodeCollection))
					{
						CS$<>8__locals1.css.AddFrame(CssSelector.FrameType.RightSibling);
						return true;
					}
					return false;
				}, delegate(ClassFilter classFilter)
				{
					if (base.<VisitNonterminal>g__VisitChild|1<ClassFilter, nodeCollection>(CS$<>8__locals1.n, new Func<ProgramNode, ClassFilter?>(CssTranslationVisitor.Build.Node.AsRule.ClassFilter), (ClassFilter cf) => cf.nodeCollection))
					{
						CS$<>8__locals1.css.Last().AddClass(classFilter.className.Value);
						return true;
					}
					return false;
				}, delegate(IDFilter iDFilter)
				{
					if (base.<VisitNonterminal>g__VisitChild|1<IDFilter, nodeCollection>(CS$<>8__locals1.n, new Func<ProgramNode, IDFilter?>(CssTranslationVisitor.Build.Node.AsRule.IDFilter), (IDFilter idf) => idf.nodeCollection))
					{
						CS$<>8__locals1.css.Last().AddAttributeValue("id", iDFilter.idName.Value, "=");
						return true;
					}
					return false;
				}, delegate(NodeNameFilter nodeNameFilter)
				{
					if (base.<VisitNonterminal>g__VisitChild|1<NodeNameFilter, nodeCollection>(CS$<>8__locals1.n, new Func<ProgramNode, NodeNameFilter?>(CssTranslationVisitor.Build.Node.AsRule.NodeNameFilter), (NodeNameFilter nnf) => nnf.nodeCollection))
					{
						CS$<>8__locals1.css.Last().AddTagName(nodeNameFilter.nodeName.Value);
						return true;
					}
					return false;
				}, delegate(ItemPropFilter itemPropFilter)
				{
					if (base.<VisitNonterminal>g__VisitChild|1<ItemPropFilter, nodeCollection>(CS$<>8__locals1.n, new Func<ProgramNode, ItemPropFilter?>(CssTranslationVisitor.Build.Node.AsRule.ItemPropFilter), (ItemPropFilter ipf) => ipf.nodeCollection))
					{
						CS$<>8__locals1.css.Last().AddAttributeValue("itemprop", itemPropFilter.propName.Value, "=");
						return true;
					}
					return false;
				}, delegate(NthChildFilter nthChildFilter)
				{
					if (base.<VisitNonterminal>g__VisitChild|1<NthChildFilter, nodeCollection>(CS$<>8__locals1.n, new Func<ProgramNode, NthChildFilter?>(CssTranslationVisitor.Build.Node.AsRule.NthChildFilter), (NthChildFilter ncf) => ncf.nodeCollection))
					{
						string text = string.Format(":nth-child({0})", nthChildFilter.idx1.Value);
						CS$<>8__locals1.css.Last().AddExtra(text);
						return true;
					}
					return false;
				}, delegate(NthLastChildFilter nthLastChildFilter)
				{
					if (base.<VisitNonterminal>g__VisitChild|1<NthLastChildFilter, nodeCollection>(CS$<>8__locals1.n, new Func<ProgramNode, NthLastChildFilter?>(CssTranslationVisitor.Build.Node.AsRule.NthLastChildFilter), (NthLastChildFilter nlcf) => nlcf.nodeCollection))
					{
						string text2 = string.Format(":nth-last-child({0})", nthLastChildFilter.idx2.Value);
						CS$<>8__locals1.css.Last().AddExtra(text2);
						return true;
					}
					return false;
				}))
				{
					return CS$<>8__locals1.css;
				}
			}
			else
			{
				atomExpr atomExpr;
				if (!CssTranslationVisitor.Build.Node.Is.atomExpr(CS$<>8__locals1.n, out atomExpr))
				{
					if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<literalExpr, atomExpr>(CS$<>8__locals1.n, new Func<ProgramNode, literalExpr?>(CssTranslationVisitor.Build.Node.As.literalExpr), (literalExpr literalExpr) => literalExpr.Cast_literalExpr_atomExpr().atomExpr))
					{
						if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<leafAtom, literalExpr>(CS$<>8__locals1.n, new Func<ProgramNode, leafAtom?>(CssTranslationVisitor.Build.Node.As.leafAtom), (leafAtom leafAtom) => leafAtom.Cast_leafAtom_literalExpr().literalExpr))
						{
							if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<leafFExpr_leafAtom, leafAtom>(CS$<>8__locals1.n, new Func<ProgramNode, leafFExpr_leafAtom?>(CssTranslationVisitor.Build.Node.AsRule.leafFExpr_leafAtom), (leafFExpr_leafAtom conv) => conv.leafAtom))
							{
								if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChildren|2<LeafAnd, leafFExpr, leafAtom>(CS$<>8__locals1.n, new Func<ProgramNode, LeafAnd?>(CssTranslationVisitor.Build.Node.AsRule.LeafAnd), (LeafAnd leafAnd) => leafAnd.leafFExpr, (LeafAnd leafAnd) => leafAnd.leafAtom))
								{
									if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<SingleSelection1, filterSelection>(CS$<>8__locals1.n, new Func<ProgramNode, SingleSelection1?>(CssTranslationVisitor.Build.Node.AsRule.SingleSelection1), (SingleSelection1 selection) => selection.filterSelection))
									{
										if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<SingleSelection2, filterSelection2>(CS$<>8__locals1.n, new Func<ProgramNode, SingleSelection2?>(CssTranslationVisitor.Build.Node.AsRule.SingleSelection2), (SingleSelection2 selection2) => selection2.filterSelection2))
										{
											if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<SingleSelection3, filterSelection3>(CS$<>8__locals1.n, new Func<ProgramNode, SingleSelection3?>(CssTranslationVisitor.Build.Node.AsRule.SingleSelection3), (SingleSelection3 selection3) => selection3.filterSelection3))
											{
												if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<SingleSelection4, filterSelection4>(CS$<>8__locals1.n, new Func<ProgramNode, SingleSelection4?>(CssTranslationVisitor.Build.Node.AsRule.SingleSelection4), (SingleSelection4 selection4) => selection4.filterSelection4))
												{
													if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<SingleSelection5, filterSelection5>(CS$<>8__locals1.n, new Func<ProgramNode, SingleSelection5?>(CssTranslationVisitor.Build.Node.AsRule.SingleSelection5), (SingleSelection5 selection5) => selection5.filterSelection5))
													{
														if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<LeafChildrenOf1, selection3>(CS$<>8__locals1.n, new Func<ProgramNode, LeafChildrenOf1?>(CssTranslationVisitor.Build.Node.AsRule.LeafChildrenOf1), (LeafChildrenOf1 children) => children.selection3))
														{
															if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<LeafChildrenOf2, selection5>(CS$<>8__locals1.n, new Func<ProgramNode, LeafChildrenOf2?>(CssTranslationVisitor.Build.Node.AsRule.LeafChildrenOf2), (LeafChildrenOf2 children) => children.selection5))
															{
																if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<LeafChildrenOf3, selection7>(CS$<>8__locals1.n, new Func<ProgramNode, LeafChildrenOf3?>(CssTranslationVisitor.Build.Node.AsRule.LeafChildrenOf3), (LeafChildrenOf3 children) => children.selection7))
																{
																	if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<LeafChildrenOf4, selection9>(CS$<>8__locals1.n, new Func<ProgramNode, LeafChildrenOf4?>(CssTranslationVisitor.Build.Node.AsRule.LeafChildrenOf4), (LeafChildrenOf4 children) => children.selection9))
																	{
																		if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<selection2_allNodes, allNodes>(CS$<>8__locals1.n, new Func<ProgramNode, selection2_allNodes?>(CssTranslationVisitor.Build.Node.AsRule.selection2_allNodes), (selection2_allNodes conv) => conv.allNodes))
																		{
																			if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<selection4_allNodes, allNodes>(CS$<>8__locals1.n, new Func<ProgramNode, selection4_allNodes?>(CssTranslationVisitor.Build.Node.AsRule.selection4_allNodes), (selection4_allNodes conv) => conv.allNodes))
																			{
																				if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<selection6_allNodes, allNodes>(CS$<>8__locals1.n, new Func<ProgramNode, selection6_allNodes?>(CssTranslationVisitor.Build.Node.AsRule.selection6_allNodes), (selection6_allNodes conv) => conv.allNodes))
																				{
																					if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<selection8_allNodes, allNodes>(CS$<>8__locals1.n, new Func<ProgramNode, selection8_allNodes?>(CssTranslationVisitor.Build.Node.AsRule.selection8_allNodes), (selection8_allNodes conv) => conv.allNodes))
																					{
																						if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<selection10_allNodes, allNodes>(CS$<>8__locals1.n, new Func<ProgramNode, selection10_allNodes?>(CssTranslationVisitor.Build.Node.AsRule.selection10_allNodes), (selection10_allNodes conv) => conv.allNodes))
																						{
																							if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChildren|2<LeafFilter1, selection2, leafFExpr>(CS$<>8__locals1.n, delegate(ProgramNode node)
																							{
																								if (CssTranslationVisitor.Build.Node.As.filterSelection(node) == null)
																								{
																									return null;
																								}
																								filterSelection? filterSelection;
																								return new LeafFilter1?(filterSelection.GetValueOrDefault().Cast_LeafFilter1());
																							}, (LeafFilter1 leafFilter1) => leafFilter1.selection2, (LeafFilter1 leafFilter1) => leafFilter1.leafFExpr))
																							{
																								if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChildren|2<LeafFilter2, selection4, leafFExpr>(CS$<>8__locals1.n, delegate(ProgramNode node)
																								{
																									if (CssTranslationVisitor.Build.Node.As.filterSelection2(node) == null)
																									{
																										return null;
																									}
																									filterSelection2? filterSelection2;
																									return new LeafFilter2?(filterSelection2.GetValueOrDefault().Cast_LeafFilter2());
																								}, (LeafFilter2 leafFilter2) => leafFilter2.selection4, (LeafFilter2 leafFilter2) => leafFilter2.leafFExpr))
																								{
																									if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChildren|2<LeafFilter3, selection6, leafFExpr>(CS$<>8__locals1.n, delegate(ProgramNode node)
																									{
																										if (CssTranslationVisitor.Build.Node.As.filterSelection3(node) == null)
																										{
																											return null;
																										}
																										filterSelection3? filterSelection3;
																										return new LeafFilter3?(filterSelection3.GetValueOrDefault().Cast_LeafFilter3());
																									}, (LeafFilter3 leafFilter3) => leafFilter3.selection6, (LeafFilter3 leafFilter3) => leafFilter3.leafFExpr))
																									{
																										if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChildren|2<LeafFilter4, selection8, leafFExpr>(CS$<>8__locals1.n, delegate(ProgramNode node)
																										{
																											if (CssTranslationVisitor.Build.Node.As.filterSelection4(node) == null)
																											{
																												return null;
																											}
																											filterSelection4? filterSelection4;
																											return new LeafFilter4?(filterSelection4.GetValueOrDefault().Cast_LeafFilter4());
																										}, (LeafFilter4 leafFilter4) => leafFilter4.selection8, (LeafFilter4 leafFilter4) => leafFilter4.leafFExpr))
																										{
																											if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChildren|2<LeafFilter5, selection10, leafFExpr>(CS$<>8__locals1.n, delegate(ProgramNode node)
																											{
																												if (CssTranslationVisitor.Build.Node.As.filterSelection5(node) == null)
																												{
																													return null;
																												}
																												filterSelection5? filterSelection5;
																												return new LeafFilter5?(filterSelection5.GetValueOrDefault().Cast_LeafFilter5());
																											}, (LeafFilter5 leafFilter5) => leafFilter5.selection10, (LeafFilter5 leafFilter5) => leafFilter5.leafFExpr))
																											{
																												if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<subNodeSequence, selection>(CS$<>8__locals1.n, new Func<ProgramNode, subNodeSequence?>(CssTranslationVisitor.Build.Node.As.subNodeSequence), (subNodeSequence subNodeSequence) => subNodeSequence.Cast_MapToWebRegion().selection))
																												{
																													if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<resultSequence_subNodeSequence, subNodeSequence>(CS$<>8__locals1.n, new Func<ProgramNode, resultSequence_subNodeSequence?>(CssTranslationVisitor.Build.Node.AsRule.resultSequence_subNodeSequence), (resultSequence_subNodeSequence conv) => conv.subNodeSequence))
																													{
																														if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<resultSequence_regionSequence, regionSequence>(CS$<>8__locals1.n, new Func<ProgramNode, resultSequence_regionSequence?>(CssTranslationVisitor.Build.Node.AsRule.resultSequence_regionSequence), (resultSequence_regionSequence conv) => conv.regionSequence))
																														{
																															if (!CS$<>8__locals1.<VisitNonterminal>g__VisitChild|1<resultSequence, nodeCollection>(CS$<>8__locals1.n, new Func<ProgramNode, resultSequence?>(CssTranslationVisitor.Build.Node.As.resultSequence), (resultSequence resultSequence) => resultSequence.Cast_ConvertToWebRegions(CssTranslationVisitor.Build).nodeCollection))
																															{
																																goto IL_0AC4;
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																							return CS$<>8__locals1.css;
																						}
																					}
																				}
																			}
																		}
																		return CS$<>8__locals1.css;
																	}
																}
															}
														}
														CS$<>8__locals1.css.AddFrame(CssSelector.FrameType.Child);
														return CS$<>8__locals1.css;
													}
												}
											}
										}
									}
								}
							}
						}
					}
					return CS$<>8__locals1.css;
				}
				if (atomExpr.Switch<bool>(CssTranslationVisitor.Build, (ContainsDate containsDate) => false, (ContainsNum containsNum) => false, delegate(ID_substring ID_substring)
				{
					CS$<>8__locals1.css.Last().AddAttributeValue("id", ID_substring.name.Value, "*=");
					return true;
				}, delegate(Class @class)
				{
					CS$<>8__locals1.css.Last().AddClass(@class.name.Value);
					return true;
				}, delegate(TitleIs titleIs)
				{
					CS$<>8__locals1.css.Last().AddAttributeValue("title", titleIs.name.Value, "=");
					return true;
				}, delegate(NodeName nodeName)
				{
					CS$<>8__locals1.css.Last().AddTagName(nodeName.name.Value);
					return true;
				}, delegate(NodeNames nodeNames)
				{
					CS$<>8__locals1.css.Last().AddTagNames(nodeNames.names.Value);
					return true;
				}, delegate(NthChild nthChild)
				{
					string text3 = string.Format(":nth-child({0})", nthChild.idx1.Value);
					CS$<>8__locals1.css.Last().AddExtra(text3);
					return true;
				}, delegate(NthLastChild nthLastChild)
				{
					string text4 = string.Format(":nth-last-child({0})", nthLastChild.idx2.Value);
					CS$<>8__locals1.css.Last().AddExtra(text4);
					return true;
				}, (ContainsLeafNodes containsLeafNodes) => false, (ChildrenCount childrenCount) => false, delegate(HasAttribute hasAttribute)
				{
					CS$<>8__locals1.css.Last().AddAttributeValue(hasAttribute.name.Value, hasAttribute.value.Value, "=");
					return true;
				}, delegate(HasStyle hasStyle)
				{
					CS$<>8__locals1.css.Last().AddAttributeValue("style", hasStyle.name.Value + ":" + hasStyle.value.Value, "*=");
					return true;
				}, (HasEntityAnchor hasEntityAnchor) => false))
				{
					return CS$<>8__locals1.css;
				}
			}
			IL_0AC4:
			throw new NotImplementedException("M-code generation does not support: " + CS$<>8__locals1.n.Rule.Id);
		}

		// Token: 0x06008601 RID: 34305 RVA: 0x001C2CFC File Offset: 0x001C0EFC
		public override CssSelector VisitLet(LetNode node, CssSelector css)
		{
			return this.VisitNonterminal(node, css);
		}

		// Token: 0x06008602 RID: 34306 RVA: 0x001C2D06 File Offset: 0x001C0F06
		public override CssSelector VisitLambda(LambdaNode node, CssSelector css)
		{
			node.BodyNode.AcceptVisitor<CssSelector, CssSelector>(this, css);
			return css;
		}

		// Token: 0x06008603 RID: 34307 RVA: 0x0003B61D File Offset: 0x0003981D
		public override CssSelector VisitLiteral(LiteralNode node, CssSelector css)
		{
			return css;
		}

		// Token: 0x06008604 RID: 34308 RVA: 0x0003B61D File Offset: 0x0003981D
		public override CssSelector VisitVariable(VariableNode node, CssSelector css)
		{
			return css;
		}

		// Token: 0x06008605 RID: 34309 RVA: 0x0003B61D File Offset: 0x0003981D
		public override CssSelector VisitHole(Hole node, CssSelector css)
		{
			return css;
		}

		// Token: 0x06008608 RID: 34312 RVA: 0x001C2D2C File Offset: 0x001C0F2C
		[CompilerGenerated]
		internal static bool <VisitNonterminal>g__VisitNode|1_0<TNode>(ProgramNode node, Func<ProgramNode, TNode?> asNode, Action<TNode> nodeAction) where TNode : struct
		{
			TNode? tnode = asNode(node);
			if (tnode != null)
			{
				nodeAction(tnode.Value);
				return true;
			}
			return false;
		}

		// Token: 0x04003748 RID: 14152
		private static readonly GrammarBuilders Build = Language.Build;
	}
}
