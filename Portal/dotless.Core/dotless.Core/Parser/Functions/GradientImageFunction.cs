using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using dotless.Core.Exceptions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200007A RID: 122
	public class GradientImageFunction : Function
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x00015F70 File Offset: 0x00014170
		protected override Node Evaluate(Env env)
		{
			GradientImageFunction.ColorPoint[] colorPoints = this.GetColorPoints();
			base.WarnNotSupportedByLessJS("gradientImage(color, color[, position])");
			string text = GradientImageFunction.ColorPoint.Stringify(colorPoints);
			string text2 = GradientImageFunction.GetFromCache(text);
			if (text2 == null)
			{
				text2 = "data:image/png;base64," + Convert.ToBase64String(this.GetImageData(colorPoints));
				GradientImageFunction.AddToCache(text, text2);
			}
			return new Url(new TextNode(text2));
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00015FCC File Offset: 0x000141CC
		private byte[] GetImageData(GradientImageFunction.ColorPoint[] points)
		{
			GradientImageFunction.ColorPoint colorPoint = points.Last<GradientImageFunction.ColorPoint>();
			int num = colorPoint.Position + 1;
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (Bitmap bitmap = new Bitmap(1, num, PixelFormat.Format32bppArgb))
				{
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						for (int i = 1; i < points.Length; i++)
						{
							Rectangle rectangle = new Rectangle(0, points[i - 1].Position, 1, points[i].Position);
							LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, points[i - 1].Color, points[i].Color, LinearGradientMode.Vertical);
							graphics.FillRectangle(linearGradientBrush, rectangle);
						}
						bitmap.SetPixel(0, colorPoint.Position, colorPoint.Color);
						bitmap.Save(memoryStream, ImageFormat.Png);
					}
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000160D4 File Offset: 0x000142D4
		private GradientImageFunction.ColorPoint[] GetColorPoints()
		{
			int count = base.Arguments.Count;
			Guard.ExpectMinArguments(2, count, this, base.Location);
			Guard.ExpectAllNodes<dotless.Core.Parser.Tree.Color>(base.Arguments.Take(2), this, base.Location);
			dotless.Core.Parser.Tree.Color color = (dotless.Core.Parser.Tree.Color)base.Arguments[0];
			List<GradientImageFunction.ColorPoint> list = new List<GradientImageFunction.ColorPoint>
			{
				new GradientImageFunction.ColorPoint((global::System.Drawing.Color)color, 0)
			};
			int num = 0;
			int num2 = 50;
			for (int i = 1; i < count; i++)
			{
				Node node = base.Arguments[i];
				Guard.ExpectNode<dotless.Core.Parser.Tree.Color>(node, this, base.Location);
				dotless.Core.Parser.Tree.Color color2 = node as dotless.Core.Parser.Tree.Color;
				int num3 = num + num2;
				if (i < count - 1)
				{
					Number number = base.Arguments[i + 1] as Number;
					if (number)
					{
						num3 = Convert.ToInt32(number.Value);
						if (num3 <= num)
						{
							throw new ParsingException(string.Format("Incrementing color point position expected, at least {0}, found {1}", num + 1, number.Value), base.Location);
						}
						num2 = num3 - num;
						i++;
					}
				}
				list.Add(new GradientImageFunction.ColorPoint((global::System.Drawing.Color)color2, num3));
				num = num3;
			}
			return list.ToArray();
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00016210 File Offset: 0x00014410
		private static string GetFromCache(string colorDefs)
		{
			GradientImageFunction._cacheLock.EnterReadLock();
			string text;
			try
			{
				GradientImageFunction.CacheItem cacheItem = GradientImageFunction._cache.FirstOrDefault((GradientImageFunction.CacheItem item) => item._def == colorDefs);
				text = ((cacheItem != null) ? cacheItem._url : null);
			}
			finally
			{
				GradientImageFunction._cacheLock.ExitReadLock();
			}
			return text;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00016278 File Offset: 0x00014478
		private static void AddToCache(string colorDefs, string imageUrl)
		{
			GradientImageFunction._cacheLock.EnterUpgradeableReadLock();
			try
			{
				if (GradientImageFunction._cache.All((GradientImageFunction.CacheItem item) => item._def != colorDefs))
				{
					GradientImageFunction._cacheLock.EnterWriteLock();
					try
					{
						if (GradientImageFunction._cache.Count >= 50)
						{
							GradientImageFunction._cache.RemoveRange(0, 25);
						}
						GradientImageFunction.CacheItem cacheItem = new GradientImageFunction.CacheItem(colorDefs, imageUrl);
						GradientImageFunction._cache.Add(cacheItem);
					}
					finally
					{
						GradientImageFunction._cacheLock.ExitWriteLock();
					}
				}
			}
			finally
			{
				GradientImageFunction._cacheLock.ExitUpgradeableReadLock();
			}
		}

		// Token: 0x040000F0 RID: 240
		public const int DEFAULT_COLOR_OFFSET = 50;

		// Token: 0x040000F1 RID: 241
		private const int CACHE_LIMIT = 50;

		// Token: 0x040000F2 RID: 242
		private static readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();

		// Token: 0x040000F3 RID: 243
		private static readonly List<GradientImageFunction.CacheItem> _cache = new List<GradientImageFunction.CacheItem>();

		// Token: 0x02000115 RID: 277
		private class ColorPoint
		{
			// Token: 0x060006D8 RID: 1752 RVA: 0x00019D42 File Offset: 0x00017F42
			public ColorPoint(global::System.Drawing.Color color, int position)
			{
				this.Color = color;
				this.Position = position;
			}

			// Token: 0x060006D9 RID: 1753 RVA: 0x00019D58 File Offset: 0x00017F58
			public static string Stringify(IEnumerable<GradientImageFunction.ColorPoint> points)
			{
				return points.Aggregate("", (string s, GradientImageFunction.ColorPoint point) => string.Format("{0}{1}#{2:X2}{3:X2}{4:X2}{5:X2},{6}", new object[]
				{
					s,
					(s == "") ? "" : ",",
					point.Color.A,
					point.Color.R,
					point.Color.G,
					point.Color.B,
					point.Position
				}));
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x060006DA RID: 1754 RVA: 0x00019D84 File Offset: 0x00017F84
			// (set) Token: 0x060006DB RID: 1755 RVA: 0x00019D8C File Offset: 0x00017F8C
			public global::System.Drawing.Color Color { get; private set; }

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x060006DC RID: 1756 RVA: 0x00019D95 File Offset: 0x00017F95
			// (set) Token: 0x060006DD RID: 1757 RVA: 0x00019D9D File Offset: 0x00017F9D
			public int Position { get; private set; }
		}

		// Token: 0x02000116 RID: 278
		private class CacheItem
		{
			// Token: 0x060006DE RID: 1758 RVA: 0x00019DA6 File Offset: 0x00017FA6
			public CacheItem(string def, string url)
			{
				this._def = def;
				this._url = url;
			}

			// Token: 0x040001FF RID: 511
			public readonly string _def;

			// Token: 0x04000200 RID: 512
			public readonly string _url;
		}
	}
}
