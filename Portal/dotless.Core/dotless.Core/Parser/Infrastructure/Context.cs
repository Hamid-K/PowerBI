using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Infrastructure
{
	// Token: 0x0200004F RID: 79
	public class Context : IEnumerable<IEnumerable<Selector>>, IEnumerable
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000E46B File Offset: 0x0000C66B
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000E473 File Offset: 0x0000C673
		private List<List<Selector>> Paths { get; set; }

		// Token: 0x0600032C RID: 812 RVA: 0x0000E47C File Offset: 0x0000C67C
		public Context()
		{
			this.Paths = new List<List<Selector>>();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000E490 File Offset: 0x0000C690
		public Context Clone()
		{
			List<List<Selector>> list = new List<List<Selector>>();
			return new Context
			{
				Paths = list
			};
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000E4B0 File Offset: 0x0000C6B0
		public void AppendSelectors(Context context, IEnumerable<Selector> selectors)
		{
			foreach (Selector selector in selectors)
			{
				this.AppendSelector(context, selector);
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		private void AppendSelector(Context context, Selector selector)
		{
			if (selector.Elements.Any((Element e) => e.Value == "&"))
			{
				NodeList<Element> nodeList = new NodeList<Element>();
				List<List<Selector>> list = new List<List<Selector>>
				{
					new List<Selector>()
				};
				foreach (Element element in selector.Elements)
				{
					if (element.Value != "&")
					{
						nodeList.Add(element);
					}
					else
					{
						List<List<Selector>> list2 = new List<List<Selector>>();
						if (nodeList.Count > 0)
						{
							this.MergeElementsOnToSelectors(nodeList, list);
						}
						foreach (List<Selector> list3 in list)
						{
							if (context.Paths.Count == 0)
							{
								if (list3.Count > 0)
								{
									list3[0].Elements = new NodeList<Element>(list3[0].Elements);
									list3[0].Elements.Add(new Element(element.Combinator, ""));
								}
								list2.Add(list3);
							}
							else
							{
								foreach (List<Selector> list4 in context.Paths)
								{
									List<Selector> list5 = new List<Selector>();
									List<Selector> list6 = new List<Selector>();
									bool flag = true;
									Selector selector2;
									if (list3.Count > 0)
									{
										selector2 = new Selector(new NodeList<Element>(list3[list3.Count - 1].Elements));
										list5.AddRange(list3.Take(list3.Count - 1));
										flag = false;
									}
									else
									{
										selector2 = new Selector(new NodeList<Element>());
									}
									if (list4.Count > 1)
									{
										list6.AddRange(list4.Skip(1));
									}
									if (list4.Count > 0)
									{
										flag = false;
										if (list4[0].Elements[0].Value == null)
										{
											selector2.Elements.Add(new Element(element.Combinator, list4[0].Elements[0].NodeValue));
										}
										else
										{
											selector2.Elements.Add(new Element(element.Combinator, list4[0].Elements[0].Value));
										}
										selector2.Elements.AddRange(list4[0].Elements.Skip(1));
									}
									if (!flag)
									{
										list5.Add(selector2);
									}
									list5.AddRange(list6);
									list2.Add(list5);
								}
							}
						}
						list = list2;
						nodeList = new NodeList<Element>();
					}
				}
				if (nodeList.Count > 0)
				{
					this.MergeElementsOnToSelectors(nodeList, list);
				}
				this.Paths.AddRange(list.Select((List<Selector> sel) => sel.Select(new Func<Selector, Selector>(this.MergeJoinedElements)).ToList<Selector>()));
				return;
			}
			if (context != null && context.Paths.Count > 0)
			{
				this.Paths.AddRange(context.Paths.Select((List<Selector> path) => path.Concat(new Selector[] { selector }).ToList<Selector>()));
				return;
			}
			this.Paths.Add(new List<Selector> { selector });
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		private void MergeElementsOnToSelectors(NodeList<Element> elements, List<List<Selector>> selectors)
		{
			if (selectors.Count == 0)
			{
				selectors.Add(new List<Selector>
				{
					new Selector(elements)
				});
				return;
			}
			foreach (List<Selector> list in selectors)
			{
				if (list.Count > 0)
				{
					list[list.Count - 1] = new Selector(list[list.Count - 1].Elements.Concat(elements));
				}
				else
				{
					list.Add(new Selector(elements));
				}
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000E96C File Offset: 0x0000CB6C
		private Selector MergeJoinedElements(Selector selector)
		{
			List<Element> list = selector.Elements.Select((Element e) => e.Clone()).ToList<Element>();
			for (int i = 1; i < list.Count; i++)
			{
				Element element = list[i - 1];
				Element element2 = list[i];
				if (!string.IsNullOrEmpty(element2.Value) && !Context.LeaveUnmerged.Contains(element2.Value[0]) && !(element2.Combinator.Value != ""))
				{
					List<Element> list2 = list;
					int num = i - 1;
					Combinator combinator = element.Combinator;
					Element element3 = element;
					list2[num] = new Element(combinator, element3.Value += element2.Value);
					list.RemoveAt(i);
					i--;
				}
			}
			return new Selector(list);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000EA50 File Offset: 0x0000CC50
		public void AppendCSS(Env env)
		{
			Func<Selector, string> <>9__3;
			IEnumerable<string> enumerable = this.Paths.Where((List<Selector> p) => p.Any((Selector s) => !s.IsReference)).Select(delegate(List<Selector> path)
			{
				Func<Selector, string> func;
				if ((func = <>9__3) == null)
				{
					func = (<>9__3 = (Selector p) => p.ToCSS(env));
				}
				return path.Select(func).JoinStrings("").Trim();
			}).Distinct<string>();
			env.Output.AppendMany(enumerable, env.Compress ? "," : ",\n");
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000EAD8 File Offset: 0x0000CCD8
		public string ToCss(Env env)
		{
			Func<Selector, string> <>9__1;
			return string.Join(env.Compress ? "," : ",\n", this.Paths.Select(delegate(List<Selector> path)
			{
				Func<Selector, string> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (Selector p) => p.ToCSS(env));
				}
				return path.Select(func).JoinStrings("").Trim();
			}).ToArray<string>());
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		public int Count
		{
			get
			{
				return this.Paths.Count;
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000EB39 File Offset: 0x0000CD39
		public IEnumerator<IEnumerable<Selector>> GetEnumerator()
		{
			return this.Paths.Cast<IEnumerable<Selector>>().GetEnumerator();
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000EB4B File Offset: 0x0000CD4B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040000BD RID: 189
		private static readonly char[] LeaveUnmerged = new char[] { '.', '#', ':' };
	}
}
