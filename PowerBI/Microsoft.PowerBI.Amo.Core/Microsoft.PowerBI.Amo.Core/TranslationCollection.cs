using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000C8 RID: 200
	[Guid("2F2699BD-B28C-4d39-A0B1-D76EEF27B7E1")]
	public class TranslationCollection : ModelComponentCollection
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x000299D6 File Offset: 0x00027BD6
		internal TranslationCollection(ModelComponent parent)
			: base(parent)
		{
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x000299DF File Offset: 0x00027BDF
		protected override Type ItemsType
		{
			get
			{
				return typeof(Translation);
			}
		}

		// Token: 0x17000231 RID: 561
		public Translation this[int index]
		{
			get
			{
				return (Translation)base[index];
			}
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000299F9 File Offset: 0x00027BF9
		public int Add(Translation item)
		{
			return base.Add(item);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00029A04 File Offset: 0x00027C04
		public Translation Add(int language)
		{
			Translation translation = new Translation(language);
			base.Add(translation);
			return translation;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00029A21 File Offset: 0x00027C21
		public void Insert(int index, Translation item)
		{
			base.Insert(index, item);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00029A2C File Offset: 0x00027C2C
		public Translation Insert(int index, int language)
		{
			if (index < 0 || index > base.Count)
			{
				throw new ArgumentOutOfRangeException("index", index, SR.Collections_IndexOutOfRangeException);
			}
			Translation translation = new Translation(language);
			base.Insert(index, translation);
			return translation;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00029A6C File Offset: 0x00027C6C
		public void Remove(Translation item)
		{
			base.Remove(item, true);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00029A76 File Offset: 0x00027C76
		public void Remove(Translation item, bool cleanUp)
		{
			base.Remove(item, cleanUp);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00029A80 File Offset: 0x00027C80
		public void Remove(int language)
		{
			base.Remove(language.ToString(CultureInfo.InvariantCulture), true);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00029A95 File Offset: 0x00027C95
		public void Remove(int language, bool cleanUp)
		{
			base.Remove(language.ToString(CultureInfo.InvariantCulture), cleanUp);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00029AAA File Offset: 0x00027CAA
		public new Translation Move(int fromIndex, int toIndex)
		{
			return (Translation)base.Move(fromIndex, toIndex);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00029AB9 File Offset: 0x00027CB9
		public void Move(Translation item, int toIndex)
		{
			base.Move(item, toIndex);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00029AC3 File Offset: 0x00027CC3
		public bool Contains(Translation item)
		{
			return base.Contains(item);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00029ACC File Offset: 0x00027CCC
		public bool Contains(int language)
		{
			return base.Contains(language.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00029AE0 File Offset: 0x00027CE0
		public int IndexOf(Translation item)
		{
			return base.IndexOf(item);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00029AE9 File Offset: 0x00027CE9
		public int IndexOf(int language)
		{
			return base.IndexOf(language.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00029AFD File Offset: 0x00027CFD
		public Translation GetByLanguage(int language)
		{
			return (Translation)base.GetItem(language.ToString(CultureInfo.InvariantCulture), true, "Language");
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00029B1C File Offset: 0x00027D1C
		public Translation FindByLanguage(int language)
		{
			return (Translation)base.GetItem(language.ToString(CultureInfo.InvariantCulture), false, null);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00029B38 File Offset: 0x00027D38
		public Translation Add(int language, string caption)
		{
			Translation translation = new Translation(language, caption);
			this.Add(translation);
			return translation;
		}
	}
}
