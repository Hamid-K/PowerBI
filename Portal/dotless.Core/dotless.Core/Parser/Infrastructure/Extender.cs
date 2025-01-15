using System;
using System.Collections.Generic;
using System.Linq;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Infrastructure
{
	// Token: 0x02000055 RID: 85
	public class Extender
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000F912 File Offset: 0x0000DB12
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x0000F91A File Offset: 0x0000DB1A
		public Selector BaseSelector { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000F923 File Offset: 0x0000DB23
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000F92B File Offset: 0x0000DB2B
		public List<Selector> ExtendedBy { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000F934 File Offset: 0x0000DB34
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0000F93C File Offset: 0x0000DB3C
		public bool IsReference { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000F945 File Offset: 0x0000DB45
		// (set) Token: 0x060003AD RID: 941 RVA: 0x0000F94D File Offset: 0x0000DB4D
		public bool IsMatched { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000F956 File Offset: 0x0000DB56
		// (set) Token: 0x060003AF RID: 943 RVA: 0x0000F95E File Offset: 0x0000DB5E
		public Extend Extend { get; private set; }

		// Token: 0x060003B0 RID: 944 RVA: 0x0000F967 File Offset: 0x0000DB67
		[Obsolete("Use the overload that accepts the Extend node")]
		public Extender(Selector baseSelector)
		{
			this.BaseSelector = baseSelector;
			this.ExtendedBy = new List<Selector>();
			this.IsReference = baseSelector.IsReference;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000F98D File Offset: 0x0000DB8D
		public Extender(Selector baseSelector, Extend extend)
			: this(baseSelector)
		{
			this.Extend = extend;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000F99D File Offset: 0x0000DB9D
		public static string FullPathSelector()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000F9A4 File Offset: 0x0000DBA4
		public void AddExtension(Selector selector, Env env)
		{
			List<IEnumerable<Selector>> list = new List<IEnumerable<Selector>> { new Selector[] { selector } };
			list.AddRange(from f in env.Frames.Skip(1)
				select from partialSelector in f.Selectors
					where partialSelector != null
					select partialSelector);
			this.ExtendedBy.Add(this.GenerateExtenderSelector(env, list));
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000FA14 File Offset: 0x0000DC14
		private Selector GenerateExtenderSelector(Env env, List<IEnumerable<Selector>> selectorPath)
		{
			Context context = this.GenerateExtenderSelector(selectorPath);
			return new Selector(new Element[]
			{
				new Element(null, context.ToCss(env))
			})
			{
				IsReference = this.IsReference
			};
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000FA50 File Offset: 0x0000DC50
		private Context GenerateExtenderSelector(List<IEnumerable<Selector>> selectorStack)
		{
			if (!selectorStack.Any<IEnumerable<Selector>>())
			{
				return null;
			}
			Context context = this.GenerateExtenderSelector(selectorStack.Skip(1).ToList<IEnumerable<Selector>>());
			Context context2 = new Context();
			context2.AppendSelectors(context, selectorStack.First<IEnumerable<Selector>>());
			return context2;
		}
	}
}
