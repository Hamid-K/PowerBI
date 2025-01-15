using System;
using System.Collections.Generic;
using AngleSharp.Css;
using AngleSharp.Dom.Css;

namespace AngleSharp.Services.Default
{
	// Token: 0x02000045 RID: 69
	public class AttributeSelectorFactory : IAttributeSelectorFactory
	{
		// Token: 0x06000162 RID: 354 RVA: 0x0000736B File Offset: 0x0000556B
		public void Register(string combinator, AttributeSelectorFactory.Creator creator)
		{
			this._creators.Add(combinator, creator);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000737C File Offset: 0x0000557C
		public AttributeSelectorFactory.Creator Unregister(string combinator)
		{
			AttributeSelectorFactory.Creator creator = null;
			if (this._creators.TryGetValue(combinator, out creator))
			{
				this._creators.Remove(combinator);
			}
			return creator;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000073A9 File Offset: 0x000055A9
		protected virtual ISelector CreateDefault(string name, string value, string prefix, bool insensitive)
		{
			return SimpleSelector.AttrAvailable(name, value, false);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000073B4 File Offset: 0x000055B4
		public ISelector Create(string combinator, string name, string value, string prefix, bool insensitive)
		{
			AttributeSelectorFactory.Creator creator = null;
			if (this._creators.TryGetValue(combinator, out creator))
			{
				return creator(name, value, prefix, insensitive);
			}
			return this.CreateDefault(name, value, prefix, insensitive);
		}

		// Token: 0x040001BF RID: 447
		private readonly Dictionary<string, AttributeSelectorFactory.Creator> _creators = new Dictionary<string, AttributeSelectorFactory.Creator>
		{
			{
				CombinatorSymbols.Exactly,
				new AttributeSelectorFactory.Creator(SimpleSelector.AttrMatch)
			},
			{
				CombinatorSymbols.InList,
				new AttributeSelectorFactory.Creator(SimpleSelector.AttrList)
			},
			{
				CombinatorSymbols.InToken,
				new AttributeSelectorFactory.Creator(SimpleSelector.AttrHyphen)
			},
			{
				CombinatorSymbols.Begins,
				new AttributeSelectorFactory.Creator(SimpleSelector.AttrBegins)
			},
			{
				CombinatorSymbols.Ends,
				new AttributeSelectorFactory.Creator(SimpleSelector.AttrEnds)
			},
			{
				CombinatorSymbols.InText,
				new AttributeSelectorFactory.Creator(SimpleSelector.AttrContains)
			},
			{
				CombinatorSymbols.Unlike,
				new AttributeSelectorFactory.Creator(SimpleSelector.AttrNotMatch)
			}
		};

		// Token: 0x02000422 RID: 1058
		// (Invoke) Token: 0x0600213A RID: 8506
		public delegate ISelector Creator(string name, string value, string prefix, bool insensitive);
	}
}
